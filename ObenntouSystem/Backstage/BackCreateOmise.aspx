<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="BackCreateOmise.aspx.cs" Inherits="ObenntouSystem.Backstage.BackCreateOmise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height: 100px"></div>
        <asp:Button ID="CreateOmisebtn" runat="server" Text="新增" OnClick="CreateOmisebtn_Click" />
        <div class="row">
            <asp:Repeater ID="Rep_Omise" runat="server">
                <ItemTemplate>
                    <div class="row col-12" style="margin: 2px,0px,2px,0px; border: solid black 1px;">
                        <div class="col-4"><%#Eval("omise_name") %></div>
                        <div class="col-4">
                            <asp:Button ID="UpdateOmisebtn" runat="server" Text="修改" CommandArgument='<%#Eval("omise_id") %>' OnClick="UpdateOmisebtn_Click" />
                        </div>
                        <div class="col-4">
                            <asp:Button ID="DelOmisebtn" runat="server" Text="刪除" CommandArgument='<%#Eval("omise_id") %>' OnClick="DelOmisebtn_Click" OnClientClick="return confirm('確定刪除？');" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
