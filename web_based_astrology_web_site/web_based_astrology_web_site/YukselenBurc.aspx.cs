using System;
using System.Data.OleDb;

namespace web_based_astrology_web_site
{
    public partial class YukselenBurc : System.Web.UI.Page
    {
        private static readonly string[,] ascendantTable = new string[12, 12]
        {
            { "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç" },
            { "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa" },
            { "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler" },
            { "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç" },
            { "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan" },
            { "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak" },
            { "Akrep", "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi" },
            { "Yay", "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep" },
            { "Oğlak", "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay" },
            { "Kova", "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak" },
            { "Balık", "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova" },
            { "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık" },
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // İlk yüklemede yapılacak işlemler (şu an için bir şey yapmıyoruz)
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            int sunSignIndex = int.Parse(ddlSunSign.SelectedValue);
            int birthTimeIndex = int.Parse(ddlBirthTime.SelectedValue);
            string risingSign = ascendantTable[birthTimeIndex, sunSignIndex];

            lblResult.Text = $"<span style='font-weight:bold; color:#000000;'>Yükselen Burcunuz: {risingSign}</span><br />";

            string risingPage = GetRisingSignPage(risingSign);
            lblResult.Text += $"<span style='color:#d9534f;'>Günlük yükselen burç yorumunuzu da okumayı unutmayınız!</span><br />";
            lblResult.Text += $"<a href='{risingPage}.aspx' class='btn-calculate'>Günlük Yükselen Burç Yorumu Okumak İçin Tıklayınız!</a><br />";

            string risingSignFeatures = GetRisingSignFeaturesFromDatabase(risingSign);
            lblResult.Text += $"<br /><span style='font-weight:bold;'>Yükselen Burcunuzun Özellikleri:</span><br />";
            lblResult.Text += $"<span>{risingSignFeatures}</span><br />";
        }

        private string GetRisingSignPage(string risingSign)
        {
            switch (risingSign)
            {
                case "Akrep": return "akrep";
                case "Aslan": return "aslan";
                case "Balık": return "balik";
                case "Başak": return "basak";
                case "Boğa": return "Boga";
                case "İkizler": return "ikizler";
                case "Koç": return "Koc";
                case "Kova": return "kova";
                case "Oğlak": return "oglak";
                case "Terazi": return "terazi";
                case "Yay": return "yay";
                case "Yengeç": return "yengec";
                default: return "";
            }
        }

        private string GetRisingSignFeaturesFromDatabase(string risingSign)
        {
            string features = "Özellikler bulunamadı.";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\genel_ozellikler.accdb";
            string query = "SELECT yukselen_ozellik FROM genel_ozellikler WHERE Burç = @RisingSign";

            using (var connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RisingSign", risingSign);
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            features = reader["yukselen_ozellik"].ToString();
                        }
                    }
                }
            }

            return features;
        }
    }
}
