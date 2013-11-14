<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AllUsers.ascx.cs" Inherits="Modules_AdminModules_AllUsers" %>
<asp:UpdatePanel ID="UpdatePanleExistingUsers" ChildrenAsTriggers="true" runat="server">
    <ContentTemplate>
        <h2 class="subtitlePostion ">
            <span class="firstLetter">П</span>отребители</h2>
        <div class="margintop30">
            <div>
                <div class="search leftFloat marginleft30">
                    <div class="leftFloat">
                    <asp:Label CssClass="label marginleft30" Text="Търси за:" AssociatedControlID="txtSearch"
                        runat="server" />
                    <asp:TextBox ID="txtSearch" CssClass="sizex250" OnTextChanged="txtSearch_Click" AutoPostBack="true"
                        runat="server" ToolTip="Търсене по Име, Фамилия, Телефон, Email, или Описание."
                        alt="Search"></asp:TextBox>
                    <asp:LinkButton ID="lbtnSearch" OnClick="txtSearch_Click" CssClass="buttonSlim" Text="Търсене"
                        runat="server"></asp:LinkButton>
                </div>
                <asp:Label ID="lblTotalUsers" CssClass="label leftFloat marginbottom10 marginleft50" runat="server" />
                <div class="customPaging rightFloat marginright10">
                    <asp:LinkButton ID="lbtnPrevious" CssClass="left" OnClick="lbtnPrevious_Click" runat="server" />
                    <asp:TextBox ID="txtCurrentPage" OnTextChanged="txtCurrentPage_TextChanged" AutoPostBack="true"
                        runat="server" />
                    <asp:Label ID="lblOf" Text=" of " runat="server" />
                    <asp:Label ID="lblTotalItems" runat="server" />
                    <asp:LinkButton ID="lbtnNext" CssClass="right" OnClick="lbtnNext_Click" runat="server" />
                </div>
            </div>
       
        <div class="contentTable margintop20 noLastCell">
            <asp:GridView ID="GridViewExistingUsers" OnRowDataBound="GridViewExistingUsers_RowDataBound"
                OnRowCommand="GridViewExistingUsers_ItemCommand" AutoGenerateColumns="false"
                runat="server">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:LinkButton ID="lbtnFirstName" CssClass="leftFloat sizex100 paddingrightx5" Text="Име"
                                CommandName="Sorting" CommandArgument="FirstName" runat="server" />
                            <asp:LinkButton ID="lbtnLastName" CssClass="leftFloat sizex100 paddingrightx5" Text="Фамилия"
                                CommandName="Sorting" CommandArgument="LastName" runat="server" />
                            <asp:LinkButton ID="lbtnEmail" CssClass="leftFloat sizex250 paddingrightx5" Text="E-mail"
                                CommandName="Sorting" CommandArgument="Email" runat="server" />
                            <asp:LinkButton ID="lbtnPhone" CssClass="leftFloat sizex130 paddingrightx5" Text="Телефон"
                                CommandName="Sorting" CommandArgument="Phone" runat="server" />
                            <asp:LinkButton ID="lbtnCreateDate" CssClass="leftFloat sizex100 paddingrightx5"
                                Text="Регистриран" CommandName="Sorting" CommandArgument="CreateDate" runat="server" />
                            <%--<asp:LinkButton ID="lbtnNotes" Text="Бележки" CommandName="Sorting" CommandArgument="Notes" runat="server" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <span class="leftFloat sizex100 paddingrightx5">
                                <%# Eval("FirstName")==null?"-":(Eval("FirstName")==string.Empty?"-":Eval("FirstName")) %></span>
                            <span class="leftFloat sizex100 paddingrightx5">
                                <%# Eval("LastName")==null?"-":(Eval("LastName")==string.Empty?"-":Eval("LastName")) %></span>
                            <span class="leftFloat sizex250 paddingrightx5">
                                <%# Eval("Email")==null?"-":(Eval("Email")==string.Empty?"-":Eval("Email")) %></span>
                            <span class="leftFloat sizex130 paddingrightx5">
                                <%# Eval("Phone")==null?"-":(Eval("Phone")==string.Empty?"-":Eval("Phone")) %></span>
                            <span class="leftFloat sizex100 textCenter paddingrightx5">
                                <%# Eval("CreateDate","{0:MM/dd/yyyy}") %></span> 
                                <%--<span class="leftFloat sizex160 paddingrightx5"><%# Eval("Notes")==null?"-":(Eval("Notes")==string.Empty?"-":Eval("Notes")) %></span>--%>
                            <asp:LinkButton ID="lbtnBlockUser" CssClass="buttonSlim rightFloat" Text="Блокирай"
                                CommandArgument='<%# Eval("ASPNETID") %>' CommandName="BlockUser" runat="server"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnUnBlockUser" CssClass="buttonSlim rightFloat" Text="Разблокирай"
                                CommandArgument='<%# Eval("ASPNETID") %>' CommandName="UnBlockUser" runat="server"></asp:LinkButton>
                            <asp:HiddenField ID="hdnUserASPNETID" Value='<%# Eval("ASPNETID") %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="customPaging rightFloat margintop10">
            <asp:LinkButton CssClass="left" ID="lbtnPreviousBottom" OnClick="lbtnPrevious_Click"
                runat="server" />
            <asp:TextBox ID="txtCurrentPageBottom" OnTextChanged="txtCurrentPage_TextChanged"
                AutoPostBack="true" runat="server" />
            <asp:Label ID="lblOfBottom" Text=" of " runat="server" />
            <asp:Label ID="lblTotalItemsBottom" runat="server" />
            <asp:LinkButton CssClass="right" ID="lbtnNextBottom" OnClick="lbtnNext_Click" runat="server" />
        </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
