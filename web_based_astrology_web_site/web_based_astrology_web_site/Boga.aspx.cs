using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web.UI.WebControls;

namespace web_based_astrology_web_site
{
    public partial class Boga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // İlk yüklemede Genel Durum sayfasını göster
                MultiView1.ActiveViewIndex = 0;
                ShowGeneralProperties();
            }
        }

        protected void btnGenelOzellik_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0; // Genel Durum View'ını göster
            ShowGeneralProperties();
        }

        protected void btnDun_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1; // Dün View'ını göster
            ShowHoroscope("Günlük", DateTime.Today.AddDays(-1));
        }

        protected void btnBugun_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2; // Bugün View'ını göster
            ShowHoroscope("Günlük", DateTime.Today);
        }

        protected void btnHaftalik_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3; // Haftalık View'ını göster
            ShowHoroscope("Haftalık", DateTime.Today); // Haftalık için bugünün tarihi
        }

        protected void btnAylik_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4; // Aylık View'ını göster
            ShowHoroscope("Aylık", DateTime.Today); // Aylık için bugünün tarihi
        }

        private void ShowGeneralProperties()
        {
            string burcAdi = "Boğa"; // Burcun adı, bu sayfanın AKREP burcu için olduğunu varsayalım

            var properties = GetGeneralPropertiesFromDatabase(burcAdi);

            // Genel özellikleri ekrana yaz
            Label generalPropertiesLiteral = (Label)View1.FindControl("lblGenelOzellikler");
            generalPropertiesLiteral.Text = "";
            foreach (var property in properties)
            {
                generalPropertiesLiteral.Text += $"{property.Key}: {property.Value}<br />";
            }
        }

        private void ShowHoroscope(string type, DateTime date)
        {
            string burcAdi = "Boğa"; // Burcun adı, bu sayfanın AKREP burcu için olduğunu varsayalım

            string description = GetHoroscopeFromDatabase(burcAdi, type, date);
            switch (type)
            {
                case "Günlük":
                    if (date.Date == DateTime.Today)
                        ((Label)View3.FindControl("lblBugun")).Text = description;
                    else if (date.Date == DateTime.Today.AddDays(-1))
                        ((Label)View2.FindControl("lblDun")).Text = description;
                    break;
                case "Haftalık":
                    ((Label)View4.FindControl("lblHaftalik")).Text = description;
                    break;
                case "Aylık":
                    ((Label)View5.FindControl("lblAylik")).Text = description;
                    break;
                default:
                    break;
            }
        }

        private string GetHoroscopeFromDatabase(string burcAdi, string type, DateTime date)
        {
            string description = "Burç yorumunuz henüz yayınlanmamıştır, lütfen gün içinde tekrar kontrol ediniz :)";
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\HoroscopeDatabase.accdb";
            string query;

            if (type == "Günlük")
            {
                query = "SELECT Description FROM HoroscopeDatabase WHERE Sign = @Sign AND Type = @Type AND DateValue(PubDate) = DateValue(@Date)";
            }
            else
            {
                query = "SELECT Description FROM HoroscopeDatabase WHERE Sign = @Sign AND Type = @Type";
            }

            using (var connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Sign", burcAdi);
                    command.Parameters.AddWithValue("@Type", type);

                    if (type == "Günlük")
                    {
                        command.Parameters.AddWithValue("@Date", date.ToString("yyyy-MM-dd"));
                    }

                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            description = reader["Description"].ToString();
                        }
                    }
                }
            }

            return description;
        }

        private Dictionary<string, string> GetGeneralPropertiesFromDatabase(string burcAdi)
        {
            var properties = new Dictionary<string, string>();
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\genel_ozellikler.accdb";
            string query = "SELECT * FROM genel_ozellikler WHERE Burç = @Sign";

            using (var connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Sign", burcAdi);
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            properties.Add("Sembol", reader["Sembol"].ToString());
                            properties.Add("Element", reader["Element"].ToString());
                            properties.Add("Nitelik", reader["Nitelik"].ToString());
                            properties.Add("Yönetici Gezegen", reader["Yönetici_Gezegen"].ToString());
                            properties.Add("Renk", reader["Renk"].ToString());
                            properties.Add("Gün", reader["Gün"].ToString());
                            properties.Add("Sayı", reader["Sayı"].ToString());
                            properties.Add("Taş", reader["Taş"].ToString());
                            properties.Add("Olumlu Özellik", reader["Olumlu_Özellik"].ToString());
                            properties.Add("Olumsuz Özellik", reader["Olumsuz_Özellik"].ToString());
                            properties.Add("En Sevdiği Şey", reader["En_Sevdiği_Şey"].ToString());
                            properties.Add("Nefret Ettiği Şey", reader["Nefret_Ettiği_Şey"].ToString());
                            properties.Add("Güçlü Yan", reader["Güçlü_Yan"].ToString());
                            properties.Add("Zayıf Yan", reader["Zayıf_Yan"].ToString());
                        }
                    }
                }
            }

            return properties;
        }
    }
}
