<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewAttrHotelControl.ascx.cs" Inherits="UserPublicCenter.HotelManage.NewAttrHotelControl" %>
                            
 <asp:Localize ID="lclZrjd" runat="server"></asp:Localize>
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