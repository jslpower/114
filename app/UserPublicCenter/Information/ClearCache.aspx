<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClearCache.aspx.cs" Inherits="UserPublicCenter.Information.ClearCache" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/C#" runat="server">
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] FilePaths = System.IO.Directory.GetFiles(Server.MapPath("/Information"));
            foreach (var item in FilePaths)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(item);
                file.CreationTime = DateTime.Now;
                divCache.InnerHtml += file.Name + "<br/>";
                file = null;          
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    以下页面缓存已经清除：
    <br />
    <div runat="server" id="divCache">
    
    </div>
    </form>
</body>
</html>
