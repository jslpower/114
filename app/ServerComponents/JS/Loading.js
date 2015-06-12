

var LoadingImg = {
    //-----显示正在加载 
    ShowLoading: function(divId) {
        var obj = document.getElementById(divId);
        if (obj != null) {
            obj.innerHTML = "<img id='_img_loading_" + divId + "' src='http://localhost:30001/images/loadingnew.gif' border='0' /><br />&nbsp;正在加载...&nbsp;";
        }
    },
    //判断是否允许将ajax调用的数据加载并添加到div中
    //编写人:zhangzy  编写时间:2009-5-22
    // //调用的前提是必须在之前有调用了ShowLoading方法
    IsLoadAddDataToDiv: function(divId) {
        var _isload = false;   //默认为不允许
        var imgLoading = document.getElementById("_img_loading_" + divId);
        if (imgLoading != null)
            _isload = true;
        return _isload;
    },


    //是否显示/隐藏正在加载的图片
    //isShow:  true显示加载图片  false  隐藏
    //loadingDivId:显示图片的div的id
    //dataDivId:显示数据的div的id
    IsShowHiddenLoading: function(isShow, loadingDivId, dataDivId) {
        var divUserSetLoading = document.getElementById(loadingDivId);
        var divUserSet = document.getElementById(dataDivId);
        if (isShow) {
            divUserSetLoading.style.display = "";
            divUserSet.style.display = "none";
        }
        else {
            divUserSetLoading.style.display = "none";
            divUserSet.style.display = "";
        }
    }
}