using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Xml;
using EyouSoft.Common.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.MQStructure
{
    /// <summary>
    /// 创建人：郑知远 2011-05-27
    /// 描述：MQ同业中心数据层
    /// </summary>
    public class IMSuperCluster:DALBase,IDAL.MQStructure.IIMSuperCluster
    {
        /// <summary>
        /// 数据库变量
        /// </summary>
        protected Database _database;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public IMSuperCluster()
        {
            this._database = base.SystemStore;
        }
        #endregion

        #region 同业中心前台管理方法

        /// <summary>
        /// 根据聊天内容、超级群Id获取该同业中心消息记录
        /// </summary>
        /// <param name="days">最近几天</param>
        /// <param name="content">聊天内容</param>
        /// <param name="clusterId">超级群ID</param>
        /// <returns>超级群消息列表</returns>
        public IList<Model.MQStructure.IMSuperClusterMsg> GetSuperClusterMsg(int days,string content, int clusterId)
        {
            // 信息实体列表
            IList<EyouSoft.Model.MQStructure.IMSuperClusterMsg> lst = new List<EyouSoft.Model.MQStructure.IMSuperClusterMsg>();

            // SQL编辑器
            var strSql = new StringBuilder();

            strSql.Append(" SELECT");
            strSql.Append("     A.msg_datetime AS DisplayDate");
            strSql.Append("     ,B.UserName + '(' + CAST(A.msg_user_mq_send AS VARCHAR(MAX)) + ') ' + CONVERT(VARCHAR(8),A.msg_datetime,108) AS MessageSender");
            strSql.Append("	    ,A.msg_content AS MessageContent");                
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperClusterMsg AS A");
            strSql.Append(" INNER JOIN");
            strSql.Append("     tbl_CompanyUser AS B");
            strSql.Append(" ON");
            strSql.Append("     A.msg_user_mq_send = B.MQ");
            strSql.Append("     AND B.IsDeleted = 0");
            strSql.Append(" WHERE");
            strSql.Append("     A.msg_supercluster = @clusterId");
            strSql.Append("     AND CONVERT(VARCHAR(10),A.msg_datetime,120) BETWEEN CONVERT(VARCHAR(10),DATEADD(d,1-@days,GETDATE()),120) AND CONVERT(VARCHAR(10),GETDATE(),120)");
            if (!string.IsNullOrEmpty(content))
            {
                strSql.Append(@"     AND dbo.Clr_RegexReplace(A.msg_content,'\[Pic\]({.*?}\..*?)\[/Pic\]|/:[a-zA-Z][0-9]','',0) LIKE '%" + Common.Utility.ToSqlLike(content) + "%'");             
            }
            strSql.Append(" ORDER BY A.msg_datetime ");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@clusterId", DbType.Int32, clusterId);                  // 同业中心ID
            this._database.AddInParameter(cmd, "@days", DbType.Int32, days);                            // 最近几天

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 信息实体
                    var model = new EyouSoft.Model.MQStructure.IMSuperClusterMsg
                    {
                        // 日期
                        DisplayDate = dr.GetDateTime(dr.GetOrdinal("DisplayDate")),

                        // 消息发起者
                        MessageSender = dr.GetString(dr.GetOrdinal("MessageSender")),

                        // 消息内容
                        MessageContent = dr.IsDBNull(dr.GetOrdinal("MessageContent"))?string.Empty: dr.GetString(dr.GetOrdinal("MessageContent"))
                    };

                    // 追加到信息列表实体
                    lst.Add(model);
                }
            }

            // 返回信息列表实体
            return lst;
        }

        /// <summary>
        /// 根据同业MQ号获取同业中心会员名片信息
        /// </summary>
        /// <param name="clusterId">同业中心ID</param>
        /// <param name="mq">MQ帐号</param>
        /// <returns>会员名片信息实体</returns>
        public Model.MQStructure.IMClusterUserCard GetUserCardInfoByMq(int clusterId, int mq)
        {
            EyouSoft.Model.MQStructure.IMClusterUserCard model = null;                                  // 会员名片实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT TOP(1)");
            strSql.Append("     UserName");                                                             // 用户姓名（性别）
            strSql.Append("     ,CompanyID");                                                           // 公司ID
            strSql.Append("	    ,CompanyName");                                                         // 公司名称
            strSql.Append("	    ,Subject");                                                             // 主营业务
            strSql.Append("	    ,Contact");                                                             // 联系方式
            strSql.Append("	    ,MQ");                                                                  // MQ
            strSql.Append("	    ,CompanyLogo");                                                         // 公司LOGO地址
            strSql.Append("	    ,EshopUrl");                                                            // 公司网店地址
            strSql.Append(" FROM");
            strSql.Append("     view_ClusterUserCardInfo_All");
            strSql.Append(" WHERE");
            strSql.Append("     ClusterID = @ClusterID");
            strSql.Append("     AND MQ = @MQ");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@ClusterID", DbType.Int32, clusterId);                  // 同业中心ID
            this._database.AddInParameter(cmd, "@MQ", DbType.Int32, mq);                                // 同业MQ号

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    model = new EyouSoft.Model.MQStructure.IMClusterUserCard
                    {
                        // 用户姓名（性别）
                        UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) ? string.Empty : dr.GetString(dr.GetOrdinal("UserName")),

                        // 公司ID
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyID")),

                        // 公司名称
                        CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? string.Empty : dr.GetString(dr.GetOrdinal("CompanyName")),

                        // 主营业务
                        Subject = dr.IsDBNull(dr.GetOrdinal("Subject")) ? string.Empty : dr.GetString(dr.GetOrdinal("Subject")),

                        // 联系方式
                        Contact = dr.IsDBNull(dr.GetOrdinal("Contact")) ? string.Empty : dr.GetString(dr.GetOrdinal("Contact")),

                        // 同业MQ号
                        MQ = dr.IsDBNull(dr.GetOrdinal("MQ")) ? 0 : dr.GetInt32(dr.GetOrdinal("MQ")),

                        // 公司LOGO地址
                        CompanyLogo = dr.IsDBNull(dr.GetOrdinal("CompanyLogo")) ? string.Empty : dr.GetString(dr.GetOrdinal("CompanyLogo")),

                        // 公司网店地址
                        EshopUrl = dr.IsDBNull(dr.GetOrdinal("EshopUrl")) ? string.Empty : dr.GetString(dr.GetOrdinal("EshopUrl")),
                    };
                }
            }

            // 返回会员名片实体
            return model;
        }

        /// <summary>
        /// 根据同业中心ID获取该中心登录者除外的会员名片信息列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="clusterId">同业中心ID</param>
        /// <param name="mq">登录者MQ</param>
        /// <returns>会员名片实体列表</returns>
        public IList<Model.MQStructure.IMClusterUserCard> GetUserCardListByClusterId(int pageSize, int pageIndex, ref int recordCount, int clusterId, int mq)
        {
            var lst = new List<EyouSoft.Model.MQStructure.IMClusterUserCard>();                         // 会员名片实体列表
            var mdl = this.GetUserCardInfoByMq(clusterId, mq);                                          // 会员名片实体

            DbCommand cmd = this._database.GetStoredProcCommand(mdl == null ? "T_StocPage" : "T_StocPageFor_IMUserCard");

            // 首页
            if (pageIndex == 1 && mdl != null)
            {
                pageSize = pageSize - 1;
            }

            this._database.AddInParameter(cmd, "PageSize", DbType.Int32, pageSize);
            this._database.AddInParameter(cmd, "PageIndex", DbType.Int32, pageIndex);
            this._database.AddInParameter(cmd, "TableName", DbType.String, "view_ClusterUserCardInfo_All");
            this._database.AddInParameter(cmd, "FieldsList", DbType.String, "UserName,CompanyID,CompanyName,Subject,Contact,MQ,CompanyLogo,EshopUrl");
            this._database.AddInParameter(cmd, "FieldSearchKey", DbType.String, string.Format("ClusterID = {0} AND MQ <> {1}", clusterId, mq));
            this._database.AddInParameter(cmd, "OrderString", DbType.String, "Frequency DESC");
            this._database.AddInParameter(cmd, "IsGroupBy", DbType.String, "0");

            IDataReader dr = DbHelper.RunReaderProcedure(cmd, this._database);

            if (dr.Read())
            {
                recordCount = dr.GetInt32(0);
            }

            dr.NextResult();

            using (dr)
            {
                while (dr.Read())
                {
                    // 公告信息实体
                    var model = new EyouSoft.Model.MQStructure.IMClusterUserCard
                    {
                        // 用户姓名（性别）
                        UserName = dr.IsDBNull(dr.GetOrdinal("UserName")) ? string.Empty : dr.GetString(dr.GetOrdinal("UserName")),

                        // 公司ID
                        CompanyId = dr.GetString(dr.GetOrdinal("CompanyID")),

                        // 公司名称
                        CompanyName = dr.IsDBNull(dr.GetOrdinal("CompanyName")) ? string.Empty : dr.GetString(dr.GetOrdinal("CompanyName")),

                        // 主营业务
                        Subject = dr.IsDBNull(dr.GetOrdinal("Subject")) ? string.Empty : dr.GetString(dr.GetOrdinal("Subject")),

                        // 联系方式
                        Contact = dr.IsDBNull(dr.GetOrdinal("Contact")) ? string.Empty : dr.GetString(dr.GetOrdinal("Contact")),

                        // 同业MQ号
                        MQ = dr.IsDBNull(dr.GetOrdinal("MQ")) ? 0 : dr.GetInt32(dr.GetOrdinal("MQ")),

                        // 公司LOGO地址
                        CompanyLogo = dr.IsDBNull(dr.GetOrdinal("CompanyLogo")) ? string.Empty : dr.GetString(dr.GetOrdinal("CompanyLogo")),

                        // 公司网店地址
                        EshopUrl = dr.IsDBNull(dr.GetOrdinal("EshopUrl")) ? string.Empty : dr.GetString(dr.GetOrdinal("EshopUrl")),
                    };

                    // 追加到会员名片实体列表
                    lst.Add(model);
                }
            }

            if (pageIndex == 1 && mdl != null)
            {
                lst.Insert(0, mdl);
            }

            // 返回会员名片实体列表
            return lst;
        }

        #endregion

        #region 同业中心后台管理方法

        /// <summary>
        /// 添加同业中心
        /// </summary>
        /// <param name="model">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Add(EyouSoft.Model.MQStructure.IMSuperCluster model)
        {
            DbCommand dc = null;                                                                        // 命令基类

            dc = this._database.GetStoredProcCommand("proc_SuperCluster_AddCluster");                   // 执行存储过程

            this._database.AddInParameter(dc, "@Title", DbType.String, model.Title);                    // 名称
            this._database.AddInParameter(dc, "@Master", DbType.Int32, model.Master);                   // 总管理员
            this._database.AddInParameter(dc, "@SelectType", DbType.Int32, model.SelectType);           // 成员构成导入类型【1：选择省市 2：选择会员ID】
            this._database.AddInParameter(dc, "@SelectValue", DbType.String, model.SelectValue.TrimEnd(',').TrimStart(','));        // 用【，】隔开导入省份或者会员ID数据
            this._database.AddInParameter(dc, "@Num", DbType.Int32, model.Num);                         // 序号
            this._database.AddInParameter(dc, "@PassWord", DbType.String, model.PassWord);              // 密码
            this._database.AddInParameter(dc, "@Opertor", DbType.String, model.Opertor);                // 操作人
            this._database.AddInParameter(dc, "@OperateTime", DbType.DateTime, model.OperateTime);      // 操作时间

            try
            {
                return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
            }
            catch (Exception de)
            {
                throw de;
            }
        }

        /// <summary>
        /// 更新同业中心
        /// </summary>
        /// <param name="model">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Upd(EyouSoft.Model.MQStructure.IMSuperCluster model)
        {
            DbCommand dc = null;                                                                        // 命令基类

            dc = this._database.GetStoredProcCommand("proc_SuperCluster_UpdCluster");                   // 执行存储过程

            this._database.AddInParameter(dc, "@ID", DbType.Int32, model.Id);                           // 超级群ID
            this._database.AddInParameter(dc, "@Title", DbType.String, model.Title);                    // 名称
            this._database.AddInParameter(dc, "@Master", DbType.Int32, model.Master);                   // 总管理员
            this._database.AddInParameter(dc, "@SelectType", DbType.Int32, model.SelectType);           // 成员构成导入类型【1：选择省市 2：选择会员ID】
            this._database.AddInParameter(dc, "@SelectValue", DbType.String, model.SelectValue.TrimEnd(',').TrimStart(','));        // 用【，】隔开导入省份或者会员ID数据
            this._database.AddInParameter(dc, "@Num", DbType.Int32, model.Num);                         // 序号
            this._database.AddInParameter(dc, "@PassWord", DbType.String, model.PassWord);              // 密码
            this._database.AddInParameter(dc, "@Opertor", DbType.String, model.Opertor);                // 操作人
            this._database.AddInParameter(dc, "@OperateTime", DbType.DateTime, model.OperateTime);      // 操作时间

            try
            {
                return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
            }
            catch (Exception de)
            {
                throw de;
            }
        }

        /// <summary>
        /// 删除同业中心
        /// </summary>
        /// <param name="id">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool Del(int[] id)
        {
            DbCommand dc = null;                                                                        // 命令基类
            var strId = string.Empty;                                                                   // 用【，】隔开的同业中心ID

            dc = this._database.GetStoredProcCommand("proc_SuperCluster_DelCluster");                   // 执行存储过程

            strId = id.Aggregate(strId, (current, i) => current + i.ToString() + ",");

            this._database.AddInParameter(dc, "@ID", DbType.String, strId);                             // 用【，】隔开的超级群ID

            try
            {
                return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
            }
            catch (Exception de)
            {
                throw de;
            }
        }

        /// <summary>
        /// 根据同业中心名判断该同业中心是否存在
        /// </summary>
        /// <param name="name">同业中心名</param>
        /// <param name="id">同业中心ID：0（新增）其他（修改）</param>
        /// <returns>True：存在 False：不存在</returns>
        public bool IsExist(string name,int id)
        {
            var strSql = new StringBuilder();                                                           // SQL编辑器

            // 新增
            if (id == 0)
            {
                strSql.Append(" SELECT ID FROM tbl_SuperCluster WHERE title = @name");
            }
            // 修改
            else
            {
                strSql.AppendFormat(" SELECT ID FROM tbl_SuperCluster WHERE title = @name AND ID <> {0}", id);
            }

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());
            this._database.AddInParameter(dc, "@name", DbType.String, name);                            // 同业中心名

            return DbHelper.Exists(dc,this._database);
        }

        /// <summary>
        /// 根据同业中心ID和序号设置序号
        /// </summary>
        /// <param name="lst">同业中心实体</param>
        /// <returns>True：成功 False：失败</returns>
        public bool SetNums(IList<EyouSoft.Model.MQStructure.IMSuperCluster> lst)
        {
            var strSql = new StringBuilder();                                                           // SQL编辑器

            foreach (var model in lst)
            {
                strSql.Append(" UPDATE tbl_SuperCluster");
                strSql.AppendFormat(" SET Num = {0}", model.Num);
                strSql.Append(" WHERE");
                strSql.AppendFormat(" ID = {0}", model.Id);
            }

            DbCommand dc = this._database.GetSqlStringCommand(strSql.ToString());

            return DbHelper.ExecuteSql(dc, this._database) > 0 ? true : false;
        }

        /// <summary>
        /// 根据同业中心ID获取同业中心实体
        /// </summary>
        /// <param name="id">同业中心ID</param>
        /// <returns>同业中心实体</returns>
        public EyouSoft.Model.MQStructure.IMSuperCluster GetSuperClusterByID(int id)
        {
            var model = new EyouSoft.Model.MQStructure.IMSuperCluster();                                // 同业中心实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT");
            strSql.Append("     title");                                                                // 名称
            strSql.Append("     ,master");                                                              // 总管理员
            strSql.Append("     ,Num");                                                                 // 序号
            strSql.Append("	    ,SelectType");                                                          // 成员构成导入类型
            strSql.Append("	    ,SelectValue");                                                         // 用【，】隔开导入省份或者会员ID数据
            strSql.Append("	    ,PassWord");                                                            // 密码
            strSql.Append("	    ,Opertor");                                                             // 操作人
            strSql.Append("	    ,OperateTime");                                                         // 操作时间
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperCluster");
            strSql.Append(" WHERE");
            strSql.Append("     ID = @ID");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行
            this._database.AddInParameter(cmd, "@ID", DbType.Int32, id);                                // 同业中心ID

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 名称
                    model.Title = dr.IsDBNull(dr.GetOrdinal("title")) ? string.Empty : dr.GetString(dr.GetOrdinal("title"));

                    // 总管理员
                    model.Master = dr.IsDBNull(dr.GetOrdinal("master")) ? 0 : dr.GetInt32(dr.GetOrdinal("master"));

                    // 序号
                    model.Num = dr.IsDBNull(dr.GetOrdinal("Num")) ? 0 : dr.GetInt32(dr.GetOrdinal("Num"));

                    // 成员构成导入类型
                    model.SelectType = dr.IsDBNull(dr.GetOrdinal("SelectType")) ? EyouSoft.Model.MQStructure.SelectType.选择省市 : (EyouSoft.Model.MQStructure.SelectType)dr.GetInt32(dr.GetOrdinal("SelectType"));

                    // 用【，】隔开导入省份或者会员ID数据
                    model.SelectValue = dr.IsDBNull(dr.GetOrdinal("SelectValue")) ? string.Empty : dr.GetString(dr.GetOrdinal("SelectValue"));

                    // 密码
                    model.PassWord = dr.IsDBNull(dr.GetOrdinal("PassWord")) ? string.Empty : dr.GetString(dr.GetOrdinal("PassWord"));

                    // 发布人
                    model.Opertor = dr.IsDBNull(dr.GetOrdinal("Opertor")) ? string.Empty : dr.GetString(dr.GetOrdinal("Opertor"));

                    // 发布时间
                    model.OperateTime = dr.IsDBNull(dr.GetOrdinal("OperateTime")) ? DateTime.Now : dr.GetDateTime(dr.GetOrdinal("OperateTime"));
                }
            }

            // 返回同业中心实体
            return model;
        }

        /// <summary>
        /// 根据同业中心ID获取其他同业中心已选省市
        /// </summary>
        /// <param name="id">同业中心ID</param>
        /// <returns>已选省市实体列表</returns>
        public IList<EyouSoft.Model.SystemStructure.ProvinceBase> GetSelectedProvincesByID(int id)
        {
            var lst = new List<EyouSoft.Model.SystemStructure.ProvinceBase>();                          // 省市列表实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT");
            strSql.Append("     SelectValue");
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperCluster");
            strSql.Append(" WHERE");
            strSql.AppendFormat("     SelectType = {0}", (int)EyouSoft.Model.MQStructure.SelectType.选择省市);
            if (id != 0)
            {
                strSql.AppendFormat("     AND ID <> {0}", id);
            }

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(dr.GetOrdinal("SelectValue")) && !dr.GetString(dr.GetOrdinal("SelectValue")).Trim().Equals(string.Empty))
                    {
                        var strArrIds = dr.GetString(dr.GetOrdinal("SelectValue")).Split(',');
                        lst.AddRange(strArrIds.Select(s => new EyouSoft.Model.SystemStructure.ProvinceBase()
                                                               {
                                                                   // 超级群ID
                                                                   ProvinceId = int.Parse(s),
                                                               }));
                    }
                }
            }

            // 返回省市实体列表
            return lst;
        }

        /// <summary>
        /// 根据同业中心ID获取同业中心实体列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>        
        /// <returns>同业中心实体列表</returns>
        public IList<Model.MQStructure.IMSuperCluster> GetList(int pageSize, int pageIndex, ref int recordCount)
        {
            var lst = new List<EyouSoft.Model.MQStructure.IMSuperCluster>();                            // 同业中心实体列表

            using (IDataReader dr = DbHelper.ExecuteReader(this._database, pageSize, pageIndex, ref recordCount
                , "tbl_SuperCluster", "ID", "ID,Num,Title,CASE SelectType WHEN 1 THEN (SELECT D.ProvinceName,(SELECT COUNT(DISTINCT A.user_id) FROM tbl_SuperClusterUser AS A JOIN tbl_CompanyUser AS B ON B.Id = A.user_id AND B.ProvinceId = C.items AND B.IsEnable = 1) AS CountNum FROM (SELECT items FROM dbo.fn_Split(SelectValue ,',')) AS C INNER JOIN tbl_SysProvince AS D ON D.ID = C.items FOR XML PATH('Value'),ROOT('Values')) WHEN 2 THEN (SELECT '其他成员' AS ProvinceName,(SELECT COUNT(*) FROM (SELECT items FROM dbo.fn_Split(SelectValue ,',')) AS AA) AS CountNum FOR XML PATH('Value'),ROOT('Values')) END AS CountValue,master,PassWord,Opertor,OperateTime"
                , String.Empty
                , "Num ASC,OperateTime DESC"))
            {
                while (dr.Read())
                {
                    // 成员构成初期化
                    var strCountValue = string.Empty;

                    if (!dr.IsDBNull(dr.GetOrdinal("CountValue")))
                    {
                        // 实例化xml
                        var xml = new XmlDocument();

                        // 读取xml文件
                        xml.LoadXml(dr.GetString(dr.GetOrdinal("CountValue")));

                        foreach (XmlNode nodeP in xml.ChildNodes[0].ChildNodes)
                        {
                            var strTmp = string.Empty;
                            foreach (XmlNode nodeT in nodeP.ChildNodes)
                            {
                                switch (nodeT.Name)
                                {
                                    case "ProvinceName":
                                        strTmp = nodeT.InnerText + "（<b style='color: red;'>{0}</b>人）,";
                                        break;
                                    case "CountNum":
                                        strTmp = string.Format(strTmp, nodeT.InnerText);
                                        break;
                                }
                            }

                            strCountValue = strCountValue + strTmp;
                        }
                    }

                    // 公告信息实体
                    var model = new EyouSoft.Model.MQStructure.IMSuperCluster
                    {
                        // 同业中心ID
                        Id = dr.GetInt32(dr.GetOrdinal("ID")),

                        // 名称
                        Title = dr.IsDBNull(dr.GetOrdinal("Title")) ? string.Empty : dr.GetString(dr.GetOrdinal("Title")),

                        // 序号
                        Num = dr.IsDBNull(dr.GetOrdinal("Num")) ? 0 : dr.GetInt32(dr.GetOrdinal("Num")),

                        // 成员构成
                        CountValue = strCountValue.TrimEnd(','),

                        // 总管理员
                        Master = dr.IsDBNull(dr.GetOrdinal("Master")) ? 0 : dr.GetInt32(dr.GetOrdinal("Master")),

                        // 密码
                        PassWord = dr.IsDBNull(dr.GetOrdinal("PassWord")) ? string.Empty : dr.GetString(dr.GetOrdinal("PassWord")),

                        // 发布人
                        Opertor = dr.IsDBNull(dr.GetOrdinal("Opertor")) ? string.Empty : dr.GetString(dr.GetOrdinal("Opertor")),

                        // 发布时间
                        OperateTime = dr.IsDBNull(dr.GetOrdinal("OperateTime")) ? DateTime.Now : dr.GetDateTime(dr.GetOrdinal("OperateTime"))
                    };

                    // 追加到同业中心实体列表
                    lst.Add(model);
                }
            }

            // 返回同业中心实体列表
            return lst;
        }

        /// <summary>
        /// 获取所有的同业中心
        /// </summary>
        /// <returns>同业中心列表</returns>
        public IList<EyouSoft.Model.MQStructure.IMSuperCluster> GetAllClusters()
        {
            var lst = new List<EyouSoft.Model.MQStructure.IMSuperCluster>();                            // 同业中心实体
            var strSql = new StringBuilder();                                                           // SQL编辑器

            strSql.Append(" SELECT");
            strSql.Append("     id");
            strSql.Append("     ,title");
            strSql.Append(" FROM");
            strSql.Append("     tbl_SuperCluster");

            DbCommand cmd = this._database.GetSqlStringCommand(strSql.ToString());                      // SQL执行

            using (IDataReader dr = DbHelper.ExecuteReader(cmd, this._database))
            {
                while (dr.Read())
                {
                    // 同业中心实体
                    var model = new EyouSoft.Model.MQStructure.IMSuperCluster()
                    {
                        // 超级群ID
                        Id = dr.GetInt32(dr.GetOrdinal("id")),

                        // 超级群名称
                        Title = dr.IsDBNull(dr.GetOrdinal("title")) ? string.Empty : dr.GetString(dr.GetOrdinal("title"))
                    };

                    // 追加到实体列表
                    lst.Add(model);
                }
            }

            // 返回同业中心实体列表
            return lst;
        }

        #endregion        
    }
}
