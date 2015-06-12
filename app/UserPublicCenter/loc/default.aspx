<%@ Page language="c#" %>
<%

string url = Request.QueryString["url"];
if(url!=string.Empty && url.IndexOf("?")!=0 )
	Response.Write("<script>location.href='" + url + "';</script>");
else
	Response.Write("<script>location.href='..';</script>");

%>