<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Settings.ascx.cs" Inherits="Modules_AdminModules_Settings" %>
<%@ Register Src="AllUsers.ascx" TagName="AllUsers" TagPrefix="uc" %>
<%@ Register Src="Reviews.ascx" TagName="Reviews" TagPrefix="uc" %>
<%@ Register Src="Library.ascx" TagName="Library" TagPrefix="uc" %>
<asp:UpdatePanel ID="UpdatePanelSettings" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2 id="spTitle" runat="server">
            <span class="firstLetter">Н</span>астройки</h2>
        <div class="settings">
            <asp:LinkButton ID="lbtnBack" Text="Назад" CssClass="buttonDeffalt btnBackPossiton"
                Visible="false" OnClick="lbtnBack_Click" runat="server" />
            <asp:Panel ID="pnlOptions" CssClass="margintop paddingtop45 marginleft50" runat="server">
                <div class="btnSettings">
                    <asp:ImageButton ID="lbtnLibrary" ImageUrl="~/App_Themes/Default/images/btnLibrary.png"
                        Width="128" Height="128" OnClick="lbtnLibrary_Click" runat="server" />
                    <span class="label">Библиотека</span>
                </div>
                <div class="btnSettings">
                    <asp:ImageButton ID="lbtnAllUsers" ImageUrl="~/App_Themes/Default/images/btnUsers.png"
                        Width="128" Height="128" OnClick="lbtnAllUsers_Click" runat="server" />
                    <span class="label marginleft10">Потребители</span>
                </div>
                <div class="btnSettings">
                    <asp:ImageButton ID="lbtnReviews" ImageUrl="~/App_Themes/Default/images/btnReviews.png"
                        Width="128" Height="128" OnClick="lbtnReviews_Click" runat="server" />
                    <span class="label marginleft10">Отзиви</span>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlAllUsers" Visible="false" runat="server">
                <uc:AllUsers ID="ucAllUsers" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlReviews" Visible="false" runat="server">
                <uc:Reviews ID="ucReviews" runat="server" />
            </asp:Panel>
            <asp:Panel ID="pnlLibrary" Visible="false" runat="server">
                <uc:Library ID="ucLibrary" runat="server" />
            </asp:Panel>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
