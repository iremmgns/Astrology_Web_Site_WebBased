using System;
using System.Collections;
using System.Data.OleDb;
using System.Globalization;
using System.Web.UI;
using System.Xml;

namespace web_based_astrology_web_site
{
    public partial class RSSParsing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DeleteOldHoroscopes();
            // RSS feed linki
            string rssUrl = "https://www.ntv.com.tr/astroloji-ve-burclar.rss";

            // Veritabanındaki mevcut burç sayısını al
            int horoscopeIDCounter = GetHoroscopeCountFromDatabase();

            // Yeni burçların tutulacağı ArrayList
            var horoscopeList = new ArrayList();

            // RSS XML'ini indir ve parse et
            using (var reader = XmlReader.Create(rssUrl))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "entry")
                    {
                        string title = "";
                        DateTime published = DateTime.MinValue;
                        string content = "";

                        // entry içindeki verileri oku
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name == "title")
                                {
                                    title = reader.ReadElementContentAsString();
                                }
                                if (reader.Name == "published")
                                {
                                    string dateString = reader.ReadElementContentAsString();
                                    published = DateTime.Parse(dateString);
                                }
                                if (reader.Name == "content")
                                {
                                    content = reader.ReadElementContentAsString();
                                    break;
                                }
                            }
                        }

                        // Günlük burçları sadece bugünün tarihine ve dünkü tarihine göre al
                        if (title.IndexOf("Günlük", StringComparison.OrdinalIgnoreCase) != -1 &&
                            (published.Date == DateTime.Today || published.Date == DateTime.Today.AddDays(-1)))
                        {
                            // CDATA içeriğini ayrıştırma
                            if (!string.IsNullOrEmpty(content))
                            {
                                ParseHoroscopesFromDescription(content, "Günlük", title, published, horoscopeIDCounter++, horoscopeList);
                            }
                        }

                        // Haftalık burçları sadece bu hafta içindeki verilerle al
                        if (title.IndexOf("Haftalık", StringComparison.OrdinalIgnoreCase) != -1 && IsDateInThisWeek(published))
                        {
                            // CDATA içeriğini ayrıştırma
                            if (!string.IsNullOrEmpty(content))
                            {
                                ParseHoroscopesFromDescription(content, "Haftalık", title, published, horoscopeIDCounter++, horoscopeList);
                            }
                        }

                        // Aylık burçları sadece bu ay içindeki verilerle al
                        if (title.IndexOf("Aylık", StringComparison.OrdinalIgnoreCase) != -1 && published.Month == DateTime.Today.Month)
                        {
                            // CDATA içeriğini ayrıştırma
                            if (!string.IsNullOrEmpty(content))
                            {
                                ParseHoroscopesFromDescription(content, "Aylık", title, published, horoscopeIDCounter++, horoscopeList);
                            }
                        }
                    }
                }
            }

            // Yeni burç listesini Session'da sakla
            Session["HoroscopeList"] = horoscopeList;

            // Veritabanına yeni burçları kaydet
            SaveHoroscopesToDatabase(horoscopeList);

            // Kullanıcıyı Main.aspx sayfasına yönlendir
            Response.Redirect("~/Main.aspx");
        }

        private void ParseHoroscopesFromDescription(string content, string type, string title, DateTime published, int idCounter, ArrayList horoscopeList)
        {
            // <figcaption> etiketlerini aramak için işaretçiler
            string figCaptionStartTag = "<figcaption>";
            string figCaptionEndTag = "</figcaption>";

            // İçerikteki tüm <figcaption> etiketlerini bul
            int startIndex = content.IndexOf(figCaptionStartTag);
            while (startIndex != -1)
            {
                // Başlangıç ve bitiş indekslerini bul
                int endIndex = content.IndexOf(figCaptionEndTag, startIndex);
                if (endIndex == -1)
                {
                    break; // Eşleşen bitiş etiketi yoksa döngüden çık
                }

                // <figcaption> içeriğini al
                string horoscopeDescription = content.Substring(startIndex + figCaptionStartTag.Length, endIndex - (startIndex + figCaptionStartTag.Length));

                // Boşlukları temizle ve trim yap
                horoscopeDescription = horoscopeDescription.Trim();

                // Açıklamadan burç işaretini al
                string horoscopeSign = GetHoroscopeSign(title, horoscopeDescription);

                // Eğer burç işareti bulunduysa yeni Horoscope nesnesi oluştur ve listeye ekle
                if (!string.IsNullOrEmpty(horoscopeSign))
                {
                    string horoscopeFullDescription = horoscopeDescription;
                    Horoscope horoscope = new Horoscope(idCounter++, title, horoscopeFullDescription, horoscopeSign, published.ToString(), type);

                    // Daha önce eklenmediyse listeye ekle
                    if (!IsHoroscopeDuplicate(horoscope))
                    {
                        horoscopeList.Add(horoscope);
                    }
                }

                // İleriye doğru işaretleyerek bir sonraki <figcaption> etiketini ara
                startIndex = content.IndexOf(figCaptionStartTag, endIndex);
            }
        }

        private bool IsDateInThisWeek(DateTime date)
        {
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek == 0 ? 7 : (int)today.DayOfWeek;
            DateTime startOfWeek = today.AddDays(1 - currentDayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            return date.Date >= startOfWeek.Date && date.Date <= endOfWeek.Date;
        }

        private string GetHoroscopeSign(string title, string description)
        {
            string[] horoscopeSigns = { "Koç", "Boğa", "İkizler", "Yengeç", "Aslan", "Başak", "Terazi", "Akrep", "Yay", "Oğlak", "Kova", "Balık" };

            foreach (var sign in horoscopeSigns)
            {
                // Belirli bir deseni kontrol et, örneğin "Koç Burcu:"
                string pattern = $"{sign} Burcu:";
                if (description.Contains(pattern))
                {
                    return sign;
                }
            }

            return string.Empty;
        }

        private bool IsHoroscopeDuplicate(Horoscope newHoroscope)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|HoroscopeDatabase.accdb";
            string query = "SELECT COUNT(*) FROM HoroscopeDatabase WHERE Description = @Description AND Type = @Type";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Description", newHoroscope.FullDescription);
                    command.Parameters.AddWithValue("@Type", newHoroscope.Type);

                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        return true; // Veritabanında aynı kayıt varsa true döndür
                    }
                }
                connection.Close();
            }

            return false; // Veritabanında aynı kayıt yoksa false döndür
        }

        private void SaveHoroscopesToDatabase(ArrayList horoscopeList)
        {
            // Veritabanına burçları kaydet
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|HoroscopeDatabase.accdb";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                foreach (Horoscope horoscope in horoscopeList)
                {
                    string query = $"INSERT INTO HoroscopeDatabase (Title, Description, Sign, PubDate, Type) VALUES (@Title, @Description, @Sign, @PublishedDate, @Type)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Title", horoscope.Title);
                        command.Parameters.AddWithValue("@Description", horoscope.FullDescription);
                        command.Parameters.AddWithValue("@Sign", horoscope.Sign);
                        command.Parameters.AddWithValue("@PubDate", horoscope.PublishedDate);
                        command.Parameters.AddWithValue("@Type", horoscope.Type); // Yeni özellik için parametre ekle
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        private int GetHoroscopeCountFromDatabase()
        {
            // Veritabanındaki mevcut burç sayısını al
            int count = 0;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|HoroscopeDatabase.accdb";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM HoroscopeDatabase";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    count = (int)command.ExecuteScalar();
                }
                connection.Close();
            }
            return count;
        }

        private void DeleteOldHoroscopes()
        {
            // Bugünden 2 gün önceki burç yorumlarını sil
            DateTime deleteDate = DateTime.Today.AddDays(-2);
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|HoroscopeDatabase.accdb";
            string query = "DELETE FROM HoroscopeDatabase WHERE Type = 'Günlük' AND DateValue(PubDate) <= DateValue(@DeleteDate)";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DeleteDate", deleteDate.ToString("yyyy-MM-dd"));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Geçen haftadan burç yorumlarını sil
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek - 6);
            DateTime endOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            query = "DELETE FROM HoroscopeDatabase WHERE Type = 'Haftalık' AND PubDate >= @StartOfWeek AND PubDate <= @EndOfWeek";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartOfWeek", startOfWeek.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndOfWeek", endOfWeek.ToString("yyyy-MM-dd"));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            // Geçen aydan burç yorumlarını sil
            DateTime startOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            query = "DELETE FROM HoroscopeDatabase WHERE Type = 'Aylık' AND PubDate >= @StartOfMonth AND PubDate <= @EndOfMonth";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StartOfMonth", startOfMonth.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@EndOfMonth", endOfMonth.ToString("yyyy-MM-dd"));
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public class Horoscope
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string FullDescription { get; set; }
        public string Sign { get; set; }
        public string PublishedDate { get; set; }
        public string Type { get; set; }

        // Constructor with 6 parameters including the new 'Type'
        public Horoscope(int id, string title, string fullDescription, string sign, string publishedDate, string type)
        {
            ID = id;
            Title = title;
            FullDescription = fullDescription;
            Sign = sign;
            PublishedDate = publishedDate;
            Type = type;
        }
    }
}

