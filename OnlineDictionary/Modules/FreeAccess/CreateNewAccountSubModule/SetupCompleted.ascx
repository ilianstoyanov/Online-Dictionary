<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetupCompleted.ascx.cs"
    Inherits="Modules_FreeAccess_CreateNewAccountSubModule_SetupCompleted" %>
<%--<h2>
    Вашата регистрация премина успешно.</h2>--%>
<div class="regForm">
    <div class="regInput">
        <asp:Label ID="lblEmail" CssClass="label" Text="Email" AssociatedControlID="txtEmail" runat="server" />
        <asp:TextBox ID="txtEmail" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblFirstName" CssClass="label"  Text="Име:" AssociatedControlID="txtFirstName" runat="server" />
        <asp:TextBox ID="txtFirstName" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblLastName" CssClass="label"  Text="Фамилия:" AssociatedControlID="txtLastName" runat="server" />
        <asp:TextBox ID="txtLastName" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblPhone" CssClass="label"  Text="Телефон:" AssociatedControlID="txtPhone" runat="server" />
        <asp:TextBox ID="txtPhone" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblCountry" CssClass="label"  Text="Държава:" AssociatedControlID="txtCountry" runat="server" />
        <asp:TextBox ID="txtCountry" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblArea" CssClass="label"  Text="Област:" AssociatedControlID="txtArea" runat="server" />
        <asp:TextBox ID="txtArea" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblCity" CssClass="label"  Text="Град:" AssociatedControlID="lblCity" runat="server" />
        <asp:TextBox ID="txtCity" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblVillage" CssClass="label"  Text="Село:" AssociatedControlID="txtVillage" runat="server" />
        <asp:TextBox ID="txtVillage" Enabled="false" runat="server" />
    </div>
    <div class="regInput">
        <asp:Label ID="lblAddress" CssClass="label"  Text="Адрес:" AssociatedControlID="txtAddress" runat="server" />
        <asp:TextBox ID="txtAddress" CssClass="regNotes" TextMode="MultiLine" Enabled="false" runat="server" />
    </div>
    
    <div class="regInput">
        <asp:Label ID="lblNotes" CssClass="label margin50"  Text="За мен:" AssociatedControlID="txtNotes" runat="server" />
        <asp:TextBox ID="txtNotes"  CssClass="regNotes" TextMode="MultiLine" Enabled="false" runat="server" />
    </div>
    <div class="regControls margin50">
        <asp:LinkButton ID="lbtnContinue" Text="Продължи" CssClass="button" OnClick="lbtnContinue_Click"
            runat="server" />
    </div>
</div>
