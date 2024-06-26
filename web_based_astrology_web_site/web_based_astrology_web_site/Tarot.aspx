<%@ Page Title="Tarot Reading" Language="C#" MasterPageFile="~/master.master" AutoEventWireup="true" CodeBehind="Tarot.aspx.cs" Inherits="web_based_astrology_web_site.Tarot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tarot-container {
            display: flex;
            justify-content: space-around;
            margin-top: 20px;
        }
        .tarot-card {
            flex: 1;
            margin: 20px;
            padding: 20px;
            text-align: center;
            border: 1px solid #ccc;
            background: rgba(255, 255, 255, 0.9);
            border-radius: 10px;
            max-width: 300px; /* Kart genişliğini sınırla */
        }
        .tarot-card img {
            max-width: 95%; /* Resim genişliğini küçült */
            height: auto;
        }
        .button-container {
            text-align: center; /* Butonu ortalar */
            margin-top: 20px;
        }
        .highlight {
            background-color: #7B2CBF; /* Mor arka plan */
            color: #FFFFFF; /* Beyaz yazı rengi */
            border: none;
            padding: 10px 20px;
            font-size: 16px;
            cursor: pointer;
            border-radius: 5px;
        }
        .highlight:hover {
            background-color: #5a1a8c; /* Daha koyu mor */
        }
    </style>
    
    <div id="cardsContainer" class="tarot-container" runat="server">
        <h1 style="color: #5a1a8c; font-size: 50px; margin-top: 10px;">TAROT FALINI ÖĞRENMEYE HAZIR MISIN?</h1>
        <%-- Cards will be displayed here --%>
    </div>
    
    <div class="button-container">
        <asp:Button ID="btnDrawCards" runat="server" CssClass="highlight" Text="ÖĞREN" OnClick="btnDrawCards_Click" />
    </div>
</asp:Content>
