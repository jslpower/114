using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Xml;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 模块名称：消息提醒
    /// 功能说明：首页登录下面的提醒信息
    /// </summary>
    /// 华磊  时间：2011-5-9
    public class Remind : DALBase, IDAL.CommunityStructure.IRemind
    {
        #region 构造函数
        /// <summary>
        /// 所在的数据库
        /// </summary>
        protected Database _database = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public Remind()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region SqlString
        /// <summary>
        /// 取出多消息来源数据，操作日志中每个表的前15条数据
        /// </summary>
        private const string Sql_GetRemindData = "select *,newid() as randid from view_Remind order by randid desc";
        //private const string Sql_GetRemindData = "select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogTour where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogTour " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogSystem where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogSystem " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogCompany where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogCompany " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogRoute where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogRoute " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogService where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogServer " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogOrder where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogOrder " +
        //    " union all select * from (select top 15 OperatorName,EventTitle,EventTime from tbl_LogUserLogin where EventTime>dateadd(day,-7,getdate()) order by EventTime desc) as LogUserLogion";
        /// <summary>
        /// 将数据添加到消息提醒表中去
        /// </summary>
        private const string Sql_InsertRemindData = "insert into [tbl_Remind]([Operator],[TilteType],[EventTime],[IsDisplay],[Sort]) values(@Operator,@TilteType,@EventTime,@IsDisplay,@Sort)";
        /// <summary>
        /// 将修改的数据更新到提醒表中去
        /// </summary>
        private const string Sql_UpdateRemindData = "update [tbl_Remind] set [Operator]=@Operator,[TilteType]=@TilteType,[EventTime]=@EventTime,[IsDisplay]=@IsDisplay,[Sort]=@Sort where [ID]=@ID";
        /// <summary>
        /// 删除选中的数据
        /// </summary>
        private const string Sql_DeleteRemindData = "delete from [tbl_Remind]";
        /// <summary>
        /// 获取表中指定条数的数据
        /// </summary>
        private const string Sql_GetrRemind = " SELECT {0} [ID],[Operator],[TilteType],[EventTime],[IsDisplay],[Sort] FROM [tbl_Remind] ";
        /// <summary>
        /// 获取实体
        /// </summary>
        private const string SQL_RemindGetModel = "Select [ID],[Operator],[TilteType],[EventTime],[IsDisplay],[Sort] from tbl_Remind where ID=@ID";
        /// <summary>
        /// 加MQ好友
        /// </summary>
        private const string SQL_IMFriendList = "select top 12 [uid],(select ContactName from tbl_CompanyUser where Id = (select bs_uid from im_member where im_uid = [uid])) as ContactName,friendid,(select ContactName from tbl_CompanyUser where Id = (select bs_uid from im_member where im_uid = friendid)) as FriendName,issuetime,case when 1=1 then '10' end as TypeTile from im_friendlist where  issuetime>dateadd(day,-3,getdate()) order by issuetime desc";
        //测试用
        //private const string SQL_IMFriendList = "select top 16 [uid],friendid,issuetime,case when 1=1 then '10' end as TypeTile from im_friendlist order by issuetime desc";

        #endregion

        #region ICommunityAdvisor成员


        /// <summary>
        /// 获取消息提醒的来源数据列表【最新的数据】
        /// </summary>
        /// <returns></returns>
        //public virtual IList<EyouSoft.Model.CommunityStructure.RemindSource> GetList()
        //{
        //    IList<EyouSoft.Model.CommunityStructure.RemindSource> list = new List<EyouSoft.Model.CommunityStructure.RemindSource>();
        //    //根据SQL语句获取最新的数据
        //    DbCommand dc = this._database.GetSqlStringCommand(Sql_GetRemindData);
        //    //操作数据集
        //    using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
        //    {
        //        EyouSoft.Model.CommunityStructure.RemindSource model = null;
        //        while (dr.Read())
        //        {
        //            model = new EyouSoft.Model.CommunityStructure.RemindSource();
        //            model.OperatorName = dr["OperatorName"].ToString();
        //            model.EventTitle = dr["EventTitle"].ToString();
        //            if (!dr.IsDBNull(dr.GetOrdinal("EventTime")))                    
        //                model.EventTime = DateTime.Parse(dr["EventTime"].ToString());
        //            list.Add(model);
        //            model = null;
        //        }
        //    }
        //    return list;
        //}
        /// <summary>
        /// 获取消息提醒的来源数据列表
        /// </summary>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.Remind> GetList()
        {
            IList<EyouSoft.Model.CommunityStructure.Remind> list = new List<EyouSoft.Model.CommunityStructure.Remind>();
            //根据SQL语句获取最新的数据
            DbCommand dc = this._database.GetSqlStringCommand(Sql_GetRemindData);
            //操作数据集
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                EyouSoft.Model.CommunityStructure.Remind model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.Remind();
                    model.Operator = dr["Operator"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TypeTile")))
                        model.Sort = int.Parse(dr["TypeTile"].ToString());
                    //订单ID
                    model.OrderID = dr["ID"].ToString();
                    list.Add(model);
                    model = null;
                }
            }

            //把加MQ为好友的数据添加到数据集里面去
            DbCommand dcIm = this._database.GetSqlStringCommand(SQL_IMFriendList);
            //操作数据集
            using (IDataReader drIm = DbHelper.ExecuteReader(dcIm, this._database))
            {
                EyouSoft.Model.CommunityStructure.Remind models = null;
                int sumCont = 0;
                while (drIm.Read())
                {
                    //循环加一
                    sumCont += 1;
                    models = new EyouSoft.Model.CommunityStructure.Remind();
                    //判断是否能被二整除
                    if (sumCont % 2 == 0)
                    {
                        models.Operator = drIm["ContactName"].ToString();
                        models.Sort = 11;
                    }
                    else
                    {
                        string strTmp = string.Empty;
                        if (!string.IsNullOrEmpty(drIm["ContactName"].ToString()))
                            strTmp = (drIm["ContactName"].ToString().Length > 3
                                         ? drIm["ContactName"].ToString().Substring(0, 3)
                                         : drIm["ContactName"]) + "加";
                        if (!string.IsNullOrEmpty(drIm["FriendName"].ToString()))
                            strTmp += (drIm["FriendName"].ToString().Length > 3
                                           ? drIm["FriendName"].ToString().Substring(0, 3)
                                           : drIm["FriendName"].ToString());

                        models.Operator = strTmp;
                        if (!drIm.IsDBNull(drIm.GetOrdinal("TypeTile")))
                        {
                            models.Sort = int.Parse(drIm["TypeTile"].ToString());
                        }
                    }
                    models.OrderID = drIm["uid"].ToString();
                    list.Add(models);
                    models = null;
                }
            }

            //取出随机数
            string[] strRandomSum = GetNewRandomSum(list.Count).Split(',');
            //遍历LIST添加时间
            IList<EyouSoft.Model.CommunityStructure.Remind> lists = new List<EyouSoft.Model.CommunityStructure.Remind>();
            int sumRand = 0;
            foreach (EyouSoft.Model.CommunityStructure.Remind items in list)
            {
                sumRand += 1;
                EyouSoft.Model.CommunityStructure.Remind model = new EyouSoft.Model.CommunityStructure.Remind();
                model.Operator = items.Operator;
                if (items.OrderID != null)
                {
                    model.TypeLink = GetLinklingLoader(items.Sort.ToString(), items.OrderID);
                }
                //数组是从0开始的
                model.EventTime = EyouSoft.Common.Utility.GetDateTime(strRandomSum[sumRand - 1].ToString(), DateTime.Now);
                lists.Add(model);
                model = null;
            }
            list = null;
            //返回值
            return lists;
        }

        #endregion

        #region 暂时没有用到的方法
        ///// <summary>
        ///// 添加提醒数据
        ///// </summary>
        ///// <param name="model">model</param>
        ///// <returns>false:失败 true:成功</returns>
        //public virtual bool AddRemind(EyouSoft.Model.CommunityStructure.Remind model)
        //{
        //    DbCommand dc = this._database.GetSqlStringCommand(Sql_InsertRemindData);
        //    this._database.AddInParameter(dc, "Operator", DbType.String, model.Operator);
        //    this._database.AddInParameter(dc, "TilteType", DbType.Byte, (int)model.TilteType);
        //    this._database.AddInParameter(dc, "EventTime", DbType.DateTime, model.EventTime);
        //    this._database.AddInParameter(dc, "IsDisplay", DbType.AnsiStringFixedLength, model.IsDisplay ? "1" : "0");
        //    this._database.AddInParameter(dc, "Sort", DbType.Int32, model.Sort);
        //    return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        //}


        /// <summary>
        /// 批量添加提醒数据
        /// </summary>
        /// <param name="models">提醒实体</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool AddRemind(IList<EyouSoft.Model.CommunityStructure.Remind> models)
        {
            if (models != null && models.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.Remind model in models)
                {
                    if (model != null)
                    {
                        DbCommand dc = this._database.GetSqlStringCommand(Sql_InsertRemindData);
                        this._database.AddInParameter(dc, "Operator", DbType.String, model.Operator);
                        this._database.AddInParameter(dc, "TilteType", DbType.Byte, (int)model.TilteType);
                        this._database.AddInParameter(dc, "EventTime", DbType.DateTime, model.EventTime);
                        this._database.AddInParameter(dc, "IsDisplay", DbType.AnsiStringFixedLength, model.IsDisplay ? "1" : "0");
                        this._database.AddInParameter(dc, "Sort", DbType.Int32, model.Sort);

                        if (DbHelper.ExecuteSqlTrans(dc, this._database) <= 0)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新提醒数据
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool UpdateRemind(EyouSoft.Model.CommunityStructure.Remind model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(Sql_UpdateRemindData);
            this._database.AddInParameter(dc, "ID", DbType.Int32, model.ID);
            this._database.AddInParameter(dc, "Operator", DbType.String, model.Operator);
            this._database.AddInParameter(dc, "TilteType", DbType.Byte, (int)model.TilteType);
            this._database.AddInParameter(dc, "EventTime", DbType.DateTime, model.EventTime);
            this._database.AddInParameter(dc, "IsDisplay", DbType.AnsiStringFixedLength, model.IsDisplay ? "1" : "0");
            this._database.AddInParameter(dc, "Sort", DbType.Int32, model.Sort);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 批量删除提醒数据
        /// </summary>
        /// <param name="CommentIds">ID</param>
        /// <returns>false:失败 true:成功</returns>
        public virtual bool DeleteRemind(params int[] CommentIds)
        {
            StringBuilder strSql = new StringBuilder(Sql_DeleteRemindData + " where ID in(");
            for (int i = 0; i < CommentIds.Length; i++)
            {
                strSql.AppendFormat("{0}{1}{2}", i == 0 ? "" : ",", "@PARM", i);
            }
            strSql.Append(") ");
            DbCommand dc = base.SystemStore.GetSqlStringCommand(strSql.ToString());
            for (int i = 0; i < CommentIds.Length; i++)
            {
                base.SystemStore.AddInParameter(dc, "PARM" + i, DbType.Int32, CommentIds[i]);
            }
            return DbHelper.ExecuteSql(dc, base.SystemStore) > 0 ? true : false;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="ID">编号</param>
        /// <returns>返回结果集</returns>
        public virtual EyouSoft.Model.CommunityStructure.Remind GetModel(int ID)
        {
            EyouSoft.Model.CommunityStructure.Remind model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_RemindGetModel);
            this._database.AddInParameter(dc, "ID", DbType.Int32, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.Remind();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                        model.ID = int.Parse(dr["ID"].ToString());
                    model.Operator = dr["Operator"].ToString();
                    if (!string.IsNullOrEmpty(dr["TilteType"].ToString()))
                        model.TilteType = (EyouSoft.Model.CommunityStructure.TitleTypes)int.Parse(dr["TilteType"].ToString());
                    if (!string.IsNullOrEmpty(dr["EventTime"].ToString()))
                        model.EventTime = DateTime.Parse(dr["EventTime"].ToString());
                    if (!string.IsNullOrEmpty(dr["IsDisplay"].ToString()) && dr["IsDisplay"].ToString().Equals("1"))
                        model.IsDisplay = true;
                    if (!string.IsNullOrEmpty(dr["Sort"].ToString()))
                        model.Sort = int.Parse(dr["Sort"].ToString());
                }
            }
            return model;
        }
        /// <summary>
        /// 分页获取提醒信息列表
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="IsDisplay">是否显示 =null返回全部</param>
        /// <returns>返回顾问团队实体集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int PageSize, int PageIndex, ref int RecordCount, bool? IsDisplay)
        {
            IList<EyouSoft.Model.CommunityStructure.Remind> List = new List<EyouSoft.Model.CommunityStructure.Remind>();
            //构造SQL语句
            string strFiles = " [ID],[Operator],[TilteType],[EventTime],[IsDisplay],[Sort] ";
            string strOrder = " Sort desc ";
            string strWhere = string.Empty;
            //是否显示
            if (IsDisplay.HasValue)
            {
                strWhere = string.Format(" IsDisplay='{0}' ", IsDisplay.Value ? "1" : "0");
            }
            using (IDataReader dr = DbHelper.ExecuteReader(base.SystemStore, PageSize, PageIndex, ref RecordCount, "tbl_Remind", "[ID]", strFiles, strWhere, strOrder))
            {
                EyouSoft.Model.CommunityStructure.Remind model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.Remind();
                    if (!string.IsNullOrEmpty(dr["ID"].ToString()))
                    {
                        model.ID = int.Parse(dr["ID"].ToString());
                    }
                    model.Operator = dr["Operator"].ToString();

                    if (!dr.IsDBNull(dr.GetOrdinal("TilteType")))
                    {
                        model.TilteType = (EyouSoft.Model.CommunityStructure.TitleTypes)int.Parse(dr["TilteType"].ToString());
                    }
                    //生成链接跳转
                    //if (!dr.IsDBNull(dr.GetOrdinal("TilteType")))
                    //{
                    //    model.TypeLink = GetLinklingLoader(dr["TilteType"].ToString());
                    //}

                    if (!dr.IsDBNull(dr.GetOrdinal("EventTime")))
                    {
                        model.EventTime = DateTime.Parse(dr["EventTime"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dr["IsDisplay"].ToString()) && dr["IsDisplay"].ToString().Equals("1"))
                    {
                        model.IsDisplay = true;
                    }
                    if (!string.IsNullOrEmpty(dr["Sort"].ToString()))
                    {
                        model.Sort = int.Parse(dr["Sort"].ToString());
                    }
                    List.Add(model);
                    model = null;
                }
            }
            return List;
        }

        /// <summary>
        /// 获取提醒信息的条数
        /// </summary>
        /// <param name="TopNum">信息条数</param>
        /// <param name="IsDisplay">是否显示</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.Remind> GetRemindLists(int TopNum, bool? IsDisplay)
        {
            IList<EyouSoft.Model.CommunityStructure.Remind> List = new List<EyouSoft.Model.CommunityStructure.Remind>();
            //构造SQL语句
            string StrSql = string.Format(Sql_GetrRemind, TopNum > 0 ? string.Format(" top {0}", TopNum) : string.Empty);
            if (IsDisplay.HasValue)
            {
                StrSql += string.Format(" where IsDisplay='{0}' ", IsDisplay.Value ? "1" : "0");
            }
            StrSql += " order by Sort desc";
            DbCommand dc = base.SystemStore.GetSqlStringCommand(StrSql);
            //执行查询并对实体进行赋值
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                EyouSoft.Model.CommunityStructure.Remind model = null;
                while (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.Remind();
                    model.Operator = dr["Operator"].ToString();
                    //if (!dr.IsDBNull(dr.GetOrdinal("TilteType")))
                    //{
                    //    model.TilteType = (EyouSoft.Model.CommunityStructure.TitleTypes)int.Parse(dr["TilteType"].ToString());
                    //}
                    if (!dr.IsDBNull(dr.GetOrdinal("TilteType")))
                    {
                        model.TypeLink = GetTitleLink(dr["TilteType"].ToString());
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("EventTime")))
                    {
                        model.EventTime = DateTime.Parse(dr["EventTime"].ToString());
                    }
                    List.Add(model);
                    model = null;
                }
            }
            return List;
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 根据MQ的ID获取MQ的用户名
        /// </summary>
        /// <param name="Imid"></param>
        /// <returns></returns>
        private string GetImUserName(string Imid)
        {
            string strUserName = string.Empty;
            if (Imid != null)
            {
                EyouSoft.DAL.MQStructure.IMMember Immember = new EyouSoft.DAL.MQStructure.IMMember();
                strUserName = Immember.GetModel(int.Parse(Imid)).im_username;
                if (strUserName != null && strUserName.Length > 3)
                {
                    strUserName = strUserName.Substring(0, 3);
                }
            }
            return strUserName;
        }

        /// <summary>
        /// 根据类型拼接字符串
        /// </summary>
        /// <param name="TitleType"></param>
        /// <returns></returns>
        private string GetTitleLink(string TitleType)
        {
            string strTtitleLink = string.Empty;
            //strTtitleLink = "<a href='" + GetLinklingLoader(TitleType) + "' target=\"_blank\">" + Convert.ToString((EyouSoft.Model.CommunityStructure.TitleTypes)int.Parse(TitleType)) + "</a>";
            return strTtitleLink;
        }

        /// <summary>
        /// 根据提醒类型查询跳转路径
        /// </summary>
        /// <param name="TitleType">提醒类型</param>
        /// <param name="strOrderID">订单ID</param>
        /// <returns></returns>
        private string GetLinklingLoader(string TitleType, string strOrderID)
        {
            string Address = string.Empty;
            string Upvalue = string.Empty;
            string Dowmvalue = string.Empty;
            string strUrl = string.Empty;
            string TypeValue = string.Empty;
            //获取XML路径
            string XMLFilePath = EyouSoft.Common.Utility.GetMapPath("Remind.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLFilePath);
            XmlNode rootNode = xmlDoc.SelectSingleNode("Remind");
            //取出该节点下的所有子节点
            XmlNodeList LevelNodeList = rootNode.ChildNodes;
            //遍历节点
            foreach (XmlNode xmlNo in LevelNodeList)
            {
                XmlElement RemindTitle = (XmlElement)xmlNo;

                //判断类型等于3的时候,即是购买的类型
                if (TitleType == "3")
                {
                    if (RemindTitle.GetAttribute("id") == TitleType)
                    {
                        Upvalue = RemindTitle.GetAttribute("UpValue").ToString();
                        Dowmvalue = RemindTitle.GetAttribute("DownValue").ToString();
                        //加链接
                        StringBuilder strSql = new StringBuilder(RemindTitle.GetAttribute("titleLink").ToString());
                        strSql.AppendFormat("{0}", strOrderID);

                        strUrl = strSql.ToString();

                        TypeValue = RemindTitle.GetAttribute("category").ToString();
                        break;
                    }
                }
                else if (TitleType == "2")
                {//发布线路购买
                    if (RemindTitle.GetAttribute("id") == TitleType)
                    {
                        Upvalue = RemindTitle.GetAttribute("UpValue").ToString();
                        Dowmvalue = RemindTitle.GetAttribute("DownValue").ToString();
                        strUrl = RemindTitle.GetAttribute("titleLink").ToString();
                        TypeValue = RemindTitle.GetAttribute("category").ToString();
                    }
                    //取出购买的路径
                    if (RemindTitle.GetAttribute("id") == "3")
                    {
                        StringBuilder strSql = new StringBuilder(RemindTitle.GetAttribute("titleLink").ToString());
                        strSql.AppendFormat("{0}", strOrderID);
                        string strListBuy = RemindTitle.GetAttribute("category").ToString();
                        Dowmvalue += "&nbsp;" + "<a href='" + strSql.ToString() + "' target=\"_blank\">" + strListBuy + "</a>";
                        break;
                    }
                }
                else
                {
                    if (RemindTitle.GetAttribute("id") == TitleType)
                    {
                        Upvalue = RemindTitle.GetAttribute("UpValue").ToString();
                        Dowmvalue = RemindTitle.GetAttribute("DownValue").ToString();
                        strUrl = RemindTitle.GetAttribute("titleLink").ToString();
                        TypeValue = RemindTitle.GetAttribute("category").ToString();
                        break;
                    }
                }
            }
            //拼接链接
            Address = Upvalue + "<a href='" + strUrl + "' target=\"_blank\">" + TypeValue + "</a>" + Dowmvalue;
            return Address;
        }

        /// <summary>
        /// 根据时间类型获取小时数
        /// </summary>
        /// <param name="hourType">小时类型</param>
        /// <returns>返回的小时</returns>
        private int Getsumhour(string hourType)
        {
            //默认值
            int sumhour = 4;
            //获取XML路径
            string XMLFilePath = EyouSoft.Common.Utility.GetMapPath("Remind.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XMLFilePath);
            XmlNode rootNode = xmlDoc.SelectSingleNode("Remind");
            //取出该节点下的所有子节点
            XmlNodeList LevelNodeList = rootNode.ChildNodes;
            //遍历节点
            foreach (XmlNode xmlNo in LevelNodeList)
            {
                XmlElement RemindTitle = (XmlElement)xmlNo;
                if (RemindTitle.GetAttribute("hourtype") == hourType)
                {
                    sumhour = int.Parse(RemindTitle.GetAttribute("sumhour").ToString());
                    break;
                }
            }
            return sumhour;
        }
        /// <summary>
        /// 根据当前时间判断，抽取最近的数据条数
        /// </summary>
        /// <param name="RandomSum">数据条数</param>
        /// <returns></returns> 
        private string GetNewRandomSum(int RandomSum)
        {
            string strNewRandomSum = string.Empty;

            DateTime EightShree = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(8).AddMinutes(30);
            DateTime TeenShree = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(22).AddMinutes(30);

            //随机抽出昨天的随机时间数
            string[] strmmmm = GetOldRadnom(RandomSum).Split(',');
            string[] abc = (string[])strmmmm.ToArray();
            var q = (from c in abc
                     orderby Guid.NewGuid()
                     select c).Take(RandomSum);

            //随机抽出今天的时间数
            string[] strnnnn = GetTodayRadnom(RandomSum).Split(',');
            string[] mn = (string[])strnnnn.ToArray();
            var n = (from m in mn
                     orderby Guid.NewGuid()
                     select m).Take(RandomSum);

            //判断当前时间
            if (DateTime.Now < EightShree)
            {//取昨天的数据
                foreach (string item in q)
                {
                    if (strNewRandomSum.Length > 0)
                    {
                        strNewRandomSum += "," + item.ToString();
                    }
                    else
                    {
                        strNewRandomSum = item.ToString();
                    }
                }
            }
            else if (EightShree <= DateTime.Now && DateTime.Now <= TeenShree)
            {
                //取今天的数据
                foreach (string nnnn in n)
                {
                    if (strNewRandomSum.Length > 0)
                    {
                        strNewRandomSum += "," + nnnn.ToString();
                    }
                    else
                    {
                        strNewRandomSum = nnnn.ToString();
                    }
                }
            }
            else if (DateTime.Now > TeenShree)
            {
                //取今天的数据
                foreach (string nnnn in n)
                {
                    if (strNewRandomSum.Length > 0)
                    {
                        strNewRandomSum += "," + nnnn.ToString();
                    }
                    else
                    {
                        strNewRandomSum = nnnn.ToString();
                    }
                }
            }
            else
            {
                //取昨天数据
                foreach (string item in q)
                {
                    if (strNewRandomSum.Length > 0)
                    {
                        strNewRandomSum += "," + item.ToString();
                    }
                    else
                    {
                        strNewRandomSum = item.ToString();
                    }
                }
            }
            return strNewRandomSum;
        }
        /// <summary>
        /// 生成今天的随机库
        /// </summary>
        /// <param name="sumDataCont">数据条数,如果大于50的话,配置小时为最大</param>
        /// <returns>返回库</returns>
        private string GetTodayRadnom(int sumDataCont)
        {
            string strN = string.Empty;
            //根据分钟数生成
            int[] arrCH = GetRandomArray(58, 0, 59);

            for (int i = 0; i < arrCH.Length; i++)
            {
                int hour = 0;
                //标示
                int sumhour = 0;
                if (DateTime.Now > DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(22).AddMinutes(30))
                {
                    hour = 22;
                    sumhour = 22;
                }
                else
                {
                    hour = System.DateTime.Now.Hour;
                }
                //获取配置小时数
                int newhour = Getsumhour("2");
                for (int m = 0; m < newhour; m++)
                {
                    //小时数
                    hour = System.DateTime.Now.Hour - m;
                    //过滤时间
                    DateTime today = DateTime.Now;
                    today = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString()));
                    //判断标示；即是取今天22:30之前的数小时数据
                    if (sumhour == 22)
                    {
                        if (today < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(22).AddMinutes(30))
                        {
                            if (strN != string.Empty)
                            {
                                strN += "," + DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                            }
                            else
                            {
                                strN += DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                            }
                        }
                    }
                    else
                    {//根据现在的时间取出时间库
                        if (today < DateTime.Now)
                        {
                            if (strN != string.Empty)
                            {
                                strN += "," + DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                            }
                            else
                            {
                                strN += DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                            }
                        }
                    }
                }

            }
            return strN;
        }

        /// <summary>
        /// 生成随机数库[昨天]
        /// </summary>
        /// <param name="sumDataCont">数据条数,如果大于50的话,配置小时为最大</param>
        /// <returns>返回库</returns>
        private string GetOldRadnom(int sumDataCont)
        {
            string strN = string.Empty;
            //根据分钟数生成
            int[] arrCH = GetRandomArray(58, 0, 59);
            for (int i = 0; i < arrCH.Length; i++)
            {
                int hour = 22;
                int sumhour = Getsumhour("1");
                for (int m = 0; m < sumhour; m++)
                {
                    hour = 22 - m;
                    //拼出日期看看是否过期
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    //如果小于22:30
                    if (yesterday.AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())) < yesterday.AddHours(22).AddMinutes(30))
                    {
                        if (strN != string.Empty)
                        {
                            strN += "," + DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                        }
                        else
                        {
                            strN += DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")).AddHours(hour).AddMinutes(int.Parse(arrCH[i].ToString())).ToString();
                        }
                    }
                }
            }
            return strN;
        }

        /// <summary>
        /// 生成分钟随机数
        /// </summary>
        /// <param name="Number">获取随机数的数量</param>
        /// <param name="minNum">开始值</param>
        /// <param name="maxNum">最大值</param>
        /// <returns></returns>
        private int[] GetRandomArray(int Number, int minNum, int maxNum)
        {
            int j;
            int[] b = new int[Number];
            Random r = new Random();
            for (j = 0; j < Number; j++)
            {
                int i = r.Next(minNum, maxNum + 1);
                int num = 0;
                for (int k = 0; k < j; k++)
                {
                    if (b[k] == i)
                    {
                        num = num + 1;
                    }
                }
                if (num == 0)
                {
                    b[j] = i;
                }
                else
                {
                    j = j - 1;
                }
            }
            return b;
        }

        #endregion
    }
}
