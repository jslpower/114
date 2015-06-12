using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IBLL.TicketStructure
{
    /// <summary>
    ///功能描述: 常旅客接口
    /// Author：刘咏梅
    /// CreateTime：2010-10-29
    /// </summary>
    public interface ITicketVisitor
    {
        /// <summary>
        /// 添加常旅客
        /// </summary>
        /// <param name="TicketVisitorInfo">常旅客实体</param>
        /// <returns></returns>
        bool AddTicketVisitorInfo(EyouSoft.Model.TicketStructure.TicketVistorInfo TicketVisitorInfo);

        /// <summary>
        /// 批量增加常旅客
        /// </summary>
        /// <param name="TicketVisitorList">常旅客集合</param>
        /// <returns></returns>
        int AddTicketVisitorList(IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> TicketVisitorList);

        /// <summary>
        /// 修改常旅客
        /// </summary>
        /// <param name="TicketVisitorInfo">常旅客实体</param>
        /// <returns></returns>
        bool UpdateTicketVisitorInfo(EyouSoft.Model.TicketStructure.TicketVistorInfo TicketVisitorInfo);

        /// <summary>
        /// 删除常旅客
        /// </summary>
        /// <param name="VisitorIdList">常旅客ID</param>
        /// <returns></returns>
        bool DeleteTicketVisitorInfo(params string[] VisitorIdList);

        /// <summary>
        /// 机票-查询常旅客列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorName">常旅客姓名</param>
        /// <param name="VisitorType">常旅客类型</param>
        /// <param name="PageSize">每页显示条数</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="RecordCount">总记录条数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetTicketVisitorList(string CompanyId, string VisitorName, EyouSoft.Model.TicketStructure.TicketVistorType? VisitorType, int PageSize, int PageIndex, ref int RecordCount);
        /// <summary>
        /// 查询酒店常旅客，得到常旅客列表
        /// </summary>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorName">常旅客姓名</param>
        /// <param name="VisitorType">常旅客类型</param>
        /// <param name="PageSize">每页显示的记录</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="RecordCount">总记录数</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetHotelVistorList(string CompanyId, string VisitorName, EyouSoft.Model.TicketStructure.TicketVistorType? VisitorType, int PageSize, int PageIndex, ref int RecordCount);
        /// <summary>
        /// 机票-判断常旅客是否已经存在
        /// </summary>
        /// <param name="CardNo">证件号码</param>
        /// <param name="CompanyId">公司ID</param>
        /// <param name="VisitorId">常旅客ID，若为新增则传空字符串</param>
        /// <returns></returns>
        bool VisitorIsExists(string CardNo, string CompanyId, string VisitorId);
        /// <summary>
        /// 判断酒店常旅客是否存在
        /// </summary>
        /// <param name="ChinaName">中文名字</param>
        /// <param name="EnglishName">英文名字</param>
        /// <param name="ContactTel">联系电话</param>
        /// <param name="CompanyId">公司编号</param>
        /// <param name="VisitorId">常旅客ID，若为新增则传空字符串</param>
        /// <returns></returns>
        bool HotelVistorIsExist(string ChinaName, string EnglishName, string ContactTel, string CompanyId, string VisitorId);
        /// <summary>
        /// 机票-匹配常旅客姓名，根据姓名模糊查询常旅客集合
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetListByKeyWord(string VistorName, string CompanyId);
        /// <summary>
        /// 酒店-匹配常旅客姓名，根据姓名模糊查询常旅客集合
        /// </summary>
        /// <param name="VistorName">常旅客姓名</param>
        /// <param name="CompanyId">公司ID</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetHotelListByName(string VistorName, string CompanyId);
        /// <summary>
        /// 获得常旅客实体
        /// </summary>
        /// <param name="VisitorId">常旅客ID</param>
        /// <returns></returns>
        EyouSoft.Model.TicketStructure.TicketVistorInfo GetTicketVisitorInfo(string VisitorId);
        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(int pageSize, int pageIndex, ref int recordCount, string companyId, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo);
        /// <summary>
        /// 获取常旅客信息集合
        /// </summary>
        /// <param name="companyId">公司编号</param>
        /// <param name="searchInfo">查询信息</param>
        /// <returns></returns>
        IList<EyouSoft.Model.TicketStructure.TicketVistorInfo> GetVisitors(string companyId, EyouSoft.Model.TicketStructure.MVisitorSearchInfo searchInfo);
    }
}
