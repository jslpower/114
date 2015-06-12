<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PictureUpdate.aspx.cs" Inherits="SiteOperationsCenter.NewsCenterControl.PictureUpdate" %>
<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="../usercontrol/SingleFileUpload.ascx" TagName="SingleFileUpload"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>图片维护</title>    
    <link href="<%=CssManage.GetCssFilePath("manager") %>" rel="stylesheet" type="text/css" />
    <link href="<%=CssManage.GetCssFilePath("swfupload") %>" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("swfupload") %>"></script>
    <style>
    #jiaodianList{ list-style:none; padding-left:0}
    #jiaodianList li input{
            width: 450px;
        }
        #inpNewsHref
        {
            width: 450px;
        }
        #inpTourHref
        {
            width: 450px;
        }
        #inpTourTitle
        {
            width: 450px;
        }
        #inpSameHref
        {
            width: 450px;
        }
        #inpSameTitle
        {
            width: 450px;
        }
        #inpSameHrefRight
        {
            width: 450px;
        }
        #inpCommunityHref
        {
            width: 450px;
        }
        #inpHrefMiddleLeft
        {
            width: 450px;
        }
        #intTitleMiddleLeft
        {
            width: 450px;
        }
        #inphrefMiddleRight
        {
            width: 450px;
        }
        #inpTitleMiddleRight
        {
            width: 450px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
     <table width="960" border="1" cellspacing="0" cellpadding="8" style="border:1px solid #ccc; padding:1px;">   
    <tr> 
      <td width="200" bgcolor="#C0DEF3" style="background:#C0DEF3; height:28px; text-align:right; font-weight:bold;">
          五张焦点图片切换<br>（建议图片大小310pxi*310pxi）</td>
      <td width="760" style="background:#fff; height:28px; text-align:left;">
      <!--如果上传的和手工输入的URL都有数值，可以默认为URL的-->
      <ul id="jiaodianList">
      
      <li>1.图片:
               <uc1:SingleFileUpload ID="SFFocusOne" runat="server" ImageWidth="310" ImageHeight="310" /><%=img_Pathone %>
               <span id="errMsg_hid" class="errmsg"></span>
               <input type="hidden" runat="server" id="hdfFocusOne" name="hdfFocusOne" />                
                                            
              <br />
              &nbsp;&nbsp;&nbsp;链接:        
              <input ID="InpFocusHrefOne" runat="server" MaxLength="250" value="http://www." />             
              <br />
              &nbsp;&nbsp;&nbsp;标题:        
              <input ID="InpFocusTitleOne" runat="server" MaxLength="250" />             
              <br />
       </li>
       
       <hr size='1'>
       
       
       <li>2.图片:
               <uc1:SingleFileUpload ID="SFFocusTwo" runat="server" ImageWidth="310" ImageHeight="310" /><%=img_Pathtwo %>
               <span id="Span1" class="errmsg"></span>
               <input type="hidden" runat="server" id="hdfFocusTwo" name="hdfFocusTwo" />            
               <br />
               &nbsp;&nbsp;&nbsp;链接:  
               <input ID="inpFocusHrefTwo" runat="server" MaxLength="250" value="http://www." />      
              
               <br />
               &nbsp;&nbsp;&nbsp;标题:    
                <input ID="inpFocusTitleTwo" runat="server" MaxLength="250" />    
             
               <br />
       </li>
       
       <hr size='1'>
       
       
       <li>3.图片:
               <uc1:SingleFileUpload ID="SFFocusShree" runat="server" ImageWidth="310" ImageHeight="310" /><%=img_PathShree %>
               <span id="Span2" class="errmsg"></span>
               <input type="hidden" runat="server" id="hdfFocusShree" name="hdfFocusShree" />
               
              <br />
               &nbsp;&nbsp;&nbsp;链接:        
               <input ID="inpFocusHrefShree" runat="server" MaxLength="250" value="http://www." />     
               <br />
               &nbsp;&nbsp;&nbsp;标题:        
              
               <input ID="inpFocusTitleShree" runat="server" MaxLength="250" />     
           <br>
       </li>
       
       <hr size='1'>
       
       <li>4.图片:
               <uc1:SingleFileUpload ID="SFFocusFour" runat="server" ImageWidth="310" ImageHeight="310" /><%=img_PathFour %>
               <span id="Span3" class="errmsg"></span>
               <input type="hidden" runat="server" id="hdfFocusFour" name="hdfFocusFour" />
               
	         <br />
               &nbsp;&nbsp;&nbsp;链接:        
             
               <input ID="inpFocusHrefFour" runat="server" MaxLength="250" value="http://www." />     
               <br />
               &nbsp;&nbsp;&nbsp;标题:        
               <input ID="inpFocusTitleFour" runat="server" MaxLength="250" />     
           <br>
       </li>
       
       <hr size='1'>
       
       <li>5.图片:
               <uc1:SingleFileUpload ID="SFFocusFive" runat="server" ImageWidth="310" ImageHeight="310" /><%=img_PathFive %>
               <span id="Span4" class="errmsg"></span>
               <input type="hidden" runat="server" id="hdfFocusFive" name="hdfFocusFive" />
               
<br />
               &nbsp;&nbsp;&nbsp;链接:
               <input ID="inpFocusHrefFive" runat="server" MaxLength="250" value="http://www." />     
               <br />
               &nbsp;&nbsp;&nbsp;标题:        
               <input ID="inpFocusTitleFive" runat="server" MaxLength="250" />     
               <br />
       </li>
       
       </ul>
      </td>
    </tr>
    
    <tr>
      <td height="26" align="right" bgcolor="#C0DEF3"><strong>焦点新闻右侧公告图片<br />
          （249*176pxi）</strong></td>
      <td height="26">图片：
             <uc1:SingleFileUpload ID="SFNews" runat="server" ImageWidth="249" ImageHeight="176" /><%=img_PathNews %>
             <span id="Span5" class="errmsg"></span>
             <input type="hidden" runat="server" id="hdfNews" name="hdfNews" />
               
            <br />
          连接： 
             <input ID="inpNewsHref" runat="server" MaxLength="250" value="http://www." />            
      </td>
  </tr>
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><p><strong>旅游资讯焦点图片<br />
          （311*202pxi）</strong></p></td>
      <td height="28">
        图片： 
            <uc1:SingleFileUpload ID="SFTour" runat="server" ImageWidth="311" ImageHeight="202" />
              <%=img_PathTour%>
            <span id="Span6" class="errmsg"></span>
            <input type="hidden" runat="server" id="hdfTour" name="hdfTour" />
            
          
        <br />
              连接：
            <input ID="inpTourHref" runat="server" MaxLength="250" value="http://www." />
            
            <br />
              标题： 
            <input ID="inpTourTitle" runat="server" MaxLength="250" />
           
            <br />
        </p>
       </td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><p><strong>同业学堂焦点图片<br />
          （311*202pxi）</strong></p></td>
      <td height="28">
          图片：
            <uc1:SingleFileUpload ID="SFSame" runat="server" ImageWidth="311" ImageHeight="202" /><%=img_PathSame%>
            <span id="Span7" class="errmsg"></span>
            <input type="hidden" runat="server" id="hdfSame" name="hdfSame" />
            
            <br />
          连接：
            <input ID="inpSameHref" runat="server" MaxLength="250" value="http://www." />
           
            <br />
          标题： 
            <input ID="inpSameTitle" runat="server" MaxLength="250" />
           
        </td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><strong>同业学堂右侧广告<br />
          （250*80pxi）</strong></td>
      <td height="28">
          图片：
            <uc1:SingleFileUpload ID="SFSameRight" runat="server" ImageWidth="250" ImageHeight="80" /><%=img_PathSameRight%>
            <span id="Span8" class="errmsg"></span>
            <input type="hidden" runat="server" id="hdfSameRight" name="hdfSameRight" />
            
           
            <br />
          连接： 
            <input ID="inpSameHrefRight" runat="server" MaxLength="250" value="http://www." />
            
        </td>
    </tr>
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><strong>同行社区（311*80pxi）</strong></td>
      <td height="28">
          图片：
            <uc1:SingleFileUpload ID="SFCommunity" runat="server" ImageWidth="311" ImageHeight="80" /><%=img_PathCommunity%>
            <span id="Span9" class="errmsg"></span>
            <input type="hidden" runat="server" id="hdfCommunity" name="hdfCommunity" />           
          
            <br />
          连接： 
            <input ID="inpCommunityHref" runat="server" MaxLength="250" value="http://www." />
           
      </td>
    </tr>     
    
    
      <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><p><strong>同业社区中间(左)（311*202pxi）</strong></p></td>
      <td height="28">
        <p>图片： 
            <uc1:SingleFileUpload ID="SFMiddleLeft" runat="server" ImageWidth="311" ImageHeight="202" />
          <p>
              <%=img_PathMiddleLeft%>
            <span id="Span12" class="errmsg"></span>
            <input type="hidden" runat="server" id="hidMiddleLeft" name="hidMiddleLeft" />
            
          
        <br />
              连接：
            <input ID="inpHrefMiddleLeft" runat="server" MaxLength="250" value="http://www." />
            
            <br />
              标题： 
            <input ID="intTitleMiddleLeft" runat="server" MaxLength="250" />
           
            <br />
        </p>
       </td>
    </tr>
   
    
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3"><p><strong>同业社区中间(右)（311*202pxi）</strong></p></td>
      <td height="28">
        <p>图片： 
            <uc1:SingleFileUpload ID="SFMiddleRight" runat="server" ImageWidth="311" ImageHeight="202" />
          <p>
              <%=img_PathMiddleRight%>
            <span id="Span10" class="errmsg"></span>
            <input type="hidden" runat="server" id="HidMiddleRight" name="HidMiddleRight" />           
          
            <br />
              连接：
            <input ID="inphrefMiddleRight" runat="server" MaxLength="250" value="http://www." />
            
            <br />
              标题： 
            <input ID="inpTitleMiddleRight" runat="server" MaxLength="250" />
           
            <br />
        </p>
       </td>
    </tr>    
    
    <tr>
      <td height="28" align="right" bgcolor="#C0DEF3">&nbsp;</td>
      <td height="28">
            <asp:Button ID="btnSave" runat="server" Text="保存" onclick="btnSave_Click" />
</td>
    </tr>
</table>

     <script type="text/javascript" src="<%=JsManage.GetJsFilePath("jquery") %>"></script>

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("validatorform") %>"></script>

    <link href="<%=CssManage.GetCssFilePath("boxy") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=JsManage.GetJsFilePath("boxy") %>"></script>
    
    <script type="text/javascript">         
        
        var strSFFocusOne, strSFFocusTwo, strSFFocusShree,strSFFocusFour,strSFFocusFive,strSFNews,strSFTour,strSFSame,strSFSameRight,strSFCommunity,strSFMiddleLeft,strSFMiddleRight;
        var isSubmit=true;
        $(function() {
             strSFFocusOne = <%=SFFocusOne.ClientID %>;
             strSFFocusTwo = <%=SFFocusTwo.ClientID %>;
             strSFFocusShree = <%=SFFocusShree.ClientID %>;             
             strSFFocusFour = <%=SFFocusFour.ClientID %>;
             strSFFocusFive = <%=SFFocusFive.ClientID %>;             
             strSFNews = <%=SFNews.ClientID %>;
             strSFTour = <%=SFTour.ClientID %>;
             strSFSame = <%=SFSame.ClientID %>;
             strSFSameRight = <%=SFSameRight.ClientID %>;
             strSFCommunity = <%=SFCommunity.ClientID %>;
             strSFMiddleLeft = <%=SFMiddleLeft.ClientID %>;
             strSFMiddleRight = <%=SFMiddleRight.ClientID %>;
             SWFUpload.WINDOW_MODE = "TRANSPARENT";
             
             

            FV_onBlur.initValid($("#<%=btnSave.ClientID %>").closest("form").get(0));
            $("#<%=btnSave.ClientID %>").click(function() {
                var isPass = true;
                var form = $(this).closest("form").get(0);                  
                if (isPass) {
                    $("input[type='checkbox'][name='ckCompanyType']").attr("disabled", "");
                    if(strSFFocusOne.getStats().files_queued ==0
                    &&strSFFocusTwo.getStats().files_queued ==0
                    &&strSFFocusShree.getStats().files_queued ==0
                    &&strSFFocusFour.getStats().files_queued==0
                    &&strSFFocusFive.getStats().files_queued==0
                    &&strSFNews.getStats().files_queued==0
                    &&strSFTour.getStats().files_queued==0
                    &&strSFSame.getStats().files_queued==0
                    &&strSFSameRight.getStats().files_queued==0
                    &&strSFCommunity.getStats().files_queued==0 
                    &&strSFMiddleLeft.getStats().files_queued==0
                    &&strSFMiddleRight.getStats().files_queued==0){
                        return true;
                    }
                    UploadLicenceImg();
                    return false;
                }else{
                    return false;
                }
            });
        });
        
       function UploadLicenceImg() {
            if (strSFFocusOne.getStats().files_queued > 0) {
                strSFFocusOne.customSettings.UploadSucessCallback = UploadBusinessSFFocusTwo;
                strSFFocusOne.startUpload();
            } else {
                UploadBusinessSFFocusTwo();
            }
        }
        function UploadBusinessSFFocusTwo() {
            if (strSFFocusTwo.getStats().files_queued > 0) {
                strSFFocusTwo.customSettings.UploadSucessCallback = UploadTaxRegSFFocusShree;
                strSFFocusTwo.startUpload();
            } else {
                UploadTaxRegSFFocusShree();
            }
        }        
        
         function UploadTaxRegSFFocusShree() {
            if (strSFFocusShree.getStats().files_queued > 0) {
                strSFFocusShree.customSettings.UploadSucessCallback = UploadTaxRegSFFocusFour;
                strSFFocusShree.startUpload();
            } else {
                UploadTaxRegSFFocusFour();
            }
        }        
        
         function UploadTaxRegSFFocusFour() {
            if (strSFFocusFour.getStats().files_queued > 0) {
                strSFFocusFour.customSettings.UploadSucessCallback = UploadBusinessSFFocusFive;
                strSFFocusFour.startUpload();
            } else {
                UploadBusinessSFFocusFive();
            }
        }
        
        
         function UploadBusinessSFFocusFive() {
            if (strSFFocusFive.getStats().files_queued > 0) {
                strSFFocusFive.customSettings.UploadSucessCallback = UploadBusinessSFSFNews;
                strSFFocusFive.startUpload();
            } else {
                UploadBusinessSFSFNews();
            }
        }
        
         function UploadBusinessSFSFNews() {
            if (strSFNews.getStats().files_queued > 0) {
                strSFNews.customSettings.UploadSucessCallback = UploadBusinessSFSFTour;
                strSFNews.startUpload();
            } else {
                UploadBusinessSFSFTour();
            }
        }
        
         function UploadBusinessSFSFTour() {
            if (strSFTour.getStats().files_queued > 0) {
                strSFTour.customSettings.UploadSucessCallback = UploadBusinessSFSFSame;
                strSFTour.startUpload();
            } else {
                UploadBusinessSFSFSame();
            }
        }
        
         function UploadBusinessSFSFSame() {
            if (strSFSame.getStats().files_queued > 0) {
                strSFSame.customSettings.UploadSucessCallback = UploadBusinessSFSameRight;
                strSFSame.startUpload();
            } else {
                UploadBusinessSFSameRight();
            }
        }
        
         function UploadBusinessSFSameRight() {
            if (strSFSameRight.getStats().files_queued > 0) {
                strSFSameRight.customSettings.UploadSucessCallback = UploadBusinessSFCommunity;
                strSFSameRight.startUpload();
            } else {
                UploadBusinessSFCommunity();
            }
        }
        
          function UploadBusinessSFCommunity() {
            if (strSFCommunity.getStats().files_queued > 0) {
                strSFCommunity.customSettings.UploadSucessCallback = UploadBusinessSFMiddleLeft;
                strSFCommunity.startUpload();
            } else {
                UploadBusinessSFMiddleLeft();
            }
        }     
        
         function UploadBusinessSFMiddleLeft() {
            if (strSFMiddleLeft.getStats().files_queued > 0) {
                strSFMiddleLeft.customSettings.UploadSucessCallback = UploadBusinessSFMiddleRight;
                strSFMiddleLeft.startUpload();
            } else {
                UploadBusinessSFMiddleRight();
            }
        }
        
         function UploadBusinessSFMiddleRight() {
            if (strSFMiddleRight.getStats().files_queued > 0) {
                strSFMiddleRight.customSettings.UploadSucessCallback = UploadSave;
                strSFMiddleRight.startUpload();
            } else {
                UploadSave();
            }
        }

        function UploadSave() {
           isSubmit=true;
           $("#<%=btnSave.ClientID %>").click();
        }
   
    </script>
    
    </form>   
    

    
</body>
</html>
