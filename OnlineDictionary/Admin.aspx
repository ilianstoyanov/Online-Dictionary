<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>

<%@ Register Src="Modules/AdminModules/AllUsers.ascx" TagName="AllUsers" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderControls" Runat="Server">
        <h2>dfa</h2>
    <uc:AllUsers ID="ucAllUsers" runat="server" />
</asp:Content>

