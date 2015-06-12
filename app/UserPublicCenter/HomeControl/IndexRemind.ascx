<%@ OutputCache Duration="1800" VaryByParam="None" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IndexRemind.ascx.cs" Inherits="UserPublicCenter.HomeControl.IndexRemind" %>
<%@ Import Namespace="EyouSoft.Common" %>         
<div class="smzhuce">
                    <ul class="login-info" style="overflow: hidden; height: 77px;">
                    <asp:Repeater  ID="rptInfo" runat="server">
                   <ItemTemplate>
                  <li style="margin-top: 0px;"><%# Convert.ToDateTime(Eval("EventTime")).ToString("HH:mm")%> <span class="wenzilanse"><%# ((int)Eval("TilteType")) != 10 ? Utils.GetText((string)Eval("Operator"), 5) : Utils.GetText((string)Eval("Operator"), 7)%></span><%# Eval("TypeLink")%></li>
                </ItemTemplate>
                </asp:Repeater>
                        </ul>

                    <script type="text/javascript">

$(function(){
$(".smzhuce").find("a").each(function(){
     $(this).attr("href","javascript:void(0)");
     $(this).attr("target","_self");
});
   
	//多行应用@Mr.Think
	var _wrap=$('ul.login-info');//定义滚动区域
	var _interval=3000;//定义滚动间隙时间
	var _moving;//需要清除的动画
	_wrap.hover(function(){
		clearInterval(_moving);//当鼠标在滚动区域中时,停止滚动
	},function(){
		_moving=setInterval(function(){
			var _field=_wrap.find('li:first');//此变量不可放置于函数起始处,li:first取值是变化的
			var _h=_field.height();//取得每次滚动高度
			_field.animate({marginTop:-_h+'px'},600,function(){//通过取负margin值,隐藏第一行
				_field.css('marginTop',0).appendTo(_wrap);//隐藏后,将该行的margin值置零,并插入到最后,实现无缝滚动
			})
		},_interval)//滚动间隔时间取决于_interval
	}).trigger('mouseleave');//函数载入时,模拟执行mouseleave,即自动滚动
});
                    </script>

                </div>
