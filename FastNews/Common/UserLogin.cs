using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastNews.Common
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { get; set; }
        public int RoleID { get; set; }
        public int UserID { get; set; }
    }
}