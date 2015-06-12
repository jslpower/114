<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalRouteView.aspx.cs"
    Inherits="UserBackCenter.LocalAgency.LocalRouteView" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExportPageSet" TagPrefix="cc1" %>
<asp:content id="LocalRouteView" contentplaceholderid="ContentPlaceHolder1" runat="server">
<table width="99%" border="0" cellspacing="0" cellpadding="0" style="border:1px solid #ABC9D9; height:470px;">
            <tr>
              <td align="left" valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:2px;">
                <tr>
                  <td width="50%" height="30" background="<%=ImageServerPath %>/images/tool/searchmenu_right_bj.gif" align="left"> &nbsp;线路名称：
                    <input name="LocalRouteView_RouteName" runat="server" id="LocalRouteView_RouteName" type="text" class="shurukuang" size="18" style="vertical-align:middle; line-height:16px; height:16px;" />
                    天数：
                    <input name="LocalRouteView_TourDays" runat="server" id="LocalRouteView_TourDays" type="text" class="shurukuang" size="2" style="vertical-align:middle; line-height:16px; height:16px;" />
                    联系人：
                    <input name="LocalRouteView_ContactName" runat="server" id="LocalRouteView_ContactName" type="text" class="shurukuang" size="12" style="vertical-align:middle; line-height:16px; height:16px;" /> 
                    </td><td height="30" background="<%=ImageServerPath %>/images/tool/searchmenu_right_bj.gif" align="left" valign="middle">
                    <img src="<%=ImageServerPath %>/images/chaxun.gif" width="62" height="21" style="cursor:pointer;" onclick="LocalRouteView.SearchData();return false;" /></td>
                </tr>
              </table>
              <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:1px;" >
                  <tr>
                    <td width="80%" class="toolbj">
					<a id="LocalAddRoute" href="/localagency/localquickroute.aspx" class="menubarleft" rel="toptab" tabrefresh="false"><img src="<%=ImageServerPath %>/images/tool/add.gif" />新增</a>

					<a id="LocalUpdateRoute" href="/localagency/localquickroute.aspx" class="menubarleft"><img src="<%=ImageServerPath %>/images/tool/modified.gif" />修改</a>
					<a id="LocalCopyRoute" href="/localagency/localquickroute.aspx" class="menubarleft" rel="toptab"><img src="<%=ImageServerPath %>/images/tool/copy.gif" />复制</a>
					<a id="LocalDelRoute" href="/localagency/localrouteview.aspx" class="menubarleft"><img src="<%=ImageServerPath %>/images/tool/dele.gif" />删除</a>					 </td>
                    <td width="20%" class="toolbj" style="display:none;"><a onclick="window.open('zxall.html','title','height=500,width=600,top=100,left=200,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no')" onmouseover="wsug(event, '请选择下面要发送给专线的线路！<br />再点击发送。>>')" onmouseout="wsug(event, 0)" ><img src="<%=ImageServerPath %>/images/sentzx.gif" style="cursor:pointer" /></a></td>
                  </tr>
                </table>
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0"  class="liststyle" id="LocalRouteView_tblRouteLis">
                  <thead>
                    <tr>
                      <th width="24">
                       <input type="checkbox" name="LocalRouteView_chkAll" id="LocalRouteView_chkAll" onclick="LocalRouteView.CheckAll();" />     
                      </th>
                      <th width="201">线路信息</th>
                      <th width="59" align="center">报价标准</th>
                      <th width="155" align="center">门市价(成人/儿童/单房差)</th>
                      <th width="155" align="center">同行价(成人/儿童/单房差)</th>
                      <th width="106">发布单位</th>
                    </tr>
                  </thead>
                  <tbody>
                  <asp:Repeater id="rptLocalRouteView" runat="server" OnItemDataBound="rptLocalRouteView_ItemDataBound">
                  <ItemTemplate>
                    <tr>
                      <td><input type="checkbox" name="LocalRouteView_chkRoute" value='<%# DataBinder.Eval(Container.DataItem,"ReleaseType") %>|<%# DataBinder.Eval(Container.DataItem,"ID") %>' /></td>
                      <td><div class="listtitle"><img src="<%=ImageServerPath %>/images/ico.gif"/><a href="/routeagency/routemanage/routeprint.aspx?RouteID=<%# DataBinder.Eval(Container.DataItem,"ID") %>" target="_blank"><%# DataBinder.Eval(Container.DataItem,"RouteName") %></a></div>
                          <div class="listcompany">天数：<%# DataBinder.Eval(Container.DataItem,"TourDays") %> 发布日期：<%# DataBinder.Eval(Container.DataItem,"IssueTime", "{0:yyyy-MM-dd}") %></div></td>
                      <asp:Literal id="ltrPriceInfo" runat="server"></asp:Literal>
                      <td align="center"><div class="listtitle2"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%></div>
                          <div class="listcompany2"><%# DataBinder.Eval(Container.DataItem, "ContactName")%>
                            <%# DataBinder.Eval(Container.DataItem, "ContactTel")%> <br /><%# EyouSoft.Common.Utils.GetMQ(DataBinder.Eval(Container.DataItem,"ContactMQID").ToString()) %></div></td>
                    </tr>
                  </ItemTemplate>
                  </asp:Repeater> 
                  <asp:Panel id="pnlNodata" runat="server" visible="false">
                    <tr>
                    <td colspan="7" style="height:50px;" align="center">暂无线路数据!&nbsp;&nbsp;<a href="/localagency/localquickroute.aspx" rel="toptab" tabrefresh="false" onclick='topTab.open($(this).attr("href"),"新增线路",{isRefresh:false});return false;' style="color:Red;">点此添加</a></td>
                    </tr>
                    </asp:Panel>
                    </tbody>
                </table>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                <tr>
                    <td bgcolor="#D6EAFE" height="28">
                        <div style="display:none;">
                            <a onclick="ShowDialog();" onmouseover="wsug(event, '请选择下面要发送给专线的线路！<br />再点击发送。>>')"
                                onmouseout="wsug(event, 0)">
                                <img src="<%=ImageServerPath %>/images/sentzx.gif"></a></div>
                    </td>
                </tr>
            </table>
                <table id="ExportPageInfo" cellspacing="0" cellpadding="0" width="98%" align="center" border="0">
                  <tr>
                    <td class="F2Back" align="right" height="40"> 
                     <cc1:ExportPageInfo ID="ExportPageInfo1" LinkType="4" runat="server" /></td>
                  </tr>
                </table></td>
            </tr>
          </table>
          
          <script type="text/javascript" src="<%=EyouSoft.Common.JsManage.GetJsFilePath("MouseFollow") %>" cache="true"></script>
 <script type="text/javascript">
    var LocalRouteView = {
        SearchData: function(){
            var RouteName = $("#<%=LocalRouteView_RouteName.ClientID %>").val();
            var TourDays = $("#<%=LocalRouteView_TourDays.ClientID %>").val();
            var ContactName = $("#<%=LocalRouteView_ContactName.ClientID %>").val();
            var url = "/localagency/localrouteview.aspx?RouteName="+ encodeURI(RouteName) +"&TourDays="+ TourDays +"&ContactName="+ encodeURI(ContactName);
            topTab.url(topTab.activeTabIndex,url);
            return false;
        },
        CheckAll: function(){
            $("input:checkbox").attr("checked",$("#LocalRouteView_chkAll").attr("checked"));
        },
        GetCheck: function(){
            var arr = new Array();
            $("#LocalRouteView_tblRouteLis input:checkbox").not("#LocalRouteView_chkAll").each(function(){
                if($(this).attr("checked"))
                {
                    // checkbox的值是以value|type的形式存储
                    arr.push($(this).val());
                }
            });
            return arr;
        },
        Modify: function(type){
            var arr = LocalRouteView.GetCheck();
            var url = $(this).attr("href");
            var count = arr.length;  
            switch(type){
                case 1:     // 修改
                    if(count == 0)
                        alert('请选择要修改的线路!');
                    else if(count > 1)
                        alert('每次只能修改一条线路!');
                    else{
                        if(arr[0].split('|')[0] == 'Quick')
                            url = "/localagency/localquickroute.aspx?type=edit&RouteId="+ arr[0].split('|')[1];
                        else
                           url = "/localagency/localstandardroute.aspx?type=edit&RouteId="+ arr[0].split('|')[1]; 
                        topTab.open(url,"修改线路",{isRefresh:false});
                    }
                    break;
                case 2:     // 复制
                    if(count == 0)
                        alert('请选择要复制的线路!');
                    else if(count > 1)
                        alert('每次只能复制一条线路!');
                    else{
                        if(arr[0].split('|')[0] == 'Quick')
                            url = "/localagency/localquickroute.aspx?type=copy&RouteId="+ arr[0].split('|')[1];
                        else
                           url = "/localagency/localstandardroute.aspx?type=copy&RouteId="+ arr[0].split('|')[1]; 
                         topTab.open(url,"复制线路",{isRefresh:false});
                    }
                    break;
                case 3:    // 删除
                    if(count == 0)
                        alert('请选择要删除的线路!');
                    else{
                        var routeidlist='';
                        for(var i = 0; i < arr.length; i ++ )
                        {
                            if(arr[i] != null)
                                routeidlist += arr[i].split('|')[1] + ',';
                        }
                        routeidlist = routeidlist.substring(0,routeidlist.length - 1);
                        if(confirm("您确定要删除这些线路吗?\n 此操作不可恢复!"))
                        {
                            $.newAjax({
	                            url:"/localagency/localrouteview.aspx?flag=del&RouteIdList="+ encodeURI(routeidlist) +"&rnd=" + Math.random(),
	                            success:function(state){
	                                if(state == 'True'){
	                                    alert("删除成功!");
						                topTab.url(topTab.activeTabIndex,'/localagency/localrouteview.aspx');
	                                }else{
	                                    //alert("删除失败!");
	                                }
	                            }
	                        });
	                    }	           
                    }
                    break;
            }
        }    
    };
    $(function(){
         $("#LocalAddRoute").click(function(){
            var IsAddGrant = '<%=IsAddGrant %>';
            if(IsAddGrant == 'False')
            {
                alert('对不起，您没有添加地接线路权限!');
            }else{
                topTab.open($(this).attr("href"),"新增线路",{isRefresh:false});
            }
            return false;
         }); 
         $("#LocalUpdateRoute").click(function(){
            var IsUpdateGrant = '<%=IsUpdateGrant %>';
            if(IsUpdateGrant == 'False')
            {
                alert('对不起，您没有修改地接线路权限!');
            }else{
                LocalRouteView.Modify(1);
            }
            return false;
         });
         $("#LocalCopyRoute").click(function(){
            var IsAddGrant = '<%=IsAddGrant %>';
            if(IsAddGrant == 'False')
            {
                alert('对不起，您没有复制地接线路权限!');
            }else{
                LocalRouteView.Modify(2);
            }
            return false;
         });
         $("#LocalDelRoute").click(function(){
            var IsDeleteGrant = '<%=IsDeleteGrant %>';
            if(IsDeleteGrant == 'False')
            {
                alert('对不起，您没有删除地接线路权限!');
            }else{
                LocalRouteView.Modify(3);
            }
            return false;
         });
                        
        //分页控件链接控制
        $("#ExportPageInfo a").each(function(){
            $(this).click(function(){       
                topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                return false;
            });
        });  
        
        $("input[type=text]").bind("keypress",function(e){
            if(document.all)e=event;
            if(e.keyCode == 13)
            {
                LocalRouteView.SearchData();
                return false;
            }
        });
    });
</script>
</asp:content>
