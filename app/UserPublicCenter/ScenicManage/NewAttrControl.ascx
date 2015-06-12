<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAttrControl.ascx.cs" Inherits="UserPublicCenter.ScenicManage.NewAttrControl" %>
    <asp:Localize ID="lclZrjd" runat="server"></asp:Localize>
          <script type="text/javascript">
        function pucker_show(name, no, hiddenclassname, showclassname,num) {
            for (var i = 1; i <= num; i++) {
                if ($("#" + name + i) != null) {
                    $("#" + name + i).addClass(hiddenclassname);
                }
            }
            $("#" + name + no).removeClass(hiddenclassname);
            $("#" + name + no).addClass(showclassname);
        }
    </script>
