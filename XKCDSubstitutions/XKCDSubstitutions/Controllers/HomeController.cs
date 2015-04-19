using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;

namespace XKCDSubstitutions.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetContent(string url)
        {
            var result = doGet(url);
            return string.IsNullOrEmpty(result.Result) ? "" : replaceWords(processGetResponse(result.Result, url));
        }

        private string cleanURL(string url)
        {
            int index = url.LastIndexOf("/");
            if (index != url.Length - 1)
            {
                url = url.Remove(index + 1, url.Length - index - 1);
            }
            return url;
        }

        private string replaceWords(string page)
        {
            var wordsToReplace = new List<string>("congressman.Marilyn Monroe.weeks.EU.case against.microsoft.apple.google.billion.million.senate.democrats.republicans.witnesses.allegedy.new study.rebuild.space.google glass.smartphone.electric.senator.car.election.congressional leaders.homeland security.could not be reached for comment".Split('.'));
            var replacementWords = new List<string>("pirate.Justin Beiber.fortnights.dat EU.duel against.a former Bill Gates company.a former Steve Jobs company.our lord and savior google.ba-jillion.zillion.jedi council.monkeys.giraffes.these dudes I know.kinda probably.tumblr post.avenge.spaaace.virtual boy.pokedex.atomic.elf-lord.cat.eating contest.river spirits.homestar runner.is guilty and everyone knows it".Split('.'));
            for (var i = 0; i < wordsToReplace.Count; ++i)
            {
                page = ReplaceString(page, wordsToReplace[i], "<b>" + replacementWords[i] + "</b>", StringComparison.OrdinalIgnoreCase);
            }
            return page;
        }

        private async Task<string> doGet(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var data = httpClient.GetAsync(url);
                return await data.Result.Content.ReadAsStringAsync();
            }
        }

        private string processGetResponse(string response, string srcURL)
        {
            srcURL = cleanURL(srcURL);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);
            foreach (HtmlNode stylesheet in doc.DocumentNode.SelectNodes("//link[@href]"))
            {
                HtmlAttribute att = stylesheet.Attributes["href"];
                if (!att.Value.Contains("http"))
                {
                    att.Value = srcURL + att.Value;
                }
            }
            foreach (HtmlNode image in doc.DocumentNode.SelectNodes("//img[@src]"))
            {
                HtmlAttribute att = image.Attributes["src"];
                if (att.Value.Contains("../"))
                {
                    att.Value = srcURL + att.Value;
                }
            }
            foreach (HtmlNode script in doc.DocumentNode.SelectNodes("//script[@src]"))
            {
                HtmlAttribute att = script.Attributes["src"];
                if (!att.Value.Contains("http"))
                {
                    att.Value = srcURL + att.Value;
                }
            }
            return doc.DocumentNode.OuterHtml;
        }

        public static string ReplaceString(string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }
    }
}