using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandofGod
{
    class L
    {
        public static string Get(string[] v, int i)
        {
            if (i >= 0 && i < v.Length)
                return v[i];
            else return "<valore non valido>";
        }

        public static readonly string[] object_affects = {"None", "Strength", "Dexterity", "Intellect", "Wisdom", "Constitution", "Charisma", "Sex", "Level",
                                                          "Age", "Char Weight", "Char Height", "Mana", "Hit Points", "Movement", "Gold", "XP", "Armor", "Hitroll",
                                                          "Damroll", "Saving Paralysis", "Saing Rod", "Saving Petrification", "Saving Breath", "Saving Spell", "Saving All",
                                                          "Resistance", "Susceptibility", "Immunity", "Spell", "Weapon Spell", "Eat Spell", "Backstab", "Kick", "Sneak",
                                                          "Hide", "Bash", "Pick", "Steal", "Track", "Hit'N'Dam", "Spellfail", "Attacks", "Haste", "Slow", "Other", "Find Traps",
                                                          "Ride", "Race Slayer", "Alignment Slayer", "Mana Regen", "Hit Regen", "Move Regen", "Mod Thirst", "Mod Hunger", "Mod Drunk",
                                                          "T Strength", "T Intellect", "T Dexterity", "T Wisdom", "T Constitution", "T Charisma", "T HPS", "T Move", "T Mana",
                                                          "Spellpower", "Blunt Dam. Red.", "Slash Dam. Red.", "Pierce Dam. Red.","Sickness", "Spell Focus","Hit-N-SP", "Rapidità",
                                                          "Competenza", "Maestria", "Resilienza", "Persistenza", "Duplicazione", "Abilità conferita", "Furto vitale", "Evasione",
                                                          "Contrattacco", "Bonus al danno (fuoco)", "Bonus al danno (freddo)", "Bonus al danno (elett.)", "Bonus al danno (anima)",
                                                          "Bonus al danno (impatto)", "Bonus al danno (taglio)", "Bonus al danno (punta)", "Bonus al danno (natura)", "Bonus al danno (caos)",
                                                          "Bonus al danno (arcano)", "Bonus al danno (lumen)", "Bonus al danno (umbra)", "Colpo critico", "Forza", "Destrezza", "Intelligenza",
                                                          "Saggezza", "Costituzione", "Fortuna", "Modifica Abilità", "Resistenza al fuoco", "Resistenza al freddo", "Resistenza all'elett.",
                                                          "Resistenza all'anima", "Resistenza all'impatto", "Resistenza al taglio", "Resistenza alla perforazione", "Resistenza all'acido",
                                                          "Resistenza alla natura", "Resistenza al risucchio", "Resistenza al sonno", "Resistenza al fascino", "Resistenza alla paralisi",
                                                          "Resistenza al caos", "Resistenza Psichica", "Resistenza all'Arcano", "Resistenza alla luce", "Resistenza alle tenebre",
                                                          "Resistenza agli Elementi", "Resistenza al Divino", "Resistenza al Fisico", "Mod. danno inflitto", "Mod. danno subito",
                                                          "Mod. cure effettuate", "Mod. cure ricevute", "Bonus alla guarigione", "Bonus al danno (acido)", "Bonus al danno (trauma)",
                                                          "Intuizione", "Letalità", "Su colpo effettuato", "Bloccare con lo scudo", "Resistenza al Trauma", "Su colpo critico",
                                                          "Su colpo eccezionale", "Su colpo perfetto", "Su critico subito", "Su colpo subito", "Affisso casuale", "Affisso specifico",
                                                          "Incavo vuoto", "Incavo pieno", "Bonus alle abilità (fuoco)", "Bonus alle abilità (freddo)", "Bonus alle abilità (elett.)",
                                                          "Bonus alle abilità (anima)", "Bonus alle abilità (impatto)", "Bonus alle abilità (taglio)", "Bonus alle abilità (punta)",
                                                          "Bonus alle abilità (acido)", "Bonus alle abilità (natura)", "Bonus alle abilità (caos)", "Bonus alle abilità (psichiche)",
                                                          "Bonus alle abilità (arcane)", "Bonus alle abilità (luce)", "Bonus alle abilità (tenebra)", "Bonus alle abilità (trauma)"};

        public static readonly string[] object_types = {"None", "Light", "Scroll", "Wand", "Staff", "Weapon", "Fire Weapon", "Missile", "Treasure", "Armor",
                                                        "Potion", "Worn", "Other", "Trash", "Trap", "Container", "Note", "Liquid Container", "Key", "Food",
                                                        "Money", "Pen", "Boat", "Audio", "Board", "Tree", "Rock"};

        public static readonly string[] object_wear = {"Take", "Finger", "Neck", "Body", "Head", "Legs", "Feet", "Hands", "Arms", "Shield", "About", "Waist",
                                                       "Wrist", "Wield", "Hold", "Throw", "Personal-Eq", "Back", "Ear", "Eye", "Aura"};

        public static readonly string[] object_wear_ita = {"Prendibile", "Anello", "Collo", "Corpo", "Testa", "Gambe", "Piedi", "Mani", "Braccia", "Scudo", "Mantello", "Cintura",
                                                       "Polso", "Arma", "Tenuto", "Lancio", "Personalizzabile", "Schiena", "Orecchio", "Occhio", "Insegna"};

        public static readonly string[] init_wear = {"Come Luce", "Sul Dito Destro", "Sul Dito Sinistro", "Intorno al Collo (1)", "Intorno al Collo (2)", "Sul Corpo", 
                                                     "Sulla Testa", "Sulle Gambe", "Ai Piedi", "Sulle Mani", "Sulle Braccia", "Come Scudo", "Intorno al Corpo", "Intorno alla Vita",
                                                     "Sul Polso Destro", "Sul Polso Sinistro", "Come Arma Principale", "In Mano", "Sulle Spalle", "All'Orecchio Destro", "All'Orecchio Sinistro",
                                                     "Davanti agli Occhi", "Come Aura"};

        public static readonly string[] object_flags = {"Luminoso", "Ronzante", "Metallico", "Minerale", "Organico", "Invisibile", "Magico", "Maledetto", "Benedetto", "Anti Good",
                                                        "Anti Evil", "Anti Neutral", "Anti Cleric", "Anti Mage", "Anti Thief", "Anti Warrior", "Brittle", 
                                                        "Resistant", "Indistruttibile", "Femminile", "Plurale", "No Edit", "Anti Barbarian",
                                                        "Anti Ranger", "Anti Paladin", "Anti Psionist", "Anti Monk", "Anti Druid", "Only Class", "Scava",
                                                        "Taglia", "Donato"};

        public static readonly string[] object_value_names = {"~~~","Colore~Tipo~Durata~Qualcosa","Livello~Incantesimo #1~Incantesimo #2~Incantesimo #3", 
                                                              "Livello~Max Cariche~Cariche Rimaste~Incantesimo",
                                                              "Livello~Max Cariche~Cariche Rimaste~Incantesimo",
                                                              "A due mani~Numero Dadi~Numero Facce~Tipo di Danno",
                                                              "Forza Minima~Distanza Minima~Bonus sul Range~Tipo di Proiettile",
                                                              "% di Rottura~Numero di Dadi(danno)~Numero Facce(danno)~Tipo di Proiettile",
                                                              "~~~","AC Attuale~AC Massima~~","Livello~Incantesimo #1~Incantesimo #2~Incantesimo #3",
                                                              "~~~","~~~","~~~","Trigger~Tipo di Danno~Livello~Cariche","Capacità~Flags~Chiave~",
                                                              "~~~","Drink-unit Attuali~Drink-unit Max~Tipo di Liquido~Flags",
                                                              "One Time~~~","Ore di Sazietà~~Avvelenato~","Monete~~~","~~~","~~~","Timing~~~","~~~","~~~","~~~"};

        public static readonly string[] liquid_flags = { "Poisoned", "Permanent" };
        public static readonly string[] liquid_flags_short = { "Po", "Pe" };
        public static readonly string[] container_flags = { "Closeable", "Pickproof", "Closed", "Locked" };
        public static readonly string[] container_flags_short = { "Ca", "P", "C", "L" };
        public static readonly string[] container_key = { "-1 Nessuna Serratura", "0 Serratura senza Chiave" };
        public static readonly string[] alignment_names = { "Good", "Neutral", "Evil" };
        public static readonly string[] melee_damage_types = { "Smite", "Stab", "Whip", "Slash","Smash", "Cleave", "Crush", "Bludgeon", "Claw", "Bite",
                                                             "Sting", "Pierce", "Blast", "Range Weapon", "Hit" };
        public static readonly string[] liquid_types = { "Water", "Beer", "Wine", "Fruit Juice", "Dark Beer", "Whisky", "Lemonade", "Firebreath",
                                                         "Liquore", "Slime", "Milk", "Tea", "Coffee", "Blood", "Saltwater", "Spumante" };
        public static readonly string[] teleport_flags = { "Look", "Count", "Random", "Spin", "No Mob" };
        public static readonly string[] door_commands = { "Open", "Pull", "Twist", "Turn", "Lift", "Push", "Dig", "Cut" };
        public static readonly string[] door_status = { "Aperta", "Chiusa", "Serrata" };
        public static readonly string[] door_flags = { "IsDoor", "Closed", "Locked", "Secret", "No Bash", "No Pick", "Climb", "Male", "No Look",
                                                       "No Knock", "Invisible"};
        public static readonly string[] door_flags_short = { "", "", "", "", "B", "P", "C", "", "L", "K", "I" };
        public static readonly string[] directions = { "North", "East", "South", "West", "Up", "Down", "Special" };
        public static readonly string[] directions_colorcodes = { "$c0010", "$c0011", "$c0004", "$c0013", "$c0014", "$c0015" };
        public static readonly string[] zone_flags = { "", "", "Astral", "Desert", "Arctic", "Underground", "No Immortal" };
        public static readonly string[] zone_flags_complete = { "If Empty", "Always", "Astral", "Desert", "Arctic", "Underground", "No Immortal" };

        public static readonly string[] zone_level_ranges = { "Novizio (1-10)", "Iniziato (11-20)", "Cadetto (21-30)", "Esperto (31-40)", "Maestro (41-50)", "Maestro+", "Maestro++", "Maestro+++" };

        public static readonly string[] repop_types = { "Never", "If Empty", "Always" };

        /* old
        public static readonly string[] room_sectors = {"Inside", "City", "Field", "Forest", "Hills", "Mountain", "Water Swim", "Water No-Swim",
                                                        "Air", "Underwater", "Desert", "Tree", "Dark City", "Teleport"};
         * */

        public static readonly string[] room_sectors = {"Al chiuso","Citta'","Pianura","Foresta","Collina",
	                                                    "Montagna","Acque Basse","Acque Profonde","A mezz'aria",
	                                                    "Sott'acqua","Deserto","Tra gli Alberi","Citta' oscura",
	                                                    "Sottosuolo","Dungeon","Caverna","Cripta","Castello",
	                                                    "Maniero","Prigione","Tempio [G]","Tempio [N]","Tempio [E]",
	                                                    "Negozio","Giungla","Costa","Spiaggia","Prato",
	                                                    "Palude","Tundra","Taiga","Ghiacci","Steppa",
	                                                    "Savana","Piano Astrale","Piano Esterno","Sigil","Vuoto cosmico",
	                                                    "Sconosciuto","Teletrasporto"};

        public static readonly string[] room_sectors_col = {"$c0008Al chiuso$c0007","$c0007Citta'$c0007","$c0015Pianura$c0007","$c0010Foresta$c0007","$c0011Collina$c0007",
	                                                        "$c0003Montagna$c0007","$c0006Acque Basse$c0007","$c0004Acque Profonde$c0007","$c0014A mezz'aria$c0007",
	                                                        "$c0012Sott'acqua$c0007","$c0009Deserto$c0007","$c0002Tra gli Alberi$c0007","$c0005Citta' oscura$c0007",
	                                                        "$c0005Sottosuolo$c0007","$c0008Dungeon$c0007","$c0008Caverna$c0007","$c0008Cripta$c0007","$c0007Castello$c0007",
	                                                        "$c0007Maniero$c0007","$c0001Prigione$c0007","$c0015Tempio$c0007","$c0007Tempio$c0007","$c0001Tempio$c0007",
	                                                        "$c0005Negozio$c0007","$c0002Giungla$c0007","$c0003Costa$c0007","$c0011Spiaggia$c0007","$c0010Prato$c0007",
	                                                        "$c0008Palude$c0007","$c0014Tundra$c0007","$c0006Taiga$c0007","$c0015Ghiacci$c0007","$c0003Steppa$c0007",
	                                                        "$c0011Savana$c0007","$c0014Piano Astrale$c0007","$c0015Piano Esterno$c0007","$c0009Sigil$c0007","$c0008Vuoto cosmico$c0007",
	                                                        "$c0008Sconosciuto$c0007","$c0013Teletrasporto$c0007"};

        /* original room sectors
         * public static readonly string[] room_sectors = {"Inside", "City", "Field", "Forest", "Hills", "Mountain", "Water Swim", "Water No-Swim",
                                                        "Air", "Underwater", "Desert", "Tree", "Dark City", "Underdark", "Dungeon", "Cavern",
                                                        "Crypt", "Castle", "Manor", "Prison", "Temple (Good)", "Temple (Neutral)", "Temple (Evil)",
                                                        "Shop", "Jungle", "Shore", "Beach", "Plains", "Swamp", "Tundra", "Taiga", "Polar", "Steppe",
                                                        "Savannah", "Astral", "Planar", "Sigil", "Vacuum", "Unknown", "Teleport"};
        */

        /* pre-rename
        public static readonly string[] room_flags = {"Dark", "Death", "No Mob", "Indoors", "Peaceful", "No Steal", "No Summon", "No Magic", "Tunnel",
                                                      "Private", "No Sound", "Large", "No Death", "Save Room", "No Track", "No Mind", "Desertic",
                                                      "Artic", "Underground", "Hot", "Wet", "Cold", "Warzone", "Bright", "No Astral", "No Regain",
                                                      "No Target Portal", "No Summon IN", "No Summon OUT"};
        */

        public static readonly string[] room_flags = {"Dark", "Death", "No Mob", "Indoors", "Peaceful", "No Steal", "No Summon", "No Magic", "Tunnel",
                                                      "Private", "No Sound", "Large", "No Death", "Save Room", "No Track", "No Mind", "Desertic",
                                                      "Artic", "Underground", "Unreachable", "Battleground", "Regain Room", "Warzone", "Bright", "No Astral", "No Regain",
                                                      "No Target Portal", "No Summon IN", "No Summon OUT"};

        public static readonly string[] mob_races = {
                                                     "Meticcio (NON USARE!)",
                                                     "Umano",
                                                     "Elfo della Luna",
                                                     "Nano",
                                                     "Halfling",
                                                     "Gnomo",
                                                     "Rettile",
                                                     "Speciale",
                                                     "Licantropo",
                                                     "Drago",
                                                     "Non-Morto",
                                                     "Orco",
                                                     "Insetto",
                                                     "Aracnoide",
                                                     "Sauro",
                                                     "Pesce",
                                                     "Uccello",
                                                     "Gigante (generico)",
                                                     "Predatore",
                                                     "Parassita",
                                                     "Fangovivo",
                                                     "Demone",
                                                     "Serpente",
                                                     "Erbivoro",
                                                     "Albero",
                                                     "Vegetale",
                                                     "Elementale",
                                                     "Extra-Planare",
                                                     "Diavolo",
                                                     "Fantasma",
                                                     "Goblin",
                                                     "Troll",
                                                     "Uomo Vegetale",
                                                     "Mindflayer",
                                                     "Primate",
                                                     "Enfan",
                                                     "Elfo Oscuro",
                                                     "Golem",
                                                     "Uomo Rapace",
                                                     "Troglodita",
                                                     "Patryn",
                                                     "Labrat",
                                                     "Sartan",
                                                     "Titano",
                                                     "Folletto",
                                                     "Gurosiano",
                                                     "Equino",
                                                     "Draagdim",
                                                     "Astraliante",
                                                     "Divinità",
                                                     "Gigante delle Colline",
                                                     "Gigante dei Ghiacci",
                                                     "Gigante del Fuoco",
                                                     "Gigante delle Nuvole",
                                                     "Gigante delle Tempeste",
                                                     "Gigante delle Rocce",
                                                     "Drago Rosso",
                                                     "Drago Nero",
                                                     "Drago Verde",
                                                     "Drago Bianco",
                                                     "Drago Blu",
                                                     "Drago Argenteo",
                                                     "Drago Dorato",
                                                     "Drago Bronzeo",
                                                     "Drago Lucido",
                                                     "Drago D'Ottone",
                                                     "Vampiro",
                                                     "Lich",
                                                     "Necrospettro",
                                                     "Morto Vivente",
                                                     "Spettro", 
                                                     "Zombi",
                                                     "Scheletro",
                                                     "Ghoul",
                                                     "Mezz'elfo",
                                                     "Mezz'ogre",
                                                     "Mezz'orco",
                                                     "Mezzo Gigante",
                                                     "Ssarkasiano",
                                                     "Duergar",
                                                     "Svirfneblin",
                                                     "Gnoll",
                                                     "Elfo Dorato",
                                                     "Elfo Selvaggio",
                                                     "Elfo Acquatico",
                                                     "Draconico",
                                                     "Kender",
                                                     "Quickling",
                                                     "Centauro",
                                                     "Costrutto",
                                                     "Djinn",
                                                     "Fey",
                                                     "Minotauro",
                                                     "Aasimar",
                                                     "Tiefling",
                                                     "Strigo",
                                                    };

        public static readonly string[] trap_trigger_types_short = { "Mov", "Obj", "Mob", "N", "E", "S", "O", "A", "B" };

        public static readonly string[] trap_trigger_types = { "Movimento", "Raccogli / Posa", "Effetto sulle Creature",
                                                               "Nord", "Est", "Sud", "Ovest", "Alto", "Basso" };
        public static readonly string[] trap_damage_types = {"Poison", "Sleep", "Teleport", "Fire (Fireball)", "Cold (Frost Breath)", "Acid (Acid Blast)",
                                                             "Energy (Colour Spray)", "Blunt", "Pierce", "Slash" };

        public static readonly string[] mob_gender = { "Indefinito", "Maschio", "Femmina" };
        public static readonly string[] mob_positions = {"Dead", "Mortally Wounded", "Incapacitated", "Stunned", "Sleeping", "Resting", "Sitting", "Fighting",
                                                         "Standing", "Mounted"};
        public static readonly string[] mob_types = { "New", "Multi Attacks", "Unbashable", "Sound", "Simple", "Detailed" };
        public static readonly string[] damage_types = {"Fire", "Cold", "Electric", "Energy", "Blunt", "Pierce", "Slash", "Acid", "Poison", "Drain",
                                                        "Sleep", "Charm", "Hold", "No-Magic", "+1", "+2", "+3", "+4"};
        public static readonly string[] damage_types_short = {"F", "C", "El", "En", "Blu", "Pie", "Sla", "A", "Po", "D", "Sle", "Ch", "H", "N-M", "+1", "+2", "+3", "+4"};
        public static readonly string[] mob_acts = {"Immobile", "Sentinel", "Scavenger", "IsNPC", "Friendly", "Aggressive", "Stay-Zone", "Wimpy", 
                                                    "Annoying", "Hateful", "Afraid", "Immortal", "Hunting", "Deadly", "Polymorphed", "Meta-Aggressive",
                                                    "Guardian", "Int. Object", "Huge", "Script", "Greet", "Magic User", "Warrior", "Cleric", "Thief",
                                                    "Druid", "Monk", "Barbarian", "Paladin", "Ranger", "Psionist", "Archer"};
        public static readonly string[] mob_affects = {"Blind", "Invisible", "Detect Evil", "Detect Invisible", "Detect Magic", "Sense Life", "Life Saving",
                                                       "Sanctuary", "Dragon Ride", "Growth", "Curse", "Flying", "Poison", "Tree Travel", "Paralyzed", "Infravision",
                                                       "Waterbreath", "Sleep", "Travelling", "Sneak", "Hide", "Silenced", "Charmed", "Follower", "Prot. Evil",
                                                       "True Sight", "Scrying", "Fireshield", "Grouped", "Telepathy", "Iceshield"};
        public static readonly string[] mob_affects_short = {"B", "I", "DE", "DI", "DM", "SL", "LP", "S", "DR", "G", "C", "F", "P", "TT", "Par", "Inf",
                                                       "W", "Sleep", "Trav", "Sn", "H", "Sil", "Charm", "Foll", "PfE", "TS", "Scr", "Fs", "Group", "Tel", "Is"};
        public static readonly string[] shop_indigence = { "Vomito", "Sorriso", "Nessuna" };
        public static readonly string[] shop_attack = { "Avvertimento", "Insulto", "Nessuna" };
        public static readonly string[] fear_types = { "Nessuno", "Sesso", "Razza", "- Riservato -", "Classe", "Allineamento <=", "Allineamento >=", "VNumber" };
        public static readonly string[] classlist = { "Mago", "Chierico", "Guerriero", "Ladro", "Druido", "Monaco", "Barbaro", "Stregone", "Paladino", "Ranger", "Psionico" };
        public static readonly string[,] init_labels = { { "Mob", "Numero Massimo", "Stanza", "" },
                                                         { "Mob", "Stanza", "", "" },
                                                         { "Verso", "Tipo", "", "" },
                                                         { "Verso", "Tipo", "", "" },
                                                         { "Mob", "Numero Massimo", "Stanza", "" },
                                                         { "Oggetto", "Numero Massimo", "Contenitore", "" },
                                                         { "Oggetto", "Numero Massimo", "Posizione", "" },
                                                         { "Oggetto", "Numero Massimo", "", "" },
                                                         { "Oggetto", "Num Max (Mondo)", "Stanza", "Num Max (Stanza)" },
                                                         { "Oggetto", "Stanza", "", "" },
                                                         { "Stanza", "Direzione", "Stato", "" },
                                                         { "", "", "", "" } };
        public static readonly string[] separators = { "*!", ";" };
        public static readonly string[] init_types = {"Aggiungi Mob","Rimuovi Mob","Paura","Odio","Seguace","Metti Oggetto",
                                                      "Indossa Oggetto","Dai Oggetto","Aggiungi Oggetto","Rimuovi Oggetto",
                                                      "Init Porta","Commento"};
        public static readonly string[] generic_comments = {"Carica il mob {0} ({1}) nella stanza {2} ({3}).",
                                                            "Rimuovi il mob {0} ({1}) nella stanza {2} ({3}).",
                                                            "Paura {0} ({1}).",
                                                            "Odio {0} ({1}).",
                                                            "Carica il seguace {0} ({1}) nella stanza {2} ({3}).",
                                                            "Metti l'oggetto {0} ({1}) nell'oggetto {2} ({3}).",
                                                            "Indossa {0}: {1} ({2}).",
                                                            "In inventario: {0} ({1}).",
                                                            "Carica l'oggetto {0} ({1}) nella stanza {2} ({3}).",
                                                            "Rimuovi l'oggetto {0} ({1}) dalla stanza {2} ({3}).",
                                                            "Uscita '{0}' della stanza {1} {2}: {3}"};

        public const string move_noexit = "Non puoi andare da quella parte.";
    }
}
