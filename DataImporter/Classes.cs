using MongoModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathfinda
{
    public static partial class ImportedData
    {
        // templates generated from Pathfinder Autosheet Google Sheet, Spellcasting tab at the bottom.

        // use this for classes that don't cast spells
        private static int[,] NoSpellsTemplate = new int[21, 10];

        // use this for sorcerer casts/day
        private static int[,] SpontaneousCasterTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   100,3,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   100,4,  0,  0,  0,  0,  0,  0,  0,  0},
            {   100,5,  0,  0,  0,  0,  0,  0,  0,  0},
            {   100,6,  3,  0,  0,  0,  0,  0,  0,  0},
            {   100,6,  4,  0,  0,  0,  0,  0,  0,  0}, // level 5
            {   100,6,  5,  3,  0,  0,  0,  0,  0,  0},
            {   100,6,  6,  4,  0,  0,  0,  0,  0,  0},
            {   100,6,  6,  5,  3,  0,  0,  0,  0,  0},
            {   100,6,  6,  6,  4,  0,  0,  0,  0,  0},
            {   100,6,  6,  6,  5,  3,  0,  0,  0,  0}, // level 10
            {   100,6,  6,  6,  6,  4,  0,  0,  0,  0},
            {   100,6,  6,  6,  6,  5,  3,  0,  0,  0},
            {   100,6,  6,  6,  6,  6,  4,  0,  0,  0},
            {   100,6,  6,  6,  6,  6,  5,  3,  0,  0},
            {   100,6,  6,  6,  6,  6,  6,  4,  0,  0}, // level 15
            {   100,6,  6,  6,  6,  6,  6,  5,  3,  0},
            {   100,6,  6,  6,  6,  6,  6,  6,  4,  0},
            {   100,6,  6,  6,  6,  6,  6,  6,  5,  3},
            {   100,6,  6,  6,  6,  6,  6,  6,  6,  4},
            {   100,6,  6,  6,  6,  6,  6,  6,  6,  6}, // level 20
        };

        // use this for sorcerer spells known
        private static int[,] SpontaneousLearnerTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   4,  2,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   5,  2,  0,  0,  0,  0,  0,  0,  0,  0},
            {   5,  3,  0,  0,  0,  0,  0,  0,  0,  0},
            {   6,  3,  1,  0,  0,  0,  0,  0,  0,  0},
            {   6,  4,  2,  0,  0,  0,  0,  0,  0,  0}, // level 5
            {   7,  4,  2,  1,  0,  0,  0,  0,  0,  0},
            {   7,  5,  3,  2,  0,  0,  0,  0,  0,  0},
            {   8,  5,  3,  2,  1,  0,  0,  0,  0,  0},
            {   8,  5,  4,  3,  2,  0,  0,  0,  0,  0},
            {   9,  5,  4,  3,  2,  1,  0,  0,  0,  0}, // level 10
            {   9,  5,  5,  4,  3,  2,  0,  0,  0,  0},
            {   9,  5,  5,  4,  3,  2,  1,  0,  0,  0},
            {   9,  5,  5,  4,  4,  3,  2,  0,  0,  0},
            {   9,  5,  5,  4,  4,  3,  2,  1,  0,  0},
            {   9,  5,  5,  4,  4,  4,  3,  2,  0,  0}, // level 15
            {   9,  5,  5,  4,  4,  4,  3,  2,  1,  0},
            {   9,  5,  5,  4,  4,  4,  3,  3,  2,  0},
            {   9,  5,  5,  4,  4,  4,  3,  3,  2,  1},
            {   9,  5,  5,  4,  4,  4,  3,  3,  3,  2},
            {   9,  5,  5,  4,  4,  4,  3,  3,  3,  3}, // level 20
        };

        // use this for wizard casts/day and spells known
        private static int[,] PreparedCasterTemplate = new int[21, 10]
        {
            //  0   1   2   3   4   5   6   7   8   9
            {   0,  0,  0,  0,  0,  0,  0,  0,  0,  0}, // level 0 (placeholder only)
            {   3,  1,  0,  0,  0,  0,  0,  0,  0,  0}, // level 1
            {   4,  2,  0,  0,  0,  0,  0,  0,  0,  0},
            {   4,  2,  1,  0,  0,  0,  0,  0,  0,  0},
            {   4,  3,  2,  0,  0,  0,  0,  0,  0,  0},
            {   4,  3,  2,  1,  0,  0,  0,  0,  0,  0}, // level 5
            {   4,  3,  3,  2,  0,  0,  0,  0,  0,  0},
            {   4,  4,  3,  2,  1,  0,  0,  0,  0,  0},
            {   4,  4,  3,  3,  2,  0,  0,  0,  0,  0},
            {   4,  4,  4,  3,  2,  1,  0,  0,  0,  0},
            {   4,  4,  4,  3,  3,  2,  0,  0,  0,  0}, // level 10
            {   4,  4,  4,  4,  3,  2,  1,  0,  0,  0},
            {   4,  4,  4,  4,  3,  3,  2,  0,  0,  0},
            {   4,  4,  4,  4,  4,  3,  2,  1,  0,  0},
            {   4,  4,  4,  4,  4,  3,  3,  2,  0,  0},
            {   4,  4,  4,  4,  4,  4,  3,  2,  1,  0}, // level 15
            {   4,  4,  4,  4,  4,  4,  3,  3,  2,  0},
            {   4,  4,  4,  4,  4,  4,  4,  3,  2,  1},
            {   4,  4,  4,  4,  4,  4,  4,  3,  3,  2},
            {   4,  4,  4,  4,  4,  4,  4,  4,  3,  3},
            {   4,  4,  4,  4,  4,  4,  4,  4,  4,  4}, // level 20
        };

        private static List<Class> _classes = null;
        public static List<Class> Classes
        {
            get
            {
                if (_classes == null)
                {
                    _classes = new List<Class>()
                    {
                        new Class()
                        {
                            Name= "Alchemist",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = Abilities.Intelligence,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Appraise, ItemProperties.Craft, ItemProperties.DisableDevice, ItemProperties.Fly, ItemProperties.Heal, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeNature, ItemProperties.Perception, ItemProperties.Profession, ItemProperties.SleightOfHand, ItemProperties.Spellcraft, ItemProperties.Survival, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Antipaladin",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Craft, ItemProperties.Disguise, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.KnowledgeReligion, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Spellcraft, ItemProperties.Stealth },
                        },
                        new Class()
                        {
                            Name= "Barbarian",
                            HitDice = 12,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.KnowledgeNature, ItemProperties.Perception, ItemProperties.Ride, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Bard",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 6,
                            SpellCastingBonus =  Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.Disguise, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeLocal, ItemProperties.KnowledgeNature, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Linguistics, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.SleightOfHand, ItemProperties.Spellcraft, ItemProperties.Stealth, ItemProperties.UseMagicDevice},
                        },
                        new Class()
                        {
                            Name= "Cavalier",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Cleric",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Wisdom,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Appraise, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.Heal, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Linguistics, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.Spellcraft },
                        },
                        new Class()
                        {
                            Name= "Druid",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 4,
                            SpellCastingBonus = Abilities.Wisdom,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Fly, ItemProperties.HandleAnimal, ItemProperties.Heal, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeNature, ItemProperties.Perception, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Spellcraft, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name="Fighter",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 2,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Climb, ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Gunslinger",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Heal, ItemProperties.Intimidate, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeLocal, ItemProperties.Perception, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SleightOfHand, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Inquisitor",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 6,
                            SpellCastingBonus = Abilities.Wisdom,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.Disguise, ItemProperties.Heal, ItemProperties.Intimidate, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeNature, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Perception, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Spellcraft, ItemProperties.Stealth, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Magus",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Intelligence,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Fly, ItemProperties.Intimidate, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgePlanes, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Spellcraft, ItemProperties.Swim, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Monk",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeReligion, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Stealth, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Ninja",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 8,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Appraise, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.DisableDevice, ItemProperties.Disguise, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeLocal, ItemProperties.KnowledgeNobility, ItemProperties.Linguistics, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.SleightOfHand, ItemProperties.Stealth, ItemProperties.Swim, ItemProperties.UseMagicDevice},
                        },
                        new Class()
                        {
                            Name= "Oracle",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 4,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.Heal, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.Spellcraft },
                        },
                        new Class()
                        {
                            Name= "Paladin",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.HandleAnimal, ItemProperties.Heal, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgeReligion, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Spellcraft },
                        },
                        new Class()
                        {
                            Name= "Ranger",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 6,
                            SpellCastingBonus = Abilities.Wisdom,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Climb, ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Heal, ItemProperties.Intimidate, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeNature, ItemProperties.Perception, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Spellcraft, ItemProperties.Stealth, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Rogue",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 8,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Appraise, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.DisableDevice, ItemProperties.Disguise, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeLocal, ItemProperties.Linguistics, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.SleightOfHand, ItemProperties.Stealth, ItemProperties.Swim, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Samurai",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Sorcerer",
                            HitDice = 6,
                            BABGrowth = 0.5,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Appraise, ItemProperties.Bluff, ItemProperties.Craft, ItemProperties.Fly, ItemProperties.Intimidate, ItemProperties.KnowledgeArcana, ItemProperties.Profession, ItemProperties.Spellcraft, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Summoner",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Fly, ItemProperties.HandleAnimal, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeLocal, ItemProperties.KnowledgeNature, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Linguistics, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Spellcraft, ItemProperties.UseMagicDevice},
                        },
                        new Class()
                        {
                            Name= "Witch",
                            HitDice = 6,
                            BABGrowth = 0.5,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Intelligence,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Fly, ItemProperties.Heal, ItemProperties.Intimidate, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeNature, ItemProperties.KnowledgePlanes, ItemProperties.Profession, ItemProperties.Spellcraft, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Wizard",
                            HitDice = 6,
                            BABGrowth = 0.5,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Intelligence,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Appraise, ItemProperties.Craft, ItemProperties.Fly, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeLocal, ItemProperties.KnowledgeNature, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Linguistics, ItemProperties.Profession, ItemProperties.Spellcraft },
                        },
                        new Class()
                        {
                            Name= "Unchained Barbarian",
                            HitDice = 12,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.HandleAnimal, ItemProperties.Intimidate, ItemProperties.KnowledgeNature, ItemProperties.Perception, ItemProperties.Ride, ItemProperties.Survival, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Unchained Monk",
                            HitDice = 10,
                            BABGrowth = 1,
                            FortGrowth = SaveGrowth.Good,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 4,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeReligion, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.Stealth, ItemProperties.Swim },
                        },
                        new Class()
                        {
                            Name= "Unchained Rogue",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Poor,
                            SkillGrowth = 8,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Appraise, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.DisableDevice, ItemProperties.Disguise, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeLocal, ItemProperties.Linguistics, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.SenseMotive, ItemProperties.SleightOfHand, ItemProperties.Stealth, ItemProperties.Swim, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Unchained Summoner",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Poor,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 2,
                            SpellCastingBonus = Abilities.Charisma,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Craft, ItemProperties.Fly, ItemProperties.HandleAnimal, ItemProperties.KnowledgeArcana, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeGeography, ItemProperties.KnowledgeHistory, ItemProperties.KnowledgeLocal, ItemProperties.KnowledgeNature, ItemProperties.KnowledgeNobility, ItemProperties.KnowledgePlanes, ItemProperties.KnowledgeReligion, ItemProperties.Linguistics, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.Spellcraft, ItemProperties.UseMagicDevice },
                        },
                        new Class()
                        {
                            Name= "Vigilante",
                            HitDice = 8,
                            BABGrowth = 0.75,
                            FortGrowth = SaveGrowth.Poor,
                            ReflexGrowth = SaveGrowth.Good,
                            WillGrowth = SaveGrowth.Good,
                            SkillGrowth = 6,
                            SpellCastingBonus = null,
                            ClassSkills = new List<ItemProperties>() { ItemProperties.Acrobatics, ItemProperties.Appraise, ItemProperties.Bluff, ItemProperties.Climb, ItemProperties.Craft, ItemProperties.Diplomacy, ItemProperties.DisableDevice, ItemProperties.Disguise, ItemProperties.EscapeArtist, ItemProperties.Intimidate, ItemProperties.KnowledgeDungeoneering, ItemProperties.KnowledgeEngineering, ItemProperties.KnowledgeLocal, ItemProperties.Perception, ItemProperties.Perform, ItemProperties.Profession, ItemProperties.Ride, ItemProperties.SenseMotive, ItemProperties.SleightOfHand, ItemProperties.Stealth, ItemProperties.Survival, ItemProperties.Swim, ItemProperties.UseMagicDevice },
                        },
                    };
                }
                return _classes;
            }
        }
    }
}
