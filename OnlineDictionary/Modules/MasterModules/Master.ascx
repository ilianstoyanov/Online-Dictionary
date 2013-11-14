<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Master.ascx.cs" Inherits="Modules_Master_Master" %>
<%@ Register Assembly="CustomWebControls" Namespace="CustomWebControls" TagPrefix="cwc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:UpdatePanel ID="UpdatePanelSpendTime" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlOption" runat="server">
            <h2>
                <span class="firstLetter">Н</span>астройки</h2>
            <div class="settingsButtons">
                <div class="marginleft30 btnSettings">
                    <asp:ImageButton ID="lbtnUsersSpendTime" ImageUrl="~/App_Themes/Default/images/spendTime.png"
                        Width="128" Height="128" OnClick="lbtnUsersSpendTime_Click" runat="server" />
                    <span class="label">Престой</span>
                </div>
                <div class="marginleft30 btnSettings">
                    <asp:ImageButton ID="lbtnShowAllAdmins" ImageUrl="~/App_Themes/Default/images/admins.png"
                        Width="128" Height="128" OnClick="lbtnShowAllAdmins_Click" runat="server" />
                    <span class="label">Администратори</span>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlShowSpendTime" Visible="false" runat="server">
            <asp:LinkButton ID="lbtnBackFromSpendTime" CssClass="buttonDeffalt" Text="Назад"
                OnClick="lbtnBack_Click" runat="server" />
            <h2>
                <span class="firstLetter">П</span>рестой</h2>
            <div class="margintop30 generalPanel">
                    <div class="marginleft30 marginbottom10 leftFloat">
                        <asp:Label Text="Търси за:" CssClass="label" AssociatedControlID="txtSearch" runat="server" />
                        <asp:TextBox ID="txtSearch" CssClass="sizex250" OnTextChanged="txtSearch_Changed" AutoPostBack="true"
                            runat="server" />
                        <asp:LinkButton ID="lbtnSearch" CssClass="buttonSlim" Text="Търси" OnClick="txtSearch_Changed"
                            runat="server" />
                    </div>
                    <div class="rightFloat">
                        <span class="label">От:</span>
                        <asp:TextBox ID="txtStartDate" runat="server" OnTextChanged="txtSearch_Changed" AutoPostBack="true"
                            Text="Изберете начална дата." alt="Изберете дата." />
                        <ajax:CalendarExtender ID="ajaxCallendarStartDate" TargetControlID="txtStartDate"
                            runat="server" Format="MM/dd/yyyy" />
                        <span class="label">До</span>
                        <asp:TextBox ID="txtEndDate" runat="server" OnTextChanged="txtSearch_Changed" AutoPostBack="true"
                            Text="Изберете крайна дата." alt="Изберете дата." />
                        <ajax:CalendarExtender ID="ajaxCallendarEndDate" TargetControlID="txtEndDate" runat="server"
                            Format="MM/dd/yyyy" />
                    </div>
                <div class="contentTable width650 noLastCell">
                    <asp:GridView ID="GridViewSpendTime" AutoGenerateColumns="false" runat="server">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <span class="leftFloat sizex130 paddingrightx5">Име</span> 
                                    <span class="leftFloat sizex160 paddingrightx5">Фамилия</span> 
                                    <span class="leftFloat sizex130 paddingrightx5">Телефон</span> 
                                    <span class="leftFloat sizex160">Прекарано време</span>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span class="leftFloat sizex130 paddingrightx5"><%# Eval("FirstName") == null ? "-" : Eval("FirstName")%></span> 
                                    <span class="leftFloat sizex160 paddingrightx5"><%# Eval("LastName") == null ? "-" : Eval("LastName")%></span> 
                                    <span class="leftFloat sizex130 paddingrightx5"><%# Eval("Phone") == null ? "-" : Eval("Phone")%></span> 
                                    <span class="leftFloat sizex160 paddingrightx5"><%# Eval("TotalSpendTime") %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlAllAdmins" Visible="false" runat="server">
            <asp:LinkButton ID="lbtnBackFromAdmins" CssClass="buttonDeffalt" Text="Назад" OnClick="lbtnBack_Click" runat="server" />
            <h2><span class="firstLetter">А</span>дминистратори</h2>
            
             <div class="margintop30 generalPanel">
            <div class="contentTable noLastCell ">
                <table>
                    <asp:Repeater ID="rptAdmins" OnItemCommand="rptAdmins_ItemCommand" runat="server">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                    <span class="leftFloat sizex130 paddingrightx5">Име</span> 
                                    <span class="leftFloat sizex160 paddingrightx5">Фамилия</span> 
                                    <span class="leftFloat sizex160 paddingrightx5">E-mail</span> 
                                    <span class="leftFloat sizex130 paddingrightx5">Телефон</span> 
                                    <span class="leftFloat sizex130 paddingrightx5">Регистриран</span> 
                                    <%--<span class="leftFloat sizex160 paddingrightx5">Позиция</span>--%>
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAdminFirstName" CssClass="leftFloat sizex130 paddingrightx5" 
                                    Text='<%# Eval("FirstName")==null?"-":Eval("FirstName") %>' runat="server" />
                                    <asp:Label ID="lblAdminLastName" CssClass="leftFloat sizex160 paddingrightx5" 
                                    Text='<%# Eval("LastName")==null?"-":Eval("LastName") %>' runat="server" />
                                    <span class="leftFloat sizex160 paddingrightx5"><%# Eval("Email")==null?"-":Eval("Email") %></span>
                                    <span class="leftFloat sizex130 paddingrightx5"><%# Eval("Phone")==null?"-":Eval("Phone") %></span>
                                    <span class="leftFloat sizex130 paddingrightx5"><%# Eval("CreateDate","{0:MM/dd/yyyy}") %></span>
                                    <%--<span class="leftFloat sizex130 paddingrightx5"><%# Eval("RoleName")%></span>--%>
                                    <asp:LinkButton ID="lbtnChangeRole" Text="Премахни" CssClass="buttonSlim rightFloat" 
                                    CommandArgument='<%# Eval("currentUserName") %>' CommandName="RemoveFromAdmin" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:LinkButton ID="lbtnShowAddAdminForm" CssClass="buttonDeffalt margintop20 rightFloat" Text="Добавяне на Администратор"
                OnClick="lbtnShowAddAdminForm_Click" runat="server" />
            <asp:Panel ID="pnlAddAdminForm" CssClass="pnlAddAdmin" Visible="false" runat="server">
                <asp:Label ID="Label1" Text="Потребител: " AssociatedControlID="ddlUsers" CssClass="labelDD" runat="server" />
                <cwc:CustomDropDown ID="ddlUsers" CssClass="defaultDD" AppendDataBoundItems="true"
                    runat="server">
                    <asp:ListItem Text="Изберете" Value="0"></asp:ListItem>
                </cwc:CustomDropDown>
                <asp:CompareValidator ID="cvLocation" ValidationGroup="addAdmin" runat="server" ControlToValidate="ddlUsers"
                    ValueToCompare="0" Operator="NotEqual" Text="*" ErrorMessage="Изберете кого искате да добавите като администратор."></asp:CompareValidator>
                <asp:LinkButton ID="lbtnAddAdmin" Text="Добави" CssClass="buttonDeffalt" ValidationGroup="addAdmin"
                        OnClick="lbtnAddAdmin_Click" runat="server" />
                <asp:LinkButton ID="lbtnCancelAddAdmin" CssClass="buttonDeffalt" Text="Отказ" OnClick="lbtnCancelAddAdmin_Click"
                        runat="server" />
                <div>
                    <asp:ValidationSummary ID="vsSelectedUser" runat="server" />
                </div>
            </asp:Panel>
            <div class="marginleft20"><asp:Label CssClass="label" ID="lblMessage" runat="server" /></div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
