using Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EFCRUD
    {
        Context dbCon = new Context();

        public void OverwriteBitVectors(List<BitVector> vectors)
        {
            foreach(var vect in dbCon.BitVectors.ToList())
                dbCon.Entry(vect).State = EntityState.Deleted;

            dbCon.SaveChanges();

            foreach (var vect in vectors)
                dbCon.Entry(vect).State = EntityState.Added;

            dbCon.SaveChanges();

        }

        public List<BitVector> GetStoredCards()
        {
            return dbCon.BitVectors.ToList();
        }

        public void SaveNetwork(AForge.Neuro.ActivationNetwork net, string code)
        {
            var existedNet = dbCon.Networks.FirstOrDefault(nr => nr.Code == code);
            if (existedNet != null)
            {
                dbCon.Entry(existedNet).State = EntityState.Modified;
            }
            else
            {
                NetworkRespawn nr = new NetworkRespawn();
                nr.Set(net);
                dbCon.Entry(nr).State = EntityState.Added;
            }
            
            dbCon.SaveChanges();
        }

        public AForge.Neuro.Network RestoreNetwork(string code)
        {
            var net = dbCon.Networks.FirstOrDefault(nr => nr.Code == code);
            return net.Get();
        }

        public void OverwriteCardOutputRealation(List<RankSuitOutputRelation> rsoList)
        {
            foreach (var rso in dbCon.RSORelations.ToList())
                dbCon.Entry(rso).State = EntityState.Deleted;

            dbCon.SaveChanges();

            foreach (var rso in rsoList)
                dbCon.Entry(rso).State = EntityState.Added;

            dbCon.SaveChanges();
        }

        public RankSuitOutputRelation getRSObyO(int classId)
        {
            dbCon.RSORelations.FirstOrDefault(rso => rso.NetworkClass == classId);
            throw new NotImplementedException();
        }
    }
}
