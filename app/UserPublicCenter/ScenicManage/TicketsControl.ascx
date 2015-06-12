<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketsControl.ascx.cs" Inherits="UserPublicCenter.ScenicManage.TicketsControl" %>
<asp:Localize ID="lclTjmp" runat="server"></asp:Localize>
    <script type="text/javascript">
        function pucker_show(name, no, hiddenclassname, showclassname) {
            for (var i = 1; i < 10; i++) {
                if ($("#" + name + i) != null) {
                    $("#" + name + i).addClass(hiddenclassname);
                }
            }
            $("#" + name + no).removeClass(hiddenclassname);
            $("#" + name + no).addClass(showclassname);
        }
    </script>