<%@ Page Title="" Language="C#" MasterPageFile="~/ObenntouSystem.Master" AutoEventWireup="true" CodeBehind="BackCreateDishes.aspx.cs" Inherits="ObenntouSystem.Backstage.BackCreateDishes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div style="height: 100px"></div>
        <asp:Button ID="CreateDishbtn" runat="server" Text="新增" OnClick="CreateDishbtn_Click" />
        <asp:DropDownList ID="DDL_Omise" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Omise_SelectedIndexChanged"></asp:DropDownList>
        <div class="row">
            <asp:Repeater ID="Rep_Dishes" runat="server">
                <ItemTemplate>
                    <div class="row col-12" style="margin: 2px,0px,2px,0px; border: solid black 1px;">
                        <div class="col-4"><%#Eval("dish_name") %></div>
                        <div class="col-4">
                            <asp:Button ID="UpdateDishbtn" runat="server" Text="修改" CommandArgument='<%#Eval("dish_id") %>' OnClick="UpdateDishbtn_Click"/>
                        </div>
                        <div class="col-4">
                            <asp:Button ID="DelDishbtn" runat="server" Text="刪除" CommandArgument='<%#Eval("dish_id") %>' OnClick="DelDishbtn_Click" OnClientClick="return confirm('確定刪除？');" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
