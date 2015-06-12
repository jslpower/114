using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;

namespace EyouSoft.BLL.CompanyStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-06-25
    /// 描述：公司资讯业务层
    /// </summary>
    public class CompanyAffiche : EyouSoft.IBLL.CompanyStructure.ICompanyAffiche
    {
        private readonly EyouSoft.IDAL.CompanyStructure.ICompanyAffiche idal = ComponentFactory.CreateDAL<EyouSoft.IDAL.CompanyStructure.ICompanyAffiche>();

        /// <summary>
        /// 创建IBLL实例对象
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.CompanyStructure.ICompanyAffiche CreateInstance()
        {
            EyouSoft.IBLL.CompanyStructure.ICompanyAffiche op = null;
            if (op == null)
            {
                op = ComponentFactory.Create<EyouSoft.IBLL.CompanyStructure.ICompanyAffiche>();
            }
            return op;
        }

        #region ICompanyAffiche成员
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.CompanyStructure.CompanyAffiche Model)
        {
            if (Model == null)
                return false;
            return idal.Add(Model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Model">公司资讯实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.CompanyStructure.CompanyAffiche Model)
        {
            if (Model == null)
                return false;
            return idal.Update(Model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(int ID)
        {
            if (ID == 0)
                return false;
            return idal.Delete(ID);
        }
        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsTop">是否置顶</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetIsTop(int ID, bool IsTop)
        {
            if (ID == 0)
                return false;
            return idal.SetIsTop(ID, IsTop);
        }
        /// <summary>
        /// 更新阅读次数
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetClicks(int ID)
        {
            if (ID == 0)
                return false;
            return idal.SetClicks(ID);
        }
        /// <summary>
        /// 设置热门
        /// </summary>
        /// <param name="ID">主键ID</param>
        /// <param name="IsHot">是否热门</param>
        /// <returns>false:失败 true:成功</returns>
        public bool SetIsHot(int ID, bool IsHot)
        {
            if (ID == 0)
                return false;
            return idal.SetIsHot(ID, IsHot);
        }
        /// <summary>
        /// 获取资讯实体类
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns>资讯实体</returns>
        public EyouSoft.Model.CompanyStructure.CompanyAffiche GetModel(int ID)
        {
            if (ID == 0)
                return null;
            return idal.GetModel(ID);
        }
        /// <summary>
        /// 分页获取指定公司指定资讯类别的列表
        /// </summary>
        /// <param name="pageSize">每页显示的条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="CompanyID">公司编号 =""返回所有公司，否则返回指定公司的数据</param>
        /// <param name="afficheType">资讯类别 =null返回所有类别，否则返回指定类别的数据</param>
        /// <param name="IsHot">是否热门 =null返回所有</param>
        /// <param name="IsTop">是否置顶 =null返回所有</param>
        /// <param name="IsPicNews">是否图片新闻 =null返回所有</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.CompanyStructure.CompanyAffiche> GetList(int pageSize, int pageIndex, ref int recordCount, string CompanyID, EyouSoft.Model.CompanyStructure.CompanyAfficheType? afficheType, bool? IsHot, bool? IsTop, bool? IsPicNews)
        {
            return idal.GetList(pageSize, pageIndex, ref recordCount, CompanyID, afficheType, IsHot, IsTop, IsPicNews);
        }
        #endregion
    }
}
