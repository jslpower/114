<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamAdd.aspx.cs" Inherits="SiteOperationsCenter.LineManage.TeamAdd" %>

<%@ Import Namespace="EyouSoft.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>线路管理-团队计划-添加</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("main") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>

    <script type="text/javascript" src="/DatePicker/WdatePicker.js"></script>

    <style type="text/css">
        #div_DateTime table
        {
            border: solid 1px #addaf8;
            border-collapse: collapse;
            margin-bottom: 5px;
        }
        #div_DateTime td
        {
            border: solid 1px #addaf8;
            border-collapse: collapse;
            height: 30px;
        }
        #div_DateTime th
        {
            background-color: #d9eefc;
            border: solid 1px #addaf8;
            border-collapse: collapse;
            height: 30px;
        }
        .weektitle
        {
            background-color: #f0dc82;
        }
        #divMonthPreNext
        {
            width: 100%;
            height: 30px;
            border: solid 1px #addaf8;
            background-color: #d9eefc;
            margin: 2px 0px;
            line-height: 30px;
        }
        #linkPreMonth
        {
            float: left;
            margin-left: 15px;
        }
        #linkNextMonth
        {
            float: right;
            margin-right: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="HidCalender" Value='2012-3-1|2011-3-2|2011-3-3'>
    </asp:HiddenField>
    <asp:HiddenField runat="server" ID="hidChildLeaveDateList" Value='2012-3-1|2012-3-2|2012-3-3'>
    </asp:HiddenField>
    <asp:HiddenField runat="server" ID="hidChildTourCodeList" Value='2010-1-1|2010-1-2|2010-1-3'>
    </asp:HiddenField>
    <input type="hidden" id="hd_routeid" runat="server" />
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="caozuo_bg">
                <a href="#">泰新马新经典</a>
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left" class="search_bg">
                <span class="search">&nbsp;</span>团号：
                <input name="TeamLineId" id="TeamLineId" runat="server" size="15" />
                出团日期：
                <input type="text" id="txtStartDate" name="StartDate" runat="server" class="size55"
                    style="width: 85px;" onfocus="WdatePicker();" />至<input type="text" id="txtEndDate"
                        name="EndDate" runat="server" style="width: 85px;" class="size55" onfocus="WdatePicker();" />
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/chaxun.gif" width="62" height="21"
                    onclick="TeamplanManage.OnSearch()" />
            </td>
        </tr>
    </table>
    <div id="divTeamLineList" align="center">
    </div>
    <div class="hr_10">
    </div>
    <table id="tab_Save" border="0" cellspacing="0" cellpadding="4" style="width: 99%;
        margin-top: 10px;">
        <tr>
            <td align="left" class="ff0000">
                ·团队行程其它信息（出团集合时间，领队联系方式），可单独修改，一般在出团前，专线商单独修改用于打印出团单
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="88%" border="1" cellpadding="2" cellspacing="0" bordercolor="#9DC4DC">
                    <tr bgcolor="#C8E6F7">
                        <td colspan="2" align="left">
                            <b>快速添加同一价格团队计划</b>
                        </td>
                    </tr>
                    <tr>
                        <td width="50%" align="left" valign="top">
                            <div id="divMonthPreNext">
                            </div>
                            <div id="div_DateTime">
                            </div>
                            <div id="hide_div_DateTime" style="display: none;">
                            </div>
                            <div id="divTourCodeHTML">
                            </div>
                        </td>
                        <td width="50%" valign="top">
                            <table width="100%" border="1" cellpadding="2" cellspacing="0" bordercolor="#addaf8">
                                <tr>
                                    <td width="25%" align="right" nowrap="nowrap">
                                        <span class="ff0000">*</span>团队人数：
                                    </td>
                                    <td width="75%" align="left">
                                        <input type="text" size="10" id="txt_tourNum" runat="server" />
                                        <input type="checkbox" id="chk_isLimit" runat="server" />
                                        不限制报名数
                                    </td>
                                </tr>
                                <tr id="tr_moreThan">
                                    <td align="right">
                                        余位：
                                    </td>
                                    <td align="left">
                                        <input type="text" size="10" id="txt_moreThan" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <span class="ff0000">*</span>成人（市/结）
                                    </td>
                                    <td align="left">
                                        <input type="text" size="10" id="txt_retailAdultPrice" runat="server" />
                                        <input type="text" size="10" id="txt_settlementAudltPrice" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        儿童（市/结）
                                    </td>
                                    <td align="left">
                                        <input type="text" size="10" id="txt_retailChildrenPrice" runat="server" />
                                        <input type="text" size="10" id="txt_settlementChildrenPrice" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        单房差：
                                    </td>
                                    <td align="left">
                                        <input type="text" size="10" id="txt_marketPrice" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        集合说明：
                                    </td>
                                    <td align="left">
                                        <input id="txt_setDec" type="text" size="25" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        出发班次时间：
                                    </td>
                                    <td align="left">
                                        <input id="txt_startDate" type="text" size="25" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        返回班次时间：
                                    </td>
                                    <td align="left">
                                        <input id="txt_endDate" type="text" size="25" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        线路销售备注：
                                    </td>
                                    <td align="left">
                                        <input id="txt_tourNotes" type="text" size="25" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        领队全陪说明：
                                    </td>
                                    <td align="left">
                                        <textarea id="txt_teamLeaderDec" cols="45" rows="4" runat="server"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <input type="button" name="AddSave" id="AddSave" onclick="TeamplanManage.AddSave()"
                                            value="添加" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <a href="javascript:void(0);" onclick="Boxy.iframeDialog({ title: '设置团号规则', iframeUrl: '/LineManage/SetTourNo.aspx?RouteId=<%=RouteId %>&AreaId=<%=AreaId %>&CompanyId=<%=CompanyId %>&rnd='+ Math.random(), height: 500, width: 400, draggable: false });return false;">
                                            自定义团号生产前缀</a>
                                    </td>
                                </tr>
                            </table>
                            <p align="left" style="padding: 5px;">
                                ·非必填项可以在后期，发班前单独修改<br />
                                ·为了方便修改出团单，每个团队的行程是独立的，如果需要统一修改行程，请点击这里，选择要修改的团队，一起修改行程。</p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("groupdate") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <script type="text/javascript">

        var type=null;
        //获取选择
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[name='checkboxLine']").each(function() {
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
    
        
    
        //TeamLineId为团号 
        var Parms = { RouteId:'<%=RouteId %>',TeamLineId: "", startDate: "",endDate:"", Page: 1 };
        var TeamplanManage = {
            GetTourIds: function() {
                var form = $("#tab_Tlist");
                var tourIds = "";
                form.find(".chk_select:checked").each(function() {
                    tourIds += $(this).val() + "|";
                })
                return tourIds.substring(0, tourIds.length - 1);
            },
            
            //团队计划列表
            GetTeamplanList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divTeamLineList");
                if (LoadingImg.IsLoadAddDataToDiv("divTeamLineList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxTeamLineList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divTeamLineList").html(html);
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
                this.GetTeamplanList();
            },
            OnSearch: function() {//查询
                Parms.TeamLineId = $("#<%=TeamLineId.ClientID%>").val();
                Parms.startDate = $("#<%=txtStartDate.ClientID%>").val();
                Parms.endDate = $("#<%=txtEndDate.ClientID%>").val();
                Parms.Page = 1;
                TeamplanManage.GetTeamplanList();
            },//加载日历
            LoadInitCalendar: function() {
                QGD.initCalendar({
                    containerId: "hide_div_DateTime",
                    currentDate: <%=CurrentDate %>,
                    firstMonthDate: <%=CurrentDate %>,
                    nextMonthDate: <%=NextDate %>,
                    areatype: <%= GetAreaType() %>,
                    listcontainer: "divTourCodeHTML",
                    parentContainerID: "<%=form1 %>"
                });
            },
            ChangeCalender: function() {
                var form = $("#AddSave").closest("form");
                var divFristHtml = form.find("#thisMonthCalendar");
                var divSecondHtml = form.find("#nextMonthCalendar");
                var preMonth = form.find("#linkPreMonth");
                var nextMonth = form.find("#linkNextMonth");
                form.find("#hide_div_DateTime").html("");
                form.find("#div_DateTime").append(divFristHtml);
                form.find("#div_DateTime").append(divSecondHtml);
                form.find("#divMonthPreNext").append(preMonth);
                form.find("#divMonthPreNext").append(nextMonth);
                QGD.updateCalendar(this.option);
            },
            LableToTxt: function(obj) {
                $(obj).hide();
                var inp = $(obj).closest("div").find("input").length > 0 ?
                $(obj).closest("div").find("input")
                :
                $(obj).closest("div").find(".sel_PowderTourStatus");
                inp.show();
                $(obj).closest("div").find(".sel_PowderTourStatus option[text='"+$.trim($(obj).text())+"']").attr("selected", true);
            },
           Delect: function(tourIds) {
                var Key = $("#tab_Tlist");
                if (tourIds.length > 0) {
                    if(confirm("确定删除计划？"))
                    {
                        $.ajax(
	                       {
	                           url: '/LineManage/TeamAdd.aspx?Operating=Delect&RouteId=<%=RouteId %>&tourIds=' + tourIds,
	                           dataType: "html",
	                           cache: false,
	                           type: "get",
	                           success: function(result) {
	                               if (result == "1") {
	                                   alert("删除成功！")
	                                   TeamplanManage.OnSearch();
	                               }
	                               else
	                               {
	                                    alert("删除失败！")
	                                   TeamplanManage.OnSearch();
	                               }
	                           },
	                           error: function() {
	                               alert("操作失败!");
	                           }
	                       });
                        }
                    }
                else {
                    alert("请选择计划");
                    return false;
                }
            },
            BtnOption: function(statue) {
                var Key = $("#tab_Tlist");
                var tourIds=TeamplanManage.GetTourIds();
                if (tourIds.length > 0) {
                    $.ajax(
	                   {
	                       url: '/LineManage/TeamAdd.aspx?Operating=BtnOption&TourStatus='+statue+'&tourIds=' + tourIds,
	                       dataType: "html",
	                       cache: false,
	                       type: "get",
	                       success: function(result) {
	                           if (result) {
	                               alert("设置成功！")
	                               TeamplanManage.OnSearch();
	                           }
	                       },
	                       error: function() {
	                           alert("操作失败!");
	                       }
	                   });
                }
                else {
                    alert("请选择计划");
                    return false;
                }
            },
            AddSave: function() {
                var Key = $("#form1");
                var data = {
                    RouteId: "", //线路Id
                    leaveDate: "", //出团时间
                    txt_tourNum: "", //团队人数
                    hd_isLimit: "", //是否显示报名人数
                    txt_moreThan: "", //余位
                    txt_retailAdultPrice: "", //成人市场价
                    txt_settlementChildrenPrice: "", //成人结算价
                    txt_retailChildrenPrice: "", //儿童市场价
                    txt_settlementChildrenPrice: "", //儿童结算价
                    txt_marketPrice: "", //单房差
                    txt_setDec: "", //集合说明
                    txt_startDate: "", //出发班次时间
                    txt_endDate: "", //返回班次时间
                    txt_tourNotes: "", //备注
                    txt_teamLeaderDec: ""//领队全陪说明
                };
                Key.find("#div_DateTime input[value!='on']:enabled:checked").each(function() {
                    data.leaveDate += $(this).attr("value") + ",";
                });
                var msg = "";
                data.leaveDate = data.leaveDate.substring(0, data.leaveDate.length - 1); //出团日期
                if (data.leaveDate.length <= 0) {
                    msg += "-请选择出团日期-\n";
                }
                data.RouteId = Key.find("#<%=hd_routeid.ClientID %>").val(); //线路id
                data.txt_tourNum = Key.find("#txt_tourNum").val(); //团队人数
                if (data.txt_tourNum.length <= 0) {
                    msg += "-请输入团队人数-\n";
                }
                data.hd_isLimit = $("#<%=chk_isLimit.ClientID %>").attr("checked") ? 0 : 1; //是否显示报名人数
                data.txt_moreThan = Key.find("#txt_moreThan").val(); //余位
                data.txt_retailAdultPrice = Key.find("#txt_retailAdultPrice").val(); //成人市场价
                data.txt_settlementAudltPrice = Key.find("#txt_settlementAudltPrice").val(); //成人结算价
                data.txt_retailChildrenPrice = Key.find("#txt_retailChildrenPrice").val(); //儿童市场价
                data.txt_settlementChildrenPrice = Key.find("#txt_settlementChildrenPrice").val(); //儿童结算价
                if (data.txt_retailAdultPrice.length <= 0) {
                    msg += "-请输入成人市场价-\n";
                }
                if (data.txt_settlementAudltPrice.length <= 0) {
                    msg += "-请输入成人结算价-\n";
                }                
//                if (data.txt_retailChildrenPrice.length <= 0) {
//                    msg += "-请输入儿童市场价-\n";
//                }                
//                if (data.txt_settlementChildrenPrice.length <= 0) {
//                    msg += "-请输入儿童结算价-\n";
//                }
                data.txt_marketPrice = Key.find("#txt_marketPrice").val(); //单房差
                data.txt_setDec = Key.find("#txt_setDec").val(); //集合说明
                data.txt_startDate = Key.find("#txt_startDate").val(); //出发班次时间
                data.txt_endDate = Key.find("#txt_endDate").val(); //返回班次时间
                data.txt_tourNotes = Key.find("#txt_tourNotes").val(); //备注
                data.txt_teamLeaderDec = Key.find("#txt_teamLeaderDec").val(); //领队全陪说明
                if(msg.length<=0)
                {
                    $.ajax(
                       {
                           url: 'TeamAdd.aspx?Operating=AddSave&CompanyId=<%=CompanyId %>&RouteId=<%=RouteId %>',
                           data: data,
                           dataType: "html",
                           cache: false,
                           type: "post",
                           success: function(result) {
                               if (result) {
                                   alert("添加成功！")
                                   TeamplanManage.OnSearch();
                               }
                           },
                           error: function() {
                               alert("操作失败!");
                           }
	              });
               }
               else
               {
                    alert(msg);
               }
            },
            
            AddSave1:function(){
                $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "TeamAdd.aspx?Operating=AddSave&RouteId=<%=RouteId %>",
                        cache: false,
                        success: function(html) {
                            if(html=="true")
                            {
                                alert("添加成功");
                            }
                            else
                                alert("添加失败");
                        }
               })               
                
            }
     }
     
     

     

        $(function() {
            TeamplanManage.LoadInitCalendar();
            setTimeout(function() {
                TeamplanManage.ChangeCalender();
                $("#div_DateTime :checkbox").click(function(i){
                    var datatimenow=new Date();  
                    var resultbool=false;
                    var datatimenow2=datatimenow.getFullYear().toString()+"-"+(datatimenow.getMonth()+1)+"-"+datatimenow.getDate().toString();
                    if(datatimenow2==$(this).val())
                    {
                        alert("不能发布当天计划");
                        $(this).attr("checked","");
                        $(this).attr("disabled","disabled");
                        return false;

                    }
                    //验证是否已经有当天的计划
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "TeamAdd.aspx?datavalue="+$(this).val()+"&Operating=CheckExcitDate&RouteId=<%=RouteId %>",
                        cache: false,
                        async:false,
                        success: function(html) {
                            if(html=="True")
                            {
                                resultbool=true;
                                alert("该天已经有了的计划");
                            }
                        },
                        error: function() {
                               alert("数据异常，请联系管理员!");
                        }
                    })  
                    if(resultbool)
                    {
                        $(this).attr("checked","");
                        $(this).attr("disabled","disabled");
                        return false;
                    }
                })
            }, 500);           
            
            var FormObj = $("#<%=form1.ClientID%>");
            
            
            


            //回车查询
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    TeamplanManage.OnSearch();
                    return false;
                }
            });
            TeamplanManage.OnSearch();
            
            $("#<%=chk_isLimit.ClientID %>").click(function(){
                 if($(this).attr("checked"))
                 {
                    $("#tr_moreThan").css("display","none");
                 }else
                 {
                    $("#tr_moreThan").css("display","");
                 }
            })
        });
    
    </script>

    </form>
</body>
</html>
