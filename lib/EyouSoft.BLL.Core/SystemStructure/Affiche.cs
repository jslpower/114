using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Component.Factory;
using EyouSoft.IBLL.SystemStructure;

namespace EyouSoft.BLL.SystemStructure
{
    /// <summary>
    /// 创建人：鲁功源 2010-05-31
    /// 描述：公告信息业务逻辑层
    /// </summary>
    public class Affiche:IAffiche
    {
        private readonly IDAL.SystemStructure.IAffiche dal = ComponentFactory.CreateDAL<IDAL.SystemStructure.IAffiche>();

        #region CreateInstance
        /// <summary>
        /// 创建公告信息业务逻辑接口的实例
        /// </summary>
        /// <returns></returns>
        public static EyouSoft.IBLL.SystemStructure.IAffiche CreateInstance()
        {
            EyouSoft.IBLL.SystemStructure.IAffiche op1 = null;
            if (op1 == null)
            {
                op1 = ComponentFactory.Create<EyouSoft.IBLL.SystemStructure.IAffiche>();
            }
            return op1;
        }
        #endregion

        #region 成员方法
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">公告实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Add(EyouSoft.Model.SystemStructure.Affiche model)
        {
            if (model == null)
                return false;
            return dal.Add(model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">公告实体</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Update(EyouSoft.Model.SystemStructure.Affiche model)
        {
            if (model == null)
                return false;
            return dal.Update(model);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns>false:失败 true:成功</returns>
        public bool Delete(int id)
        {
            if (id==0)
                return false;
            return dal.Delete(id);
        }
        /// <summary>
        /// 获取指定行数的新闻信息
        /// </summary>
        /// <param name="topNumber">需要返回的行数 =0返回全部</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.Affiche> GetTopList(int topNumber, EyouSoft.Model.SystemStructure.AfficheType? affichType)
        {
            return dal.GetTopList(topNumber, affichType);
        }
        /// <summary>
        /// 获取新闻实体
        /// </summary>
        /// <param name="ID">主键编号</param>
        /// <returns></returns>
        public EyouSoft.Model.SystemStructure.Affiche GetModel(int ID)
        {
            if (ID <= 0)
                return null;
            return dal.GetModel(ID);
        }
        /// <summary>
        /// 分页获取新闻列表
        /// </summary>
        /// <param name="pageSize">每页显示条数</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="affichType">新闻类别 =null返回全部</param>
        /// <returns></returns>
        public IList<EyouSoft.Model.SystemStructure.Affiche> GetList(int pageSize, int pageIndex, ref int recordCount, EyouSoft.Model.SystemStructure.AfficheType? affichType)
        {
            return dal.GetList(pageSize, pageIndex, ref recordCount, affichType);
        }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <returns>false:失败 true:成功</returns>
        public bool SetTop(int id, bool istop)
        {
            return dal.SetTop(id, istop);
        }
        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        public bool UpdateReadCount(int id)
        {
            return dal.UpdateReadCount(id);
        }
        #endregion

    }
}
