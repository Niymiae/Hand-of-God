using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HandofGod
{
    public partial class frm_MudPreview : Form
    {
        const string NewLine = "\n";

        #region Vars
        public Area ParentArea;
        private Dictionary<int, List<PMob>> MobsInRoom;
        private Dictionary<int, List<PObj>> ObjectsInRoom;
        private Room inRoom;
        private bool isAFk;
        #endregion

        private bool showVNums
        {
            get { return Options.data.preview_showvnums; }
            set { Options.data.preview_showvnums = value; }
        }

        private bool autoExits
        {
            get { return Options.data.preview_autoexits; }
            set { Options.data.preview_autoexits = value; }
        }

        #region Constructor
        public frm_MudPreview()
        {
            InitializeComponent();
            MobsInRoom = new Dictionary<int, List<PMob>>();
            ObjectsInRoom = new Dictionary<int, List<PObj>>();
        }

        public void SetArea(Area parentarea, Room startingRoom = null)
        {
            ParentArea = parentarea;
            inRoom = startingRoom != null ? startingRoom : parentarea.rooms.First();
            txt_preview.Clear();
            SetupInits();
            Do_Look();
        }
        #endregion

        #region PElements Constructors
        private PObj CreatePObj(int vnum, Init i)
        {
            Obj o = ParentArea.Get<Obj>(vnum);
            PObj result = new PObj();
            result.vnum = vnum;
            result.percent = i.values[C.iv_percent];

            if (o == null)
            {
                result.shortdesc = "<obj non trovato>";
                result.longdesc = "<obj non trovato>";
                result.keys = "";
                result.parent = null;
            }
            else
            {
                result.shortdesc = o.shortdesc;
                result.longdesc = o.description;
                result.keys = o.keys;
                result.parent = o;
            }

            return result;
        }

        private PMob CreatePMob(int vnum, Init i)
        {
            Mob m = ParentArea.Get<Mob>(vnum);
            PMob result = new PMob();
            result.vnum = vnum;
            result.percent = i.values[C.iv_percent];

            if (m == null)
            {
                result.shortdesc = "<mob non trovato>";
                result.longdesc = "<mob non trovato>";
                result.description = "";
                result.keys = "";
                result.parent = null;
            }
            else
            {
                result.shortdesc = m.shortdesc;
                result.longdesc = m.longdesc;
                result.description = m.description;
                result.keys = m.keys;
                result.parent = m;
            }
            return result;
        }
        #endregion

        #region Utils
        private string RoomPrefix(Room r)
        {
            if (r == null)
                return "";
            return showVNums ? "[$c0006" + r.vnum + "$c0007] " : "";
        }

        private string ElementPrefix(PElement p)
        {
            if (p == null)
                return "";
            return showVNums ? "[$c0006" + p.vnum + "$c0007] " : "";
        }

        private string ElementSuffix(PElement p)
        {
            if (p == null)
                return "";
            return " ($c0015" + p.percent + "%$c0007)";
        }

        private bool matchElement(PElement p, string s)
        {
            foreach (string key in p.keys.ToLower().Split(' '))
                if (key.StartsWith(s))
                    return true;

            return false;
        }

        private string ElemToString(PElement p, bool shortvers = true)
        {
            return ElementPrefix(p) + (shortvers ? p.shortdesc : p.longdesc) + ElementSuffix(p);
        }

        private string ExitToString(Exit e)
        {
            if (e == null)
                return "";

            return (e.flags[1 << C.df_secret] ? "$c0009[" : "") + "$c001" + (e.dir <= C.dir_down ? e.dir + L.Get(L.directions, e.dir) : "0" + e.nameinlist) + (e.flags[1 << C.df_secret] ? "$c0009]" : "") + "$c0007 ";
        }

        #endregion

        #region Inits
        private void SetupInits()
        {
            PElement last = null;
            foreach (Zone z in ParentArea.zones)
                foreach (Init i in z.inits)
                {
                    if (i.type == C.it_comment)
                        continue;

                    if (i.GetElementType(2) == C.i_room)
                        // mobs and objects in rooms
                        switch (i.GetElementType(C.iv_value0))
                        {
                            case C.i_mob:

                                if (!MobsInRoom.ContainsKey(i.values[2]))
                                    MobsInRoom.Add(i.values[2], new List<PMob>());

                                PMob pm = CreatePMob(i.values[0], i);
                                MobsInRoom[i.values[2]].Add(pm);
                                last = pm;
                                break;
                            case C.i_obj:

                                if (!ObjectsInRoom.ContainsKey(i.values[2]))
                                    ObjectsInRoom.Add(i.values[2], new List<PObj>());

                                PObj po = CreatePObj(i.values[0], i);
                                ObjectsInRoom[i.values[2]].Add(po);

                                List<PObj> added = null;
                                int id = z.inits.IndexOf(i) + 1;
                                while (id < z.inits.Count && (z.inits[id].type == C.it_obj_put || z.inits[id].type == C.it_comment))
                                {
                                    if (z.inits[id].type == C.it_comment)
                                    {
                                        id++;
                                        continue;
                                    }

                                    if (added == null)
                                    {
                                        added = new List<PObj>();
                                        added.Add(po);
                                    }

                                    PObj container = added.Find(x => x.vnum == z.inits[id].values[2]);

                                    if (container == null)
                                    {
                                        Append("$c0001Warning $c0009- non trovo il container per " + z.inits[id].values[0]);
                                        id++;
                                        continue;
                                    }

                                    PObj po2 = CreatePObj(z.inits[id].values[0], i);
                                    container.AddObj(po2);
                                    added.Add(po2);
                                    id++;
                                }
                                last = null;
                                break;
                        }
                        // secondary objects
                    else if (i.GetElementType(0) == C.i_obj)
                    {
                        switch (i.type)
                        {
                            case C.it_obj_give:
                            case C.it_obj_wear:
                                PMob pm = last as PMob;

                                if (pm == null)
                                {
                                    Append("$c0001Warning - init give o wear ma non trovo il mob proprietario!");
                                    continue;
                                }

                                PObj po = CreatePObj(i.values[0], i);

                                if (i.type == C.it_obj_wear)
                                {
                                    if (!pm.AddEquipped(i.values[2], po))
                                        Append("$c0001Warning - $c0009L'oggetto " + po.shortdesc + " non puo' essere indossato da " + pm.shortdesc + " perche' lo slot e' occupato!");
                                }
                                else
                                    pm.AddInventory(po);
                                break;
                        }
                    }
                }
        }
        #endregion

        #region Methods
        private bool ChangeRoom(Room r)
        {
            if (r == null)
                return false;

            inRoom = r;
            return true;
        }

        private bool ChangeRoom(int vnum)
        {
            Room r = ParentArea.Get<Room>(vnum);
            if (ChangeRoom(r))
                return true;

            Append("Stanza #" + vnum + " non trovata.");
            return false;
        }
        #endregion

        #region Commands
        #region Do_Look, Do_Examine
        private void Do_LookMob(PMob pm)
        {
            Append("Guardi: " + pm.shortdesc);
            Append(pm.description);

            if (pm.equipped != null)
            {
                Append(" ");
                Append("$c0003Sta indossando: $c0007");
                foreach (var kv in pm.equipped)
                {
                    string wearpos = (L.Get(L.init_wear, kv.Key));
                    string spacer = "..........................".Substring(0, 26 - wearpos.Length);
                    Append("[$c0015" + wearpos + "$c0007]" + spacer + ElemToString(kv.Value));
                }
            }

            if (pm.inventory != null)
            {
                Append(" ");
                Append("$c0003In inventario: $c0007");
                foreach (PObj po in pm.inventory)
                    Append(ElemToString(po));
            }
        }

        private void Do_Look(string arg = "")
        {
            if (arg == "")
            {
                WriteRoom();
                return;
            }

            // look at dir
            for (int i = C.dir_north; i <= C.dir_down; i++)
                if (C.mud_commands[i].StartsWith(arg))
                {
                    Exit e = inRoom.GetExit(i);
                    if (e != null && e.desc != "")
                    {
                        Append(e.desc);
                        return;
                    }
                }

            // look at mob
            if (MobsInRoom.ContainsKey(inRoom.vnum))
            {
                foreach (PMob pm in MobsInRoom[inRoom.vnum])
                {
                    if (matchElement(pm, arg))
                        {
                            Do_LookMob(pm);
                            return;
                        }
                }
            }

            Append("Non lo vedi qui intorno.");
        }

        private void Do_Examine(string arg = "")
        {
            foreach (ExtraDesc ex in inRoom.extras)
                foreach (string s in ex.keys.ToLower().Split(' '))
                    if (s == arg)
                        Append(ex.desc);

            bool found = false;

            if (ObjectsInRoom.ContainsKey(inRoom.vnum))
            foreach (PObj po in ObjectsInRoom[inRoom.vnum])
            {
                if (po.parent != null)
                    foreach (ExtraDesc ex in po.parent.extras)
                        foreach (string s in ex.keys.ToLower().Split(' '))
                            if (s == arg)
                                Append(ex.desc);

                if (!found && matchElement(po, arg) && po.objects != null)
                {
                    Append("Guardi all'interno di " + po.shortdesc + "..");
                    foreach (PObj po1 in po.objects)
                    {
                        Append(ElemToString(po1));
                        found = true;
                    }
                }
            }
        }
        #endregion

        #region Do_Move, Do_Enter, Do_GoTo, Do_Teleport
        private void Do_Move(int dir, string name = "")
        {
            Exit e = inRoom.GetExit(dir, name);
            if (e == null)
            {
                Append(dir <= C.dir_down ? L.move_noexit : "Entrare dove?");
                return;
            }

            if (ChangeRoom(ParentArea.Get<Room>(e.room)))
                WriteRoom();
            else if (e.room == 0) // fake exit
                Append(e.desc);
             else Append("Probabilmente porta in un'altra area.");

            Init i = e.GetDoorInit(ParentArea);
            if (i != null)
            {
                Obj k = ParentArea.Get<Obj>(e.door.objkey);
                string skey = e.door.objkey > 0 ? " con la chiave " + (showVNums ? "#" + e.door.objkey + " " : "") + (k != null ? k.shortdesc : "(non trovata)") : "";
                Append("$c0012Hai attraversato " + e.door.keys + " (" + L.Get(L.door_status, i.values[2]) + ") " + skey);
            }
        }

        private void Do_Enter(string arg)
        {
            Do_Move(C.dir_special, arg);
        }

        private void Do_GoTo(string arg = "")
        {
            int vnum;
            int.TryParse(arg, out vnum);
            if (ChangeRoom(vnum))
                WriteRoom();
        }

        private void Do_Teleport()
        {
            if (inRoom.sect != C.rs_teleport)
                return;

            if (ChangeRoom(inRoom.tel_toroom))
                WriteRoom();
        }
        #endregion

        #region For Fun
        private void Do_Afk()
        {
            Append("$c0006" + (isAFk ? "Ritorni alla tua tastiera." : "Ti allontani momentaneamente dalla tastiera.") + "$c0007");
            isAFk = !isAFk;
        }

        private void Do_Who()
        {
              Append("$c0005Giocatori su Hand of God");
              Append("$c0005---------------------------");
              Append(" ");
              Append("$c0005Totale giocatori visibili: $c00150");
              Append("$c0005Picco massimo giocatori  : $c00150");
              Append(" ");
              Append("$c0013[$c0015Saregon$c0013] ti manda il pensiero 'Cosa ti aspettavi? :P'");
        }
        #endregion

        #region Do_Stat
        private void WriteRoomStats()
        {
            Append("$c0006Settore: $c0007" + L.Get(L.room_sectors, inRoom.sect));
            string flags = "";
            for (int i = 0; i <= C.rf_end; i++)
                if (inRoom.flags[1 << i])
                    flags += " " + L.Get(L.room_flags, i) + ",";
            Append("$c0006Flags:$c0007" + flags);
            Append("$c0006Extradesc:$c0007");
            foreach (ExtraDesc ex in inRoom.extras)
                Append(ex.keys);
        }

        private void WriteMobStats(PMob pm)
        {
            Mob p = pm.parent;

            Append("Settore: " + L.Get(L.room_sectors, inRoom.sect));
            Append("$c0014#" + pm.vnum + "$c0007");
            if (p != null)
                Append("$c0006Sex:$c0007 " + L.Get(L.mob_gender, p.values[C.mv_sex]) + ", $c0006Name:$c0007 " + pm.keys);
            Append("$c0006Short description:$c0007 " + pm.shortdesc);
            Append("$c0006Long description:$c0007 " + pm.longdesc);
            if (p != null)
            {
                Append("$c0006Monster Class:$c0007 " + L.Get(L.mob_types, p.values[C.mv_type]));
                Append("$c0006Level $c0007" + p.values[C.mv_level] + ", $c0006Alignment:$c0007 [" + p.values[C.mv_align] + "]");
                Append("$c0006AC:$c0007 [" + p.values[C.mv_ac] + "], $c0006Coins:$c0007 [" + p.values[C.mv_gold] + "], $c0006XP Bonus:$c0007 [" + p.values[C.mv_xpbonus] + "]");
                Append("$c0006Thac0:$c0007 [" + p.values[C.mv_thac0] + "], $c0006SpellPower:$c0007 [" + p.values[C.mv_spellpower] + "]");
                Append("$c0006DamRedBlunt:$c0007 [" + p.values[C.mv_red_blunt] + "] $c0006DamRedSlash:$c0007 [" + p.values[C.mv_red_slash] + "] $c0006DamRedPierce:$c0007 [" + p.values[C.mv_red_pierce] + "]");
                Append("$c0006LoadPosition:$c0007 " + L.Get(L.mob_positions, p.values[C.mv_loadpos]) + ", $c0006Default position:$c0007 " + L.Get(L.mob_positions, p.values[C.mv_defaultpos]));
                string s = "";
                for (int i = 0; i <= C.mf_end; i++)
                    s = s + (p.flags[1 << i] ? L.Get(L.mob_acts, i) + " " : "");
                Append("$c0006NPC flags:$c0007 " + s);
                Append("$c0006NPC Bare Hand Damage $c0007" + p.damage);
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (p.immunities[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") Append("$c0006Immune to:$c0007 " + s);
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (p.susceptibles[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") Append("$c0006Susceptible to:$c0007 " + s);
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (p.resistances[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") Append("$c0006Resistant to:$c0007 " + s);
                Append("$c0006Race:$c0007 " + L.Get(L.mob_races, p.values[C.mv_race]));
                s = "$c0006Affected by:$c0007 ";
                for (int i = 0; i <= C.ma_end; i++)
                    s = s + (p.affects[1 << i] ? L.Get(L.mob_affects, i) + " " : "");
                Append(s);
            }
        }

        private void WriteObjStats(PObj po)
        {
            Obj p = po.parent;

            Append("$c0014#" + po.vnum + "$c0007");
            if (p != null)
            {
                Append("$c0006Oggetto$c0007 '" + po.keys + "', $c0006Tipo:$c0007 " + L.Get(L.object_types, p.properties[C.op_type]));
                string s = "";
                for (int i = 0; i <= C.of_end; i++)
                    s = s + (p.flags[1 << i] ? L.Get(L.object_flags, i) + " " : "");
                Append("$c0006L'oggetto e':$c0007 " + s);
                s = "";
                for (int i = 0; i <= C.ow_end; i++)
                    s = s + (p.wearpos[1 << i] ? L.Get(L.object_wear, i) + " " : "");
                Append("$c0006Puo' essere indossato su:$c0007 " + s);
                Append("$c0006Peso:$c0007 " + p.properties[C.op_weight] + ", $c0006Valore:$c0007 " + p.properties[C.op_value] + ", $c0006Costo di rent:$c0007 " + p.properties[C.op_rent]);

                if (p.properties[C.op_type] == C.ot_armor)
                    Append("$c0006Il modificatore della AC e'$c0007 " + p.values[1]);

                if (p.properties[C.op_type] == C.ot_weapon || p.properties[C.op_type] == C.ot_missile)
                    Append("$c0006Il dado dei danni e'$c0007 '" + p.values[1] + "d" + p.values[2]);

                Append("$c0006Ecco i suoi effetti:$c0007");
                foreach (ObjAffect aff in p.affects)
                    if (aff.index > 0)
                    {
                        Append("$c0006Effetto :$c0007 " + L.Get(L.object_affects, aff.index) + " by " + aff.ValueToString());
                    }
            }
        }

        private void Do_Stat(string arg = "")
        {
            if (arg == "" || "room".StartsWith(arg))
            {
                WriteRoomStats();
                return;
            }

            if (MobsInRoom.ContainsKey(inRoom.vnum))
            {
                foreach (PMob pm in MobsInRoom[inRoom.vnum])
                {
                    if (matchElement(pm, arg))
                    {
                        WriteMobStats(pm);
                        return;
                    }
                }
            }

            if (ObjectsInRoom.ContainsKey(inRoom.vnum))
                foreach (PObj po in ObjectsInRoom[inRoom.vnum])
                {
                    if (matchElement(po, arg))
                    {
                        WriteObjStats(po);
                        return;
                    }
                }
        }
        #endregion

        private void Do_Where(string arg = "")
        {
            Append("$c0014Oggetti:$c0007 ");
            string itemscolor = "$c0007";

            List<string> mobs = new List<string>();
            List<string> containers = new List<string>();
            List<string> inventory = new List<string>();
            List<string> equipped = new List<string>();

            foreach (var kv in ObjectsInRoom)
                if (kv.Value != null)
                {
                    foreach (PObj po in kv.Value)
                    {
                        if (matchElement(po, arg))
                        {
                            Room r = ParentArea.Get<Room>(kv.Key);
                            Append(ElemToString(po) + " $c0012nella stanza$c0007 " + RoomPrefix(r) + (r != null ? r.shortdesc : "non trovata"));
                        }

                        if (po.objects != null)
                            foreach (PObj po2 in po.objects)
                            {
                                if (matchElement(po2, arg))
                                    containers.Add(ElemToString(po2) + " $c0012dentro all'oggetto$c0007 " + ElemToString(po));
                            }
                    }
                }

            foreach (var kv in MobsInRoom)
                if (kv.Value != null)
                {
                    foreach (PMob pm in kv.Value)
                    {
                        if (matchElement(pm, arg))
                        {
                            Room r = ParentArea.Get<Room>(kv.Key);
                            mobs.Add(ElemToString(pm) + " $c0012nella stanza$c0007 " + RoomPrefix(r) + itemscolor + (r != null ? r.shortdesc : "non trovata"));
                        }

                        if (pm.inventory != null)
                            foreach (PObj po in pm.inventory)
                                if (matchElement(po, arg))
                                    inventory.Add(ElemToString(po) + " $c0012nell'nventario di $c0007" + ElemToString(pm));

                        if (pm.equipped != null)
                            foreach (var kv1 in pm.equipped)
                                if (matchElement(kv1.Value, arg))
                                    inventory.Add(ElemToString(kv1.Value) + " $c0012indossato da $c0007" + ElemToString(pm));
                    }
            }

            foreach (string s in containers)
                Append(s);
            foreach (string s in inventory)
                Append(s);
            foreach (string s in equipped)
                Append(s);
            Append(" ");
            Append("$c0014Mob:$c0007 ");

            foreach (string s in mobs)
                Append(s);
        }

        private void Do_AutoExits()
        {
            autoExits = !autoExits;
            Append("$c0015Uscite Automatiche: " + (autoExits ? "$c0012ON" : "$c0012OFF"));
        }

        private void Do_ShowVNums()
        {
            showVNums = !showVNums;
            Append("$c0015Mostra VNum: " + (showVNums ? "$c0012ON" : "$c0012OFF"));
        }

        private void Do_Help()
        {
            Append("$c0006Lista comandi:");
            string result = "";
            foreach (string s in C.mud_commands)
                result += s + ", ";

            Append(result.Substring(0, result.Length - 2));
        }
        #endregion

        #region Room/PMob/PObj Writes
        private void WriteObj(PObj po)
        {
            Append(ElemToString(po, false) + " $c0003(" + po.keys + ")");
        }

        private void WriteMob(PMob pm)
        {
            Append(ElemToString(pm, false) + " $c0003(" + pm.keys + ")");
        }

        private void WriteRoom()
        {
            string Pshort = showVNums ? "$c0015[" + inRoom.vnum + "] $c0014" : "$c0014";
            string Eshort = "";
            if (inRoom.sect == C.rs_teleport)
                Eshort = " $c0007[" + L.Get(L.room_sectors_col, inRoom.tel_sect) + "$c0007] (" + L.Get(L.room_sectors_col, inRoom.sect) + ")";
            else Eshort = " $c0007[" + L.Get(L.room_sectors_col, inRoom.sect) + "$c0007]";
            
            Eshort += inRoom.flags[1 << C.rf_death] ? "$c0001[Death]$c0014" : "";
            Append(Pshort + inRoom.shortdesc + Eshort + "$c0007");
            Append("$c0007" + inRoom.longdesc + "$c0007");
            Append("$c0007" + WriteExits() + "$c0007");

            if (MobsInRoom.ContainsKey(inRoom.vnum))
                foreach (PMob pm in MobsInRoom[inRoom.vnum])
                    WriteMob(pm);

            if (ObjectsInRoom.ContainsKey(inRoom.vnum))
                foreach (PObj po in ObjectsInRoom[inRoom.vnum])
                    WriteObj(po);

            if (autoExits)
                WriteAutoExits();
        }

        private string WriteExits()
        {
            string exits = "Uscite: ";

            foreach (Exit e in inRoom.exits)
                exits += ExitToString(e);

            if (exits == "Uscite: ")
                exits += "$c0011Nessuna.";
            return exits + "$c0007";
        }

        private void WriteAutoExits()
        {
            Append("--------------");
            Append("Uscite visibili:");
            if (inRoom.exits.Count <= 0)
                Append("$c0011Nessuna.");
            else
            foreach (Exit e in inRoom.exits)
            {
                Room t = ParentArea.Get<Room>(e.room);
                Append(ExitToString(e) + "$c0007 verso " + (t != null ? RoomPrefix(t) + t.shortdesc : "(non trovata)"));
            }
            Append("--------------");
        }
        #endregion

        #region Append & WritePrompt
        private void Append(string s, bool withprompt = false)
        {
            txt_preview.AppendText(NewLine);

            foreach (var x in utils.ProcessTextColorCodes(s, Color.LightGray))
            {
                txt_preview.SelectionColor = x.c;
                txt_preview.AppendText(x.s);
            }

            if (withprompt)
                WritePrompt();
        }

        private void WritePrompt()
        {
            Append(NewLine + ">" + (inRoom != null ? "Stanza #" + inRoom.vnum + ">" : ""), false);
        }
        #endregion

        #region Commands Processing
        private int GetCommand(string s)
        {
            int i = 0;
            while (i < C.mud_commands.Length && !C.mud_commands[i].StartsWith(s))
                i++;
            return i;
        }

        private void ProcessCmd(string cmd)
        {
            string[] desc = cmd.Split(' ');

            if (desc.Length <= 0)
                return;

            desc[0] = desc[0].ToLower();

            if (isAFk)
                Do_Afk();

            if (desc[0] == "who")
            {
                Do_Who();
                WritePrompt();

                txt_preview.SelectionStart = txt_preview.Text.Length - 1;
                txt_preview.ScrollToCaret();
                return;
            }

            int i = GetCommand(desc[0]);

            string arg = desc.Length > 1 ? desc[1] : "";

            arg = arg.ToLower();

            switch (i)
            {
                case C.dir_north:
                case C.dir_east:
                case C.dir_south:
                case C.dir_west:
                case C.dir_up:
                case C.dir_down:
                    Do_Move(i);
                    break;
                case C.mcmd_enter: Do_Enter(arg); break;
                case C.mcmd_look: Do_Look(arg); break;
                case C.mcmd_goto: Do_GoTo(arg); break;
                case C.mcmd_examine: Do_Examine(arg); break;
                case C.mcmd_afk: Do_Afk(); break;
                case C.mcmd_teleport: Do_Teleport(); break;
                case C.mcmd_showvnums: Do_ShowVNums(); break;
                case C.mcmd_stat: Do_Stat(arg); break;
                case C.mcmd_where: Do_Where(arg); break;
                case C.mcmd_autoexits: Do_AutoExits(); break;
                case C.mcmd_help: Do_Help(); break;
                default: Append("Pardon?"); break;
            }

            WritePrompt();

            txt_preview.SelectionStart = txt_preview.Text.Length - 1;
            txt_preview.ScrollToCaret();
        }
        #endregion

        #region Widgets' Methods
        private void txt_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
                e.SuppressKeyPress = true;
        }

        private void txt_preview_KeyDown(object sender, KeyEventArgs e)
        {
            txt_input.Focus();
            e.SuppressKeyPress = true;
        }

        private void txt_input_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnexec_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void btnexec_Click(object sender, EventArgs e)
        {
            ProcessCmd(txt_input.Text);
            txt_input.SelectAll();
        }
        #endregion
    }
}
