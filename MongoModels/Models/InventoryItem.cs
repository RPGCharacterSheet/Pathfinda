using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoModels.Models
{
    public class InventoryItem
    {
        public string Name { get; set; }
        public bool IsEquipped { get; set; } = false;
        /// <summary>
        /// In lbs.
        /// Remember: Armor fitted for Small characters weighs half as much, and armor fitted for Large characters weighs twice as much
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Would be false if the item is carried in a bag of holding or a saddlebag or otherwise does not count against your carrying capacity.
        /// </summary>
        public bool WeightCounts { get; set; } = true;
        /// <summary>
        /// In Gold pieces
        /// </summary>        
        public double Value { get; set; }
        /// <summary>
        /// For weapons only
        /// e.g. 1d4 
        /// or 2d8+2
        /// </summary>
        public string Damage { get; set; }
        /// <summary>
        /// For weapons only
        /// e.g. 19-20/x2 
        /// or x3
        /// </summary>
        public string Critical { get; set; }
        /// <summary>
        /// e.g. Disarm, breaks on critical fail, talks
        /// </summary>
        public string Special { get; set; }

        public Dictionary<ItemProperties, int> Properties { get; set; }
        public List<ItemDescriptors> Descriptors { get; set; }

        public InventoryItem()
        {
            Properties = new Dictionary<ItemProperties, int>();
            Descriptors = new List<ItemDescriptors>();
        }
    }
}
