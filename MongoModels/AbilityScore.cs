using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels
{
    public class AbilityScore
    {
        public Abilities Ability { get; set; }
        public int Score { get; set; }
        public int Modifier
        {
            get
            {
                // 1:       -5
                // 2-3:     -4
                // 10-11:   0
                // 18-19:   +4
                return (int)((double)Score / 2d) - 5;
            }
        }

        public AbilityScore()
        {
        }

        public AbilityScore(Abilities ability, int score)
        {
            Ability = ability;
            Score = score;
        }
    }
}
