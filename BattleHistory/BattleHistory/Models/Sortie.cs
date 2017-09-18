using BattleHistory.Repository;
using Grabacr07.KanColleWrapper.Models.Raw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleHistory.Models
{
    public class Sortie
    {
        public int RashinFlg
        {
            get;
            private set;
        }

        public int MapAreaId
        {
            get;
            private set;
        }

        public int MapInfoNo
        {
            get;
            private set;
        }

        public int CellNo
        {
            get;
            private set;
        }

        public int ColorNo
        {
            get;
            private set;
        }

        public int EventId
        {
            get;
            private set;
        }

        public int EventKind
        {
            get;
            private set;
        }

        public int Next
        {
            get;
            private set;
        }

        public int BossCellNo
        {
            get;
            private set;
        }

        public int BossComp
        {
            get;
            private set;
        }

        public int SortieCount
        {
            get;
            private set;
        }

        public DateTime UpdateTime
        {
            get;
            private set;
        }

        internal Sortie(kcsapi_map_start api_map_start)
        {
            RashinFlg = api_map_start.api_rashin_flg;
            MapAreaId = api_map_start.api_maparea_id;
            MapInfoNo = api_map_start.api_mapinfo_no;
            CellNo = api_map_start.api_no;
            ColorNo = api_map_start.api_color_no;
            EventId = api_map_start.api_event_id;
            EventKind = api_map_start.api_event_kind;
            Next = api_map_start.api_next;
            BossCellNo = api_map_start.api_bosscell_no;
            BossComp = api_map_start.api_bosscomp;
            SortieCount = GetSortieCount();
            UpdateTime = DateTime.Now;
        }

        internal int GetSortieCount()
        {
            var rep = new BattleHistoryRepository();
            return rep.GetSortieCount();
        }

        internal void Update(kcsapi_map_start api_map_start)
        {
            RashinFlg = api_map_start.api_rashin_flg;
            MapAreaId = api_map_start.api_maparea_id;
            MapInfoNo = api_map_start.api_mapinfo_no;
            CellNo = api_map_start.api_no;
            ColorNo = api_map_start.api_color_no;
            EventId = api_map_start.api_event_id;
            EventKind = api_map_start.api_event_kind;
            Next = api_map_start.api_next;
            BossCellNo = api_map_start.api_bosscell_no;
            BossComp = api_map_start.api_bosscomp;
            UpdateTime = DateTime.Now;
        }
    }
}
