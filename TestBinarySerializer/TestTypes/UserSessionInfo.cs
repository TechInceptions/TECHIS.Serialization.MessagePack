using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TECHIS.EndApps.AppModel.Business
{
    public class UserSessionInfo:UserSessionInfoData
    {
        public RoleMember RoleMember { get; set; }

        private string _NoSetter = nameof(UserSessionInfo);

        [ScaffoldColumn(false)]
        [XmlIgnore]
        [IgnoreDataMember]
        public string IgnoreThis { get; set; }

        public string NoSetter
        {
            get
            {
                return _NoSetter;
            }
        }
    }
}
