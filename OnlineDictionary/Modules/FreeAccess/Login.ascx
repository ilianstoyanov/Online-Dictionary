<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Modules_FreeAccess_Login" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelMain" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel  ID="pnlLogin" CssClass="loginPage" DefaultButton="lbtnLogin" runat="server">
            <div>
                <asp:TextBox ID="txtEmail" Text="E-mail" CssClass="orangeInput jsTxtEmail" ClientIDMode="Static" runat="server" ToolTip="Въведете e-mail адрес"></asp:TextBox>
                <asp:RegularExpressionValidator ID="regEmail" ValidationGroup="LoginCtrl" ControlToValidate="txtEmail"
                    ErrorMessage="Грешен формат." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Text="*" runat="server"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="Въведете е-mail адрес." ToolTip="Полете е-mail не може да бъде празно."
                    Text="*" ValidationGroup="LoginCtrl">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ErrorMessage="Въведете е-mail адрес."  ControlToValidate="txtEmail"
                    ValueToCompare="E-mail" Operator="NotEqual" Text="*" runat="server" ValidationGroup="LoginCtrl" />
            </div>
            <div class="margintop10">
                <%--<asp:Label ID="lblPassword" Text="Парола" AssociatedControlID="txtPassword" runat="server" />--%>
                <asp:TextBox ID="txtPassword" CssClass="orangeInput jsPassword" Text="Password" ClientIDMode="Static" runat="server" TextMode="Password"
                    ToolTip="Въведете парола" EnableViewState="false"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Въведете парола" ToolTip="Полето парола не може да бъде празно."
                    ValidationGroup="LoginCtrl">*</asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label runat="server" ID="lblFailure" ForeColor="Red" ClientIDMode="Static" EnableViewState="true"></asp:Label>
                <asp:LinkButton ID="lbtnLogin" CssClass="button loginButton" runat="server" CommandName="Login" Text="Вход" ValidationGroup="LoginCtrl"
                    OnClick="lbtnLogin_Click" />
                <asp:HyperLink NavigateUrl="~/registration.aspx" CssClass="orangeText" Text="Регистрация" runat="server" />
            </div>
            <div>
                <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" EnableClientScript="true"
                        ValidationGroup="LoginCtrl" Font-Bold="true" CssClass="error" />
            </div>
            <div>
                <%--  <li class="menu">
            <cwc:CustomCheckBox runat="server" ID="cwcNewWidnow" CssClass="checkbox" CssCheckedClass="checkbox selected" Text="Open in new Window" AutoPostBack="false" Checked="false"></cwc:CustomCheckBox>
            <asp:LinkButton ID="LinkButton1" Text="Забравена парола" PostBackUrl="~/PasswordRecovery.aspx" runat="server" />
        </li>--%>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
