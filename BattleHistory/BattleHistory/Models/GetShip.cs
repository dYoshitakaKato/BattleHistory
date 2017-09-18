using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace BattleHistory.Models
{
    public class GetShip
    {
        public int Id { get ;}
        public string Name { get; }

        internal GetShip(int id = 0, string name = null)
        {
            Id = id;
            Name = name;
        }


        internal GetShip(kcsapi_battleresult_getship api_get_ship)
            :this( api_get_ship.api_ship_id , api_get_ship.api_ship_name)
        {
        }
    }
}
