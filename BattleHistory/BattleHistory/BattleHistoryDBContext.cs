using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleHistory
{
    internal class BattleHistoryDBContext : DbContext
    {
        public BattleHistoryDBContext() : base("name=LevelLogDBContext")
        {

        }

        public virtual DbSet<Models.BattleHistory> BattleHistories { get; set; }
        public virtual DbSet<Models.GetShip> GetShips { get; set; }
        public virtual DbSet<Models.BattleResult> BattleResults { get; set; }
        public virtual DbSet<Models.Sortie> Sorties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
