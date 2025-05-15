using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace HandofGod
{
    class C
    {
        // application version
        public const string AppVersion = "v3.0 (beta)";

        // application name
        public const string AppName = "Hand of God";

        //file extensions
        public static readonly string[] file_ext = { ".zon", ".wld", ".mob", ".obj", ".shp", ".hog" };

        // zone descriptions
        public const int zd_name = 0;
        public const int zd_author = 1;
        public const int zd_level = 2;
        public const int zd_quests = 3;
        public const int zd_desc = 4;
        public const int zd_end = 4;
        
        // zone level ranges
        public const int zlr_end = 7;

        // zone format lengths (number of descriptions)
        // 1 desc base version
        // 5 descs Helpzon version as of Dec. 2014
        public static readonly int[] zone_format_len = new int[2] { 0, 4 };

        // indexes, also used as column indexes for the HoGListView component
        public const int i_zone = 0;
        public const int i_room = 1;
        public const int i_mob = 2;
        public const int i_obj = 3;
        public const int i_shop = 4;
        public const int i_hogvisual = 5;
        // column indexes part 2
        public const int i_exit = 5; // this is not an error, hogvisual is not used as a column index
        public const int i_init_door = 10;
        public const int i_spell = 11;
        // reports
        public const int i_report_gems = 12;
        public const int i_report_coinsfame = 13;
        public const int i_report_treasures = 14;
        public const int i_report_keys = 15;
        public const int i_report_deathrooms = 16;
        public const int i_report_externalexits = 17;
        public const int i_report_nolinkedrooms = 18;
        public const int i_report_noinitmob = 19;
        public const int i_report_noinitobj = 20;
        // mob dialogs
        public const int i_mobdialog_Q = 21;
        public const int i_mobdialog_A = 22;
        public const int i_mobdialog_function = 23;
        // new shop item list
        public const int i_shop_item_list = 24;

        //directions
        public const int dir_north = 0;
        public const int dir_east = 1;
        public const int dir_south = 2;
        public const int dir_west = 3;
        public const int dir_up = 4;
        public const int dir_down = 5;
        public const int dir_special = 6;
        public static readonly Color[] dir_colors = { Color.Lime, Color.Yellow, Color.Blue, Color.Fuchsia, Color.Cyan, Color.White };
        //commands
        public const int cmd_open = 0;
        public const int cmd_pull = 1;
        public const int cmd_twist = 2;
        public const int cmd_turn = 3;
        public const int cmd_lift = 4;
        public const int cmd_push = 5;
        public const int cmd_dig = 6;
        public const int cmd_cut = 7;
        public const int cmd_end = 7;

        // zone flags
        public const int zf_if_empty = 0;
        public const int zf_always = 1;
        public const int zf_astral = 2;
        public const int zf_desert = 3;
        public const int zf_arctic = 4;
        public const int zf_underground = 5;
        public const int zf_noimmortal = 6;
        public const int zf_end = 6;

        //roomflags
        public const int rf_dark = 0;
        public const int rf_death = 1;
        public const int rf_nomob = 2;
        public const int rf_indoors = 3;
        public const int rf_peaceful = 4;
        public const int rf_nosteal = 5;
        public const int rf_nosummon = 6;
        public const int rf_nomagic = 7;
        public const int rf_tunnel = 8;
        public const int rf_private = 9;
        public const int rf_nosound = 10;
        public const int rf_large = 11;
        public const int rf_nodeath = 12;
        public const int rf_saveroom = 13;
        public const int rf_notrack = 14;
        public const int rf_nomind = 15;
        public const int rf_desertic = 16;
        public const int rf_artic = 17;
        public const int rf_underground = 18;
        public const int rf_hot = 19;
        public const int rf_wet = 20;
        public const int rf_cold = 21;
        public const int rf_warzone = 22;
        public const int rf_bright = 23;
        public const int rf_noastral = 24;
        public const int rf_noregain = 25;
        public const int rf_notargetportal = 26;
        public const int rf_nosummon_in_zone = 27;
        public const int rf_nosummon_out_zone = 28;
        public const int rf_end = 28;

        //Room Sectors
        public const int rs_inside = 0;
        public const int rs_city = 1;
        public const int rs_field = 2;
        public const int rs_forest = 3;
        public const int rs_hills = 4;
        public const int rs_mountain = 5;
        public const int rs_waterswim = 6;
        public const int rs_waternoswim = 7;
        public const int rs_air = 8;
        public const int rs_underwater = 9;
        public const int rs_desert = 10;
        public const int rs_tree = 11;
        public const int rs_darkcity = 12;
        public const int rs_underdark = 13;
        public const int rs_dungeon = 14;
        public const int rs_cavern = 15;
        public const int rs_crypt = 16;
        public const int rs_castle = 17;
        public const int rs_manor = 18;
        public const int rs_prison = 19;
        public const int rs_temple_good = 20;
        public const int rs_temple_neutral = 21;
        public const int rs_temple_evil = 22;
        public const int rs_shop = 23;
        public const int rs_jungle = 24;
        public const int rs_shore = 25;
        public const int rs_beach = 26;
        public const int rs_plains = 27;
        public const int rs_swamp = 28;
        public const int rs_tundra = 29;
        public const int rs_taiga = 30;
        public const int rs_polar = 31;
        public const int rs_steppe = 32;
        public const int rs_savannah = 33;
        public const int rs_astral = 34;
        public const int rs_planar = 35;
        public const int rs_sigil = 36;
        public const int rs_vacuum = 37;
        public const int rs_unknown = 38;
        public const int rs_teleport = 39;

        //doorflags
        public const int df_door = 0;
        public const int df_closed = 1;
        public const int df_locked = 2;
        public const int df_secret = 3;
        public const int df_nobash = 4;
        public const int df_nopick = 5;
        public const int df_climb = 6;
        public const int df_male = 7;
        public const int df_nolook = 8;
        public const int df_noknock = 9;
        public const int df_invisible = 10;
        public const int df_window = 11;
        public const int df_keyhole = 12;
        public const int df_samecmd = 13;
        public const int df_end = 13;

        //teleport flags
        public const int tf_look = 0;
        public const int tf_count = 1;
        public const int tf_random = 2;
        public const int tf_spin = 3;
        public const int tf_nomob = 4;
        public const int tf_end = 4;

        //gem types
        public const int gt_diamond = 0;
        public const int gt_emerald = 1;
        public const int gt_ruby = 2;
        public const int gt_sapphire = 3;
        public const int gt_end = 3;
        public const string gt_charset = "DSRZ";

        // epic talents
        public const int et_end = 18;

        //mob stats
        public const string ms_to_char = "SDIWOCVUGETKP";
        public const int ms_str = 0;
        public const int ms_dex = 1;
        public const int ms_int = 2;
        public const int ms_wis = 3;
        public const int ms_con = 4;
        public const int ms_chr = 5;
        // used by mob dialogs
        public const int ms_inventory_obj = 6;
        public const int ms_equipment_obj = 7;
        public const int ms_room_location = 8;
        public const int ms_mob_state = 9;
        public const int ms_mob_diag_state = 10;
        public const int ms_mob_skill = 11;
        public const int ms_mob_apply = 12;
        public const int ms_end = 12;
        public const int roll_ms_end = 5;

        //mob dialog values
        public const int mdv_id = 0;
        public const int mdv_next = 1;
        public const int mdv_num_check = 2;
        public const int mdv_num_roll = 3;
        public const int mdv_end = 3;
        //mob dialog descriptions
        public const int mdd_dialog = 0;
        public const int mdd_unspoken = 1;
        public const int mdd_before_act_tochar = 2;
        public const int mdd_before_act_tovict = 3;
        public const int mdd_before_act_toroom = 4;
        public const int mdd_after_act_tochar = 5;
        public const int mdd_after_act_tovict = 6;
        public const int mdd_after_act_toroom = 7;
        public const int mdd_end = 7;

        //mob positions
        public const int mp_dead = 0;
        public const int mp_mortally_wounded = 1;
        public const int mp_incapacitated = 2;
        public const int mp_stunned = 3;
        public const int mp_sleeping = 4;
        public const int mp_resting = 5;
        public const int mp_sitting = 6;
        public const int mp_fighting = 7;
        public const int mp_standing = 8;
        public const int mp_mounted = 9;
        public const int mp_end = 9;

        //mob types
        public const int mt_new = 0;
        public const int mt_multi_attacks = 1;
        public const int mt_unbashable = 2;
        public const int mt_sound = 3;
        public const int mt_simple = 4;
        public const int mt_detailed = 5;
        public const int mt_end = 5;

        //mob values
        public const int mv_type = 0;
        public const int mv_align = 1;
        public const int mv_level = 2;
        public const int mv_ac = 3;
        public const int mv_sex = 4;
        public const int mv_thac0 = 5;
        public const int mv_hpbonus = 6;
        public const int mv_attacks = 7;
        public const int mv_xpbonus = 8;
        public const int mv_race = 9;
        public const int mv_gold = 10;
        public const int mv_loadpos = 11;
        public const int mv_defaultpos = 12;
        public const int mv_spellpower = 13;
        public const int mv_red_blunt = 14;
        public const int mv_red_slash = 15;
        public const int mv_red_pierce = 16;
        public const int mv_balance_type = 17;
        public const int mv_end = 17;

        //mob acts
        public const int mf_immobile = 0;
        public const int mf_sentinel = 1;
        public const int mf_scavenger = 2;
        public const int mf_isnpc = 3;
        public const int mf_nice_thief = 4;
        public const int mf_aggressive = 5;
        public const int mf_zone = 6;
        public const int mf_wimpy = 7;
        public const int mf_annoying = 8;
        public const int mf_hateful = 9;
        public const int mf_afraid = 10;
        public const int mf_immortal = 11;
        public const int mf_hunting = 12;
        public const int mf_deadly = 13;
        public const int mf_polyself = 14;
        public const int mf_metaaggr = 15;
        public const int mf_guardian = 16;
        public const int mf_illusion = 17;
        public const int mf_huge = 18;
        public const int mf_script = 19;
        public const int mf_greet = 20;
        public const int mf_mu = 21;
        public const int mf_wa = 22;
        public const int mf_cl = 23;
        public const int mf_th = 24;
        public const int mf_dr = 25;
        public const int mf_mk = 26;
        public const int mf_ba = 27;
        public const int mf_pa = 28;
        public const int mf_ra = 29;
        public const int mf_ps = 30;
        public const int mf_archer = 31;
        public const int mf_end = 31;

        // mob affects
        public const int ma_blind = 0;
        public const int ma_invisible = 1;
        public const int ma_detect_evil = 2;
        public const int ma_detect_invisible = 3;
        public const int ma_detect_magic = 4;
        public const int ma_sense_life = 5;
        public const int ma_life_prot = 6;
        public const int ma_sanctuary = 7;
        public const int ma_dragon_ride = 8;
        public const int ma_growth = 9;
        public const int ma_curse = 10;
        public const int ma_flying = 11;
        public const int ma_poison = 12;
        public const int ma_tree_travel = 13;
        public const int ma_paralysis = 14;
        public const int ma_infravision = 15;
        public const int ma_waterbreath = 16;
        public const int ma_sleep = 17;
        public const int ma_travelling = 18;
        public const int ma_sneak = 19;
        public const int ma_hide = 20;
        public const int ma_silence = 21;
        public const int ma_charm = 22;
        public const int ma_follow = 23;
        public const int ma_protection_from_evil = 24;
        public const int ma_true_sight = 25;
        public const int ma_scrying = 26;
        public const int ma_fireshield = 27;
        public const int ma_group = 28;
        public const int ma_telepathy = 29;
        public const int ma_iceshield = 30;
        public const int ma_end = 30;

        //obj properties
        public const int op_type = 0;
        public const int op_weight = 1;
        public const int op_value = 2;
        public const int op_rent = 3;
        public const int op_version = 4;
        public const int op_end = 4;

        //objtype
        public const int ot_none = 0;
        public const int ot_light = 1;
        public const int ot_scroll = 2;
        public const int ot_wand = 3;
        public const int ot_staff = 4;
        public const int ot_weapon = 5;
        public const int ot_fireweapon = 6;
        public const int ot_missile = 7;
        public const int ot_treasure = 8;
        public const int ot_armor = 9;
        public const int ot_potion = 10;
        public const int ot_worn = 11;
        public const int ot_other = 12;
        public const int ot_trash = 13;
        public const int ot_trap = 14;
        public const int ot_container = 15;
        public const int ot_note = 16;
        public const int ot_liquidcontainer = 17;
        public const int ot_key = 18;
        public const int ot_food = 19;
        public const int ot_money = 20;
        public const int ot_pen = 21;
        public const int ot_boat = 22;
        public const int ot_audio = 23;
        public const int ot_board = 24;
        public const int ot_tree = 25;
        public const int ot_rock = 26;
        public const int ot_material = 27;
        public const int ot_coadjuvant = 28;
        public const int ot_end = 28;

        //objaffects
        public const int oa_none = 0;
        public const int oa_str = 1;
        public const int oa_dex = 2;
        public const int oa_int = 3;
        public const int oa_wis = 4;
        public const int oa_con = 5;
        public const int oa_chr = 6;
        public const int oa_sex = 7;
        public const int oa_level = 8;
        public const int oa_age = 9;
        public const int oa_char_weight = 10;
        public const int oa_char_height = 11;
        public const int oa_mana = 12;
        public const int oa_hit = 13;
        public const int oa_move = 14;
        public const int oa_gold = 15;
        public const int oa_exp = 16;
        public const int oa_ac = 17;
        public const int oa_armor = 17;
        public const int oa_hitroll = 18;
        public const int oa_damroll = 19;
        public const int oa_saving_para = 20;
        public const int oa_saving_rod = 21;
        public const int oa_saving_petri = 22;
        public const int oa_saving_breath = 23;
        public const int oa_saving_spell = 24;
        public const int oa_saving_all = 25;
        public const int oa_resistant = 26;
        public const int oa_susceptible = 27;
        public const int oa_immune = 28;
        public const int oa_spell = 29;
        public const int oa_weaponspell = 30;
        public const int oa_eatspell = 31;
        public const int oa_backstab = 32;
        public const int oa_kick = 33;
        public const int oa_sneak = 34;
        public const int oa_hide = 35;
        public const int oa_bash = 36;
        public const int oa_pick = 37;
        public const int oa_steal = 38;
        public const int oa_track = 39;
        public const int oa_hitndam = 40;
        public const int oa_spellfail = 41;
        public const int oa_attacks = 42;
        public const int oa_haste = 43;
        public const int oa_slow = 44;
        public const int oa_bv2 = 45;
        public const int oa_findtraps = 46;
        public const int oa_ride = 47;
        public const int oa_race_slayer = 48;
        public const int oa_alignment_slayer = 49;
        public const int oa_manaregen = 50;
        public const int oa_hitregen = 51;
        public const int oa_moveregen = 52;
        public const int oa_mod_thirst = 53;
        public const int oa_mod_hunger = 54;
        public const int oa_mod_drunk = 55;
        public const int oa_fire_res = 56;
        public const int oa_cold_res = 57;
        public const int oa_elec_res = 58;
        public const int oa_energy_res = 59;
        public const int oa_blunt_res = 60;
        public const int oa_pierce_res = 61;
        public const int oa_slash_res = 62;
        public const int oa_nature_res = 63;
        public const int oa_psych_res = 64;
        public const int oa_spellpower = 65;
        public const int oa_reduction_blunt = 66;
        public const int oa_reduction_slash = 67;
        public const int oa_reduction_pierce = 68;
        public const int oa_sickness = 69;
        // 69 off limits
        // +1 conversion in IO.cs to keep the array linear
        // This has been removed, I may want another way to hide part of the affects in the dropdown lists.
        public const int oa_critical = 70; // real ID -> 70
        public const int oa_hitnsp = 71; // real ID -> 71
        public const int oa_physicalred = 72;
        public const int oa_magicalred = 73;
        public const int oa_lethality = 74;
        public const int oa_penetration = 75; 
        public const int oa_warpaint = 76;                
        public const int oa_lumen_res = 77;
        public const int oa_umbra_res = 78;      
        public const int oa_chaos_res = 79;   
        public const int oa_new_str = 80;     
        public const int oa_new_dex = 81;    
        public const int oa_new_con = 82; 
        public const int oa_new_int = 83; 
        public const int oa_new_wis = 84; 
        public const int oa_new_chr = 85; 
        public const int oa_trauma_res = 86; 
        public const int oa_physical_res = 87; 
        public const int oa_elemental_res = 88; 
        public const int oa_divine_res = 89; 
        public const int oa_all_res = 90; 
        public const int oa_fire_damage = 91; 
        public const int oa_cold_damage = 92; 
        public const int oa_elec_damage = 93; 
        public const int oa_energy_damage = 94; 
        public const int oa_blunt_damage = 95; 
        public const int oa_pierce_damage = 96; 
        public const int oa_slash_damage = 97; 
        public const int oa_nature_damage = 98;
        public const int oa_psych_damage = 99;
        public const int oa_lumen_damage = 100;
        public const int oa_umbra_damage = 101;
        public const int oa_chaos_damage = 102; 
        public const int oa_trauma_damage = 103;
        public const int oa_res_acid = 104;
        public const int oa_acid_damage = 105;
        public const int oa_specific_affix = 106;
        public const int oa_random_affix = 107;
        public const int oa_has_class = 108;
        public const int oa_status_effect = 109;
        public const int oa_empty_socket = 110;
        public const int oa_filled_socket = 111;
        public const int oa_cunning = 112; 
        public const int oa_wspell_power = 113; 
        public const int oa_special_power = 114; 
        public const int oa_shield_block = 115; 
        public const int oa_perception = 116;
        public const int oa_scorching = 117;
        public const int oa_physical_melee = 118; 
        public const int oa_magical_melee = 119;
        public const int oa_end = 120;

        // object values & affects
        // 4 characters each
        // _ = null, N = number, S = spell, R = race, E = element (resistances), M melee damage type, 
        // X = sex, A = spell affect (bitvector), G = alignment
        // C container flags, I container special (-1 no lock, 0 lock without key, items list)
        // L liquid type, K liquid flags
        // T trap trigger type, Y trap damage type
        // (uses objtype as index)
        public static readonly string[] objvalues = {"____", // none
                                            "NNNN", // light
                                            "NSSS", // scroll
                                            "NNNS", // wand
                                            "NNNS", // staff
                                            "NNNM", // weapon
                                            "NNNN", // fire weapon
                                            "NNNN", // missile
                                            "____", // treasure
                                            "NNN_", // armor
                                            "NSSS", // potion
                                            "____", // worn
                                            "____", // other
                                            "____", // trash
                                            "TYNN", // trap
                                            "NCI_", // container
                                            "____", // note
                                            "NNLK", // liquid container
                                            "N___", // key
                                            "N_N_", // food
                                            "N___", // money
                                            "____", // pen
                                            "____", // boat
                                            "N___", // audio
                                            "____", // board
                                            "____", // tree
                                            "____", // rock
                                            "____", // material
                                            "____"};// coadjuvant
        // (uses objaffect as index)
        // Q complex affects
        public static readonly char[] objaffects = {'_','N','N','N','N','N','N','X',  // none str dex int wis con chr sex                                   - 7
                                                    'N','N','N','N','N','N','N','N',  // level age weight height mana hit move gold                         - 15
                                                    'N','N','N','N','N','N','N','N',  // xp ac hitroll damroll para rod petri breath                        - 23
                                                    'N','N','E','E','E','A','S','S',  // spell all resistant susceptible immune spell weaponspell eatspell  - 31
                                                    'N','N','N','N','N','N','N','N',  // backstab kick sneak hide bash pick steal track                     - 39
                                                    'N','N','N','N','N','N','N','N',  // hitndam spellfail attacks haste slow bv2 findtraps ride            - 47
                                                    'R','G','N','N','N','N','N','N',  // race alignment manareg hitreg movreg thirst hunger drunk           - 55
                                                    'N','N','N','N','N','N','N','N',  // tstr tint tdex twis tcon tchr thps tmove                           - 63
                                                    'N','N','N','N','N','N','N','N',  // tmana spellpower blunt slash pierce sick focus hitnsp              - 71
                                                    'N','N','N','N','N','N','N','N','N',  // speed prof mastery resil pers fork gainsk lifesteal evasion    - 80
                                                    'N','N','N','N','N','N','N','N',  // riposte bfire bcold belec banima bblunt bslash bpierce             - 88
                                                    'N','N','N','N','N','N','N','N',  // bnature bchaos bpsych barcane blumen bumbra critical nstr          - 96
                                                    'N','N','N','N','N','Q','N','N',  // ndex nint nwis ncon nlck spellmod resifire resicold                - 104
                                                    'N','N','Q','N','N','N','N','N',  // reselec resanima resblunt resslash respierce resacid resnat rdrain - 112
                                                    'N','N','N','N','N','N','N','N',  // rsleep rcharm rhold rchaos rpsych rarcane rlumen rumbra            - 120
                                                    'N','N','N','N','N','N','N','N',  // relem rdivine rphys mdamdone mdamtak mhealdone mhealtak bhealing   - 128
                                                    'N','N','N','N','N','N','Q','Q',  // bacid btrauma intuition lethality onhit block rtrauma oncrit       - 136
                                                    'Q','Q','Q','Q','Q','Q','Q','Q',  // onexc onexcrit onctaken onhittaken randomaf specaf esocket fsock   - 144
                                                    'Q','Q','Q','Q','Q','Q','Q','Q',  // firesk coldsk elesk animask bluntsk slashsk piercesk acidsk        - 152
                                                    'Q','Q','Q','Q','Q','Q','Q','Q'}; // natsk chaossk psysk arcanesk lumensk umbrask traumask end          - 160
                                               
                                                                                    
                                                                        

        //trap triggers
        public const int tt_move = 0;
        public const int tt_object = 1;
        public const int tt_room = 2;
        public const int tt_north = 3;
        public const int tt_east = 4;
        public const int tt_south = 5;
        public const int tt_west = 6;
        public const int tt_up = 7;
        public const int tt_down = 8;
        //trap damage types
        public const int td_poison = 0;
        public const int td_sleep = 1;
        public const int td_teleport = 2;
        public const int td_fire = 3;
        public const int td_cold = 4;
        public const int td_acid = 5;
        public const int td_energy = 6;
        public const int td_blunt = 7;
        public const int td_pierce = 8;
        public const int td_slash = 9;
        public static readonly int[] trapDam_types = { -4, -3, -2, 26, 308, 67, 10, 312, 313, 314 };
        //obj container flags
        public const int cf_closeable = 0;
        public const int cf_pickproof = 1;
        public const int cf_closed = 2;
        public const int cf_locked = 3;
        public const int cf_end = 3;
        //obj liquid container flags
        public const int lcf_poisoned = 0;
        public const int lcf_permanent = 1;
        public const int lcf_end = 1;
        //obj liquid types
        public const int lt_water = 0;
        public const int lt_beer = 1;
        public const int lt_wine = 2;
        public const int lt_fruitjuice = 3;
        public const int lt_darkbeer = 4;
        public const int lt_whisky = 5;
        public const int lt_lemonade = 6;
        public const int lt_firebreath = 7;
        public const int lt_liquore = 8;
        public const int lt_slime = 9;
        public const int lt_milk = 10;
        public const int lt_tea = 11;
        public const int lt_coffee = 12;
        public const int lt_blood = 13;
        public const int lt_saltwater = 14;
        public const int lt_spumante = 15;
        public const int lt_end = 15;
        //obj weapon damage types
        public const int wdt_smite = 0;
        public const int wdt_stab = 1;
        public const int wdt_whip = 2;
        public const int wdt_slash = 3;
        public const int wdt_smash = 4;
        public const int wdt_cleave = 5;
        public const int wdt_crush = 6;
        public const int wdt_bludgeon = 7;
        public const int wdt_claw = 8;
        public const int wdt_bite = 9;
        public const int wdt_sting = 10;
        public const int wdt_pierce = 11;
        public const int wdt_blast = 12;
        public const int wdt_rangeweapon = 13;
        public const int wdt_hit = 14;
        public const int wdt_end = 14;
        //objflags
        public const int of_glow = 0;
        public const int of_hum = 1;
        public const int of_metal = 2;
        public const int of_mineral = 3;
        public const int of_organic = 4;
        public const int of_invisible = 5;
        public const int of_magic = 6;
        public const int of_nodrop = 7;
        public const int of_bless = 8;
        public const int of_nogood = 9;
        public const int of_noevil = 10;
        public const int of_noneutral = 11;
        public const int of_nocl = 12;
        public const int of_nomu = 13;
        public const int of_noth = 14;
        public const int of_nowa = 15;
        public const int of_brittle = 16;
        public const int of_resistant = 17;
        public const int of_artifact = 18;
        public const int of_noman = 19;
        public const int of_nowoman = 20;
        public const int of_nosun = 21;
        public const int of_noba = 22;
        public const int of_nora = 23;
        public const int of_nopa = 24;
        public const int of_nops = 25;
        public const int of_nomk = 26;
        public const int of_nodr = 27;
        public const int of_onlyclass = 28;
        public const int of_dig = 29;
        public const int of_cut = 30;
        public const int of_donated = 31;
        public const int of_end = 31;

        //objwear
        public const int ow_take = 0;
        public const int ow_finger = 1;
        public const int ow_neck = 2;
        public const int ow_body = 3;
        public const int ow_head = 4;
        public const int ow_legs = 5;
        public const int ow_feet = 6;
        public const int ow_hands = 7;
        public const int ow_arms = 8;
        public const int ow_shield = 9;
        public const int ow_about = 10;
        public const int ow_waist = 11;
        public const int ow_wrist = 12;
        public const int ow_wield = 13;
        public const int ow_hold = 14;
        public const int ow_throw = 15;
        public const int ow_personal = 16;
        public const int ow_back = 17;
        public const int ow_ear = 18;
        public const int ow_eye = 19;
        public const int ow_insegna = 20;
        public const int ow_broken = 21;
        public const int ow_ephemeral = 22;
        public const int ow_feminine = 23;
        public const int ow_plural = 24;
        public const int ow_end = 24;

        //obj equipment pos
        public const int oe_light = 0;
        public const int oe_fingerR = 1;
        public const int oe_fingerL = 2;
        public const int oe_neck1 = 3;
        public const int oe_neck2 = 4;
        public const int oe_body = 5;
        public const int oe_head = 6;
        public const int oe_legs = 7;
        public const int oe_feet = 8;
        public const int oe_hands = 9;
        public const int oe_arms = 10;
        public const int oe_shield = 11;
        public const int oe_about = 12;
        public const int oe_waist = 13;
        public const int oe_wristR = 14;
        public const int oe_wristL = 15;
        public const int oe_wield = 16;
        public const int oe_hold = 17;
        public const int oe_back = 18;
        public const int oe_earR = 19;
        public const int oe_earL = 20;
        public const int oe_eye = 21;
        public const int oe_insegna = 22;
        public const int oe_end = 22;

        // damage types
        public const int dt_fire = 0;
        public const int dt_cold = 1;
        public const int dt_electric = 2;
        public const int dt_energy = 3;
        public const int dt_blunt = 4;
        public const int dt_pierce = 5;
        public const int dt_slash = 6;
        public const int dt_acid = 7;
        public const int dt_poison = 8;
        public const int dt_drain = 9;
        public const int dt_sleep = 10;
        public const int dt_charm = 11;
        public const int dt_hold = 12;
        public const int dt_nonmagic = 13;
        public const int dt_plus1 = 14;
        public const int dt_plus2 = 15;
        public const int dt_plus3 = 16;
        public const int dt_plus4 = 17;
        public const int dt_chaos = 18;
        public const int dt_trauma = 19;
        public const int dt_end = 19;
        // damage reductions
        public const int dr_blunt = 0;
        public const int dr_slash = 1;
        public const int dr_pierce = 2;
        public const int dr_end = 2;

        // races
        public const int race_halfbreed = 0;
        public const int race_human = 1;
        public const int race_elven = 2;
        public const int race_dwarf = 3;
        public const int race_halfling = 4;
        public const int race_gnome = 5;
        public const int race_reptile = 6;
        public const int race_special = 7;
        public const int race_lycanth = 8;
        public const int race_dragon = 9;
        public const int race_undead = 10;
        public const int race_orc = 11;
        public const int race_insect = 12;
        public const int race_arachnid = 13;
        public const int race_dinosaur = 14;
        public const int race_fish = 15;
        public const int race_bird = 16;
        public const int race_giant = 17;
        public const int race_predator = 18;
        public const int race_parasite = 19;
        public const int race_slime = 20;
        public const int race_demon = 21;
        public const int race_snake = 22;
        public const int race_herbiv = 23;
        public const int race_tree = 24;
        public const int race_veggie = 25;
        public const int race_element = 26;
        public const int race_planar = 27;
        public const int race_devil = 28;
        public const int race_ghost = 29;
        public const int race_goblin = 30;
        public const int race_troll = 31;
        public const int race_vegman = 32;
        public const int race_mflayer = 33;
        public const int race_primate = 34;
        public const int race_enfan = 35;
        public const int race_drow = 36;
        public const int race_golem = 37;
        public const int race_skexie = 38;
        public const int race_trogman = 39;
        public const int race_patryn = 40;
        public const int race_labrat = 41;
        public const int race_sartan = 42;
        public const int race_tytan = 43;
        public const int race_smurf = 44;
        public const int race_roo = 45;
        public const int race_horse = 46;
        public const int race_draagdim = 47;
        public const int race_astral = 48;
        public const int race_god = 49;
        public const int race_giant_hill = 50;
        public const int race_giant_frost = 51;
        public const int race_giant_fire = 52;
        public const int race_giant_cloud = 53;
        public const int race_giant_storm = 54;
        public const int race_giant_stone = 55;
        public const int race_dragon_red = 56;
        public const int race_dragon_black = 57;
        public const int race_dragon_green = 58;
        public const int race_dragon_white = 59;
        public const int race_dragon_blue = 60;
        public const int race_dragon_silver = 61;
        public const int race_dragon_gold = 62;
        public const int race_dragon_bronze = 63;
        public const int race_dragon_copper = 64;
        public const int race_dragon_brass = 65;
        public const int race_undead_vampire = 66;
        public const int race_undead_lich = 67;
        public const int race_undead_wight = 68;
        public const int race_undead_ghast = 69;
        public const int race_undead_spectre = 70;
        public const int race_undead_zombie = 71;
        public const int race_undead_skeleton = 72;
        public const int race_undead_ghoul = 73;
        public const int race_half_elven = 74;
        public const int race_half_ogre = 75;
        public const int race_half_orc = 76;
        public const int race_half_giant = 77;
        public const int race_lizardman = 78;
        public const int race_dark_dwarf = 79;
        public const int race_deep_gnome = 80;
        public const int race_gnoll = 81;
        public const int race_gold_elf = 82;
        public const int race_wild_elf = 83;
        public const int race_sea_elf = 84;
        public const int race_draconic = 85;
        public const int race_kender = 86;
        public const int race_quickling = 87;
        public const int race_centaur = 88;
        public const int race_construct = 89;
        public const int race_djinn = 90;
        public const int race_fey = 91;
        public const int race_minotaur = 92;
        public const int race_halfangel = 93;
        public const int race_halfdemon = 94;
        public const int race_witcher = 95;
        public const int race_end = 95;

        // shop properties
        public const int shp_mob = 0;
        public const int shp_room = 1;
        public const int shp_objtosell0 = 2;
        public const int shp_objtosell1 = 3;
        public const int shp_objtosell2 = 4;
        public const int shp_objtosell3 = 5;
        public const int shp_objtosell4 = 6;
        public const int shp_objtobuy0 = 7;
        public const int shp_objtobuy1 = 8;
        public const int shp_objtobuy2 = 9;
        public const int shp_objtobuy3 = 10;
        public const int shp_objtobuy4 = 11;
        public const int shp_react_indigence = 12;
        public const int shp_react_attack = 13;
        public const int shp_open0 = 14;
        public const int shp_close0 = 15;
        public const int shp_open1 = 16;
        public const int shp_close1 = 17;
        public const int shp_props_end = 17;
        // shop speeches
        public const int shp_speech_vendor_noobj = 0;
        public const int shp_speech_buyer_noobj = 1;
        public const int shp_speech_nosell = 2;
        public const int shp_speech_vendor_nomoney = 3;
        public const int shp_speech_buyer_nomoney = 4;
        public const int shp_speech_sell = 5;
        public const int shp_speech_buy = 6;
        public const int shp_speech_end = 6;

        // zone init values
        public const int iv_value0 = 0;
        public const int iv_value1 = 1;
        public const int iv_value2 = 2;
        public const int iv_value3 = 3;
        public const int iv_type = 4;
        public const int iv_percent = 5;
        public const int iv_end = 5;
        // zone init types
        public const int it_mob_add = 0;
        public const int it_mob_rem = 1;
        public const int it_mob_fear = 2;
        public const int it_mob_hate = 3;
        public const int it_follower_add = 4;
        public const int it_obj_put = 5;
        public const int it_obj_wear = 6;
        public const int it_obj_give = 7;
        public const int it_obj_add = 8;
        public const int it_obj_rem = 9;
        public const int it_door_init = 10;
        public const int it_comment = 11;
        public const int it_end = 11;
        public const string it_charset = "MKFHCPEGORD";

        // init fear/hate values
        public const int ifh_none = 0;
        public const int ifh_gender = 1;
        public const int ifh_race = 2;
        public const int ifh_reserved = 3;
        public const int ifh_class = 4;
        public const int ifh_aligninf = 5;
        public const int ifh_alignsup = 6;
        public const int ifh_vnum = 7;

        // mud preview
        public static string[] mud_commands = { "north", "east", "south", "west", "up", "down", "enter", "look", "afk", "examine", 
                                                "goto" , "teleport", "showvnums", "stat", "where", "autoexits", "help"};
        public const int mcmd_enter = 6;
        public const int mcmd_look = 7;
        public const int mcmd_afk = 8;
        public const int mcmd_examine = 9;
        public const int mcmd_goto = 10;
        public const int mcmd_teleport = 11;
        public const int mcmd_showvnums = 12;
        public const int mcmd_stat = 13;
        public const int mcmd_where = 14;
        public const int mcmd_autoexits = 15;
        public const int mcmd_help = 16;
    }
}
