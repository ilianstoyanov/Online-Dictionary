<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShortSearch.ascx.cs" Inherits="Modules_FreeAccess_ShortSearch" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelSearch" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2>
            <span class="firstLetter">П</span>ревод</h2>
        <div class="search">
        <div class="searchOption">
            <asp:Label CssClass="labelDD" Text="Търси за:" AssociatedControlID="ddlSearchMethod" runat="server" />
        <cwc:CustomDropDown CssClass="defaultDD" AutoPostBack="true" OnTextChanged="Repeater_PageChanged"
                    AppendDataBoundItems="true" ID="ddlSearchMethod" runat="server">
                    <asp:ListItem Text="Всички съвпадения" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Точна дума" Value="2"></asp:ListItem>
                </cwc:CustomDropDown>
                <asp:Label ID="Label3" CssClass="labelDD marginleft10" Text="Език:" AssociatedControlID="ddlLanguage" runat="server" />
                <cwc:CustomDropDown CssClass="defaultDD" AutoPostBack="true" OnTextChanged="Repeater_PageChanged"
                    ID="ddlLanguage" runat="server">
                </cwc:CustomDropDown>
        </div>
        <asp:Panel ID="pnlSearch" DefaultButton="lbtnTranslate"  class="searchContainer" runat="server" >
            <div class="searchForm" >
                <asp:Label ID="lblSearh" CssClass="labelDD" Text="Търсене:" AssociatedControlID="txtSearch" runat="server" />
                <asp:TextBox CssClass="TextBox leftFloat " ID="txtSearch" OnTextChanged="lbtnTranslate_Click" AutoPostBack="true"
                    ToolTip="Въвдете думата на която искате да ознаете значението." runat="server" />
                <asp:LinkButton ID="lbtnTranslate" CssClass="buttonDeffalt leftFloat" Text="Превод" OnClick="lbtnTranslate_Click" runat="server" />
                
                <asp:Label ID="lblResultCount" Visible="false" CssClass="label rightFloat" runat="server" />
            </div>
            <asp:Panel ID="pnlEnterWord"  runat="server">
                <div class="searchStart"></div>
                <p class="enterWordText" >Въведете желаната дума.</p>
            </asp:Panel>
            
                <asp:Panel ID="pnlPagingTop" CssClass="customPaging rightFloat" Visible="false" runat="server">
                    <asp:LinkButton ID="lbtnPrevious" CssClass="left" OnClick="lbtnPrevious_Click" runat="server" />
                    <asp:TextBox ID="txtCurrentPage" OnTextChanged="txtCurrentPage_TextChanged" AutoPostBack="true"
                        runat="server" />
                    <asp:Label ID="lblOf" Text=" of " runat="server" />
                    <asp:Label ID="lblTotalItems" runat="server" />
                    <asp:LinkButton ID="lbtnNext" CssClass="right" OnClick="lbtnNext_Click" runat="server" />
                </asp:Panel>
                
                <asp:Repeater ID="rptSearchResult" runat="server">
                    <ItemTemplate>
                        <div class="searchResult">
                            <h3>
                                <%# Eval("Word") %></h3>
                            <h4>
                                Транскрипция</h4>
                            <%# Eval("Тranscription") %>
                            <h3>
                                Описание</h3>
                            <%# Eval("Description") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pnlPaigingBottom" Visible="false" runat="server">
                    <asp:LinkButton ID="lbtnPreviousBottom" OnClick="lbtnPrevious_Click" runat="server" />
                    <asp:TextBox ID="txtCurrentPageBottom" OnTextChanged="txtCurrentPage_TextChanged"
                        AutoPostBack="true" runat="server" />
                    <asp:Label ID="lblOfBottom" Text=" of " runat="server" />
                    <asp:Label ID="lblTotalItemsBottom" runat="server" />
                    <asp:LinkButton ID="lbtnNextBottom" Text=">>" OnClick="lbtnNext_Click" runat="server" />
                </asp:Panel>
                <asp:Panel ID="pnlNoFound" Visible="false" runat="server">
                    <h3 class="label" >
                        Не бяха открите съвпадения.</h3>
                </asp:Panel>
            
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
