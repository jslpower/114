<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemoCalendar.ascx.cs"
    Inherits="UserBackCenter.usercontrol.MemoCalendar" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" id="<%=tblID %>">
    <tr>
        <td width="22%" height="22" align="center" bgcolor="#EFF7FF">
            <div id="divCalendar">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="22%" height="22" align="center" bgcolor="#EFF7FF">
                            <strong>日程安排 <a href="/Memorandum/AddMemorandum.aspx" onclick="topTab.open($(this).attr('href'),'添加备忘录',{isRefresh:false,data:{UpPage:0}});return false;"
                                style="font-size: 14px; font-weight: bold;">
                                <img src="<%=ImageServerPath %>/images/arrowpl.gif" width="11" height="9" />添加日程</a></strong>
                        </td>
                        <td width="55%" align="center" bgcolor="#EFF7FF">
                            <img src="<%=ImageServerPath %>/images/dadeleft.gif" width="11" height="12" id="imgPrevMonth"
                                style="cursor: pointer;" /><strong><asp:Label ID="lblVisibleDate" runat="server"></asp:Label></strong><img
                                    src="<%=ImageServerPath %>/images/daderight.gif" style="cursor: pointer;" width="11"
                                    height="12" id="imgNextMonth" />
                        </td>
                        <td width="23%" align="center" bgcolor="#EFF7FF">
                            <a href="/Memorandum/AddMemorandum.aspx" onclick="topTab.open($(this).attr('href'),'添加备忘录',{isRefresh:false,data:{UpPage:0}});return false;"
                                style="font-size: 14px; font-weight: bold;">
                                <img src="<%=ImageServerPath %>/images/arrowpl.gif" width="11" height="9" />添加日程</a>
                        </td>
                    </tr>
                </table>
                <asp:Calendar ID="CalendarDate" runat="server" FirstDayOfWeek="Monday" BorderWidth="1px"
                    BorderColor="#DBE2F2" CssClass="datezt" Width="100%" BackColor="White" Height="200px"
                    CellSpacing="0" CellPadding="0" OnDayRender="CalendarDate_DayRender" UseAccessibleHeader="true">
                    <NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom">
                    </NextPrevStyle>
                    <DayHeaderStyle Font-Size="9pt" Font-Bold="False" BorderWidth="1" BackColor="#F0F5FB"
                        BorderColor="#DBE2F2" Height="10px"></DayHeaderStyle>
                    <DayStyle BorderWidth="1" Height="70" HorizontalAlign="Center" BorderColor="#DBE2F2">
                    </DayStyle>
                    <TitleStyle Font-Size="14pt" BorderWidth="0px" Height="22px" BackColor="#EFF7FF"
                        Width="14%" Font-Bold="True"></TitleStyle>
                </asp:Calendar>
            </div>
        </td>
    </tr>
</table>

<script type="text/javascript">

   $(document).ready(function(){
        commonTourModuleData.add({
            ContainerID:'<%=tblID %>',
            IsBackDefault:'<%=IsBackDefault %>'
        });
        var obj = commonTourModuleData.get('<%=tblID %>');
        $("#"+ obj.ContainerID).find("table[id$=CalendarDate]").find("tr").eq(0).css("display","none");
        $("#"+ obj.ContainerID).find("img[id$=imgPrevMonth]").click(function(){$("#"+ obj.ContainerID).find("a[id$=PrevMonth]").click();});
        $("#"+ obj.ContainerID).find("img[id$=imgNextMonth]").click(function(){$("#"+ obj.ContainerID).find("a[id$=NextMonth]").click();});
   });
      
            
   function ChangeMonth(flag,date,id)
   {
        var obj = commonTourModuleData.get(id);
        $.ajax({
            type:"POST",
            url:"/Memorandum/AjaxChangeMonth.aspx?flag="+ flag +"&date="+ date +"&IsBackDefault="+ obj.IsBackDefault +"&ContainerID="+ obj.ContainerID +"&rnd="+ Math.random(),
            cache:false,
            success:function(html){
                if(html != "")
                {
                    $("#"+ obj.ContainerID).find("div[id$=divCalendar]").html(html);
                }
            }
        });
   }
</script>

