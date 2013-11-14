<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Reviews.ascx.cs" Inherits="Modules_AdminModules_Reviews" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<asp:UpdatePanel ID="UpdatePanelReviews" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2 class="subtitlePostion ">
            <span class="firstLetter">О</span>тзиви</h2>
        <div class="reviewGeneral">
            <asp:Repeater ID="rptExistingReview" runat="server">
                <ItemTemplate>
                    <div class="leftClear review">
                        <div class="reviewIn">
                            <span class="showUserRating  rating<%# Eval("rating").ToString().Replace(".","") %>">
                            </span>
                            <div class="reviewName">
                                <span class="reviewText">
                                    <%# Eval("firstName") %></span> <span class="reviewText">
                                        <%# Eval("lastName") %></span>
                            </div>
                            <hr />
                            <div class="reviewPersonal">
                                <span>
                                    <%# Eval("personalReview") %></span>
                            </div>
                            <div class="">
                                <asp:Label ID="lblPublished" Text="Публикуван" AssociatedControlID="chkPublished"
                                    runat="server" />
                                <cwc:CustomCheckBox ToolTip='<%# Eval("date") %>' ID="chkPublished" CssClass="checkbox selected"
                                    CssCheckedClass="checkbox" AutoPostBack="true" Checked='<%# Eval("published") %>'
                                    OnCheckedChanged="chkPublished_CheckedChanged" runat="server" />
                                <asp:LinkButton ID="lbtnDelete" Text="Изтрии" CssClass="buttonSlim rightFloat marginright20"
                                    CommandArgument='<%# Eval("date") %>' CommandName="" OnCommand="lbtnDelete_Click"
                                    runat="server" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
