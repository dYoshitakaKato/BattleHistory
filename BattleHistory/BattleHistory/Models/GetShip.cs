using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace BattleHistory.Models
{
    class GetShip
    {
        private kcsapi_battleresult_getship api_get_ship;

        private int id { get;}
        private string name { get; }

        public GetShip(int id = 0, string name = null)
        {
            this.id = id;
            this.name = name;
        }


        public GetShip(kcsapi_battleresult_getship api_get_ship)
            :this(api_get_ship == null ? api_get_ship.api_ship_id : 0, 
                 api_get_ship == null ? api_get_ship.api_ship_name : null)
        {
        }
    }
}
