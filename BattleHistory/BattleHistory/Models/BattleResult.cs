using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;
using Grabacr07.KanColleWrapper.Models.Raw;

namespace BattleHistory.Models
{
    class BattleResult : RawDataWrapper<kcsapi_battleresult>
    {
        internal BattleResult(kcsapi_battleresult rawData) : base(rawData)
        {
            getShip = new GetShip(rawData.api_get_ship);
        }

        public string WinRank => this.RawData.api_win_rank;

        public GetShip getShip;
    }
}
