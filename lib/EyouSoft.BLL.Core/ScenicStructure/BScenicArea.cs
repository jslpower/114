using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.BLL.ScenicStructure
{
    /// <summary>
    /// 景区
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class BScenicArea:EyouSoft.IBLL.ScenicStructure.IScenicArea
    {
        private readonly EyouSoft.IDAL.ScenicStructure.IScenicArea dal =
            EyouSoft.Component.Factory.ComponentFactory.CreateDAL<EyouSoft.IDAL.ScenicStructure.IScenicArea>();

        public static IBLL.ScenicStructure.IScenicArea CreateInstance()
        {
            IBLL.ScenicStructure.IScenicArea op = null;
            if (op == null)
            {
                op = EyouSoft.Component.Factory.ComponentFactory.Create<IBLL.ScenicStructure.IScenicArea>();
            }
            return op;
        }

        #region IScenicArea 成员
        /// <summary>
        /// 添加景区(用户后台)
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        public bool Add(MScenicArea item)
        {
            bool result = false;
            if (item != null && !string.IsNullOrEmpty(item.ContactOperator)
                && item.Company != null && !string.IsNullOrEmpty(item.Company.ID))
            {
                item.ScenicId = Guid.NewGuid().ToString();
                //B2B,B2C排序值默认50
                item.B2BOrder = 50; 
                item.B2COrder = 50;
                //用户后台默认状态为待审核
                item.Status = ExamineStatus.待审核;
                result = dal.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 添加景区(运营后台)
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        public bool OperateAdd(MScenicArea item)
        {
            bool result = false;
            if (item != null && !string.IsNullOrEmpty(item.ContactOperator)
                && item.Company != null && !string.IsNullOrEmpty(item.Company.ID))
            {
                item.ScenicId = Guid.NewGuid().ToString();
                result = dal.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 修改景区
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        public bool Update(MScenicArea item)
        {
            bool result = false;
            if (item != null && !string.IsNullOrEmpty(item.ContactOperator))
            {
                result = dal.Update(item);
            }
            return result;
        }
        /// <summary>
        /// 修改景区审核状态
        /// </summary>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">状态</param>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public bool UpdateExamineStatus(int operatorId, ExamineStatus es,params string[] scenicId)
        {
            bool result = false;
            if (operatorId > 0 && scenicId != null && scenicId.Length > 0)
            {
                StringBuilder scenic = new StringBuilder();
                for (int i = 0; i < scenicId.Length; i++)
                {
                    scenic.AppendFormat("{0},", scenicId[i]);
                }
                result = dal.UpdateExamineStatus(scenic.ToString().Substring(0, scenic.ToString().Length - 1), operatorId, es);
            }
            return result;
        }
        /// <summary>
        /// 删除景区（有门票则不能删除）
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1：删除成功  3：删除失败</returns>
        public int Delete(string scenicId, string companyId)
        {
            int result = 3;
            if (!string.IsNullOrEmpty(scenicId) && !string.IsNullOrEmpty(companyId))
            {
                result = dal.Delete(scenicId, companyId);
            }
            return result;
        }
        /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public bool UpdateClickNum(string scenicId)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(scenicId))
            {
                result = dal.UpdateClickNum(scenicId);
            }
            return result;
        }
         /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        public bool UpdateClickNum(long Id)
        {
            bool result = false;

            if (Id > 0)
            {
                result = dal.UpdateClickNum(Id);
            }
            return result;
        }
        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景区实体</returns>
        public MScenicArea GetModel(string scenicId, string companyId)
        {
            MScenicArea item = null;
            if (!string.IsNullOrEmpty(scenicId) && !string.IsNullOrEmpty(companyId))
            {
                item = dal.GetModel(scenicId, companyId);
            }
            return item;
        }

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns>景区实体</returns>
        public MScenicArea GetModel(string scenicId)
        {
            MScenicArea item = null;
            if (!string.IsNullOrEmpty(scenicId))
            {
                item = dal.GetModel(scenicId, true);
            }
            return item;
        }

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="isTickets">TRUE:已审核的门票 false:所有门票</param>
        /// <returns>景区实体</returns>
        public MScenicArea GetModel(string scenicId, bool isTickets)
        {
            MScenicArea item = null;
            if (!string.IsNullOrEmpty(scenicId))
            {
                item = dal.GetModel(scenicId, isTickets);
            }
            return item;
        }

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="Id">自增编号</param>
        /// <returns>景区实体</returns>
        public MScenicArea GetModel(long Id)
        {
            MScenicArea item = null;
            if (Id > 0)
            {
                item = dal.GetModel(Id);
            }
            return item;
        }

        /// <summary>
        /// 获取公司所有景区
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MScenicArea> GetList(string companyId)
        {
            IList<MScenicArea> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                list = dal.GetList(companyId);
            }
            return list;
        }
        /// <summary>
        /// 指定条数获取景区列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public IList<MScenicArea> GetList(int topNum, string companyId, MSearchSceniceArea search)
        {
            IList<MScenicArea> list = null;

            topNum = topNum < 1 ? 10 : topNum;
            if (search == null) search = new MSearchSceniceArea() { IsQH = false };
            else search.IsQH = false;
            list = dal.GetList(topNum, companyId, search);
            return list;
        }
        /// <summary>
        /// 景区列表(网店)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        public IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount, string companyId)
        {
            IList<MScenicArea> list = null;
            if (!string.IsNullOrEmpty(companyId))
            {
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                list = dal.GetList(pageSize, pageIndex, ref recordCount, companyId);
            }
            return list;
        }
        /// <summary>
        /// 景区列表(SiteOperationsCenter)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        public IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            return dal.GetList(pageSize, pageIndex, ref recordCount, search);
        }

        /// <summary>
        /// 景区列表(UserPublicCenter)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件(可以为null)</param>
        /// <returns></returns>
        public IList<MScenicArea> GetPublicList(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            if (search == null) { search = new MSearchSceniceArea() { IsQH = false,Status = ExamineStatus.已审核 }; }
            else { search.IsQH = false; search.Status = ExamineStatus.已审核; }
            return dal.GetPublicList(pageSize, pageIndex, ref recordCount, search);
        }
        /// <summary>
        /// 景区列表(包含门票,主题信息)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        public IList<MScenicArea> GetListAndTickets(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            return dal.GetListAndTickets(pageSize, pageIndex, ref recordCount, search);
        }

        #endregion
    }
}
