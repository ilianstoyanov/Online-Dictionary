<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Src="Modules/FreeAccess/Login.ascx" TagName="Login" TagPrefix="uc" %>
<%@ Register Src="Modules/FreeAccess/CreateNewAccount.ascx" TagName="CreateNewAccount" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderControls" Runat="Server">
    <uc:Login ID="ucLogin" runat="server" />
    <uc:CreateNewAccount ID="ucCreateNewAccount" runat="server" />
</asp:Content>

