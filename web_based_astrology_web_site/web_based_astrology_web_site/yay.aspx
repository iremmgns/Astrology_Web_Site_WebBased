﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="yay.aspx.cs" Inherits="web_based_astrology_web_site.yay" MasterPageFile="~/master.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .header-with-image img {
            margin-right: 40px; /* Resim ile metin arasında boşluk bırakır */
            width: 230px; /* Resmin genişliğini ayarlar */
            height: 270px; /* Oranını korumak için otomatik */
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="header-with-image">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <img src="<%= ResolveUrl("~/images/yay_detail.png") %>" alt="YAY Burcu" />
        <h2>YAY Burcu</h2>
    </div>
        
    <div class="panel-with-image">
        <div class="buttons-container">
            <asp:Button ID="btnGenelOzellik" runat="server" Text="Genel Özellikler" OnClick="btnGenelOzellik_Click" CssClass="button" />
            <asp:Button ID="btnDun" runat="server" Text="Dün" OnClick="btnDun_Click" CssClass="button" />
            <asp:Button ID="btnBugun" runat="server" Text="Bugün" OnClick="btnBugun_Click" CssClass="button" />
            <asp:Button ID="btnHaftalik" runat="server" Text="Haftalık" OnClick="btnHaftalik_Click" CssClass="button" />
            <asp:Button ID="btnAylik" runat="server" Text="Aylık" OnClick="btnAylik_Click" CssClass="button" />
        </div>
        <div class="panel-content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <h3>Yay Genel Özellikler</h3>
                            <asp:Label ID="lblGenelOzellikler" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <h3>Yay Dün Yorumu</h3>
                            <asp:Label ID="lblDun" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="View3" runat="server">
                            <h3>Yay Bugün Yorumu</h3>
                            <asp:Label ID="lblBugun" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="View4" runat="server">
                            <h3>Yay Haftalık Yorumu</h3>
                            <asp:Label ID="lblHaftalik" runat="server"></asp:Label>
                        </asp:View>
                        <asp:View ID="View5" runat="server">
                            <h3>Yay Aylık Yorumu</h3>
                            <asp:Label ID="lblAylik" runat="server"></asp:Label>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGenelOzellik" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnDun" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnBugun" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnHaftalik" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAylik" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
