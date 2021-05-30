<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ObenntouSystem.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="height: 100%">
        <div class="row">
            <div class="col-8">
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="搜尋組名" />
            </div>
            <div class="col-4">
                <asp:LinkButton ID="Linklogin" runat="server" OnClick="Linklogin_Click">登入</asp:LinkButton>
                <asp:LinkButton ID="Linklogout" runat="server" OnClick="Linklogout_Click">登出</asp:LinkButton>
                <asp:LinkButton ID="Linkcreate" runat="server" OnClick="Linkcreate_Click">建立</asp:LinkButton>
            </div>
        </div>
        <div style="height:50px"></div>
        <div class="row">
            <asp:Repeater ID="repGroup" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-4" style="border: solid black 2px;">
                        <a href="./GroupDetail.aspx?id=<%#Eval("group_id") %>">
                            <div class="row col-12">
                                <div class="col-6">
                                    <img src="<%#Eval("group_pic") %>" width="100" height="100" />
                                </div>
                                <div class="col-6">
                                    <div class="col-12">
                                    <h2><%#Eval("group_name") %></h2>
                                    </div>
                                    <div class="col-12">
                                        <%#Eval("group_type") %>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="row">
            <a href="./Index.aspx?Page=1" title="前往第1頁">First</a>｜
                <asp:Repeater runat="server" ID="repPaging">
                    <ItemTemplate>
                        <a href="<%# Eval("Link") %>" title="<%# Eval("Title") %>"><%# Eval("Name") %></a>｜
                    </ItemTemplate>
                </asp:Repeater>
            <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="前往尾頁">Last</asp:HyperLink>
        </div>
    </div>
</asp:Content>
