<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="BackIndex.aspx.cs" Inherits="ObenntouSystem.Backstage.BackIndex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height:100px"></div>
        <div class="row">
            <div class="col-12"><a href="BackCreateUser.aspx">建立使用者</a></div>
            <div class="col-12"><a href="BackCreateOmise.aspx">建立店家</a></div>
            <div class="col-12"><a href="BackCreateDishes.aspx">建立菜單</a></div>
            <div class="col-12"><a href="SettingGroupPic.aspx">新增小組圖片</a></div>
        </div>
    </div>
</asp:Content>
