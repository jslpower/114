<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineLibraryList.aspx.cs"
    Inherits="UserBackCenter.TeamService.LineLibraryList" %>

<asp:content id="LineLibraryList" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        commonTourModuleData.add({
            ContainerID: '<%=Key %>',
            ReleaseType: 'LineLibraryList'
        });
    </script>
<div id="<%=Key %>" class="right">
    <div class="tablebox">
    	<%if (showGN)
       { %>
    	 <table border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1">
    	    <tr>
    	      <td align="left" class="title">国内专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	          <asp:Repeater runat="server" id="rpt_gn">
    	              <ItemTemplate>
        	            <a href="javascript:void(0);" travelrangetype="<%#(int)Eval("RouteType") %>"  class="a_line" lineid="<%#Eval("AreaId") %>"><%#EyouSoft.Common.Utils.GetText2( Eval("AreaName").ToString(),6,false)%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	     
    	      </td>
  	      </tr>
  	    </table>
  	    <%} if (showGJ)
       {%>
    	  <div class="hr_10"></div>
    	<table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1" >
    	    <tr>
    	      <td align="left" class="title">国际专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	          <asp:Repeater runat="server" id="rpt_gj">
    	              <ItemTemplate>
        	            <a  href="javascript:void(0);"  class="a_line" travelrangetype="<%#(int)Eval("RouteType") %>" lineid="<%#Eval("AreaId") %>"><%#EyouSoft.Common.Utils.GetText2(Eval("AreaName").ToString(), 6, false)%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	      </td>
  	      </tr>
  	    </table>
  	    <%} if (showZB)
       { %>
    	  <div class="hr_10"></div>
    	<table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;" class="toolbj1">
    	    <tr>
    	      <td align="left" class="title">周边及地接专线：</td>
  	      </tr>
    	    <tr>
    	      <td align="left">
    	         <asp:Repeater runat="server" id="rpt_zb">
    	              <ItemTemplate>
        	            <a href="javascript:void(0);" class="a_line" travelrangetype="<%#(int)Eval("RouteType") %>" lineid="<%#Eval("AreaId") %>"><%#EyouSoft.Common.Utils.GetText2( Eval("AreaName").ToString(),6,false)%></a> 
    	              </ItemTemplate>
    	          </asp:Repeater>
    	      </td>
  	      </tr>
  	    </table>
    	  <div class="hr_5"></div>
    	  <%} %>
        <div id="list_body" style="display:none">
            <table id="tab_goCity" border="0" align="center" cellpadding="0" cellspacing="0" style="width: 100%;
                    background: #fff;" class="toolbj padd5">
                    <tr>
                        <td width="65" height="30" align="left">
                            <strong>出发地点：</strong>
                        </td>
                        <td align="left">
                            <asp:repeater runat="server" id="rpt_goCity">
                                <ItemTemplate>
                                    <a href="javascript:void(0);"  val="<%#Eval("CityId") %>" text="<%#Eval("CityName")%>"><%#Eval("CityName")%></a>
                                </ItemTemplate>
                            </asp:repeater>
                        </td>
                    </tr>
            </table>
        <div class="hr_5"></div>
        <table  border="0" align="center" cellpadding="0" cellspacing="0" style="width:100%;">
           <tr style="background:url(<%=ImgURL%>/images/tool/searchmenu_right_bj.gif) repeat-x center center;">
             <td width="1%" height="30" align="left">&nbsp;</td>
             <td align="left"><span class="search">&nbsp;</span>关键字
               <input type="text" size="20" id="txt_keyWord" runat="server" style="width:60px;" />
               出发地
               <input  type="text" id="txt_goCity" size="20" runat="server" style="width:60px;" />
               出团月份
               <asp:DropDownList runat="server" id="sel_goTime">
               <asp:listItem value="-1">-全部-</asp:listItem>
               </asp:DropDownList>
               <a href="javascript:void(0);" id="a_select"><img src="<%= ImgURL%>/images/chaxun.gif" width="62" height="21" style="margin-bottom:-4px;"/></a></td>
           </tr>
         </table>
        <div id="line_list"></div>
		<%--<table width="100%" border="0" cellpadding="0" cellspacing="0">
          <tr>
            <td height="25" align="left"><strong>线路库和前面的散拼计划有什么不同？</strong></td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>答</strong>：旅游线路如已有确定的出团计划，可以直接散客报名，如果没有出团计划是无法在散拼计划中显示的，而线路库没有这个限制。只要是专线商发布上架的线路，都可以在线路库看到。</td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>什么是单团预定？</strong></td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>答</strong>：如果组团社有单位或团体需要单独组团活动，可以来线路库选择线路，进行单团预定操作。单团操作不限制出团时间，可以自定义出团时间和人员数量。</td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>线路库的线路是按照什么排序的？</strong></td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>答</strong>：线路按照 有无 有效出班计划，推荐VIP商家，更新时间排序；有出班的在前，有VIP的在前，最新更新的在前。</td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>线路行程单和团队行程单有什么不同？</strong></td>
          </tr>
          <tr>
            <td height="25" align="left"><strong>答：</strong>线路行程单包括了该线路最近有效出团时间和当前最低市场价，而团队行程单主要正对确实的某一天出团的内容，包括了出发时间，准确的市场价格等信息。团队行程单请前往散客计划列表页面打印。</td>
          </tr>
        </table>--%>
        </div>
    </div>
</div>
<script type="text/javascript">
    var LineLibraryList = {
        SetTxtGoCity: function(obj) {
            var form = $("#<%=Key %>");
            form.find("#tab_goCity .ff0000").removeClass("ff0000");
            $(obj).addClass("ff0000");
            form.find("#<%=txt_goCity.ClientID %>").val($(obj).text());
            LineLibraryList.GetList(form.find(".select").attr("travelrangetype"));
        },
        GetList: function(lineType/*线路区域类型*/) {
            var form = $("#<%=Key %>");
            form.find('#list_body').show();
            form.find("#line_list").html('<div id="div_load"><img src="<%= ImgURL%>/images/default/tree/loading.gif"/>加载中......</div>')
            //查询参数对象
            var SelectData = {
                lineId: "", //专线Id
                lineType: "", //专线类型
                keyWord: "", //关键字
                goCityId: "", //出发城市Id
                goCity: "", //出发城市
                businessLine: "", //专线商
                goTime: "", //出发月份
                page: "",
                companyID: ""
            }
            var line = form.find(".select");

            //查询条件
            SelectData.companyID = '<%=SiteUserInfo.CompanyID %>';
            if (line.length == 1) {
                SelectData.lineId = line.attr("lineid");
            }
            SelectData.page = '<%=page %>'
            SelectData.lineType = lineType;
            if (SelectData.lineType == null) {
                SelectData.lineType = '<%=type %>';
            }
            SelectData.keyWord = $.trim(form.find("#<%=txt_keyWord.ClientID %>").val());
            SelectData.goCityId = $.trim(form.find("#tab_goCity .ff0000").attr("val"))
            SelectData.goCity = $.trim(form.find("#<%=txt_goCity.ClientID %>").val());
            SelectData.businessLine = form.find("#sel_businessLine").val() == null ? "" : $.trim(form.find("sel_businessLine").val());
            SelectData.goTime = form.find("#<%=sel_goTime.ClientID %>").val();
            //发送ajax请求
            $.newAjax({
                type: "get",
                url: "/TeamService/AjaxLineLibraryList.aspx",
                data: SelectData,
                cache: false,
                dataType: "html",
                success: function(html) {
                    form.find("#line_list").html(html);
                    form.find("#line_list a:not(.a_SingleGroupPre):not([target='_blank'])").click(function() {
                        LineLibraryList.GoUrl(this)
                        return false;
                    })
                    form.find("#line_list .a_SingleGroupPre").click(function() {
                        topTab.open($(this).attr("href"), "组团社-单团预定", {})
                        return false
                    })
                    //列表颜色效果
                    form.find("#line_list tr").hover(
                    function() {
                        if ($(this).attr("class") != "list_basicbg")
                            $(this).addClass("highlight");
                    },
                    function() {
                        if ($(this).attr("class") != "list_basicbg")
                            $(this).removeClass("highlight");
                    })
                    .click(function() {
                        $(this).parent().find("tr").removeClass("selected");
                        $(this).addClass("selected");
                    })
                }, error: function() {
                    alert("获取异常")
                    topTab.url(topTab.activeTabIndex, "/TeamService/LineLibraryList.aspx");
                }
            })
        },
        GoUrl: function(obj) {
            topTab.url(topTab.activeTabIndex, $(obj).attr("href"));
            return false;
        }
    }
    $(function() {
        var form = $("#<%=Key %>");
        form.find(".a_line").click(function() {

            form.find(".select").removeClass("select");

            $(this).addClass("select");
            LineLibraryList.GetList($(this).attr("travelrangetype"));
            return false;
        })
        form.find("#tab_goCity a").click(function() {
            LineLibraryList.SetTxtGoCity(this);
            return false;

        })
        form.find("#a_select").click(function() {
            LineLibraryList.GetList(form.find(".select").attr("travelrangetype"));
        })
        form.find("#<%=txt_goCity.ClientID %>").blur(function() {
            form.find("#tab_goCity a.ff0000").removeClass("ff0000");
            form.find("#tab_goCity a[text='" + $.trim($(this).val()) + "']").addClass("ff0000");
            return false;
        })
        var type = '<%=type %>';
        if (type.length > 0) {
            //form.find(".a_line[travelrangetype='" + type + "']").addClass("select")
            if (type == 3) {
                form.find(".a_line[travelrangetype='" + type + "']").addClass("select");
                form.find(".a_line[travelrangetype='2']").addClass("select")
            }
            else {
                form.find(".a_line[travelrangetype='" + type + "']").addClass("select")
            }
            LineLibraryList.GetList();
        }
        else {
            var lineId = '<%=lineId %>';
            if (lineId.length > 0) {
                form.find(".a_line[lineid='" + lineId + "']").addClass("select");
                LineLibraryList.GetList();
            }
        }

    })   
</script>
</asp:content>
