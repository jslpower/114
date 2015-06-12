<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/PublicCenterMasterPage.Master" AutoEventWireup="true" CodeBehind="HotelCheck.aspx.cs" Inherits="UserPublicCenter.HotelManage.HotelCheck" %>
<%@ Import Namespace="EyouSoft.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
<link href="<%=CssManage.GetCssFilePath("HotelManage") %>" rel="stylesheet" type="text/css" />
	<div class="main">
    	<img class="add01" src="<%=ImageServerUrl%>/Images/hotel/hotel_add01.gif" />
        <!--content start-->
        <div class="content">
       		<!--sidebar start-->
        	<div class="sidebar sidebarSearch">
              <!--sidebar_1-->
           	  <div class="sidebar_1">
               	<h1 class="T">酒店搜索</h1>
               <!--search_box start-->
                <div class="search_box">
                	<div class="field"><label><font class="C_red">*</font> 输入城市：</label></div>
               	  <div class="field field02">
               	    <select id="citycode" onchange="selectChange(this)" name="citycode" >
        <option value="ABA">ABAZHOU阿坝州ABA</option>
        <option value="ANJ">ANJI安吉ANJ</option>
        <option value="AQG">ANQING安庆AQG</option>
        <option value="AOG">ANSHAN鞍山AOG</option>
        <option value="ANS">ANSHUN安顺ANS</option>
        <option value="AYN">ANYANG安阳AYN</option>
        <option value="MFM">AOMEN澳门MFM</option>
        <option value="BAS">BAISHAN白山BAS</option>
        <option value="BFU">BANGBU蚌埠BFU</option>
        <option value="BAD">BAODING保定BAD</option>
        <option value="BAJ">BAOJI宝鸡BAJ</option>
        <option value="BSD">BAOSHAN保山BSD</option>
        <option value="BUT">BAOTING保亭BUT</option>
        <option value="BAV">BAOTOU包头BAV</option>
        <option value="BYZ">BAYANNAOER巴彦淖尔BYZ</option>
        <option value="BDH">BEIDAIHE北戴河BDH</option>
        <option value="BHY">BEIHAI北海BHY</option>
        <option value="PEK" selected="selected">BEIJING北京PEK</option>
        <option value="BEX">BENXI本溪BEX</option>
        <option value="BIZ">BINZHOU滨州BIZ</option>
        <option value="CAZ">CANGZHOU沧州CAZ</option>
        <option value="CGQ">CHANGCHUN长春CGQ</option>
        <option value="CGD">CHANGDE常德CGD</option>
        <option value="CHJ">CHANGJI昌吉CHJ</option>
        <option value="CSX">CHANGSHA长沙CSX</option>
        <option value="CHS">CHANGSHU常熟CHS</option>
        <option value="CZX">CHANGZHOU常州CZX</option>
        <option value="CHU">CHAOHU巢湖CHU</option>
        <option value="CZH">CHAOZHOU潮州CZH</option>
        <option value="CHD">CHENGDE承德CHD</option>
        <option value="CTU">CHENGDOU成都CTU</option>
        <option value="CEZ">CHENZHOU郴州CEZ</option>
        <option value="CIF">CHIFENG赤峰CIF</option>
        <option value="CZU">CHIZHOU池州CZU</option>
        <option value="CKG">CHONGQING重庆CKG</option>
        <option value="CHX">CHUXIONG楚雄CHX</option>
        <option value="CUZ">CHUZHOU滁州CUZ</option>
        <option value="CIX">CIXI慈溪CIX</option>
        <option value="COH">CONGHUA从化COH</option>
        <option value="DLU">DALI大理DLU</option>
        <option value="DLC">DALIAN大连DLC</option>
        <option value="DDG">DANDONG丹东DDG</option>
        <option value="DYA">DANYANG丹阳DYA</option>
        <option value="DAQ">DAQING大庆DAQ</option>
        <option value="DAT">DATONG大同DAT</option>
        <option value="DAX">DAXINGANLING大兴安岭DAX</option>
        <option value="DZH">DAZHOU达州DZH</option>
        <option value="DEF">DENGFENG登封DEF</option>
        <option value="DEQ">DEQING德清DEQ</option>
        <option value="DEY">DEYANG德阳DEY</option>
        <option value="DZO">DEZHOU德州DZO</option>
        <option value="DGM">DONGGUAN东莞DGM</option>
        <option value="DOT">DONGTAI东台DOT</option>
        <option value="DYN">DONGYANG东阳DYN</option>
        <option value="DOY">DONGYING东营DOY</option>
        <option value="DOJ">DOUJIANGYAN都江堰DOJ</option>
        <option value="DNH">DUNHUANG敦煌DNH</option>
        <option value="ERD">EERDUOSI鄂尔多斯ERD</option>
        <option value="EMS">EMEISHAN峨眉山EMS</option>
        <option value="ENH">ENSHI恩施ENH</option>
        <option value="ERL">ERLIANHAOTE二连浩特ERL</option>
        <option value="FAC">FANGCHENGGANG防城港FAC</option>
        <option value="FCG">FEICHENG肥城FCG</option>
        <option value="FEH">FENGHUA奉化FEH</option>
        <option value="FHX">FENGHUANGXIAN凤凰县FHX</option>
        <option value="FUO">FOSHAN佛山FUO</option>
        <option value="FUD">FUDING福鼎FUD</option>
        <option value="FUQ">FUQING福清FUQ</option>
        <option value="FUS">FUSHUN抚顺FUS</option>
        <option value="FUG">FUYANG阜阳FUG</option>
        <option value="FUY">FUYANG富阳FUY</option>
        <option value="FOC">FUZHOU福州FOC</option>
        <option value="GZH">GANZHOU赣州GZH</option>
        <option value="GAY">GAOYOU高邮GAY</option>
        <option value="GUA">GUANGAN广安GUA</option>
        <option value="GUY">GUANGYUAN广元GUY</option>
        <option value="CAN">GUANGZHOU广州CAN</option>
        <option value="GUG">GUIGANG贵港GUG</option>

        <option value="KWL">GUILIN桂林KWL</option>
        <option value="KWE">GUIYANG贵阳KWE</option>
        <option value="HRB">HAERBIN哈尔滨HRB</option>
        <option value="HAA">HAIAN海安HAA</option>
        <option value="HAK">HAIKOU海口HAK</option>
        <option value="HLG">HAILUOGOU海螺沟HLG</option>
        <option value="HAI">HAINING海宁HAI</option>
        <option value="HAY">HAIYAN海盐HAY</option>
        <option value="HIY">HAIYANG海阳HIY</option>
        <option value="HDN">HANDAN邯郸HDN</option>
        <option value="HGH">HANGZHOU杭州HGH</option>
        <option value="HZG">HANZHONG汉中HZG</option>
        <option value="HEB">HEBI鹤壁HEB</option>
        <option value="HFE">HEFEI合肥HFE</option>
        <option value="HEG">HEGANG鹤岗HEG</option>
        <option value="HEK">HEIHE黑河HEK</option>
        <option value="HDA">HENGDIAN横店HDA</option>
        <option value="HSU">HENGSHUI衡水HSU</option>
        <option value="HNY">HENGYANG衡阳HNY</option>
        <option value="HSH">HESHAN鹤山HSH</option>
        <option value="HEY">HEYUAN河源HEY</option>
        <option value="HZE">HEZE菏泽HZE</option>
        <option value="HUA">HUAIAN淮安HUA</option>
        <option value="HUH">HUAIHUA怀化HUH</option>
        <option value="HUI">HUAINAN淮南HUI</option>
        <option value="TXN">HUANGSHAN黄山TXN</option>
        <option value="HUS">HUANGSHI黄石HUS</option>
        <option value="HET">HUHEHAOTE呼和浩特HET</option>
        <option value="HUZ">HUIZHOU惠州HUZ</option>
        <option value="HLD">HULUDAO葫芦岛HLD</option>
        <option value="HUL">HULUNBEIER呼伦贝尔HUL</option>
        <option value="HZO">HUZHOU湖州HZO</option>
        <option value="JMU">JIAMUSI佳木斯JMU</option>
        <option value="JID">JIANDE建德JID</option>
        <option value="JDU">JIANGDOU江都JDU</option>
        <option value="JIM">JIANGMEN江门JIM</option>
        <option value="JIY">JIANGYIN江阴JIY</option>
        <option value="JYO">JIANGYOU江油JYO</option>
        <option value="JNA">JIAONAN胶南JNA</option>
        <option value="JZU">JIAOZUO焦作JZU</option>
        <option value="JAS">JIASHAN嘉善JAS</option>
        <option value="JIX">JIAXING嘉兴JIX</option>
        <option value="JGN">JIAYUGUAN嘉峪关JGN</option>
        <option value="JYN">JIEYANG揭阳JYN</option>
        <option value="JIL">JILIN吉林JIL</option>
        <option value="JMO">JIMO即墨JMO</option>
        <option value="TNA">JINAN济南TNA</option>
        <option value="JIC">JINCHENG晋城JIC</option>
        <option value="JDZ">JINGDEZHEN景德镇JDZ</option>
        <option value="JGS">JINGGANGSHAN井冈山JGS</option>
        <option value="JIJ">JINGJIANG靖江JIJ</option>
        <option value="JMN">JINGMEN荆门JMN</option>
        <option value="JZG">JINGZHOU荆州JZG</option>
        <option value="JHA">JINHUA金华JHA</option>
        <option value="JIN">JINING集宁JIN</option>
        <option value="JNG">JINING济宁JNG</option>
        <option value="JJN">JINJIANG晋江JJN</option>
        <option value="JIT">JINTAN金坛JIT</option>
        <option value="JYU">JINYUN缙云JYU</option>
        <option value="JZO">JINZHONG晋中JZO</option>
        <option value="JNZ">JINZHOU锦州JNZ</option>
        <option value="JHS">JIUHUASHAN九华山JHS</option>
        <option value="JIU">JIUJIANG九江JIU</option>
        <option value="CHW">JIUQUAN酒泉CHW</option>
        <option value="JZH">JIUZHAIGOU九寨沟JZH</option>
        <option value="JYA">JIYUAN济源JYA</option>
        <option value="JUR">JURONG句容JUR</option>
        <option value="KAF">KAIFENG开封KAF</option>
        <option value="KAL">KAILI凯里KAL</option>
        <option value="KAP">KAIPING开平KAP</option>
        <option value="KHG">KASHEN喀什KHG</option>
        <option value="KLY">KELAMAYI克拉玛依KLY</option>
        <option value="KRL">KUERLE库尔勒KRL</option>
        <option value="KMG">KUNMING昆明KMG</option>
        <option value="KUS">KUNSHAN昆山KUS</option>
        <option value="LAW">LAIWU莱芜LAW</option>
        <option value="LXI">LAIXI莱西LXI</option>
        <option value="LAY">LAIYANG莱阳LAY</option>
        <option value="LAF">LANGFANG廊坊LAF</option>
        <option value="LAX">LANXI兰溪LAX</option>
        <option value="LHW">LANZHOU兰州LHW</option>
        <option value="LXA">LASA拉萨LXA</option>
        <option value="LEQ">LEQING乐清LEQ</option>
        <option value="LSA">LESHAN乐山LSA</option>
        <option value="LIC">LIANCHENG连城LIC</option>
        <option value="LYG">LIANYUNGANG连云港LYG</option>
        <option value="LCN">LIAOCHENG聊城LCN</option>
        <option value="LIY">LIAOYANG辽阳LIY</option>
        <option value="LJG">LIJIANG丽江LJG</option>
        <option value="LIA">LINAN临安LIA</option>
        <option value="LSX">LINGSHUIXIAN陵水县LSX</option>
        <option value="LHA">LINHAI临海LHA</option>
        <option value="LYI">LINYI临沂LYI</option>
        <option value="LZI">LINZHI林芝LZI</option>
        <option value="LIS">LISHUI丽水LIS</option>
        <option value="LAN">LIUAN六安LAN</option>
        <option value="LZH">LIUZHOU柳州LZH</option>
        <option value="LYN">LIYANG溧阳LYN</option>
        <option value="LOY">LONGYAN龙岩LOY</option>
        <option value="LUH">LUOHE漯河LUH</option>
        <option value="LYA">LUOYANG洛阳LYA</option>
        <option value="LUZ">LUSHAN庐山LUZ</option>
        <option value="LZO">LUZHOU泸州LZO</option>
        <option value="MAA">MAANSHAN马鞍山MAA</option>
        <option value="MAZ">MANZHOULI满洲里MAZ</option>
        <option value="MAM">MAOMING茂名MAM</option>
        <option value="MZU">MEIZHOU梅州MZU</option>
        <option value="MIG">MIANYANG绵阳MIG</option>
        <option value="MDG">MUDANJIANG牡丹江MDG</option>
        <option value="KHN">NANCHANG南昌KHN</option>
        <option value="NAO">NANCHONG南充NAO</option>
        <option value="NDH">NANDAIHE南戴河NDH</option>
        <option value="NKG">NANJING南京NKG</option>
        <option value="NNG">NANNING南宁NNG</option>
        <option value="NTG">NANTONG南通NTG</option>
        <option value="NNY">NANYANG南阳NNY</option>
        <option value="NEJ">NEIJIANG内江NEJ</option>
        <option value="NGB">NINGBO宁波NGB</option>
        <option value="NID">NINGDE宁德NID</option>
        <option value="NHX">NINGHAIXIAN宁海县NHX</option>
        <option value="PAJ">PANJIN盘锦PAJ</option>
        <option value="PZI">PANZHIHUA攀枝花PZI</option>
        <option value="PEL">PENGLAI蓬莱PEL</option>
        <option value="PIH">PINGHU平湖PIH</option>
        <option value="PIX">PINGXIANG萍乡PIX</option>
        <option value="PYO">PINGYAO平遥PYO</option>
        <option value="PIZ">PIZHOU邳州PIZ</option>
        <option value="PUJ">PUJIANG浦江PUJ</option>
        <option value="PUT">PUTIAN莆田PUT</option>
        <option value="PUY">PUYANG濮阳PUY</option>
        <option value="QIH">QIANDAOHU千岛湖QIH</option>
        <option value="QID">QIDONG启东QID</option>
        <option value="TAO">QINGDAO青岛TAO</option>
        <option value="QYN">QINGYUAN清远QYN</option>
        <option value="QIZ">QINGZHOU青州QIZ</option>
        <option value="SHP">QINHUANGDAO秦皇岛SHP</option>
        <option value="QZO">QINZHOU钦州QZO</option>
        <option value="QHA">QIONGHAI琼海QHA</option>
        <option value="NDG">QIQIHAER齐齐哈尔NDG</option>
        <option value="QUZ">QUANZHOU泉州QUZ</option>
        <option value="QUF">QUFU曲阜QUF</option>
        <option value="QUJ">QUJING曲靖QUJ</option>
        <option value="JUZ">QUZHOU衢州JUZ</option>
        <option value="RIK">RIKAZE日喀则RIK</option>
        <option value="RIZ">RIZHAO日照RIZ</option>
        <option value="ROC">RONGCHENG荣成ROC</option>
        <option value="RUG">RUGAO如皋RUG</option>
        <option value="RUA">RUIAN瑞安RUA</option>
        <option value="RUL">RUILI瑞丽RUL</option>
        <option value="SAM">SANMENXIA三门峡SAM</option>
        <option value="SMI">SANMING三明SMI</option>
        <option value="SYX">SANYA三亚SYX</option>
        <option value="SHA">SHANGHAI上海SHA</option>
        <option value="SHQ">SHANGQIU商丘SHQ</option>
        <option value="SHR">SHANGRAO上饶SHR</option>
        <option value="SYU">SHANGYU上虞SYU</option>
        <option value="SHN">SHANNAN山南SHN</option>
        <option value="SWA">SHANTOU汕头SWA</option>
        <option value="SHW">SHANWEI汕尾SHW</option>
        <option value="HSC">SHAOGUAN韶关HSC</option>
        <option value="SHS">SHAOSHAN韶山SHS</option>
        <option value="SHX">SHAOXING绍兴SHX</option>
        <option value="SYG">SHAOYANG邵阳SYG</option>
        <option value="SZU">SHENGZHOU嵊州SZU</option>
        <option value="SHE">SHENYANG沈阳SHE</option>
        <option value="SZX">SHENZHEN深圳SZX</option>
        <option value="SJW">SHIJIAZHUANG石家庄SJW</option>
        <option value="SHI">SHISHI石狮SHI</option>
        <option value="SYA">SHIYAN十堰SYA</option>
        <option value="SHG">SHOUGUANG寿光SHG</option>
        <option value="SHD">SHUNDE顺德SHD</option>
        <option value="SOY">SONGYUAN松原SOY</option>
        <option value="SUF">SUIFENHE绥芬河SUF</option>
        <option value="SUN">SUINING遂宁SUN</option>
        <option value="SUZ">SUIZHOU随州SUZ</option>
        <option value="SZV">SUZHOU苏州SZV</option>
        <option value="TAA">TAIAN泰安TAA</option>
        <option value="TAC">TAICANG太仓TAC</option>
        <option value="TSA">TAISHAN台山TSA</option>
        <option value="TSH">TAISHUN泰顺TSH</option>
        <option value="TYN">TAIYUAN太原TYN</option>
        <option value="TAZ">TAIZHOU台州TAZ</option>
        <option value="TZU">TAIZHOU泰州TZU</option>
        <option value="TAS">TANGSHAN唐山TAS</option>
        <option value="TCH">TENGCHONG腾冲TCH</option>
        <option value="TSN">TIANJIN天津TSN</option>
        <option value="TIS">TIANSHUI天水TIS</option>
        <option value="TTA">TIANTAI天台TTA</option>
        <option value="TZS">TIANZHUSHAN天柱山TZS</option>
        <option value="TIL">TIELING铁岭TIL</option>
        <option value="TNH">TONGHUA通化TNH</option>
        <option value="TOL">TONGLI同里TOL</option>
        <option value="TGO">TONGLIAO通辽TGO</option>
        <option value="TOG">TONGLING铜陵TOG</option>
        <option value="TLU">TONGLU桐庐TLU</option>
        <option value="TOX">TONGXIANG桐乡TOX</option>
        <option value="TOZ">TONGZHOU通洲TOZ</option>
        <option value="TUL">TULUFAN吐鲁番TUL</option>
        <option value="WAN">WANNING万宁WAN</option>
        <option value="WEF">WEIFANG潍坊WEF</option>
        <option value="WEH">WEIHAI威海WEH</option>
        <option value="WEC">WENCHANG文昌WEC</option>
        <option value="WED">WENDENG文登WED</option>
        <option value="WEG">WENLING温岭WEG</option>
        <option value="WNZ">WENZHOU温州WNZ</option>
        <option value="WHA">WUHAI乌海WHA</option>
        <option value="WUH">WUHAN武汉WUH</option>
        <option value="WHU">WUHU芜湖WHU</option>
        <option value="WJI">WUJIANG吴江WJI</option>
        <option value="URC">WULUMUQI乌鲁木齐URC</option>
        <option value="WTS">WUTAISHAN五台山WTS</option>
        <option value="WUX">WUXI无锡WUX</option>
        <option value="WYI">WUYI武义WYI</option>
        <option value="WUS">WUYISHAN武夷山WUS</option>
        <option value="WUY">WUYUAN婺源WUY</option>
        <option value="XZS">WUZHISHAN五指山XZS</option>
        <option value="WZS">WUZHONG吴忠WZS</option>
        <option value="WUZ">WUZHOU梧州WUZ</option>
        <option value="XHX">XIAHEXIAN夏河县XHX</option>
        <option value="XMN">XIAMEN厦门XMN</option>
        <option value="SIA">XIAN西安SIA</option>
        <option value="XFN">XIANGFAN襄樊XFN</option>
        <option value="HKG">XIANGGANG香港HKG</option>
        <option value="XIG">XIANGGELILA香格里拉XIG</option>
        <option value="XSH">XIANGSHAN象山XSH</option>
        <option value="XIT">XIANGTAN湘潭XIT</option>
        <option value="XIY">XIANYANG咸阳XIY</option>
        <option value="XGN">XIAOGAN孝感XGN</option>
        <option value="XIC">XICHANG西昌XIC</option>
        <option value="XIL">XILINHAOTE锡林浩特XIL</option>
        <option value="XHA">XINCHANG新昌XHA</option>
        <option value="XEN">XINGCHENG兴城XEN</option>
        <option value="XNT">XINGTAI邢台XNT</option>
        <option value="XNN">XINING西宁XNN</option>
        <option value="XIX">XINXIANG新乡XIX</option>
        <option value="XYA">XINYANG信阳XYA</option>
        <option value="XIN">XINYI新沂XIN</option>
        <option value="JHG">XISHUANGBANNA西双版纳JHG</option>
        <option value="XIQ">XIUQIAN宿迁XIQ</option>
        <option value="XIO">XIUZHOU宿州XIO</option>
        <option value="XCA">XUCHANG许昌XCA</option>
        <option value="XUZ">XUZHOU徐州XUZ</option>
        <option value="YAA">YAAN雅安YAA</option>
        <option value="YBL">YABULI亚布力YBL</option>
        <option value="ENY">YANAN延安ENY</option>
        <option value="YNZ">YANCHENG盐城YNZ</option>
        <option value="YAD">YANDANGSHAN雁荡山YAD</option>
        <option value="YAJ">YANGJIANG阳江YAJ</option>
        <option value="YAH">YANGSHUO阳朔YAH</option>
        <option value="YZO">YANGZHOU扬州YZO</option>
        <option value="YNJ">YANJI延吉YNJ</option>
        <option value="YNT">YANTAI烟台YNT</option>
        <option value="YZH">YANZHOU兖州YZH</option>
        <option value="YBP">YIBIN宜宾YBP</option>
        <option value="YIH">YICHANG宜昌YIH</option>
        <option value="YIC">YICHUN宜春YIC</option>
        <option value="INC">YINCHUAN银川INC</option>
        <option value="YIK">YINGKOU营口YIK</option>
        <option value="YIT">YINGTAN鹰潭YIT</option>
        <option value="YIN">YINING伊宁YIN</option>
        <option value="YIW">YIWU义乌YIW</option>
        <option value="YIX">YIXING宜兴YIX</option>
        <option value="YIY">YIYANG益阳YIY</option>
        <option value="YIZ">YIZHENG仪征YIZ</option>
        <option value="YOK">YONGKANG永康YOK</option>
        <option value="YUY">YUEYANG岳阳YUY</option>
        <option value="UYN">YULIN榆林UYN</option>
        <option value="YUL">YULIN玉林YUL</option>
        <option value="YUC">YUNCHENG运城YUC</option>
        <option value="YNF">YUNFU云浮YNF</option>
        <option value="YUX">YUXI玉溪YUX</option>
        <option value="YUA">YUYAO余姚YUA</option>
        <option value="ZAZ">ZAOZHUANG枣庄ZAZ</option>
        <option value="ZHJ">ZHANGJIAGANG张家港ZHJ</option>
        <option value="DYG">ZHANGJIAJIE张家界DYG</option>
        <option value="ZJK">ZHANGJIAKOU张家口ZJK</option>
        <option value="ZHY">ZHANGYE张掖ZHY</option>
        <option value="ZHZ">ZHANGZHOU漳州ZHZ</option>
        <option value="ZHA">ZHANJIANG湛江ZHA</option>
        <option value="ZHQ">ZHAOQING肇庆ZHQ</option>
        <option value="CGO">ZHENGZHOU郑州CGO</option>
        <option value="ZJA">ZHENJIANG镇江ZJA</option>
        <option value="ZIS">ZHONGSHAN中山ZIS</option>
        <option value="ZWS">ZHONGWEI中卫ZWS</option>
        <option value="HSN">ZHOUSHAN舟山HSN</option>
        <option value="ZZS">ZHOUZHUANG周庄ZZS</option>
        <option value="ZUH">ZHUHAI珠海ZUH</option>
        <option value="ZJI">ZHUJI诸暨ZJI</option>
        <option value="ZHM">ZHUMADIAN驻马店ZHM</option>
        <option value="ZHO">ZHUZHOU株洲ZHO</option>
        <option value="ZIB">ZIBO淄博ZIB</option>
        <option value="ZYI">ZUNYI遵义ZYI</option>
      </select>
           	      </div>
                  <div class="field"><label><font class="C_red">*</font> 入住日期：</label><input name="textfield" type="text" id="textfield" size="12">
                  </div>
                    <div class="field"><label><font class="C_red">*</font> 离店日期：</label><input name="textfield" type="text"  id="textfield" size="12">
                  </div>
                  <div class="field"><label>价格范围：</label><input name="textfield" type="text"  id="textfield" size="5"> - <input name="textfield" type="text"  id="textfield" size="5">
                    </div>
                    <div class="field"><label>酒店等级：</label><select name="select2" id="select2" ><option value="0">不限</option><option value="1">ddddddddddddd</option></select>
                    </div>
                    <div class="field"><label>关键词：</label><input name="textfield" type="text"size="12">
                  </div>    
                  <div class="submit"><a href="searchresult_login.html"><img src="<%=ImageServerUrl%>/Images/hotel/search_but.gif" /></a><a href="#">高级搜索>></a></div>
                  <div class="field">
                  <h2 class="T_sub">缩小范围搜索</h2>
                  	<ul>
					  <li><label><input type="checkbox" name="checkbox" value="checkbox" /></label><font class="C_Grb">即时确认</font></li>
					  <li><label><input type="checkbox" name="checkbox" value="checkbox" /></label><font class="C_Grb">接机服务</font></li>
					  <li><label><input type="checkbox" name="checkbox" value="checkbox" /></label><font class="C_Grb">优惠礼品</font></li>
					  <li><label><input type="checkbox" name="checkbox" value="checkbox" /></label><font class="C_Grb">上网设施</font></li>
					  <li>装修时间：<select name="select2" id="select2"><option value="0">请选择</option><option value="1">2010</option></select></li>
					  <li>特殊房型：<select name="select2" id="select2"><option value="0">请选择</option><option value="1">特殊</option></select></li>
					  <li>床&nbsp;&nbsp;&nbsp;&nbsp;型：<select name="select2" id="select2"><option value="0">请选择</option><option value="1">大床</option></select></li>
					  </ul>
                      <div class="search_bottom"></div>
                  </div>
                </div>
                <!--search_box end-->
              </div>
              <!--sidebar_1 end-->
              <div style="margin-top:10px;"><img src="<%=ImageServerUrl%>/Images/hotel/left_add0102.gif" /></div>
             <!-- sidebar_2 start-->
              <div class="sidebar_2 sidebar_2_ClassPage">
              	<p class="more moreClassPage"><span>酒店常识</span></p>
                <ul>
                	<li><a href="#">住店注意事项 </a></li>
                    <li><a href="#">酒店的部分设置</a></li>
                    <li><a href="#">酒店基本房型</a></li>
                    <li><a href="#">酒店星级</a></li>
                    <li><a href="#">酒店类型</a></li>
                    <li><a href="#">酒店的各项惯例</a></li>
                </ul>
              </div>
              <div style="margin-top:10px;"><img src="<%=ImageServerUrl%>/Images/hotel/left_add0202.gif" /></div>
            </div>
           <!--sidebar02 start-->
            <div class="sidebar02 sidebar02Search">
            	<div class="sidebar02_1">
                    <p class="xuanzhe"><span>核对预订单信息</span><img src="<%=ImageServerUrl%>/Images/hotel/liucheng04.gif"></p>
                   <!--sidebar02SearchC start-->
                    <div class="sidebar02SearchC">
                        	
                      <div class="yd_jiange C_red">为了与旅客及时确认并联系，请再次确认订单信息</span>
                    </div>  
					
     <div class="yuding"><h1>入住信息</h1>
	 <div class="biaoge2">
	 <table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="20%" align="center" valign="middle" bgcolor="#FDF5EA">房间数：</td>
            <td align="left">&nbsp;<font class="frb">2</font>（间）</td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人姓名：<br /></td>
            <td align="left">&nbsp;王芳,吴梅,严畅,严加 </td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人类型：<br /></td>
            <td align="left">&nbsp;内宾,内宾,内宾,内宾</td>
            </tr>
          <tr>
            <td align="center" valign="middle" bgcolor="#FDF5EA">入住客人手机：<br /></td>
            <td align="left">&nbsp;13588888888<font class="C_red" style="margin-left:40px;">短信通知</font></td>
            </tr>
        </table>
	 </div>
	 </div>
	 
	 <div class="yuding">
		<h1>预订酒店信息</h1>
            <div class="biaoge2">
              <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="20%" align="center" valign="middle" bgcolor="#FDF5EA">酒店名称：</td>
                  <td width="25%" align="left">&nbsp;北京千禧大酒店</td>
                  <td width="20%" align="center" bgcolor="#FDF5EA">房型：</td>
                  <td width="33%" align="left">&nbsp;豪华大床间</td>
                </tr>
                <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">住店日期：</td>
                  <td align="left">&nbsp;张三</td>
                  <td align="center" bgcolor="#FDF5EA">支付方式：</td>
                  <td align="left">&nbsp;前台现付</td>
                </tr>
                <tr>
                  <td align="center" valign="middle" bgcolor="#FDF5EA">其他要求：</td>
                  <td colspan="3" align="left">&nbsp;尽量无烟层房间  每天每间加一早餐  同一楼层  相邻房间 </td>
                </tr>
              </table>
            </div>
                <h1>价格清单</h1>
                <div class="biaoge2">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <th align="center">&nbsp;</th>
                    <th align="center">月/日</th>
                    <th align="center">周一</th>
                    <th align="center">周二</th>
                    <th align="center">周三</th>
                    <th align="center">周四</th>
                    <th align="center">周五</th>
                    <th align="center">周六</th>
                    <th align="center">周日</th>
                    <th align="center">每间总价</th>
                    <th align="center">总计</th>
                  </tr>
                  <tr>
                    <td align="center" class="orange">售价</td>
                    <td rowspan="3" align="center">11/19<br />
                      -<br />
                    11/19</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td class="green strong" rowspan="3" align="center">销售价：<br />
                      ￥478.0<br />
                      返佣价：<br />
                ￥478.0</td>
                    <td class="orange strong" rowspan="3" align="center">销售价：<br />
                ￥1356.0<br />
                返佣价：<br />
                ￥1356.0</td>
                  </tr>
                  <tr>
                    <td align="center" class="orange">返佣价</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    <td align="center">￥478.0</td>
                    </tr>
                  <tr>
                    <td align="center" class="orange">早餐</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    <td align="center">双早</td>
                    </tr>
                </table>
                </div>
	 		</div>
	 <div class="yuding"><h1>联系人信息</h1>
<div class="biaoge2">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <th width="13%" align="center" class="pandl">姓名：</th>
    <td width="20%" align="center" class="pandl">严畅</td>
    <th width="13%" align="center" class="pandl">手机：</th>
    <td width="20%" align="center" class="pandl">13815640020</td>
    <th width="13%" align="center" class="pandl">座机：</th>
    <td width="20%" align="center" class="pandl">0571-12345678-123</td>
  </tr>
</table>
</div>
<ul class="btn02">
  <li><a href="overrderform.html"><img src="<%=ImageServerUrl%>/Images/hotel/qrtijiaobtn.gif" width="128" height="35" border="0" /></a></li>
  <li><a href="yuding.html"><img src="<%=ImageServerUrl%>/Images/hotel/backbtn.gif" border="0" /></a></li>
  <div class="clear"></div>
</ul>

	 </div>
	                </div>
                    <!--sidebar02SearchC end-->
              </div>
                    </div>
            </div>
          <!--sidebar02 end-->
        </div>
</asp:Content>
