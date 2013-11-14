<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountSecurity.ascx.cs"
    Inherits="Modules_FreeAccess_CreateNewAccountSubModule_AccountSecurity" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>

<asp:UpdatePanel ID="UpdatePanelAccountSecurity" ChildrenAsTriggers="true" UpdateMode="Always" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlAccountSecurity" runat="server">
            <div class="regForm" >
                <div class="regInput">
                    <asp:Label ID="lblEmail" CssClass="label" Text="Email:" runat="server" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" ToolTip="Въведете вашият Email адрес." runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" Text="*" ControlToValidate="txtEmail" ErrorMessage="Въведете E-mail адрес."
                        ToolTip="Полето Email трябва да бъде попълнено." ValidationGroup="AccountSecurity"
                        runat="server"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regEmail" ValidationGroup="AccountSecurity" ControlToValidate="txtEmail"
                        ErrorMessage="Въведеният E-mail е в грешен формат." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        runat="server"></asp:RegularExpressionValidator>
                </div>
                <div class="regInput" >
                    <asp:Label ID="lblPassword" CssClass="label" runat="server" Text="Парола:" AssociatedControlID="txtPassword"></asp:Label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" ToolTip="Въведете вашата парола."
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" Text="*" ControlToValidate="txtPassword"
                        ErrorMessage="Въведете парола." ToolTip="Полето парола трябва да бъде попълнено."
                        ValidationGroup="AccountSecurity" runat="server"></asp:RequiredFieldValidator>
                </div>
                <div class="regInput">
                    <asp:Label ID="lblConfirmPassword" CssClass="label" Text="Отново:" AssociatedControlID="txtConfirmPassword"
                        runat="server" />
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" ToolTip="Въведете отново вашата парола."
                        runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" Text="*" ControlToValidate="txtConfirmPassword"
                        ErrorMessage="Повторете вашата парола." ToolTip="Полето за повторени на паролата трябва да бъде попълнено."
                        ValidationGroup="AccountSecurity" runat="server"></asp:RequiredFieldValidator>
                </div>
                <div class="regInput">
                    <asp:Label ID="lblCurrentGeneratePassword" CssClass="label" Visible="false" Text="Текуща парола: "
                        AssociatedControlID="lblGeneratePassword" runat="server" />
                    <asp:Label ID="lblGeneratePassword" AssociatedControlID="lbtnGeneratePassword" runat="server" />
                </div>
                <div class="regInput" >
                    <asp:LinkButton ID="lbtnGeneratePassword" CssClass="buttonSlim" Text="Генериране на парола" OnClick="lbtnGeneratePassword_Click"
                        ToolTip="Автоматично генериране на парола." runat="server"></asp:LinkButton>
                </div>
            
            <div class="regInput">
                <asp:Label ID="lblQuestion" CssClass="label" Text="Таен въпрос:" AssociatedControlID="txtQuestion"
                    runat="server" />
                <asp:TextBox ID="txtQuestion" ToolTip="Въпрос за сигурност." runat="server" />
                <asp:RequiredFieldValidator ID="rfvQuestion" Text="*" ControlToValidate="txtQuestion"
                    ErrorMessage="Въведете таен въпрос." ToolTip="Полето таен въпрос трябва да бъде попълнено."
                    ValidationGroup="AccountSecurity" runat="server"></asp:RequiredFieldValidator>
            </div>
            <div class="regInput">
                <asp:Label ID="lblAnswer" CssClass="label" Text="Отговор:" AssociatedControlID="txtAnswer" runat="server" />
                <asp:TextBox ID="txtAnswer" EnableViewState="true" TextMode="Password" ToolTip="Отговор на въпроса за сигурност."
                    runat="server" />
                <asp:RequiredFieldValidator ID="rfvAnswer" Text="*" ControlToValidate="txtAnswer"
                    ErrorMessage="Въведете отговор за тайният въпрос." ToolTip="Полето за отговор на въпроса трябва да бъде попълнено."
                    ValidationGroup="AccountSecurity" runat="server"></asp:RequiredFieldValidator>
            </div>
            <div class="regInput">
                <asp:Label ID="lblConfirmAnswer" CssClass="label" Text="Отново:" AssociatedControlID="txtConfirmAnswer"
                    runat="server" />
                <asp:TextBox ID="txtConfirmAnswer" EnableViewState="true" TextMode="Password" ToolTip="Повторен отговор на въпроса за сигурност."
                    runat="server" />
                <asp:RequiredFieldValidator ID="rfvConfirmAnswer" Text="*" ControlToValidate="txtConfirmAnswer"
                    ErrorMessage="Повторете отговора на тайният въпрос." ToolTip="Полето за повторен отговор на въпроса трябва да бъде попълнено."
                    ValidationGroup="AccountSecurity" runat="server"></asp:RequiredFieldValidator>
            </div>
            <div class="regInput">
                <asp:Label ID="lblReminder" CssClass="label" Text="Напомняне:" AssociatedControlID="txtReminder" runat="server" />
                <asp:TextBox ID="txtReminder" CssClass="regNotes" TextMode="MultiLine" ToolTip="Имнформация подсказваща отговора на тайният въпрос."
                    runat="server" />
            </div>
            <div class="regApproved" >
                <asp:Label ID="lblApproved" CssClass="label" runat="server" AssociatedControlID="chkApproved" ToolTip="Профилът ще бъде активен."
                    Text="Активен"></asp:Label>
                <cwc:CustomCheckBox ID="chkApproved" CssClass="checkbox selected" CssCheckedClass="checkbox" Checked="true" runat="server" />
            </div>
            <asp:HiddenField ID="hdnEditAccountSecutiry" runat="server" />
           
                <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
            </div>
            <asp:ValidationSummary ID="vldSummary" CssClass="labelError" runat="server" ValidationGroup="AccountSecurity" />
            <div class="regControls">
            <asp:LinkButton ID="lbtnClear" CssClass="marginleft50 button leftFloat" Text="Изчисти" OnClick="lbtnClear_Click" runat="server" />
            <asp:LinkButton ID="lbtnNextStep" CssClass="button rightFloat" ValidationGroup="AccountSecurity" Text="Напред"
                OnClick="lbtnNextStep_Click" runat="server" />
                </div>
                 <div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
