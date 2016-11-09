using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
    public class CharacterClass : MongoEntityBase<CharacterClass>
    {
        protected CharacterClass() : base()
        {
            // initialize a default Character Class
        }
    }
}
