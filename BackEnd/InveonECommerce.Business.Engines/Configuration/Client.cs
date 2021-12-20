using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Configuration
{
    public class Client
    {
        public string Id { get; set; }
        //It's defined for keeping like username & password etc. infos
        public string Secret { get; set; }
        public List<string> Audiences{ get; set; }

        /// <summary>
        /// It's defined for which api access that client want 
        /// www.myapi1.com
        /// www.myapi2.com
        /// </summary>

    }
}
