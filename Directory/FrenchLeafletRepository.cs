using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Directory
{
    public static class FrenchLeafletRepository
    {
        private static readonly string BaseUrl= $"http://agence-prd.ansm.sante.fr/php/ecodex/";

        public static async Task<ILeaflet> GetSideEffectFor(string drugNoticeDocumentId)
        {
            var url = $"{BaseUrl}notice/N{drugNoticeDocumentId}.htm";
       
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            var doc = new HtmlDocument();
            doc.LoadHtml(pageContents);
            var body = doc.DocumentNode.SelectSingleNode("//body");

            return new Leaflet(pageContents)
            {
                SideEffects = GetDrugSideEffects(body),
                Information = GetDrugInformationAndWarnings(body),
                Posology = GetUsualPosology(body),
                Description = GetDrugDescriptionsFrom(body)
            };
        }

        private static string GetDrugSideEffects(HtmlNode body)
        {
            var sideEffectsParagraphNodes = body.SelectNodes("//p")
                .SkipWhile(p => !p.InnerHtml.Contains("Ann3bEffetsIndesirables")).ToArray();

            var sb = new StringBuilder();

            foreach (var item in sideEffectsParagraphNodes)
            {
                if (item.InnerHtml.Contains("Ann3bConservation"))
                {
                    break;
                }

                sb.AppendLine(item.InnerText);
                Console.WriteLine(item.InnerText);
            }

            var sideEffects = sb.ToString();
            return sideEffects;
        }

        private static string GetUsualPosology(HtmlNode body)
        {
            var posologyParagraphNodes =
                body.SelectNodes("//p").SkipWhile(p => !p.InnerHtml.Contains("Ann3bCommentPrendre")).ToArray();
            var posologySb = new StringBuilder();

            foreach (var item in posologyParagraphNodes)
            {
                if (item.InnerHtml.Contains("Ann3bEffetsIndesirables"))
                {
                    break;
                }

                posologySb.AppendLine(item.InnerText);
                Console.WriteLine(item.InnerText);
            }

            var posology = posologySb.ToString();
            return posology;
        }

        private static string GetDrugInformationAndWarnings(HtmlNode body)
        {
            var warningsParagraphNodes =
                body.SelectNodes("//p").SkipWhile(p => !p.InnerHtml.Contains("Ann3bInfoNecessaires")).ToArray();
            var warningsSb = new StringBuilder();

            foreach (var item in warningsParagraphNodes)
            {
                if (item.InnerHtml.Contains("Ann3bCommentPrendre"))
                {
                    break;
                }

                warningsSb.AppendLine(item.InnerText);
                Console.WriteLine(item.InnerText);
            }

            var information = warningsSb.ToString();
            return information;
        }

        private static string GetDrugDescriptionsFrom(HtmlNode body)
        {
            var descriptionParagraphNodes =
                body.SelectNodes("//p").SkipWhile(p => !p.InnerHtml.Contains("Ann3bQuestceque")).ToArray();
            var descriptionsSb = new StringBuilder();

            foreach (var item in descriptionParagraphNodes)
            {
                if (item.InnerHtml.Contains("Ann3bInfoNecessaires"))
                {
                    break;
                }

                descriptionsSb.AppendLine(item.InnerText);
                Console.WriteLine(item.InnerText);
            }

            var descriptions = descriptionsSb.ToString();
            return descriptions;
        }
    }
}