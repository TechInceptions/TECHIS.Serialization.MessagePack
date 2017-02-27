using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHIS.EndApps.AppModel.Business;

namespace TECHIS.EndApps.AppModel.Business
{
    public class UserSessionInfo
    {
        public Guid Key { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public RoleMember RoleMember { get; set; }
        public Dictionary<string,string> StringValues { get; set; }
        public Dictionary<string, int> IntValues { get; set; }
        public Dictionary<string, long> LongValues { get; set; }
        public Dictionary<string, decimal> DecimalValues { get; set; }
    }
}
