<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourPriceStand.ascx.cs"
    Inherits="UserBackCenter.usercontrol.RouteAgency.TourPriceStand" %>
<style type="text/css">
    .zhonglan
    {
        border-bottom: 1px solid #93B5D7;
        background: #F4F9FF;
        height: 20px;
        color: #000000;
    }
</style>
<table width="90%" border="0" align="center" cellpadding="0" cellspacing="2" style="border: 1px solid #ECECEC;
    background: #FAFAFA; margin: 8px 0px 10px 0px; width: 99%;">
    <tr>
        <td width="13%" align="right">
            价格等级：
        </td>
        <td width="80%" align="left">
            <a href="javascript:void(0)" onclick="TourPriceStand.SetMyPriceGrade('<%=CompanyId %>');return false;">
                <span class="huise">
                    <img src="<%=ImageServerPath %>/images/jiben5.gif" width="8" height="12" />设置常用价格等级</span></a>
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-bottom: 5px; width: 95%;" colspan="2">
            <table id="<%=ContainerID %>tblPriceStand" width="100%" border="0" cellspacing="0"
                cellpadding="0" style="border: 1px solid #93B5D7;">
                <tr id='<%=ContainerID %>HeadTr'>
                    <td style="border: 1px solid #93B5D7;" width="10%">
                    </td>
                    <asp:Repeater ID="rptPriceStand" runat="server">
                        <ItemTemplate>
                            <td style="border: 1px solid #93B5D7;" align="center" width="18%">
                                <%# DataBinder.Eval(Container.DataItem, "FieldName")%>(元/人)
                                <input type="hidden" name="hidCustomerLevelID" value='<%# DataBinder.Eval(Container.DataItem, "FieldId")%>' />
                            </td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <td style="border: 1px solid #93B5D7;">
                        &nbsp;操作
                    </td>
                </tr>
                <%=strTourPriceDetail %>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript">
    var TourPriceStand = {
	    _getData:function(id){
		    return commonTourModuleData.get(id);
	    },
        SetMyPriceGrade: function(CompanyId) {
            TourModule.OpenDialog('设置价格等级','/RouteAgency/SetPriceStand.aspx?ContainerID=<%=ContainerID %>&rnd='+ Math.random(),620,500);
        },
        addthis: function(element,initmodel,id) {//新增一行
            var obj = this._getData(id);
            //判断有无选择相同的报价等级 
            var ckPriceList = [],ckPriceNameList = [],ckPriceIdList = [];
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function() {
                ckPriceList.push($(this).val());
            });
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").eq(0).find("option").each(function() {
                ckPriceIdList.push($(this).val());
                ckPriceNameList.push($(this).text());
            });
            var isHave = false;
            var Newarr = ckPriceList.join(",") + ",";
            for (var i = 0; i < Newarr.length; i++) {
                if (Newarr.replace(ckPriceList[i] + ",", "").indexOf(ckPriceList[i] + ",") > -1) {
                    isHave = true;
                }
            }
            if (ckPriceList.toString() == "") {
                alert("请设置报价等级!");
                return false;
            }
            if (isHave) {
                alert("不能选择相同的报价等级!");
                return false;
            } else {
                if(initmodel != null)
                {
                    $("#"+ obj.ContainerID +"tblPriceStand tr").not($("#"+ obj.ContainerID +"HeadTr")).eq(0).remove();
                    for(var v = 0; v < initmodel.length; v ++)
                    {
                        var str = [];
                        str.push("<tr class=\"zhonghui\">");
                        str.push("<td align=\"right\" style=\"border:1px solid #93B5D7;\"><select name=\"drpPriceRank\" onchange=\"TourPriceStand.isSamePrice(this,'"+ obj.ContainerID +"')\" valid=\"required\" errmsg=\"请选择报价等级\">");
                        for(j = 0; j < ckPriceIdList.length; j ++ )
                        {
                            if(ckPriceIdList[j] == initmodel[v].PriceStandId)
                            {
                                str.push("<option value=\""+ ckPriceIdList[j] +"\" selected>"+ ckPriceNameList[j] +"</option>");
                            }
                            else
                            {
                                str.push("<option value=\""+ ckPriceIdList[j] +"\">"+ ckPriceNameList[j] +"</option>");
                            }
                        }
                        str.push("</select></td>");
                        
                        var arrLevelID = new Array();
                        $("#"+ obj.ContainerID +"tblPriceStand tr").eq(0).find("input[type=hidden]").each(function(){
                            if($(this).val() != '0' && $(this).val() != '1' && $(this).val() != '2' && $(this).val() != '')
                            {
                                arrLevelID.push($(this).val());
                            }
                        });
                        
                        var PriceDetail = initmodel[v].PriceDetail;
                        var strDF = '';
                        var arrCustomerLevelID = new Array();
                        for(var h = 0; h < PriceDetail.length; h ++)
                        {
                            arrCustomerLevelID.push(PriceDetail[h].CustomerLevelType);
                            switch(PriceDetail[h].CustomerLevelType)
                            {
                                case 0:
                                case 1:                            
                                    str.push("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"bitiansm\"  type=\"text\" id=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].AdultPrice <= 0 ? 0 : PriceDetail[h].AdultPrice) +"\">");
                                    str.push("&nbsp;<input name=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].ChildrenPrice <= 0 ? 0 : PriceDetail[h].ChildrenPrice) +"\"></nobr></TD>");
                                break;
                                case 2:
                                    strDF +="<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"room\"><nobr><input name=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].AdultPrice <= 0 ? 0 : PriceDetail[h].AdultPrice) +"\">";
                                    strDF += "&nbsp;<input name=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].ChildrenPrice <= 0 ? 0 : PriceDetail[h].ChildrenPrice) +"\"></nobr></TD>";
                                break;
                                default:
                                    var oid = -1;
                                    if(arrLevelID != null){
                                        for(var e = 0;e < arrLevelID.length; e ++){
                                            if(arrLevelID[e] == PriceDetail[h].CustomerLevelType){
                                                oid = 0;
                                            }
                                        }
                                    }
                                    if(oid == 0)
                                    {
                                        str.push("<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].AdultPrice <= 0 ? 0 : PriceDetail[h].AdultPrice) +"\">");
                                        str.push("&nbsp;<input name=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice"+ PriceDetail[h].CustomerLevelId +"\" value=\""+ (PriceDetail[h].ChildrenPrice <= 0 ? 0 : PriceDetail[h].ChildrenPrice) +"\"></nobr></TD>");
                                   }
                                break;     
                           }
                        }
                        
                        var strTMP = '';
                        if(arrLevelID.length > 0)
                        {
                            for(var l = 0; l < arrLevelID.length; l ++){
                                if(arrLevelID[l] != ''){
                                    var oid = -1;
                                    for(var a = 0; a < arrCustomerLevelID.length; a ++)
                                    {
                                        if(arrLevelID[l] != arrCustomerLevelID[a] && arrCustomerLevelID[a] != '0' && arrCustomerLevelID[a] != '1' && arrCustomerLevelID[a] != '2')
                                        {
                                            oid = 0;
                                        }
                                    }
                                    if(oid == 0)
                                    {
                                        if(arrLevelID[l] != '0' && arrLevelID[l] != '1' && arrLevelID[l] != '2' && arrLevelID[l] != ''){
                                            strTMP += "<TD align=\"center\" style=\"border:1px solid #93B5D7;\" class=\"adultorchildren\"><nobr><input name=\"PeoplePrice"+ arrLevelID[l] +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"PeoplePrice"+ arrLevelID[l] +"\" value=\"成人价\">";
                                            strTMP += "&nbsp;<input name=\"ChildPrice"+ arrLevelID[l] +"\" size=\"7\" class=\"shurukuang\"  type=\"text\" id=\"ChildPrice"+ arrLevelID[l] +"\" value=\"儿童价\"></nobr></TD>";
                                        }
                                    }
                                }
                            }
                            str.push(strTMP);
                        }
                        
                        str.push(strDF);
                        str.push("<td align=\"left\" style=\"border:1px solid #93B5D7;\"><a onclick=\"TourPriceStand.addthis(this,null,'"+ obj.ContainerID +"');return false;\" href=\"javascript:void(0);\">增加一行</a>&nbsp;<a onclick=\"TourPriceStand.delthis(this);return false;\" href=\"javascript:void(0);\">删除</a></td>");
                        str.push("</tr>");
                        $("#"+ obj.ContainerID +"tblPriceStand").find("tbody").append(str.join('')); 
                        
                        if ("<%=ReleaseType %>" == "AddStandardTour" || "<%=ReleaseType %>" == "AddStandardRoute" || "<%=ReleaseType %>" == "LocalStandardRoute") {
                            $("#"+ obj.ContainerID +"HeadTr").addClass("shenglan_lr");
                            $("#"+ obj.ContainerID +"HeadTr td").attr("width", "18%");
                        }else{
                            $("#"+ obj.ContainerID +"tblPriceStand").attr("style","");
                            $("#"+ obj.ContainerID +"tblPriceStand td").attr("style","");
                        } 
                    }                 
                }else{  
                    var $tr = $(element).parent().parent();
                    var $tb = $(element).parent().parent().parent();                  
                    var $clonetr = $tr.clone();
                    $clonetr.find("input").each(function() {
                        if ($(this).attr("type") == "hidden") {
                            $(this).val("0");
                        }
                    });
//                    $clonetr.find("input").each(function(i) {
//                        if ($(this).attr("type") == "text") {
//                            if (i == 0 || i == 2) {
//                                $(this).val("成人价");
//                            } else if (i == 1 || i ==3) {
//                                $(this).val("儿童价");
//                            } else if (i == 4 ) {
//                                $(this).val("结算价");
//                            } else if (i == 5) {
//                                $(this).val("门市价");
//                            }
//                        }
//                    });
                    //门市
                    $clonetr.find('td.adultorchildren').find(' input[type="text"]:eq(0)').val("成人价");
                    $clonetr.find('td.adultorchildren').find(' input[type="text"]:eq(1)').val("儿童价");
                    //同行
                    $clonetr.find('td.adultorchildren').find(' input[type="text"]:eq(2)').val("成人价");
                    $clonetr.find('td.adultorchildren').find(' input[type="text"]:eq(3)').val("儿童价");
                    //单房差
                    $clonetr.find('td.room').find(' input[type="text"]:eq(0)').val("结算价");
                    $clonetr.find('td.room').find(' input[type="text"]:eq(1)').val("门市价");
                    $tb.append($clonetr);
                }
                TourPriceStand.setValue(id);
            }
        },
        delthis: function(element) {//删除一行
            var $tr = $(element).parent().parent();
            var $tb = $(element).parent().parent().parent();
            var trCount = $tb.find("tr").length;
            if (trCount > 2) {
                $tr.remove();
            }
        },
        isSamePrice: function(element,id) {//选择报价等级时看是否有重复的选项
            var obj = this._getData(id);
            var ckPriceList = new Array();
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function(i) {
                ckPriceList.push($(this).val());
            });
            var isHave = false;
            var Newarr = ckPriceList.join(",") + ",";
            for (var i = 0; i < Newarr.length; i++) {
                if (Newarr.replace(ckPriceList[i] + ",", "").indexOf(ckPriceList[i] + ",") > -1) {
                    isHave = true;
                }
            }
            if (isHave) {
                alert("不能选择相同的报价等级!");
                return false;
            } else {

            }
        },
        SetNewPrice: function(NewObj,id) {
            var obj = this._getData(id);
            //原来价格等级默认选项
            var OldckVal = new Array();
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function() {
                OldckVal.push($(this).val());
            });

            //更新价格等级
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function() {
                var $td = $(this).parent("td");
                $td.html(NewObj.toString());
            });

            //默认选中原来的选项
            $("#"+ obj.ContainerID +"tblPriceStand").find("select[name='drpPriceRank']").each(function(i) {
                var oldVal = OldckVal[i];
                $(this).attr("value", oldVal.toString());
                //原来选中的报低等级被删除后默认选中第一个
                if ($(this).val() == "" || $(this).val() == null) {
                    $(this)[0].selectedIndex = 0;
                }
            });
        },
        setValue: function(id) {
            var obj = this._getData(id);
            $("#"+ obj.ContainerID +"tblPriceStand").find("tr").each(function() {
                //门市
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(0)').focus(function() { if ($.trim(this.value) == "成人价") { this.value = ""; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(0)').blur(function() { if ($.trim(this.value) == "") { this.value = "成人价"; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(1)').focus(function() { if ($.trim(this.value) == "儿童价") { this.value = ""; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(1)').blur(function() { if ($.trim(this.value) == "") { this.value = "儿童价"; } });
                //同行
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(2)').focus(function() { if ($.trim(this.value) == "成人价") { this.value = ""; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(2)').blur(function() { if ($.trim(this.value) == "") { this.value = "成人价"; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(3)').focus(function() { if ($.trim(this.value) == "儿童价") { this.value = ""; } });
                $(this).find('td.adultorchildren').find(' input[type="text"]:eq(3)').blur(function() { if ($.trim(this.value) == "") { this.value = "儿童价"; } });
                //单房差
                $(this).find('td.room').find(' input[type="text"]:eq(0)').focus(function() { if ($.trim(this.value) == "结算价") { this.value = ""; } });
                $(this).find('td.room').find(' input[type="text"]:eq(0)').blur(function() { if ($.trim(this.value) == "") { this.value = "结算价"; } });
                $(this).find('td.room').find(' input[type="text"]:eq(1)').focus(function() { if ($.trim(this.value) == "门市价") { this.value = ""; } });
                $(this).find('td.room').find(' input[type="text"]:eq(1)').blur(function() { if ($.trim(this.value) == "") { this.value = "门市价"; } });
            });

        }
    };
    $(document).ready(function() {
        if ("<%=ReleaseType %>" == "AddStandardTour" || "<%=ReleaseType %>" == "AddStandardRoute" || "<%=ReleaseType %>" == "LocalStandardRoute") {
            $("#<%=ContainerID %>HeadTr").addClass("shenglan_lr");
            $("#<%=ContainerID %>HeadTr td").attr("width", "10%");
            $("#<%=ContainerID %>tblPriceStand").attr("style","{border:1px solid #93B5D7;}");
        }else{
            $("#<%=ContainerID %>tblPriceStand").attr("style","");
            $("#<%=ContainerID %>tblPriceStand td").attr("style","");
        }
        TourPriceStand.setValue('<%=ContainerID %>');
    });
</script>

