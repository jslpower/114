<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdveControl.ascx.cs"
    Inherits="UserPublicCenter.WebControl.AdveControl" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
    <tr>
        <td>
            <%=imgUrl1%>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #FEC698;
    margin-bottom: 10px; background: url(<%=ImageServerPath %>/images/UserPublicCenter/gqhuodongbg.gif) repeat-x bottom;">
    <tr>
        <td>
            <div id="Qhmain_comm1">
                <div class="bar_on_comm1l">
                </div>
                <div class="bar_on_comm2" id="you_link_1" onmouseover="chgIndexDiv2(1,2,'you_link_','you_div_','bar_on_comm2','bar_un_comm2')">
                    <a href="javascript:void(0)">本周新闻排行</a>
                </div>
                <div class="bar_un_comm2" id="you_link_2" onmouseover="chgIndexDiv2(2,2,'you_link_','you_div_','bar_on_comm2','bar_un_comm2')">
                    <a href="javascript:void(0)">最新新闻资讯</a>
                </div>
                <div class="bar_on_comm2r">
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding: 5px; height: 250px;" valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="you_div_1">
                <tr>
                    <td>
                        <cc1:CustomRepeater ID="rpt_NewsTopList" runat="server">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="xuetang3">
                                            <ul>
                                                <%#BindOrderNew(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "ArticleTitle")), 0)%>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="you_div_2" style="display: none;">
                <tr>
                    <td >
                        <cc1:CustomRepeater ID="rpt_NewNewsInfo" runat="server">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="xuetang3" >
                                            <ul>
                                                <%#BindOrderNew(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "ArticleTitle")), 1)%>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 10px;">
    <tr>
        <td>
            <%=imgUrl2%>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td class="hangleft">
            <strong>本周最具人气企业推荐</strong>
        </td>
    </tr>
    <tr>
        <td class="hanglk">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="33%" align="center">
                        <asp:DataList ID="dal_PicAdvList" runat="server" BorderWidth="0px" CellPadding="0"
                            CellSpacing="0" HorizontalAlign="Center" ItemStyle-VerticalAlign="Top" RepeatColumns="2"
                            RepeatDirection="Horizontal" Width="98%">
                            <ItemTemplate>
                                <div class="jings">
                                    <%# ShowPicAdvInfo(DataBinder.Eval(Container.DataItem, "RedirectURL").ToString(), DataBinder.Eval(Container.DataItem, "ImgPath").ToString())%>
                                </div>
                                <div class="jingx">
                                    <%# ShowTitleAdvInfo(DataBinder.Eval(Container.DataItem, "Title").ToString(), DataBinder.Eval(Container.DataItem, "RedirectURL").ToString())%>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0" style="border: 1px solid #C4C4C4;
    margin-top: 10px;">
    <tr>
        <td>
            <div id="Qhmain_comm1">
                <div class="bar_on_comm3l">
                </div>
                <div class="bar_on_comm3" id="xue_link_1" onmouseover="chgIndexDiv2(1,3,'xue_link_','xue_div_','bar_on_comm3','bar_un_comm3')">
                    <a href="javascript:void(0)">新手计调</a>
                </div>
                <div class="bar_un_comm3" id="xue_link_2" onmouseover="chgIndexDiv2(2,3,'xue_link_','xue_div_','bar_on_comm3','bar_un_comm3')">
                    <a href="javascript:void(0)">带团分享</a>
                </div>
                <div class="bar_un_comm3" id="xue_link_3" onmouseover="chgIndexDiv2(3,3,'xue_link_','xue_div_','bar_on_comm3','bar_un_comm3')">
                    <a href="javascript:void(0)">案例分析</a>
                </div>
                <div class="bar_on_comm3r">
                </div>
            </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="padding: 5px; height: 245px;" valign="top">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="xue_div_1">
                <tr>
                    <td>
                        <cc1:CustomRepeater ID="rep_NewEnjoyStudy" runat="server">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="xuetang3">
                                            <ul>
                                                <%#BindOrderNew(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "ArticleTitle")), 1)%>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="xue_div_2" style="display: none;">
                <tr>
                    <td>
                        <cc1:CustomRepeater ID="rep_TourEnjoy" runat="server">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="xuetang3">
                                            <ul>
                                                <%#BindOrderNew(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "ArticleTitle")), 1)%>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="xue_div_3" style="display: none;">
                <tr>
                    <td>
                        <cc1:CustomRepeater ID="rep_AnLiFenXi" runat="server">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="xuetang3">
                                            <ul>
                                                <%#BindOrderNew(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")), Convert.ToString(DataBinder.Eval(Container.DataItem, "ArticleTitle")), 1)%>
                                            </ul>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </cc1:CustomRepeater>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

<script type="text/javascript">
function chgIndexDiv2(num,total,linkName,divName,class1,class2){
	if($("#"+linkName+num).attr("class")==class1){
		return;
	}else{
		for(i=1;i<total+1;i++){
			if($("#"+linkName+i).attr("class")==class1){
				$("#"+linkName+i).attr("class",class2);
				$("#"+divName+i).get(0).style.display='none';
				$("#"+linkName+num).attr("class",class1);
				$("#"+divName+num).get(0).style.display='';
				break;
			}
		}
	}
}
</script>

