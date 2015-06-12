<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EShopPage.aspx.cs" Inherits="UserBackCenter.EShop.EShopPage" MasterPageFile="~/MasterPage/Site1.Master" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="eshoppage_content2" runat="server">
    <table width="99%" border="0" cellspacing="0" cellpadding="0" style="margin-top:5px;">
      <tr>
        <td width="25%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/wangdanshiyi.gif" width="91" height="23"/></td>
       
        <td width="18%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#007BBB; font-size:16px; text-align:right; font-weight:bold;"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/wangdanshiyi1.gif"/></td>

        <td width="17%" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-left:15px; padding-top:0px; color:#FF6000; font-size:12px; text-align:left; font-weight:bold;"><div style="CURSOR: hand" id="showhideshop" ><span id="HSText1" style="background:#FFF9E8; border:1px solid #FF9933; padding:1px; ">隐藏网店模版</span></div></td>
        <td width="30%" align="left" style="background:#F7F9FD; border-bottom:1px solid #98B7CC; height:25px; padding-top:0px; color:#007BBB; font-size:16px; text-align:left; font-weight:bold;"><a href="<%=EShopUrl %>" target="_blank" id="lnkEShop"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/right_bottom.gif" width="12" height="12" /> 进入我的网店 </a></td>
      </tr>
      <tr>
        <td colspan="5" style=" padding-left:60px; text-align:left;"><img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/seniorshop/dianxia.gif" width="16" height="5" /></td>
      </tr>
    </table>
  <div  id="DivHiddenTourInfo1">
	<table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" style="border:1px solid #C6DDE9; padding:5px; margin:3px;">
      <tr> 
        <td width="25%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="p-dimg">
            <tr>
                <td align="center">
                    <label for="temple1">
                        <a href="javascript:void(0)">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/moban1.gif" width="200" height="76" border="0" /></a>
                    </label>
                </td>
            </tr>
            <tr>
              <td align="center"><input type="radio" name="eshoppage_rdoset" value="1" id="temple1" />
                <label for="temple1">模板1</label></td>
            </tr>
        </table></td>
        <td width="25%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="p-dimg">
            <tr>
                <td align="center">
                    <label for="temple2">
                        <a href="javascript:void(0)">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/moban2.gif" width="200" height="76" border="0" /></a>
                    </label>
                </td>
            </tr>
            <tr>
              <td align="center"><input type="radio" name="eshoppage_rdoset" value="2" id="temple2" />
                <label for="temple2">模板2</label></td>
            </tr>
        </table></td>
        <td width="25%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0" class="p-dimg">
            <tr>
                <td align="center">
                    <label for="temple3">
                        <a href="javascript:void(0)">
                            <img src="<%=ImageManage.GetImagerServerUrl(1)%>/Images/moban3.gif" width="200" height="76" border="0" /></a>
                    </label>
                </td>
            </tr>

            <tr>
              <td align="center"> <input type="radio" name="eshoppage_rdoset" value="3" id="temple3" />
                <label for="temple3" >模板3</label></td>
            </tr>
        </table></td>
        <td width="25%">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="p-dimg">
                <tr>
                    <td align="center">
                        <label for="temple4">
                            <a href="javascript:void(0)">
                                <img src="<%=ImageManage.GetImagerServerUrl(1)%>/T4/images/moban4.gif" width="200" height="76" border="0" /></a>
                        </label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <input type="radio" name="eshoppage_rdoset" <%=role=="0"?"disabled=disabled":"" %> value="4" id="temple4"  />
                        <label for="temple4">
                            模板4</label>
                    </td>
                </tr>
            </table>
        </td>       
      </tr>
	   <tr >
    <td align="center" style="padding-left:400px;" colspan="4">
        <a href="javascript:void(0)" class="xiayiye" id="eshoppage_btnsave">保存</a><input runat="server" type="hidden" runat="server" id="eshoppage_oldstyleIndex" value="1" /> </td>

  </tr>
    </table>
  </div>
  <table width="98%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td><iframe name="eshoppage_ifrmname" id="eshoppage_ifrmname" frameborder="0" width="100%" height="670" marginheight="0" marginwidth="0" scrolling="yes" src="<%=EShopMUrl %>" style="float: left; border: 2px solid #FFFF00"></iframe></td>
  </tr>
</table>
<script type="text/javascript">
    function initSelectRadio(index) {
        $("input[type='radio']").each(function() {
            if ($(this).val() == index) {
                $(this).attr("checked", true)
            }
        });
    }

    $(document).ready(function() {
        $("#showhideshop").click(function() {
            if ($("#HSText1").html() == "隐藏网店模版") {
                $("#DivHiddenTourInfo1").hide();
                $("#HSText1").html("显示网店模版");
            } else {
                $("#DivHiddenTourInfo1").show();
                $("#HSText1").html("隐藏网店模版");
            }
        });

        $("#eshoppage_btnsave").click(function() {
            var selectval = 0;
            $("input[type='radio']").each(function() {
                if (this.checked) {
                    selectval = this.value;
                }
            });

            if (selectval == 0) {
                alert("请选择模板！");
                return false;
            }

            $.newAjax({
                url: "/EShop/EShopPage.aspx?StyleIndex=" + selectval,
                cache: false,
                dataType: "json",
                success: function(data) {
                    alert(data.msg);
                    if (data.isSuccess) {
                        document.getElementById("eshoppage_ifrmname").contentWindow.location.href = data.sMUrl;
                        $("#lnkEShop").attr("href", data.sUrl);
                    }
                },
                error: function() {
                    alert("操作失败!");
                }
            });

        });

        //点击模板后立即跳转到模板管理页面
        $("input[name='eshoppage_rdoset']").click(function() {
            var mPageUrl = "/Eshop/EShopSet.aspx";
            switch (this.value) {
                case "4": mPageUrl = "/Eshop/EshopSet4.aspx"; break;
                default: mPageUrl += "?style=" + this.value;
                    break;
            }

            document.getElementById("eshoppage_ifrmname").contentWindow.location.href = mPageUrl;
        });
    });
</script>
</asp:Content>
