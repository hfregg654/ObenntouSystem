<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="GroupDetail.aspx.cs" Inherits="ObenntouSystem.GroupDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-12 row">
            <div class="co-4">
                <div class="col-12">
                    <asp:Image ID="Image_G" runat="server" Width="250px" Height="250px" />
                </div>
                <div class="col-12">
                    <asp:Literal ID="ltl_omise" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="col-8 row">
                <div class="col-12">
                    <asp:Literal ID="ltlgroupname" runat="server"></asp:Literal>
                </div>
                <div class="col-12">
                    狀態：<div runat="server" id="IsUser">
                        <asp:Literal ID="ltlgrouptype" runat="server"></asp:Literal>
                    </div>
                    <div runat="server" id="IsConvener">
                        <asp:DropDownList ID="DDL_type" runat="server">
                            <asp:ListItem>未結團</asp:ListItem>
                            <asp:ListItem>結團</asp:ListItem>
                            <asp:ListItem>已到</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-12">
                    主揪：<asp:Literal ID="ltlConName" runat="server"></asp:Literal>
                    <asp:Repeater ID="Rep_OrderCount" runat="server"></asp:Repeater>
                </div>
            </div>
        </div>
        <div style="height: 50px"></div>
        <div class="row">
            <asp:Repeater ID="Rep_Dish" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-4" style="border: solid black 2px;">
                        <div class="row col-12">
                            <div class="col-6">
                                <img src="\Images\Group_Black.jpg" width="100" height="100" />
                            </div>
                            <div class="col-6">
                                <div class="col-12">名稱：<%#Eval("dish_name") %></div>
                                <div class="col-12">價格：NT$<%#Eval("dish_price") %></div>
                                <div class="col-12">
                                    數量：<asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem Selected="True" Value="0">請選擇數量</asp:ListItem>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="adddishdiv" class="col-12 row justify-content-end" runat="server">
            <asp:Button ID="adddishbtn" runat="server" Text="加入" />
        </div>

        <div style="height: 50px"></div>
        <div class="row">
            <asp:Repeater ID="Rep_Order" runat="server" OnItemDataBound="Rep_Order_ItemDataBound">
                <ItemTemplate>
                    <div class="col-12 col-md-4 row" style="border: solid black 1px;">
                        <div class="col-2" runat="server" id="kickdiv">
                            <asp:Button ID="kickbtn" runat="server" Text="X" CommandName="KickPeople" CommandArgument='<%#Eval("order_userid") %>' />
                        </div>
                        <div class="col-4">
                            <%#Eval("User_name") %>
                        </div>
                        <div class="col-6" style="border: solid Blue 1px;">
                            <asp:Repeater ID="Rep_Orderdish" runat="server">
                                <ItemTemplate>
                                    <div class="col-12"><%#Eval("dish_name") %> X  <%#Eval("order_num") %></div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <div style="height: 250px"></div>
        <div id="LogIndiv" runat="server">
            <div class="col-12 row">
                <div class="col-12">
                    <div class="col-3">
                        <asp:Literal ID="ltlusername" runat="server"></asp:Literal>
                    </div>
                    <div class="col-9">
                        <asp:Repeater ID="Repeater3" runat="server">
                            <ItemTemplate>
                                <div class="col-12">
                                    <div class="col-6">
                                        <asp:Literal ID="ltlorderusername" runat="server"></asp:Literal>
                                    </div>
                                    <div class="col-6" style="border: solid black 1px;">
                                        <asp:Literal ID="ltlorderdish" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-12 justify-content-end">
                    <asp:Button ID="Button3" runat="server" Text="OK" />
                    <asp:Button ID="Button2" runat="server" Text="Reset" />
                </div>
            </div>
        </div>
    </div>
    <div class="container">
        <div class="row justify-content-end">
            <asp:Button ID="BackListbtn" runat="server" Text="返回列表" OnClick="BackListbtn_Click" />
        </div>
    </div>
</asp:Content>
