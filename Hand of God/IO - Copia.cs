using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace HandofGod
{
    public class FileManager
    {
        private string CutInitComments(string s)
        {
            int i = s.IndexOf("*!");

            if (i == -1)
                i = s.IndexOf(";");

            if (i > 0)
                return s.Substring(0, i);
            else return s;
        }

        #region Converters
        private decimal ToDecimal(string s)
        {
            decimal result = 0;
            if (decimal.TryParse(s, out result))
                return result;
            else
                return 0;
        }

        private int ToInt(char c)
        { return (int)Char.GetNumericValue(c); }

        private int ToInt(string s)
        {
            int result = 0;
            if (int.TryParse(s, out result))
                return result;
            else
                return 0;
        }
        #endregion

        #region Load

        #region Reads
        private string ReadUntil(StreamReader file, char pattern)
        {
            char[] buffer = new char[1];
            string s = "";

            while (!file.EndOfStream && buffer[0] != pattern)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] != pattern)
                    s = s + buffer[0];
            }
            return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        private string ReadUntil(StreamReader file, char pattern, char pattern2)
        {
            char[] buffer = new char[1];
            string s = "";

            while (!file.EndOfStream && buffer[0] != pattern && buffer[0] != pattern2)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] != pattern && buffer[0] != pattern2)
                    s = s + buffer[0];
            }
            return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }
        #endregion

        #region Zones (.Zon)
        public void LoadZon(List<Zone> zones, string filename, bool inits = false)
        {
            StreamReader file = new StreamReader(filename);
            char[] buffer;
            char lastchar = '\n';
            int lowest_vnum = 99999999;
            int highest_vnum = 0;
            Zone curr;

            string s = "";
            string[] desc;
            buffer = new char[1];

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    int vnum = ToInt(s);

                    curr = zones.Find(z => z.vnum == vnum);
                    if (curr == null)
                    {
                        curr = new Zone();
                        zones.Add(curr);
                    }

                    curr.filename = Path.GetFileNameWithoutExtension(filename);

                    // vnum
                    curr.vnum = vnum;
                    if (curr.vnum < lowest_vnum)
                        lowest_vnum = curr.vnum;
                    if (curr.vnum > highest_vnum)
                        highest_vnum = curr.vnum;

                    // zone name
                    curr.shortdesc = ReadUntil(file, '~');

                    ReadUntil(file, '\n');

                    desc = ReadUntil(file, '\n').Split(' ');
                    // vnum max
                    curr.vnum_max = ToInt(desc[0]);

                    // repop interval
                    curr.repop_interval = ToInt(desc[1]);

                    // flags
                    curr.flags[ToInt(desc[2])] = true;

                    if (desc.Length > 3)
                    {
                        curr.limit_xp = ToInt(desc[3]);
                        curr.limit_gems = ToInt(desc[4]);
                    }

                    if (curr.flags[1 << constants.zf_if_empty])
                        curr.repop_type = 1;
                    else if (curr.flags[1 << constants.zf_always])
                        curr.repop_type = 2;
                    else curr.repop_type = 0;

                    s = "";
                    Init lastInit = null;                    
                    while (!file.EndOfStream && !(s.Length > 0 && s[0] == 'S'))
                    {
                        s = ReadUntil(file, '\n').TrimEnd();
                        if (inits)
                        {
                            if (s.Length > 0 && Init.CharToIndex(s[0]) > -1)
                            {
                                desc = Regex.Split(CutInitComments(s), @"\D+");
                                Init init = new Init();
                                init.values[constants.iv_type] = Init.CharToIndex(s[0]);
                                if (desc.Length > 1)
                                    init.values[constants.iv_value0] = ToInt(desc[1]);
                                if (desc.Length > 2)
                                    init.values[constants.iv_value1] = ToInt(desc[2]);
                                if (desc.Length > 3)
                                    init.values[constants.iv_value2] = ToInt(desc[3]);
                                if (desc.Length > 4)
                                    init.values[constants.iv_value3] = ToInt(desc[4]);
                                init.original_str = s;

                                if (constants.ip_charset.IndexOf(s[0]) > -1)
                                {
                                    lastInit = init;
                                    curr.inits.Add(init);
                                }
                                else if (constants.is_charset.IndexOf(s[0]) > -1 && lastInit != null)
                                    lastInit.children.Add(init);
                            }
                        }
                    }
                }
                lastchar = buffer[0];
            }


            file.Close();
        }
        #endregion

        #region Rooms (.Wld)
        public void LoadWld(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            StreamReader file = new StreamReader(filename);
           // clear the current rooms list
            area.rooms.Clear();
            char[] buffer = new char[1];
            string[] desc;
            string[] flagsbuffer;
            char lastchar = '\n';
            int lowest_vnum = 99999999;
            int highest_vnum = 0;
            Room curr;
            string s = "";

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    curr = new Room();
                    // vnum
                    curr.vnum = ToInt(s);

                    if (curr.vnum < lowest_vnum)
                        lowest_vnum = curr.vnum;
                    if (curr.vnum > highest_vnum)
                        highest_vnum = curr.vnum;
                    area.rooms.Add(curr);
                }
                lastchar = buffer[0];
            }

            //zone.room_vnum_min = lowest_vnum;
            //zone.room_vnum_max = highest_vnum;
            file.BaseStream.Position = 0;
            file.DiscardBufferedData();

            lastchar = '\n';

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    curr = area.rooms.Find(r => r.vnum == ToInt(s));

                    // short desc
                    curr.shortdesc = ReadUntil(file, '~');
                    ReadUntil(file, '\n');

                    // long desc
                    curr.longdesc = ReadUntil(file, '~').TrimEnd();
                    ReadUntil(file, '\n');

                    desc = ReadUntil(file, '\n').Split(' ');
                    // zone
                    curr.area = ToInt(desc[0]);
                    // flags
                    flagsbuffer = desc[1].Split('|');
                    foreach (string v in flagsbuffer)
                    {
                        int flag = ToInt(v);
                        curr.flags[flag] = true;
                    }

                    curr.sect = ToInt(desc[2]);
                    if (curr.sect == -1)
                    {
                        curr.sect = constants.rs_teleport; // this will be converted again to -1 in the filesave method
                        curr.tel_time = ToInt(desc[3]);
                        curr.tel_toroom = ToInt(desc[4]);
                        flagsbuffer = desc[5].Split('|');
                        foreach (string v in flagsbuffer)
                        {
                            int flag = ToInt(v);
                            curr.tel_flags[flag] = true;
                        }
                        curr.tel_sect = ToInt(desc[6]);
                    }
                    else if (curr.sect == constants.rs_waternoswim || curr.sect == constants.rs_underwater)
                    {
                        curr.water_current_vel = ToInt(desc[3]);
                        curr.water_current_dir = ToInt(desc[4]);
                    }

                    if (curr.flags[1 << constants.rf_tunnel])
                       curr.max_pc = ToInt(desc[desc.Length - 1]);

                    while (!file.EndOfStream && buffer[0] != 'S')
                    {
                        file.Read(buffer, 0, 1);
                        if (lastchar == '\n')
                            switch (buffer[0])
                            {
                               // extra desc
                                case 'E': ExtraDesc ex = new ExtraDesc();
                                          ReadUntil(file, '\n');
                                          ex.keys = ReadUntil(file, '~');
                                          ReadUntil(file, '\n');
                                          ex.desc = ReadUntil(file, '~').TrimEnd();
                                          curr.extras.Add(ex);
                                          break;
                               // exits
                                case 'D': int dir = -1;
                                          Exit e = new Exit();
                                          file.Read(buffer, 0, 1);
                                          if (buffer[0] == ' ' || buffer[0] == '\n') // special exit
                                            {
                                                e.name = ReadUntil(file, '~');
                                                e.nameinlist = ReadUntil(file, '~');
                                                e.str_to = ReadUntil(file, '~');
                                                e.str_from = ReadUntil(file, '~');
                                                e.inverse = ReadUntil(file, '~');
                                                dir = constants.dir_special;
                                            }
                                            else // normal exit
                                            {
                                                dir = ToInt(buffer[0]);
                                            }

                                        ReadUntil(file, '\n');

                                        e.desc = ReadUntil(file, '~').TrimEnd();

                                        ReadUntil(file, '\n'); // necessario?

                                       // door keys
                                        e.door.keys = ReadUntil(file, '~');

                                        ReadUntil(file, '\n');

                                        s = ReadUntil(file, '\n');
                                        string[] exit_vars = s.Split(' ');

                                       // exit flags
                                        flagsbuffer = exit_vars[0].Split('|');
                                        foreach (string v in flagsbuffer)
                                        {
                                            int flag = ToInt(v);
                                            e.flags[flag] = true;
                                        }
                                       // door key object vnum
                                        e.door.objkey = ToInt(exit_vars[1]);
                                       // to room vnum
                                        int roomvnum = ToInt(exit_vars[2]);
                                        Room to = area.rooms.Find(r => r.vnum == roomvnum);
                                        e.room = to;
                                       // door command
                                        int cmd = ToInt(exit_vars[3]);
                                        e.door.cmd = Door.CmdToIndex(cmd);

                                        if (roomvnum > -1 && dir > -1)
                                        {
                                            if (to != null)
                                            {
                                                curr.pos.X = to.pos.X;
                                                curr.pos.Y = to.pos.Y;
                                            }

                                            curr.SetExit(to, roomvnum, dir, false, e);
                                            curr.SetPosition(area, dir, true); // sets the visual position
                                        }
                                    break;
                                // day & night desc
                                case 'L': 
                                            ReadUntil(file, '\n');
                                            curr.nightdesc = ReadUntil(file, '~').TrimEnd();
                                            curr.daydesc = ReadUntil(file, '~').TrimEnd();
                                            break;
                                case 'O':
                                            ReadUntil(file, '\n');
                                            curr.max_obj = ToInt(ReadUntil(file, '\n'));
                                            break;
                            }
                    }
                }
            }

            file.Close();
        }
        #endregion

        #region Mobs (.Mob)
        public void LoadMob(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            StreamReader file = new StreamReader(filename);
            // clear the current mobs list
            area.mobs.Clear();
            char[] buffer = new char[1];
            string[] desc;
            string[] flagsbuffer;
            char lastchar = '\n';
            Mob curr;
            string s = "";

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    curr = new Mob();
                    // vnum
                    curr.vnum = ToInt(s);
                    area.mobs.Add(curr);

                    // keys
                    curr.keys = ReadUntil(file, '~');
                    ReadUntil(file, '\n');

                    // short desc
                    curr.shortdesc = ReadUntil(file, '~');
                    ReadUntil(file, '\n');

                    // long desc
                    curr.longdesc = ReadUntil(file, '~').TrimEnd();
                    ReadUntil(file, '\n');

                    // description
                    curr.description = ReadUntil(file, '~').TrimEnd();
                    ReadUntil(file, '\n');

                  // first numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    // acts
                    flagsbuffer = desc[0].Split('|');
                    foreach (string v in flagsbuffer)
                    {
                        int flag = ToInt(v);
                        curr.flags[flag] = true;
                    }
                    // affects
                    flagsbuffer = desc[1].Split('|');
                    foreach (string v in flagsbuffer)
                    {
                        int flag = ToInt(v);
                        curr.affects[flag] = true;
                    }
                    curr.values[constants.mv_align] = ToInt(desc[2]);
                    curr.values[constants.mv_type] = Mob.TypeToIndex(desc[3]);
                    if (curr.values[constants.mv_type] == constants.mt_multi_attacks || curr.values[constants.mv_type] == constants.mt_sound)
                        curr.values[constants.mv_attacks] = ToInt(desc[4]);

                  // second numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    curr.values[constants.mv_level] = ToInt(desc[0]);
                    curr.values[constants.mv_thac0] = ToInt(desc[1]);
                    curr.values[constants.mv_ac] = ToInt(desc[2]);
                    curr.values[constants.mv_hpbonus] = ToInt(desc[3]);
                    curr.damage = desc[4].TrimEnd();

                  // third numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    // cos'e' desc[0] ?
                    curr.values[constants.mv_gold] = ToInt(desc[1]);
                    curr.values[constants.mv_xpbonus] = ToInt(desc[2]);
                    curr.values[constants.mv_race] = ToInt(desc[3]);

                  // fourth numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    curr.values[constants.mv_loadpos] = ToInt(desc[0]);
                    curr.values[constants.mv_defaultpos] = ToInt(desc[1]);
                    //
                    if (ToInt(desc[2]) > 2) // da risolvere questione gender > 2
                        curr.values[constants.mv_sex] = ToInt(desc[2]) - 3;
                    else
                        curr.values[constants.mv_sex] = ToInt(desc[2]);
                    //
                    if (desc.Length > 3)
                    {
                        // resistances
                        flagsbuffer = desc[3].Split('|');
                        foreach (string v in flagsbuffer)
                        {
                            int flag = ToInt(v);
                            curr.resistances[flag] = true;
                        }
                        // immunities
                        flagsbuffer = desc[4].Split('|');
                        foreach (string v in flagsbuffer)
                        {
                            int flag = ToInt(v);
                            curr.immunities[flag] = true;
                        }
                        // susceptibles
                        flagsbuffer = desc[5].Split('|');
                        foreach (string v in flagsbuffer)
                        {
                            int flag = ToInt(v);
                            curr.susceptibles[flag] = true;
                        }
                    }
                    // sound descriptions
                    if (curr.values[constants.mv_type] == constants.mt_sound)
                    {
                        // same room
                        curr.samesound = ReadUntil(file, '~').TrimEnd();
                        ReadUntil(file, '\n');
                        // adjacent
                        curr.adjacentsound = ReadUntil(file, '~').TrimEnd();
                        ReadUntil(file, '\n');
                    }

                    while (!file.EndOfStream && file.Peek() != '#')
                    {
                        file.Read(buffer, 0, 1);
                        switch (buffer[0])
                        {
                            case 'X': ReadUntil(file, '\n');
                                      curr.values[constants.mv_spellpower] = ToInt(ReadUntil(file, '\n'));  
                                      break;
                            case 'F': ReadUntil(file, '\n');
                                      curr.fame = ToInt(ReadUntil(file, '\n'));
                                      break;
                            case 'G': ReadUntil(file, '\n');
                                      file.Read(buffer, 0, 1);
                                      switch (buffer[0])
                                      {
                                          case 'D':
                                          case 'S':
                                          case 'R':
                                          case 'Z':
                                                    file.Read();
                                                    int index = constants.gt_charset.IndexOf(buffer[0]);
                                                    s = ReadUntil(file, ' ');
                                                    curr.gems[index].percent = ToInt(s);
                                                    curr.gems[index].dice = ReadUntil(file, '\n');
                                                    break;
                                      }
                                      break;
                            case 'R': ReadUntil(file, '\n');
                                      file.Read(buffer, 0, 1);
                                      switch (buffer[0])
                                      {
                                          case 'B': 
                                                     file.Read();
                                                     curr.values[constants.mv_red_blunt] = ToInt(ReadUntil(file, '\n'));
                                                     break;
                                          case 'S': 
                                                     file.Read();
                                                     curr.values[constants.mv_red_slash] = ToInt(ReadUntil(file, '\n'));
                                                     break;
                                          case 'P':
                                                     file.Read();
                                                     curr.values[constants.mv_red_pierce] = ToInt(ReadUntil(file, '\n'));
                                                     break;
                                      }
                                      break;
                            case 'E': ReadUntil(file, '\n');
                                      desc = ReadUntil(file, '\n').Split(' ');
                                      curr.epic_talents[ToInt(desc[0])] = ToInt(desc[1]);
                                      break;
                        }
                    }
                }
            }

            file.Close();
        }
        #endregion

        #region Objects (.Obj)
        public void LoadObj(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            StreamReader file = new StreamReader(filename);
            // clear the current objects list
            area.objects.Clear();
            char[] buffer = new char[1];
            string[] desc;
            string[] flagsbuffer;
            char lastchar = '\n';
            Obj curr;
            string s = "";

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    curr = new Obj(); 
                    // vnum
                    curr.vnum = ToInt(s);
                    area.objects.Add(curr);

                    // keys
                    curr.keys = ReadUntil(file, '~');
                    ReadUntil(file, '\n');

                    // short desc
                    curr.shortdesc = ReadUntil(file, '~');
                    ReadUntil(file, '\n');

                    // description
                    curr.description = ReadUntil(file, '~').TrimEnd();
                    ReadUntil(file, '\n');

                    // action description
                    curr.actiondesc = ReadUntil(file, '~').TrimEnd();
                    ReadUntil(file, '\n');

                    // first numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    curr.properties[constants.op_type] = ToInt(desc[0]);

                    // flags
                    flagsbuffer = desc[1].Split('|');
                    foreach (string v in flagsbuffer)
                    {
                        int flag = ToInt(v);
                        curr.flags[flag] = true;
                    }

                    // wear position
                    flagsbuffer = desc[2].Split('|');
                    foreach (string v in flagsbuffer)
                    {
                        int flag = ToInt(v);
                        curr.wearpos[flag] = true;
                    }

                    
                    // second numbers line
                    desc = ReadUntil(file, '\n').Split(' ');

                    // values
                    for (int i = 0; i <= 3; i++)
                        curr.values[i] = ToInt(desc[i]);

                    // third numbers line
                    desc = ReadUntil(file, '\n').Split(' ');
                    curr.properties[constants.op_weight] = ToInt(desc[0]);
                    curr.properties[constants.op_value] = ToInt(desc[1]);
                    curr.properties[constants.op_rent] = ToInt(desc[2]);

                    while (!file.EndOfStream && file.Peek() != '#')
                    {
                        file.Read(buffer, 0, 1);
                        switch (buffer[0])
                        {
                            // extra desc
                            case 'E': ExtraDesc ex = new ExtraDesc();
                                      ReadUntil(file, '\n');
                                      ex.keys = ReadUntil(file, '~');
                                      ReadUntil(file, '\n');
                                      ex.desc = ReadUntil(file, '~').TrimEnd();
                                      curr.extras.Add(ex);
                                      break;
                            // affect
                            case 'A': ReadUntil(file, '\n');
                                      desc = ReadUntil(file, '\n').Split(' ');
                                      curr.SetAffect(ToInt(desc[0]), ToInt(desc[1]));
                                      break;
                        }
                    }
                }
            }

            file.Close();
        }
        #endregion

        #region Shops (.Shp)
        public void LoadShop(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            StreamReader file = new StreamReader(filename);
            // clear the current objects list
            area.shops.Clear();
            char[] buffer = new char[1];
            char lastchar = '\n';
            Shop curr;
            string s = "";

            while (!file.EndOfStream)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] == '#' && lastchar == '\n')
                {
                    s = ReadUntil(file, '\n');
                    curr = new Shop();
                    // vnum
                    curr.vnum = ToInt(s);
                    area.shops.Add(curr);
                    

                    for (int i = constants.shp_objtosell0; i <= constants.shp_objtosell4; i++)
                         curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                    curr.mul_sell = ToDecimal(ReadUntil(file, '\n'));
                    curr.mul_buy = ToDecimal(ReadUntil(file, '\n'));

                    for (int i = constants.shp_objtobuy0; i <= constants.shp_objtobuy4; i++)
                        curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                    for (int i = 0; i <= constants.shp_speech_end; i++)
                    {
                        curr.speech[i] = ReadUntil(file, '~');
                        ReadUntil(file, '\n');
                    }
                    curr.properties[constants.shp_react_indigence] = ToInt(ReadUntil(file, '\n'));
                    curr.properties[constants.shp_react_attack] = ToInt(ReadUntil(file, '\n'));
                    curr.properties[constants.shp_mob] = ToInt(ReadUntil(file, '\n'));

                    ReadUntil(file, '\n'); // che valore e'?

                    curr.properties[constants.shp_room] = ToInt(ReadUntil(file, '\n'));

                    for (int i = constants.shp_open0; i <= constants.shp_close1; i++)
                        curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                    curr.room = area.Get<Room>(curr.properties[constants.shp_room]);
                    curr.mob = area.Get<Mob>(curr.properties[constants.shp_mob]);
                }
            }

            file.Close();
        }
        #endregion

        #endregion

        #region Save

        #region Writes
        private string NewLine
        { get { return "\r"; } }

        private string WriteFlags(BitVector32 bv, int length)
        {
            string s = "";
            bool found = false;
            for (int i = 0; i <= length; i++)
                if (bv[1 << i])
                {
                    string flag = (1 << i).ToString();
                    s = s + (found ? "|" + flag : flag);
                    found = true;
                }
            return s != "" ? s : "0";
        }

        private void write(StreamWriter f, string s)
        {
            f.Write(s.Replace("\r","\r\n").Replace("\n","\r\n").Replace("\r\n", "\r"));
        }
        #endregion

        #region Zone (.Zon)
        public void SaveZon(List<Zone> zones, string filename)
        {
            StreamWriter file = new StreamWriter(filename);

            List<Zone> SortedList = zones.OrderBy( x => x.vnum ).ToList();

            foreach (Zone z in SortedList)
            {

                file.Write("#" + z.vnum + NewLine);
                file.Write(z.shortdesc + "~" + NewLine);

                file.Write(z.vnum_max + " ");
                file.Write(z.repop_interval + " ");

                file.Write(z.flags.Data);

                if (z.limit_xp > 0 || z.limit_gems > 0)
                {
                    file.Write(" " + z.limit_xp);
                    file.Write(" " + z.limit_gems);
                }

                file.Write(NewLine);

                // inits

                file.Write("*! Inizializzazione della zona " + z.vnum + NewLine + NewLine + NewLine);
                file.Write("*! Inizializzazione dei KILL MOBS" + NewLine);
                //kill mobs
                file.Write("*! Fine dell'inizializzazione dei KILL MOBS" + NewLine + NewLine);

                file.Write("*! Inizializzazione dei MOBS" + NewLine);
                //mobs
                file.Write("*! Fine dell'inizializzazione dei MOBS" + NewLine + NewLine);

                file.Write("*! Inizializzazione dei REMOVE OGGETTI" + NewLine);
                // remove oggetti
                file.Write("*! Fine dell'inizializzazione dei REMOVE OGGETTI" + NewLine + NewLine);

                file.Write("*! Inizializzazione degli  OGGETTI" + NewLine);
                // oggetti
                file.Write("*! Fine dell'inizializzazione degli OGGETTI" + NewLine + NewLine);

                file.Write("*! Inizializzazione delle PORTE" + NewLine);
                // porte
                file.Write("*! Fine dell'inizializzazione delle PORTE" + NewLine + NewLine + NewLine);

                file.Write("*! Fine dell'inizializzazione della zona " + z.vnum + NewLine);

           /*
                foreach (Init i in z.inits)
                {
                    file.Write(NewLine);

                    // temporaneo
                    file.Write(i.original_str + NewLine);
                    foreach (Init ch in i.children)
                        file.Write(ch.original_str + NewLine);
                    /*file.Write(constants.it_charset[i.values[constants.iv_type]] + " ");
                    file.Write(i.values[constants.iv_value0] + " ");
                    file.Write(i.values[constants.iv_value1] + " ");
                    file.Write(i.values[constants.iv_value2] + " ");
                    file.Write(i.values[constants.iv_value3] + " " + NewLine + NewLine);
                }*/


                file.Write("S" + NewLine);
            }
            file.Close();
        }
        #endregion

        #region Rooms (.Wld)
        public void SaveWld(List<Room> rooms, string filename)
        {
            if (rooms.Count <= 0)
                return;

            StreamWriter file = new StreamWriter(filename);

            List<Room> SortedList = rooms.OrderBy(x => x.vnum).ToList();

            foreach (Room r in SortedList)
            {
                file.Write("#" + r.vnum + NewLine);
                file.Write(r.shortdesc + "~" + NewLine);
                file.Write(r.longdesc + NewLine + "~" + NewLine);

                file.Write(r.area + " ");

                file.Write(WriteFlags(r.flags, constants.rf_end) + " ");
                file.Write(r.sect == constants.rs_teleport ? -1 : r.sect);

                if (r.sect == constants.rs_teleport)
                {
                    file.Write(" " + r.tel_time);
                    file.Write(" " + r.tel_toroom);
                    file.Write(" " + WriteFlags(r.tel_flags, constants.tf_end));
                    file.Write(" " + r.tel_sect);
                }
                else if (r.sect == constants.rs_waternoswim || r.sect == constants.rs_underwater)
                {
                    file.Write(" " + r.water_current_vel);
                    file.Write(" " + r.water_current_dir);
                }

                if (r.flags[1 << constants.rf_tunnel])
                    file.Write(" " + r.max_pc);

                file.Write(NewLine);

                List<Exit> SortedExits = r.exits.OrderBy(x => x.dir).ToList();

                foreach (Exit e in SortedExits)
                    switch (e.dir)
                    {
                        case constants.dir_special:
                            file.Write("D ");
                            file.Write(e.name + "~");
                            file.Write(e.nameinlist + "~");
                            file.Write(e.str_to + "~");
                            file.Write(e.str_from + "~");
                            file.Write(e.inverse + "~");
                            break;
                        default:
                            file.Write("D" + e.dir + NewLine);
                            file.Write(e.desc + NewLine + "~" + NewLine);
                            file.Write(e.door.keys + "~" + NewLine);
                            file.Write(WriteFlags(e.flags, constants.df_end) + " ");
                            file.Write(e.door.objkey + " ");
                            file.Write(e.roomvnum + " ");
                            file.Write(Door.IndexToCmd(e.door.cmd) + " " + NewLine);
                            break;
                    }
                

                foreach (ExtraDesc ed in r.extras)
                {
                    file.Write("E" + NewLine);
                    file.Write(ed.keys + "~" + NewLine);
                    file.Write(ed.desc + NewLine + "~" + NewLine);
                }

                if (r.daydesc != "" || r.nightdesc != "")
                {
                    file.Write("L" + NewLine);
                    file.Write(r.nightdesc + NewLine + "~" + NewLine);
                    file.Write(r.daydesc + NewLine + "~" + NewLine);
                }

                if (r.flags[1 << constants.rf_saveroom])
                {
                    file.Write("O" + NewLine);
                    file.Write(r.max_obj + NewLine);
                }

                file.Write("S" + NewLine);
            }
            file.Close();
        }
        #endregion

        #region Mobs (.Mob)
        public void SaveMob(List<Mob> mobs, string filename)
        {
            if (mobs.Count <= 0)
                return;

            StreamWriter file = new StreamWriter(filename);

            List<Mob> SortedList = mobs.OrderBy(x => x.vnum).ToList();

            foreach (Mob m in SortedList)
            {
                file.Write("#" + m.vnum + NewLine);
                file.Write(m.keys + "~" + NewLine);
                file.Write(m.shortdesc + "~" + NewLine);
                file.Write(m.longdesc + NewLine + "~" + NewLine);
                file.Write(m.description + NewLine + "~" + NewLine);

                // first numbers line
                file.Write(WriteFlags(m.flags, constants.mf_end) + " ");
                file.Write(WriteFlags(m.affects, constants.ma_end) + " ");
                file.Write(m.values[constants.mv_align] + " ");
                file.Write(Mob.IndexToType(m.values[constants.mv_type]));
                if (m.values[constants.mv_type] == constants.mt_multi_attacks || m.values[constants.mv_type] == constants.mt_sound)
                    file.Write(" " + m.values[constants.mv_attacks]);

                file.Write(NewLine);

                // second numbers line
                file.Write(m.values[constants.mv_level] + " ");
                file.Write(m.values[constants.mv_thac0] + " ");
                file.Write(m.values[constants.mv_ac] + " ");
                file.Write(m.values[constants.mv_hpbonus] + " ");
                file.Write(m.damage + " " + NewLine);

                // third numbers line
                file.Write("-1" + " "); // cos'e'?
                file.Write(m.values[constants.mv_gold] + " ");
                file.Write(m.values[constants.mv_xpbonus] + " ");
                file.Write(m.values[constants.mv_race] + " " + NewLine);

                // fourth numbers line
                file.Write(m.values[constants.mv_loadpos] + " ");
                file.Write(m.values[constants.mv_defaultpos] + " ");
                file.Write(m.values[constants.mv_sex] + " ");
                if (m.resistances.Data > 0 || m.susceptibles.Data > 0 || m.immunities.Data > 0)
                {
                    file.Write(WriteFlags(m.resistances, constants.dt_end) + " ");
                    file.Write(WriteFlags(m.immunities, constants.dt_end) + " ");
                    file.Write(WriteFlags(m.susceptibles, constants.dt_end));
                }

                if (m.values[constants.mv_type] == constants.mt_sound)
                {
                    file.Write(NewLine);
                    file.Write(m.samesound + NewLine + "~" + NewLine);
                    file.Write(m.adjacentsound + NewLine + "~");
                }

                if (m.values[constants.mv_spellpower] > 0)
                {
                    file.Write(NewLine);
                    file.Write("X" + NewLine);
                    file.Write(m.values[constants.mv_spellpower]);
                }

                for (int i = 0; i <= constants.gt_end; i++)
                    if (m.gems[i].percent > 0)
                    {
                        file.Write(NewLine);
                        file.Write("G" + NewLine);
                        file.Write(constants.gt_charset[i] + " " + m.gems[i].percent + " " + m.gems[i].dice);
                    }

                if (m.fame > 0)
                {
                    file.Write(NewLine);
                    file.Write("F" + NewLine);
                    file.Write(m.fame);
                }

                for (int i = 0; i <= constants.et_end; i++)
                    if (m.epic_talents[i] > 0)
                    {
                        file.Write(NewLine);
                        file.Write("E" + NewLine);
                        file.Write(i + " " + m.epic_talents[i]);
                    }

                if (m.values[constants.mv_red_blunt] > 0)
                {
                    file.Write(NewLine);
                    file.Write("R" + NewLine);
                    file.Write('B' + " " + m.values[constants.mv_red_blunt]);
                }
                if (m.values[constants.mv_red_slash] > 0)
                {
                    file.Write(NewLine);
                    file.Write("R" + NewLine);
                    file.Write('S' + " " + m.values[constants.mv_red_slash]);
                }
                if (m.values[constants.mv_red_pierce] > 0)
                {
                    file.Write(NewLine);
                    file.Write("R" + NewLine);
                    file.Write('P' + " " + m.values[constants.mv_red_pierce]);
                }


                file.Write(NewLine);
            }
            file.Close();
        }
        #endregion

        #region Objects (.Obj)
        public void SaveObj(List<Obj> objects, string filename)
        {
            if (objects.Count <= 0)
                return;

            StreamWriter file = new StreamWriter(filename);

            List<Obj> SortedList = objects.OrderBy(x => x.vnum).ToList();

            foreach (Obj o in SortedList)
            {
                file.Write("#" + o.vnum + NewLine);
                file.Write(o.keys + "~" + NewLine);
                file.Write(o.shortdesc + "~" + NewLine);
                file.Write(o.description + "~" + NewLine);
                file.Write(o.actiondesc + NewLine + "~" + NewLine);

                //first numbers line
                file.Write(o.properties[constants.op_type] + " ");
                file.Write(WriteFlags(o.flags, constants.of_end) + " ");
                file.Write(WriteFlags(o.wearpos, constants.ow_end) + NewLine);

                //second numbers line
                for (int i = 0; i <= 3; i++)
                    file.Write(o.values[i] + (i == 3 ? NewLine : " "));

                 // third numbers line
                file.Write(o.properties[constants.op_weight] + " ");
                file.Write(o.properties[constants.op_value] + " ");
                file.Write(o.properties[constants.op_rent] + NewLine);

                foreach (ExtraDesc ed in o.extras)
                {
                    file.Write("E" + NewLine);
                    file.Write(ed.keys + "~" + NewLine);
                    file.Write(ed.desc + NewLine + "~" + NewLine);
                }

                for (int i = 0; i <= 4; i++)
                    if (o.affects[i, 0] > 0)
                    {
                        file.Write("A" + NewLine);
                        file.Write(o.affects[i, 0] + " " + o.affects[i, 1] + NewLine);
                    }
            }

            file.Close();
        }
        #endregion

        #region Shops (.Shp)
        public void SaveShop(List<Shop> shops, string filename)
        {
            if (shops.Count <= 0)
                return;

            StreamWriter file = new StreamWriter(filename);

            List<Shop> SortedList = shops.OrderBy(x => x.vnum).ToList();

            foreach (Shop sh in SortedList)
            {
                file.Write("#" + sh.vnum + NewLine);
                for (int i = constants.shp_objtosell0; i <= constants.shp_objtosell4; i++)
                    file.Write(sh.properties[i] + NewLine);

                file.Write(sh.mul_sell + NewLine);
                file.Write(sh.mul_buy + NewLine);

                for (int i = constants.shp_objtobuy0; i <= constants.shp_objtobuy4; i++)
                    file.Write(sh.properties[i] + NewLine);

                for (int i = 0; i <= constants.shp_speech_end; i++)
                    file.Write(sh.speech[i] + "~" + NewLine);

                file.Write(sh.properties[constants.shp_react_indigence] + NewLine);
                file.Write(sh.properties[constants.shp_react_attack] + NewLine);
                file.Write(sh.properties[constants.shp_mob] + NewLine);

                file.Write(0 + NewLine); // che valore e'?

                file.Write(sh.properties[constants.shp_room] + NewLine);

                for (int i = constants.shp_open0; i <= constants.shp_close1; i++)
                    file.Write(sh.properties[i] + NewLine);
            }

            file.Close();
        }
        #endregion

        #endregion

        #region Options I/O
        #region Load
        public void LoadOptions(Options data)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);

            if (!File.Exists(path + "\\config.cfg"))
                return;

            StreamReader file = new StreamReader(path + "\\config.cfg");
            int dircounter = 0;
            string s = "";

            while (dircounter < 5)
            {
                s = file.ReadLine();
                data.directories[dircounter] = s;
                dircounter++;
            }
            file.ReadToEnd();

            file.Close();
        }
        #endregion

        #region Save
        public void SaveOptions(Options data)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            StreamWriter file = new StreamWriter(path + "\\config.cfg");
            int dircounter = 0;

            while (dircounter < 5)
            {
                file.WriteLine(data.directories[dircounter]);
                dircounter++;
            }

            file.Close();
        }
        #endregion
        #endregion
    }
}
