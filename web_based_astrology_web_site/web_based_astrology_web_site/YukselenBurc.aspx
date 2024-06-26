<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/master.Master" CodeBehind="YukselenBurc.aspx.cs" Inherits="web_based_astrology_web_site.YukselenBurc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Yükselen Burç Hesaplama</title>
    <style>
        .panel-with-image {
            border: 1px solid #ccc;
            border-radius: 8px;
            overflow: hidden;
            margin-bottom: 20px;
            background-color: rgba(249, 249, 249, 0.5); /* Saydam arka plan rengi */
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .header-with-image {
            background-color: rgba(255, 255, 255, 0);
            padding: 10px;
            color: #fff;
            text-align: center;
        }
        .header-with-image h2 {
            margin: 0;
            color: #28095b;
            font-size: 42px; /* Metin boyutunu belirtin */
        }
        .panel-content {
            padding: 20px;
            font-size: 18px; /* Metin boyutunu belirtin */
        }
        .form-group {
            margin-bottom: 15px;
            text-align: left; /* Metinleri sola hizala */
        }
        .label {
            font-weight: bold;
            margin-right: 10px;
            font-size: 18px; /* Metin boyutunu büyüt */
            color: #5a189a; /* Metin rengini değiştir */
        }
        .warning {
            color: red;
            font-weight: bold;
            font-style: italic; /* Eğik yazı stili */
        }
        .btn-calculate {
            background-color: #7b2cbf;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin-top: 10px;
        }
        .btn-calculate:hover {
            background-color: #5a189a;
        }
        .result {
            margin-top: 20px;
            font-size: 18px;
            border: 1px solid #ccc;
            padding: 10px;
            border-radius: 8px;
            background-color: #fff;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .dropdown {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
            background-color: rgba(255, 255, 255, 0.5); /* Burada saydamlık ayarlanıyor */
            cursor: pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel-with-image">
        <div class="header-with-image">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <img src="https://cafeastrology.com/wp-content/uploads/2016/10/ascendantsymb-150x150.png" />
            <h2>Yükselen Burcunuzu Hesaplayın</h2>
        </div>
        <div class="panel-content">
            <p>
            Yükselen burç doğum anınızda bulunduğunuz coğrafi konuma göre, doğu ufkunda yükselmekte olan burca verilen isimdir. 12 ev sistemine göre düzenlenmiş olan doğum haritanızdaki ilk evinizdir.

            Sizi çok iyi tanıyan kişilerin bazen hakkınızda yaptıkları değerlendirmeyi yanlış bulduğunuz olmuştur. Art niyetleri bir tarafa bırakacak olursanız, bu durum yükselen burcunuzdan kaynaklanıyor olabilir.

            İnsanlar kendilerini güneş burçlarına göre tarif etseler de, dışarıdan farklı görünebilirler. Çünkü dışarıya verilen izlenim daha çok yükselen burçla alakalıdır. Unutmayın. Yükselen burcunuz imajınız ve dış dünyaya verdiğiniz izlenimdir.

            Mesela Güneş burcunuz Balık olsun. Bu size duygusallık, merhamet duyguları verir doğal olarak. Fakat yine diyelim ki yükselen burcunuz Koç olsun. Bu ise girişkenlik ve belli derecede sertlik verir.

            Bu halde siz kendinizi merhamet ve naiflik odaklı tanımlarken, etrafınızdaki kişiler, sert ve kırıcı olarak görebilirler. Bu çok normal bir durumdur. Çünkü insanlarla daha çok yükselen burcunuzun etkisi ile ilişki kurarsınız.            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblBirthMonth" runat="server" CssClass="label" Text="Doğum Ayı: " />
                        <asp:DropDownList ID="ddlBirthMonth" runat="server" CssClass="dropdown">
                            <asp:ListItem Text="Ocak" Value="1" />
                            <asp:ListItem Text="Şubat" Value="2" />
                            <asp:ListItem Text="Mart" Value="3" />
                            <asp:ListItem Text="Nisan" Value="4" />
                            <asp:ListItem Text="Mayıs" Value="5" />
                            <asp:ListItem Text="Haziran" Value="6" />
                            <asp:ListItem Text="Temmuz" Value="7" />
                            <asp:ListItem Text="Ağustos" Value="8" />
                            <asp:ListItem Text="Eylül" Value="9" />
                            <asp:ListItem Text="Ekim" Value="10" />
                            <asp:ListItem Text="Kasım" Value="11" />
                            <asp:ListItem Text="Aralık" Value="12" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSunSign" runat="server" CssClass="label" Text="Güneş Burcu: " />
                        <asp:DropDownList ID="ddlSunSign" runat="server" CssClass="dropdown">
                            <asp:ListItem Text="Koç" Value="0" />
                            <asp:ListItem Text="Boğa" Value="1" />
                            <asp:ListItem Text="İkizler" Value="2" />
                            <asp:ListItem Text="Yengeç" Value="3" />
                            <asp:ListItem Text="Aslan" Value="4" />
                            <asp:ListItem Text="Başak" Value="5" />
                            <asp:ListItem Text="Terazi" Value="6" />
                            <asp:ListItem Text="Akrep" Value="7" />
                            <asp:ListItem Text="Yay" Value="8" />
                            <asp:ListItem Text="Oğlak" Value="9" />
                            <asp:ListItem Text="Kova" Value="10" />
                            <asp:ListItem Text="Balık" Value="11" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblBirthTime" runat="server" CssClass="label" Text="Doğum Saati: " />
                        <asp:DropDownList ID="ddlBirthTime" runat="server" CssClass="dropdown">
                            <asp:ListItem Text="06:00-08:00" Value="0" />
                            <asp:ListItem Text="08:00-10:00" Value="1" />
                            <asp:ListItem Text="10:00-12:00" Value="2" />
                            <asp:ListItem Text="12:00-14:00" Value="3" />
                            <asp:ListItem Text="14:00-16:00" Value="4" />
                            <asp:ListItem Text="16:00-18:00" Value="5" />
                            <asp:ListItem Text="18:00-20:00" Value="6" />
                            <asp:ListItem Text="20:00-22:00" Value="7" />
                            <asp:ListItem Text="22:00-00:00" Value="8" />
                            <asp:ListItem Text="00:00-02:00" Value="9" />
                            <asp:ListItem Text="02:00-04:00" Value="10" />
                            <asp:ListItem Text="04:00-06:00" Value="11" />
                        </asp:DropDownList>
                        <asp:Label ID="lblTimeWarning" runat="server" CssClass="warning" Text="*Doğum ayınız Nisan-Ekim aralığında ise doğum saatinizden 1 saat çıkararak seçim yapınız.*" Visible="true" />
                    </div>
                    <asp:Button ID="btnCalculate" runat="server" CssClass="btn-calculate" Text="Hesapla" OnClick="btnCalculate_Click" />
                    <div class="result">
                        <asp:Label ID="lblResult" runat="server" Text="" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
