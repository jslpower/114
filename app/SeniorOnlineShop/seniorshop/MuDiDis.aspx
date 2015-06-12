<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MuDiDis.aspx.cs" Inherits="SeniorOnlineShop.seniorshop.MuDiDis" MasterPageFile="~/master/SeniorShop.Master" %>
<%@ MasterType VirtualPath="~/master/SeniorShop.Master" %>
<asp:Content ContentPlaceHolderID="c1" ID="c1" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="neiringht">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="19%" class="shenglan">
                            出游指南
                        </td>
                        <td width="29%" class="huise" >
                            关键字：
                            <input name="txtKeyWord" id="txtKeyWord" value="<%=Request.QueryString["k"] %>" type="text" size="18" />
                        </td>
                        <td width="52%">
                            <a id="linkSearch" href="javascript:void(0)">
                                <img src="<%=ImageServerUrl %>/images/seniorshop/search.gif" border="0" /></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div class="neiringhtk">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="border: 1px solid #EAEAEA;
                        padding: 1px;">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="muhang"
                                    style="border-bottom: 1px solid #EEEEEE;">
                                    <tr>
                                        <td width="88%" class="lvsezi14">
                                            <strong>风土人情介绍</strong>
                                        </td>
                                        <td width="12%">
                                            <a href="MuDiDis2_1_<%=CompanyId %>" class="lvsezi">更多&gt;&gt;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            <%=Guide1Html %>                               
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maint10" style="border: 1px solid #EAEAEA;
                        padding: 1px;">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="muhang"
                                    style="border-bottom: 1px solid #EEEEEE;">
                                    <tr>
                                        <td width="88%" class="lvsezi14">
                                            <strong>温馨提醒</strong>
                                        </td>
                                        <td width="12%">
                                            <a href="MuDiDis2_2_<%=CompanyId %>" class="lvsezi">更多&gt;&gt;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                 <%=Guide2Html %>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="maintop5" style="border: 1px solid #EAEAEA;
                        padding: 1px;">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="muhang"
                                    style="border-bottom: 1px solid #EEEEEE;">
                                    <tr>
                                        <td width="88%" class="lvsezi14">
                                            <strong>综合介绍</strong>
                                        </td>
                                        <td width="12%">
                                            <a href="MuDiDis2_3_<%=CompanyId %>" class="lvsezi">更多&gt;&gt;</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                 <%=Guide3Html %>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
     <script type="text/javascript">
    var newsList = {
        search:function(){
            var k = $("#txtKeyWord").val();
            var cid="<%=CompanyId %>";
            window.location.href="/seniorshop/MuDiDis.aspx?"+$.param({k:k,cid:cid});
            return false;
        }
    };
    $(function(){
        $("#linkSearch").click(function(){
            return newsList.search();
        });
         $("#txtKeyWord").bind("keypress", function(e)
          {
            if (e.keyCode == 13) {
            $("#linkSearch").click(); 
            return false;
            }
        });
    });   
    //鼠标移上来，数组中的这个图片的显示，其它显示表题
//   function showonmouseover(thisid,typeId)   
//   {
//       var idsArry=arryToshowArryId(typeId);
//       for(i;i<idsArry.length;i++)       
//       {
//            if(idsArry[i]==thisid)
//            {    
//                $("#show"+thisid).show();
//                $("#"+thisid).hide();
//            }else
//            {
//                $("#show"+idsArry[i]).hide();      //图片div隐藏
//                $("#"+idsArry[i]).show();      //图片div隐藏
//            }
//            
//       }
//   } 

   
//   function arryToshowArryId(tyeId)
//   {
//        var ids;
//        switch (tyeId)
//        {
//            case "1":
//                ids="<%=ArryId1 %>".substr(0,"<%=ArryId1%>".lastIndexOf(','));
//                return ids.split(',');
//                break;
//            case "2":
//                ids="<%=ArryId2 %>".substr(0,"<%=ArryId2%>".lastIndexOf(','));               
//                return ids.split(',');
//                break;
//            case "3":
//                ids="<%=ArryId3 %>".substr(0,"<%=ArryId3%>".lastIndexOf(','));
//              return ids.split(',');
//              break;
//             default :
//                window.location.reload();
//                return;
//        }
//     
//   }
    </script>
</asp:Content>