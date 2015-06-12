<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebHeader.ascx.cs" Inherits="UserBackCenter.usercontrol.WebHeader" %>
<div id="top">
    <div class="topda">
        <li class="daleft">
            <%=LoginMsg %></li>
        <li class="daright"><a href="<%=userpubliccenter %>/Default.aspx" target="_blank">首页</a> | <a href="<%=userpubliccenter %>/Register/CompanyUserRegister.aspx" target="_blank">加入同业114</a>
            | <a href="http://im.tongye114.com/" target="_blank">免费下载同业MQ</a> | <a href="<%= EyouSoft.Common.Utils.GetUrlByProposal(4,0) %>" target="_blank">提建议</a> | <a href="<%= EyouSoft.Common.Utils.HelpCenterUrl %>" target="_blank">帮助中心</a> | <a href="javascript:void(0)" onclick="setHome(location.href)">设为首页</a> | <a href="javascript:void(0)" onclick="add_collect('同业114',location.href)">加入收藏</a></li>
    </div>
</div>

<script type="text/javascript">
//添加到收藏夹 add collect (firefox、IE通用 )
function add_collect(title,url) { 
if (window.sidebar) { 
window.sidebar.addPanel(title,url,""); 
}else if( document.all ) { 
window.external.AddFavorite(url,title); 
}else if( window.opera && window.print ) { 
return true; 
} 
}


// 设为主页 set homepage (firefox、IE通用 )
function setHome(url) 
{ 
if (document.all){ //ie
document.body.style.behavior='url(#default#homepage)'; 
document.body.setHomePage(url); 
}else if (window.sidebar){ //firefox
if(window.netscape){ 
try{ 
netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); 
}catch (e){ 
alert( "该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true" ); 
} 
} 
if(window.confirm("你确定要设置"+url+"为首页吗？")==1){ 
var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch); 
prefs.setCharPref('browser.startup.homepage',url); 
} 
} 
}

</script>