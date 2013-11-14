<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdditionalInformation.ascx.cs"
    Inherits="Modules_FreeAccess_CreateNewAccountSubModule_AdditionalInformation" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelAdditionalInformation" ChildrenAsTriggers="true"
    UpdateMode="Always" runat="server">
    <ContentTemplate>
        <div class="regForm">
        <div class="regInput">
                <asp:Label ID="lblPhone" CssClass="label leftFloat" Text="Телефон:" AssociatedControlID="txtPhone"
                    runat="server" />
                <asp:TextBox ID="txtPhone" runat="server" />
                <asp:RegularExpressionValidator ID="revPhone" ErrorMessage="Грешен формат." ToolTip="Въведеният телефон е в грешен формат."
                    ControlToValidate="txtPhone" ValidationGroup="AdditionalInformation" ValidationExpression="(^\D*(?:\d\D*){10,10}$)?"
                    runat="server" />
            </div>
            <div class="regInput">
                <asp:Label ID="lblArea" CssClass="label leftFloat" Text="Област:" AssociatedControlID="ddlArea"
                    runat="server" />
                <cwc:CustomDropDown CssClass="defaultDD rightFloat" OnSelectedIndexChanged="ddlArea_SelectedIndedChanged"
                    AppendDataBoundItems="true" AutoPostBack="true" ID="ddlArea" runat="server">
                    <asp:ListItem Text="Области" Value="0"></asp:ListItem>
                </cwc:CustomDropDown>
            </div>
            <div class="regInput">
                <asp:Label ID="lblCity" CssClass="label leftFloat" Text="Град:" AssociatedControlID="ddlCity"
                    runat="server" />
                <cwc:CustomDropDown Enabled="false" OnSelectedIndexChanged="ddlCity_SelectedIndedChanged"
                    AppendDataBoundItems="true" AutoPostBack="true" CssClass="defaultDD rightFloat"
                    ID="ddlCity" runat="server">
                    <asp:ListItem Text="Градове" Value="0"></asp:ListItem>
                </cwc:CustomDropDown>
            </div>
            <div class="regInput">
                <asp:Label ID="lblVillage" CssClass="label leftFloat" Text="Село:" AssociatedControlID="ddlVillage"
                    runat="server" />
                <cwc:CustomDropDown Enabled="false" AppendDataBoundItems="true" AutoPostBack="true"
                    CssClass="defaultDD rightFloat" ID="ddlVillage" runat="server">
                    <asp:ListItem Text="Села" Value="0"></asp:ListItem>
                </cwc:CustomDropDown>
            </div>
            <div class="regInput">
                <asp:Label ID="lblAddress" CssClass="label margintop10" Text="Адрес:" AssociatedControlID="txtAddress"
                    runat="server" />
                <asp:TextBox ID="txtAddress" CssClass="regNotes" TextMode="MultiLine" runat="server" />
            </div>
            
            <div class="regInput2">
                <asp:Label ID="lblNotes" CssClass="label margin50" Text="За мен:" AssociatedControlID="txtNotes" runat="server" />
                <asp:TextBox ID="txtNotes" CssClass="regNotes" TextMode="MultiLine" ToolTip="Лична информация която ще бъде споделена с други хора."
                    runat="server" />
            </div>
            <div class="regInput">
                
                <asp:FileUpload ID="fuSource"  accept="image/jpeg" onchange="jQuery('.FileField').val(jQuery('input[type=file]').attr('value'));" CssClass="BrowserHidden2 uploadFiles jsFuSource" runat="server" />
                    <asp:Label ID="Label1" CssClass="label " Text="Снимка" runat="server" />
                    <asp:TextBox ID="TextBox1" CssClass="FileField rightFloat margintop10  marginleft10" Enabled="false" runat="server" />
                    <asp:LinkButton ID="LinkButton1" Text="Намери" CssClass="buttonSlim margintop10 rightFloat" runat="server" />
                
                
            </div>
            
            <asp:Panel ID="pnlPreviewPicture" CssClass="jsPanelPreviewPicture" runat="server">
            </asp:Panel>
            <div>
                <asp:Label ID="lblError" runat="server" EnableViewState="False"></asp:Label>
            </div>
            <div class="regControls margintop80">
                <asp:LinkButton ID="lbtnClear" CssClass="button" Text="Изчисти" OnClick="lbtnClear_Click"
                    runat="server" />
                <asp:LinkButton ID="lbtnFinishStep" CssClass="button rightFloat marginright10" ValidationGroup="AdditionalInformation"
                    Text="Напред" OnClick="lbtnFinishStep_Click" runat="server" />
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lbtnFinishStep" />
    </Triggers>
</asp:UpdatePanel>
