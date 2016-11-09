using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
    public class CharacterModifier
    {
        public ItemProperties PropertyModified { get; set; }
        public int Value { get; set; }
        public string ModificationReason { get; set; }
    }
}
