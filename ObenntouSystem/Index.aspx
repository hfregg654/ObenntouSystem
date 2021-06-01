<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ObenntouSystem.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .togglediv {
            display: none;
        }
        .Indextext{
            text-align:center;
        }
    </style>
    <script> 
        $(document).ready(function () {
            var divlist = document.getElementsByClassName("Togglediv");
            for (var i = 0; i < divlist.length; i++) {
                divlist[i].classList.add('togglediv');
            }
            $(".dishdiv").click(function () {
                var td = this.getElementsByClassName("Togglediv")[0];
                td.classList.toggle('togglediv');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" style="height: 100%">

        <div class="row">
            <div class="col-8">
                <asp:Literal ID="ltlWelcome" runat="server"></asp:Literal>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DDL_Search" runat="server">
                    <asp:ListItem Value="0">團名</asp:ListItem>
                    <asp:ListItem Value="1">店名</asp:ListItem>
                    <asp:ListItem Value="2">菜名</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnSearch" runat="server" Text="搜尋" OnClick="btnSearch_Click" />
            </div>
            <div class="col-4">
                <asp:LinkButton ID="Linklogin" runat="server" OnClick="Linklogin_Click">登入</asp:LinkButton>
                <asp:LinkButton ID="Linklogout" runat="server" OnClick="Linklogout_Click">登出</asp:LinkButton>
                <asp:LinkButton ID="Linkcreate" runat="server" OnClick="Linkcreate_Click">建立</asp:LinkButton>
                <asp:LinkButton ID="LinkBackstage" runat="server" Visible="false">後台</asp:LinkButton>
            </div>
        </div>
        <div id="Nothingdiv" class="col-12" runat="server" style="font-size: 120px">
            無
        </div>
        <div style="height: 50px"></div>
        <div class="row">
            <asp:Repeater ID="repGroup" runat="server" OnItemDataBound="repGroup_ItemDataBound">
                <ItemTemplate>
                    <div class="col-12 Indextext" style="border: solid black 2px;">
                        <a href="./GroupDetail.aspx?id=<%#Eval("group_id") %>">
                            <div class="row col-12" style="margin: 0px; padding: 0px">
                                <div class="col-3">
                                    <img src="<%#Eval("group_pic") %>" width="100" height="100" />
                                </div>
                                <div class="col-9 row">
                                    <div class="col-6">
                                        團名：<%#Eval("group_name") %>
                                    </div>
                                    <div class="col-6">
                                        主揪：<%#Eval("user_name") %>
                                    </div>
                                    <div class="col-6">
                                        店名：<%#Eval("omise_name") %>
                                    </div>
                                    <div class="col-6">
                                        目前人數：<%#Eval("peoplenum") %>
                                    </div>
                                    <div class="col-12" style="text-align:center;font-size:20px">
                                        <%#Eval("group_type") %>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <div class="dishdiv" style="text-align: center">
                            菜單
                             <div class="Togglediv" style="border: solid black 1px;">
                                 <asp:HiddenField ID="HFdishomise" runat="server" Value='<%#Eval("omise_id") %>' />
                                 <div class="row col-12" style="margin: 0px; padding: 0px">
                                     <asp:Repeater ID="Rep_Indexdish" runat="server">
                                         <ItemTemplate>
                                             <div class="col-4" style="text-align: center; border: solid blue 1px;"><%#Eval("dish_name") %></div>
                                         </ItemTemplate>
                                     </asp:Repeater>
                                 </div>
                                 <div style="height: 50px"></div>
                             </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="row">
            <asp:HyperLink ID="HLfirst" runat="server" ToolTip="前往第一頁">First</asp:HyperLink>｜
            <asp:Repeater runat="server" ID="repPaging">
                <ItemTemplate>
                    <a href="<%# Eval("Link") %>" title="<%# Eval("Title") %>"><%# Eval("Name") %></a>｜
                </ItemTemplate>
            </asp:Repeater>
            <asp:HyperLink ID="HLlast" runat="server" ToolTip="前往尾頁">Last</asp:HyperLink>
        </div>
    </div>
</asp:Content>
