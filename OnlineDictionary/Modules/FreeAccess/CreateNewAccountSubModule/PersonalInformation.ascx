<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PersonalInformation.ascx.cs"
    Inherits="Modules_FreeAccess_CreateNewAccountSubModule_PersonalInformation" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>

<asp:UpdatePanel ID="UpdatePanelPersonalInformation" ChildrenAsTriggers="true" UpdateMode="Always" runat="server">
    <ContentTemplate>
            <div class="regForm">
                <div class="regInput">
                    <asp:Label CssClass="label" ID="lblFirstName" Text="Име:" AssociatedControlID="txtFirstName" runat="server" />
                    <asp:TextBox ID="txtFirstName" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvFirstName" Text="*" ErrorMessage="Въведете вашето име."
                        ToolTip="Полето име не може да бъде празно." ControlToValidate="txtFirstName"
                        ValidationGroup="PersonalInformation" runat="server" />
                    <asp:RegularExpressionValidator ID="revFirstName" ErrorMessage="Required" ToolTip="Въведете вашето име."
                        ControlToValidate="txtFirstName" ValidationGroup="PersonalInformation" ValidationExpression="[\w\s\-_&]*"
                        runat="server" />
                </div>
                <div class="regInput">
                    <asp:Label CssClass="label" ID="lblLastName" Text="Фамилия:" AssociatedControlID="txtLastName" runat="server" />
                    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" Text="*" ErrorMessage="Въведете вашата фамилия."
                        ToolTip="Полето фамилия не може да бъде празно." ControlToValidate="txtLastName"
                        ValidationGroup="PersonalInformation" runat="server" />
                    <asp:RegularExpressionValidator ID="revLastName" ErrorMessage="Required" ToolTip="Въведете вашата фамилия."
                        ControlToValidate="txtLastName" ValidationGroup="PersonalInformation" ValidationExpression="[\w\s\-_&]*"
                        runat="server" />
                </div>
                <div class="regInput">
                    <asp:Label ID="lblCountry" CssClass="label leftFloat" Text="Държава" AssociatedControlID="ddlCountry" runat="server" />
                    <cwc:CustomDropDown CssClass="defaultDD rightFloat" AppendDataBoundItems="true" ID="ddlCountry" runat="server">
                        <asp:ListItem Text="Държави" Value="0"></asp:ListItem>
                    </cwc:CustomDropDown>
                    <asp:CompareValidator ID="cvCountry" ValidationGroup="PersonalInformation" runat="server" ControlToValidate="ddlCountry"
                        ValueToCompare="0" Operator="NotEqual" Text="*" ErrorMessage="Изберете държава."></asp:CompareValidator>
                </div>
                <asp:HiddenField ID="hdnEditPersonalInformation" runat="server" />
                <div>
                    <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
                </div>
            </div>
            <div>
                <asp:ValidationSummary CssClass="labelError" ValidationGroup="PersonalInformation" runat="server" />    
            </div>
            <div class="regControls">
            <asp:LinkButton ID="lbtnNextStep" CssClass="button rightFloat" ValidationGroup="PersonalInformation" Text="Напред"
                OnClick="lbtnNextStep_Click" runat="server" />
                </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
