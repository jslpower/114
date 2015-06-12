using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Model.ScenicStructure;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.ScenicStructure
{ 
    /// <summary>
    /// 景区图片
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class DScenicImg : DALBase, EyouSoft.IDAL.ScenicStructure.IScenicImg
    {
        private readonly Database _db = null;

        public DScenicImg()
        {
            this._db = base.SystemStore;
        }

        #region IScenicImg 成员
        /// <summary>
        /// 添加景区图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Add(MScenicImg item)
        {
            string sql = "INSERT INTO tbl_ScenicImg(ImgId,ScenicId,ImgType,Address,ThumbAddress,Description,CompanyId) VALUES(@id,@sid,@type,@address,@ThumbAddress,@des,@com)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.ImgId);
            this._db.AddInParameter(comm, "@sid", DbType.AnsiStringFixedLength, item.ScenicId);
            this._db.AddInParameter(comm, "@type", DbType.Byte, (int)item.ImgType);
            this._db.AddInParameter(comm, "@address", DbType.String, item.Address);
            this._db.AddInParameter(comm, "@ThumbAddress", DbType.String, item.ThumbAddress);
            this._db.AddInParameter(comm, "@des", DbType.String, item.Description);
            this._db.AddInParameter(comm, "@com", DbType.AnsiStringFixedLength, item.CompanyId);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改图片
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Update(MScenicImg item)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("declare @imgAddress nvarchar(500)");
            sql.Append(" select @imgAddress = Address from tbl_ScenicImg WHERE ImgId = @id AND ImgType = @type");
            sql.AppendFormat(" if @imgAddress <> '{0}'", item.Address);
            sql.Append(" begin");
            sql.AppendFormat(" INSERT INTO tbl_SysDeletedFileQue(FilePath,FileState) VALUES(@imgAddress,0)");
            sql.Append(" UPDATE tbl_ScenicImg SET Address = @address,ThumbAddress=@ThumbAddress,Description = @des WHERE ImgId = @id AND ImgType = @type");
            sql.Append(" end");
            sql.Append(" else begin");
            sql.Append(" UPDATE tbl_ScenicImg SET Description = @des WHERE ImgId = @id AND ImgType = @type");
            sql.Append(" end");
            
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            this._db.AddInParameter(comm, "@address", DbType.String, item.Address);
            this._db.AddInParameter(comm, "@ThumbAddress", DbType.String, item.ThumbAddress);
            this._db.AddInParameter(comm, "@des", DbType.String, item.Description);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, item.ImgId);
            this._db.AddInParameter(comm, "@type", DbType.Byte, (int)item.ImgType);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="ImgId">图片编号</param>
        /// <returns></returns>
        public virtual bool Delete(string ImgId)
        {
            string sql = "INSERT INTO tbl_SysDeletedFileQue(FilePath,FileState) SELECT Address,0 FROM tbl_ScenicImg WHERE ImgId = @id;DELETE FROM tbl_ScenicImg WHERE ImgId = @id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.AnsiStringFixedLength, ImgId);
            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="scenicId">景区编号</param>
        /// <returns></returns>
        public virtual IList<MScenicImg> GetList(string scenicId)
        {
            string sql = "SELECT a.ImgId,a.ImgType,a.Address,a.ThumbAddress,a.Description,a.CompanyId,b.ScenicName,b.Id FROM tbl_ScenicImg a inner join tbl_ScenicArea b on a.ScenicId = b.ScenicId WHERE a.ScenicId = @ScenicId order by ImgType";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ScenicId", DbType.AnsiStringFixedLength, scenicId);

            IList<MScenicImg> list = new List<MScenicImg>();
            MScenicImg item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MScenicImg()
                        {
                            Id = long.Parse(reader["Id"].ToString()),
                            ImgId = reader["ImgId"].ToString(),
                            ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), reader["ImgType"].ToString()),
                            Address = reader["Address"].ToString(),
                            ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                            ScenicId = scenicId,
                            CompanyId = reader["CompanyId"].ToString(),
                            ScenicName = reader["ScenicName"].ToString()
                        });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="Id">景区自增编号</param>
        /// <returns></returns>
        public virtual IList<MScenicImg> GetList(long Id)
        {
            string sql = "SELECT a.ImgId,a.ImgType,a.Address,a.ThumbAddress,a.Description,a.CompanyId,b.ScenicName,b.Id,b.ScenicId FROM tbl_ScenicImg a inner join tbl_ScenicArea b on a.ScenicId = b.ScenicId WHERE b.Id = @Id order by ImgType";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@Id", DbType.Int64, Id);

            IList<MScenicImg> list = new List<MScenicImg>();
            MScenicImg item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(
                        item = new MScenicImg()
                        {
                            Id = long.Parse(reader["Id"].ToString()),
                            ImgId = reader["ImgId"].ToString(),
                            ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), reader["ImgType"].ToString()),
                            Address = reader["Address"].ToString(),
                            ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                            Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                            ScenicId = reader["ScenicId"].ToString(),
                            CompanyId = reader["CompanyId"].ToString(),
                            ScenicName = reader["ScenicName"].ToString()
                        });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取景区形象图片
        /// </summary>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public virtual IList<MScenicImg> GetList(int topNum)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select top {0} A.Id, A.ScenicId,A.ScenicName,B.Address,B.ThumbAddress,A.CompanyId from tbl_ScenicArea A", topNum);
            sql.AppendFormat(" left join tbl_ScenicImg B on A.ScenicId = B.ScenicId and B.ImgType = {0}", (int)ScenicImgType.景区形象);
            sql.AppendFormat(" where A.Status = {0} AND A.B2B = {1}",(int)ExamineStatus.已审核,(int)ScenicB2BDisplay.列表置顶);
            sql.Append(" order by A.LastUpdateTime DESC");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());

            IList<MScenicImg> list = new List<MScenicImg>();
            MScenicImg item = null;

            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicImg()
                    {
                        Id = long.Parse(reader["Id"].ToString()),
                        ScenicId = reader["ScenicId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString(),
                        ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? string.Empty : reader["Address"].ToString(),
                        CompanyId = reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? string.Empty : reader["CompanyId"].ToString()
                    });
                }
            }
            return list;
        }


        /// <summary>
        /// 指定条数获取景区图片
        /// </summary>
        /// <param name="topNum">获取条数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MScenicImg> GetList(int topNum, string companyId, MScenicImgSearch search)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            if (topNum > 0)
            {
                sql.AppendFormat(" top({0})", topNum);
            }
            sql.Append(" ImgId,ScenicId,CompanyId,ImgType,Address,ThumbAddress,Description,ScenicName");
            sql.Append(" from view_ScenicImg_Select ");
            sql.AppendFormat(" where CompanyId = '{0}'", companyId);
            if (search != null)
            {
                //图片类型
                if (search.ImgType != null && search.ImgType.Count() > 0)
                {
                    sql.AppendFormat(" and ImgType in {0}", ConvertScenicImgType(search.ImgType));
                }
            }
            sql.Append(" Order By ImgType ASC");
            DbCommand comm = this._db.GetSqlStringCommand(sql.ToString());
            IList<MScenicImg> list = new List<MScenicImg>();
            MScenicImg item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm, this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicImg()
                    {
                        ImgId = reader["ImgId"].ToString(),
                        ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), reader["ImgType"].ToString()),
                        Address = reader["Address"].ToString(),
                        ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        ScenicId = reader["ScenicId"].ToString(),
                        CompanyId = reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? string.Empty : reader["CompanyId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString()
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 获取景区图片
        /// </summary>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="recordCount">总记录数</param>
        /// <param name="companyId">公司编号</param>
        /// <param name="search">搜索实体</param>
        /// <returns></returns>
        public virtual IList<MScenicImg> GetList(int pageSize, int pageIndex, ref int recordCount,
            string companyId, MScenicImgSearch search)
        {
            string tableName = "view_ScenicImg_Select";
            string primaryKey = "ImgId";
            string orderBy = "ImgType ASC";
            string fileds = "ImgId,ScenicId,CompanyId,ImgType,Address,Description,ThumbAddress,ScenicName";
            StringBuilder query = new StringBuilder();
            query.AppendFormat(" CompanyId = '{0}'", companyId);
            if (search != null)
            {
                //图片类型
                if (search.ImgType != null && search.ImgType.Count() > 0)
                {
                    query.AppendFormat(" and ImgType in {0}", ConvertScenicImgType(search.ImgType));
                }
            }
            IList<MScenicImg> list = new List<MScenicImg>();
            MScenicImg item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(this._db, pageSize, pageIndex, ref recordCount,
                tableName, primaryKey, fileds, query.ToString(), orderBy))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicImg()
                    {
                        ImgId = reader["ImgId"].ToString(),
                        ImgType = (ScenicImgType)Enum.Parse(typeof(ScenicImgType), reader["ImgType"].ToString()),
                        Address = reader["Address"].ToString(),
                        ThumbAddress = reader.IsDBNull(reader.GetOrdinal("ThumbAddress")) ? string.Empty : reader["ThumbAddress"].ToString(),
                        Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader["Description"].ToString(),
                        ScenicId = reader["ScenicId"].ToString(),
                        CompanyId = reader.IsDBNull(reader.GetOrdinal("CompanyId")) ? string.Empty : reader["CompanyId"].ToString(),
                        ScenicName = reader["ScenicName"].ToString()
                    });
                }
            }

            return list;
        }
        #endregion

        #region

        private string ConvertScenicImgType(ScenicImgType?[] imgType)
        {
            StringBuilder query = new StringBuilder();
            //图片类型
            if (imgType != null && imgType.Count() > 0)
            {
                query.Append(" (");
                foreach (ScenicImgType t in imgType)
                {
                    query.AppendFormat("{0},", (int)t);
                }
                query.Replace(",", "", query.Length - 1, 1);
                query.Append(")");
            }

            return query.ToString();
        }

        #endregion
    }
}
