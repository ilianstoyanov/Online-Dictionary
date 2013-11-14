<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateNewAccount.ascx.cs" Inherits="Modules_FreeAccess_CreateNewAccount" %>
<%@ Register Src="CreateNewAccountSubModule/AccountSecurity.ascx" TagName="AccountSecurity" TagPrefix="uc" %>
<%@ Register Src="CreateNewAccountSubModule/PersonalInformation.ascx" TagName="PersonalInformation" TagPrefix="uc" %>
<%@ Register Src="CreateNewAccountSubModule/AdditionalInformation.ascx" TagName="AdditionalInformation" TagPrefix="uc" %>
<%@ Register Src="CreateNewAccountSubModule/SetupCompleted.ascx" TagName="SetupCompleted" TagPrefix="uc" %>

<asp:UpdatePanel ChildrenAsTriggers="false" ID="UpdatePanelNewAccount" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
    <h2><span class="firstLetter">Р</span>егистрация</h2>
        <asp:Panel ID="pnlCreateNewAccount"  runat="server">
            <span class="subTitle">Направете вашата регистрация в 3 лесни стъпки.</span>
            <div>
                <asp:Panel ID="pnlCreateNewAccountSetupSteps" runat="server">
                    <asp:LinkButton ID="lbtnAccountSecurity" Text="Сигурност" CommandArgument="AccountSecurity" OnClick="btnStep_Click" runat="server" />
                    <asp:LinkButton ID="lbtnPersonalInformation" Text="Лична информация" CommandArgument="PersonalInformation" Enabled="false" OnClick="btnStep_Click" runat="server" />
                    <asp:LinkButton ID="lbtnAdditionalInformation" Text="Допълнителна Информаия" CommandArgument="AdditionalInformation" OnClick="btnStep_Click" Enabled="false" runat="server" />
                </asp:Panel>
                <uc:AccountSecurity ID="stepAccountSecurity" Visible="true" runat="server" />
                <uc:PersonalInformation ID="stepPersonalInformation" Visible="false" runat="server" />
                <uc:AdditionalInformation ID="stepAdditionalInformation" Visible="false" runat="server" />
                <uc:SetupCompleted ID="stepCompleted" Visible="false" runat="server" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlSuccess" CssClass="regCompleted" Visible="false" runat="server">
            <h2>Вашата регистрация е направена успешно.</h2>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>