using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Spoj.Notifier
{
    public static class SendUnsolvedReminder
    {
        [FunctionName(nameof(SendUnsolvedReminder))]
        public static async Task Run(
            [TimerTrigger("0 0 9 * * MON")]TimerInfo timer, // Every Monday @ 9:00 AM
            [SendGrid] IAsyncCollector<SendGridMessage> messageCollector,
            ILogger log)
        {
            var browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

            string profileUrl = "https://www.spoj.com/users/davidgalehouse";
            var profileDocument = await browsingContext.OpenAsync(profileUrl);
            var solvedProblems = profileDocument
                .QuerySelector("#user-profile-tables table")
                .QuerySelectorAll("tr td a")
                .Select(e => e.TextContent?.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToHashSet();
            log.LogInformation($"Profile URL opened, {solvedProblems.Count} solved problems found.");

            string problems151to200Url = "https://www.spoj.com/problems/classical/sort=6,start=150";
            var problems151to200Document = await browsingContext.OpenAsync(problems151to200Url);
            var problems151to200 = problems151to200Document
                .QuerySelectorAll("table.problems tbody tr")
                .Select(e => e.QuerySelector("a"))
                .OfType<IHtmlAnchorElement>()
                .Select(a => a.Href.Split('/', StringSplitOptions.RemoveEmptyEntries).Last()?.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToArray();
            log.LogInformation($"Problems 151 to 200 URL opened, {problems151to200.Length} problems found " +
                $"(first problem: {problems151to200.FirstOrDefault()}, last problem: {problems151to200.LastOrDefault()}).");

            if (solvedProblems.Count < 200
                || solvedProblems.Distinct().Count() != solvedProblems.Count
                || solvedProblems.Any(p => !IsValidProblem(p))
                || problems151to200.Length != 50
                || problems151to200.Distinct().Count() != 50
                || problems151to200.Any(p => !IsValidProblem(p)))
                throw new Exception("There's something wrong with the way SPOJ is being scraped.");

            var unsolvedProblems = problems151to200
                .Select((p, i) => (problem: p, index: 151 + i))
                .Where(p => !solvedProblems.Contains(p.problem))
                .ToArray();
            if (unsolvedProblems.Any())
            {
                log.LogInformation($"Unsolved problems found: {string.Join(", ", unsolvedProblems)}.");

                var message = new SendGridMessage();
                message.AddTo(Environment.GetEnvironmentVariable("SendGridMessageTo"));
                message.SetSubject(unsolvedProblems.Length == 1 ? "A problem from SPOJ's top 200 needs to be solved!"
                    : $"{unsolvedProblems.Length} problems from SPOJ's top 200 need to be solved!");
                message.AddContent("text/plain", string.Join(Environment.NewLine, unsolvedProblems
                    .Select(p => $"#{p.index}: https://www.spoj.com/problems/{p.problem}")));
                await messageCollector.AddAsync(message);
            }
        }

        private static bool IsValidProblem(string problem)
            => problem.All(c => char.IsLetterOrDigit(c) || c == '_')
            && problem == problem.ToUpperInvariant();
    }
}
