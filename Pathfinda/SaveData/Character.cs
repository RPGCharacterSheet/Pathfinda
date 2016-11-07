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
                return (Loads)(Inventory.Where(x => x.Properties.Any(y => y.Key == ItemProperties.ArmorWeight))?.Max(x => x.Properties[ItemProperties.ArmorWeight]) ?? 0);
            }
        }

        public Loads Encumbrance
        {
            get
            {
                return (Loads)Math.Max((int)EncumbranceByWeight, (int)EncumbranceByArmor);
            }
        }


        public int getDexModifier()
        {
            return Math.Min(AbilityScores[Abilities.Dexterity].Modifier, Encumbrance.MaxDexBonus());
        }

        private int tryKey (Gear item, ItemProperties key,  int def = 0)
        {
            return item.Properties.ContainsKey(key) ? item.Properties[key] : def;
        }

        // Aggregate all item additions in inventory. Should we be filtering by equipped or not?
        private int getItemsModifier(List<ItemProperties> items)
        {
            return Inventory.Sum(item => items.Sum(x => tryKey(item, x)));
        }

        private List<MiscEffect> filterMiscEffects(List<ItemProperties> matches)
        {
            return MiscEffects.Where(misc => matches.Contains(misc.Property)) as List<MiscEffect>;
        }

        private int ArmorCalculator(List<ItemProperties> modifiers)
        {
            // misc effects
            var miscArmorEffects = filterMiscEffects(modifiers);

            return new int[]
            {
                10,                                 //always get natural 10
                getDexModifier(),                   //addDex
                Size.ACAndAttackBonus(),            //Add ac from Size
                miscArmorEffects.Sum(x => x.Value), //Add misc armor effects
                getItemsModifier(modifiers)             //Add Modifiers from Items
            }.Sum();
        }

        delegate int del(Gear item,  ItemProperties key);
        public string ArmorClassDescription { get; private set; }
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
                });
            }
        }

        public string TouchACDescription { get; private set; }
        public int TouchAC
        {
            get
            {
                return ArmorCalculator(new List<ItemProperties> 
                {
                    ItemProperties.Dodge,
                    ItemProperties.Deflection
                });
                
            }
        }

        public string FlatFootedACDescription { get; private set; }
        public int FlatFootedAC
        {
            get
            {

                return ArmorCalculator(new List<ItemProperties> 
                {
                    ItemProperties.ArmorBonus,
                    ItemProperties.NaturalArmor,
                    ItemProperties.Deflection
                });
            }
        }

        public string SpellResistanceDescription { get; private set; }
        public int SpellResistance
        {
            get
            {
                int spellResistance = 0;
                SpellResistanceDescription = "";
                foreach (var item in Inventory)
                {
                    if (item.Properties.ContainsKey(ItemProperties.SpellResistance))
                    {
                        spellResistance += item.Properties[ItemProperties.SpellResistance];
                        SpellResistanceDescription += $"{item.Properties[ItemProperties.SpellResistance]} from {item.Name} + ";
                    }
                }
                foreach (var thing in MiscEffects.Where(x => x.Property == ItemProperties.SpellResistance))
                {
                    spellResistance += thing.Value;
                    SpellResistanceDescription += $"{thing.Value} from {thing.Source} + ";
                }
                SpellResistanceDescription.Trim(" +".ToCharArray());
                return spellResistance;
            }
        }

        public string FortitudeDescription { get; private set; }
        public int FortitudeSave
        {
            get
            {
                int fort = 0;
                fort += AbilityScores[Abilities.Constitution].Modifier; // base save + con modifier + magic modifier + misc
                FortitudeDescription = fort.ToString() + " from Con";
                foreach (var item in Inventory.Where(x => x.Properties.ContainsKey(ItemProperties.FortitudeBonus)))
                {
                    fort += item.Properties[ItemProperties.FortitudeBonus];
                    FortitudeDescription += " + " + item.Properties[ItemProperties.FortitudeBonus] + " from " + item.Name;
                }
                foreach (var miscFort in MiscEffects.Where(x => x.Property == ItemProperties.FortitudeBonus))
                {
                    fort += miscFort.Value;
                    FortitudeDescription += " + " + miscFort.Value + " from " + miscFort.Source;
                }
                return fort;
            }
        }

        public string ReflexDescription { get; private set; }
        public int ReflexSave
        {
            get
            {
                int reflex = 0;
                reflex += AbilityScores[Abilities.Dexterity].Modifier; // base save + con modifier + magic modifier + misc
                ReflexDescription = reflex.ToString() + " from Dex";
                foreach (var item in Inventory.Where(x => x.Properties.ContainsKey(ItemProperties.ReflexBonus)))
                {
                    reflex += item.Properties[ItemProperties.ReflexBonus];
                    ReflexDescription += " + " + item.Properties[ItemProperties.ReflexBonus] + " from " + item.Name;
                }
                foreach (var miscFort in MiscEffects.Where(x => x.Property == ItemProperties.ReflexBonus))
                {
                    reflex += miscFort.Value;
                    ReflexDescription += " + " + miscFort.Value + " from " + miscFort.Source;
                }
                return reflex;
            }
        }

        public string WillDescription { get; private set; }
        public int WillSave
        {
            get
            {
                int will = 0;
                will += AbilityScores[Abilities.Wisdom].Modifier; // base save + con modifier + magic modifier + misc
                WillDescription = $"{will.ToString()} from Wis";
                foreach (var item in Inventory.Where(x => x.Properties.ContainsKey(ItemProperties.WillBonus)))
                {
                    will += item.Properties[ItemProperties.WillBonus];
                    WillDescription += " + " + item.Properties[ItemProperties.WillBonus] + " from " + item.Name;
                }
                foreach (var miscFort in MiscEffects.Where(x => x.Property == ItemProperties.WillBonus))
                {
                    will += miscFort.Value;
                    WillDescription += " + " + miscFort.Value + " from " + miscFort.Source;
                }
                return will;
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
                if (AbilityScores[Abilities.Strength].Modifier != 0)
                {
                    cmd += AbilityScores[Abilities.Strength].Modifier;
                    CMDDescription += $" + {AbilityScores[Abilities.Strength].Modifier} from Str";
                }
                if (AbilityScores[Abilities.Dexterity].Modifier != 0)
                {
                    cmd += AbilityScores[Abilities.Dexterity].Modifier;
                    CMDDescription += $" + {AbilityScores[Abilities.Dexterity].Modifier} from Dex";
                }
                if (Size.ACAndAttackBonus() != 0)
                {
                    cmd += Size.ACAndAttackBonus();
                    CMDDescription += " + {Size.ACAndAttackBonus()} from {Size.ToString()} Size";
                }
                foreach (var thing in MiscEffects.Where(x => x.Property == ItemProperties.CombatManeuverDefense))
                {
                    cmd += thing.Value;
                    CMDDescription += $" + {thing.Value} from {thing.Source}";
                }
                CMDDescription += $" + {String.Join("/", BaseAttackBonus)} from BAB";

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
            Race = Races.Human;
            Size = Sizes.Medium;
            MaxHP = 6;
            CurrentHP = 2;
        }
    }
}
