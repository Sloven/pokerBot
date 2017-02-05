using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PlayerCollection:IEnumerable<User>{

        public PlayerCollection() { Players = new List<User>(); }
        public PlayerCollection(List<User> pList) { Players = pList; }

        public List<User> Players{get;set;}

        public User this[int i]
        {
            get{return Players[i];}
            set{
                if(Players[i] != null)
                    Players[i] = value;
            }
        }

        public User this[string login]
        {
            get {
                var exists = Players.FirstOrDefault(p => p.GameLogin == login);
                return exists; 
            }
            set
            {
                var exists = Players.FirstOrDefault(p => p.GameLogin == login);
                if(exists != null)
                    exists = value;
            }
        }

        public void Add(User player)
        {
            var exists = Players.FirstOrDefault(p => p.GameLogin == player.GameLogin);
            if (exists != null)
                throw new ArgumentException("Player with this login already exists");

            Players.Add(player);
        }

        public void Remove(int i)
        {
            Players.Remove(Players[i]);
        }

        public void Remove(string login)
        {
            Players.Remove(this[login]);
        }

        public int Count()
        {
            return Players.Count;
        }

        public IEnumerator<User> GetEnumerator()
        {
            return (IEnumerator<User>)Players;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Players as IEnumerator;
        }
    }
}
