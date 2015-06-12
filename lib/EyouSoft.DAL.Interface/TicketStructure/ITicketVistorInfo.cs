using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.TicketStructure
{
    /// <summary>
    /// 常旅客信息数据接口
    /// </summary>
    /// 创建人：luofx 2010-10-25
    public interface ITicketVistorInfo
    {
        /// <summary>
        /// 获取常旅客实体
        /// </summary>
        /// <param name="TicketVistorId">常旅客Id</param>
        /// <returns>返回获取常旅客实体</returns>
        EyouSoft.Model.TicketStructure.TicketVistorInfo GetModel(string TicketVistorId);
        /*/// <summary>
        /// 获取常旅客信息
        /// </summary>
        /// <param name="PageSize">每页条数</param>
        /// <param name="PageIndex">当前页数</param>
        /// <param name="RecordCount">总记录数</param>
        /// <param name="CompanyId">公司id</param>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="VistorType">旅客类型（0：成人，1：儿童，2：婴儿），为null时不做条件</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVistorList(int PageSize, int PageIndex, ref int RecordCount, string CompanyId, string VistorName, EyouSoft.Model.TicketStructure.TicketVistorType? VistorType, EyouSoft.Model.TicketStructure.TicketDataType? DataType);*/
        /*/// <summary>
        /// 根据输入的旅客姓名模糊查找常旅客信息
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司Id</param>
        /// <returns>返回获取常旅客信息集合</returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVistorListByName(string VistorName, string CompanyId, EyouSoft.Model.TicketStructure.TicketDataType? DataType);*/
        /// <summary>
        /// 判断常旅客是否存在
        /// </summary>
        /// <param name="CardNo">常旅客证件号</param>
        /// <param name="CompanyId">公司Id</param>
        /// <param name="VistorId">常旅客信息主键Id,若为新增,则传空</param>
        /// <returns>true：成功，false：失败</returns>
        bool IsExist(string CardNo, string CompanyId, string VistorId);
        /// <summary>
        /// 判断酒店常旅客是否存在
        /// </summary>
        /// <param name="ChinaName">中文名字</param>
        /// <param name="EnglishName">英文名字</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="VistorId">常旅客信息主键Id,若为新增,则传空</param>
        /// <returns></returns>
        bool IsExist(string ChinaName, string EnglishName, string ContactTel, string CompanyId, string VistorId);
        /*/// <summary>
        /// 添加常旅客
        /// </summary>
        /// <param name="model">常旅客信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        int Add(EyouSoft.Model.TicketStructure.TicketVistorInfo model);*/
        /// <summary>
        /// 批量添加常旅客
        /// </summary>
        /// <param name="VistorList">常旅客信息集合</param>
        /// <returns>大于等于1：成功，0：失败</returns>
        int AddVistorList(IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> VistorList);
        /// <summary>
        /// 修改常旅客
        /// </summary>
        /// <param name="model">常旅客信息实体</param>
        /// <returns>1：成功，0：失败</returns>
        int Update(EyouSoft.Model.TicketStructure.TicketVistorInfo model);
        /// <summary>
        /// 删除常旅客信息
        /// </summary>
        /// <param name="TicketVistorIds">常旅客主键Id</param>
        /// <returns>true：成功，false：失败</returns>
        bool Delete(params string[] TicketVistorIds);
        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="isPaging">是否分页</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(int pageSize, int pageIndex, ref int recordCount, string companyId, bool isPaging, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo);
    }
}
