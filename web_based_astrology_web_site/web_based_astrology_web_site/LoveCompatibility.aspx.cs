using System;
using System.Collections.Generic;

namespace web_based_astrology_web_site
{
    public partial class LoveCompatibility : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            string yourSign = ddlYourSign.SelectedValue;
            string partnerSign = ddlPartnerSign.SelectedValue;

            int compatibilityScore = CalculateCompatibility(yourSign, partnerSign);

            lblResult.Text = "Uyum Skoru: " + compatibilityScore + "%";
        }

        private int CalculateCompatibility(string yourSign, string partnerSign)
        {
            Dictionary<string, (string element, string quality)> zodiacTraits = new Dictionary<string, (string, string)>()
            {
                { "Koç", ("Ateş", "Kardinal") },
                { "Boğa", ("Toprak", "Sabit") },
                { "İkizler", ("Hava", "Değişken") },
                { "Yengeç", ("Su", "Kardinal") },
                { "Aslan", ("Ateş", "Sabit") },
                { "Başak", ("Toprak", "Değişken") },
                { "Terazi", ("Hava", "Kardinal") },
                { "Akrep", ("Su", "Sabit") },
                { "Yay", ("Ateş", "Değişken") },
                { "Oğlak", ("Toprak", "Kardinal") },
                { "Kova", ("Hava", "Sabit") },
                { "Balık", ("Su", "Değişken") }
            };

            var yourTraits = zodiacTraits[yourSign];
            var partnerTraits = zodiacTraits[partnerSign];

            int score = 0;

            // Element uyumu
            if (yourTraits.element == partnerTraits.element)
            {
                score += 50; // Aynı element %50 uyum sağlar
            }

            // Nitelik uyumu
            if (yourTraits.quality == partnerTraits.quality)
            {
                score += 30; // Aynı nitelik %30 uyum sağlar
            }

            // Kombinasyon uyumu
            if ((yourTraits.element == "Ateş" && partnerTraits.element == "Hava") ||
                (yourTraits.element == "Hava" && partnerTraits.element == "Ateş") ||
                (yourTraits.element == "Toprak" && partnerTraits.element == "Su") ||
                (yourTraits.element == "Su" && partnerTraits.element == "Toprak"))
            {
                score += 20; // Uyumlu kombinasyonlar %20 uyum sağlar
            }

            return score;
        }
    }
}
