<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExchangeListControl.ascx.cs"
    Inherits="SupplyInformation.UserControl.ExchangeListControl" %>
<dl>
    <dt>
        <input type="hidden" value="0" /><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?SearchType=1&type=-1" target="_blank"><strong>供求信息</strong></a><input
                type="hidden" value="1" /><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?SearchType=1&type=1" target="_blank">团队询价</a><input
                    type="hidden" value="2" /><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?SearchType=1&type=2" target="_blank">地接报价</a><input
                        type="hidden" value="4" /><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?SearchType=1&type=4" target="_blank">车辆</a><input
                            type="hidden" value="6" /><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx?SearchType=1&type=6" target="_blank">招聘</a><span
                                class="gqadd"><a href="/supplyinformation/addsupplyinfo.aspx" onclick="topTab.open($(this).attr('href'),'我的供求',{isRefresh:true});return false;">发布我的供求</a></span>
        <span class="mor"><a href="<%=EyouSoft.Common.Domain.UserPublicCenter %>/SupplierInfo/ExchangeList.aspx" target="_blank">
            更多>></a></span></dt>
    <dd>
        <ul>
            <div id="divExchangeList0">
            </div>
            <div id="divExchangeList1">
            </div>
            <div id="divExchangeList2">
            </div>
            <div id="divExchangeList4">
            </div>
            <div id="divExchangeList6">
            </div>
        </ul>
    </dd>
</dl>

<script type="text/javascript">
var ExchangeList = {
    ExchangeType:'0',
    TabChange: function(){
	    $(".tixing dl dt>a:first").addClass("tixingActive");
	    $(".tixing dl dd ul").not(":first").hide();
	    $(".tixing dl dt>a").unbind("mouseover").bind("mouseover", function(){
		    $(this).siblings("a").removeClass("tixingActive").end().addClass("tixingActive");
		    ExchangeList.ExchangeType = $(this).prev("input").val();
		    ExchangeList.CheckData(ExchangeList.ExchangeType);
		});
	},
    GetData: function(){
        var url = '/SupplyInformation/AjaxGetExchangeList.aspx?ExchangeType='+ ExchangeList.ExchangeType;
        $.ajax({
            url:url,
            cache:false,
            success:function(html){
                if(html != '')
                {
                     $("#divExchangeList" + ExchangeList.ExchangeType).html(html).attr("style","display: ").siblings("div").attr("style","display:none");
                }
            }
        });
    },
    CheckData:function(type){
        if($.trim($("#divExchangeList" + type).html()) == '' || $.trim($("#divExchangeList" + type).html()) == null || $.trim($("#divExchangeList" + type).html()) == undefined)
        {
            ExchangeList.GetData();
        }else{
            $("#divExchangeList" + type).attr("style","display: ").siblings("div").attr("style","display:none");
        }
    }
};
ExchangeList.TabChange();
ExchangeList.GetData();
</script>

