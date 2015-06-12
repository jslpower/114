using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.OpenStructure
{
    /// <summary>
    /// 各平台用户对应关系业务逻辑
    /// </summary>
    /// 周文超 2011-04-02
    public class BUser : IBLL.OpenStructure.IBUser
    {
        /// <summary>
        /// 各平台用户对应关系数据访问接口
        /// </summary>
        private readonly IDAL.OpenStructure.IDUser dal = Component.Factory.ComponentFactory.CreateDAL<IDAL.OpenStructure.IDUser>();

        /// <summary>
        /// 构造订单信息业务逻辑接口
        /// </summary>
        /// <returns>订单信息业务逻辑接口</returns>
        public static IBLL.OpenStructure.IBUser CreateInstance()
        {
            IBLL.OpenStructure.IBUser op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<IBLL.OpenStructure.IBUser>();
            }
            return op;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public BUser()
        {

        }

        #region IMUser 成员

        /// <summary>
        /// 添加各平台用户对应关系
        /// </summary>
        /// <param name="model">各平台用户对应关系实体</param>
        /// <returns>返回1成功，其他失败</returns>
        public int AddMUser(Model.OpenStructure.MUserInfo model)
        {
            if (model == null)
                return 0;

            return dal.AddMUser(model);
        }

        /// <summary>
        /// 根据条件获取各平台用户对应关系
        /// </summary>
        /// <param name="SystemUserId">系统用户编号，其它系统（非114平台）时赋值</param>
        /// <param name="SystemType">系统类型</param>
        /// <param name="PlatformUserId">平台用户编号，大平台（114平台）时赋值</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.OpenStructure.MUserInfo> GetMUserList(int SystemUserId, int SystemType, string PlatformUserId)
        {
            if (SystemType <= 0)
            {
                return null;
            }

            EyouSoft.OpenRelation.Model.SystemType sysType = (EyouSoft.OpenRelation.Model.SystemType)SystemType;

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.Platform && string.IsNullOrEmpty(PlatformUserId))
            {
                return null;
            }

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.TYT && SystemUserId < 1)
            {
                return null;
            }

            if (sysType == EyouSoft.OpenRelation.Model.SystemType.YYT && SystemUserId < 1)
            {
                return null;
            }

            //根据大平台来查询关系时系统类型将不做为查询条件
            if (sysType == EyouSoft.OpenRelation.Model.SystemType.Platform) SystemType = 0; 

            return dal.GetMUserList(SystemUserId, SystemType, PlatformUserId);
        }

        /// <summary>
        /// 根据条件获取平台用户的系统权限
        /// </summary>
        /// <param name="SystemType">系统类型</param>
        /// <param name="SystemUserId">平台用户编号，大平台（114平台）时赋值</param>
        /// <returns></returns>
        public int[] GetUserPermission(int SystemType, int SystemUserId)
        {
            int[] PermissionList = null;
            string returnVal = string.Empty;
            if (SystemUserId > 0)
            {
                returnVal = EyouSoft.BLL.SyncStructure.SyncUser.SyncGetUserPermission((EyouSoft.OpenRelation.Model.SystemType)SystemType
                    , SystemUserId);
            }
            if (!string.IsNullOrEmpty(returnVal))
            {
                string[] pArray = returnVal.Split(',');
                PermissionList = new int[pArray.Length];
                for (int i = 0; i < pArray.Length; i++ )
                {
                    PermissionList[i] = Convert.ToInt32(pArray[i]);
                }
            }
            return PermissionList;
        }
        #endregion
    }
}
