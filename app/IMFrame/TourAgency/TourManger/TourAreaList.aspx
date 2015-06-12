<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourAreaList.aspx.cs" Inherits="IMFram.TourAgency.TourManger.TourAreaList" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../../WebControls/SiteSelect.ascx" TagName="SiteSelect" TagPrefix="uc1" %>
<%@ Register Src="../../WebControls/IMTop.ascx" TagName="IMTop" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>指定供应商产品</title>
    <style>
        BODY
        {
            color: #333;
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            text-align: center;
            background: #fff;
            margin: 0px;
        }
        img
        {
            border: thin none;
        }
        table
        {
            border-collapse: collapse;
            margin: 0px auto;
            padding: 0px auto;
        }
        TD
        {
            font-size: 12px;
            color: #0E3F70;
            line-height: 20px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
        }
        div
        {
            margin: 0px auto;
            text-align: left;
            padding: 0px auto;
            border: 0px;
        }
        textarea
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        select
        {
            font-size: 12px;
            font-family: "宋体" ,Arial, Helvetica, sans-serif;
            color: #333;
        }
        .ff0000
        {
            color: #f00;
        }
        a
        {
            color: #0E3F70;
            text-decoration: none;
        }
        a:hover
        {
            color: #f00;
            text-decoration: underline;
        }
        a:active
        {
            color: #f00;
            text-decoration: none;
        }
        .bar_on_comm
        {
            width: 105px;
            height: 21px;
            float: left;
            border: 1px solid #94B2E7;
            border-bottom: 0px;
            background: #ffffff;
            text-align: center;
            color: #cc0000;
        }
        .bar_un_comm
        {
            width: 100px;
            height: 21px;
            float: left;
            text-align: center;
        }
        .bar_un_comm a
        {
            color: #0E3F70;
        }
        a.cliewh
        {
            display: block;
            width: 190px;
            height: 22px;
            overflow: hidden;
        }
        .aun
        {
            background: url(<%=ImageServerUrl%>/IM/images/sreach_annui.gif) no-repeat center;
            text-align: center;
        }
        .aun a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:visited
        {
            color: #1E446F;
            font-size: 14px;
        }
        .aun a:hover
        {
            color: #f00;
            font-size: 14px;
        }
        .aon
        {
            background: url(/Images/TourNewImages/areabottonnone.gif) no-repeat center;
            text-align: center;
            color: #aaa;
        }
        .aon a
        {
            color: #1E446F;
            font-size: 14px;
        }
        .lineon
        {
            background: url(<%=ImageServerUrl%>/IM/images/ztlineon.gif) no-repeat bottom;
            text-align: center;
            width: 66px;
            height: 16px;
            line-height: 16px;
            padding-top: 3px;
        }
        .lineon a
        {
            color: #f60;
        }
        .lineun
        {
            background: url(<%=ImageServerUrl%>/IM/images/ztlineun.gif) no-repeat bottom;
            text-align: center;
            width: 66px;
            height: 16px;
            line-height: 16px;
            padding-top: 3px;
        }
        .lineun a
        {
            color: #fff;
        }
        .linebj
        {
            background: url(<%=ImageServerUrl%>/IM/images/ztflbjline.gif) repeat-x bottom;
        }
        #SiteSelect1_table_SiteInfo
        {
            width: 202px !important;
        }
    </style>
</head>
<body oncontextmenu="return false;">
    <form id="form1" runat="server">
    <uc2:IMTop ID="IMTop1" PageType="false" runat="server" />
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" background="<%=ImageServerUrl%>/IM/images/ztopbj.gif">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td height="33" valign="bottom" background="<%=ImageServerUrl%>/IM/images/ztopbj.gif">
                            <table width="210" border="0" height="32" align="center" cellpadding="0" cellspacing="0"
                                style="margin-top: 3px;">
                                <tr>
                                    <td width="111" height="30" align="center" style="background: url(<%=ImageServerUrl%>/IM/images/ztopbtttt2.gif) no-repeat center;
                                        font-size: 14px; height: 30px;">
                                        <img src="<%=ImageServerUrl%>/IM/images/comments.gif" width="16" height="16" style="margin-bottom: -3px;" />
                                        <a href="/TourAgency/TourManger/TourAreaList.aspx"><font color="#cc0000"><strong>散客报名</strong></font></a>
                                    </td>
                                    <td width="111" align="center" style="background: url(<%=ImageServerUrl%>/IM/images/ztopbtttt.gif) no-repeat center;
                                        height: 26px;">
                                        <img src="<%=ImageServerUrl%>/IM/images/shop.gif" width="16" height="16" style="margin-bottom: -3px;" />
                                        <a href="/TourAgency/TourOrder/Default.aspx">我的订单</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:SiteSelect ID="SiteSelect1" runat="server" />
                <table width="210" border="0" align="center" cellpadding="0" cellspacing="0" style="background: url(<%=ImageServerUrl%>/IM/images/topzzh.gif) repeat-x bottom;
                    margin-bottom: 3px; margin-top: 2px;">
                    <tr>
                        <td align="center">
                            <strong><a href="/TourAgency/TourManger/SetAttentionCompany.aspx?IsSettionCompany=1"
                                id="a_SettionCompany" runat="server" class="ff0000">
                                <img src="<%=ImageServerUrl%>/IM/images/edit.gif" width="13" height="12" style="margin-bottom: -2px;
                                    margin-right: 10px;" />修改我的批发商（专线）目录</a></strong>
                            <div id="divAjaxMysettionCompany" style="width: 210px">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 5px; width: 210px">
        <tr>
            <td width="70" class="linebj">
                <div id="divChangeType0" class="lineon" onclick="ChangeTourArealist(0);">
                    <a href="javascript:void(0);">国内长线</a></div>
            </td>
            <td width="70" class="linebj">
                <div id="divChangeType2" class="lineun" onclick="ChangeTourArealist(2);">
                    <a href="javascript:void(0);">国内短线</a></div>
            </td>
            <td width="70" class="linebj">
                <div id="divChangeType1" class="lineun" onclick="ChangeTourArealist(1);">
                    <a href="javascript:void(0);">国际线</a></div>
            </td>
        </tr>
    </table>
    <div id="AjaxAreaNationType0" style="width: 210px" class="areatd">
    </div>
    <div id="AjaxAreaNationType2" style="width: 210px" class="areatd">
    </div>
    <div id="AjaxAreaNationType1" style="width: 210px" class="areatd">
    </div>
    <asp:HiddenField ID="hidSiteId" runat="server" />
    <asp:HiddenField ID="hidCityId" runat="server" />

    <script language="javascript" type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script src="<%=JsManage.GetJsFilePath("Loading") %>" type="text/javascript"></script>

    <script type="text/javascript">
    
   
    //获取我关注的供应商列表
    function GetMysettionCompany()
    {
        $.ajax
        ({
            url:"/TourAgency/TourManger/MySettionCompany.aspx",
            cache:false,
            success:function(html)
            {  
                 $("#divAjaxMysettionCompany").html(html);   
            }
        });
    }
    //切换线路区域类型
    function ChangeTourArealist(TypeId)
    {
        for(var i=0;i<3;i++)
        {
            if(i==TypeId)
            {
                $("#divChangeType"+TypeId).removeClass("lineun").addClass("lineon");
                $("#AjaxAreaNationType"+TypeId).show();
            }
            else
            {
                $("#divChangeType"+i).removeClass("lineon").addClass("lineun");
                $("#AjaxAreaNationType"+i).hide();
            }
        }
        
    }
    //切换销售城市时改变线路区域
    function GetAreaList(theCityId)
    {   
        if(theCityId==undefined||theCityId==""||theCityId=="0"||theCityId==null)//如果销售城市不存在取当前用户公司城市
        {
          theCityId=<%=cityId %>;
        }
        $("#AjaxAreaNationType0").hide();
        $("#AjaxAreaNationType2").hide();
        $("#AjaxAreaNationType1").hide();
        var strUrl = "/TourAgency/TourManger/AjaxAreaList.aspx?cityId="+theCityId;
        LoadingImg.ShowLoading("AjaxAreaNationType0");
        $.ajax
            ({
                url: strUrl+"&areaType=0",
                cache:false,
                success:function(html)
                {
                     $("#AjaxAreaNationType0").html(html);//取国内长线           
                }
            });
        $("#AjaxAreaNationType0").show();
         $.ajax
            ({
                url: strUrl+"&areaType=2",
                cache:false,
                success:function(html)
                {
                     $("#AjaxAreaNationType2").html(html);//取国内短线            
                }
            });
         $.ajax
            ({
                url: strUrl+"&areaType=1",
                cache:false,
                success:function(html)
                {
                     $("#AjaxAreaNationType1").html(html);//取国际线            
                }
            });
    }
    
    //初始化数据
    function GetDataList()
    {
        GetMysettionCompany();//获取我收藏的批发商
        GetAreaList(<%=cityId %>);//获取当前用户城市下的线路区域
    }
    
      //选择批发商的下拉框-跳转到网店
    function SelectChange(objSelect)
    { 
      var jObjSelect=$(objSelect).find("option:selected");
      if(jObjSelect.length>0&&jObjSelect.val()!="0")
      { 
        window.open(jObjSelect.attr("href"),'_blank')
      }
      return false;
    }
    
    function hideSelect()
    {
       $("#divAjaxMysettionCompany")[0].style.visibility="hidden";   
    }
    function showSelect()
    {
       $("#divAjaxMysettionCompany")[0].style.visibility="visible";   
    }
     $(document).ready(function(){ 
     
        LoadingImg.stringPort = "<%=EyouSoft.Common.Domain.ServerComponents %>";
        GetMysettionCompany();//初始化页面数据
        
     });
    
//    function SetAttentionCompany(url)
//    {
//      var SiteId= document.getElementById("<%=hidSiteId.ClientID %>").value;
//      var CityId=document.getElementById("<%=hidCityId.ClientID %>").value;
//        url=url+"&SiteId="+SiteId+"&CityId="+CityId;
//        LoadingImg.ShowLoading("divCompanyList");
//         $.ajax
//        ({
//            url:url,
//            cache:false,
//            success:function(html)
//            {
//                 $("#divCompanyList").html(html); 
//            }
//        });
//        
//    }
//      function Search()
//      {
//        var Name=document.getElementById("txtCompanyName").value;
//         SetAttentionCompany(strUrl+"&CompanyName="+escape(Name));
//        
//      }
  
//     function LoadData(obj)
//    {
//        var intpage = exporpage.getgotopage(obj);
//        page = "&Page=" + intpage;
//        SetAttentionCompany(strUrl+ page);  
//    }
    
    
//      function GetCompanyList(obj)
//    {
//        if(obj!=null)
//        {
//            var strSiteId=obj.value.split(",")[0];
//            var strCityId=obj.value.split(",")[1];
//            var hidSiteId=document.getElementById("<%=hidSiteId.ClientID %>");
//            if(hidSiteId!=null)
//            {
//                hidSiteId.value=strSiteId;
//                
//            }
//            var hidCityId=document.getElementById("<%=hidCityId.ClientID %>");
//            if(hidCityId!=null)
//            {
//                hidCityId.value=strCityId;
//                
//            }
//         }
//        SetAttentionCompany(strUrl);
//    }
    
   
             
    </script>

    </form>
</body>
</html>
