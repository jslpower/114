<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScatteredfightList.aspx.cs"
    Inherits="SiteOperationsCenter.LineManage.ScatteredfightList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>散拼计划管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <script language="JavaScript">
 
  function mouseovertr(o) {
	  o.style.backgroundColor="#FFF9E7";
      //o.style.cursor="hand";
  }
  function mouseouttr(o) {
	  o.style.backgroundColor=""
  }

    </script>

    <script type="text/javascript">

//鼠标跟随代码//
function wsug(e, str){ 
	var oThis = arguments.callee;
	if(!str) {
		oThis.sug.style.visibility = 'hidden';
		document.onmousemove = null;
		return;
	}		
	if(!oThis.sug){
		var div = document.createElement('div'), css = 'top:0; left:-30px;text-align:left;color:#2C709F;position:absolute; z-index:100; visibility:hidden';
			div.style.cssText = css;
			div.setAttribute('style',css);
		var sug = document.createElement('div'), css= 'font:normal 12px/16px "宋体"; white-space:nowrap; color:#666; padding:3px; position:absolute; left:-30px; top:0; z-index:10; background:#f9fdfd; border:1px solid #629BC7;text-align:left;color:#2C709F;';
			sug.style.cssText = css;
			sug.setAttribute('style',css);
		var dr = document.createElement('div'), css = 'position:absolute; top:3px; left:-27px; background:#333; filter:alpha(opacity=30); opacity:0.3; z-index:9';
			dr.style.cssText = css;
			dr.setAttribute('style',css);
		var ifr = document.createElement('iframe'), css='position:absolute; left:0; top:-10; z-index:8; filter:alpha(opacity=0); opacity:0';
			ifr.style.cssText = css;
			ifr.setAttribute('style',css);
		div.appendChild(ifr);
		div.appendChild(dr);
		div.appendChild(sug);
		div.sug = sug;
		document.body.appendChild(div);
		oThis.sug = div;
		oThis.dr = dr;
		oThis.ifr = ifr;
		div = dr = ifr = sug = null;
	}
	var e = e || window.event, obj = oThis.sug, dr = oThis.dr, ifr = oThis.ifr;
	obj.sug.innerHTML = str;
	
	var w = obj.sug.offsetWidth, h = obj.sug.offsetHeight, dw = document.documentElement.clientWidth||document.body.clientWidth; dh = document.documentElement.clientHeight || document.body.clientHeight;
	var st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
	var left = e.clientX +sl +17 + w < dw + sl && e.clientX + sl + 15 || e.clientX +sl-8 - w, top = e.clientY + st + 17;
	obj.style.left = left+ 10 + 'px';
	obj.style.top = top + 10 + 'px';
	dr.style.width = w + 'px';
	dr.style.height = h + 'px';
	ifr.style.width = w + 3 + 'px';
	ifr.style.height = h + 3 + 'px';
	obj.style.visibility = 'visible';
	document.onmousemove = function(e){
		var e = e || window.event, st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
		var left = e.clientX +sl +17 + w < dw + sl && e.clientX + sl + 15 || e.clientX +sl-8 - w, top = e.clientY + st +17 + h < dh + st && e.clientY + st + 17 || e.clientY + st - 5 - h;
		obj.style.left = left + 'px';
		obj.style.top = top + 'px';
	}
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="hr_10">
    </div>
    <!---国内游------->
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 99%;"
        class="toolbj1">
        <tr>
            <td align="left" class="title">
                国内长线：
            </td>
        </tr>
        <tr>
            <td align="left">
                <%=Bindstrgc()%>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <!----国际游------->
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 99%;"
        class="toolbj1">
        <tr>
            <td align="left" class="title">
                国际专线：
            </td>
        </tr>
        <tr>
            <td align="left">
                <%=Bindstrgj()%>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <!---周边游------->
    <table border="0" align="center" cellpadding="0" cellspacing="0" style="width: 99%;"
        class="toolbj1">
        <tr>
            <td align="left" class="title">
                国内短线：
            </td>
        </tr>
        <tr>
            <td align="left">
                <%=Bindstrgd() %>
            </td>
        </tr>
    </table>
    <div class="hr_10">
    </div>
    <!---常用------->
    <!--<table width="98%" border="0" align="center" class="table_basic">
  <tr>
    <td><strong>常用</strong>：<a href="#">苏州乐园</a> <a href="#">常州恐龙园</a> <a href="#">上海欢乐谷</a></td>
  </tr>
</table>
-->
    <!---主题分类------->
    <table width="98%" border="0" align="center" class="table_basic" >
        <tr>
            <td>
                <strong>主题分类</strong>：<%=BindThemId()%>
            </td>
        </tr>
    </table>
    <!---杭州周边城市------->
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td nowrap="nowrap" style="background: #dce7f6; line-height: 26px; padding-left: 15px;
                padding-top: 3px; padding-bottom: 3px;">
                关键
                <input id="txt_SearchKeyword" name="textfield" runat="server" style="color: #999"
                    type="text" value="团号、线路名称" size="20" onfocus="$(this).css('color', 'black');if(this.value == '团号、线路名称') {this.value = '';}"
                    onblur="if (this.value == '') {$(this).css('color', '#999');this.value = '团号、线路名称';}" />
                出发地
                <input id="txt_Departure" name="txt_Departure" runat="server" size="8" />
                专线类型
                <asp:DropDownList ID="DropSearchLine" runat="server" onchange="OnchangeWord(this.value,'GetLineByType')">
                </asp:DropDownList>
                专线
                <asp:DropDownList ID="DropSearchLineId" runat="server" onchange="OnchangeWord(this.value,'GetCompanyByLine')">
                    <asp:ListItem Value="0">专线</asp:ListItem>
                </asp:DropDownList>
                专线商：
                <asp:DropDownList ID="dropBusinessLineId" runat="server">
                    <asp:ListItem Value="0">专线商</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" style="background: #dce7f6; line-height: 26px; padding-left: 15px;
                padding-top: 3px; padding-bottom: 3px;">
                出团日期
                <input type="text" id="txtStartDate" name="txtStartDate" class="size55" style="width: 85px;"
                    onfocus="WdatePicker();" />至<input type="text" id="txtEndDate" name="txtEndDate" style="width: 85px;"
                        class="size55" onfocus="WdatePicker();" />
                成人市场价范围
                <input name="txt_PriceStart" id="txt_PriceStart" size="8" realvalue="realValue" runat="server" />
                至
                <input name="txt_PriceEnd" id="txt_PriceEnd" size="8" realvalue="realValue" runat="server" />
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxun.gif" width="62" height="21"
                    style="vertical-align: top;" onclick="ScatteredfightManage.OnSearch()" />
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="search_bg">
                专线
                <asp:DropDownList ID="dropLine" runat="server" onchange="OnchangeWordAdd(this.value,'GetLineByType')">
                </asp:DropDownList>
                <asp:DropDownList ID="dropLine1" runat="server">
                </asp:DropDownList>
                <a href="javascript:void(0)" onclick="UrlAddLine();return false;" class="add_btn">添 加</a> &nbsp;
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td width="99%" align="left" class="zhtai_bg">
                <span class="guestmenu">收客状态</span>
                <%=GetTourMarkerNote()%>
                <span class="guestmenu">团队类型</span>
                <%=GetTeamType()%>
            </td>
        </tr>
    </table>
    <div id="divScatteredfightList" align="center">
    </div>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="30" align="right">
            </td>
        </tr>
    </table>
   
    <%--菜单快捷操作--%>
    <div style="display: none; width: 120px; border: 1px solid gray; position: absolute;
        background-color: White;" id="divShortcutMenu">
        <input id="hid_TourID" type="hidden" />
        <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;
            border-bottom: 1px solid gray;">
            <tbody>
                <tr class="usedea">
                    <td align="left" class="chekboxtitle">
                        操作快捷方式
                    </td>
                    <td align="left">
                        <img onclick="ScatteredfightManage.menuClose(this)" style="" src="<%=ImageServerUrl %>/Images/tool/close.gif" />
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;
            border-bottom: 1px solid gray;">
            <tbody>
                <tr>
                    <td align="left" class="usedea">
                        <span id="divUpdate"><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.tourUpdate(); return false;">
                            修改</a></span> <span id="divDelete"><a href="javascript:void(0)" onclick="NotStartingTeamsDetail.tourDelete(); return false;">
                                删除</a></span>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;
            border-bottom: 1px solid gray;" id="TourMarket">
            <tbody>
                <tr>
                    <td align="left" class="chekboxtitle">
                        团队类型：
                    </td>
                    <td>
                        <a style="color: rgb(0, 0, 0);" onclick="NotStartingTeamsDetail.saveTourMarkerNote(''); return false;"
                            href="javascript:void(0)">
                            <nobr>取消</nobr>
                        </a>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2" class="usedea">
                        <a onclick="ScatteredfightManage.setTourMarkerNote('1'); return false;" href="javascript:void(0)"
                            id="1381"><span style="white-space: nowrap;">推荐</span></a> <a onclick="ScatteredfightManage.setTourMarkerNote('2'); return false;"
                                href="javascript:void(0)" id="1382"><span style="white-space: nowrap;">促销</span></a>
                        <a onclick="ScatteredfightManage.setTourMarkerNote('3'); return false;" href="javascript:void(0)"
                            id="1383"><span style="white-space: nowrap;">最新</span></a> <a onclick="ScatteredfightManage.setTourMarkerNote('4'); return false;"
                                href="javascript:void(0)" id="1384"><span style="white-space: nowrap;">品质</span></a>
                        <a onclick="ScatteredfightManage.setTourMarkerNote('5'); return false;" href="javascript:void(0)"
                            id="1385"><span style="white-space: nowrap;">纯玩</span></a>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="92%" cellspacing="0" cellpadding="0" border="0" align="center" style="margin-top: 3px;
            border-bottom: 1px solid gray;">
            <tbody>
                <tr>
                    <td align="left" class="chekboxtitle">
                        收客状态：
                    </td>
                </tr>
                <tr>
                    <td align="left" class="usedea">
                        <a onclick="ScatteredfightList.changeState(1)" href="javascript:void(0)" id="2">客满</a>
                        <a onclick="ScatteredfightList.changeState(2)" href="javascript:void(0)" id="0">停收</a>
                        <a onclick="ScatteredfightList.changeState(3)" href="javascript:void(0)">正常</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    </form>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript">
    
    function UrlAddLine()
    {
        var dropline=$("#<%=dropLine.ClientID %>").val();
        var dropline1=$("#<%=dropLine1.ClientID %>").val();
        window.location="AddLine.aspx?dropLineType="+dropline+"&dropLine="+dropline1+"";
    }
    
    </script>

    <script type="text/javascript">

        var type=null;
        //获取选择
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[name='ckbScatteredfight']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id += $(this).val()+"$";
                }
            });
            if (num == 0) {
                return 0;
            }
            else {
                return id;
            }
        }
    
        
    
        //Line1为专线（国内，国外，周边） line2为（所选专线区域） BusinessLine专线商 Departure为出发地
        var Parms = { SearchKeyword: "", Line1: 0,Line2:0,BusinessLine: 0,Departure:"",StartDate:"",EndDate:"",PriceStart:"",PriceEnd:"",Themeid:0,Page: 1 };
        var ScatteredfightManage = {
            IsGrantUpdate:"<%=IsGrantUpdate %>",
            IsGrantDelete:"<%=IsGrantDelete %>",
            //景区列表
            GetScatteredfightList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divScatteredfightList");
                if (LoadingImg.IsLoadAddDataToDiv("divScatteredfightList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxScatteredfightList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divScatteredfightList").html(html);
                        }
                    });
                }
            },

           ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            
            
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetScatteredfightList();
            },
            into:function(){
                if($("#<%=txt_SearchKeyword.ClientID%>").val()!="团号、线路名称"&&$("#<%=txt_SearchKeyword.ClientID%>").val()!="")
                    Parms.SearchKeyword = $("#<%=txt_SearchKeyword.ClientID%>").val();
                else
                    Parms.SearchKeyword="";
                Parms.Line1 = $("#<%=DropSearchLine.ClientID%>").val();
                Parms.Line2=$("#<%=DropSearchLineId.ClientID%>").val();
                Parms.BusinessLine=$("#<%=dropBusinessLineId.ClientID%>").val();
                Parms.Departure = $.trim($("#txt_Departure").val());
                Parms.StartDate = $.trim($("#txtStartDate").val());
                Parms.EndDate = $.trim($("#txtEndDate").val());
                Parms.PriceStart = $.trim($("#txt_PriceStart").val());
                Parms.PriceEnd = $.trim($("#txt_PriceEnd").val());
                Parms.Page = 1;
            },
            OnSearch: function() {//查询
                ScatteredfightManage.into();
                ScatteredfightManage.GetScatteredfightList();
            },
            OnSearchByAreaId:function(obj,aid){//根据专线筛选
                $("#form1 a.classLine").each(function(){
                    if($(this).css("color")=="red")
                    {
                        $(this).css("color","");
                    }                
                })
                $(obj).css("color","red");
                Parms.Line2=aid;
                ScatteredfightManage.GetScatteredfightList();
            },
            OnSearchByThemeid:function(obj,tid){//根据主题筛选
                $(obj).closest("td").find("a").each(function(){
                    if($(this).css("color")=="red")
                    {
                        $(this).css("color","");
                    }                
                })
                $(obj).css("color","red");
                Parms.Themeid=tid;
                ScatteredfightManage.OnSearch();
            },
            
            getPosition:function(obj) 
            {
                var objPosition={Top:0,Left:0}
                var offset = $(obj).offset();
                objPosition.Left=offset.left;
                objPosition.Top=offset.top+$(obj).height();
                return objPosition;
            } ,
            menuClose:function(){
                $("#divShortcutMenu").hide()
            },
        
        
            changeState: function(t){
                if('<%=IsGrantUpdate %>'!="True"){
                  alert("对不起，你目前的帐号没有该权限！");
                  return;
                }
                var selecedchk=IsSelectAdv();
                if(selecedchk==0)
                {
                     alert("请选择一项");
                     return false;
                }
                var thispage=$("#hidAjaxNotStartingTeamsPage").val();
                $.ajax({
                      cache:false,
                      url:"/LineManage/ScatteredfightList.aspx?action=changeState&rd="+Math.random(),
                      data:{TourID:selecedchk,TourState:t,Page:thispage},
                      success:function(html){
                          if(html=="1"){
                              Parms.Page=thispage;
                              ScatteredfightManage.OnSearch();
                              alert("操作成功！")
                          }else{
                              alert("操作失败！")
                          }
                      }
                  })
                return false;
            },
            
            BatchUpdate: function(){
                if('<%=IsGrantUpdate %>'!="True"){
                  alert("对不起，你目前的帐号没有该权限！");
                  return;
                }
                var selecedchk=IsSelectAdv();
                if(selecedchk==0)
                {
                     alert("请选择一项");
                     return false;
                }
                var thispage=$("#hidAjaxNotStartingTeamsPage").val();
                $.ajax({
                      cache:false,
                      url:"/LineManage/ScatteredfightList.aspx?action=BatchUpdate&rd="+Math.random(),
                      data:{TourID:selecedchk,TourState:t,Page:thispage},
                      success:function(html){
                          if(html=="1"){
                              Parms.Page=thispage;
                              ScatteredfightManage.OnSearch();
                              alert("操作成功！")
                          }else{
                              alert("操作失败！")
                          }
                      }
                  })
                return false;
            },
        

           saveTourMarkerNote: function(type){
            
                if('<%=IsGrantUpdate %>'!="True"){
                  alert("对不起，你目前的帐号没有该权限！");
                  return;
                }
                var selecedchk=IsSelectAdv();
                if(selecedchk==0)
                {
                     alert("请选择一项");
                     return false;
                }
                var thispage=$("#hidAjaxNotStartingTeamsPage").val();
                $.ajax({
                      cache:false,
                      url:"/LineManage/ScatteredfightList.aspx?action=setTourMarkerNote&rd="+Math.random(),
                      data:{TemplateTourID:selecedchk,TourMarkerNote:type,Page:thispage},
                      success:function(html){
                          if(html=="1"){
                              Parms.Page=thispage;
                              ScatteredfightManage.OnSearch();
                              alert("操作成功！")
                          }else{
                              alert("操作失败！")
                          }
                      }
                  })
                return false;
            },
                
        
        
            setTourMarkerNote:function(typeLine){//线路类型设置
            type=typeLine;
            var selecedchk=IsSelectAdv();
            if(selecedchk==0)
            {
                 alert("请选择一项");
                 return false;
            }
            else
            {
                 if('<%=IsGrantUpdate %>'!="True"){
                     alert("对不起，你目前的帐号没有修改权限！");
                     return;
                 }
                 if(type!="0"){
                    Boxy.iframeDialog({title:'团队推广说明', iframeUrl:'/LineManage/tourdescription.aspx',width:315,height:150,draggable:true,data:{callBack:'ScatteredfightManage.saveTourMarkerNote'}});
                 }else{
                    this.saveTourMarkerNote('');//取消设置
                 }
                 return false;
             }
        }
     }
     
     
     
        $(document).ready(function() {
            var FormObj = $("#<%=form1.ClientID%>");

            //回车查询
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    ScatteredfightManage.OnSearch();
                    return false;
                }
            });
            ScatteredfightManage.OnSearch();
            
            
            $("#tbl_ScatteredfightList input[name='ckbScatteredfight']").each(function(){
            $(this).click(function(){
                if(this.checked){
                    var pos= ScatteredfightManage.getPosition(this)
                    $("#divShortcutMenu").show().css({left:Number( pos.Left+23)+"px",top:pos.Top+"px",'z-index':10});
                }else{$("#divShortcutMenu").hide()}
            })
         });   
    });        
    
    </script>

    <script type="text/javascript">
    
     //专线类型（国内国际周边等等）
        function OnchangeWord(v,t)
        {
             $.ajax({
                 url: "ScatteredfightList.aspx?action="+t+"&argument="+v,
                 cache: false,
                 type:"POST",
                 dataType:"json",
                 success: function(result) {
                     if(t=="GetLineByType"){//根据专线类型获取专线
                         $("#<%=DropSearchLineId.ClientID %>").html("");
                         $("#<%=DropSearchLineId.ClientID %>").append("<option value=\"0\">专线</option>"); 
                         var list = result.tolist;               
                         for(var i=0;i<list.length;i++)
                         {
                             $("#<%=DropSearchLineId.ClientID %>").append("<option value=\""+list[i].AreaId+"\">"+list[i].AreaName+"</option>"); 
                         }
                     }
                     else if(t=="GetCompanyByLine")//根据专线获取专线商
                     {
                         $("#<%=dropBusinessLineId.ClientID %>").html("");
                         $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\"0\">专线商</option>"); 
                         var list1 = result.tolist;
                         for(var j=0;j<list1.length;j++)
                         {
                             $("#<%=dropBusinessLineId.ClientID %>").append("<option value=\""+list1[j].ID+"\">"+list1[j].CompanyName+"</option>"); 
                         }
                     }                    
                 },
                 error: function() {
                     alert("操作失败!");
                 }    
             });
        }
        
        function OnchangeWordAdd(v,t)
        {
             if(v=="-1")
             {
                $("#<%=dropLine1.ClientID %>").html("");
                $("#<%=dropLine1.ClientID %>").append("<option value=\"0\">请选择</option>"); 
                return false;
             }
             $.ajax({
                 url: "ScatteredfightList.aspx?action="+t+"&argument="+v,
                 cache: false,
                 type:"POST",
                 dataType:"json",
                 success: function(result) {
                     if(t=="GetLineByType"){//根据专线类型获取专线
                         $("#<%=dropLine1.ClientID %>").html("");
                         $("#<%=dropLine1.ClientID %>").append("<option value=\"0\">请选择</option>"); 
                         var list = result.tolist;               
                         for(var i=0;i<list.length;i++)
                         {
                             $("#<%=dropLine1.ClientID %>").append("<option value=\""+list[i].AreaId+"\">"+list[i].AreaName+"</option>"); 
                         }
                     }                
                 },
                 error: function() {
                     alert("操作失败!");
                 }    
             });
        }
        
    </script>

</body>
</html>
