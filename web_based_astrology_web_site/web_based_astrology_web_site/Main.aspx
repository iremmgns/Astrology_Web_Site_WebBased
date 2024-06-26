<%@ Page Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="web_based_astrology_web_site.Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .zodiac-button {
            margin: 10px;
            display: inline-block;
            text-align: center;
            width: 150px;   /* Buton genişliği */
            height: 150px;  /* Buton yüksekliği */
            border: 2px solid #DFBFF9;  /* Kenarlık kalınlığı ve rengi */
            border-radius: 10px;     /* Kenar yuvarlaklığı */
        }

        .zodiac-button img {
            width: 100%;     /*Resim genişliğini butona göre ayarlar*/
            height: auto;    /*Yüksekliği orantılı olarak ayarlar*/
         /*   width: 150px;
            height: 150px;*/
        }
        .zodiac-button:hover {
            border-color: #9d4edd;  /* Buton üzerine gelindiğinde kenarlık rengini değiştir */
            background-color: #ffffff;  /* Buton üzerine gelindiğinde arka plan rengini değiştir */
            transform: scale(1.1); /* Buton üzerine gelindiğinde boyutunu arttır */
        }
        .panel {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
        }
        .panel .row {
            display: flex;
            width: 100%;
            justify-content: center;
            margin-bottom: 20px;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;">
        <img src='<%= ResolveUrl("~/images/burc_baslik_main.png") %>') alt="Burçlar" class="burclar-image" />
        <asp:Panel ID="panelBurclar" runat="server" CssClass="panel">
            <div class="row">
                <asp:ImageButton ID="btnKoc" runat="server" OnClick="BtnKoc_Click" CssClass="zodiac-button" ImageUrl="~/images/koc.png" AlternateText="Koç" />
                <asp:ImageButton ID="btnBoga" runat="server" OnClick="BtnBoga_Click" CssClass="zodiac-button" ImageUrl="~/images/boga.png" AlternateText="Boğa" />
                <asp:ImageButton ID="btnIkizler" runat="server" OnClick="BtnIkizler_Click" CssClass="zodiac-button" ImageUrl="~/images/ikizler.png" AlternateText="İkizler" />
                <asp:ImageButton ID="btnYengec" runat="server" OnClick="BtnYengec_Click" CssClass="zodiac-button" ImageUrl="~/images/yengec.png" AlternateText="Yengeç" />
                <asp:ImageButton ID="btnAslan" runat="server" OnClick="BtnAslan_Click" CssClass="zodiac-button" ImageUrl="~/images/aslan.png" AlternateText="Aslan" />
                <asp:ImageButton ID="btnBasak" runat="server" OnClick="BtnBasak_Click" CssClass="zodiac-button" ImageUrl="~/images/basak.png" AlternateText="Başak" />
            </div>
            <div class="row">
                <asp:ImageButton ID="btnTerazi" runat="server" OnClick="BtnTerazi_Click" CssClass="zodiac-button" ImageUrl="~/images/terazi.png" AlternateText="Terazi" />
                <asp:ImageButton ID="btnAkrep" runat="server" OnClick="BtnAkrep_Click" CssClass="zodiac-button" ImageUrl="~/images/akrep.png" AlternateText="Akrep" />
                <asp:ImageButton ID="btnYay" runat="server" OnClick="BtnYay_Click" CssClass="zodiac-button" ImageUrl="~/images/yay.png" AlternateText="Yay" />
                <asp:ImageButton ID="btnOglak" runat="server" OnClick="BtnOglak_Click" CssClass="zodiac-button" ImageUrl="~/images/oglak.png" AlternateText="Oğlak" />
                <asp:ImageButton ID="btnKova" runat="server" OnClick="BtnKova_Click" CssClass="zodiac-button" ImageUrl="~/images/kova.png" AlternateText="Kova" />
                <asp:ImageButton ID="btnBalik" runat="server" OnClick="BtnBalik_Click" CssClass="zodiac-button" ImageUrl="~/images/balik.png" AlternateText="Balık" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
