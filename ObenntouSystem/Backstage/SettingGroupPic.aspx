<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="SettingGroupPic.aspx.cs" Inherits="ObenntouSystem.Backstage.SettingGroupPic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Script/mystyle.css" rel="stylesheet" />
    <script>
        function onFileChange(sender) {
            document.getElementById("ContentPlaceHolder1_ImgNGroup").src = window.URL.createObjectURL(sender.files[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height: 100px"></div>
        <div class="row" style="text-align: left">
            <div class="col-12">
                圖片：<asp:Image ID="ImgNGroup" runat="server" Height="250px" Width="250px" ImageUrl="~/Images/Group_Black.jpg" />
                <asp:FileUpload ID="FUpNGroup" runat="server" onchange="onFileChange(this)" />
            </div>
            <div class="col-2">
                <asp:Button ID="Clearbtn" runat="server" Text="Clear" OnClick="Clearbtn_Click" />
            </div>
            <div class="col-2">
                <asp:Button ID="Cancelbtn" runat="server" Text="Cancel" OnClick="Cancelbtn_Click" />
            </div>
            <div class="col-2">
                <asp:Button ID="OKbtn" runat="server" Text="OK" OnClick="OKbtn_Click" />
                <asp:Label ID="lblerror" Text="" runat="server" CssClass="errorMessage" />
            </div>
            <div class="col-12" style="height: 100px"></div>
            <asp:Repeater ID="Rep_Gpic" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-3">
                        <asp:Button ID="Delbtn" runat="server" Text="X" CommandArgument='<%#Eval("gpic_id") %>' OnClick="Delbtn_Click" OnClientClick="return confirm('確定刪除？');" />
                        圖片：<asp:Image ID="ImgGroup" runat="server" Height="250px" Width="250px" ImageUrl='<%#Eval("gpic_path") %>' />

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
