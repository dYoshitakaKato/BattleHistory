using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace BattleHistory.Models
{
    public class BattleHistory : Sortie
    {
        public BattleResult BattleResult { get; set; }

        public BattleHistory(kcsapi_map_start api_map_start) : base(api_map_start)
        {
        }
    }
}
