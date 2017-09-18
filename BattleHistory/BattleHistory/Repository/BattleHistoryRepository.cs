using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleHistory.Repository
{
    public class BattleHistoryRepository
    {
        public bool Insert(Models.BattleHistory model)
        {
            using (BattleHistoryDBContext context = new BattleHistoryDBContext())
            {
                //context.BattleHistories.
            }
            return true;
        }

        internal int GetSortieCount()
        {
            int count = 1;
            using (BattleHistoryDBContext context = new BattleHistoryDBContext())
            {
                count = context.BattleHistories.Select(x => x.SortieCount).Max();
            }
            return count;
        }
    }
}
