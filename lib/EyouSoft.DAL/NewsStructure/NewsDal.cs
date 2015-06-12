using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.SystemStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.NewsStructure
{
    /// <summary>
    /// 创建人：郑知远 2011-03-31
    /// 描述：新闻中心数据层
    /// </summary>
    public class NewsDal : DALBase,IDAL.NewsStructure.INewsDal
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public NewsDal()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SQL定义
        const string SQL_Affiche_SELECT = "SELECT ID,OperatorID,OperatorName,AfficheClass,AfficheTitle,TitleColor,AfficheInfo,Clicks,IsHot,IsPicNews,PicPath,AfficheDesc,AfficheSource,AfficheAuthor,AffichePageNum,ProvinceId,ProvinceName,CityId,CityName,RecPositionId,OtherItem,AfficheSort,IssueTime,UpdateTime from tbl_Affiche order by IsHot DESC,IssueTime DESC";
        const string SQL_Affiche_INSERT = "INSERT INTO tbl_Affiche(OperatorID,OperatorName,AfficheClass,AfficheTitle,TitleColor,AfficheInfo,Clicks,IsHot,IsPicNews,PicPath,AfficheDesc,AfficheSource,AfficheAuthor,AffichePageNum,ProvinceId,ProvinceName,CityId,CityName,RecPositionId,OtherItem,AfficheSort,IssueTime,UpdateTime) values(@OperatorID,@OperatorName,@AfficheClass,@AfficheTitle,@TitleColor,@AfficheInfo,@Clicks,@IsHot,@IsPicNews,@PicPath,@AfficheDesc,@AfficheSource,@AfficheAuthor,@AffichePageNum,@ProvinceId,@ProvinceName,@CityId,@CityName,@RecPositionId,@OtherItem,@AfficheSort,@IssueTime,@UpdateTime);SELECT @@IDENTITY;";
        const string SQL_Affiche_UPDATE = "UPDATE tbl_Affiche set OperatorID=@OperatorID,OperatorName=@OperatorName,AfficheClass=@AfficheClass,AfficheTitle=@AfficheTitle,TitleColor=@TitleColor,AfficheInfo=@AfficheInfo,Clicks=@Clicks,IsHot=@IsHot,IsPicNews=@IsPicNews,PicPath=@PicPath,AfficheDesc=@AfficheDesc,AfficheSource=@AfficheSource,AfficheAuthor=@AfficheAuthor,AffichePageNum=@AffichePageNum,ProvinceId=@ProvinceId,ProvinceName=@ProvinceName,CityId=@CityId,CityName=@CityName,RecPositionId=@RecPositionId,OtherItem=@OtherItem,AfficheSort=@AfficheSort,IssueTime=@IssueTime,UpdateTime=@UpdateTime WHERE ID=@ID";
        const string SQL_Affiche_DELETE = "DELETE tbl_Affiche WHERE ID=@ID";
        const string SQL_Affiche_SELECT_ByID = @"SELECT tb1.ID,tb3.ID ContID,tb4.ClassName,GotoUrl,AfficheDesc,AfficheAuthor,TitleColor,AfficheTitle,
                                                AffichePageNum,tb1.IssueTime,AfficheSource,
                                                tb1.AfficheClass,tb1.AfficheCate,RecPositionId,tb3.[InfoContent],tb3.PageIndex 
                                                FROM tbl_Affiche tb1 
                                                LEFT JOIN tbl_AfficheContent tb3 ON tb1.ID = tb3.NewsId
                                                LEFT JOIN tbl_NewsClass tb4 ON tb1.AfficheClass = tb4.ID
                                                WHERE (tb3.PageIndex = @pageIndex or tb3.PageIndex is null) AND tb1.ID = @id";
        const string SQL_TagKeyInfo_SELECT_ByNewid = @"SELECT tb1.ID,tb1.ItemName,tb2.ID TKID,tb2.ItemUrl FROM tbl_AfficheSub tb1 
                                                        LEFT JOIN tbl_TagKeyList tb2 ON tb1.ItemId = tb2.ID
                                                        WHERE tb1.ItemType = @type AND tb1.NewsId = @newsid ";
        const string SQL_Affiche_UpdateClicks = "UPDATE tbl_Affiche SET Clicks = Clicks + 1 WHERE ID = @id";

        const string SQL_Affiche_SETTOP = "UPDATE tbl_Affiche set IsHot=@IsTop where ID=@ID";
        const string SQL_Affiche_SETREADCOUNT = "UPDATE tbl_Affiche set Clicks=Clicks+1 where ID=@ID";
        const string SQL_Affiche_GETTOPLIST = "SELECT {0} ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,Clicks from tbl_Affiche {1} order by IsHot DESC,IssueTime DESC";
        const string SQL_Affiche_GETMODEL = "SELECT ID,AfficheClass,AfficheTitle,AfficheInfo,IsHot,IsPicNews,PicPath,IssueTime,OperatorID,OperatorName,Clicks from tbl_Affiche where ID=@ID";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_Affiche] WHERE [ID]=@ID AND [PicPath]=@PicPath)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [PicPath] FROM [tbl_Affiche] WHERE [ID]=@ID AND PicPath<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [PicPath] FROM [tbl_Affiche] WHERE [ID]=@ID AND PicPath<>'';";
        #endregion

        #region 后台管理成员方法
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="model">新闻内容实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool AddNews(EyouSoft.Model.NewsStructure.NewsModel model)
        {
            return AddorUpdNews(model, "添加");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">公告信息实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateNews(EyouSoft.Model.NewsStructure.NewsModel model)
        {
            return AddorUpdNews(model, "修改");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool DelNews(int id)
        {
            DbCommand dc = this._database.GetStoredProcCommand("proc_Affiche_DelNews");                 //执行存储过程命令
            this._database.AddInParameter(dc, "@ID", DbType.Int32, id);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.NewsStructure.NewsModel GetModel(int ID)
        {
            EyouSoft.Model.NewsStructure.NewsModel model = null;                                        //新闻实体
            StringBuilder sbSQL = new StringBuilder();                                                  //SQL编辑器

            sbSQL.Append(" SELECT");
            sbSQL.Append("  ProvinceId");                                                               //省份ID
            sbSQL.Append("  ,ProvinceName");                                                            //省份名
            sbSQL.Append("  ,CityId");                                                                  //城市ID
            sbSQL.Append("  ,CityName");                                                                //城市名
            sbSQL.Append("  ,AfficheCate");                                                             //咨询类别
            sbSQL.Append("  ,AfficheTitle");                                                            //标题
            sbSQL.Append("  ,RecPositionId");                                                           //推荐位置
            sbSQL.Append("  ,TitleColor");                                                              //标题颜色
            sbSQL.Append("  ,AfficheClass");                                                            //新闻类型
            sbSQL.Append("  ,AfficheDesc");                                                             //新闻描述
            sbSQL.Append("  ,PicPath");                                                                 //图片路径
            sbSQL.Append("  ,AfficheSource");                                                           //新闻来源
            sbSQL.Append("  ,AfficheAuthor");                                                           //新闻作者
            sbSQL.Append("  ,OtherItem");                                                               //附加项
            sbSQL.Append("  ,AfficheInfo");                                                             //新闻内容
            sbSQL.Append("  ,AfficheSort");                                                             //文章排序
            sbSQL.Append("  ,GotoUrl");                                                                 //URL跳转
            sbSQL.Append("  ,UpdateTime");                                                                 //置顶时间
            sbSQL.Append(" FROM");
            sbSQL.Append("  tbl_Affiche");
            sbSQL.Append(" WHERE");
            sbSQL.Append("  ID = @ID");

            DbCommand dc = this._database.GetSqlStringCommand(sbSQL.ToString());                        //SQL执行
            this._database.AddInParameter(dc, "@ID", DbType.Int32, ID);                                 //参数设置

            //信息读取
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.NewsStructure.NewsModel();
                    model.ProvinceId = dr.IsDBNull(dr.GetOrdinal("ProvinceId")) ? 0 : dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    model.ProvinceName = dr.IsDBNull(dr.GetOrdinal("ProvinceName"))?"": dr.GetString(dr.GetOrdinal("ProvinceName"));
                    model.CityId = dr.IsDBNull(dr.GetOrdinal("CityId"))?0: dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.CityName = dr.IsDBNull(dr.GetOrdinal("CityName"))?"": dr.GetString(dr.GetOrdinal("CityName"));
                    model.AfficheCate = (EyouSoft.Model.NewsStructure.NewsCategory)dr.GetInt32(dr.GetOrdinal("AfficheCate"));
                    model.AfficheTitle = dr.IsDBNull(dr.GetOrdinal("AfficheTitle"))?"": dr.GetString(dr.GetOrdinal("AfficheTitle"));
                    model.RecPositionId = dr.IsDBNull(dr.GetOrdinal("RecPositionId"))?null: GetRecPosition(dr.GetString(dr.GetOrdinal("RecPositionId")));
                    model.TitleColor = dr.IsDBNull(dr.GetOrdinal("TitleColor")) ? "" : dr.GetString(dr.GetOrdinal("TitleColor"));
                    model.AfficheClass = dr.IsDBNull(dr.GetOrdinal("AfficheClass")) ? 0 : dr.GetByte(dr.GetOrdinal("AfficheClass"));
                    model.NewsKeyWordItem = GetTagKeys(ID,EyouSoft.Model.NewsStructure.ItemCategory.KeyWord);
                    model.NewsTagItem = GetTagKeys(ID, EyouSoft.Model.NewsStructure.ItemCategory.Tag);
                    model.AfficheDesc = dr.IsDBNull(dr.GetOrdinal("AfficheDesc"))?"": dr.GetString(dr.GetOrdinal("AfficheDesc"));
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath"))?"": dr.GetString(dr.GetOrdinal("PicPath"));
                    model.AfficheSource = dr.IsDBNull(dr.GetOrdinal("AfficheSource"))?"": dr.GetString(dr.GetOrdinal("AfficheSource"));
                    model.AfficheAuthor = dr.IsDBNull(dr.GetOrdinal("AfficheAuthor"))?"": dr.GetString(dr.GetOrdinal("AfficheAuthor"));
                    model.OtherItem = dr.IsDBNull(dr.GetOrdinal("OtherItem"))?null: GetOthers(dr.GetString(dr.GetOrdinal("OtherItem")));
                    model.AfficheContent = dr.IsDBNull(dr.GetOrdinal("AfficheInfo"))?"": dr.GetString(dr.GetOrdinal("AfficheInfo"));
                    model.AfficheSort = dr.IsDBNull(dr.GetOrdinal("AfficheSort")) ? EyouSoft.Model.NewsStructure.AfficheSource.默认排序 : (EyouSoft.Model.NewsStructure.AfficheSource)dr.GetByte(dr.GetOrdinal("AfficheSort"));
                    model.GotoUrl = dr.IsDBNull(dr.GetOrdinal("GotoUrl")) ? "" : dr.GetString(dr.GetOrdinal("GotoUrl"));
                    model.UpdateTime = dr.GetDateTime(dr.GetOrdinal("UpdateTime"));
                }
            }

            //返回新闻实体
            return model;
        }

        #endregion        

        #region 前台运用成员方法
        /// <summary>
        /// 设置浏览次数
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetClicks(int Id)
        {
            DbCommand comm = this._database.GetSqlStringCommand(SQL_Affiche_UpdateClicks);
            this._database.AddInParameter(comm, "@id", DbType.Int32, Id);

            return DbHelper.ExecuteSql(comm, this._database) > 0 ? true : false;
            
        }        
        /// <summary>
        /// 获取指定类别指定条数的新闻
        /// </summary>
        /// <param name="TopNum">需要返回的记录数</param>
        /// <param name="Category">资讯大类别 =null不做条件</param>
        /// <param name="QueryType">检索类别 =null不做条件</param>
        /// <param name="Position">推荐位置</param>
        /// <param name="NewsType">新闻类别编号 小于等于0时不做条件</param>
        /// <param name="IsPic">是否图片 =null不做条件</param>
        /// <param name="CurrNewsId">当前新闻信息编号 小于等于0时不做条件</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetTopNumNews(int TopNum
                                                                            , EyouSoft.Model.NewsStructure.NewsCategory? Category
                                                                            , EyouSoft.Model.NewsStructure.NewsQueryType? QueryType
                                                                            , EyouSoft.Model.NewsStructure.RecPosition? Position
                                                                            , int NewsType
                                                                            , bool? IsPic
                                                                            , int CurrNewsId)
        {
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> list =new List<EyouSoft.Model.NewsStructure.WebSiteNews>();            
            System.Text.StringBuilder strSQL = new StringBuilder();        
                           
            //热点资讯
            strSQL.Append("	SELECT TOP(@TOP)");
            strSQL.Append("		A.ID");
            strSQL.Append("		,A.AfficheCate");                                                       //咨询类别
            strSQL.Append("		,A.AfficheClass");                                                      //信息类型
            strSQL.Append("		,B.ClassName");                                                         //类型名称
            strSQL.Append("		,A.AfficheTitle");                                                      //公告标题
            strSQL.Append("		,A.TitleColor");                                                        //标题颜色
            strSQL.Append("		,A.Clicks");                                                            //阅读数
            strSQL.Append("		,A.PicPath");                                                           //图片路径
            strSQL.Append("		,A.AfficheDesc");                                                       //新闻描述
            strSQL.Append("		,A.RecPositionId");                                                     //推荐位置类别
            strSQL.Append("		,A.GotoUrl");                                                           //URL跳转
            strSQL.Append("		,A.IssueTime");                                                         //发布公告时间
            strSQL.Append("	FROM");
            strSQL.Append("		tbl_Affiche AS A");
            strSQL.Append("	LEFT OUTER JOIN");
            strSQL.Append("		tbl_NewsClass AS B");
            strSQL.Append("	ON");
            strSQL.Append("		B.ID = A.AfficheClass");
            switch (Position)
            {
                case EyouSoft.Model.NewsStructure.RecPosition.资讯头条:
                    //咨询头条
                    strSQL.Append("	WHERE");
                    strSQL.AppendFormat("		A.RecPositionId LIKE '%{0}%'", (int)Position);                               //咨询头条
                    strSQL.Append("	ORDER BY");
                    strSQL.Append("		A.UpdateTime DESC");
                    break;
                case EyouSoft.Model.NewsStructure.RecPosition.推荐至首页:
                    break;
                case EyouSoft.Model.NewsStructure.RecPosition.普通推荐资讯:
                    //普通推荐资讯
                    if (QueryType.Equals(EyouSoft.Model.NewsStructure.NewsQueryType.按普通推荐))
                    {
                        //相关资讯
                        if (CurrNewsId > 0)
                        {
                            strSQL.Append("	WHERE");
                            strSQL.Append("		A.AfficheClass = (SELECT AfficheClass FROM tbl_Affiche WHERE ID = @ID)");
                            strSQL.Append("	ORDER BY");
                            strSQL.Append("		A.UpdateTime DESC");
                        }
                        //推荐资讯
                        else
                        {
                            strSQL.Append("	WHERE");
                            strSQL.Append("		A.IsPicNews = " + (IsPic==true ? "1": "0"));                                  //新闻类型
                            strSQL.Append("	ORDER BY");
                            strSQL.Append("		A.UpdateTime DESC");
                        }
                    }
                    else
                    {
                        strSQL.Append("	WHERE 1 = 1 ");
                        if (Category != null)
                        {
                            strSQL.Append("	and A.AfficheCate = @CATE  ");                           //咨询类别
                        }
                        if (NewsType > 0)
                        {
                            strSQL.AppendFormat(" and A.AfficheClass = {0} ", NewsType);                   //新闻类型
                        }
                        //strSQL.AppendFormat("		A.RecPositionId LIKE '%{0}%'",(int)Position);   //咨询头条
                        strSQL.Append("	ORDER BY");
                        strSQL.AppendFormat("		(SELECT i.items FROM dbo.fn_Split(RecPositionId ,',') AS i WHERE i.items='{0}') DESC", (int)Position);
                        strSQL.Append("		,A.UpdateTime DESC");
                    }
                    break;
                case EyouSoft.Model.NewsStructure.RecPosition.其他:
                    //热点资讯排行
                    strSQL.Append("	WHERE");
                    strSQL.Append("		A.AfficheCate = @CATE");                                        //根据咨询类别查询热点咨询排行
                    //strSQL.Append("		AND A.RecPositionId LIKE '%@LIKE%'");                           //咨询头条
                    strSQL.Append("	ORDER BY");
                    if (QueryType == null)
                    {
                        strSQL.Append("		A.Clicks DESC");
                    }
                    else
                    {
                        strSQL.Append("		A.UpdateTime DESC");
                    }
                    break;
            }

            DbCommand dbCmd = this._database.GetSqlStringCommand(strSQL.ToString());                    //SQL执行

            this._database.AddInParameter(dbCmd, "@TOP", DbType.Int32, TopNum);                         //返回的记录数
            if (Position != null)
            {
                this._database.AddInParameter(dbCmd, "@LIKE", DbType.String, (int)Position);             //推荐位置
            }
            if (Category != null)
            {
                this._database.AddInParameter(dbCmd, "@CATE", DbType.Int32, (int)Category);             //咨询类别
            }
            if (NewsType != 0)
            {
                this._database.AddInParameter(dbCmd, "@TYPE", DbType.Byte, NewsType);                  //新闻类型
            }
            //是否图片
            if (IsPic != null)
            {
                this._database.AddInParameter(dbCmd, "@IsPic", DbType.String, IsPic == true ? "1" : "0");
            }
            if (CurrNewsId != 0)
            {
                this._database.AddInParameter(dbCmd, "@ID", DbType.Int32, CurrNewsId);                  //新闻ID
            }

            using (IDataReader dr = DbHelper.ExecuteReader(dbCmd, this._database))
            {
                while (dr.Read())
                {
                    //新闻实体
                    EyouSoft.Model.NewsStructure.WebSiteNews model = new EyouSoft.Model.NewsStructure.WebSiteNews();

                    model.Id = dr.GetInt32(dr.GetOrdinal("ID"));
                    if (!dr.IsDBNull(dr.GetOrdinal("AfficheCate")))
                    {
                        model.AfficheCate = (EyouSoft.Model.NewsStructure.NewsCategory)(dr.GetInt32(dr.GetOrdinal("AfficheCate")));
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("AfficheClass")))
                    {
                        model.AfficheClass = dr.GetByte(dr.GetOrdinal("AfficheClass"));
                    }
                    model.AfficheTitle = dr.IsDBNull(dr.GetOrdinal("AfficheTitle")) ? "" : dr.GetString(dr.GetOrdinal("AfficheTitle"));
                    model.ClassName = dr.IsDBNull(dr.GetOrdinal("ClassName")) ? "" : dr.GetString(dr.GetOrdinal("ClassName"));
                    model.TitleColor = dr.IsDBNull(dr.GetOrdinal("TitleColor")) ? "" : dr.GetString(dr.GetOrdinal("TitleColor"));
                    if (!dr.IsDBNull(dr.GetOrdinal("Clicks")))
                    {
                        model.Clicks = dr.GetInt32(dr.GetOrdinal("Clicks"));
                    }
                    model.PicPath = dr.IsDBNull(dr.GetOrdinal("PicPath")) ? "" : dr.GetString(dr.GetOrdinal("PicPath"));
                    model.AfficheDesc = dr.IsDBNull(dr.GetOrdinal("AfficheDesc")) ? "" : dr.GetString(dr.GetOrdinal("AfficheDesc"));
                    model.RecPositionId = dr.IsDBNull(dr.GetOrdinal("RecPositionId")) ? null : GetRecPosition(dr.GetString(dr.GetOrdinal("RecPositionId")));
                    model.GotoUrl = dr.IsDBNull(dr.GetOrdinal("GotoUrl")) ? "" : dr.GetString(dr.GetOrdinal("GotoUrl"));
                    if (!dr.IsDBNull(dr.GetOrdinal("IssueTime")))
                    {
                        model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    }

                    //新闻实体列表
                    list.Add(model);
                    model = null;
                }
            }

            //返回新闻实体
            return list;
        }
        /// <summary>
        /// 获取指定新闻详细信息
        /// </summary>
        /// <param name="Id">主键编号</param>
        /// <param name="pageIndex">当前内容页码</param>
        /// <returns></returns>
        public EyouSoft.Model.NewsStructure.WebSiteNews GetModel(int Id, int pageIndex)
        {
            EyouSoft.Model.NewsStructure.WebSiteNews item = new EyouSoft.Model.NewsStructure.WebSiteNews();

            DbCommand comm = this._database.GetSqlStringCommand(SQL_Affiche_SELECT_ByID);
            this._database.AddInParameter(comm, "@pageIndex", DbType.Int32, pageIndex);
            this._database.AddInParameter(comm, "@id", DbType.Int32, Id);
            IList<EyouSoft.Model.NewsStructure.RecPosition> listPosition = new List<EyouSoft.Model.NewsStructure.RecPosition>();
            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._database))
            {
                if (reader.Read())
                {
                    if (!reader.IsDBNull(reader.GetOrdinal("RecPositionId")))
                    {//读取推荐位置转换为RecPosition枚举，添加到listPosition
                        if (!string.IsNullOrEmpty(reader["RecPositionId"].ToString()))
                        {
                            string[] postionID = reader["RecPositionId"].ToString().Split(',');
                            for (int i = 0; i < postionID.Length; i++)
                            {
                                listPosition.Add((EyouSoft.Model.NewsStructure.RecPosition)
                                    Enum.Parse(typeof(EyouSoft.Model.NewsStructure.RecPosition), postionID[i]));
                            }
                        }
                    }
                    return new EyouSoft.Model.NewsStructure.WebSiteNews()
                    {
                        Id = (int)reader["ID"],
                        ClassName = reader["ClassName"].ToString(),
                        TitleColor = reader.IsDBNull(reader.GetOrdinal("TitleColor")) ? "" : reader["TitleColor"].ToString(),
                        AfficheTitle = reader.IsDBNull(reader.GetOrdinal("AfficheTitle")) ? "" : reader["AfficheTitle"].ToString(),
                        AfficheSource = reader.IsDBNull(reader.GetOrdinal("AfficheSource")) ? "" : reader["AfficheSource"].ToString(),
                        AfficheAuthor = reader.IsDBNull(reader.GetOrdinal("AfficheAuthor")) ? "" : reader["AfficheAuthor"].ToString(),
                        AffichePageNum = reader.IsDBNull(reader.GetOrdinal("AffichePageNum")) ? 0 : (int)reader["AffichePageNum"],
                        AfficheDesc = reader.IsDBNull(reader.GetOrdinal("AfficheDesc"))?"":reader["AfficheDesc"].ToString(),
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        GotoUrl = reader.IsDBNull(reader.GetOrdinal("GotoUrl"))?"":reader["GotoUrl"].ToString(),
                        NextNewsInfo = GetNewsOnOrNext(Id, false), //获取下一条新闻
                        PrevNewsInfo = GetNewsOnOrNext(Id, true), //获取上一条新闻
                        NewsKeyWordItem = GetTagKeyInfoById(Id, EyouSoft.Model.NewsStructure.ItemCategory.KeyWord), //获取关键字列表
                        NewsTagItem = GetTagKeyInfoById(Id, EyouSoft.Model.NewsStructure.ItemCategory.Tag),  //获取tag标签列表
                        ContentInfo = reader.IsDBNull(reader.GetOrdinal("ContID")) ? null : new EyouSoft.Model.NewsStructure.NewsContent()
                        {
                            Content = reader.IsDBNull(reader.GetOrdinal("InfoContent")) ? "" : reader["InfoContent"].ToString(),
                            Id = (int)reader["ContId"],
                            NewId = Id,
                            PageIndex = pageIndex
                        },
                        RecPositionId = listPosition,
                        AfficheClass = (byte)reader["AfficheClass"],
                        AfficheCate = (EyouSoft.Model.NewsStructure.NewsCategory)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.NewsCategory), reader["AfficheCate"].ToString())
                    };
                }
            }

            return null;
        }

        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int topNum,
            EyouSoft.Model.NewsStructure.NewsCategory cate,
            EyouSoft.Model.NewsStructure.RecPosition positioin
            ,int? tag
            ,int? newsType)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT TOP(@topNum) tb1.ID,AfficheTitle,AfficheClass,tb1.GotoUrl,tb1.PicPath,tb1.IssueTime,tb2.[InfoContent],UpdateTime FROM tbl_Affiche tb1
                         LEFT JOIN tbl_AfficheContent tb2 ON tb1.ID = tb2.[NewsId] ");
            if (tag != null)                                                //tag标签
                sql.Append(" LEFT JOIN tbl_AfficheSub tb3 ON tb1.ID = tb3.[NewsId] ");
            sql.Append(@"WHERE charindex(','+ltrim(@position)+',',','+tb1.RecPositionId+',') > 0 AND
	                    tb2.PageIndex = 1 AND tb1.AfficheCate = @cate");    // AND UpdateTime <= getdate()
            if (newsType != null)                                           //新闻小类别
                sql.Append("  AND tb1.AfficheClass = @afficheClass");
            if (tag != null)
                sql.Append(" AND tb3.ItemId = @tag AND tb3.ItemType = 1");  //tb3.ItemType = 1 指定获取Tag标签
            sql.Append(" ORDER BY tb1.UpdateTime DESC");
            IList<EyouSoft.Model.NewsStructure.NewsModel> list = new List<EyouSoft.Model.NewsStructure.NewsModel>();
            DbCommand comm = this._database.GetSqlStringCommand(sql.ToString());
            this._database.AddInParameter(comm, "@topNum", DbType.Int32, topNum);
            this._database.AddInParameter(comm, "@position", DbType.Int32, (int)positioin);
            this._database.AddInParameter(comm, "@cate", DbType.Int32, (int)cate);
            if (newsType != null)
                this._database.AddInParameter(comm, "@afficheClass", DbType.Int16, newsType);
            if (tag != null)
                this._database.AddInParameter(comm, "@tag", DbType.Int32, tag);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    list.Add(new EyouSoft.Model.NewsStructure.NewsModel()
                    {
                        Id = (int)reader["Id"],
                        AfficheTitle = reader["AfficheTitle"].ToString(),
                        AfficheContent = reader.IsDBNull(reader.GetOrdinal("InfoContent")) ? "" : reader["InfoContent"].ToString(),
                        PicPath = reader.IsDBNull(reader.GetOrdinal("PicPath")) ? "" : reader["PicPath"].ToString(),
                        GotoUrl = reader.IsDBNull(reader.GetOrdinal("GotoUrl")) ? "" : reader["GotoUrl"].ToString(),
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        UpdateTime = DateTime.Parse(reader["UpdateTime"].ToString()),
                        AfficheClass = reader.IsDBNull(reader.GetOrdinal("AfficheClass")) ? 0 : (byte)reader["AfficheClass"]
                    });
                }
            }

            return list;

        }

        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">查询实体</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.NewsModel> GetList(int pageSize
                                                                    , int pageIndex
                                                                    , ref int recordCount
                                                                    , EyouSoft.Model.NewsStructure.SearchOrderInfo info)
        {
            IList<EyouSoft.Model.NewsStructure.NewsModel> list = new List<EyouSoft.Model.NewsStructure.NewsModel>();
            string fileds = @"ID,OperatorID,OperatorName,AfficheClass,AfficheTitle,TitleColor,AfficheInfo,GotoUrl,Clicks,
                                PicPath,AfficheDesc,AfficheSource,AfficheAuthor,AffichePageNum,ProvinceId,ProvinceName,CityId,
                               CityName,RecPositionId,OtherItem,AfficheSort,IssueTime,UpdateTime,ClassName";
            #region 构建查询条件
            StringBuilder query = new StringBuilder();
            query.Append("1 = 1");
            if (info != null)
            {
                if (info.Type > 0)                                      //新闻小类别
                    query.Append(" AND AfficheClass = " + info.Type);
                if (!string.IsNullOrEmpty(info.Title))                  //新闻标题
                    query.Append(" AND AfficheTitle LIKE '%" + info.Title + "%'");
                if (info.Category != null)                              //新闻大类别
                    query.Append(" AND AfficheCate = " + (int)info.Category);
                if (info.IsPic != null)                                 //是否带图片
                    query.Append(" AND IsPicNews = " + (info.IsPic == true ? "1" : "0"));
                if (info.ProvinceId != null)                            //省份
                    query.Append(" AND ProvinceId = " + info.ProvinceId);
                if (info.Tag > 0)                                       //Tag标签
                    query.Append(" AND ID IN(SELECT NewsId FROM tbl_AfficheSub WHERE ItemId = " + info.Tag + " AND ItemType = 1)");
                if (info.Source != null)                                //文章排序
                    query.Append(" AND AfficheSort = " + (int)info.Source);
                if (info.IsRecPositionId != null)                       //是否推荐
                {
                    if (info.IsRecPositionId == true)
                    {
                        query.Append(" AND RecPositionID is not null AND RecPositionID <> ''");
                    }
                }
                if (info.City > 0)                                      //城市
                    query.Append(" AND CityId = " + info.City);
            }
            #endregion

            EyouSoft.Model.NewsStructure.NewsModel item = null;
            string[] recList; 

            using (IDataReader reader = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount,
                "view_Affiche", "ID", fileds, query.ToString(), "UpdateTime DESC"))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.NewsStructure.NewsModel()
                          {

                              Id = (int)reader["ID"],
                              OperatorID = reader.IsDBNull(reader.GetOrdinal("OperatorID")) ? 0 : (int)reader["OperatorID"],
                              ClassName = reader.IsDBNull(reader.GetOrdinal("ClassName")) ? "" : reader["ClassName"].ToString(),
                              OperatorName = reader.IsDBNull(reader.GetOrdinal("OperatorName")) ? "" : reader["OperatorName"].ToString(),
                              AfficheClass = (byte)reader["AfficheClass"],
                              AfficheTitle = reader.IsDBNull(reader.GetOrdinal("AfficheTitle")) ? "" : reader["AfficheTitle"].ToString(),
                              TitleColor = reader.IsDBNull(reader.GetOrdinal("TitleColor")) ? "" : reader["TitleColor"].ToString(),
                              AfficheContent = reader.IsDBNull(reader.GetOrdinal("AfficheInfo")) ? "" : reader["AfficheInfo"].ToString(),
                              Clicks = (int)reader["Clicks"],
                              PicPath = reader.IsDBNull(reader.GetOrdinal("PicPath")) ? "" : reader["PicPath"].ToString(),
                              AfficheDesc = reader.IsDBNull(reader.GetOrdinal("AfficheDesc")) ? "" : reader["AfficheDesc"].ToString(),
                              AfficheSource = reader.IsDBNull(reader.GetOrdinal("AfficheSource")) ? "" : reader["AfficheSource"].ToString(),
                              AfficheAuthor = reader.IsDBNull(reader.GetOrdinal("AfficheAuthor")) ? "" : reader["AfficheAuthor"].ToString(),
                              AffichePageNum = reader.IsDBNull(reader.GetOrdinal("AffichePageNum")) ? 0 : (int)reader["AffichePageNum"],
                              ProvinceId = reader.IsDBNull(reader.GetOrdinal("ProvinceId")) ? 0 : (int)reader["ProvinceId"],
                              ProvinceName = reader.IsDBNull(reader.GetOrdinal("ProvinceName")) ? "" : reader["ProvinceName"].ToString(),
                              CityId = reader.IsDBNull(reader.GetOrdinal("CityId")) ? 0 : (int)reader["CityId"],
                              CityName = reader.IsDBNull(reader.GetOrdinal("CityName")) ? "" : reader["CityName"].ToString(),
                              GotoUrl = reader.IsDBNull(reader.GetOrdinal("GotoUrl")) ? "" : reader["GotoUrl"].ToString(),
                              IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                              UpdateTime = DateTime.Parse(reader["UpdateTime"].ToString())
                          };
                    if (!reader.IsDBNull(reader.GetOrdinal("AfficheSort")))
                        item.AfficheSort = (EyouSoft.Model.NewsStructure.AfficheSource)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.AfficheSource), reader["AfficheSort"].ToString());
                    if (!reader.IsDBNull(reader.GetOrdinal("RecPositionId")))
                    {
                        if (!string.IsNullOrEmpty(reader["RecPositionId"].ToString()))
                        {
                            //推荐位置转换
                            item.RecPositionId = new List<EyouSoft.Model.NewsStructure.RecPosition>();
                            recList = reader["RecPositionId"].ToString().Split(',');
                            for (int i = 0; i < recList.Length; i++)
                            {
                                item.RecPositionId.Add((EyouSoft.Model.NewsStructure.RecPosition)
                                    Enum.Parse(typeof(EyouSoft.Model.NewsStructure.RecPosition), recList[i]));
                            }
                        }
                    }
                    list.Add(item);
                }
            }

            return list;
        }

        /// <summary>
        /// 指定条数获取新闻列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="cate">新闻大类别</param>
        /// <param name="positioin">推荐位置</param>
        /// <param name="tag">Tag标签</param>
        /// <param name="newsType">新闻小类别</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.NewsStructure.WebSiteNews> GetList(int topNum,
            EyouSoft.Model.NewsStructure.NewsCategory? cate,
            EyouSoft.Model.NewsStructure.RecPosition? positioin
            , int? tag
            , int? newsType)
        {
            var sql = new StringBuilder();
            sql.Append(@"SELECT TOP(@topNum) tb1.ID,AfficheTitle,AfficheClass,tb1.GotoUrl,tb1.PicPath,tb1.IssueTime,tb2.[InfoContent],UpdateTime FROM tbl_Affiche tb1
                         LEFT JOIN tbl_AfficheContent tb2 ON tb1.ID = tb2.[NewsId] ");
            if (tag != null)                                                //tag标签
                sql.Append(" LEFT JOIN tbl_AfficheSub tb3 ON tb1.ID = tb3.[NewsId] ");
            sql.Append(" where tb2.PageIndex = 1 ");
            if (cate.HasValue)
                sql.AppendFormat(" and tb1.AfficheCate = {0} ", (int)cate.Value);
            if(positioin.HasValue)
                sql.AppendFormat(" and charindex(','+ltrim({0})+',',','+tb1.RecPositionId+',') > 0 ", (int)positioin.Value);
            if (newsType != null)                                           //新闻小类别
                sql.Append("  AND tb1.AfficheClass = @afficheClass");
            if (tag != null)
                sql.Append(" AND tb3.ItemId = @tag AND tb3.ItemType = 1");  //tb3.ItemType = 1 指定获取Tag标签
            sql.Append(" ORDER BY tb1.UpdateTime DESC");
            IList<EyouSoft.Model.NewsStructure.WebSiteNews> list = new List<EyouSoft.Model.NewsStructure.WebSiteNews>();
            DbCommand comm = this._database.GetSqlStringCommand(sql.ToString());
            this._database.AddInParameter(comm, "@topNum", DbType.Int32, topNum);
            if (newsType != null)
                this._database.AddInParameter(comm, "@afficheClass", DbType.Int16, newsType);
            if (tag != null)
                this._database.AddInParameter(comm, "@tag", DbType.Int32, tag);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    list.Add(new EyouSoft.Model.NewsStructure.WebSiteNews()
                    {
                        Id = (int)reader["Id"],
                        AfficheTitle = reader["AfficheTitle"].ToString(),
                        PicPath = reader.IsDBNull(reader.GetOrdinal("PicPath")) ? "" : reader["PicPath"].ToString(),
                        GotoUrl = reader.IsDBNull(reader.GetOrdinal("GotoUrl")) ? "" : reader["GotoUrl"].ToString(),
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                        AfficheClass = reader.IsDBNull(reader.GetOrdinal("AfficheClass")) ? 0 : (byte)reader["AfficheClass"]
                    });
                }
            }

            return list;

        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 获取新闻的上一条/下一条数据
        /// </summary>
        /// <param name="newID">新闻编号</param>
        /// <param name="m">true:上一条 false:下一条</param>
        /// <returns>WebSiteNews实体对象</returns>
        private EyouSoft.Model.NewsStructure.WebSiteNews GetNewsOnOrNext(int newID, bool m)
        {
            string sql = string.Empty;
            if (m)
                sql = "SELECT TOP 1 ID,AfficheTitle FROM tbl_Affiche WHERE ID < @id ORDER BY ID DESC";
            else
                sql = "SELECT TOP 1 ID,AfficheTitle FROM tbl_Affiche WHERE ID > @id ORDER BY ID ASC";

            DbCommand comm = this._database.GetSqlStringCommand(sql);
            this._database.AddInParameter(comm, "@id", DbType.Int32, newID);

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                if (reader.Read())
                {
                    return new EyouSoft.Model.NewsStructure.WebSiteNews()
                    {
                        Id = (int)reader["ID"],
                        AfficheTitle = reader["AfficheTitle"].ToString()
                    };
                }
            }


            return null;
        }

        /// <summary>
        /// 获取新闻的关键字列表/tag标签列表
        /// </summary>
        /// <param name="newID">新闻编号</param>
        /// <param name="cate">项目类别</param>
        /// <returns>TagKeyInfo集合</returns>
        private IList<EyouSoft.Model.NewsStructure.NewsSubItem> GetTagKeyInfoById(int newID,
            EyouSoft.Model.NewsStructure.ItemCategory cate)
        {

            DbCommand comm = this._database.GetSqlStringCommand(SQL_TagKeyInfo_SELECT_ByNewid);
            this._database.AddInParameter(comm, "@type", DbType.Int32, (int)cate);
            this._database.AddInParameter(comm, "@newsid", DbType.Int32, newID);

            IList<EyouSoft.Model.NewsStructure.NewsSubItem> list = new List<EyouSoft.Model.NewsStructure.NewsSubItem>();

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    list.Add(new EyouSoft.Model.NewsStructure.NewsSubItem()
                    {
                        Id = (int)reader["Id"],
                        ItemName = reader["ItemName"].ToString(),
                        NewId = newID,
                        ItemType = cate,
                        ItemUrl = reader.IsDBNull(reader.GetOrdinal("ItemUrl")) ? "" : reader["ItemUrl"].ToString(),
                        ItemId = reader.IsDBNull(reader.GetOrdinal("TKID")) ? 0 : (int)reader["TKID"]
                    });
                }
            }

            return list;
        }

        /// <summary>
        /// 获取推荐位置数组
        /// </summary>
        /// <param name="strRecPosition">【，】隔开的推荐位置字符串</param>
        /// <returns>推荐位置数组</returns>
        private IList<EyouSoft.Model.NewsStructure.RecPosition> GetRecPosition(string strRecPosition)
        {
            IList<EyouSoft.Model.NewsStructure.RecPosition> list = new List<EyouSoft.Model.NewsStructure.RecPosition>();

            if (strRecPosition != null && strRecPosition.Trim()!="" && strRecPosition.Split(',').Count() > 0)
            {
                string[] strArry = strRecPosition.Split(',');
                for (int i = 0; i < strArry.Length; i++)
                {
                    list.Add((EyouSoft.Model.NewsStructure.RecPosition)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.RecPosition), strArry[i]));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取附加项数组
        /// </summary>
        /// <param name="strOthers">【，】隔开的附加项字符串</param>
        /// <returns>附加项数组</returns>
        private IList<EyouSoft.Model.NewsStructure.OtherItems> GetOthers(string strOthers)
        {
            IList<EyouSoft.Model.NewsStructure.OtherItems> list = new List<EyouSoft.Model.NewsStructure.OtherItems>();

            if (strOthers != null && strOthers.Trim()!="" && strOthers.Split(',').Count() > 0)
            {
                string[] strArry = strOthers.Split(',');
                for (int i = 0; i < strArry.Length; i++)
                {
                    list.Add((EyouSoft.Model.NewsStructure.OtherItems)Enum.Parse(typeof(EyouSoft.Model.NewsStructure.OtherItems), strArry[i]));
                }
            }

            return list;
        }

        /// <summary>
        /// 获取推荐位置【，】隔开的字符串
        /// </summary>
        /// <param name="lst">推荐位置枚举数组</param>
        /// <returns>推荐位置</returns>
        private string GetRecPosition(IList<EyouSoft.Model.NewsStructure.RecPosition> lst)
        {
            string strRtn = string.Empty;

            if (lst != null && lst.Count > 0)
            {
                foreach (EyouSoft.Model.NewsStructure.RecPosition en in lst)
                {
                    strRtn = strRtn + (int)en + ",";
                }
            }
            return strRtn.TrimEnd(',');
        }

        /// <summary>
        /// 获取附加项【，】隔开的字符串
        /// </summary>
        /// <param name="lst">附加项枚举数组</param>
        /// <returns>附加项</returns>
        private string GetOthers(IList<EyouSoft.Model.NewsStructure.OtherItems> lst)
        {
            string strRtn = string.Empty;

            if (lst != null && lst.Count > 0)
            {
                foreach (EyouSoft.Model.NewsStructure.OtherItems en in lst)
                {
                    strRtn = strRtn + (int)en + ",";
                }
            }
            return strRtn.TrimEnd(',');
        }

        /// <summary>
        /// 根据新闻类型和项目类别获取标签关键字列表
        /// </summary>
        /// <param name="intNewsId">新闻类型</param>
        /// <param name="cate">项目类别</param>
        /// <returns>标签关键字列表</returns>
        private IList<EyouSoft.Model.NewsStructure.NewsSubItem> GetTagKeys(int intNewsId
                                                                            ,EyouSoft.Model.NewsStructure.ItemCategory cate)
        {
            //标签关键字列表
            IList<EyouSoft.Model.NewsStructure.NewsSubItem> list = new List<EyouSoft.Model.NewsStructure.NewsSubItem>();
            System.Text.StringBuilder strSQL = new StringBuilder();                                     //SQL编辑器

            //热点资讯
            strSQL.Append("	SELECT");
            strSQL.Append("		ItemId");                                                               //关联编号
            strSQL.Append("		,ItemName");                                                            //关联名称
            strSQL.Append("	FROM");
            strSQL.Append("		tbl_AfficheSub");
            strSQL.Append("	WHERE");
            strSQL.Append("		NewsId = @NewsId");                                                     //新闻类型
            strSQL.Append("		AND ItemType = @ItemType");                                             //项目类别

            DbCommand dbCmd = this._database.GetSqlStringCommand(strSQL.ToString());                    //SQL执行

            this._database.AddInParameter(dbCmd, "@NewsId", DbType.Int32, intNewsId);                   //所属新闻编号
            this._database.AddInParameter(dbCmd, "@ItemType", DbType.Int32, (int)cate);                 //关联类型

            using (IDataReader dr = DbHelper.ExecuteReader(dbCmd, this._database))
            {
                while (dr.Read())
                {
                    //标签关键字实体
                    EyouSoft.Model.NewsStructure.NewsSubItem model = new EyouSoft.Model.NewsStructure.NewsSubItem();

                    model.ItemId = dr.GetInt32(dr.GetOrdinal("ItemId"));                                //关联编号
                    model.ItemName = dr.GetString(dr.GetOrdinal("ItemName"));                           //关联名称

                    //标签关键字实体
                    list.Add(model);
                    model = null;
                }
            }

            //标签关键字列表
            return list;
        }

        /// <summary>
        /// 根据标签关键字内容和项目类别判断是否存在
        /// </summary>
        /// <param name="intNewsType">新闻类型</param>
        /// <param name="strTagKey">标签关键字内容</param>
        /// <param name="enmCate">项目类别</param>
        /// <returns>True:存在 False:不存在</returns>
        private bool TagKeyExists(string strTagKey, EyouSoft.Model.NewsStructure.ItemCategory enmCate)
        {
            StringBuilder sbSql = new StringBuilder();                                                  //SQL编辑器

            sbSql.Append(" SELECT");
            sbSql.Append("      COUNT(1)");
            sbSql.Append(" FROM");
            sbSql.Append("      tbl_TagKeyList");
            sbSql.Append(" WHERE");
            //sbSql.AppendFormat("  NewsId = '{0}'", intNewsType);
            sbSql.AppendFormat("  ItemName = '{0}'", strTagKey);
            sbSql.AppendFormat("  AND Category = '{0}'", (int)enmCate);

            DbCommand dc = this._database.GetSqlStringCommand(sbSql.ToString());                       //SQL执行

            return DbHelper.Exists(dc, this._database);
        }

        /// <summary>
        /// 根据新闻类型获取咨询类别
        /// </summary>
        /// <param name="Type">新闻类型</param>
        /// <returns>咨询类别</returns>
        private int GetNewsCateByType(int Type)
        {
            int rtn = 0;                                                                                //咨询类别
            System.Text.StringBuilder strSQL = new StringBuilder();                                     //SQL编辑器

            //SQL编辑
            strSQL.Append("	SELECT");
            strSQL.Append("		Category");                                                             //咨询类别
            strSQL.Append("	FROM");
            strSQL.Append("		tbl_NewsClass");
            strSQL.Append("	WHERE");
            strSQL.Append("		Id = @Type");                                                           //新闻类型

            DbCommand dbCmd = this._database.GetSqlStringCommand(strSQL.ToString());                    //SQL执行

            this._database.AddInParameter(dbCmd, "@Type", DbType.Byte, Type);                           //新闻类型

            using (IDataReader dr = DbHelper.ExecuteReader(dbCmd, this._database))
            {
                while (dr.Read())
                {
                    rtn = dr.IsDBNull(dr.GetOrdinal("Category")) ? 0 : dr.GetByte(dr.GetOrdinal("Category"));
                }
            }

            //咨询类别
            return rtn;

        }

        /// <summary>
        /// 根据新闻实体添加、修改新闻
        /// </summary>
        /// <param name="model">新闻实体</param>
        /// <param name="Type">类型</param>
        /// <returns>false:失败 true:成功</returns>
        private bool AddorUpdNews(EyouSoft.Model.NewsStructure.NewsModel model,string Type)
        {
            StringBuilder sbXml = null;                                                                 //XML编辑器
            System.Xml.XmlDocument xml = null;                                                          //XML对象
            DbCommand dc = null;
            if (Type == "修改")
            {
                dc = this._database.GetStoredProcCommand("proc_Affiche_UpdateNews");                 //执行存储过程命令
                this._database.AddInParameter(dc, "@ID", DbType.Int32, model.Id);        //新闻ID
                this._database.AddInParameter(dc, "@DtUpdate", DbType.DateTime, model.ModifyTime);        //修改时间
            }
            else
            {
                dc = this._database.GetStoredProcCommand("proc_Affiche_AddNews");                 //执行存储过程命令
            }

            this._database.AddInParameter(dc, "@OperatorID", DbType.Int32, model.OperatorID);           //操作员编号
            this._database.AddInParameter(dc, "@OperatorName", DbType.String, model.OperatorName);      //操作员姓名
            this._database.AddInParameter(dc, "@AfficheClass", DbType.Byte, model.AfficheClass);        //信息类型
            this._database.AddInParameter(dc, "@AfficheTitle", DbType.String, model.AfficheTitle);      //公告标题
            this._database.AddInParameter(dc, "@TitleColor", DbType.String, model.TitleColor);          //标题颜色
            this._database.AddInParameter(dc, "@AfficheInfo", DbType.String, model.AfficheContent);     //公告内容
            this._database.AddInParameter(dc, "@Clicks", DbType.Int32, model.Clicks);                   //阅读数
            this._database.AddInParameter(dc, "@IsHot", DbType.AnsiStringFixedLength, "0");             //是否热门
            this._database.AddInParameter(dc, "@IsPicNews", DbType.AnsiStringFixedLength, model.PicPath != null && model.PicPath.Trim() != "" ? "1" : "0");         //是否图片新闻
            this._database.AddInParameter(dc, "@PicPath", DbType.String, model.PicPath);                //图片路径
            this._database.AddInParameter(dc, "@AfficheDesc", DbType.String, model.AfficheDesc);        //新闻描述
            this._database.AddInParameter(dc, "@AfficheSource", DbType.String, model.AfficheSource);    //新闻来源
            this._database.AddInParameter(dc, "@AfficheAuthor", DbType.String, model.AfficheAuthor);    //新闻作者
            this._database.AddInParameter(dc, "@AffichePageNum", DbType.Int32, model.AffichePageNum);   //新闻总页数
            this._database.AddInParameter(dc, "@ProvinceId", DbType.Int32, model.ProvinceId);           //区域省份编号
            this._database.AddInParameter(dc, "@ProvinceName", DbType.String, model.ProvinceName);      //区域省份名称
            this._database.AddInParameter(dc, "@CityId", DbType.Int32, model.CityId);                   //区域城市编号
            this._database.AddInParameter(dc, "@CityName", DbType.String, model.CityName);              //区域城市名称
            this._database.AddInParameter(dc, "@RecPositionId", DbType.String, GetRecPosition(model.RecPositionId));      //推荐位置类别
            this._database.AddInParameter(dc, "@OtherItem", DbType.String, GetOthers(model.OtherItem));         //附加项
            this._database.AddInParameter(dc, "@AfficheSort", DbType.Byte, model.AfficheSort);          //文章排序
            this._database.AddInParameter(dc, "@GotoUrl", DbType.String, model.GotoUrl);                //URL跳转
            this._database.AddInParameter(dc, "@AfficheCate", DbType.Int32, GetNewsCateByType(model.AfficheClass));         //咨询类别
            this._database.AddInParameter(dc, "@UpdateTime", DbType.DateTime, model.UpdateTime);        //置顶时间

            IList<EyouSoft.Model.NewsStructure.NewsSubItem> lst = new List<EyouSoft.Model.NewsStructure.NewsSubItem>();
            if (model.NewsKeyWordItem != null)
            {
                foreach (EyouSoft.Model.NewsStructure.NewsSubItem cls in model.NewsKeyWordItem)
                {
                    lst.Add(cls);
                }
            }
            if (model.NewsTagItem != null)
            {
                foreach (EyouSoft.Model.NewsStructure.NewsSubItem cls in model.NewsTagItem)
                {
                    lst.Add(cls);
                }
            }
            //不存在的关键字、标签
            xml = new System.Xml.XmlDocument();
            sbXml = new StringBuilder();
            if (lst != null)
            {
                sbXml.Append("<item>");
                foreach (EyouSoft.Model.NewsStructure.NewsSubItem item in lst)
                {
                    if (!TagKeyExists(item.ItemName, item.ItemType))
                    {
                        sbXml.Append("<value id=\"" + (int)item.ItemType + "\" val=\"" + Common.Utility.ReplaceXmlSpecialCharacter(item.ItemName) + "\">" + (item.ItemUrl == null ? string.Empty : item.ItemUrl) + "</value>");
                    }
                }
                sbXml.Append("</item>");
            }
            xml.LoadXml(sbXml.ToString());
            this._database.AddInParameter(dc, "@KeyTag", DbType.Xml, xml.InnerXml);

            //所有的关键字标签
            xml = new System.Xml.XmlDocument();
            sbXml = new StringBuilder();
            if (lst != null)
            {
                sbXml.Append("<item>");
                foreach (EyouSoft.Model.NewsStructure.NewsSubItem item in lst)
                {
                    //if (!TagKeyExists(item.ItemName, EyouSoft.Model.NewsStructure.ItemCategory.Tag))
                    //{
                    sbXml.Append("<value id=\"" + (int)item.ItemType + "\" val=\"" + Common.Utility.ReplaceXmlSpecialCharacter(item.ItemName) + "\">" + (item.ItemUrl == null ? string.Empty : item.ItemUrl) + "</value>");
                    //}
                }
                sbXml.Append("</item>");
            }
            xml.LoadXml(sbXml.ToString());
            this._database.AddInParameter(dc, "@KeyTag1", DbType.Xml, xml.InnerXml);

            //内容
            xml = new System.Xml.XmlDocument();
            sbXml = new StringBuilder();
            if (model.NewsContent != null)
            {
                sbXml.Append("<news>");
                foreach (EyouSoft.Model.NewsStructure.NewsContent item in model.NewsContent)
                {
                    sbXml.Append("<cont pageindex=\"" + item.PageIndex.ToString() + "\">" + Common.Utility.ReplaceXmlSpecialCharacter(item.Content) + "</cont>");
                }
                sbXml.Append("</news>");
            }
            xml.LoadXml(sbXml.ToString());
            this._database.AddInParameter(dc, "@xmlAfficheinfo", DbType.Xml, xml.InnerXml);

            try
            {
                return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
            }
            catch (Exception de)
            {
                throw de;
            }
        }
        #endregion
    }
}
