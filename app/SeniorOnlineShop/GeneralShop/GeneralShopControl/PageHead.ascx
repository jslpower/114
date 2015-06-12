<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PageHead.ascx.cs" Inherits="SeniorOnlineShop.GeneralShop.GeneralShopControl.PageHead" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Common.URLREWRITE" %>

<div id="site-nav">
    	<div id="site-nav-bd">
        	<p class="login-info"><%=strMessage %></p>
            <ul class="quick-menu">
                <li><a target="_blank" href="http://www.enowinfo.com/News" title="易诺动态">易诺动态</a><span>|</span></li>
                <li><a target="_blank" href="http://www.enowinfo.com/Try" title="免费试用旅行社管理软件">免费试用旅行社管理软件</a><span>|</span></li>
            	<li class="Download-MQ"><a href="http://im.tongye114.com" target="_blank" title="同业MQ免费下载">免费下载同业MQ</a><span>|</span></li>
                <li><a href="<%= UrlByProposal %>" target="_blank">提建议</a><span>|</span></li>
                <li><a href="<%= EyouSoft.Common.Utils.HelpCenterUrl  %>">帮助中心</a><span>|</span></li>
                <li><a href="javascript:void(0)" onclick="setHome(location.href)">设为首页</a><span>|</span></li>
                <li><a href="javascript:void(0)" onclick="add_collect('同业114',location.href)">加入收藏 </a></li>
            </ul>
        </div>
    </div>

<script type="text/javascript">
    //添加到收藏夹
    function add_collect(title, url) {
        if (window.sidebar) {
            window.sidebar.addPanel(title, url, "");
        } else if (document.all) {
            window.external.AddFavorite(url, title);
        } else if (window.opera && window.print) {
            return true;
        }
    }
    //设为首页 
    function setHome(url) {
        if (document.all) { //ie
            document.body.style.behavior = 'url(#default#homepage)';
            document.body.setHomePage(url);
        } else if (window.sidebar) { //firefox
            if (window.netscape) {
                try {
                    netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
                } catch (e) {
                    alert("该操作被浏览器拒绝，如果想启用该功能，请在地址栏内输入 about:config,然后将项 signed.applets.codebase_principal_support 值该为true");
                }
            }
            if (window.confirm("你确定要设置" + url + "为首页吗？") == 1) {
                var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
                prefs.setCharPref('browser.startup.homepage', url);
            }
        }
    }
</script>

