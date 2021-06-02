<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="SettingDish.aspx.cs" Inherits="ObenntouSystem.Backstage.SettingDish" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../Script/mystyle.css" rel="stylesheet" />
    <script>
        function onFileChange(sender) {
            document.getElementById("ContentPlaceHolder1_ImgDish").src = window.URL.createObjectURL(sender.files[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height: 100px"></div>
        <div class="row" style="text-align: left">
             <div class="col-12" id="CreateModeTitle" runat="server">
                新增 <asp:Label ID="lblOmiseName" Text="" runat="server"/>菜單
            </div>
            <div class="col-12">
                名稱：&emsp;&emsp;<asp:TextBox ID="TBName" runat="server" MaxLength="25"></asp:TextBox>
                <asp:Label ID="lblNameerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                價格：&emsp;&emsp;<asp:TextBox ID="TBPrice" runat="server" MaxLength="18" TextMode="Number"></asp:TextBox>
                <asp:Label ID="lblPriceerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                圖片：<asp:Image ID="ImgDish" runat="server" Height="250px" Width="250px" ImageUrl="~/Images/Group_Black.jpg" />
                <asp:FileUpload ID="FUpDish" runat="server" onchange="onFileChange(this)" />
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
