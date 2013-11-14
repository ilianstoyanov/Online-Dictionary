<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Tests.ascx.cs" Inherits="Modules_RestrictedAccess_Tests" %>
<asp:UpdatePanel ID="UpdatePanelTests" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlGenerelTests" runat="server">
            <h2>
                <span class="firstLetter">Т</span>естове</h2>
            <span class="suMenu">Изберете тест</span>
            <div class="contentTable width650 noLastCell">
                <table>
                    <asp:Repeater ID="rptTests" OnItemCommand="rptTests_ItemCommand" runat="server">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    <span class="leftFloat sizex350 paddingrightx5">Име</span> <span class="leftFloat sizex160 paddingrightx5">
                                        Трудност</span>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label CssClass="leftFloat sizex350 paddingrightx5" Text='<%# Eval("name") %>'
                                        runat="server" />
                                    <asp:Label CssClass="leftFloat sizex160 paddingrightx5" Text='<%# Eval("level") %>'
                                        runat="server" />
                                    <asp:LinkButton ID="lbtnStartTest" Text="Започни" CssClass="buttonSlim" CommandArgument='<%# Eval("name") %>'
                                        CommandName="Start" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlShowTest" CssClass="test" Visible="false" runat="server">
            <div class="lesson">
                <span class="label">Урок:</span>
                <asp:Label CssClass="label" ID="lblTitle" runat="server" />
            </div>
            <div class="level">
                <asp:Label CssClass="label rightFloat" ID="lblLevel" runat="server" />
                <span class="label rightFloat">Ниво: </span>
            </div>
            <div class="lessonDescription label marginleft0">
                <asp:Label ID="lblDescription" CssClass="margintop30 subTitle " runat="server" />
            </div>
            <div class="lessonExample margintop80">
                <asp:Label CssClass="label  leftFloat" ID="lblExample" runat="server" />
            </div>
            <div>
            <asp:Panel CssClass="jsAnswerOfQuestion questions questionInput" ID="pnlQuestions" ClientIDMode="Static" runat="server">
            </asp:Panel>
            <div class="margintop30 rightFloat">
                <asp:LinkButton ID="lbtnBack" CssClass="buttonDeffalt margintop20" Text="Назад" OnClick="lbtnBack_Click"
                    runat="server" />
                <asp:LinkButton ID="lbtnCheck" Text="Провери" CssClass="buttonDeffalt margintop20" OnClick="lbtnCheck_Click"
                    OnClientClick="CheckTest();" runat="server" />
            </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlShowResult" CssClass="PopUp2" Visible="false" runat="server">
            <asp:Panel runat="server">
                <asp:Label ID="lblTestResult" CssClass="label" runat="server"></asp:Label>
                <div>
                    <asp:LinkButton ID="lbtnCloseResult" Text="Затвори" CssClass="buttonSlim margintop10"
                        OnClick="lbtnCloseResult_Click" runat="server" />
                    <asp:LinkButton ID="lbtnShowError" CssClass="buttonSlim margintop10 rightFloat" Text="Грешки"
                        OnClick="lbtnShowError_Click" runat="server" />
                </div>
                <asp:Panel ID="pnlErrorAnswers" Visible="false" CssClass="margintopp30" runat="server">
                    <asp:Label ID="lblErrorAnswers" runat="server" />
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <asp:TextBox runat="server" ID="txtAnswers" Style="display: none;" CssClass="jsShowResult" />
    </ContentTemplate>
</asp:UpdatePanel>
