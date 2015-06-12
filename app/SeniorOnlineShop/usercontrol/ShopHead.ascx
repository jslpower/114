<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopHead.ascx.cs" Inherits="SeniorOnlineShop.usercontrol.ShopHead" %>
<div class="topda">
    <%=strMessage %>
    <li class="daright"><a target="_blank" href="<%=tongyeurl%>">首页</a> | <a target="_blank" href="<%=addtongurl %>">加入同业114</a> | <a href="http://im.tongye114.com" target="_blank">
        免费下载同业MQ</a> | <a href="<%=idealurl %>" target="_blank">提建议</a> | <a href="<%=helpurl %>" target="_blank">帮助中心</a> | <a href="javascript:void(0)" onclick="ShopHead.setHome(location.href)">设为首页</a>
        | <a  href="javascript:void(0)" onclick="ShopHead.add_collect('同业114',location.href)">加入收藏</a></li>
</div>
<script type="text/javascript">
//添加到收藏夹 add collect (firefox、IE通用 )
var ShopHead=
{
    add_collect:function(title,url) 
    { 
        if (window.sidebar) { 
            window.sidebar.addPanel(title,url,""); 
        }else if( document.all ) { 
            window.external.AddFavorite(url,title); 
        }else if( window.opera && window.print ) { 
            return true; 
        } 
    },
// 设为主页 set homepage (firefox、IE通用 )
    setHome:function (url) 
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
}
</script>