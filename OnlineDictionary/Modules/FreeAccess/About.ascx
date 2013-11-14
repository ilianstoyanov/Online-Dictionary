<%@ Control Language="C#" AutoEventWireup="true" CodeFile="About.ascx.cs" Inherits="Modules_FreeAccess_About" %>
<asp:UpdatePanel ID="UpdatePanelAbout" ChildrenAsTriggers="true" UpdateMode="Always"
    runat="server">
    <ContentTemplate>
        <h2>
            <span class="firstLetter">З</span>а нас</h2>
            <span class="suMenu">Контакти</span>
       <div class="aboutForm" >
       <span class="contactsTitle">Направи запитване</span>
       <div class="margintop30 marginleft50">
        <div class="regInput margintop30">
            <asp:Label CssClass="label" Text="Име:" AssociatedControlID="txtFirstName" runat="server" />
            <asp:TextBox ID="txtFirstName" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="txtFirstName"
                ErrorMessage="Въведете вашето име." ToolTip="Полето Име трябва да бъде попълнено."
                ValidationGroup="sendEmail" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="regInput">
            <asp:Label CssClass="label" ID="Label1" Text="Фамилия:" AssociatedControlID="txtLastName" runat="server" />
            <asp:TextBox ID="txtLastName" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="*" ControlToValidate="txtLastName"
                ErrorMessage="Въведете вашата фамилия." ToolTip="Полето Фамилия трябва да бъде попълнено."
                ValidationGroup="sendEmail" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="regInput">
            <asp:Label ID="lblEmail"  CssClass="label" Text="E-mail:" AssociatedControlID="txtEmail" runat="server" />
            <asp:TextBox ID="txtEmail" runat="server" />
            <asp:RequiredFieldValidator ID="rfvEmail" Text="*" ControlToValidate="txtEmail" ErrorMessage="Полето Email трябва да бъде попълнено."
                ToolTip="Полето Email трябва да бъде попълнено." ValidationGroup="sendEmail"
                runat="server"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="regEmail" ValidationGroup="sendEmail" ControlToValidate="txtEmail"
                ErrorMessage="Грешен формат." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                runat="server"></asp:RegularExpressionValidator>
        </div>
        <div class="regInput">
            <asp:Label Text="Отностно:" CssClass="label margintop50"  AssociatedControlID="txtSubject" runat="server" />
            <asp:TextBox ID="txtSubject" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="*" ControlToValidate="txtSubject"
                ErrorMessage="Задължително поле." ToolTip="Полето Отностно трябва да бъде попълнено."
                ValidationGroup="sendEmail" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div class="regInput2">
            <asp:Label Text="Съобщение:" CssClass="label margintop10"  AssociatedControlID="txtMessageBody" runat="server" />
            <asp:TextBox ID="txtMessageBody" CssClass="regNotes" TextMode="MultiLine" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Text="*" ControlToValidate="txtMessageBody"
                ErrorMessage="Въведете вашето съобщение." ToolTip="Полето Съобщение трябва да бъде попълнено."
                ValidationGroup="sendEmail" runat="server"></asp:RequiredFieldValidator>
        </div>
        <div>
            <asp:ValidationSummary CssClass="labelError" ValidationGroup="sendEmail" runat="server" />
        </div>
        <div class="margintop30 sizex350">
        <asp:LinkButton ID="lbtnClear" CssClass="buttonDeffalt" ValidationGroup="sendEmail" Text="Изчисти" OnClick="lbtnSendEmail_Click"
            runat="server" />
        <asp:LinkButton ID="lbtnSendEmail" CssClass="buttonDeffalt rightFloat" ValidationGroup="sendEmail" Text="Изпрати" OnClick="lbtnSendEmail_Click"
            runat="server" />
            </div>
            </div>
            </div>
            <div class="contactsForm">
                <span class="contactsTitle ">Контакти</span>
                <div>
                <span class="label leftFloat margintop10 marginleft10">Илиан Стоянов</span>
                </div>
                <div>
                <span class="label leftFloat marginleft10">Телефон: XXXX XXX-XXX</span>
                </div>
                <div>
                <span class="label leftFloat marginleft10">E-mail: stoianov.ilian@gmail.com</span>
                </div>
                
            </div>
            <%--<asp:HiddenField ID="hfCurrentPage" runat="server"/>--%>
            <div class="contactStatus">
                <asp:Label ID="lblError" CssClass="label" runat="server" />
            </div>
    </ContentTemplate>
</asp:UpdatePanel>
