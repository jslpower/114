<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScenicBeautiesDetail.aspx.cs"
    Inherits="SeniorOnlineShop.scenicspots.T1.ScenicBeautiesDetail" MasterPageFile="/master/ScenicSpotsT1.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="/scenicspots/usercontrol/ChildMenu.ascx" TagName="ChildMenu" TagPrefix="uc1" %>
<asp:Content runat="server" ID="HeadPlaceHolder" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content runat="server" ID="MainPlaceHolder" ContentPlaceHolderID="MainPlaceHolder">
    <style type="text/css">
        .bigpic
        {
            display: table-cell;
            vertical-align: middle;
            text-align: center; *display:block;*font-size:377px;*font-family:Arial;width:679px;height:454px;}
        .bigpic img
        {
            vertical-align: middle;
        }
    </style>
    <div class="sidebar02 sidebar02Scenic">
        <uc1:ChildMenu ID="ChildMenu1" runat="server" />
        <div class="content content02">
            <div class="turn_up" id="div_last" style="cursor: pointer; text-align: center;">
                <a href="javascript:void(0)" style="width: 100%; color: #194c07; font-weight: bold;">
                    上一张</a></div>
            <div class="turn_down" id="div_next" style="cursor: pointer; text-align: center;">
                <a href="javascript:void(0)" style="width: 100%; color: #194c07; font-weight: bold;">
                    下一张</a></div>
            <div class="bigpic" style="height: 454px; width: 679px; text-align: center; display: table-cell;
                vertical-align: middle;">
                <a href="<%=CurrImagePath %>" title="点击查看原图" target="_blank">
                    <img width="679" height="454" id="bigScenicPic" src="<%=CurrImagePath %>" style="vertical-align: middle;" /></a>
                <div class="clearboth">
                </div>
            </div>
        </div>
        <div class="flashpic">
            <table style="width: 100%;">
                <tr>
                    <td colspan="3" style="color: Red; height: 20px;" id="showErrMsg">
                    </td>
                </tr>
                <tr>
                    <td valign="bottom" style="width: 22px; height: 94px; padding-bottom: 15px;">
                        <a href="javascript:void(0)" id="a_FourPicLast">
                            <img src="<%=ImageServerPath %>/scenicspots/T1/images/lefticon.gif" />
                        </a>
                    </td>
                    <td style="width: 630px;">
                        &nbsp;
                        <ul id="imgPicButtons" style="padding: 0px;">
                            <li>
                                <div style="cursor: pointer;">
                                    <img width="123" height="82" src="" /></div>
                            </li>
                            <li>
                                <div style="cursor: pointer;">
                                    <img width="123" height="82" src="" /></div>
                            </li>
                            <li>
                                <div style="cursor: pointer;">
                                    <img width="123" height="82" src="" /></div>
                            </li>
                            <li>
                                <div style="cursor: pointer;">
                                    <img width="123" height="82" src="" /></div>
                            </li>
                            <div class="clearboth">
                            </div>
                        </ul>
                    </td>
                    <td valign="bottom" style="width: 22px; padding-bottom: 15px;">
                        <a href="javascript:void(0)" id="a_FourPicNext">
                            <img src="<%=ImageServerPath %>/scenicspots/T1/images/righticon.gif" /></a>
                    </td>
                </tr>
            </table>
            <div class="clearboth">
            </div>
        </div>
        <div>
            &nbsp;
        </div>
    </div>

    <script language="javascript" type="text/javascript">
        var ScenicBeautiesDetail = {
            index: 1,     //横向索引基数
            currImgNum: "<%=index %>",
            currPartImages: eval("<%=AllImagePathString %>"),
            hasImages: ["", "", "", ""],
            QFord_getImageSize: function(FilePath) {
                var imgSize = { width: 0, height: 0 };
                var image = new Image();
                image.src = FilePath;
                imgSize.width = image.width;
                imgSize.height = image.height;
                image = null;
                return imgSize;
            },
            showImg: function() { //显示图片及相关提示信息
                var self = this;
                var imgUrl = "<%=EyouSoft.Common.Domain.FileSystem %>" + this.currPartImages[this.currImgNum - 1];
                //$("#bigScenicPic").css({ "background-image": "url(" + imgUrl + ")", "background-repeat": "no-repeat" });
                var imgSize = this.QFord_getImageSize(imgUrl);
                $("#bigScenicPic").attr("src", imgUrl);
                if (document.all) {
                    document.getElementById("bigScenicPic").onreadystatechange = function() {
                        if (this.readyState == "complete") {
                            $("#bigScenicPic").show(300);
                        }
                    }
                } else {
                    document.getElementById("bigScenicPic").onload = function() {
                        if (this.complete == true) {
                            $("#bigScenicPic").show(300);
                        }
                    }
                }
                if (imgSize.width >= 679 || imgSize.width == 0) {
                    $("#bigScenicPic").attr("width", 679);
                } else {
                    $("#bigScenicPic").removeAttr("width");
                }
                if (imgSize.height >= 454 || imgSize.height == 0) {
                    $("#bigScenicPic").attr("height", 454).parent().css({ "padding-bottom": "0px", "padding-top": "0px" });
                } else {
                    $("#bigScenicPic").removeAttr("height");
                }
                var onImgIndex = this.currImgNum - 1 - ((this.index - 1) * 4)
                $("#imgPicButtons li").each(function(i) {
                    if (i == onImgIndex) {
                        $(this).addClass("turn_on");
                    } else {
                        $(this).removeClass("turn_on");
                    }
                })
                if (this.currImgNum <= 1) {
                    $("#showErrMsg").html("已经是第一张景区美图了！")
                } else {
                    if (this.currImgNum >= this.currPartImages.length) {
                        $("#showErrMsg").html("已经是最后一张景区美图了！")
                    } else { $("#showErrMsg").html("&nbsp;") }
                }
            },
            getIndex: function() {  //计算横向索引值
                if ((this.currImgNum / 4) > parseInt(this.currImgNum / 4)) {
                    this.index = parseInt(this.currImgNum / 4) + 1;
                } else {
                    this.index = parseInt(this.currImgNum / 4);
                }
                this.gethasImages();
            },
            getCurrImgNum: function(isAdd) {//赋值当前图片在currPartImages中的index值
                if (isAdd) {
                    this.currImgNum++;
                } else {
                    this.currImgNum--;
                }
                var index = parseInt(this.currImgNum);
                var length = this.currPartImages.length;
                if (index >= 0 && index < length) {
                    this.currImgNum = isAdd ? Number(index) : (Number(index) < 1 ? 1 : Number(index));
                } else if (index < 1) {
                    this.currImgNum = 1;
                } else {
                    this.currImgNum = length;
                }
                this.getIndex();
            },
            gethasImages: function() { //获取横向btn图片集合信息
                var self = this;
                var begainNum = (this.index - 1) * 4;
                if (begainNum < this.currPartImages.length) {
                    this.hasImages[0] = this.currPartImages[begainNum];
                } else { this.hasImages[0] = "noimge" }
                if (Number(begainNum + 1) < this.currPartImages.length) {
                    this.hasImages[1] = this.currPartImages[Number(begainNum + 1)];
                } else { this.hasImages[1] = "noimge" }
                if (Number(begainNum + 2) < this.currPartImages.length) {
                    this.hasImages[2] = this.currPartImages[Number(begainNum + 2)];
                } else { this.hasImages[2] = "noimge" }
                if (Number(begainNum + 3) < this.currPartImages.length) {
                    this.hasImages[3] = this.currPartImages[Number(begainNum + 3)];
                } else { this.hasImages[3] = "noimge" }
                $("#imgPicButtons img").each(function(i) {
                    $(this).show();
                    if (self.hasImages[i] == "noimge") {//到最后时缺少图片时隐藏img标签
                        $(this).hide();
                    } else {
                        var imgUrl = "<%=EyouSoft.Common.Domain.FileSystem %>" + self.hasImages[i];
                        $(this).attr("src", imgUrl);
                    }
                });
            },
            initPage: function() {
                var self = this;
                self.getIndex();
                self.showImg();
                $("#div_last,#a_FourPicLast").click(function() {
                    self.getCurrImgNum(false);
                    self.showImg();
                    $(this).blur();
                })
                $("#div_next,#a_FourPicNext").click(function() {
                    self.getCurrImgNum(true);
                    self.showImg();
                    $(this).blur();
                })
                $("#imgPicButtons img").each(function(i) {
                    $(this).click(function() {
                        self.currImgNum = (self.index - 1) * 4 + i + 1;
                        self.showImg();
                    })
                })
            }
        }
        $(function() {
            ScenicBeautiesDetail.initPage();
        })
    </script>

</asp:Content>
