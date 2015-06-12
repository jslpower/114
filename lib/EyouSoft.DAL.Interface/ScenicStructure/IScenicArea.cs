using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;

namespace EyouSoft.IDAL.ScenicStructure
{
    /// <summary>
    /// 景区接口
    /// 创建者：郑付杰
    /// 创建时间：2011/10/27
    /// </summary>
    public interface IScenicArea
    {
        /// <summary>
        /// 添加景区
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        bool Add(MScenicArea item);

        /// <summary>
        /// 修改景区
        /// </summary>
        /// <param name="item">景区实体</param>
        /// <returns></returns>
        bool Update(MScenicArea item);
        /// <summary>
        /// 修改景区审核状态
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="operatorId">审核用户编号</param>
        /// <param name="es">状态</param>
        /// <returns></returns>
        bool UpdateExamineStatus(string scenicId, int operatorId, ExamineStatus es);

        /// <summary>
        /// 删除景区（有门票则不能删除）
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>1：删除成功 3：删除失败</returns>
        int Delete(string scenicId, string companyId);
        /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        bool UpdateClickNum(string scenicId);
        /// <summary>
        /// 增加点击量
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        bool UpdateClickNum(long Id);
        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="companyId">公司编号</param>
        /// <returns>景区实体</returns>
        MScenicArea GetModel(string scenicId, string companyId);
        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <param name="isTickets">TRUE:已审核的门票 false:所有门票</param>
        /// <returns>景区实体</returns>
        MScenicArea GetModel(string scenicId,bool isTickets);

        /// <summary>
        /// 获取景区实体
        /// </summary>
        /// <param name="Id">自增编号</param>
        /// <returns>景区实体</returns>
        MScenicArea GetModel(long Id);

        /// <summary>
        /// 获取公司所有景区
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MScenicArea> GetList(string companyId);

        /// <summary>
        /// 景区列表(高级网店)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <returns></returns>
        IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount, string companyId);

        /// <summary>
        /// 景区列表
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        IList<MScenicArea> GetList(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search);

        /// <summary>
        /// 指定条数获取景区列表
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        IList<MScenicArea> GetList(int topNum, string companyId, MSearchSceniceArea search);

        /// <summary>
        /// 景区列表(UserPublicCenter)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        IList<MScenicArea> GetPublicList(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search);

        /// <summary>
        /// 景区列表(包含门票信息,主题信息)
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="search">查询条件</param>
        /// <returns>景区集合</returns>
        IList<MScenicArea> GetListAndTickets(int pageSize, int pageIndex, ref int recordCount, MSearchSceniceArea search);
    }
}
