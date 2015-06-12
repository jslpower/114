<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicList.aspx.cs" Inherits="SiteOperationsCenter.ScenicManage.ScenicList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/ProvinceAndCityAndCounty.ascx" TagName="ProvinceAndCityAndCounty"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>景区管理</title>
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("Load") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("GetCityList") %>" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("MouseFollow") %>"></script>

    <script language="JavaScript" type="text/javascript">



        function mouseovertr(o) {
            o.style.backgroundColor = "#FFF9E7";
            //o.style.cursor="hand";
        }
        function mouseouttr(o) {
            o.style.backgroundColor = ""
        }
 

    </script>

    <script>

        //鼠标跟随代码//
        function wsug(e, str) {
            var oThis = arguments.callee;
            if (!str) {
                oThis.sug.style.visibility = 'hidden';
                document.onmousemove = null;
                return;
            }
            if (!oThis.sug) {
                var div = document.createElement('div'), css = 'top:0; left:-30px;text-align:left;color:#2C709F;position:absolute; z-index:100; visibility:hidden';
                div.style.cssText = css;
                div.setAttribute('style', css);
                var sug = document.createElement('div'), css = 'font:normal 12px/16px "宋体"; white-space:nowrap; color:#666; padding:3px; position:absolute; left:-30px; top:0; z-index:10; background:#f9fdfd; border:1px solid #629BC7;text-align:left;color:#2C709F;';
                sug.style.cssText = css;
                sug.setAttribute('style', css);
                var dr = document.createElement('div'), css = 'position:absolute; top:3px; left:-27px; background:#333; filter:alpha(opacity=30); opacity:0.3; z-index:9';
                dr.style.cssText = css;
                dr.setAttribute('style', css);
                var ifr = document.createElement('iframe'), css = 'position:absolute; left:0; top:-10; z-index:8; filter:alpha(opacity=0); opacity:0';
                ifr.style.cssText = css;
                ifr.setAttribute('style', css);
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

            var w = obj.sug.offsetWidth, h = obj.sug.offsetHeight, dw = document.documentElement.clientWidth || document.body.clientWidth; dh = document.documentElement.clientHeight || document.body.clientHeight;
            var st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
            var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17;
            obj.style.left = left + 10 + 'px';
            obj.style.top = top + 10 + 'px';
            dr.style.width = w + 'px';
            dr.style.height = h + 'px';
            ifr.style.width = w + 3 + 'px';
            ifr.style.height = h + 3 + 'px';
            obj.style.visibility = 'visible';
            document.onmousemove = function(e) {
                var e = e || window.event, st = document.documentElement.scrollTop || document.body.scrollTop, sl = document.documentElement.scrollLeft || document.body.scrollLeft;
                var left = e.clientX + sl + 17 + w < dw + sl && e.clientX + sl + 15 || e.clientX + sl - 8 - w, top = e.clientY + st + 17 + h < dh + st && e.clientY + st + 17 || e.clientY + st - 5 - h;
                obj.style.left = left + 'px';
                obj.style.top = top + 'px';
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td height="32" class="search_bg">
                &nbsp;景区名称
                <input id="txtScenicName" size="20" name="txtScenicName" runat="server" />
                <uc1:ProvinceAndCityAndCounty ID="ProvinceAndCityAndCounty1" runat="server" />
            </td>
        </tr>
        <tr>
            <td height="32" class="search_bg">
                状态
                <asp:DropDownList ID="DdlStatus" runat="server">
                </asp:DropDownList>
                B2B
                <asp:DropDownList ID="DdlB2B" runat="server">
                </asp:DropDownList>
                B2C
                <asp:DropDownList ID="DdlB2C" runat="server">
                </asp:DropDownList>
                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/chaxun.gif" width="62"
                    height="21" style="margin-bottom: -3px;" onclick="ScenicManage.OnSearch();" />
            </td>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td class="caozuo_bg">
                <table width="19%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="4%" align="right">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_da.gif" width="3"
                                height="20" />
                        </td>
                        <td width="23%">
                            <a href="AddScenic.aspx">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xinzeng.gif" width="50"
                                    height="25" border="0" /></a>
                        </td>
                        <td width="4%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" />
                        </td>
                        <td width="24%">
                            <a href="javascript:void()" id="hrefUpdate">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/xiugai.gif" width="50"
                                    height="25" border="0" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" border="0" />
                        </td>
                        <td width="23%">
                            <a href="javascript:void()" id="hrefDelete">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/shanchu.gif" width="51"
                                    height="25" /></a>
                        </td>
                        <td width="5%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_hang.gif" width="2"
                                height="25" border="0" />
                        </td>
                        <td>
                            <input id="btnListExamine" onclick="ListExamineStatue()" value="批量审核" type="button" />
                        </td>
                        <td width="17%">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/images/yunying/ge_d.gif" width="11"
                                height="25" border="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
        </tr>
    </table>
    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <div id="divCompanyList" align="center">
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
    
        var Parms = { ProvinceId: 0, CityId: 0,CountyId:0, ScenicName: "", Page: 1,Status:0,B2B:"",B2C:"" };
        var ScenicManage = {//景区列表
            GetScenicList: function() {
                 if(<%=currentPage %> >0 ){
                     Parms.Page=<%=currentPage %>;
                 }
                LoadingImg.ShowLoading("divCompanyList");
                if (LoadingImg.IsLoadAddDataToDiv("divCompanyList")) {
                    $.ajax({
                        type: "GET",
                        dataType: 'html',
                        url: "AjaxScenicList.aspx",
                        data: Parms,
                        cache: false,
                        success: function(html) {
                            $("#divCompanyList").html(html);
                        }
                    });
                }
            },

           ckAllCompany: function(obj) {//全选
                $("#tbCompanyList").find("input[type='checkbox']").attr("checked", $(obj).attr("checked"));
            },
            createUrl: function(url, params) {
                var isHaveParam = false;
                var isHaveQuestionMark = false;
                var questionMark = "?";
                var questionMarkIndex = url.indexOf(questionMark);
                var urlLength = url.length;

                if (questionMarkIndex == urlLength - 1) {
                    isHaveQuestionMark = true;
                } else if (questionMarkIndex != -1) {
                    isHaveParam = true;
                }

                if (isHaveParam == true) {
                    for (var key in params) {
                        url = url + "&" + key + "=" + params[key];
                    }
                } else {
                    if (isHaveQuestionMark == false) {
                        url += questionMark;
                    }
                    for (var key in params) {
                        url = url + key + "=" + params[key] + "&";
                    }
                    url = url.substr(0, url.length - 1);
                }
                return url;
            },
            
            
            LoadData: function(obj) {//分页
                var Page = exporpage.getgotopage(obj);
                Parms.Page = Page;
                this.GetScenicList();
            },
            OnSearch: function() {//查询
                Parms.ProvinceId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].provinceId)).val();
                Parms.CityId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].cityId)).val();
                Parms.CountyId=$(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].countyId)).val();
                Parms.ScenicName = $.trim($("#txtScenicName").val());
                Parms.Page = 1;
                Parms.Status = $("#DdlStatus").val();
                if($("#DdlB2B").val()=="请选择")
                Parms.B2B="";else Parms.B2B=$("#DdlB2B").val();
                
                if($("#DdlB2C").val()=="请选择")
                Parms.B2C="";else Parms.B2C=$("#DdlB2C").val();
                ScenicManage.GetScenicList();
            }
        }
        
        
          $(function() {
            LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
            
            $("#tb_SearchList input").bind("keypress", function(e) {
                if (e.keyCode == 13) {
                    ScenicManage.OnSearch();
                    return false;
                }
            });
            
            ScenicManage.GetScenicList();

        });


        //获取选择
        function IsSelectAdv() {
            var num = 0;
            var id = "";
            $("input[type='checkbox']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id = $(this).val();
                }
            });
            if (num == 1) {
                return id;
            }
            else {
                return -1;
            }
        }

        //获取选择
        function IsSelectAdv2() {
            var num = 0;
            var id = "";
            $("input[name='chkindex']").each(function() {
                if ($(this).attr("checked")) {
                    num++;
                    id += $(this).val().split('$')[0]+"$";
                }
            });
            if (num == 0) {
                return 0;
            }
            else {
                return id;
            }
        }
        
        //批量审核
        function ListExamineStatue()
        {
            
            //var id=IsSelectAdv2().substring(0,IsSelectAdv2().length-1);
            var id=IsSelectAdv2();
            if(id==0){
                alert("请选择一项");
                return false;
            }
            id=id+<%=ManageUserName %>;
            $.ajax({
                    url: "AjaxAll.ashx?type=ListExamineStatue&arg=" + id,
                    cache: false,
                    async: false,
                    success: function(msg) {
                           if(msg=="")
                           {
                                $("input[type='checkbox']").each(function() {
                                    if ($(this).attr("checked")) {
                                        $(this).attr("checked","");
                                    }
                                });
                                alert("批量审核成功");
                                ScenicManage.GetScenicList();
                           }
                           else if(msg=="NoLogin")
                           {
                                alert("你还未登录");
                           }
                           else
                           {
                                alert("批量审核失败");
                           }
                            
                    },
                    error: function() {
                        alert("操作失败");
                    }
                });
            
        }
        
        
        //根据权限判断隐藏和显示按钮
        function GantShowAndHide() {
            if ("<%=IsAddGant %>".toLowerCase() == "false") {
                $("#hrefAdd").hide();
            }
            if ("<%=IsUpdateGant %>".toLowerCase() == "false") {
                $("#hrefUpdate").hide();
                $("input[type='checkbox']").each(function() {
                    $(this).focus(function() {
                        this.blur();
                    });
                });
            }
            if ("<%=IsDeleteGant %>".toLowerCase() == "false") {
                $("#hrefDelete").hide();
            }
        }
        
        $("#hrefUpdate").click(function() {
                //if ("<%=IsUpdateGant %>".toLowerCase() == "true") {
                    var advid = IsSelectAdv();
                    if (advid != -1) {
                        $(this).attr("href", "AddScenic.aspx?ScenicId=" + advid);
                    } else {
                        alert("请选择一项！");
                        return false;
                    }
//                } else {
//                    alert("您没有修改的权限");
//                    return false;
//                }
            });
        
        
        
        $(function(){
            $("#hrefDelete").click(function(){
//                if("<%=IsUpdateGant %>".toLowerCase() == "true")
//                {
                    var advid = IsSelectAdv();
                    if (advid != -1) {
                        if (!confirm("你确定要删除该条数据吗？")) {
                            return false;
                        }
                        $.ajax
                        ({
                            url: "AjaxAll.ashx?type=DeleteScenic&arg=" + advid,
                            cache: false,
                            async: false,
                            success: function(msg) {
                                    switch (msg) {
                                        case "1":
                                            $("#tr"+advid.split('$')[0]).html("");
                                            alert("删除成功！");
                                            
                                            break;
                                        case "3":
                                            alert("删除失败！");
                                            break;
                                        case "2":
                                            alert("有门票不能删除！");
                                            break;
                                        case "NoLogin":
                                            alert("你还未登录");
                                            break;
                                    }
                            },
                            error: function() {
                                alert("操作失败");
                            }
                        });
                    } else {
                        alert("请选择一项！");
                        return false;
                    }
                //}
//                else
//                {
//                    alert("你没有删除的权限");
//                }
            })
        
        
        });


        var ScenicList = {

            OnSearch: function() {
                //省份
                var ProId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].provinceId)).val();
                //城市
                var CityId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].cityId)).val();
                //标题
                var ScenicName = $.trim($("#txtScenicName").val());
                //县区
                var CountyId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].countyId)).val();
                var DdlSta=$("#DdlStatus").val();
                //var Params = { ScenicName: ScenicName, ProId: ProId, CityId: CityId, CountyId: CountyId,DdlSta:DdlSta };
                //window.location.href = "/ScenicManage/ScenicList.aspx?" + $.param(Params);
                var Params="ScenicName='"+ScenicName+"'$ProId='"+ProId+"'$='"+CityId+"'$='"+CountyId+"'DdlSta='"+DdlSta+"'";
                $.ajax({
                     url: "AjaxAll.ashx?type=GetScenicList&arg="+Params,
                     cache: false,
                     type: "post",
                     success: function(result) {
                         if(result=="error")
                         {
                         }
                         else if(result=="NoLogin")
                         {
                            alert("你还未登录");
                         }
                         else{
                             var list = eval(result);
                             $("#Td_ChbLankId").html("");
                             for(var i=0;i<list.length;i++)
                             {
                                $("#Td_ChbLankId").append("<input type=\"checkbox\" name=\"chkboxLankid\" value=\""+list[i].Id+"\" id=\"cbx_"+i+"\" /><label for=\"cbx_"+i+"\">"+list[i].Por+"</label>"); 
                             }
                            }
                        },
                     error: function() {
                         alert("操作失败!");
                     }    
                    });
                
            },

            DelNew: function(Id) {
                //省份
                var ProId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].provinceId)).val();
                //城市
                var CityId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].cityId)).val();
                //标题
                var Title = $.trim($("#txtNewTitle").val());
                //县区
                var CountyId = $(("#" + provinceAndCityUserControl["<%=ProvinceAndCityAndCounty1.ClientID%>"].countyId)).val();
                var Params = { Id: Id, State: "Del", Title: Title, CityId: CityId, CountyId: CountyId };
                window.location.href = "/NewsCenterControl/NewsList.aspx?" + $.param(Params);
            },
            //鼠标选中后的背景样式
            mouseovertr: function(o) {
                o.style.backgroundColor = "#FFF9E7";
            },
            mouseouttr: function(o) {
                o.style.backgroundColor = "";
            }
        };
        $(document).ready(function() {
            var FormObj = $("#<%=form1.ClientID%>");

            //回车查询
            FormObj.keydown(function(event) {
                if (event.keyCode == 13) {
                    ScenicManage.OnSearch();
                    return false;
                }
            });
        });
    </script>

    </form>
</body>
</html>
