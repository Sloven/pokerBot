using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class RankSuitOutputRelation
    {
        public int id { get; set; }
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }
        public int NetworkClass { get; set; }
    }
}
