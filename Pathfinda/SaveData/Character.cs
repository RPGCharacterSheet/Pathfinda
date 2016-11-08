using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda.SaveData
{
    public class Character
    {
        public class MiscEffect
        {
            public ItemProperties Property { get; set; }
            public int Value { get; set; }
            public string Source { get; set; }
        }

        public Dictionary<Abilities, AbilityScore> AbilityScores { get; set; }
        public List<Gear> Inventory { get; set; }
        public List<MiscEffect> MiscEffects { get; set; }
        public Races Race { get; set; }
        public Sizes Size { get; set; }
        public double GearWeight
        {
            get
            {
                return Inventory?.Sum(x => x.WeightCounts ? x.Weight : 0) ?? 0;
            }
        }

        public Loads EncumbranceByWeight
        {
            get
            {
                return Formulas.GetEncumbrance(GearWeight, AbilityScores[Abilities.Strength].Score, Size);
            }
        }

        public Loads EncumbranceByArmor
        {
            get
            {
                return (Loads)(Inventory.Where(x => x.Properties.Any(y => y.Key == ItemProperties.ArmorWeight))?.Max(x => (int?)x.Properties[ItemProperties.ArmorWeight]) ?? 0);
            }
        }

        public Loads Encumbrance
        {
            get
            {
                return (Loads)Math.Max((int)EncumbranceByWeight, (int)EncumbranceByArmor);
            }
        }


        public int DexModifierForAC()
        {
            return Math.Min(AbilityScores[Abilities.Dexterity].Modifier, Encumbrance.MaxDexBonus());
        }

        private int TryGetItemProperty(Gear item, ItemProperties property, int @default = 0)
        {
            return item.Properties.ContainsKey(property) ? item.Properties[property] : @default;
        }

        // Aggregate modifiers for relevant items in inventory.
        private int SumInventoryModifiers(List<ItemProperties> modifiers, out string descriptionOfWhatGotSummed, bool mustBeEquipped = true)
        {
            int result = 0;
            descriptionOfWhatGotSummed = "";
            foreach (var item in Inventory.Where(x => x.Properties.Count > 0 && (!mustBeEquipped || x.IsEquipped)))
            {
                int sumOfRelevantProperties = item.Properties.Sum(x => modifiers.Contains(x.Key) ? x.Value : 0);
                if (sumOfRelevantProperties != 0)
                {
                    descriptionOfWhatGotSummed += $"+{sumOfRelevantProperties} from {item.Name}";
                    result += sumOfRelevantProperties;
                }
            }
            return result;
        }

        private int SumMiscModifiers(List<ItemProperties> modifiers, out string descriptionOfWhatGotSummed)
        {
            int result = 0;
            descriptionOfWhatGotSummed = "";
            foreach (var effect in MiscEffects.Where(x => modifiers.Contains(x.Property)))
            {
                descriptionOfWhatGotSummed += $"+{effect.Value} from {effect.Source}";
                result += effect.Value;
            }
            return result;
        }

        private int SumInventoryAndMiscModifiers(List<ItemProperties> modifiers, out string descriptionOfWhatGotSummed)
        {
            string miscDescription = "";
            var miscTotal = SumMiscModifiers(modifiers, out miscDescription);
            string armorDescription = "";
            var itemTotal = SumInventoryModifiers(modifiers, out armorDescription);
            descriptionOfWhatGotSummed = $"{armorDescription} {miscDescription}";
            return itemTotal + miscTotal;
        }

        private int ArmorCalculator(List<ItemProperties> modifiers, out string descriptionOfArmorBonuses)
        {
            string miscDescription = "";
            var miscArmorEffects = SumMiscModifiers(modifiers, out miscDescription);
            string armorDescription = "";
            var itemArmor = SumInventoryModifiers(modifiers, out armorDescription);
            descriptionOfArmorBonuses = $"10 + {DexModifierForAC()} from Dex + {Size.ACAndAttackBonus()} from Size {armorDescription} {miscArmorEffects}";

            return new int[]
            {
                10,                                 //always get natural 10
                DexModifierForAC(),                 //add Dex
                Size.ACAndAttackBonus(),            //Add ac from Size
                miscArmorEffects,                   //Add misc armor effects
                itemArmor                           //Add Modifiers from Items
            }.Sum();
        }

        private string _armorClassDescription = "";
        public string ArmorClassDescription { get { return _armorClassDescription; } }
        public int ArmorClass
        {
            get
            {
                return ArmorCalculator(new List<ItemProperties>
                {
                    ItemProperties.ArmorBonus,
                    ItemProperties.NaturalArmor,
                    ItemProperties.Dodge,
                    ItemProperties.Deflection
                }, out _armorClassDescription);
            }
        }

        private string _touchACDescription = "";
        public string TouchACDescription { get { return _touchACDescription; } }
        public int TouchAC
        {
            get
            {
                return ArmorCalculator(new List<ItemProperties>
                {
                    ItemProperties.Dodge,
                    ItemProperties.Deflection
                }, out _touchACDescription);

            }
        }

        private string _flatFootedACDescription = "";
        public string FlatFootedACDescription { get { return _flatFootedACDescription; } }
        public int FlatFootedAC
        {
            get
            {

                return ArmorCalculator(new List<ItemProperties>
                {
                    ItemProperties.ArmorBonus,
                    ItemProperties.NaturalArmor,
                    ItemProperties.Deflection
                }, out _flatFootedACDescription);
            }
        }

        private string _spellResistanceDescription = "";
        public string SpellResistanceDescription { get { return _spellResistanceDescription; } }
        public int SpellResistance
        {
            get
            {
                return SumInventoryAndMiscModifiers(new List<ItemProperties>() { ItemProperties.SpellResistance }, out _spellResistanceDescription);
            }
        }

        public string FortitudeDescription { get; private set; }
        public int FortitudeSave
        {
            get
            {
                int fort = AbilityScores[Abilities.Constitution].Modifier; // base save + con modifier + magic modifier + misc
                string itemEffectsDescription = "";
                int itemEffects = SumInventoryAndMiscModifiers(new List<ItemProperties>() { ItemProperties.FortitudeBonus }, out itemEffectsDescription);
                FortitudeDescription = $"{fort} from Con {itemEffectsDescription}";
                return fort + itemEffects;
            }
        }

        public string ReflexDescription { get; private set; }
        public int ReflexSave
        {
            get
            {
                int reflex = AbilityScores[Abilities.Dexterity].Modifier; // base save + con modifier + magic modifier + misc
                string itemEffectsDescription = "";
                int itemEffects = SumInventoryAndMiscModifiers(new List<ItemProperties>() { ItemProperties.ReflexBonus }, out itemEffectsDescription);
                ReflexDescription = $"{reflex} from Dex {itemEffectsDescription}";
                return reflex + itemEffects;
            }
        }

        public string WillDescription { get; private set; }
        public int WillSave
        {
            get
            {
                int will = AbilityScores[Abilities.Wisdom].Modifier; // base save + con modifier + magic modifier + misc
                string itemEffectsDescription = "";
                int itemEffects = SumInventoryAndMiscModifiers(new List<ItemProperties>() { ItemProperties.WillBonus }, out itemEffectsDescription);
                WillDescription = $"{will} from Wis {itemEffects}";
                return will + itemEffects;
            }
        }

        public string CMDDescription { get; private set; }
        public List<int> CombatManeuverDefense
        {
            get
            {
                List<int> cmds = new List<int>();
                int cmd = 10;
                CMDDescription = "10";
                cmd += AbilityScores[Abilities.Strength].Modifier;
                CMDDescription += $" + {AbilityScores[Abilities.Strength].Modifier} from Str";
                cmd += AbilityScores[Abilities.Dexterity].Modifier;
                CMDDescription += $" + {AbilityScores[Abilities.Dexterity].Modifier} from Dex";
                cmd += Size.ACAndAttackBonus();
                CMDDescription += $" + {Size.ACAndAttackBonus()} from {Size} Size";
                foreach (var thing in MiscEffects.Where(x => x.Property == ItemProperties.CombatManeuverDefense))
                {
                    cmd += thing.Value;
                    CMDDescription += $" + {thing.Value} from {thing.Source}";
                }
                CMDDescription += $" + {string.Join("/", BaseAttackBonus)} from BAB";

                foreach (var bab in BaseAttackBonus)
                {
                    cmds.Add(cmd + bab);
                }
                return cmds;
            }
        }

        public List<int> BaseAttackBonus
        {
            get
            {
                // TODO
                return new List<int>() { 0 };
            }
        }

        public int Initiative
        {
            get
            {
                return 2; // dex modifier + misc
            }
        }

        public int Speed
        {
            get
            {
                return 30;
            }
        }

        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public double Gold { get; set; }
        public int XPCurrent { get; set; }
        public int XPNext { get; set; }
        public string Languages { get; set; }

        #region Specified by the player and not used in any calculations
        public Alignments Alignment { get; set; }
        public string Deity { get; set; }
        public string Homeland { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string DamageReduction { get; set; }
        public string Resistances { get; set; }
        public string Immunities { get; set; }
        public string Notes { get; set; }
        //public int Strength { get { return AbilityScores[Abilities.Strength].Score; } }
        //public int Dexterity { get { return AbilityScores[Abilities.Dexterity].Score; } }
        //public int Constitution { get { return AbilityScores[Abilities.Constitution].Score; } }
        //public int Wisdom { get { return AbilityScores[Abilities.Wisdom].Score; } }
        //public int Intelligence { get { return AbilityScores[Abilities.Intelligence].Score; } }
        //public int Charisma { get { return AbilityScores[Abilities.Charisma].Score; } }
        #endregion

        public Character()
        {
            Inventory = new List<Gear>();
            AbilityScores = new Dictionary<Abilities, AbilityScore>()
            {
                {Abilities.Strength, new AbilityScore(Abilities.Strength, 7) },
                {Abilities.Constitution, new AbilityScore(Abilities.Constitution, 10) },
                {Abilities.Dexterity,  new AbilityScore(Abilities.Dexterity, 10) },
                {Abilities.Wisdom,  new AbilityScore(Abilities.Wisdom, 10) },
                {Abilities.Intelligence, new AbilityScore(Abilities.Intelligence, 10) },
                {Abilities.Charisma, new AbilityScore(Abilities.Charisma, 18) },
            };
            MiscEffects = new List<MiscEffect>();
            Race = Races.Human;
            Size = Sizes.Medium;
            MaxHP = 6;
            CurrentHP = 2;
        }
    }
}
