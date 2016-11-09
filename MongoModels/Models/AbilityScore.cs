using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
    public class AbilityScore
    {
        public Abilities Ability { get; set; }
        public int Score { get; set; }

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
