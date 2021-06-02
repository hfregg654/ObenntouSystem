<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="ObenntouSystem.CreateGroupaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Script/mystyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            團名：<asp:TextBox runat="server" ID="Groupname" MaxLength="15"></asp:TextBox>
            <span class="errorMessage">
                <asp:Literal ID="ltlgname" runat="server"></asp:Literal>
            </span>
        </div>
        <div class="row">
            店名：<asp:DropDownList ID="DDL_Omise" runat="server"></asp:DropDownList>
        </div>
        <div class="row">
            <asp:Image ID="img_pic" runat="server" Height="100px" Width="100px"></asp:Image>
            <asp:DropDownList runat="server" AutoPostBack="true" ID="DDL_Pic" OnSelectedIndexChanged="Pic_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:Button runat="server" Text="OK" ID="OKbtn" OnClick="OKbtn_Click" />
                <asp:Button runat="server" Text="Reset" ID="Resetbtn" OnClick="Resetbtn_Click" />
                <span class="errorMessage">
                    <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
