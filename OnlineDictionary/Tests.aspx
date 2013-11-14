<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeFile="Tests.aspx.cs" Inherits="Tests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function CheckTest() {
            var Contain = "";
            
            jQuery(".jsCheckTest").each(function () {
                Contain += $(this).val() + "$";
            });
            jQuery('.jsShowResult').val(Contain);
        };

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PlaceHolderControls" runat="Server">
</asp:Content>
