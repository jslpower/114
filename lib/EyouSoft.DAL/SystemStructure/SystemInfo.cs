using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using EyouSoft.Common.DAL;
using EyouSoft.Model;
using EyouSoft.IDAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace EyouSoft.DAL.SystemStructure
{
    /// <summary>
    /// 系统信息 数据访问
    /// </summary>
    /// 周文超 2010-05-14
    public class SystemInfo : DALBase, IDAL.SystemStructure.ISystemInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemInfo()
        { }

        #region SqlString

        private const string Sql_SystemInfo_Update = " UPDATE [tbl_SystemInfo] SET [SystemName] = @SystemName,[ContactName] = @ContactName,[ContactTel] = @ContactTel,[ContactMobile] = @ContactMobile,[QQ1] = @QQ1,[QQ2] = @QQ2,[QQ3] = @QQ3,[QQ4] = @QQ4,[QQ5] = @QQ5,[Msn] = @Msn,[MQ] = @MQ,[SystemRemark] = @SystemRemark,[AgencyLog] = @AgencyLog,[UnionLog] = @UnionLog,[AllRight] = @AllRight WHERE [ID] = @ID ";
        private const string Sql_SystemInfo_Select = " select top 1 [ID],[SystemName],[ContactName],[ContactTel],[ContactMobile],[QQ1],[QQ2],[QQ3],[QQ4],[QQ5],[Msn],[MQ],[SystemRemark],[AgencyLog],[UnionLog],[AllRight] from [tbl_SystemInfo] ";
        /// <summary>
        /// 删除前移动文件记录
        /// </summary>
        private const string SQL_DELETEDFILE_MOVE = "IF (SELECT COUNT(*) FROM [tbl_SystemInfo] WHERE [UnionLog]=@UnionLog AND [ID]=@ID)=0 INSERT INTO tbl_SysDeletedFileQue(FilePath) SELECT [UnionLog] FROM [tbl_SystemInfo] WHERE ID=@ID AND UnionLog<>'';";
        const string SQL_SELECT_GetSysSetting = "SELECT [Key],[Value] FROM [tbl_SysSetting]";

        const string SettingKey_OrderSmsCompanyType = "OrderSmsCompanyType";
        const string SettingKey_OrderSmsOrderType = "OrderSmsOrderType";
        const string SettingKey_OrderSmsTemplate = "OrderSmsTemplate";
        const string SettingKey_OrderSmsCompanyId = "OrderSmsCompanyId";
        const string SettingKey_OrderSmsUserId = "OrderSmsUserId";
        const string SettingKey_OrderSmsIsEnable = "OrderSmsIsEnable";
        const string SettingKey_OrderSmsChannelIndex = "OrderSmsChannelIndex";

        const string SQL_SET_SetSysSettings = "IF NOT EXISTS(SELECT 1 FROM [tbl_SysSetting] WHERE [Key]=@Key{0}) BEGIN INSERT INTO [tbl_SysSetting]([Key],[Value]) VALUES(@Key{0},@Value{0}) END ELSE BEGIN UPDATE [tbl_SysSetting] set [Value]=@Value{0} WHERE [Key]=@Key{0} END;";

        #endregion

        #region 函数成员

        /// <summary>
        /// 更新系统信息
        /// </summary>
        /// <param name="model">系统信息实体</param>
        /// <returns>返回受影响行数</returns>
        public virtual int UpdateSystemInfo(EyouSoft.Model.SystemStructure.SystemInfo model)
        {
            DbCommand dc = base.SystemStore.GetSqlStringCommand(SQL_DELETEDFILE_MOVE + Sql_SystemInfo_Update);

            #region 参数赋值

            base.SystemStore.AddInParameter(dc, "SystemName", DbType.String, model.SystemName);
            base.SystemStore.AddInParameter(dc, "ContactName", DbType.String, model.ContactName);
            base.SystemStore.AddInParameter(dc, "ContactTel", DbType.String, model.ContactTel);
            base.SystemStore.AddInParameter(dc, "ContactMobile", DbType.String, model.ContactMobile);
            base.SystemStore.AddInParameter(dc, "QQ1", DbType.String, model.QQ1);
            base.SystemStore.AddInParameter(dc, "QQ2", DbType.String, model.QQ2);
            base.SystemStore.AddInParameter(dc, "QQ3", DbType.String, model.QQ3);
            base.SystemStore.AddInParameter(dc, "QQ4", DbType.String, model.QQ4);
            base.SystemStore.AddInParameter(dc, "QQ5", DbType.String, model.QQ5);
            base.SystemStore.AddInParameter(dc, "Msn", DbType.String, model.Msn);
            base.SystemStore.AddInParameter(dc, "MQ", DbType.String, model.MQ);
            base.SystemStore.AddInParameter(dc, "SystemRemark", DbType.String, model.SystemRemark);
            base.SystemStore.AddInParameter(dc, "AgencyLog", DbType.String, model.AgencyLog);
            base.SystemStore.AddInParameter(dc, "UnionLog", DbType.String, model.UnionLog);
            base.SystemStore.AddInParameter(dc, "AllRight", DbType.String, model.AllRight);
            base.SystemStore.AddInParameter(dc, "ID", DbType.Int32, model.ID);

            #endregion

            return DbHelper.ExecuteSql(dc, base.SystemStore);
        }

        /// <summary>
        /// 获取系统信息(同时获取所有的区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SystemInfo GetSystemInfoModel()
        {
            Model.SystemStructure.SystemInfo model = new EyouSoft.Model.SystemStructure.SystemInfo();

            string strWhere = " SELECT [Id],[SaleArea],[SaleType],[ContactName],[ContactTel],[ContactMobile],[QQ],[MQ] FROM [tbl_SysAreaContact]; ";
            strWhere += Sql_SystemInfo_Select + " ; ";

            DbCommand dc = base.SystemStore.GetSqlStringCommand(strWhere);

            #region 实体赋值

            List<Model.SystemStructure.SysAreaContact> AllAreaContact = new List<Model.SystemStructure.SysAreaContact>();
            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                Model.SystemStructure.SysAreaContact AreaContactModel = null;
                while (dr.Read())
                {
                    AreaContactModel = new EyouSoft.Model.SystemStructure.SysAreaContact();
                    if (!dr.IsDBNull(0))
                        AreaContactModel.ID = dr.GetInt32(0);
                    AreaContactModel.SaleArea = dr[1].ToString();
                    if (!dr.IsDBNull(2))
                        AreaContactModel.SaleType = dr.GetByte(2);
                    AreaContactModel.ContactName = dr[3].ToString();
                    AreaContactModel.ContactTel = dr[4].ToString();
                    AreaContactModel.ContactMobile = dr[5].ToString();
                    AreaContactModel.QQ = dr[6].ToString();
                    AreaContactModel.MQ = dr[7].ToString();

                    AllAreaContact.Add(AreaContactModel);
                }

                AreaContactModel = null;
                dr.NextResult();

                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        model.ID = dr.GetInt32(0);
                    model.SystemName = dr[1].ToString();
                    model.ContactName = dr[2].ToString();
                    model.ContactTel = dr[3].ToString();
                    model.ContactMobile = dr[4].ToString();
                    model.QQ1 = dr[5].ToString();
                    model.QQ2 = dr[6].ToString();
                    model.QQ3 = dr[7].ToString();
                    model.QQ4 = dr[8].ToString();
                    model.QQ5 = dr[9].ToString();
                    model.Msn = dr[10].ToString();
                    model.MQ = dr[11].ToString();
                    model.SystemRemark = dr[12].ToString();
                    model.AgencyLog = dr[13].ToString();
                    model.UnionLog = dr[14].ToString();
                    model.AllRight = dr[15].ToString();

                    model.SysAreaContact = AllAreaContact;

                    break;
                }

                AllAreaContact.Clear();
                AllAreaContact = null;
            }

            #endregion

            return model;
        }

        /// <summary>
        /// 根据系统ID获取系统信息(不含区域联系人)
        /// </summary>
        /// <returns>返回系统信息实体</returns>
        public virtual EyouSoft.Model.SystemStructure.SystemInfo GetSystemModel()
        {
            Model.SystemStructure.SystemInfo model = new EyouSoft.Model.SystemStructure.SystemInfo();
            DbCommand dc = base.SystemStore.GetSqlStringCommand(Sql_SystemInfo_Select);

            #region 实体赋值

            using (IDataReader dr = DbHelper.ExecuteReader(dc, base.SystemStore))
            {
                while (dr.Read())
                {
                    if (!dr.IsDBNull(0))
                        model.ID = dr.GetInt32(0);
                    model.SystemName = dr[1].ToString();
                    model.ContactName = dr[2].ToString();
                    model.ContactTel = dr[3].ToString();
                    model.ContactMobile = dr[4].ToString();
                    model.QQ1 = dr[5].ToString();
                    model.QQ2 = dr[6].ToString();
                    model.QQ3 = dr[7].ToString();
                    model.QQ4 = dr[8].ToString();
                    model.QQ5 = dr[9].ToString();
                    model.Msn = dr[10].ToString();
                    model.MQ = dr[11].ToString();
                    model.SystemRemark = dr[12].ToString();
                    model.AgencyLog = dr[13].ToString();
                    model.UnionLog = dr[14].ToString();
                    model.AllRight = dr[15].ToString();

                    break;
                }
            }

            #endregion

            return model;
        }

        /// <summary>
        /// 设置平台配置，返回1成功，其它失败
        /// </summary>
        /// <param name="info">配置信息</param>
        /// <param name="removeKeys">不处理的键组</param>
        /// <returns></returns>
        public virtual int SetSysSettings(EyouSoft.Model.SystemStructure.MSysSettingInfo info,string[] removeKeys)
        {
            string orderSmsCompanyTypes = string.Empty;
            string orderSmsOrderTypes = string.Empty;

            if (info.OrderSmsCompanyTypes != null && info.OrderSmsCompanyTypes.Length > 0)
            {
                foreach (var item in info.OrderSmsCompanyTypes)
                {
                    orderSmsCompanyTypes += "," + (int)item;
                }

                orderSmsCompanyTypes = orderSmsCompanyTypes.Substring(1);
            }

            if (info.OrderSmsOrderTypes != null && info.OrderSmsOrderTypes.Length > 0)
            {
                foreach (var item in info.OrderSmsOrderTypes)
                {
                    orderSmsOrderTypes += "," + (int)item;
                }

                orderSmsOrderTypes = orderSmsOrderTypes.Substring(1);
            }

            DbCommand cmd = SystemStore.GetSqlStringCommand("PRINT 1");
            StringBuilder cmdText = new StringBuilder();

            if (!removeKeys.Contains(SettingKey_OrderSmsCompanyId))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsCompanyId);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsCompanyId, DbType.AnsiString, SettingKey_OrderSmsCompanyId);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsCompanyId, DbType.String, info.OrderSmsCompanyId);
            }

            if (!removeKeys.Contains(SettingKey_OrderSmsCompanyType))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsCompanyType);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsCompanyType, DbType.AnsiString, SettingKey_OrderSmsCompanyType);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsCompanyType, DbType.String, orderSmsCompanyTypes);
            }

            if (!removeKeys.Contains(SettingKey_OrderSmsIsEnable))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsIsEnable);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsIsEnable, DbType.AnsiString, SettingKey_OrderSmsIsEnable);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsIsEnable, DbType.String, info.OrderSmsIsEnable ? "1" : "0");
            }


            if (!removeKeys.Contains(SettingKey_OrderSmsOrderType))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsOrderType);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsOrderType, DbType.AnsiString, SettingKey_OrderSmsOrderType);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsOrderType, DbType.String, orderSmsOrderTypes);
            }

            if (!removeKeys.Contains(SettingKey_OrderSmsTemplate))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsTemplate);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsTemplate, DbType.AnsiString, SettingKey_OrderSmsTemplate);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsTemplate, DbType.String, info.OrderSmsTemplate);
            }

            if (!removeKeys.Contains(SettingKey_OrderSmsUserId))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsUserId);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsUserId, DbType.AnsiString, SettingKey_OrderSmsUserId);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsUserId, DbType.String, info.OrderSmsUserId);
            }

            if (!removeKeys.Contains(SettingKey_OrderSmsChannelIndex))
            {
                cmdText.AppendFormat(SQL_SET_SetSysSettings, SettingKey_OrderSmsChannelIndex);
                SystemStore.AddInParameter(cmd, "Key" + SettingKey_OrderSmsChannelIndex, DbType.AnsiString, SettingKey_OrderSmsChannelIndex);
                SystemStore.AddInParameter(cmd, "Value" + SettingKey_OrderSmsChannelIndex, DbType.String, info.OrderSmsChannelIndex);
            }

            if (string.IsNullOrEmpty(cmdText.ToString())) return 1;

            cmd.CommandText = cmdText.ToString();

            return DbHelper.ExecuteSql(cmd, SystemStore) > 0 ? 1 : 0;
        }
        /// <summary>
        /// 获取平台配置
        /// </summary>
        /// <returns></returns>
        public virtual EyouSoft.Model.SystemStructure.MSysSettingInfo GetSysSetting()
        {
            EyouSoft.Model.SystemStructure.MSysSettingInfo info = null;
            DbCommand cmd = SystemStore.GetSqlStringCommand(SQL_SELECT_GetSysSetting);
            using (IDataReader rdr = DbHelper.ExecuteReader(cmd, SystemStore))
            {
                while (rdr.Read())
                {
                    info = info ?? new EyouSoft.Model.SystemStructure.MSysSettingInfo();
                    string key = rdr.GetString(0);
                    string value = rdr.GetString(1);

                    switch (key)
                    {
                        case SettingKey_OrderSmsCompanyId: 
                            info.OrderSmsCompanyId = value; 
                            break;
                        case SettingKey_OrderSmsCompanyType:
                            string[] _items1 = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (_items1 != null && _items1.Length > 0)
                            {
                                int length = _items1.Length;
                                info.OrderSmsCompanyTypes = new EyouSoft.Model.CompanyStructure.CompanyLev[length];
                                for (int i = 0; i < length; i++)
                                {
                                    info.OrderSmsCompanyTypes[i] = (EyouSoft.Model.CompanyStructure.CompanyLev)int.Parse(_items1[i]);
                                }
                            }
                            break;
                        case SettingKey_OrderSmsIsEnable:
                            info.OrderSmsIsEnable = value == "1";
                            break;
                        case SettingKey_OrderSmsOrderType:
                            string[] _items2 = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            if (_items2 != null && _items2.Length > 0)
                            {
                                int length = _items2.Length;
                                info.OrderSmsOrderTypes = new Model.NewTourStructure.OrderSource[length];
                                for (int i = 0; i < length; i++)
                                {
                                    info.OrderSmsOrderTypes[i] = (EyouSoft.Model.NewTourStructure.OrderSource)int.Parse(_items2[i]);
                                }
                            }
                            break;
                        case SettingKey_OrderSmsTemplate:
                            info.OrderSmsTemplate = value;
                            break;
                        case SettingKey_OrderSmsUserId:
                            info.OrderSmsUserId = value;
                            break;
                        case SettingKey_OrderSmsChannelIndex:
                            info.OrderSmsChannelIndex = byte.Parse(value);
                            break;
                        default: break;
                    }
                }
            }

            return info;
        }
        #endregion
    }
}
