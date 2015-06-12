<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FinancialManageTab.ascx.cs"
    Inherits="UserBackCenter.usercontrol.FinancialManagement.FinancialManageTab" %>
<table id="tbl_<%=this.ClientID %>" width="100%" border="0" cellspacing="0" cellpadding="0"
    class="zttoolbar" style="padding-top: 2px;">
    <tr>
        <td width="150" class="<%=TabIndex==0?"zttooltitle":"zttooltitleun" %>" style="padding-top: 5px;">
            <a href="/FinanceManage/AccountsReceivable.aspx" rel="FinancialManageTab">应收账款</a>
        </td>
        <td width="150" align="left" class="<%=TabIndex==1?"zttooltitle":"zttooltitleun" %>"
            style="padding-top: 5px;">
            <a href="/FinanceManage/AccountsReceived.aspx" rel="FinancialManageTab">已收账款</a>
        </td>
        <td width="150" align="left" class="<%=TabIndex==2?"zttooltitle":"zttooltitleun" %>"
            style="padding-top: 5px;">
            <a href="/FinanceManage/AccountPayable.aspx" rel="FinancialManageTab">应付账款</a>
        </td>
        <td width="150" align="left" class="<%=TabIndex==3?"zttooltitle":"zttooltitleun" %>"
            style="padding-top: 5px;">
            <a href="/FinanceManage/AccountPayabled.aspx" rel="FinancialManageTab">已付账款</a>
        </td>
        <td width="182" align="center" style="padding-left: 10px; margin-top: 0px;">
            <a href="#" id="addFinacial" style="cursor: hand">
                <img src="<%=ImageServerUrl %>/images/<%= TabIndex<2?"xz.gif":"xz001.gif" %>"
                     height="23" border="0" /></a>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ztlistsearch">
                <tr>
                    <td width="24" valign="top">
                        <img src="<%=ImageServerUrl %>/images/searchico2.gif" width="23" height="24" />
                    </td>
                    <td align="left">
                        团号
                        <input name="textfield" class="shurukuang" id="<%=this.ClientID %>_TourNo" value="<%=Request.QueryString["TourNo"] %>"
                            type="text" size="15" />
                        线路名称
                        <input name="textfield2" class="shurukuang" id="<%=this.ClientID %>_RouteName" value="<%=Request.QueryString["RouteName"] %>"
                            type="text" size="15" />
                        <%= TabIndex< 2 ? "下单人" : "供应商" %>
                        <input name="textfield2" class="shurukuang" id="<%=this.ClientID %>_OperatorName"
                            value="<%=Request.QueryString["OperatorName"] %>" type="text" style="width: 90px;" />
                        <%= TabIndex < 2 ? "组团社名称" : "供应商类型"%>
                        <input name="textfield2" class="shurukuang" id="<%=this.ClientID %>_TourCompanyName"
                            value="<%=Request.QueryString["TourCompanyName"] %>" type="text" size="15" />
                        出团时间
                        <input name="textfield2" class="shurukuang" onfocus="WdatePicker()" id="<%=this.ClientID %>_StartDate"
                            value="<%=Request.QueryString["StartDate"] %>" type="text" style="width: 70px;" />
                        至
                        <input name="textfield2" class="shurukuang" onfocus="WdatePicker()" id="<%=this.ClientID %>_EndDate"
                            value="<%=Request.QueryString["EndDate"] %>" type="text" style="width: 70px;" />
                        <input type="image" id="<%=this.ClientID %>btnSearch" value="提交" src="<%=ImageServerUrl %>/images/chaxun.gif"
                            style="width: 62px; height: 21px; border: none; margin-bottom: -3px;" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
    var <%=this.ClientID %>={
        TourNo:"",
        RouteName:"",
        OperatorName:"",
        TourCompanyName:"",
        StartDate:"",
        EndDate:"",
        getUrl:function(){
            this.TourNo=$("#<%=this.ClientID %>_TourNo").val();
            this.RouteName=$("#<%=this.ClientID %>_RouteName").val();
            this.OperatorName=$("#<%=this.ClientID %>_OperatorName").val();
            this.TourCompanyName=$("#<%=this.ClientID %>_TourCompanyName").val();
            this.StartDate=$("#<%=this.ClientID %>_StartDate").val();
            this.EndDate=$("#<%=this.ClientID %>_EndDate").val();
            var _href='/FinanceManage/<%= TabIndex<2?(TabIndex>0?"AccountsReceived.aspx":"AccountsReceivable.aspx"):(TabIndex==2?"AccountPayable.aspx":"AccountPayabled.aspx") %>';
            _href=encodeURI(_href+"?TourNo="+ this.TourNo+"&RouteName="+this.RouteName+"&OperatorName="+this.OperatorName+"&TourCompanyName="+this.TourCompanyName+"&StartDate="+this.StartDate+"&EndDate="+this.EndDate);
            return _href;
        },
        addFinacialDialog:function(){
            var isGrant="<%=IsGrant %>";
            Boxy.iframeDialog({ title: '<%= TabIndex < 2 ? "新增收款" : "新增付款"%>', iframeUrl: '/FinanceManage/AddAccount.aspx<%= TabIndex < 2 ?"":"?AddType=pay"%>', width: 700, height: 425, draggable: true, data: {} });                           
        }
    };
    $(function(){
        $("#tbl_<%=this.ClientID %> a[rel='FinancialManageTab']").click(function(){
                topTab.url(topTab.activeTabIndex,$(this).attr("href"));
                return false;
        });
        $("#<%=this.ClientID %>btnSearch").click(function(){
            var _href=<%=this.ClientID %>.getUrl();            
            topTab.url(topTab.activeTabIndex,_href);
            return false;
        });  
        $("#addFinacial").click(function(){
            <%=this.ClientID %>.addFinacialDialog();
            return false;
        });                  
    });
</script>

