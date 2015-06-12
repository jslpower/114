	var win;
	Ext.Win = function(){
		var frameId = new Date().format('ymdgis');
		return{
			open: function(url,title,attr,closed){
				win = new Ext.Window({
						id: attr.id,
						layout:'fit',
						title: title,
						width: attr.width,
						height: attr.height + 40 > Ext.getBody().getViewSize().height ? Ext.getBody().getViewSize().height : attr.height + 40,
						footer: false,
						modal: attr.modal == 'no' ? false : true,
						maximizable: attr.maximize == 'yes' ? true : false,
						plain: true,
						closable: attr.closable == 'no' ?  false : true ,
						html: "<iframe id='CWindow_Iframe_Content_"+frameId+"' style='overflow-x:hidden;' src='about:blank' frameborder='0' scrolling='" + (attr.scrollbars == 'no' ? 'no' : 'yes')+ "' width='100%' height='100%'></iframe>",
						buttons: [
							{
								text: '关闭',
								handler: function(){
									Ext.WindowMgr.getActive().close();
								}
							}
						]
					});
					if (attr.closebtn == 'no'){
						win.buttons[0].hide();
					}
				win.show(attr.target);			
				win.mask.setSize(Ext.getBody().getViewSize().width,document.body.scrollHeight);
				Ext.getDom("CWindow_Iframe_Content_"+frameId).contentWindow.document.write("<span style='font-size:14px'>页面加载中，请稍后...</span>");
				Ext.getDom("CWindow_Iframe_Content_"+frameId).src = url;
				if (typeof(closed) == "function"){
					win.on("close",closed);
				}
				win.on("close",function(){
					//Ext.getDom("CWindow_Iframe_Content_"+frameId).src = 'about:blank';
					Ext.get("CWindow_Iframe_Content_"+frameId).remove();
					CollectGarbage();
				});
			},
			close: function(){
				Ext.WindowMgr.getActive().close();
			}
		}
	}
	
	function CWindow(url,title,attr){
		
	}
	
	Controls={};
	Controls.Dialog=function(url,title,attr){
		this.open=Ext.Win().open(url,title,attr);
	}