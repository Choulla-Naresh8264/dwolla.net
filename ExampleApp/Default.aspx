<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExampleApp._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Label ID="Label1" runat="server" Font-Size="Large" style="font-weight: 700" Text="View the example application user's transaction history:"></asp:Label>
<br />
<br />
<asp:ListBox ID="TransHistoryLB" runat="server" Height="219px" style="text-align: justify" Width="480px"></asp:ListBox>
<br />
<br />
<asp:Button ID="tHistButton" runat="server" OnClick="tHistButton_Click" Text="Refresh" Width="99px" CausesValidation="False" UseSubmitBehavior="False" />
<br />
<hr />
<br />
    <asp:Label ID="Label2" runat="server" Font-Size="Large" style="font-weight: 700" Text="Send a user money (using the example application's account):"></asp:Label>
    <br />
    <br />
    <table style="width: 24%;">
        <tr>
            <td class="auto-style2">Destination ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>
                <asp:TextBox ID="DestTB" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Destination Type:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>
                <asp:DropDownList ID="destDD" runat="server" Width="200px">
                    <asp:ListItem>Dwolla ID</asp:ListItem>
                    <asp:ListItem>Email</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Amount:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>
                    <span class="newStyle1">
                    <asp:TextBox ID="AmountTB" runat="server" Width="75px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">PIN (for example account):</td>
            <td>
                    <span class="newStyle1">
                    <asp:TextBox ID="PinTB" runat="server" MaxLength="4" Width="75px">1337</asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="SendMoneyButton" runat="server" OnClick="SendMoneyButton_Click" Text="Send Money!" />
    <br />
</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="HeadContent">
    <style type="text/css">
    .thistbox {
        float: right;
    }
        .auto-style2 {
            width: 180px;
            font-weight: bold;
        }
    </style>
</asp:Content>

