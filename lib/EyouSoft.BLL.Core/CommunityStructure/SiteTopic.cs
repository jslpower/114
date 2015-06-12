using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Cache.Facade;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.CommunityStructure;

namespace EyouSoft.BLL.CommunityStructure
{
    /// <summary>
    /// 创建人：鲁功源  2010-07-13
    /// 描述：供求栏目固定文字业务层
    /// </summary>
    public class SiteTopic:IBLL.CommunityStructure.ISiteTopic
    {
        private readonly IDAL.CommunityStructure.ISiteTopic dal = ComponentFactory.CreateDAL<IDAL.CommunityStructure.ISiteTopic>();

        #region CreateInstance
        /// <summary>
        /// 创建IBLL实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CommunityStructure.ISiteTopic CreateInstance()
        {
            EyouSoft.IBLL.CommunityStructure.ISiteTopic op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.CommunityStructure.ISiteTopic>();
            }
            return op1;
        }
        #endregion

        /// <summary>
        /// 设置访谈内容
        /// </summary>
        /// <param name="Content">访谈内容</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetInterview(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return false;
            return dal.SetSiteTopic("Interview", Content);
        }
        /// <summary>
        /// 设置同业交流专区
        /// </summary>
        /// <param name="Content">同业交流</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetCommArea(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return false;
            return dal.SetSiteTopic("CommArea", Content);
        }
        /// <summary>
        /// 设置学堂介绍
        /// </summary>
        /// <param name="Content">学堂介绍</param>
        /// <returns>true:成功 false:失败</returns>
        public bool SetSchool(string Content)
        {
            if (string.IsNullOrEmpty(Content))
                return false;
            return dal.SetSiteTopic("School", Content);
        }
        /// <summary>
        /// 获取访谈内容
        /// </summary>
        /// <returns>不存在返回“”</returns>
        public string GetInterview()
        {
            return dal.GetSiteTopic("Interview");
        }
        /// <summary>
        /// 获取同业交流专区内容
        /// </summary>
        /// <returns>不存在返回“”</returns>
        public string GetCommArea()
        {
            return dal.GetSiteTopic("CommArea");
        }
        /// <summary>
        /// 获取同业学堂介绍
        /// </summary>
        /// <returns>不存在返回“”</returns>
        public string GetSchool()
        {
            return dal.GetSiteTopic("School");
        }
    }
}
