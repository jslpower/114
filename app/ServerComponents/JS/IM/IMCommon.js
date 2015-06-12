/*
need javascript library jquery 1.3.2
*/
function GetDesPlatformUrlForMQMsg(desUrl,mqid,mqPwd){
    var data = {
        loginuid:mqid,
        passwd:mqPwd,
        desurl:encodeURIComponent(desUrl)
    };
    
    return "/MQTransit.aspx?"+$.param(data);
 }