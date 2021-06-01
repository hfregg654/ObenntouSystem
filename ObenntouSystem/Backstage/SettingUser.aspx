<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="SettingUser.aspx.cs" Inherits="ObenntouSystem.Backstage.SettingUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Script/mystyle.css" rel="stylesheet" />
    <script>
        function onFileChange(sender) {
            document.getElementById("ContentPlaceHolder1_ImgUser").src = window.URL.createObjectURL(sender.files[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height: 100px"></div>
        <div class="row" style="text-align: left">
            <div class="col-12">
                名稱：&emsp;&emsp;<asp:TextBox ID="TBName" runat="server" MaxLength="15"></asp:TextBox>
                <asp:Label ID="lblNameerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                電話：&emsp;&emsp;<asp:TextBox ID="TBPhone" runat="server" MaxLength="10"></asp:TextBox>
                <asp:Label ID="lblPhoneerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                電子郵件：<asp:TextBox ID="TBMail" runat="server" MaxLength="100"></asp:TextBox>
                <asp:Label ID="lblMailerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12" id="accforcreate" runat="server">
                帳號：&emsp;&emsp;<asp:TextBox ID="TBAcc" runat="server" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblAccerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                密碼：&emsp;&emsp;<asp:TextBox ID="TBPwd" runat="server" TextMode="Password" MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblPwderror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12" id="pwdforcreate" runat="server" visible="false">
                新密碼：&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TBNPwd" runat="server" TextMode="Password"  MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblNPwderror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                密碼確認：<asp:TextBox ID="TBPwdC" runat="server" TextMode="Password"  MaxLength="50"></asp:TextBox>
                <asp:Label ID="lblPwdCerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12">
                權限：&emsp;&emsp;<asp:DropDownList ID="DDL_Pri" runat="server">
                    <asp:ListItem>Manager</asp:ListItem>
                    <asp:ListItem Selected="True">User</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-12">
                圖片：<asp:Image ID="ImgUser" runat="server" Height="250px" Width="250px" ImageUrl="~/Images/Group_Black.jpg" />
                <asp:FileUpload ID="FUpUser" runat="server" onchange="onFileChange(this)" />
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
