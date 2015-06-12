using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EyouSoft.Common.DAL;
namespace EyouSoft.DAL.BackgroundServices
{
    public class TourServices : EyouSoft.Common.DAL.DALBase , EyouSoft.IDAL.BackgroundServices.ITourServices
    {
        public IEnumerable<System.Data.DataRow> GetNextOutbound(bool executeOnAll)
        {
            IEnumerable<System.Data.DataRow> messages = Enumerable.Empty<System.Data.DataRow>();
            IList<System.Data.DataRow> list = new List<System.Data.DataRow>();
            DbCommand dc = this.SystemStore.GetSqlStringCommand("SELECT * FROM A WHERE state=0;UPDATE A SET state=2 WHERE state=0");
            using (DataTable dt = DbHelper.DataTableQuery(dc, this.SystemStore))
            {
                messages = dt.Select("1=1");
            }
            return messages;
        }

        public void Save(System.Data.DataRow message)
        {
            DbCommand dc = this.SystemStore.GetStoredProcCommand("UPDATE A SET state=2 WHERE ID=@ID");
            this.SystemStore.AddInParameter(dc, "ID", DbType.String, message["ID"].ToString());
            DbHelper.DataTableQuery(dc, this.SystemStore);
        }
    }
}
