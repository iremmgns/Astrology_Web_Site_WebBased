using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.Services;
using Newtonsoft.Json.Linq;

namespace web_based_astrology_web_site
{
    public partial class Tarot : System.Web.UI.Page
    {
        private const string ImagePath = "/cards/";
        private const string JsonFilePath = "~/App_Data/tarot-images.json";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnDrawCards_Click(object sender, EventArgs e)
        {
            var json = File.ReadAllText(Server.MapPath(JsonFilePath));
            var tarotData = JObject.Parse(json);
            var cards = tarotData["cards"].ToList();

            var random = new Random();
            var selectedCards = cards.OrderBy(x => random.Next()).Take(3).ToList();

            cardsContainer.InnerHtml = string.Empty;

            foreach (var card in selectedCards)
            {
                var img = ImagePath + card["img"];
                var name = card["name"].ToString();
                var fortuneTelling = string.Join("<br/>", card["fortune_telling"].Select(x => x.ToString()));

                cardsContainer.InnerHtml += $@"
                    <div class='tarot-card'>
                        <img src='{img}' alt='{name}' />
                        <h3>{name}</h3>
                        <p>{fortuneTelling}</p>
                    </div>";
            }
        }
    }
}