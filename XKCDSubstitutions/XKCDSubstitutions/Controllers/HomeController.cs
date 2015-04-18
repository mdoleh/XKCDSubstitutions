using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public string GetContent(string url)
        {
            var result = doGet(url);
            return processGetResponse(result.Result, url);
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
    }
}