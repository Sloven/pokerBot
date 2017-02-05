using Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess
{
    public class Context : DbContext
    {
        public Context() : base("CEDB") { }

        public DbSet<Entity.BitVector> BitVectors { get; set; }
        public DbSet<Entity.NetworkRespawn> Networks { get; set; }
        public DbSet<Entity.RankSuitOutputRelation> RSORelations { get; set; }
    }
}
