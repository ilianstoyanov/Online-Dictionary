<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WriteAReview.ascx.cs"
    Inherits="Modules_RestrictedAccess_WriteAReview" %>
<asp:UpdatePanel ID="UpdatePanelReview" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2>
            <span class="firstLetter">О</span>тзиви</h2>
        <asp:Panel ID="pnlExistingReview" CssClass="reviewGeneral" runat="server">
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
                            <hr  class="margintop10" />
                            <div class="reviewPersonal">
                                <span>
                                    <%# Eval("personalReview") %></span>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <asp:LinkButton ID="lbtnWriteAReview" CssClass="buttonDeffalt marginleft50 marginCenter"
                Text="Оставете отзив" OnClick="lbtnWriteAReview_Click" runat="server" />
        </asp:Panel>
        <asp:Panel ID="pnlWriteAReview" Visible="false" runat="server">
            <div class="regForm">
                <div class="entry-content top-padding entryContent">
                    <asp:Literal ID="LitContent" runat="server"></asp:Literal>
                </div>
                <asp:Panel runat="server" ID="pnlLeaveReview" CssClass="aboutForm " Visible="true">
                    <div>
                        <div class="sizex350 margintop10 marginleft30">
                            <asp:Label CssClass="label" ID="lblFirstName" runat="server" AssociatedControlID="txtFirstName"
                                Text="Име"></asp:Label>
                            <asp:TextBox runat="server" CssClass="rightFloat" ID="txtFirstName" ToolTip="Вашето първо име."></asp:TextBox>
                        </div>
                        <div class="sizex350 margintop10 marginleft30">
                            <asp:Label CssClass="label" ID="Label1" runat="server" AssociatedControlID="txtLastName"
                                Text="Фамилия"></asp:Label>
                            <asp:TextBox runat="server" CssClass="rightFloat" ID="txtLastName" ToolTip="Вашата фамилия."></asp:TextBox>
                        </div>
                        <div>
                        <div class="sizex350 margintop10 marginleft30">
                            <asp:Label CssClass="label" ID="lblEmail" runat="server" AssociatedControlID="txtEmail"
                                Text="E-mail"></asp:Label>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="rightFloat" ToolTip="E-mail адрес."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" ValidationGroup="revform" runat="server"
                                ControlToValidate="txtEmail" Display="Dynamic" Text="*" ErrorMessage="Моля въведете вашият E-mail адрес."></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtEmail" ValidationGroup="revform" runat="server"
                                ControlToValidate="txtEmail" Display="Dynamic" CssClass="validator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Text="*" ErrorMessage="Въведеният E-mail адрес е невалиден."></asp:RegularExpressionValidator>
                            </div>
                            
                        </div>
                        <div class="sizex350 margintop10 marginleft30">
                            <div>
                                <span class="label marginleft30">Кликнете върху съответната звезда.</span>
                                </div>
                                <label class="leftClear label">Оценка</label>
                            <asp:RadioButtonList runat="server" ID="rbtnlRating" RepeatLayout="UnorderedList"
                                CssClass="userRating">
                                <asp:ListItem Value="0" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="0.5" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="1.0" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="1.5" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="2.0" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="2.5" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="3.0" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="3.5" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="4.0" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="4.5" Selected="False"></asp:ListItem>
                                <asp:ListItem Value="5.0" Selected="True"></asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </div>
                        <div class="sizex350 margintop10 marginleft30 clearLeft leftClear">
                            <asp:Label CssClass="label" ID="lblBoxReview" runat="server" AssociatedControlID="txtBoxReview"
                                Text="Мнение"></asp:Label>
                            <asp:TextBox runat="server" CssClass="regNotes" ID="txtBoxReview" TextMode="MultiLine" Wrap="true" ToolTip="Вашето мнение за сайта."></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBoxReview" CssClass="validator" ValidationGroup="revform"
                                runat="server" ControlToValidate="txtBoxReview" Display="Dynamic" Text="*" ErrorMessage="Въведете вашето мнение."></asp:RequiredFieldValidator>
                        </div>
                        <div>
                            <asp:ValidationSummary ID="ValidationSummary" runat="server" DisplayMode="List" EnableClientScript="true"
                                ValidationGroup="revform" CssClass="error" />
                        </div>
                        <asp:UpdatePanel ID="UpdatePanelMain" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlMessage" Visible="false" runat="server">
                                    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
                                </asp:Panel>
                                <div class="sizex350 margintop80 marginleft30 clearLeft leftClear">
                                <asp:LinkButton ID="lbtnCancelWriteAReview" CssClass="buttonDeffalt" Text="Отказ"
                                    OnClick="lbtnCancelWriteAReview_Click" runat="server" />
                                <asp:LinkButton runat="server" ID="btnSubmit" CssClass="buttonDeffalt rightFloat" ToolTip="Изпрати"
                                    OnClick="rev_btnSubmit_Click" Text="Изпрати" ValidationGroup="revform" />
                                    </div>
                                <asp:Panel Visible="false" ID="pnlRating" runat="server" CssClass="PopUp">
                                    <div>
                                        <asp:LinkButton Text="" ID="lbtnClose" runat="server" CssClass="close" OnClick="lbtnClose_click"></asp:LinkButton>
                                        <asp:Label ID="lblReviewPostResponse" runat="server" Text="Вашето мнение е изпратено успешно."></asp:Label>
                                        <%--<asp:Label ID="lblReviewText" runat="server" CssClass="copyReview"></asp:Label>--%>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </asp:Panel>

                
            </div>
            <asp:Panel Visible="false" ID="pnlWriteAReviewInfo" runat="server" CssClass="rightFloat margintop50">
                <span class="label ">Вашият Е-mail адрес  няма да бъде побликуван.</span>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
