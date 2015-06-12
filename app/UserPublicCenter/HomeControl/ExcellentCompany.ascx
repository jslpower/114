<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExcellentCompany.ascx.cs"
    Inherits="UserPublicCenter.HomeControl.ExcellentCompany" %>
<div class="tjcomlogobar">
    <span>优秀企业展示</span></div>
    <div class="comlogolist" id="divCompanyList"></div>
    
    <textarea id="textAreaCompanyList" style="display:none">
    <%= strAllCompany%>
    </textarea>

<script type="text/javascript">
    
    var divTop = $("#divCompanyList").position().top;
    var clearInt = null;
    
    clearInt = setInterval(function() {
        var divscrollTop = GetWindowClient().h + $(document).scrollTop();
        if (divscrollTop >= divTop) {
            if ($("#divCompanyList").html() == "") {
                var v = $("#textAreaCompanyList").html();
                if(v.indexOf("<")==-1){
                    $("#divCompanyList").html(htmlDecode(v));
                }else{
                    $("#divCompanyList").html(v);
                }
                clearInterval(clearInt);
            }
        }
    }, 500);
       
   
    function htmlDecode(text) {
        var oDiv = document.createElement("div");
        oDiv.innerHTML = text;
        var output = oDiv.innerText || oDiv.textContent;
        oDiv = null;
        return output;
    }

</script>