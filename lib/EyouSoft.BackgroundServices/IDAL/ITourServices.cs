using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyouSoft.IDAL.BackgroundServices
{
    public interface ITourServices
    {
        IEnumerable<System.Data.DataRow> GetNextOutbound(bool executeOnAll);
        void Save(System.Data.DataRow message);
    }
}
