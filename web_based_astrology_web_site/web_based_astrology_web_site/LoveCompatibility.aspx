<%@ Page Title="Aşk Uyumu" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="LoveCompatibility.aspx.cs" Inherits="web_based_astrology_web_site.LoveCompatibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-with-image {
            padding: 20px;
            border-radius: 10px;
            background-color: rgba(255, 255, 255, 0.7);
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
            width: 500px;
            margin: 0 auto;
            position: relative;
        }
        .header-with-image {
            display: flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 20px;
        }
        .header-with-image img {
            height: 60px;
            margin-right: 15px;
        }
        .header-with-image h2 {
            color: #28095b;
            font-size: 36px;
            font-weight: bold;
            text-transform: uppercase;
        }
        .panel-content {
            margin-top: 20px;
            font-size: 18px;
            text-align: center;
        }
        .panel-content label {
            font-weight: bold;
            display: block;
            margin-bottom: 10px;
        }
        .panel-content .dropdown {
            width: 100%;
            margin-bottom: 20px;
        }
        .panel-content select {
            width: 100%;
            padding: 10px;
            border-radius: 5px;
            border: 1px solid #ccc;
            font-size: 16px;
        }
        .button-container {
            margin-top: 20px;
        }
        .button {
            background-color: #7b2cbf;
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            transition: background-color 0.3s ease;
        }
        .button:hover {
            background-color: #5a189a;
        }
        .result-label {
            margin-top: 20px;
            font-size: 20px;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel-with-image">
        <div class="header-with-image">
            <img src='<%= ResolveUrl("~/images/purple_heart.png") %>' alt="Heart" />
            <h2>Aşk Uyumu</h2>
        </div>
        <div class="panel-content">
            <label for="ddlYourSign">Senin Burcun:</label>
            <asp:DropDownList ID="ddlYourSign" runat="server" CssClass="dropdown">
                <asp:ListItem Value="Koç">Koç</asp:ListItem>
                <asp:ListItem Value="Boğa">Boğa</asp:ListItem>
                <asp:ListItem Value="İkizler">İkizler</asp:ListItem>
                <asp:ListItem Value="Yengeç">Yengeç</asp:ListItem>
                <asp:ListItem Value="Aslan">Aslan</asp:ListItem>
                <asp:ListItem Value="Başak">Başak</asp:ListItem>
                <asp:ListItem Value="Terazi">Terazi</asp:ListItem>
                <asp:ListItem Value="Akrep">Akrep</asp:ListItem>
                <asp:ListItem Value="Yay">Yay</asp:ListItem>
                <asp:ListItem Value="Oğlak">Oğlak</asp:ListItem>
                <asp:ListItem Value="Kova">Kova</asp:ListItem>
                <asp:ListItem Value="Balık">Balık</asp:ListItem>
            </asp:DropDownList>

            <label for="ddlPartnerSign">Onun Burcu:</label>
            <asp:DropDownList ID="ddlPartnerSign" runat="server" CssClass="dropdown">
                <asp:ListItem Value="Koç">Koç</asp:ListItem>
                <asp:ListItem Value="Boğa">Boğa</asp:ListItem>
                <asp:ListItem Value="İkizler">İkizler</asp:ListItem>
                <asp:ListItem Value="Yengeç">Yengeç</asp:ListItem>
                <asp:ListItem Value="Aslan">Aslan</asp:ListItem>
                <asp:ListItem Value="Başak">Başak</asp:ListItem>
                <asp:ListItem Value="Terazi">Terazi</asp:ListItem>
                <asp:ListItem Value="Akrep">Akrep</asp:ListItem>
                <asp:ListItem Value="Yay">Yay</asp:ListItem>
                <asp:ListItem Value="Oğlak">Oğlak</asp:ListItem>
                <asp:ListItem Value="Kova">Kova</asp:ListItem>
                <asp:ListItem Value="Balık">Balık</asp:ListItem>
            </asp:DropDownList>

            <div class="button-container">
                <asp:Button ID="btnCalculate" runat="server" Text="Uyumu Hesapla" OnClick="btnCalculate_Click" CssClass="button" />
            </div>

            <asp:Label ID="lblResult" runat="server" Text="" CssClass="result-label"></asp:Label>
        </div>
    </div>
</asp:Content>
