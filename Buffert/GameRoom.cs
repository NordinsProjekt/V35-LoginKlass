using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buffert
{
    public class GameRoom
    {
        private List<GamersRecord> gamers = new List<GamersRecord>();

        public void AddGamer(GamersRecord gr)
        {
            gamers.Add(gr);
        }

        public void UpdateGamer(GamersRecord gr)
        {
            int index = gamers.FindLastIndex(x => x.username == gr.username);
            gamers[index] = gr;
        }

        public void DeleteGamer(string username)
        {
            int index = gamers.FindLastIndex(x => x.username == username);
            gamers.RemoveAt(index);
        }

        public List<GamersRecord> GetAllGamers()
        {
            return gamers;
        }
    }
    public record GamersRecord(string username,string mood);
}
