<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="SettingOmise.aspx.cs" Inherits="ObenntouSystem.Backstage.SettingOmise" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Script/mystyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="container">
        <div style="height: 100px"></div>
        <div class="row" style="text-align: left">
            <div class="col-12">
                名稱：<asp:TextBox ID="TBName" runat="server" MaxLength="15"></asp:TextBox>
                <asp:Label ID="lblNameerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-2">
                <asp:Button ID="Clearbtn" runat="server" Text="Clear" OnClick="Clearbtn_Click" />
            </div>
            <div class="col-2">
                <asp:Button ID="Cancelbtn" runat="server" Text="Cancel" OnClick="Cancelbtn_Click"/>
            </div>
            <div class="col-2">
                <asp:Button ID="OKbtn" runat="server" Text="OK" OnClick="OKbtn_Click" />
                <asp:Label ID="lblerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
        </div>
    </div>
</asp:Content>
