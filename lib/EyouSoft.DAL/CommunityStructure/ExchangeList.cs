using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.IDAL.CommunityStructure;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-07-16
    /// 描述：供求信息数据层
    /// </summary>
    /// 修改人:zhengfj 2011-5-18
    /// 修改内容 SQL_ExchangeList_GetTopNumList 读取 Category
    ///          GetModel(string ID) 读取 Category
    public class ExchangeList : DALBase, IDAL.CommunityStructure.IExchangeList
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExchangeList()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region 供求信息SQL定义
        private const string SQL_ExchangeList_ADD = "INSERT INTO tbl_ExchangeList(ID,TopicClassID,ExchangeTagId,ExchangeTitle,ExchangeText,CompanyId,CompanyName,OperatorId,OperatorName,OperatorMQ,ProvinceId,CityId,ContactName,ContactTel,IssueTime,AttatchPath,IsCheck)  VALUES(@ID,@TopicClassID,@ExchangeTagId,@ExchangeTitle,@ExchangeText,@CompanyId,@CompanyName,@OperatorId,@OperatorName,@OperatorMQ,@ProvinceId,@CityId,@ContactName,@ContactTel,@IssueTime,@AttatchPath,@IsCheck);";
        private const string SQL_ExchangeList_UPDATE = "UPDATE tbl_ExchangeList SET TopicClassID = @TopicClassID,ExchangeTagId = @ExchangeTagId,ExchangeTitle = @ExchangeTitle,ExchangeText = @ExchangeText,CompanyId = @CompanyId,CompanyName = @CompanyName,OperatorId = @OperatorId,OperatorName = @OperatorName,OperatorMQ = @OperatorMQ,ProvinceId = @ProvinceId,CityId = @CityId,ContactName = @ContactName,ContactTel = @ContactTel,IsTop = @IsTop,TopTime = (case when @IsTop='1' then getdate() else IssueTime end),AttatchPath = @AttatchPath WHERE ID=@ExchangeId;";
        private const string SQL_ExchangeList_GETMODEl = "SELECT [ID],[TopicClassID],[ExchangeTagId],[ExchangeTitle],[ExchangeText],[WriteBackCount],[ViewCount],[CompanyId],[CompanyName],[OperatorId],[OperatorName],[OperatorMQ],[ProvinceId],[CityId],[ContactName],[ContactTel],[IssueTime],[IsTop],[TopTime],[AttatchPath],[IsCheck],Category FROM [tbl_ExchangeList] where ID=@ID ";
        private const string SQL_ExchangeList_DELETE = "DELETE FROM [tbl_ExchangeList]  WHERE ID=@ID; DELETE tbl_ExchangeComment WHERE TopicId=@ID and TopicClassId=@TopicClassId;";
        private const string SQL_ExchangeList_SETTOP = "Update tbl_ExchangeList set IsCheck='1',IsTop=@IsTop,TopTime=(case when @IsTop='1' then getdate() else IssueTime end) where ID=@ID";
        private const string SQL_ExchangeList_SetReadCount = "Update tbl_ExchangeList set ViewCount=ViewCount+1 where ID=@ID";
        private const string SQL_ExchangeList_SetWriteBackCount = "Update tbl_ExchangeList set WriteBackCount=WriteBackCount+1 where ID=@ID";
        private const string SQL_ExchangeList_GetTopNumList = "select {0} ID,TopicClassID,ExchangeTagId,ExchangeTitle,ExchangeText,WriteBackCount,ViewCount,CompanyId,CompanyName,ProvinceId,CityId,ContactName,OperatorMQ,IssueTime,IsTop,TopTime,AttatchPath,Category from tbl_ExchangeList {1} order by IsTop desc,TopTime desc,IssueTime desc";
        private const string SQL_ExchangeList_SetVisited = "if not exists(select 1 from tbl_ExchangeVisited where ExchangeId=@ExchangeId and CompanyId=@CompanyId and OperatorId=@OperatorId) begin	insert into tbl_ExchangeVisited(ExchangeId,CompanyId,OperatorId,IssueTime) values(@ExchangeId,@CompanyId,@OperatorId,@IssueTime) end";
        private const string SQL_ExchangeList_SetCheck = "Update tbl_ExchangeList set IsCheck=@IsCheck where ID=@ID";
        private const string SQL_ExchangeList_BatchCheck = "update [tbl_ExchangeList] set Ischeck=(case when IsCheck='0' then '1' else '1' end ) ";

        #region zhengfj
        private const string SQL_ExchangeList_ADDNew = "INSERT INTO tbl_ExchangeList(ID,TopicClassID,ExchangeTagId,Category,ExchangeTitle,ExchangeText,CompanyId,CompanyName,OperatorId,OperatorName,OperatorMQ,ProvinceId,CityId,ContactName,ContactTel,IssueTime,AttatchPath,IsCheck)  VALUES(@ID,@TopicClassID,@ExchangeTagId,@Category,@ExchangeTitle,@ExchangeText,@CompanyId,@CompanyName,@OperatorId,@OperatorName,@OperatorMQ,@ProvinceId,@CityId,@ContactName,@ContactTel,@IssueTime,@AttatchPath,@IsCheck);";
        private const string SQL_ExchangeList_UPDATENew = "UPDATE tbl_ExchangeList SET TopicClassID = @TopicClassID,ExchangeTagId = @ExchangeTagId,ExchangeTitle = @ExchangeTitle,ExchangeText = @ExchangeText,CompanyId = @CompanyId,CompanyName = @CompanyName,OperatorId = @OperatorId,OperatorName = @OperatorName,OperatorMQ = @OperatorMQ,ProvinceId = @ProvinceId,CityId = @CityId,ContactName = @ContactName,ContactTel = @ContactTel,IsTop = @IsTop,TopTime = (case when @IsTop='1' then getdate() else IssueTime end),AttatchPath = @AttatchPath,Category=@Category WHERE ID=@ExchangeId;";
        private const string SQL_ExchangeList_GetTopList = "SELECT TOP(@topNum) ID,TopicClassID,ExchangeTagId,Category,ExchangeTitle,ExchangeText,OperatorMQ,ProvinceId,IssueTime FROM tbl_ExchangeList WHERE TopicClassID = @topicClassID AND ExchangeTagId = @exchangeTagId AND Category IN(1,2) Order By IssueTime DESC";
        private const string SQL_ExchangeList_GetTopList2 = "SELECT TOP(@topNum) ID,TopicClassID,ExchangeTagId,Category,ExchangeTitle,ExchangeText,OperatorMQ,ProvinceId,IssueTime FROM tbl_ExchangeList WHERE TopicClassID = @topicClassID AND ExchangeTagId <> @exchangeTagId AND Category IN(1,2) Order By IssueTime DESC";
        private const string SQL_ExchangeList_GetExchangeListCount = "SELECT COUNT(ID) FROM tbl_ExchangeList ";
        #endregion

        const string SQL_INSERT_QG_InsertQQGroupMessage = "INSERT INTO [tbl_qqgroupmessage]([MId],[Title],[Content],[QGID],[QUID],[QMTime],[IssueTime],[Status],[FUID]) VALUES (@MId,@Title,@Content,@QGID,@QUID,@QMTime,@IssueTime,@Status,@FUID)";
        const string SQL_SELECT_QG_GetQQGroupMessageInfo = "SELECT * FROM [tbl_qqgroupmessage] WHERE [MId]=@MId";
        const string SQL_UPDATE_QG_SetQQGroupMessageStatus = "UPDATE [tbl_qqgroupmessage] SET [Status]=@Status WHERE [MId]=@MId";
        const string SQL_UPDATE_QG_SetQQGroupOfferTitle = "UPDATE [tbl_ExchangeList] SET [ExchangeTitle]=@Title WHERE [Id]=@OfferId";
        #endregion

        #region 供求信息城市关系，相关图片SQL定义
        private const string SQL_ExchangeCityContact_ADD = "INSERT INTO tbl_ExchangeCityContact(ExchangeId,ProvinceId,CityId) values('{0}',{1},{2});";
        private const string SQL_ExchangePhoto_ADD = "INSERT INTO tbl_ExchangePhoto(ImgId,ExchangeId,ImgPath,IssueTime) values('{0}','{1}','{2}','{3}');";
        private const string SQL_ExchangeCityContact_DELETE = "DELETE tbl_ExchangeCityContact where ExchangeId=@ExchangeId;";
        private const string SQL_ExchangePhoto_DELETE = "DELETE tbl_ExchangePhoto where ExchangeId=@ExchangeId AND ImgId='{0}';";
        private const string SQL_ExchangePhoto_SELECT = "SELECT [ImgId],[ExchangeId],[ImgPath],[IssueTime] FROM [tbl_ExchangePhoto] where ExchangeId=@ExchangeId ";
        private const string SQL_ExchangeCityContact_SELECT = "SELECT [ExchangeId],[CityId],[ProvinceId] FROM [tbl_ExchangeCityContact] where ExchangeId=@ExchangeId ";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_UPDATEMOVE = "IF (SELECT COUNT(*) FROM [tbl_ExchangePhoto] WHERE ExchangeId=@ExchangeId AND [ImgPath]='{1}' AND ImgId='{0}')=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_ExchangePhoto] WHERE ExchangeId=@ExchangeId AND ImgId='{0}' AND ImgPath<>'';";
        private const string SQL_DELETEDFILE_DELETEMOVE = "INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [ImgPath] FROM [tbl_ExchangePhoto] WHERE ExchangeId=@ID AND ImgPath<>'';";
        #endregion

        #region IExchangeList成员
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Add(EyouSoft.Model.CommunityStructure.ExchangeList model)
        {
            string ExchangeId = Guid.NewGuid().ToString();
            model.ID = ExchangeId;
            #region 生成添加SQL语句
            StringBuilder strSql = new StringBuilder(SQL_ExchangeList_ADD);
            if (model.CityContactList != null && model.CityContactList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeCityContact CityModel in model.CityContactList)
                {
                    strSql.AppendFormat(SQL_ExchangeCityContact_ADD, ExchangeId, CityModel.ProvinceId, CityModel.CityId);
                }
            }
            if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangePhoto PhotoModel in model.ExchangePhotoList)
                {
                    strSql.AppendFormat(SQL_ExchangePhoto_ADD, Guid.NewGuid().ToString(), ExchangeId, PhotoModel.ImgPath, DateTime.Now.ToString());
                }
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ExchangeId);
            this._database.AddInParameter(dc, "TopicClassID", DbType.Byte, (int)model.TopicClassID);
            this._database.AddInParameter(dc, "ExchangeTagId", DbType.Byte, (int)model.ExchangeTag);
            this._database.AddInParameter(dc, "ExchangeTitle", DbType.String, model.ExchangeTitle);
            this._database.AddInParameter(dc, "ExchangeText", DbType.String, model.ExchangeText);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "AttatchPath", DbType.String, model.AttatchPath);
            this._database.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, model.IsCheck ? "1" : "0");
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求信息实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Update(EyouSoft.Model.CommunityStructure.ExchangeList model)
        {
            #region 生成SQL语句
            StringBuilder strSql = new StringBuilder(SQL_ExchangeList_UPDATE);
            strSql.Append(SQL_ExchangeCityContact_DELETE); //删除原有的城市关系
            //strSql.Append(SQL_ExchangePhoto_DELETE);//删除原有的图片
            if (model.CityContactList != null && model.CityContactList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeCityContact CityModel in model.CityContactList)
                {
                    strSql.AppendFormat(SQL_ExchangeCityContact_ADD, model.ID, CityModel.ProvinceId, CityModel.CityId);
                }
            }
            if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangePhoto PhotoModel in model.ExchangePhotoList)
                {
                    strSql.AppendFormat(SQL_DELETEDFILE_UPDATEMOVE, PhotoModel.ImgId, PhotoModel.ImgPath);
                    strSql.AppendFormat(SQL_ExchangePhoto_DELETE, PhotoModel.ImgId);
                    strSql.AppendFormat(SQL_ExchangePhoto_ADD, Guid.NewGuid().ToString(), model.ID, PhotoModel.ImgPath, DateTime.Now.ToString());
                }
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "TopicClassID", DbType.Byte, (int)model.TopicClassID);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "ExchangeTagId", DbType.Byte, (int)model.ExchangeTag);
            this._database.AddInParameter(dc, "ExchangeTitle", DbType.String, model.ExchangeTitle);
            this._database.AddInParameter(dc, "ExchangeText", DbType.String, model.ExchangeText);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "AttatchPath", DbType.String, model.AttatchPath);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, model.IsTop ? "1" : "0");
            //this._database.AddInParameter(dc, "TopTime", DbType.DateTime, model.IsTop?DateTime.Now:DateTime.MinValue);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 获取供求信息实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>供求信息实体</returns>
        public virtual EyouSoft.Model.CommunityStructure.ExchangeList GetModel(string ID)
        {
            EyouSoft.Model.CommunityStructure.ExchangeList model = null;
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_GETMODEl);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                if (dr.Read())
                {
                    model = new EyouSoft.Model.CommunityStructure.ExchangeList();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(1).ToString());
                    if (!dr.IsDBNull(2))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(2).ToString());
                    model.ExchangeTitle = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ExchangeText = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.WriteBackCount = dr.GetInt32(5);
                    model.ViewCount = dr.GetInt32(6);
                    model.CompanyId = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.CompanyName = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.OperatorId = dr.IsDBNull(9) ? string.Empty : dr.GetString(9);
                    model.OperatorName = dr.IsDBNull(10) ? string.Empty : dr.GetString(10);
                    model.OperatorMQ = dr.IsDBNull(11) ? string.Empty : dr.GetString(11);
                    model.ProvinceId = dr.GetInt32(12);
                    model.CityId = dr.GetInt32(13);
                    model.ContactName = dr.IsDBNull(14) ? string.Empty : dr.GetString(14);
                    model.ContactTel = dr.IsDBNull(15) ? string.Empty : dr.GetString(15);
                    model.IssueTime = dr.GetDateTime(16);
                    model.IsTop = dr.IsDBNull(17) ? false : (dr.GetString(17) == "1" ? true : false);
                    model.TopTime = dr.IsDBNull(18) ? DateTime.MinValue : dr.GetDateTime(18);
                    model.AttatchPath = dr.IsDBNull(19) ? string.Empty : dr.GetString(19);
                    model.CityContactList = GetCityContactList(model.ID);
                    model.ExchangePhotoList = GetExchangePhotos(model.ID);
                    model.IsCheck = dr.IsDBNull(20) ? false : (dr.GetString(20) == "1" ? true : false);
                    model.ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), dr.GetByte(21).ToString());
                }

            }
            return model;
        }
        /// <summary>
        /// 删除供求信息【同时删除浏览过或者收藏过该信息的记录】
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool Delete(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_DELETEDFILE_DELETEMOVE + SQL_ExchangeList_DELETE);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "TopicClassId", DbType.Byte, (int)EyouSoft.Model.CommunityStructure.TopicType.供求);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置置顶状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetTop(string ID, bool IsTop)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_SETTOP);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, IsTop ? "1" : "0");
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <param name="IsCheck">是否审核</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetCheck(string ID, bool IsCheck)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_SetCheck);
            this._database.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, IsCheck ? "1" : "0");
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IDs">主键编号集合</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool BatchCheck(string[] IDs)
        {
            StringBuilder strSql = new StringBuilder(SQL_ExchangeList_BatchCheck);
            strSql.Append(" where ID in(");
            for (int i = 0; i < IDs.Length; i++)
            {
                strSql.AppendFormat("{0}{1}{2}", i > 0 ? "," : "", "@Param", i);
            }
            strSql.Append(")");
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            for (int i = 0; i < IDs.Length; i++)
            {
                this._database.AddInParameter(dc, "@Param" + i.ToString(), DbType.AnsiStringFixedLength, IDs[i]);
            }
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 更新浏览次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetReadCount(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_SetReadCount);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 更新回复次数
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetWriteBackCount(string ID)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_SetWriteBackCount);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 分页获取供求信息
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <param name="ProvinceId">省份编号 =0返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IssueTime">添加时间 =null返回全部</param>
        /// <param name="OperatorId">用户编号 =""返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <param name="StartDate">开始日期</param>
        /// <param name="EndDate">结束日期</param>
        /// <param name="Days">最近天数 =null返回全部</param>
        /// <param name="IsCurrWeek">是否本周 =null返回全部</param>
        /// <param name="IsCurrMonth">是否本月 =null返回全部</param>
        /// <param name="IsCheck">是否已审核 =null返回全部</param>
        /// <returns>供求信息列表</returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, bool? IsTop, int ProvinceId, int CityId, DateTime? IssueTime, string OperatorId, string KeyWord, DateTime? StartDate, DateTime? EndDate,
            int? Days, bool? IsCurrWeek, bool? IsCurrMonth, bool? IsCheck)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();
            string tableName = "tbl_ExchangeList";
            string fields = "ID,TopicClassID,ExchangeTagId,ExchangeTitle,ExchangeText,WriteBackCount,ViewCount,CompanyId,CompanyName,ProvinceId,CityId,ContactName,OperatorMQ,IssueTime,IsTop,TopTime,AttatchPath,OperatorName,IsCheck";
            string primaryKey = "ID";
            string orderbyString = "IsTop desc,TopTime desc,IssueTime desc";
            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (ExchangeType.HasValue)
            {
                strWhere.AppendFormat(" and TopicClassID={0} ", (int)ExchangeType.Value);
            }
            if (Tag.HasValue)
            {
                strWhere.AppendFormat(" and ExchangeTagId={0} ", (int)Tag.Value);
            }
            if (ProvinceId > 0)
            {
                strWhere.AppendFormat(" and ID in(select ExchangeId from tbl_ExchangeCityContact where ProvinceId={0} )", ProvinceId);
            }
            if (CityId > 0)
            {
                strWhere.AppendFormat(" and ID in(select ExchangeId from tbl_ExchangeCityContact where cityId={0} )", CityId);
            }
            if (IssueTime.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',IssueTime)=0 ", IssueTime.Value.ToString());
            }
            if (!string.IsNullOrEmpty(OperatorId))
            {
                strWhere.AppendFormat(" and OperatorId='{0}' ", OperatorId);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere.AppendFormat(" and ExchangeTitle like '%{0}%'", KeyWord);
            }
            if (IsTop.HasValue)
            {
                strWhere.AppendFormat(" and IsTop='{0}' ", IsTop.Value ? "1" : "0");
            }
            if (StartDate.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',IssueTime)>-1", StartDate.Value.ToString());
            }
            if (EndDate.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,IssueTime,'{0}')>-1", EndDate.Value.ToString());
            }
            if (Days.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,IssueTime,getdate())<={0}", Days.Value);
            }
            if (IsCurrMonth.HasValue && IsCurrMonth.Value)
            {
                strWhere.Append(" and datediff(mm,IssueTime,getdate())=0");
            }
            if (IsCurrWeek.HasValue && IsCurrWeek.Value)
            {
                strWhere.Append(" and datediff(wk,IssueTime,getdate())=0");
            }
            if (IsCheck.HasValue)
            {
                strWhere.AppendFormat(" and IsCheck='{0}' ", IsCheck.Value ? "1" : "0");
            }
            strWhere.AppendFormat(" AND Category IN({0},{1}) ", (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.供, (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.求);
            #endregion
            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderbyString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(1).ToString());
                    if (!dr.IsDBNull(2))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(2).ToString());
                    model.ExchangeTitle = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ExchangeText = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.WriteBackCount = dr.GetInt32(5);
                    model.ViewCount = dr.GetInt32(6);
                    model.CompanyId = dr.GetString(7);
                    model.CompanyName = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.ProvinceId = dr.GetInt32(9);
                    model.CityId = dr.IsDBNull(10) ? 0 : dr.GetInt32(10);
                    model.ContactName = dr.IsDBNull(11) ? string.Empty : dr.GetString(11);
                    model.OperatorMQ = dr.IsDBNull(12) ? string.Empty : dr.GetString(12);
                    model.IssueTime = dr.GetDateTime(13);
                    model.IsTop = dr.IsDBNull(14) ? false : (dr.GetString(14) == "1" ? true : false);
                    model.TopTime = dr.IsDBNull(15) ? DateTime.MinValue : dr.GetDateTime(15);
                    model.AttatchPath = dr.IsDBNull(16) ? string.Empty : dr.GetString(16);
                    model.OperatorName = dr.IsDBNull(17) ? string.Empty : dr.GetString(17);
                    model.IsCheck = dr.IsDBNull(18) ? false : (dr.GetString(18) == "1" ? true : false);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 得到MQ内嵌页供求列表（共40条，前五条显示置顶的）
        /// </summary>
        /// <param name="ProvinceIds">某几个省份下的供求信息</param>
        /// <param name="Stime">时间（全部=0,今天=1,昨天=2,前天=3,更早=4</param>
        /// <param name="ExchangeTitle">要搜索的标题关键字</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(string ProvinceIds, int Stime, string ExchangeTitle)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();
            DbCommand dc = this._database.GetStoredProcCommand("proc_GetMQSupplierInfoList");
            if (!string.IsNullOrEmpty(ProvinceIds))
            {
                this._database.AddInParameter(dc, "ProvinceIds", DbType.String, ProvinceIds);
            }
            else
            {
                this._database.AddInParameter(dc, "ProvinceIds", DbType.String, "0");
            }
            this._database.AddInParameter(dc, "Stime", DbType.Int32, Stime);
            this._database.AddInParameter(dc, "ExchangeTitle", DbType.String, @ExchangeTitle);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
                    model.ID = dr["ID"].ToString();
                    if (!dr.IsDBNull(dr.GetOrdinal("TopicClassID")))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(dr.GetOrdinal("TopicClassID")).ToString());
                    if (!dr.IsDBNull(dr.GetOrdinal("ExchangeTagId")))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(dr.GetOrdinal("ExchangeTagId")).ToString());
                    model.ExchangeTitle = dr["ExchangeTitle"].ToString();
                    model.ExchangeText = dr["ExchangeText"].ToString();
                    model.CompanyId = dr["CompanyId"].ToString();
                    model.CompanyName = dr["CompanyName"].ToString();
                    model.ProvinceId = dr.IsDBNull(dr.GetOrdinal("ProvinceId")) ? 0 : dr.GetInt32(dr.GetOrdinal("ProvinceId"));
                    model.CityId = dr.IsDBNull(dr.GetOrdinal("CityId")) ? 0 : dr.GetInt32(dr.GetOrdinal("CityId"));
                    model.ContactName = dr["ContactName"].ToString();
                    model.OperatorMQ = dr["OperatorMQ"].ToString();
                    model.IssueTime = dr.GetDateTime(dr.GetOrdinal("IssueTime"));
                    model.IsTop = dr.IsDBNull(dr.GetOrdinal("IsTop")) ? false : (dr.GetString(dr.GetOrdinal("IsTop")) == "1" ? true : false);
                    model.TopTime = dr.IsDBNull(dr.GetOrdinal("TopTime")) ? DateTime.MinValue : dr.GetDateTime(dr.GetOrdinal("TopTime"));
                    model.AttatchPath = dr["AttatchPath"].ToString();
                    model.OperatorName = dr["OperatorName"].ToString();
                    model.IsCheck = dr.IsDBNull(dr.GetOrdinal("IsCheck")) ? false : (dr.GetString(dr.GetOrdinal("IsCheck")) == "1" ? true : false);
                    model.ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), dr.GetByte(dr.GetOrdinal("Category")).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }


        /// <summary>
        /// 返回指定条数的供求信息列表
        /// </summary>
        /// <param name="TopNumber">需要返回的记录数</param>
        /// <param name="ExchangeType">供求类别 =null返回全部</param>
        /// <param name="Tag">供求标签 =null返回全部</param>
        /// <param name="IsTop">是否置顶 =null返回全部</param>
        /// <param name="ProvinceId">省份编号 =0返回全部</param>
        /// <param name="CityId">城市编号 =0返回全部</param>
        /// <param name="IssueTime">添加时间 =null返回全部</param>
        /// <param name="OperatorId">用户编号 =""返回全部</param>
        /// <param name="KeyWord">需要匹配的关键字</param>
        /// <returns>供求信息列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopNumList(int TopNumber, EyouSoft.Model.CommunityStructure.ExchangeType? ExchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag? Tag, bool? IsTop, int ProvinceId, int CityId, DateTime? IssueTime, string OperatorId, string KeyWord)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder(" where 1=1 and IsCheck='1' ");
            if (ExchangeType.HasValue)
            {
                strWhere.AppendFormat(" and TopicClassID={0} ", (int)ExchangeType.Value);
            }
            if (Tag.HasValue)
            {
                strWhere.AppendFormat(" and ExchangeTagId={0} ", (int)Tag.Value);
            }
            if (ProvinceId > 0)
            {
                strWhere.AppendFormat(" and ID in(select ExchangeId from tbl_ExchangeCityContact where ProvinceId={0} )", ProvinceId);
            }
            if (CityId > 0)
            {
                strWhere.AppendFormat(" and ID in(select ExchangeId from tbl_ExchangeCityContact where cityId={0} )", CityId);
            }
            if (IssueTime.HasValue)
            {
                strWhere.AppendFormat(" and datediff(dd,'{0}',IssueTime)=0 ", IssueTime.Value.ToString());
            }
            if (!string.IsNullOrEmpty(OperatorId))
            {
                strWhere.AppendFormat(" and OperatorId='{0}' ", OperatorId);
            }
            if (!string.IsNullOrEmpty(KeyWord))
            {
                strWhere.AppendFormat(" and ExchangeTitle like '%{0}%'", KeyWord);
            }
            if (IsTop.HasValue)
            {
                strWhere.AppendFormat(" and IsTop='{0}' ", IsTop.Value ? "1" : "0");
            }
            strWhere.AppendFormat(" AND Category IN({0},{1}) ", (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.供, (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.求);
            #endregion

            string strSql = string.Format(SQL_ExchangeList_GetTopNumList, TopNumber > 0 ? string.Format(" top {0} ", TopNumber) : string.Empty, strWhere.ToString());
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeList model = new EyouSoft.Model.CommunityStructure.ExchangeList();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(1).ToString());
                    if (!dr.IsDBNull(2))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(2).ToString());
                    model.ExchangeTitle = dr.IsDBNull(3) ? string.Empty : dr.GetString(3);
                    model.ExchangeText = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.WriteBackCount = dr.GetInt32(5);
                    model.ViewCount = dr.GetInt32(6);
                    model.CompanyId = dr.IsDBNull(7) ? string.Empty : dr.GetString(7);
                    model.CompanyName = dr.IsDBNull(8) ? string.Empty : dr.GetString(8);
                    model.ProvinceId = dr.GetInt32(9);
                    model.CityId = dr.GetInt32(10);
                    model.ContactName = dr.IsDBNull(11) ? string.Empty : dr.GetString(11);
                    model.OperatorMQ = dr.IsDBNull(12) ? string.Empty : dr.GetString(12);
                    model.IssueTime = dr.GetDateTime(13);
                    model.IsTop = dr.IsDBNull(14) ? false : (dr.GetString(14) == "1" ? true : false);
                    model.TopTime = dr.IsDBNull(15) ? DateTime.MinValue : dr.GetDateTime(15);
                    model.AttatchPath = dr.IsDBNull(16) ? string.Empty : dr.GetString(16);
                    model.ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), dr.GetByte(17).ToString());
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        /// <summary>
        /// 删除供求信息图片
        /// </summary>
        /// <param name="ID">图片主键编号</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool DeletePhoto(string ID)
        {
            string strSql = "delete tbl_ExchangePhoto where ImgID=@ID";
            DbCommand dc = this._database.GetSqlStringCommand(strSql);
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, ID);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 设置用户浏览记录
        /// </summary>
        /// <param name="model">供求浏览实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool SetVisited(EyouSoft.Model.CommunityStructure.ExchangeVisited model)
        {
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeList_SetVisited);
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, model.ExchangeId);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 分页获取供求信息浏览记录
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetVisitedList(int pageSize, int pageIndex, ref int recordCount, string OperatorId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> list = new List<EyouSoft.Model.CommunityStructure.ExchangeListBase>();
            string tableName = "tbl_ExchangeList";
            string fields = "ID,TopicClassID,ExchangeTagId,ExchangeTitle,CompanyId,CompanyName,OperatorMQ,IssueTime";
            string primaryKey = "ID";
            string orderByString = "IssueTime desc";

            #region 生成查询条件
            StringBuilder strWhere = new StringBuilder(" 1=1 ");
            if (string.IsNullOrEmpty(OperatorId))
            {
                strWhere.AppendFormat(" and ID in(select ExchangeId from tbl_ExchangeVisited where OperatorId='{0}')", OperatorId);
            }
            strWhere.Append(" and ID in(select ExchangeId from tbl_ExchangeVisited)");
            #endregion

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount, tableName, primaryKey, fields, strWhere.ToString(), orderByString))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeListBase model = new EyouSoft.Model.CommunityStructure.ExchangeListBase();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(1).ToString());
                    if (!dr.IsDBNull(2))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(2).ToString());
                    model.ExchangeTitle = dr.GetString(3);
                    model.CompanyId = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.CompanyName = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.OperatorMQ = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.IssueTime = dr.IsDBNull(7) ? DateTime.MaxValue : dr.GetDateTime(7);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }

        /// <summary>
        /// 获取指定条数的供求信息浏览记录
        /// </summary>
        /// <param name="topNumber">需要返回的总记录数</param>
        /// <param name="OperatorId">添加人编号 =""返回全部</param>
        /// <returns>供求信息浏览记录列表</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> GetTopVisitedList(int topNumber, string OperatorId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeListBase> list = new List<EyouSoft.Model.CommunityStructure.ExchangeListBase>();
            #region 生成SQL语句
            StringBuilder strSql = new StringBuilder("select ");
            if (topNumber > 0)
            {
                strSql.AppendFormat(" top {0} ", topNumber);
            }
            strSql.Append(" ID,TopicClassID,ExchangeTagId,ExchangeTitle,CompanyId,CompanyName,OperatorMQ,IssueTime,ProvinceId from tbl_ExchangeList");
            if (string.IsNullOrEmpty(OperatorId))
            {
                strSql.AppendFormat(" where ID in(select ExchangeId from tbl_ExchangeVisited where OperatorId='{0}')", OperatorId);
            }
            else
            {
                strSql.Append(" where ID in(select ExchangeId from tbl_ExchangeVisited)");
            }
            strSql.Append(" order by IssueTime desc ");
            #endregion
            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeListBase model = new EyouSoft.Model.CommunityStructure.ExchangeListBase();
                    model.ID = dr.GetString(0);
                    if (!dr.IsDBNull(1))
                        model.TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)int.Parse(dr.GetByte(1).ToString());
                    if (!dr.IsDBNull(2))
                        model.ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)int.Parse(dr.GetByte(2).ToString());
                    model.ExchangeTitle = dr.GetString(3);
                    model.CompanyId = dr.IsDBNull(4) ? string.Empty : dr.GetString(4);
                    model.CompanyName = dr.IsDBNull(5) ? string.Empty : dr.GetString(5);
                    model.OperatorMQ = dr.IsDBNull(6) ? string.Empty : dr.GetString(6);
                    model.IssueTime = dr.IsDBNull(7) ? DateTime.MaxValue : dr.GetDateTime(7);
                    model.ProvinceId = dr.GetInt32(8);
                    list.Add(model);
                    model = null;
                }
            }
            return list;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取指定供求信息的城市关系列表
        /// </summary>
        /// <param name="ExchangeId">供求信息编号</param>
        /// <returns></returns>
        private IList<EyouSoft.Model.CommunityStructure.ExchangeCityContact> GetCityContactList(string ExchangeId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangeCityContact> list = new List<EyouSoft.Model.CommunityStructure.ExchangeCityContact>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangeCityContact_SELECT);
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, ExchangeId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangeCityContact CityModel = new EyouSoft.Model.CommunityStructure.ExchangeCityContact();
                    CityModel.ExchangeId = dr.GetString(0);
                    CityModel.CityId = dr.IsDBNull(1) ? 0 : dr.GetInt32(1);
                    CityModel.ProvinceId = dr.IsDBNull(2) ? 0 : dr.GetInt32(2);
                    list.Add(CityModel);
                    CityModel = null;
                }
            }
            return list;
        }
        private IList<EyouSoft.Model.CommunityStructure.ExchangePhoto> GetExchangePhotos(string ExchangeId)
        {
            IList<EyouSoft.Model.CommunityStructure.ExchangePhoto> list = new List<EyouSoft.Model.CommunityStructure.ExchangePhoto>();
            DbCommand dc = this._database.GetSqlStringCommand(SQL_ExchangePhoto_SELECT);
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, ExchangeId);
            using (IDataReader dr = DbHelper.ExecuteReader(dc, this._database))
            {
                while (dr.Read())
                {
                    EyouSoft.Model.CommunityStructure.ExchangePhoto PhotoModel = new EyouSoft.Model.CommunityStructure.ExchangePhoto();
                    PhotoModel.ImgId = dr.GetString(0);
                    PhotoModel.ExchangeId = dr.GetString(1);
                    PhotoModel.ImgPath = dr.GetString(2);
                    PhotoModel.IssueTime = dr.GetDateTime(3);
                    list.Add(PhotoModel);
                    PhotoModel = null;
                }
            }
            return list;
        }

        #endregion

        #region zhengfj

        #region  增删改

        /// <summary>
        /// 修改供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool UpdateExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model)
        {
            #region 生成SQL语句
            StringBuilder strSql = new StringBuilder(SQL_ExchangeList_UPDATENew);
            strSql.Append(SQL_ExchangeCityContact_DELETE); //删除原有的城市关系
            //strSql.Append(SQL_ExchangePhoto_DELETE);//删除原有的图片
            if (model.CityContactList != null && model.CityContactList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeCityContact CityModel in model.CityContactList)
                {
                    strSql.AppendFormat(SQL_ExchangeCityContact_ADD, model.ID, CityModel.ProvinceId, CityModel.CityId);
                }
            }
            if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangePhoto PhotoModel in model.ExchangePhotoList)
                {
                    strSql.AppendFormat(SQL_DELETEDFILE_UPDATEMOVE, PhotoModel.ImgId, PhotoModel.ImgPath);
                    strSql.AppendFormat(SQL_ExchangePhoto_DELETE, PhotoModel.ImgId);
                    strSql.AppendFormat(SQL_ExchangePhoto_ADD, Guid.NewGuid().ToString(), model.ID, PhotoModel.ImgPath, DateTime.Now.ToString());
                }
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ExchangeId", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "TopicClassID", DbType.Byte, (int)model.TopicClassID);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "ExchangeTagId", DbType.Byte, (int)model.ExchangeTag);
            this._database.AddInParameter(dc, "ExchangeTitle", DbType.String, model.ExchangeTitle);
            this._database.AddInParameter(dc, "ExchangeText", DbType.String, model.ExchangeText);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "AttatchPath", DbType.String, model.AttatchPath);
            this._database.AddInParameter(dc, "IsTop", DbType.AnsiStringFixedLength, model.IsTop ? "1" : "0");
            //this._database.AddInParameter(dc, "TopTime", DbType.DateTime, model.IsTop?DateTime.Now:DateTime.MinValue);
            this._database.AddInParameter(dc, "Category", DbType.Byte, (int)model.ExchangeCategory);
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }
        /// <summary>
        /// 添加供求信息
        /// </summary>
        /// <param name="model">供求实体</param>
        /// <returns>true:成功 false:失败</returns>
        public virtual bool AddExchangeList(EyouSoft.Model.CommunityStructure.ExchangeList model)
        {
            //string ExchangeId = Guid.NewGuid().ToString();
            //model.ID = ExchangeId;
            #region 生成添加SQL语句
            StringBuilder strSql = new StringBuilder(SQL_ExchangeList_ADDNew);
            if (model.CityContactList != null && model.CityContactList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangeCityContact CityModel in model.CityContactList)
                {
                    strSql.AppendFormat(SQL_ExchangeCityContact_ADD, model.ID, CityModel.ProvinceId, CityModel.CityId);
                }
            }
            if (model.ExchangePhotoList != null && model.ExchangePhotoList.Count > 0)
            {
                foreach (EyouSoft.Model.CommunityStructure.ExchangePhoto PhotoModel in model.ExchangePhotoList)
                {
                    strSql.AppendFormat(SQL_ExchangePhoto_ADD, Guid.NewGuid().ToString(), model.ID, PhotoModel.ImgPath, DateTime.Now.ToString());
                }
            }
            #endregion

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "ID", DbType.AnsiStringFixedLength, model.ID);
            this._database.AddInParameter(dc, "TopicClassID", DbType.Byte, (int)model.TopicClassID);
            this._database.AddInParameter(dc, "ExchangeTagId", DbType.Byte, (int)model.ExchangeTag);
            this._database.AddInParameter(dc, "Category", DbType.Byte, (int)model.ExchangeCategory);
            this._database.AddInParameter(dc, "ExchangeTitle", DbType.String, model.ExchangeTitle);
            this._database.AddInParameter(dc, "ExchangeText", DbType.String, model.ExchangeText);
            this._database.AddInParameter(dc, "CompanyId", DbType.AnsiStringFixedLength, model.CompanyId);
            this._database.AddInParameter(dc, "CompanyName", DbType.String, model.CompanyName);
            this._database.AddInParameter(dc, "OperatorId", DbType.AnsiStringFixedLength, model.OperatorId);
            this._database.AddInParameter(dc, "OperatorName", DbType.String, model.OperatorName);
            this._database.AddInParameter(dc, "OperatorMQ", DbType.String, model.OperatorMQ);
            this._database.AddInParameter(dc, "ProvinceId", DbType.Int32, model.ProvinceId);
            this._database.AddInParameter(dc, "CityId", DbType.Int32, model.CityId);
            this._database.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            this._database.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            this._database.AddInParameter(dc, "IssueTime", DbType.DateTime, DateTime.Now);
            this._database.AddInParameter(dc, "AttatchPath", DbType.String, model.AttatchPath);
            this._database.AddInParameter(dc, "IsCheck", DbType.AnsiStringFixedLength, model.IsCheck ? "1" : "0");
            return DbHelper.ExecuteSqlTrans(dc, this._database) > 0 ? true : false;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 获取当天的供求信息量
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>当天供求量总数</returns>
        public virtual int GetExchangeListCount(bool way)
        {
            string sql = way ? SQL_ExchangeList_GetExchangeListCount : SQL_ExchangeList_GetExchangeListCount + " WHERE DATEDIFF(DD,GETDATE(),IssueTime) = 0";
            DbCommand comm = this._database.GetSqlStringCommand(sql);

            return (int)DbHelper.GetSingle(comm, this._database);
        }

        /// <summary>
        /// 分页获取供求信息(请指定SearchInfo的BeforeOrAfter属性)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="info">查询实体</param>
        /// <returns></returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetList(int pageSize, int pageIndex, ref int recordCount,
            EyouSoft.Model.CommunityStructure.SearchInfo info)
        {
            StringBuilder query = new StringBuilder(" 1 = 1");
            string orderBy = " IssueTime DESC";
            if (info != null)
            {
                #region 时间条件
                //构造查询条件
                switch (info.SearchDateTimeType)
                {
                    case EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe.今天:
                        query.Append(" AND DATEDIFF(dd,GETDATE(),IssueTime) = 0");
                        break;
                    case EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe.昨天:
                        query.Append(" AND DATEDIFF(dd,GETDATE(),IssueTime) = -1");
                        break;
                    case EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe.前天:
                        query.Append(" AND DATEDIFF(dd,GETDATE(),IssueTime) = -2");
                        break;
                    case EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe.更早:
                        query.Append(" AND DATEDIFF(dd,GETDATE(),IssueTime) < -2");
                        break;
                    case EyouSoft.Model.CommunityStructure.SearchDateTimeTyoe.时间段:
                        if (!string.IsNullOrEmpty(info.StartDate))                  //开始时间
                        {
                            query.AppendFormat(" AND DATEDIFF(dd,IssueTime,'{0}') <= 0", info.StartDate);
                        }
                        if (!string.IsNullOrEmpty(info.EndDate))                    //结束时间
                        {
                            query.AppendFormat(" AND DATEDIFF(dd,IssueTime,'{0}') >= 0", info.EndDate);
                        }
                        break;
                    default:
                        break;

                }
                #endregion
                if (info.ExchangeTag.HasValue)                                       //供求标签
                {
                    query.AppendFormat(" AND ExchangeTagId = {0}", (int)info.ExchangeTag);
                }
                if (info.ExchangeType.HasValue)                                      //供求类别
                {
                    query.AppendFormat(" AND TopicClassID = {0}", (int)info.ExchangeType);
                }
                if (info.ExchangeCategory.HasValue)                                 //供或求
                {
                    query.AppendFormat(" AND Category = {0}", (int)info.ExchangeCategory);
                }
                else
                {
                    query.AppendFormat(" AND Category IN({0},{1}) ", (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.供, (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.求);
                }
                if (!string.IsNullOrEmpty(info.ExchangeTitle))                      //标题关键字
                {
                    query.AppendFormat(" AND ExchangeTitle LIKE '%{0}%'", info.ExchangeTitle);
                }
                if (info.Province.HasValue)                                         //省份
                {
                    query.AppendFormat(" AND ID IN(SELECT ExchangeId FROM tbl_ExchangeCityContact WHERE ProvinceId = {0} )", info.Province);
                }
                if (info.City.HasValue)                                             //城市
                {
                    query.AppendFormat(" AND ID IN(SELECT ExchangeId FROM tbl_ExchangeCityContact WHERE CityId = {0} )", info.City);
                }
                if (!string.IsNullOrEmpty(info.OperatorId))
                {
                    query.AppendFormat(" AND OperatorId = {0}", info.OperatorId);
                }
                if (!string.IsNullOrEmpty(info.OfferId))
                {
                    query.AppendFormat(" AND Id='{0}' ", info.OfferId);
                }

                ////如果供求标签为全部，则优先读取EyouSoft.Model.CommunityStructure.ExchangeTag.急急急的供求信息
                //if (!info.ExchangeTag.HasValue && !info.BeforeOrAfter)
                //{
                //    orderBy = string.Format(" CASE WHEN ExchangeTagId = {0} THEN 1 ELSE 2 END,IssueTime DESC", (int)EyouSoft.Model.CommunityStructure.ExchangeTag.急急急);
                //}
                if (!info.BeforeOrAfter)
                {
                    orderBy = " IsTop DESC,IssueTime DESC";
                }
            }

            string fields = "ID,TopicClassID,ExchangeTagId,ExchangeTitle,WriteBackCount,ViewCount,CompanyName,OperatorName,OperatorMQ,ProvinceId,CityId,IssueTime,AttatchPath,Category";

            if (info != null && info.ExchangeCategory.HasValue && info.ExchangeCategory.Value == EyouSoft.Model.CommunityStructure.ExchangeCategory.QGroup)
            {
                fields += ",(SELECT [QUID] FROM [tbl_qqgroupmessage] WHERE [MId]=[tbl_ExchangeList].[ID]) AS QGroupSendU";
            }
            else
            {
                fields += ",0 AS QGroupSendU";
            }

            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();

            using (IDataReader reader = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount,
                "tbl_ExchangeList", "ID", fields, query.ToString(), orderBy))
            {
                GetIDataReader(reader, ref list);
            }

            return list;
        }

        /// <summary>
        /// 根据类别，标签获取指定条数的供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <param name="exchangeType">类别</param>
        /// <param name="exchangeTag">标签</param>
        /// <param name="way">true:最新供求 false:同类其他供求</param>
        /// <returns>供求信息集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum, EyouSoft.Model.CommunityStructure.ExchangeType exchangeType,
            EyouSoft.Model.CommunityStructure.ExchangeTag exchangeTag, bool way)
        {
            DbCommand comm = this._database.GetSqlStringCommand(way ? SQL_ExchangeList_GetTopList : SQL_ExchangeList_GetTopList2);
            this._database.AddInParameter(comm, "@topNum", DbType.Int32, topNum);
            this._database.AddInParameter(comm, "@topicClassID", DbType.Byte, (int)exchangeType);
            this._database.AddInParameter(comm, "@exchangeTagId", DbType.Byte, (int)exchangeTag);

            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();

            EyouSoft.Model.CommunityStructure.ExchangeList item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.CommunityStructure.ExchangeList()
                    {
                        ID = reader["ID"].ToString(),
                        TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), reader["TopicClassID"].ToString()),
                        ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), reader["ExchangeTagId"].ToString()),
                        ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), reader["Category"].ToString()),
                        ExchangeTitle = reader.IsDBNull(reader.GetOrdinal("ExchangeTitle")) ? "" : reader["ExchangeTitle"].ToString(),
                        ExchangeText = reader.IsDBNull(reader.GetOrdinal("ExchangeText")) ? "" : reader["ExchangeText"].ToString(),
                        OperatorMQ = reader["OperatorMQ"].ToString(),
                        ProvinceId = reader.IsDBNull(reader.GetOrdinal("ProvinceId")) ? 0 : (int)reader["ProvinceId"],
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString())
                    };

                    list.Add(item);
                }

                return list;
            }

        }


        /// <summary>
        /// 指定条数获取最新供求信息
        /// </summary>
        /// <param name="topNum">指定条数</param>
        /// <returns>供求信息集合</returns>
        public virtual IList<EyouSoft.Model.CommunityStructure.ExchangeList> GetTopList(int topNum)
        {
            DbCommand comm = this._database.GetSqlStringCommand(string.Format("SELECT TOP(@topNum) ID,TopicClassID,ExchangeTagId,Category,ExchangeTitle,ExchangeText,OperatorMQ,ProvinceId,IssueTime FROM tbl_ExchangeList WHERE Category IN({0},{1}) Order By IssueTime DESC", (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.供, (int)EyouSoft.Model.CommunityStructure.ExchangeCategory.求));
            this._database.AddInParameter(comm, "@topNum", DbType.Int32, topNum);

            IList<EyouSoft.Model.CommunityStructure.ExchangeList> list = new List<EyouSoft.Model.CommunityStructure.ExchangeList>();

            EyouSoft.Model.CommunityStructure.ExchangeList item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    item = new EyouSoft.Model.CommunityStructure.ExchangeList()
                    {
                        ID = reader["ID"].ToString(),
                        TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), reader["TopicClassID"].ToString()),
                        ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), reader["ExchangeTagId"].ToString()),
                        ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), reader["Category"].ToString()),
                        ExchangeTitle = reader.IsDBNull(reader.GetOrdinal("ExchangeTitle")) ? "" : reader["ExchangeTitle"].ToString(),
                        ExchangeText = reader.IsDBNull(reader.GetOrdinal("ExchangeText")) ? "" : reader["ExchangeText"].ToString(),
                        OperatorMQ = reader["OperatorMQ"].ToString(),
                        ProvinceId = reader.IsDBNull(reader.GetOrdinal("ProvinceId")) ? 0 : (int)reader["ProvinceId"],
                        IssueTime = DateTime.Parse(reader["IssueTime"].ToString())
                    };

                    list.Add(item);
                }

                return list;
            }
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 读取IDataReader
        /// </summary>
        /// <param name="reader">读取结果集</param>
        /// <param name="list">集合</param>
        /// <returns>供求信息列表</returns>
        private void GetIDataReader(IDataReader reader, ref IList<EyouSoft.Model.CommunityStructure.ExchangeList> list)
        {
            EyouSoft.Model.CommunityStructure.ExchangeList item = null;
            while (reader.Read())
            {
                item = new EyouSoft.Model.CommunityStructure.ExchangeList()
                {
                    ID = reader["ID"].ToString(),
                    TopicClassID = (EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeType), reader["TopicClassID"].ToString()),
                    ExchangeTag = (EyouSoft.Model.CommunityStructure.ExchangeTag)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeTag), reader["ExchangeTagId"].ToString()),
                    ExchangeCategory = (EyouSoft.Model.CommunityStructure.ExchangeCategory)Enum.Parse(typeof(EyouSoft.Model.CommunityStructure.ExchangeCategory), reader["Category"].ToString()),
                    ExchangeTitle = reader.IsDBNull(reader.GetOrdinal("ExchangeTitle")) ? "" : reader["ExchangeTitle"].ToString(),
                    WriteBackCount = (int)reader["WriteBackCount"],
                    ViewCount = (int)reader["ViewCount"],
                    CompanyName = reader.IsDBNull(reader.GetOrdinal("CompanyName")) ? "" : reader["CompanyName"].ToString(),
                    OperatorName = reader.IsDBNull(reader.GetOrdinal("OperatorName")) ? "" : reader["OperatorName"].ToString(),
                    OperatorMQ = reader["OperatorMQ"].ToString(),
                    ProvinceId = reader.IsDBNull(reader.GetOrdinal("ProvinceId")) ? 0 : (int)reader["ProvinceId"],
                    CityId = reader.IsDBNull(reader.GetOrdinal("CityId")) ? 0 : (int)reader["CityId"],
                    IssueTime = DateTime.Parse(reader["IssueTime"].ToString()),
                    AttatchPath = reader["AttatchPath"].ToString(),
                    QGroupSendU = reader["QGroupSendU"].ToString()
                };

                list.Add(item);
            }
        }
        #endregion

        #endregion

        #region IExchangeList 成员

        /// <summary>
        /// 获取各分类的供求总数
        /// </summary>
        /// <param name="way">true:总供求量 false:当天供求量</param>
        /// <returns>字典</returns>
        public virtual Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int> GetExchangeTypeCount(bool way)
        {
            StringBuilder sql = new StringBuilder("SELECT TopicClassId,ISNULL(COUNT(ID),0) Num FROM tbl_ExchangeList");
            if (!way)
                sql.Append(" WHERE DATEDIFF(DD,GETDATE(),IssueTime) = 0");
            sql.Append(" Group By TopicClassId");

            DbCommand comm = this._database.GetSqlStringCommand(sql.ToString());

            Type exchangeType = typeof(EyouSoft.Model.CommunityStructure.ExchangeType);

            Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int> dic =
                new Dictionary<EyouSoft.Model.CommunityStructure.ExchangeType, int>();

            foreach (EyouSoft.Model.CommunityStructure.ExchangeType type
                in Enum.GetValues(exchangeType))
            {
                dic.Add(type, 0);  //初始值为0
            }

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._database))
            {
                while (reader.Read())
                {
                    dic[(EyouSoft.Model.CommunityStructure.ExchangeType)Enum.Parse(exchangeType, reader["TopicClassId"].ToString())]
                        = (int)reader["Num"];
                }
            }

            return dic;

        }

        /// <summary>
        /// 判断用户是否发布过这个标题的供求信息
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="strTitle">供求信息标题</param>
        /// <param name="strExchangeId">要排除的供求Id，为空不排除</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回是否存在</returns>
        public virtual bool ExistsSameTitle(string strUserId, string strTitle, string strExchangeId, DateTime? startTime
            , DateTime? endTime)
        {
            if (string.IsNullOrEmpty(strUserId) || string.IsNullOrEmpty(strTitle))
                return false;

            var strSql = new StringBuilder();
            strSql.Append(" select count(ID) from tbl_ExchangeList ");
            strSql.AppendFormat(" where OperatorId = '{0}' ", strUserId);
            strSql.AppendFormat(" and ExchangeTitle = '{0}' ", strTitle.Trim());
            if (!string.IsNullOrEmpty(strExchangeId))
                strSql.AppendFormat(" and ID <> '{0}' ", strExchangeId.Trim());
            if (startTime.HasValue)
                strSql.AppendFormat(" and IssueTime >= '{0}' ", startTime);
            if (endTime.HasValue)
                strSql.AppendFormat(" and IssueTime <= '{0}' ", endTime);

            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());
            return DbHelper.Exists(dc, _database);
        }

        /// <summary>
        /// 获取用户某个时间段内发布的供求信息数量
        /// </summary>
        /// <param name="strUserId">用户Id</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>返回是否存在</returns>
        public int GetExchangeListCount(string strUserId, DateTime? startTime, DateTime? endTime)
        {
            if (string.IsNullOrEmpty(strUserId))
                return 0;

            int iReturn = 0;
            var strSql = new StringBuilder();
            strSql.Append(" select count(ID) from tbl_ExchangeList ");
            strSql.AppendFormat(" where OperatorId = '{0}' ", strUserId);

            if (startTime.HasValue)
                strSql.AppendFormat(" and IssueTime >= '{0}' ", startTime);
            if (endTime.HasValue)
                strSql.AppendFormat(" and IssueTime <= '{0}' ", endTime);

            DbCommand dc = _database.GetSqlStringCommand(strSql.ToString());

            using (IDataReader dr = DbHelper.ExecuteReader(dc,_database))
            {
                if(dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        iReturn = dr.GetInt32(0);
                }
            }

            return iReturn;
        }

        /// <summary>
        /// 写入QQ群消息，返回1成功，其它失败
        /// </summary>
        /// <param name="info">QQ群消息业务实体</param>
        /// <returns>1：成功 其它失败</returns>
        public virtual int QG_InsertQQGroupMessage(EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo info)
        {
            DbCommand cmd = _database.GetSqlStringCommand(SQL_INSERT_QG_InsertQQGroupMessage);
            _database.AddInParameter(cmd, "MId", DbType.AnsiStringFixedLength, info.MessageId);
            _database.AddInParameter(cmd, "Title", DbType.String, info.Title);
            _database.AddInParameter(cmd, "Content", DbType.String, info.Content);
            _database.AddInParameter(cmd, "QGID", DbType.AnsiString, info.QGID);
            _database.AddInParameter(cmd, "QUID", DbType.AnsiString, info.QUID);
            _database.AddInParameter(cmd, "QMTime", DbType.AnsiString, info.QMTime);
            _database.AddInParameter(cmd, "IssueTime", DbType.DateTime, info.IssueTime);
            _database.AddInParameter(cmd, "Status", DbType.Byte, info.Status);
            _database.AddInParameter(cmd, "FUID", DbType.AnsiString, info.FUID);

            int sqlExceptionCode = 0;

            try
            {
                sqlExceptionCode = DbHelper.ExecuteSql(cmd, _database) > 0 ? 1 : 0;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                sqlExceptionCode = 0 - e.Number;
            }

            return sqlExceptionCode;
        }

        /// <summary>
        /// 获取QQ群消息业务实体
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <returns></returns>
        public virtual EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo QG_GetQQGroupMessageInfo(string messageId)
        {
            DbCommand cmd = _database.GetSqlStringCommand(SQL_SELECT_QG_GetQQGroupMessageInfo);
			_database.AddInParameter(cmd, "MId", DbType.AnsiStringFixedLength, messageId);
            EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo info = null;

            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, _database))
            {
                if (rdr.Read())
                {
                    info = new EyouSoft.Model.CommunityStructure.MQQGroupMessageInfo()
                    {
                        Content = rdr["Content"].ToString(),
                        IssueTime = rdr.GetDateTime(rdr.GetOrdinal("IssueTime")),
                        MessageId = messageId,
                        QGID = rdr["QGID"].ToString(),
                        QMTime = rdr["QMTime"].ToString(),
                        QUID = rdr["QUID"].ToString(),
                        Status = (EyouSoft.Model.CommunityStructure.QQGroupMessageStatus)rdr.GetByte(rdr.GetOrdinal("Status")),
                        Title = rdr["Title"].ToString(),
                        FUID = rdr["FUID"].ToString()
                    };
                }
            }

            return info;
        }
        /// <summary>
        /// 设置QQ群消息状态
        /// </summary>
        /// <param name="messageId">消息编号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public virtual bool QG_SetQQGroupMessageStatus(string messageId, EyouSoft.Model.CommunityStructure.QQGroupMessageStatus status)
        {
            DbCommand cmd = _database.GetSqlStringCommand(SQL_UPDATE_QG_SetQQGroupMessageStatus);
            _database.AddInParameter(cmd, "Status", DbType.Byte, status);
            _database.AddInParameter(cmd, "MId", DbType.AnsiStringFixedLength, messageId);

            return DbHelper.ExecuteSql(cmd, _database) > 0 ? true : false;
        }

        /// <summary>
        /// 设置QQ群消息供求标题
        /// </summary>
        /// <param name="offerId">供求信息编号</param>
        /// <param name="title">供求标题</param>
        /// <returns></returns>
        public virtual bool QG_SetQQGroupOfferTitle(string offerId, string title)
        {
            DbCommand cmd = _database.GetSqlStringCommand(SQL_UPDATE_QG_SetQQGroupOfferTitle);
            _database.AddInParameter(cmd, "Title", DbType.String, title);
            _database.AddInParameter(cmd, "OfferId", DbType.AnsiStringFixedLength, offerId);

            return DbHelper.ExecuteSql(cmd, _database) > 0 ? true : false;
        }
        #endregion
    }
}
