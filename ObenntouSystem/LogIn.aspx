<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ObenntouSystem.LogIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Script/mystyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 250px"></div>
    <div class="container" style="border: solid blue 2px;">
        <div class="row">
            <div class="col-12">
                帳號：
                <asp:TextBox ID="TextBoxacc" runat="server" ToolTip="帳號" Width="60%" MaxLength="50" ></asp:TextBox>
                <span class="errorMessage">
                    <asp:Literal ID="ltlacc" runat="server"></asp:Literal>
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                密碼：
                <asp:TextBox ID="TextBoxpwd" runat="server" ToolTip="密碼" Width="60%" TextMode="Password" MaxLength="50"></asp:TextBox>
                <span class="errorMessage">
                    <asp:Literal ID="ltlpwd" runat="server"></asp:Literal>
                </span>
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Button ID="btnlogin" runat="server" Text="登入" OnClick="Button1_Click" />
                <span class="errorMessage">
                    <asp:Literal ID="ltMessage" runat="server"></asp:Literal>
                </span>
            </div>
        </div>


    </div>
</asp:Content>
