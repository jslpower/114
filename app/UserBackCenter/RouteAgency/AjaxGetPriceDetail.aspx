<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxGetPriceDetail.aspx.cs"
    Inherits="UserBackCenter.RouteAgency.AjaxGetPriceDetail" %>

<style type="text/css">
    .zhonglan
    {
        border-bottom: 1px solid #93B5D7;
        background: #F4F9FF;
        height: 20px;
        color: #000000;
    }
</style>
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" style="border: 1px solid #ECECEC;
    background: #FAFAFA; margin: 8px 0px 10px 0px; width: 99%;">
    <tr>
        <td width="13%" align="right">
            价格等级：
        </td>
        <td width="87%" align="left">
            <a href="javascript:void(0)" onclick="window.open('zdyjgdj.html','title','height=510,width=500,top=150,left=200,toolbar=no,menubar=no,scrollbars=yes,resizable=no,location=no,status=no')">
                <span class="huise">
                    <img src="../images/jiben5.gif" width="8" height="12" />设置常用价格等级</span></a>
        </td>
    </tr>
    <tr>
        <td align="right" valign="bottom" style="padding-bottom: 6px;">
            <select name="select2">
                <option>请选择</option>
            </select>
        </td>
        <td align="left" style="padding-bottom: 5px;">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td>
                        门市价(元/人)
                    </td>
                    <td>
                        同行价(元/人)
                    </td>
                    <td>
                        单房差(元/人)
                    </td>
                    <td width="300">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <nobr>
                    <input name="textfield332" type="text" class="bitiansm" value="成人" size="7" />
                    <input name="textfield3322" type="text" class="shurukuangsm" value="儿童" size="7" />
                  </nobr>
                    </td>
                    <td>
                        <nobr>
                    <input name="textfield3323" type="text" class="bitiansm" value="成人" size="7" />
                    <input name="textfield33222" type="text" class="shurukuangsm" value="儿童" size="7" />

                  </nobr>
                    </td>
                    <td>
                        <nobr>
                    <input name="textfield3324" type="text" class="bitiansm" value="门市" size="7" />
                    <input name="textfield33223" type="text" class="shurukuangsm" value="同行" size="7" />
                  </nobr>
                    </td>
                    <td>
                        <a href="#">增加一行</a> <a href="#">删除</a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript">
    var SetMyPriceUrl = "SetPriceStand.aspx";
    var AjaxGetPriceDetail = {
        SetMyPriceGrade: function(CompanyId) {
            new Controls.Dialog(SetMyPriceUrl + "?CompanyId=" + CompanyId + "&rnd=" + new Date().getTime(), '设置价格等级', { width: 600, height: 580, minmize: 'no', maximize: 'yes', scrollbars: 'yes', closebtn: 'yes' });
        },
        addthis: function(obj) {//新增一行
            //判断有无选择相同的报价等级 
            var ckPriceList = new Array();
            $("#tblPriceStand").find("select[@name='drpPriceRank']").each(function(i) {
                ckPriceList.push($(this).val());
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
            if (isHave == true) {
                alert("不能选择相同的报价等级!");
                return false;
            } else {
                var $tr = $(obj).parent().parent();
                var $tb = $(obj).parent().parent().parent();
                var $clonetr = $tr.clone();
                $clonetr.find("input").each(function() {
                    if ($(this).attr("type") == "hidden") {
                        $(this).val("0");
                    }
                });
                $clonetr.find("input").each(function(i) {
                    if ($(this).attr("type") == "text") {
                        if (i == 0 || i == 2) {
                            $(this).val("成人价");
                        } else if (i == 1 || i ==3) {
                            $(this).val("儿童价");
                        } else if (i == 4 ) {
                            $(this).val("结算价");
                        } else if (i == 5) {
                            $(this).val("门市价");
                        }
                    }
                });

                $tb.append($clonetr);
                AjaxGetPriceDetail.setValue();
            }
        },
        delthis: function(obj) {//删除一行
            var $tr = $(obj).parent().parent();
            var $tb = $(obj).parent().parent().parent();
            var trCount = $tb.find("tr").length;
            if (trCount > 2) {
                $tr.remove();
            }
        },
        isSamePrice: function(obj) {//选择报价等级时看是否有重复的选项
            var ckPriceList = new Array();
            $("#tblPriceStand").find("select[@name='drpPriceRank']").each(function(i) {
                ckPriceList.push($(this).val());
            });
            var isHave = false;
            var Newarr = ckPriceList.join(",") + ",";
            for (var i = 0; i < Newarr.length; i++) {
                if (Newarr.replace(ckPriceList[i] + ",", "").indexOf(ckPriceList[i] + ",") > -1) {
                    isHave = true;
                }
            }
            if (isHave == true) {
                alert("不能选择相同的报价等级!");
                return false;
            } else {

            }
        },
        SetNewPrice: function(NewObj) {
            //原来价格等级默认选项
            var OldckVal = new Array();
            $("#tblPriceStand").find("select[@name='drpPriceRank']").each(function() {
                OldckVal.push($(this).val());
            });

            //更新价格等级
            $("#tblPriceStand").find("select[@name='drpPriceRank']").each(function() {
                var $td = $(this).parent("td");
                $td.html(NewObj.toString());
            });

            //默认选中原来的选项
            $("#tblPriceStand").find("select[@name='drpPriceRank']").each(function(i) {
                var oldVal = OldckVal[i];
                $(this).attr("value", oldVal.toString());
                //原来选中的报低等级被删除后默认选中第一个
                if ($(this).val() == "" || $(this).val() == null) {
                    $(this)[0].selectedIndex = 0;
                }
            });
        },
        setValue: function() {
            $("#tblPriceStand").find("tr").each(function() {

                //门市
                $(this).find('td.adultorchildren:eq(0)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "成人价") { this.value = ""; } });
                $(this).find('td.adultorchildren:eq(0)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "成人价"; } });
                $(this).find('td.adultorchildren:eq(1)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "儿童价") { this.value = ""; } });
                $(this).find('td.adultorchildren:eq(1)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "儿童价"; } });
                //同行
                $(this).find('td.adultorchildren:eq(2)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "成人价") { this.value = ""; } });
                $(this).find('td.adultorchildren:eq(2)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "成人价"; } });
                $(this).find('td.adultorchildren:eq(3)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "儿童价") { this.value = ""; } });
                $(this).find('td.adultorchildren:eq(3)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "儿童价"; } });
                //单房差
                $(this).find('td.room:eq(0)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "结算价") { this.value = ""; } });
                $(this).find('td.room:eq(0)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "结算价"; } });
                $(this).find('td.room:eq(1)').find(' input[type="text"]').focus(function() { if ($.trim(this.value) == "门市价") { this.value = ""; } });
                $(this).find('td.room:eq(1)').find(' input[type="text"]').blur(function() { if ($.trim(this.value) == "") { this.value = "门市价"; } });
            });

        }
    };
    $(document).ready(function() {
        if ("<%=NoFast %>" == "True") {
            $("#HeadTr").addClass("shenglan_lr");
            $("#tblPriceStand").attr("width", "90%");
        }
        AjaxGetPriceDetail.setValue();
    });
</script>

