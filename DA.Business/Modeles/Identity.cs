using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BA.Business.Modeles
{
    public class Identity
    {
        public string Publisher { get; set; }
        
        public string Audience { get; set; }
        
        public string Key { get; set; }
        
        public int LifeTime { get; set; }

        public string Salt { get; set; }
    }
}
