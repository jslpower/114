using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using EyouSoft.Common;
using EyouSoft.Common.Function;

namespace SiteOperationsCenter.NewsCenterControl
{
    /// <summary>
    /// 描述：运营后台新闻添加页面
    /// 修改记录：
    /// 1. 2010-03-31 AM 曹胡生 创建
    /// </summary>
    public partial class NewsAdd : EyouSoft.Common.Control.YunYingPage
    {
        #region 新闻初始化，保存操作

        //新闻内容
        string Content = "";

        protected override void OnPreLoad(EventArgs e)
        {
            int NewId = Utils.GetInt(Utils.GetQueryStringValue("Id"));
            ProvinceAndCityList1.SetProvinceId = Utils.GetInt(ProvinceId.Value);
            ProvinceAndCityList1.SetCityId = Utils.GetInt(CityId.Value);
            //判断新增权限
            if (NewId == 0)
            {
                //权限验证
                YuYingPermission[] editParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_新增 };
                if (!CheckMasterGrant(editParms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.新闻中心_新增, true);
                    return;
                }
            }
            //判断修改权限
            else
            {
                //权限验证
                YuYingPermission[] editParms = { YuYingPermission.新闻中心_管理该栏目, YuYingPermission.新闻中心_修改 };
                if (!CheckMasterGrant(editParms))
                {
                    Utils.ResponseNoPermit(YuYingPermission.新闻中心_修改, true);
                    return;
                }
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                DataInit();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataSave();
        }

        /// <summary>
        /// 新闻数据初始化
        /// </summary>
        private void DataInit()
        {
            //新闻编号
            int NewId = Utils.GetInt(Utils.GetQueryStringValue("Id"));
            EyouSoft.BLL.NewsStructure.NewsBll NewsBll = new EyouSoft.BLL.NewsStructure.NewsBll();
            EyouSoft.Model.NewsStructure.NewsModel NewsModel = NewsBll.GetModel(NewId);
            BindNewsClass();
            BindAfficheSource();
            BindRecPosition();
            if (NewsModel != null)
            {
                //省份
                ProvinceAndCityList1.SetProvinceId = NewsModel.ProvinceId;
                //城市
                ProvinceAndCityList1.SetCityId = NewsModel.CityId;
                //新闻类别
                ddlNewsClass.SelectedValue = NewsModel.AfficheClass.ToString();
                //新闻标题
                txtNewsTitle.Value = NewsModel.AfficheTitle;
                //修改时间 
                UpdateTime.Value = NewsModel.UpdateTime.ToString();
                //新闻推荐位置类别
                if (NewsModel.RecPositionId != null)
                {
                    foreach (EyouSoft.Model.NewsStructure.RecPosition re in NewsModel.RecPositionId)
                    {
                        try
                        {
                            chkRecPositionList.Items.FindByValue(((int)re).ToString()).Selected = true;
                        }
                        catch
                        {

                        }
                    }
                    //当推荐位置为URL跳转，跳转到的URL
                    if (chkRecPositionList.Items.FindByValue(((int)EyouSoft.Model.NewsStructure.RecPosition.URL跳转).ToString()).Selected == true)
                    {
                        drumpUr2.Value = NewsModel.GotoUrl;
                    }
                }
                //标题颜色
                selTitleColor.Value = NewsModel.TitleColor;
                #region 当前新闻的关键字
                if (NewsModel.NewsKeyWordItem != null)
                {
                    foreach (EyouSoft.Model.NewsStructure.NewsSubItem str in NewsModel.NewsKeyWordItem)
                    {
                        txtNewsKeys.Value += str.ItemName + " ";
                        hidNewsKeysId.Value += str.ItemId + ",";

                    }
                    //新闻关键字
                    txtNewsKeys.Value = txtNewsKeys.Value.TrimEnd(' ');
                    hidNewsKeysId.Value = hidNewsKeysId.Value.TrimEnd(',');
                }
                #endregion
                #region 当前新闻的Tags
                if (NewsModel.NewsTagItem != null)
                {
                    foreach (EyouSoft.Model.NewsStructure.NewsSubItem str in NewsModel.NewsTagItem)
                    {
                        txtNewsTags.Value += str.ItemName + " ";
                        hidNewsTagsId.Value += str.ItemId + ",";

                    } //新闻Tag
                    txtNewsTags.Value = txtNewsTags.Value.TrimEnd(' ');
                    hidNewsTagsId.Value = hidNewsTagsId.Value.TrimEnd(',');
                }
                #endregion
                //新闻描述
                txtNewsDesc.Value = NewsModel.AfficheDesc;
                //图片路径
                if (!string.IsNullOrEmpty(NewsModel.PicPath))
                {
                    lblImagePath.Visible = true;
                    lblImagePath.NavigateUrl = Domain.FileSystem + NewsModel.PicPath;
                    ImagePath.Value = NewsModel.PicPath;
                }
                //新闻来源
                txtNewsSource.Value = NewsModel.AfficheSource;
                //新闻作者
                txtNewsArticle.Value = NewsModel.AfficheAuthor;
                //新闻内容
                FCK_PlanTicketContent.Value = NewsModel.AfficheContent;
                //文章排序方式
                radSortList.SelectedValue = ((int)NewsModel.AfficheSort).ToString();
                DelLink.Checked = true;
                downloadRemotePic.Checked = true;
                addKey.Checked = true;
            }
            //添加时进行初始化
            else
            {
                //发布时，默认选中全国
                ProvinceAndCityList1.SetProvinceId = 35;
                addWater.Checked = true;
                DelLink.Checked = true;
                downloadRemotePic.Checked = true;
                addKey.Checked = true;
                string NewSource = EyouSoft.Common.ConfigModel.ConfigClass.GetConfigString("NewsSource", MasterUserInfo.ID.ToString());
                if (!string.IsNullOrEmpty(NewSource) && NewSource.IndexOf(',') > 0)
                {
                    txtNewsSource.Value = NewSource.Split(',')[0];
                    txtNewsArticle.Value = NewSource.Split(',')[1];
                }
            }
        }

        /// <summary>
        /// 保存后再添加，初始化
        /// </summary>
        private void SaveInit()
        {
            selTitleColor.Items.FindByValue(Utils.GetFormValue("selTitleColor")).Selected = false;
            selTitleColor.Items[0].Selected = true;
            BindRecPosition();
            BindAfficheSource();
            FCK_PlanTicketContent.Value = "";
            //新闻标题
            txtNewsTitle.Value = "";
            //标题颜色
            lblImagePath.Text = "";
            txtNewsKeys.Value = "";
            txtNewsKey.Value = "";
            txtNewsTags.Value = "";
            txtNewsTag.Value = "";
            hidNewsKeysId.Value = "";
            hidNewsTagsId.Value = "";
            txtNewsDesc.Value = "";
            drumpUr2.Value = "";
        }

        /// <summary>
        /// 新闻修改
        /// </summary>
        private void DataSave()
        {
            #region 实例化
            //分页标示符
            string FengYeSign = string.Empty;
            switch (Request.Browser.Browser)
            {
                case "Firefox":
                    FengYeSign = "<div style=\"page-break-after: always;\"><span style=\"display: none;\">&nbsp;</span></div>";
                    break;
                case "IE":
                    FengYeSign = "<div style=\"page-break-after: always\"><span style=\"display: none\">&nbsp;</span></div>";
                    break;
                default:
                    FengYeSign = "<div style=\"page-break-after: always;\"><span style=\"display: none;\">&nbsp;</span></div>";
                    break;
            }
            //选中的关键字
            string[] NewsKeys1 = txtNewsKeys.Value.Split(' ');
            //填写的关键字
            string[] NewsKeys2 = txtNewsKey.Value.Split(' ');
            //选中的Tags
            string[] NewsTags1 = txtNewsTags.Value.Split(' ');
            //填写的Tags
            string[] NewsTags2 = txtNewsTag.Value.Split(' ');
            //新闻业务类
            EyouSoft.BLL.NewsStructure.NewsBll NewsBll = new EyouSoft.BLL.NewsStructure.NewsBll();
            //新闻综合实体
            EyouSoft.Model.NewsStructure.NewsModel NewsModel = new EyouSoft.Model.NewsStructure.NewsModel();
            //新闻关键字
            IList<EyouSoft.Model.NewsStructure.NewsSubItem> NewsKeyWordItem = new List<EyouSoft.Model.NewsStructure.NewsSubItem>();
            //Tag关联实体
            IList<EyouSoft.Model.NewsStructure.NewsSubItem> NewsTagItem = new List<EyouSoft.Model.NewsStructure.NewsSubItem>();
            //新闻内容分页列表实体
            IList<EyouSoft.Model.NewsStructure.NewsContent> NewsContentList = new List<EyouSoft.Model.NewsStructure.NewsContent>();
            //新闻的推荐位置
            IList<EyouSoft.Model.NewsStructure.RecPosition> RecPositionList = new List<EyouSoft.Model.NewsStructure.RecPosition>();
            #endregion

            #region 新闻基础信息

            //新闻编号
            int NewId = Utils.GetInt(Utils.GetQueryStringValue("Id"));
            //新闻主键编号
            NewsModel.Id = NewId;
            //新闻作者
            NewsModel.AfficheAuthor = txtNewsArticle.Value.Trim();
            //新闻描述
            NewsModel.AfficheDesc = txtNewsDesc.Value;
            //新闻的排序方式
            NewsModel.AfficheSort = (EyouSoft.Model.NewsStructure.AfficheSource)(Utils.GetInt(radSortList.SelectedValue));
            //新闻来源
            NewsModel.AfficheSource = txtNewsSource.Value;
            //新闻所属城市编号
            if (Utils.GetInt(CityId.Value) != 0)
            {
                NewsModel.CityId = Utils.GetInt(CityId.Value);
            }
            //新闻所属城市名称
            NewsModel.CityName = CityText.Value == "请选择" ? "" : CityText.Value;
            //新闻所属省份编号
            if (Utils.GetInt(ProvinceId.Value) != 0)
            {
                NewsModel.ProvinceId = Utils.GetInt(ProvinceId.Value);
            }
            //新闻所属省份名称
            NewsModel.ProvinceName = ProvinceText.Value == "请选择" ? "" : ProvinceText.Value;
            //新闻类别编号
            NewsModel.AfficheClass = Utils.GetInt(ddlNewsClass.SelectedValue);
            //新闻类别名称
            NewsModel.ClassName = ddlNewsClass.SelectedItem.Text;
            //新闻标题
            NewsModel.AfficheTitle = txtNewsTitle.Value;
            //操作人编号
            NewsModel.OperatorID = MasterUserInfo.ID;
            //操作人姓名
            NewsModel.OperatorName = MasterUserInfo.ContactName;
            //新闻图片路径
            NewsModel.PicPath = Utils.GetFormValue("SingleFileUpload1$hidFileName");
            if (string.IsNullOrEmpty(NewsModel.PicPath))
            {
                NewsModel.PicPath = ImagePath.Value;
            }
            else
            {
                NewsModel.PicPath = NewsModel.PicPath;
            }
            //新闻标题颜色代码
            NewsModel.TitleColor = selTitleColor.Value == "请选择" ? "" : selTitleColor.Value;
            //新闻内容
            Content = FCK_PlanTicketContent.Value;
            //添加时间
            NewsModel.IssueTime = System.DateTime.Now;
            if (DelLink.Checked)
            {
                DelSiteOutLink();
            }
            if (downloadRemotePic.Checked)
            {
                DownSource();
            }
            if (addKey.Checked)
            {
                ContentAddKeys();
            }
            NewsModel.AfficheContent = Content;

            #endregion

            #region 新闻推荐位置类别
            NewsModel.GotoUrl = "";
            foreach (ListItem item in chkRecPositionList.Items)
            {
                if (item.Selected)
                {
                    //当推荐位置为URL跳转，跳转到的URL
                    if ((EyouSoft.Model.NewsStructure.RecPosition)(Utils.GetInt(item.Value)) == EyouSoft.Model.NewsStructure.RecPosition.URL跳转)
                    {
                        if (!drumpUr2.Value.ToLower().StartsWith("http://"))
                        {
                            drumpUr2.Value = "http://" + drumpUr2.Value;
                        }
                        NewsModel.GotoUrl = drumpUr2.Value == "请输入跳转的网址" ? "" : drumpUr2.Value;
                    }
                    RecPositionList.Add((EyouSoft.Model.NewsStructure.RecPosition)(Utils.GetInt(item.Value)));
                }
            }
            NewsModel.RecPositionId = RecPositionList;
            #endregion

            #region 新闻关键字，Tag关联实体
            //选中的关键字
            for (int i = 0; i < NewsKeys1.Length; i++)
            {
                if (NewsKeys1[i] != "")
                {
                    EyouSoft.Model.NewsStructure.NewsSubItem NewsSubItem = new EyouSoft.Model.NewsStructure.NewsSubItem();
                    NewsSubItem.ItemId = Utils.GetInt(hidNewsKeysId.Value.Split(',')[i]);
                    NewsSubItem.ItemName = NewsKeys1[i];
                    NewsSubItem.ItemType = EyouSoft.Model.NewsStructure.ItemCategory.KeyWord;
                    NewsKeyWordItem.Add(NewsSubItem);
                }
            }
            //填写的关键字
            if (chkSaveKeys.Checked)
            {
                for (int i = 0; i < NewsKeys2.Length; i++)
                {
                    //该填写的关键字是否选过
                    bool isZxist = false;
                    foreach (var item in NewsKeyWordItem)
                    {
                        if (NewsKeys2[i] == item.ItemName)
                        {
                            isZxist = true;
                            break;
                        }
                    }
                    if (NewsKeys2[i] != "" && NewsKeys2[i] != "多个用空格隔开" && !isZxist)
                    {
                        EyouSoft.Model.NewsStructure.NewsSubItem NewsSubItem = new EyouSoft.Model.NewsStructure.NewsSubItem();
                        NewsSubItem.ItemId = 0;
                        NewsSubItem.ItemName = NewsKeys2[i];
                        NewsSubItem.ItemType = EyouSoft.Model.NewsStructure.ItemCategory.KeyWord;
                        NewsKeyWordItem.Add(NewsSubItem);
                    }
                }
            }
            //选中的Tags
            for (int i = 0; i < NewsTags1.Length; i++)
            {
                if (NewsTags1[i] != "")
                {
                    EyouSoft.Model.NewsStructure.NewsSubItem NewsSubItem = new EyouSoft.Model.NewsStructure.NewsSubItem();
                    NewsSubItem.ItemId = Utils.GetInt(hidNewsTagsId.Value.Split(',')[i]);
                    NewsSubItem.ItemName = NewsTags1[i];
                    NewsSubItem.ItemType = EyouSoft.Model.NewsStructure.ItemCategory.Tag;
                    NewsTagItem.Add(NewsSubItem);
                }
            }
            //填写的Tags
            if (chkSaveTags.Checked)
            {
                for (int i = 0; i < NewsTags2.Length; i++)
                {
                    //该填写的Tag是否选过
                    bool isZxist = false;
                    foreach (var item in NewsTagItem)
                    {
                        if (NewsTags2[i] == item.ItemName)
                        {
                            isZxist = true;
                            break;
                        }
                    }
                    if (NewsTags2[i] != "" && NewsTags2[i] != "多个用空格隔开" && !isZxist)
                    {
                        EyouSoft.Model.NewsStructure.NewsSubItem NewsSubItem = new EyouSoft.Model.NewsStructure.NewsSubItem();
                        NewsSubItem.ItemId = 0;
                        NewsSubItem.ItemName = NewsTags2[i];
                        NewsSubItem.ItemType = EyouSoft.Model.NewsStructure.ItemCategory.Tag;
                        NewsTagItem.Add(NewsSubItem);
                    }
                }
            }
            NewsModel.NewsTagItem = NewsTagItem;
            NewsModel.NewsKeyWordItem = NewsKeyWordItem;
            #endregion

            #region 新闻的排序方式
            NewsModel.AfficheSort = (EyouSoft.Model.NewsStructure.AfficheSource)(Utils.GetInt(radSortList.SelectedValue));
            switch (NewsModel.AfficheSort)
            {
                case EyouSoft.Model.NewsStructure.AfficheSource.置顶半年:
                    NewsModel.UpdateTime = DateTime.Now.AddMonths(6);
                    break;
                case EyouSoft.Model.NewsStructure.AfficheSource.置顶三个月:
                    NewsModel.UpdateTime = DateTime.Now.AddMonths(3);
                    break;
                case EyouSoft.Model.NewsStructure.AfficheSource.置顶一个月:
                    NewsModel.UpdateTime = DateTime.Now.AddMonths(1);
                    break;
                case EyouSoft.Model.NewsStructure.AfficheSource.置顶一年:
                    NewsModel.UpdateTime = DateTime.Now.AddMonths(12);
                    break;
                case EyouSoft.Model.NewsStructure.AfficheSource.置顶一周:
                    NewsModel.UpdateTime = DateTime.Now.AddDays(7);
                    break;
                default: break;
            }
            #endregion

            #region 新闻内容分页实体
            string[] AfficheContent = System.Text.RegularExpressions.Regex.Split(NewsModel.AfficheContent, FengYeSign, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            for (int i = 0; i < AfficheContent.Length; i++)
            {
                EyouSoft.Model.NewsStructure.NewsContent NewsContent = new EyouSoft.Model.NewsStructure.NewsContent();
                if (!string.IsNullOrEmpty(AfficheContent[i]))
                {
                    NewsContent.Content = AfficheContent[i];
                    NewsContent.PageIndex = i + 1;
                    NewsContentList.Add(NewsContent);

                }
            }
            NewsModel.AffichePageNum = NewsContentList.Count;
            NewsModel.NewsContent = NewsContentList;
            #endregion

            #region 保存
            //没有新闻ID，则添加
            if (NewId == 0)
            {
                switch (NewsModel.AfficheSort)
                {
                    case EyouSoft.Model.NewsStructure.AfficheSource.默认排序:
                        NewsModel.UpdateTime = DateTime.Now;
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶半年:
                        NewsModel.UpdateTime = DateTime.Now.AddMonths(6);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶三个月:
                        NewsModel.UpdateTime = DateTime.Now.AddMonths(3);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一个月:
                        NewsModel.UpdateTime = DateTime.Now.AddMonths(1);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一年:
                        NewsModel.UpdateTime = DateTime.Now.AddMonths(12);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一周:
                        NewsModel.UpdateTime = DateTime.Now.AddDays(7);
                        break;
                    default: break;
                }
                NewsModel.ModifyTime = DateTime.Now;
                NewsModel.IssueTime = DateTime.Now;
                if (NewsBll.AddNews(NewsModel))
                {
                    this.btnSave.Visible = true;
                    SaveInit();
                    MessageBox.ResponseScript(this, "if(!confirm('添加成功,是否继续发布 !')){ window.location.href='NewsList.aspx'}");
                }
            }
            //有新闻ID，则修改
            else if (NewId > 0)
            {
                NewsModel.UpdateTime = Utils.GetDateTime(UpdateTime.Value);
                NewsModel.ModifyTime = NewsModel.UpdateTime;
                switch (NewsModel.AfficheSort)
                {
                    case EyouSoft.Model.NewsStructure.AfficheSource.默认排序:
                        NewsModel.UpdateTime = NewsModel.UpdateTime;
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶半年:
                        NewsModel.UpdateTime = NewsModel.UpdateTime.AddMonths(6);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶三个月:
                        NewsModel.UpdateTime = NewsModel.UpdateTime.AddMonths(3);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一个月:
                        NewsModel.UpdateTime = NewsModel.UpdateTime.AddMonths(1);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一年:
                        NewsModel.UpdateTime = NewsModel.UpdateTime.AddMonths(12);
                        break;
                    case EyouSoft.Model.NewsStructure.AfficheSource.置顶一周:
                        NewsModel.UpdateTime = NewsModel.UpdateTime.AddDays(7);
                        break;
                    default: break;
                }
                if (NewsBll.UpdateNews(NewsModel))
                {
                    this.btnSave.Visible = true;
                    MessageBox.ShowAndRedirect(this, "修改成功", "NewsList.aspx");
                }
            }
            #endregion
        }

        #endregion

        #region 附加项操作
        /// <summary>
        /// 删除站外链接
        /// </summary>
        private void DelSiteOutLink()
        {
            //获取所有链接正则表达式
            string reg = @"<a[\s\S]*?>[\s\S]*?</a>";
            //获取链接文本的正则表达式
            string regText = @"(?<=<a[\s\S]*?>)[\s\S]*?(?=</a>)";
            //获取<a>标签里的链接
            string regHref = "(?<=a[\\s\\S]*?href=\")[\\s\\S]*?(?=\")";
            Content = formatHTML(Content);
            List<string> list = parseHref(Content, reg);
            if (list != null)
            {
                foreach (string var in list)
                {
                    //判断是否是站内链接
                    try
                    {
                        if (IsSiteOutLink(parseHref(var, regHref)[0]))
                        {
                            //去除站外链接
                            Content = Content.Replace(var, parseHref(var, regText)[0]);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 下载远程图片和资源
        /// </summary>
        private void DownSource()
        {
            //用于查找图片的正则表达式
            string reg = "(?<=<img[\\s\\S]*?src=\")[\\s\\S]*?(?=\")";
            //用于取返回消息的正则表达
            string regMsg = "(?<=<msg>).*?(?=</msg>)";
            //list多个图片地址
            List<string> list = parseHref(Content, reg);
            if (list != null)
            {
                foreach (string var in list)
                {
                    //得到下载保存返回的路径
                    string path = parseHref(DownSaveFile(var, addWater.Checked), regMsg)[0];
                    //如果未登录，则跳转到登录页面
                    if (path == "请重新登录")
                    {
                        Utils.ShowAndRedirect("请重新登录", "/Login.aspx");
                    }
                    //如果返回路径为空，说明没有下载成功或水印添加失败，则删除该图片
                    if (path == "")
                    {
                        List<string> temp = parseHref(Content, string.Format("<[^>]*?src=\"{0}\"?[\\s\\S]*?><.*?>|<[^>]*?src=\"{0}\"?[\\s\\S]*?/>", var));
                        if (temp.Count > 0)
                        {
                            Content = Content.Replace(temp[0], "");
                        }
                        temp = null;
                    }
                    if (!string.IsNullOrEmpty(path) && path != var)
                    {
                        //用新的文件路径替换以前的文件路径
                        Content = Content.Replace(var, path);
                    }
                }
            }
        }

        /// <summary>
        /// 内容添加关键字
        /// </summary>
        private void ContentAddKeys()
        {
            //计数器,一篇文章最多加5个关键字
            int i = 0;
            //每篇文章加关键字的最大数量
            int count = 5;
            //开始查找位置
            int startIndex = 0;
            //每隔40个字符加一个关键字
            int step = 40;
            //已处理过的关键字
            System.Collections.Hashtable KeyEd = new System.Collections.Hashtable();
            Hashtable replay = new Hashtable();

            EyouSoft.BLL.NewsStructure.TagKeyInfo TagKeyInfoBll = new EyouSoft.BLL.NewsStructure.TagKeyInfo();
            //得到所有的关键字
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> list = TagKeyInfoBll.GetAllKeyWord();
            //得到内容中的所有关键字
            IList<EyouSoft.Model.NewsStructure.TagKeyInfo> AllKeyListInContent = new List<EyouSoft.Model.NewsStructure.TagKeyInfo>();
            //用指定的字符串实例化StringBuilder
            StringBuilder strBuilder = null;
            //在加关键字之前先删除以前的关键字链接
            string reg = "";
            string NewsKeys = txtNewsKeys.Value.Trim() + ' ' + txtNewsKey.Value.Trim();
            if (!string.IsNullOrEmpty(NewsKeys.Trim(' ')))
            {
                reg = NewsKeys.Trim(' ').Replace(' ', '|');
            }
            if (!string.IsNullOrEmpty(reg))
            {
                reg = reg.TrimEnd('|');
                List<string> list2 = parseHref(Content, reg);
                if (list2 != null && list != null)
                {
                    foreach (string str in list2)
                    {
                        foreach (EyouSoft.Model.NewsStructure.TagKeyInfo item in list)
                        {
                            if (str == item.ItemName)
                            {
                                AllKeyListInContent.Add(item);
                                string attrReg = string.Format("(?<==\"[^=<>]*?){0}(?=[^=<>]*?\")", item.ItemName);
                                string linkReg = string.Format("(?<=<a[\\s\\S]*?>[\\s\\S]*?){0}(?=[\\s\\S]*?</a>)", item.ItemName);
                                string randStr = GenerateRandomString(item.ItemName.Length);
                                while (replay.ContainsKey(randStr))
                                {
                                    randStr = GenerateRandomString(item.ItemName.Length);
                                }
                                if (!replay.ContainsValue(item.ItemName))
                                {
                                    Content = System.Text.RegularExpressions.Regex.Replace(Content, attrReg, randStr);
                                    Content = System.Text.RegularExpressions.Regex.Replace(Content, linkReg, randStr);
                                    replay.Add(randStr, item.ItemName);
                                }
                                break;
                            }
                        }
                    }
                }
            }

            strBuilder = new StringBuilder(Content);
            //在加关键字之前先删除以前的关键字链接
            if (list != null)
            {
                foreach (EyouSoft.Model.NewsStructure.TagKeyInfo item in list)
                {
                    string temp = string.Empty;
                    List<string> templist = parseHref(Content, string.Format("<a[^>]*?>{0}</a>", item.ItemName));
                    if (templist != null && templist.Count > 0)
                    {
                        temp = templist[0];
                    }
                    if (!string.IsNullOrEmpty(temp))
                    {
                        strBuilder = strBuilder.Replace(temp, item.ItemName);
                    }
                }
            }
            int length = 0;
            if (AllKeyListInContent != null)
            {
                foreach (EyouSoft.Model.NewsStructure.TagKeyInfo item in AllKeyListInContent)
                {
                    //如果替换大于等于最大次数，则跳出
                    if (i >= count || startIndex > strBuilder.Length)
                    {
                        break;
                    }
                    //开始替换的位置
                    int index = strBuilder.ToString().IndexOf(item.ItemName, startIndex);
                    if (index == -1 && startIndex >= strBuilder.Length) { break; };
                    if ((index - length) < step && length > 0)
                    {
                        //下次查找位置的开始
                        startIndex += item.ItemName.Length;
                        continue;
                    }
                    if (index > -1)
                    {
                        //要替换成的字符串
                        string newString = string.Format("<a InnerLinkId=\"{0}\" {3} href=\"{1}\">{2}</a>", item.Id, item.ItemUrl == "" ? "javascript:" : item.ItemUrl, item.ItemName, string.IsNullOrEmpty(item.ItemUrl) ? "" : "target=\"blank\"");
                        //替换的字符串
                        string oldString = item.ItemName;
                        //判断是否存在相应的关键字
                        if (!KeyEd.ContainsKey(item.ItemName))
                        {
                            //替换指定开始位置到指定长度内的所有字符串
                            strBuilder.Replace(oldString, newString, index, item.ItemName.Length);
                            //将已处理的关键字存入哈希表，避免重复替换
                            KeyEd.Add(item.ItemName, item.Id);
                            //计数器加1
                            i++;
                            length = index + newString.Length;
                            //下次查找位置的开始
                            startIndex = length;
                        }
                        else
                        {
                            //下次查找位置的开始
                            startIndex = index;
                        }
                    }
                }
            }
            System.Collections.IDictionaryEnumerator enumerator1 = replay.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                string key = enumerator1.Key.ToString();
                string valeu = enumerator1.Value.ToString();
                strBuilder.Replace(key, valeu);
            }
            Content = strBuilder.ToString();
        }

        /// <summary>
        /// 根据URL下载图片，并返回地址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isAddWater"></param>
        /// <returns></returns>
        private string DownSaveFile(string url, bool isAddWater)
        {
            //读取流的编码格式
            string Charset = "UTF-8";
            //要提交的参数
            string strParams = string.Format("UrlAdd={0}&isAddWater={1}", url, isAddWater ? "1" : "0");
            HttpWebRequest httpRequest = null;
            httpRequest = (HttpWebRequest)WebRequest.Create(Domain.FileSystem + "/SiteOperation/UploadByUrl.aspx?" + strParams);
            //以GET方式提交
            httpRequest.Method = "GET";
            //加上当前登录后的Cookie
            httpRequest.Headers[HttpRequestHeader.Cookie] = HttpContext.Current.Request.Headers["Cookie"];
            HttpWebResponse response = (HttpWebResponse)httpRequest.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(Charset));
            //返回响应字符串
            return responseReader.ReadToEnd();
        }

        /// <summary>
        /// 判断Url是不是站外链接
        /// </summary>
        /// <param name="Url"></param>
        /// <returns>True:站外链接 False:站内链接</returns>
        private bool IsSiteOutLink(string Url)
        {
            string curUrl = Request.Url.Host;
            if (curUrl != "localhost")
            {
                curUrl = curUrl.Substring(curUrl.IndexOf('.') + 1);
            }
            if (!string.IsNullOrEmpty(Url))
            {
                try
                {
                    Uri uri = new Uri(Url);
                    if (uri.Host.Substring(uri.Host.IndexOf('.') + 1) != curUrl)
                    {
                        return true;
                    } if (uri.Host == "localhost" || parseHref(uri.Host, @"192\.168\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])").Count != 0)
                    {
                        return false;
                    }
                }
                catch
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据正则表达式，返回该表达式匹配的所有项
        /// </summary>
        /// <param name="html"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public List<string> parseHref(string html, string pattern)
        {
            List<string> results = new List<string>();
            if (string.IsNullOrEmpty(html) || string.IsNullOrEmpty(pattern)) return null;
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(pattern);
            //开始编译
            System.Text.RegularExpressions.MatchCollection mc = reg.Matches(html);
            foreach (System.Text.RegularExpressions.Match ma in mc)
            {
                string temp = string.Empty;
                //返回多组数据Group[1]...
                for (int i = 0; i < ma.Groups.Count; i++)
                {
                    temp += ma.Groups[i].Value + "|";
                }
                temp = temp.TrimEnd('|');
                results.Add(temp);
            }
            return results;
        }

        /// <summary>
        /// 描述:格式化网页源码,去除一些不必要的标记，以保证提取数据的完整性
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        public string formatHTML(string htmlContent)
        {
            string result = "";
            result = htmlContent.Replace("&raquo;", "").
                Replace("&copy;", "").Replace("\r", "").Replace("\t", "")
                    .Replace("\n", "").Replace("&amp;", "&");
            return result;
        }

        /// <summary>
        /// 生成一个指定长度的字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        static public string GenerateRandomString(int length)
        {
            string s = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random(DateTime.Now.Millisecond);
            while (reValue.Length < length)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return "$" + reValue + "$";
        }
        #endregion

        #region 控件数据绑定

        /// <summary>
        /// 绑定新闻类别
        /// </summary>
        private void BindNewsClass()
        {
            EyouSoft.BLL.NewsStructure.NewsType NewsType = new EyouSoft.BLL.NewsStructure.NewsType();
            IList<EyouSoft.Model.NewsStructure.NewsType> list = NewsType.GetAllType();
            if (list != null && list.Count > 0)
            {
                ddlNewsClass.DataTextField = "ClassName";
                ddlNewsClass.DataValueField = "Id";
                ddlNewsClass.DataSource = list;
                ddlNewsClass.DataBind();
                this.ddlNewsClass.Items.Insert(0, new ListItem("请选择", ""));
            }
            NewsType = null;
            list = null;
        }

        /// <summary>
        /// 绑定文章排序
        /// </summary>
        private void BindAfficheSource()
        {
            System.Collections.Generic.List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.NewsStructure.AfficheSource));
            if (list != null && list.Count > 0)
            {
                this.radSortList.DataTextField = "Text";
                this.radSortList.DataValueField = "Value";
                this.radSortList.DataSource = list;
                this.radSortList.DataBind();
                this.radSortList.Items[0].Selected = true;
            }
            list = null;
        }

        /// <summary>
        /// 绑定推荐位置
        /// </summary>
        private void BindRecPosition()
        {
            System.Collections.Generic.List<EnumObj> list = EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.NewsStructure.RecPosition));
            if (list != null && list.Count > 0)
            {
                //去除其它
                list.RemoveAt(list.Count - 1);
                this.chkRecPositionList.DataTextField = "Text";
                this.chkRecPositionList.DataValueField = "Value";
                this.chkRecPositionList.DataSource = list;
                this.chkRecPositionList.DataBind();
                foreach (ListItem item in chkRecPositionList.Items)
                {
                    //如果为URL跳转，则点击后出现文本框，填写跳转到的URL
                    if (EyouSoft.Model.NewsStructure.RecPosition.URL跳转 == (EyouSoft.Model.NewsStructure.RecPosition)Utils.GetInt(item.Value))
                    {
                        item.Attributes.Add("onclick", "showInput();");
                        break;
                    }
                }

            }
            list = null;
        }

        #endregion
    }
}