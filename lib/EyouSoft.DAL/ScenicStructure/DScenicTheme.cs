using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Model.ScenicStructure;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using EyouSoft.Common.DAL;

namespace EyouSoft.DAL.ScenicStructure
{
    /// <summary>
    /// 景区主题
    /// 创建者：郑付杰
    /// 创建时间：2011/10/28
    /// </summary>
    public class DScenicTheme:DALBase,EyouSoft.IDAL.ScenicStructure.IScenicTheme
    {
        private readonly Database _db = null;

        public DScenicTheme()
        {
            this._db = base.SystemStore;
        }



        #region IScenicTheme 成员
        /// <summary>
        /// 添加主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Add(MScenicTheme item)
        {
            string sql = "INSERT INTO tbl_ScenicTheme(ThemeName) VALUES(@ThemeName)";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ThemeName", DbType.String, item.ThemeName);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Update(MScenicTheme item)
        {
            string sql = "UPDATE tbl_ScenicTheme SET ThemeName = @ThemeName WHERE ThemeId = @id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@ThemeName", DbType.String, item.ThemeName);
            this._db.AddInParameter(comm, "@id", DbType.Int32, item.ThemeId);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 删除主题
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        public virtual bool Delete(int themeId)
        {
            string sql = "DELETE FROM  tbl_ScenicTheme WHERE ThemeId = @id";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            this._db.AddInParameter(comm, "@id", DbType.Int32, themeId);

            return DbHelper.ExecuteSql(comm, this._db) > 0 ? true : false;
        }
        /// <summary>
        /// 获取主题
        /// </summary>
        /// <returns></returns>
        public virtual IList<MScenicTheme> GetList()
        {
            string sql = "SELECT ThemeId,ThemeName FROM tbl_ScenicTheme WHERE IsDelete = '0'";
            DbCommand comm = this._db.GetSqlStringCommand(sql);
            IList<MScenicTheme> list = new List<MScenicTheme>();
            MScenicTheme item = null;
            using (IDataReader reader = DbHelper.ExecuteReader(comm,this._db))
            {
                while (reader.Read())
                {
                    list.Add(item = new MScenicTheme()
                    {
                        ThemeId = (int)reader["ThemeId"],
                        ThemeName = reader["ThemeName"].ToString()
                    });
                }
            }

            return list;
        }

        #endregion
    }
}
