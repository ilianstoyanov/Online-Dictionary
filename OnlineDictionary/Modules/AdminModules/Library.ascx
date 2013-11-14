<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Library.ascx.cs" Inherits="Modules_AdminModules_Library" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelLibrary" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2 class="subtitlePostion margintop10">
            <span class="firstLetter">Б</span>иблиотека</h2>
        <asp:Panel ID="pnlExistingsWords" runat="server">
            <div class="searchOption margintop20">
                <div class="searchCtrl">
                    <asp:Label ID="Label1" CssClass="labelDD" Text="Търси за:" AssociatedControlID="ddlSearchMethod"
                        runat="server" />
                    <cwc:CustomDropDown CssClass="defaultDD" AutoPostBack="true"
                        AppendDataBoundItems="true" ID="ddlSearchMethod" runat="server">
                        <asp:ListItem Text="Всички съвпадения" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Точна дума" Value="2"></asp:ListItem>
                    </cwc:CustomDropDown>
                    <asp:Label ID="lblLanguage" CssClass="labelDD marginleft30" Text="Език:" AssociatedControlID="ddlLanguage"
                        runat="server" />
                    <cwc:CustomDropDown CssClass="defaultDD" AutoPostBack="true" OnTextChanged="lbtnSearch_Click"
                         OnSelectedIndexChanged="ddlSearch_selectedIndexChanged" AppendDataBoundItems="true" ID="ddlLanguage" runat="server">
                        <asp:ListItem Text="Английски-Български" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Български-Английски" Value="2"></asp:ListItem>
                    </cwc:CustomDropDown>
                    <asp:Label ID="lblTotalWords" CssClass="label rightFloat" runat="server" />
                </div>
                
            </div>
            <div>
                <asp:Label ID="lblResult" runat="server" />
            </div>
            <div class="marginleft15pr margintop30">
                <div class="leftFloat marginleft50 marginbottom10">
                    <asp:Label ID="lblSearh" CssClass="label" Text="Търсене:" AssociatedControlID="txtSearch"
                        runat="server" />
                    <asp:TextBox ID="txtSearch" CssClass="sizex250" OnTextChanged="lbtnSearch_Click"
                        AutoPostBack="true" runat="server" />
                    <asp:LinkButton ID="lbtnSearch" Text="Търси" CssClass="buttonSlim" OnClick="lbtnSearch_Click"
                        runat="server" />
                </div>
                <div class="customPaging rightFloat">
                    <asp:LinkButton ID="lbtnPrevious" CssClass="left" OnClick="lbtnPrevious_Click" runat="server" />
                    <asp:TextBox ID="txtCurrentPage" OnTextChanged="txtCurrentPage_TextChanged" AutoPostBack="true"
                        runat="server" />
                    <asp:Label ID="lblOf" Text=" of " runat="server" />
                    <asp:Label ID="lblTotalItems" runat="server" />
                    <asp:LinkButton ID="lbtnNext" CssClass="right" OnClick="lbtnNext_Click" runat="server" />
                    <asp:Label ID="lblTotalUsers" runat="server" />
                </div>
            </div>
            <div class="contentTable noLastCell">
                <asp:GridView ID="GridViewAllWords" OnRowCommand="GridViewAllWords_RowCommand" AutoGenerateColumns="false"
                    runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <span class="leftFloat sizex130 paddingrightx5">Дума</span> 
                                <span class="leftFloat sizex130 paddingrightx5">Транскрипция</span> 
                                <span class="leftFloat sizex400 paddingrightx5">Описание</span>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div>
                                    <span class="leftFloat sizex130 paddingrightx5">
                                        <%# Eval("Word") %></span> <span class="leftFloat sizex130 paddingrightx5">
                                            <%# Eval("Тranscription")%></span> <span class="leftFloat sizex400 paddingrightx5">
                                                <%# Eval("Description")%></span>
                                    <asp:LinkButton ID="lbtnDeleteWord" CssClass="buttonSlim rightFloat " Text="Изтрии"
                                        CommandArgument='<%# Eval("Id") %>' CommandName="DeleteWord" runat="server" />
                                    <asp:LinkButton ID="lbtnEditWord" CssClass="buttonSlim rightFloat " Text="Промени"
                                        CommandArgument='<%# Eval("Id") %>' CommandName="EditWord" runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="customPaging rightFloat margintop20">
                <asp:LinkButton ID="lbtnPreviousBottom" CssClass="left" OnClick="lbtnPrevious_Click"
                    runat="server" />
                <asp:TextBox ID="txtCurrentPageBottom" OnTextChanged="txtCurrentPage_TextChanged"
                    AutoPostBack="true" runat="server" />
                <asp:Label ID="lblOfBottom" Text=" of " runat="server" />
                <asp:Label ID="lblTotalItemsBottom" runat="server" />
                <asp:LinkButton ID="lbtnNextBottom" CssClass="right" OnClick="lbtnNext_Click" runat="server" />
            </div>
            <asp:LinkButton ID="lbtnAddNewWord" CssClass="buttonDeffalt margintop10" Text="Добави нова дума"
                OnClick="lbtnAddNewWord_Click" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlEditWord" CssClass="editWordGeneral" Visible="false" runat="server">
            <div class="leftFloat">
                <div class="margintop10">
                    <asp:Label CssClass="label" ID="lblWord" Text="Дума:" AssociatedControlID="txtWord"
                        runat="server" />
                    <asp:TextBox ID="txtWord" CssClass="rightFloat" runat="server" />
                    <asp:RequiredFieldValidator ID="rfvWord" Text="*" ErrorMessage="Полето Дума не може да бъде празно."
                        ValidationGroup="EditWord" ControlToValidate="txtWord" runat="server" />
                </div>
                <div class="margintop10">
                    <asp:Label CssClass="label" ID="lblТranscription" Text="Транскрипция:" AssociatedControlID="txtТranscription"
                        runat="server" />
                    <asp:TextBox CssClass="rightFloat" ID="txtТranscription" runat="server" />
                </div>
                <div class="margintop10">
                    <asp:Label CssClass="label margintop30" ID="lblDescription" Text="Описание:" AssociatedControlID="txtDescription"
                        runat="server" />
                    <asp:TextBox CssClass="rightFloat regNotes" ID="txtDescription" TextMode="MultiLine"
                        runat="server" />
                    <asp:RequiredFieldValidator ID="rfvDescription" Text="*" ErrorMessage="Полето Описание не може да бъде празно."
                        ValidationGroup="EditWord" ControlToValidate="txtDescription" runat="server" />
                </div>
                <asp:ValidationSummary ID="vldEditWords" ValidationGroup="EditWord" runat="server" />
                <div class="editWordControls">
                    <asp:HiddenField ID="hdnCurrentWordId" runat="server" />
                    <asp:LinkButton ID="lbtnCancelChanges" CssClass="buttonDeffalt" Text="Отказ" OnClick="lbtnCancelChanges_Click"
                        runat="server" />
                    <asp:LinkButton ID="lbtnSaveChanges" CssClass="buttonDeffalt rightFloat" Text="Запази"
                        OnClick="lbtnSaveChanges_Click" ValidationGroup="EditWord" runat="server" />
                </div>
                <asp:Label CssClass="label" ID="lblError" runat="server" />
            </div>
            <div class="editWordRight">
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoFound" Visible="false" runat="server">
            <h2>
                Не бяха открите съвпадения.</h2>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
