<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AccountInformation.ascx.cs"
    Inherits="Modules_RestrictedAccess_AccountInformation" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelAccountSecurity" ChildrenAsTriggers="true" UpdateMode="Always"
    runat="server">
    <ContentTemplate>
        <h2>
            <span class="firstLetter">И</span>нформация</h2>
        <div class="information">
            <div class="informationLeft ">
                <div class="img">
                <asp:Image ID="imgPersonalPicture" ImageUrl="~/App_Themes/Default/images/logo.png"
                    AlternateText="Лична снимка" runat="server" />
                    </div>
                <asp:LinkButton ID="lbtnChangePicture" CssClass="buttonSlim  margintop10" Text="Промени" OnClick="lbtnChangePicture_Click"
                    runat="server" />
            </div>
            <asp:Panel CssClass="PopUp2" ID="pnlChangePicture" Visible="false" runat="server">
                <div>  <%--BrowserHidden--%>
                    <asp:TextBox CssClass="FileField" Enabled="false" ClientIDMode="Static" runat="server" />
                    <asp:FileUpload ID="fuSource" CssClass="BrowserHidden uploadFiles" onchange="jQuery('.FileField').val(jQuery('input[type=file]').attr('value'));" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvSource" Text="*" ErrorMessage="Изберете снимка." ControlToValidate="fuSource" 
                    runat="server" ValidationGroup="changeImage"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ValidationGroup="changeImage" ID="rexp" runat="server" ControlToValidate="fuSource"
                            ErrorMessage="Може да добавите само снимка." Text="*" ValidationExpression="(.*\.([jJ][pP][gG])$)|(.*\.([jJ][pP][gG][eE])$)|(.*\.([bB][mM][pP])$)"></asp:RegularExpressionValidator>
                    <asp:LinkButton Text="Намери" CssClass="buttonSlim" runat="server" />
                    <asp:ValidationSummary ID="vfChangeImage" CssClass="labelError" ValidationGroup="changeImage" runat="server" />
                    <asp:LinkButton ID="lbtnCancelChangePicture" Text="Отказ" CssClass="buttonSlim margintop10" OnClick="lbtnCancelChangePicture_Click"
                        runat="server" />
                    <asp:LinkButton ID="lbtnSaveChangePicture" ValidationGroup="changeImage" CssClass="rightFloat margintop10 buttonSlim" Text="Запази" OnClick="lbtnSaveChangePicture_Click"
                        runat="server" />
                        <asp:Label CssClass="labelError" ID="lblResultMessage" runat="server" />
                </div>
            </asp:Panel>
            <div class="informationRight">
                <div>
                    <div class="infoLine">
                        <asp:Label ID="lblFirstName" Text="Име" CssClass="label" AssociatedControlID="txtFirstName"
                            runat="server" />
                        <asp:TextBox ID="txtFirstName" ToolTip="Вашето собствено име." runat="server" />
                        <asp:RequiredFieldValidator ErrorMessage="Въведете вашето собствено име." ValidationGroup="changeNames"
                            ControlToValidate="txtFirstName" Text="*" runat="server" />
                    </div>
                    <div class="infoLine">
                        <asp:Label Text="Фамилия" CssClass="label" AssociatedControlID="txtLastName" runat="server" />
                        <asp:TextBox ID="txtLastName" ToolTip="Вашата фамилия." runat="server" />
                        <asp:RequiredFieldValidator ID="rfvChangeLastName" ErrorMessage="Въведете вашaтa фамилия."
                            ValidationGroup="changeNames" ControlToValidate="txtLastName" Text="*" runat="server" />
                    </div>
                    <div>
                        <asp:ValidationSummary ValidationGroup="changeNames" runat="server" />
                    </div>
                    <div>
                        <asp:LinkButton ID="lbtnCancelNames" CssClass="buttonSlim rightFloat" Text="Отказ" OnClick="lbtnCancelNames_Click"
                            runat="server" />
                        <asp:LinkButton ID="lbtnSaveNames" CssClass="buttonSlim" ValidationGroup="changeNames"
                            Text="Запази" OnClick="lbtnSaveNames_Click" runat="server" />
                    </div>
                    <asp:LinkButton ID="lbtnChangeNames" CssClass="buttonSlim rightFloat" Text="Промени" OnClick="lbtnChangeNames_Click"
                        runat="server" />
                </div>
                <hr class="leftClear rightClear" />
                <div>
                    <%--<h4>
                        Адрес</h4>--%>
                    <div class="infoLine">
                        <asp:Label ID="lblCountry" CssClass="label leftFloat" Text="Държава" AssociatedControlID="txtCountry"
                            runat="server" />
                        <asp:TextBox ID="txtCountry" ToolTip="Държава." runat="server" />
                        <cwc:CustomDropDown CssClass="defaultDD rightFloat" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" ID="ddlCountry" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Text="Държава" Value="0"></asp:ListItem>
                        </cwc:CustomDropDown>
                        <asp:CompareValidator ErrorMessage="Изберете държава" ControlToValidate="ddlCountry"
                            runat="server" ValidationGroup="editAccount" Text="*" ValueToCompare="0" Operator="NotEqual" />
                    </div>
                    <div class="infoLine">
                        <asp:Label ID="lblArea" CssClass="label leftFloat" Text="Област" AssociatedControlID="txtArea" runat="server" />
                        <asp:TextBox ID="txtArea" ToolTip="Област." runat="server" />
                        <cwc:CustomDropDown CssClass="defaultDD rightFloat" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" AppendDataBoundItems="true" ID="ddlArea"
                            runat="server">
                            <asp:ListItem Text="Област" Value="0"></asp:ListItem>
                        </cwc:CustomDropDown>
                    </div>
                    <div class="infoLine">
                        <asp:Label ID="lblCity" CssClass="label leftFloat" Text="Град" AssociatedControlID="txtCity"
                            runat="server" />
                        <asp:TextBox ID="txtCity" ToolTip="Град." runat="server" />
                        <cwc:CustomDropDown CssClass="defaultDD rightFloat" AutoPostBack="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" ID="ddlCity" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Text="Град" Value="0"></asp:ListItem>
                        </cwc:CustomDropDown>
                    </div>
                    <div class="infoLine">
                        <asp:Label ID="lblVillage" CssClass="label leftFloat" Text="Село" AssociatedControlID="txtVillage"
                            runat="server" />
                        <asp:TextBox ID="txtVillage" ToolTip="Село." runat="server" />
                        <cwc:CustomDropDown CssClass="defaultDD rightFloat" ID="ddlVillage" AppendDataBoundItems="true"
                            runat="server">
                            <asp:ListItem Text="Село" Value="0"></asp:ListItem>
                        </cwc:CustomDropDown>
                    </div>
                    <div class="infoLine">
                        <asp:Label ID="lblAddress" CssClass="label" Text="Адрес" AssociatedControlID="txtAddress"
                            runat="server" />
                        <asp:TextBox ID="txtAddress" CssClass="regNotes" TextMode="MultiLine" runat="server" />
                    </div>
                    <div>
                        <asp:ValidationSummary ValidationGroup="editAccount" runat="server" />
                    </div>
                    <div>
                        <asp:LinkButton ID="lbtnCancelAddress" CssClass="buttonSlim rightFloat" Text="Отказ" OnClick="lbtnCancelAddress_Click"
                            runat="server" />
                        <asp:LinkButton ID="lbtnSaveAddress" CssClass="buttonSlim" ValidationGroup="editAccount"
                            Text="Запази" OnClick="lbtnSaveAddress_Click" runat="server" />
                    </div>
                    <asp:LinkButton ID="lbtnChangeAddress" CssClass="buttonSlim rightFloat" Text="Промени" OnClick="lbtnChangeAddress_Click"
                        runat="server" />
                </div>
                <hr class="leftClear rightClear" />
                <div>
                    <div class="rightFloat">
                        <%--<h3>
                            Сигурност</h3>--%>
                    </div>
                    <asp:Panel ID="pnlChangePasswordShow" runat="server">
                    
                        <asp:Label Text="Промяна на парола" CssClass="label" AssociatedControlID="lbtnChangePassword"
                            runat="server" />
                        <asp:LinkButton ID="lbtnChangePassword" CssClass="buttonSlim" Text="Промени" OnClick="lbtnChangePassword_Click"
                            runat="server" />
                            </asp:Panel>
                    <div class="jsPanelChangePassword">
                        <asp:ChangePassword Visible="false" ID="ChangePasswordAccountInformation" runat="server">
                            <ChangePasswordTemplate>
                                <div>
                                    <span class="label marginleft10pr">Промяна на парола</span>
                                </div>
                                <div class="infoLine">
                                    <asp:Label ID="CurrentPasswordLabel" CssClass="label" runat="server" AssociatedControlID="CurrentPassword">Парола:</asp:Label>
                                    <asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                                        ErrorMessage="Въведете вашата актуална парола." ToolTip="Актуална парола." Text="*"
                                        ValidationGroup="ctl00$ChangePasswordAccountInformation"></asp:RequiredFieldValidator>
                                </div>
                                <div class="infoLine">
                                    <asp:Label ID="NewPasswordLabel" runat="server" CssClass="label" AssociatedControlID="NewPassword">Нова парола:</asp:Label>
                                    <asp:TextBox ID="NewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                                        ErrorMessage="Въведете вашата нова парола." ToolTip="Нова парола." ValidationGroup="ctl00$ChangePasswordAccountInformation"
                                        Text="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="infoLine">
                                    <asp:Label ID="ConfirmNewPasswordLabel" CssClass="label" runat="server" AssociatedControlID="ConfirmNewPassword">Потвърдете:</asp:Label>
                                    <asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                                        ErrorMessage="Потвърдете вашата нова парола." ToolTip="Потвърждаване на новата парола."
                                        ValidationGroup="ctl00$ChangePasswordAccountInformation" Text="*"></asp:RequiredFieldValidator>
                                </div>
                                <div class="infoLine">
                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword"
                                        ControlToValidate="ConfirmNewPassword" Display="Dynamic" ErrorMessage="Новите пароли трябва да бъдат еднакви."
                                        ValidationGroup="ctl00$ChangePasswordAccountInformation"></asp:CompareValidator>
                                </div>
                                <div>
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                </div>
                                <div>
                                    <asp:LinkButton ID="lbtnCancelChangePassword" Text="Отказ" OnClick="lbtnCancelChangePassword_Click" CssClass="buttonSlim" runat="server" />
                                <asp:LinkButton ID="CancelPushButton" Visible="false" CssClass="buttonSlim" runat="server" CausesValidation="False"
                                        CommandName="Cancel" Text="Отказ" />
                                    <asp:LinkButton ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword"
                                        Text="Промени" CssClass="buttonSlim" ValidationGroup="ctl00$ChangePasswordAccountInformation" />
                                    
                                </div>
                            </ChangePasswordTemplate>
                        </asp:ChangePassword>
                    </div>
                </div>
                <div>
                    <asp:Label ID="lblError" runat="server" />
                </div>
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="lbtnSaveChangePicture" />
    </Triggers>
</asp:UpdatePanel>
