using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;

namespace HandofGod
{
    public enum IOResult
    { FileNotFound, FileCorrupted, Ok }

    public class FileManager
    {

        #region Parses
        private static decimal ToDecimal(string s)
        {
            decimal result = 0;
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
                return result;
            else
                return 0;
        }

        private static int ToInt(char c)
        { return (int)Char.GetNumericValue(c); }

        private static int ToInt(string s)
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
        private static string ReadUntil(StreamReader file, char pattern)
        {
            char[] buffer = new char[1];
            string s = "";

            while (!file.EndOfStream && buffer[0] != pattern)
            {
                file.Read(buffer, 0, 1);
                if (buffer[0] != pattern)
                    s = s + buffer[0];
            }
            if (buffer[0] != pattern)
                return "";
            else return s.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }

        private static string ReadUntil(StreamReader file, char pattern, char pattern2)
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
        private static string CutAndStoreInitComments(Init init, string s)
        {
            int i = s.IndexOf("*!");
            if (i == -1)
            {
                i = s.IndexOf(";");
                if (i > -1)
                    init.separator = 1;
            }

            if (i > -1)
            {
                init.shortdesc = s.Substring(i + 2, s.Length - 2 - i).TrimStart();
                return s.Substring(0, i);
            }
            else return s;
        }

        public static IOResult LoadZon(List<Zone> zones, string filename, bool inits = false)
        {
            if (!File.Exists(filename))
                return IOResult.FileNotFound;

            Encoding enc = EncodingType.GetType(filename);
            char[] buffer = new char[1];
            string s = "";
            char lastchar = '\n';
            Dictionary<int, int> zone_versions = new Dictionary<int, int>();

            using (StreamReader pre_loading = new StreamReader(filename, enc))
            {
                while (!pre_loading.EndOfStream)
                {
                    pre_loading.Read(buffer, 0, 1);
                    if (buffer[0] == '#' && lastchar == '\n')
                    {
                        s = ReadUntil(pre_loading, '\n');
                        int vnum = ToInt(s);

                        int counter = 0;

                        while (!pre_loading.EndOfStream && pre_loading.Peek() != '#')
                        { 
                            pre_loading.Read(buffer, 0, 1);
                            if (buffer[0] == '~')
                                counter++;
                        }
                        switch (counter)
                        {
                            case 1: zone_versions.Add(vnum, 0); break; // base version
                            case 5: zone_versions.Add(vnum, 1); break;// Helpzon
                           default: break;
                        }
                    }
                    lastchar = buffer[0];
                }
                pre_loading.Close();
            }

            using (StreamReader file = new StreamReader(filename, enc))
            {
                int lowest_vnum = 99999999;
                int highest_vnum = 0;
                Zone curr;
                s = "";
                string[] desc;

                lastchar = '\n';
                while (!file.EndOfStream)
                {
                    //lastchar = buffer[0];
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

                        //curr.filename = Path.GetFileNameWithoutExtension(filename);
                        curr.filename = filename;

                        // vnum
                        curr.vnum = vnum;
                        if (curr.vnum < lowest_vnum)
                            lowest_vnum = curr.vnum;
                        if (curr.vnum > highest_vnum)
                            highest_vnum = curr.vnum;

                        curr.shortdesc = ReadUntil(file, '~');
                        ReadUntil(file, '\n');

                        // zone descriptions
                        if (zone_versions.ContainsKey(vnum))
                        {
                            try
                            {
                                for (int i = 1; i <= C.zone_format_len[zone_versions[vnum]]; i++)
                                {
                                    s = ReadUntil(file, '~');
                                    curr.descriptions[i] = s;
                                    ReadUntil(file, '\n');
                                }
                            }
                            catch
                            {
                                throw new Exception("Formato della zona #" + vnum.ToString() + " (" + filename + ") non corretto.");
                            }
                        }
                        else
                        {
                            zones.Remove(curr);
                            file.Close();
                            return IOResult.FileCorrupted;
                        }

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

                            if (desc.Length > 5)
                            { 
                                curr.map_id = ToInt(desc[5]);
                                curr.map_x = ToInt(desc[6]);
                                curr.map_y = ToInt(desc[7]);
                            }
                        }

                        if (curr.flags[1 << C.zf_if_empty])
                            curr.repop_type = 1;
                        else if (curr.flags[1 << C.zf_always])
                            curr.repop_type = 2;
                        else curr.repop_type = 0;

                        s = "";
                        while (!file.EndOfStream && !(s.Length > 0 && s[0] == 'S'))
                        {
                            s = ReadUntil(file, '\n').Trim();
                            if (inits)
                            {
                                Init init = new Init();

                                desc = Regex.Split(CutAndStoreInitComments(init, s), @"\s+");

                                if (s.Length > 0)
                                {
                                    init.type = Init.CharToIndex(s[0]);
                                    if (desc.Length > 1)
                                        init.values[C.iv_percent] = Init.ValueToPercent(ToInt(desc[1]));
                                    if (desc.Length > 2)
                                        init.values[C.iv_value0] = ToInt(desc[2]);
                                    if (desc.Length > 3)
                                    {
                                        int toint;
                                        bool isNumber = int.TryParse(desc[3], out toint);
                                        if (isNumber)
                                            init.values[C.iv_value1] = toint;
                                        else  // special exits hack
                                        {
                                            init.values[C.iv_value1] = C.dir_special;
                                            init.se_name = desc[3];
                                        }
                                    }
                                    if (desc.Length > 4)
                                        init.values[C.iv_value2] = ToInt(desc[4]);
                                    if (desc.Length > 5)
                                        init.values[C.iv_value3] = ToInt(desc[5]);

                                    curr.inits.Add(init);
                                }
                                else
                                {
                                    init.type = C.it_comment;
                                    curr.inits.Add(init);
                                }
                            }
                        }
                        if (inits)
                            curr.inits.RemoveAt(curr.inits.Count() - 1);
                        lastchar = '\n';
                    }
                    else lastchar = buffer[0];
                }

                file.Close();
            }
            return IOResult.Ok;
        }
        #endregion

        #region Rooms (.Wld)
        public static void LoadWld(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            using (StreamReader file = new StreamReader(filename, EncodingType.GetType(filename)))
            {
                // clear the current rooms list
                area.rooms.Clear();
                char[] buffer = new char[1];
                string[] desc;
                string[] flagsbuffer;
                char lastchar = '\n';
                int lowest_vnum = int.MaxValue;
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
                        curr.zone = ToInt(desc[0]);
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
                            curr.sect = C.rs_teleport; // this will be converted again to -1 in the filesave method
                            try
                            {
                                if (desc.Length > 3)
                                    curr.tel_time = ToInt(desc[3]);
                                if (desc.Length > 4)
                                    curr.tel_toroom = ToInt(desc[4]);
                                if (desc.Length > 5)
                                    flagsbuffer = desc[5].Split('|');
                                foreach (string v in flagsbuffer)
                                {
                                    int flag = ToInt(v);
                                    curr.tel_flags[flag] = true;
                                }
                                if (desc.Length > 7)
                                {
                                    curr.tel_counter = ToInt(desc[6]);
                                    curr.tel_sect = ToInt(desc[7]);
                                }
                                else if (desc.Length > 6)
                                    curr.tel_sect = ToInt(desc[6]);   
                            }
                            catch
                            {

                            }
                        }
                        else if (curr.sect == C.rs_waternoswim || curr.sect == C.rs_underwater)
                        {
                            try
                            {
                                curr.water_current_vel = ToInt(desc[3]);
                                curr.water_current_dir = ToInt(desc[4]);
                            }
                            catch
                            {

                            }
                        }

                        if (curr.flags[1 << C.rf_tunnel])
                            curr.max_pc = ToInt(desc[desc.Length - 1]);

                        while (!file.EndOfStream && buffer[0] != 'S')
                        {
                            file.Read(buffer, 0, 1);
                            if (lastchar == '\n')
                                switch (buffer[0])
                                {
                                    // extra desc
                                    case 'E': 
                                        ExtraDesc ex = new ExtraDesc();
                                        ReadUntil(file, '\n');
                                        ex.keys = ReadUntil(file, '~');
                                        ReadUntil(file, '\n');
                                        ex.desc = ReadUntil(file, '~').TrimEnd();
                                        curr.extras.Add(ex);
                                        break;
                                    // exits
                                    case 'R':
                                    case 'D': 
                                        bool isNewExit = false;

                                        if (buffer[0] == 'R')
                                            isNewExit = true;

                                        int dir = -1;
                                        Exit e = new Exit(curr);
                                        file.Read(buffer, 0, 1);
                                        if (buffer[0] == ' ' || buffer[0] == '\n') // special exit
                                        {
                                            e.name = ReadUntil(file, '~');
                                            e.nameinlist = ReadUntil(file, '~');
                                            e.str_to = ReadUntil(file, '~');
                                            e.str_from = ReadUntil(file, '~');
                                            e.inverse = ReadUntil(file, '~');
                                            dir = C.dir_special;
                                        }
                                        else // normal exit
                                        {
                                            dir = ToInt(buffer[0]);
                                        }

                                        ReadUntil(file, '\n');

                                        e.desc = ReadUntil(file, '~').TrimEnd();
                                        ReadUntil(file, '\n');

                                        // door keys
                                        e.door.keys = ReadUntil(file, '~');
                                        ReadUntil(file, '\n');

                                        if (isNewExit)
                                        {
                                            e.doorDescription = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');
                                            e.openToChar = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');
                                            e.openToRoom = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');
                                            e.closeToChar = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');
                                            e.closeToRoom = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');

                                            s = ReadUntil(file, '\n');
                                            string[] skillDifficulties = s.Split(' ');

                                            e.pickDiff = ToInt(skillDifficulties[0]);
                                            e.bashDiff = ToInt(skillDifficulties[1]);
                                            e.knockDiff = ToInt(skillDifficulties[2]);
                                            e.climbDiff = ToInt(skillDifficulties[3]);
                                            e.percDiff = ToInt(skillDifficulties[4]);

                                            s = ReadUntil(file, '\n');
                                            string[] statDifficulties = s.Split(' ');

                                            e.strDiff = ToInt(statDifficulties[0]);
                                            e.dexDiff = ToInt(statDifficulties[1]);
                                            e.intDiff = ToInt(statDifficulties[2]);
                                            e.wisDiff = ToInt(statDifficulties[3]);
                                            e.conDiff = ToInt(statDifficulties[4]);
                                            e.chrDiff = ToInt(statDifficulties[5]);
                                        }
                                        
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
                                        // door command
                                        int cmd = ToInt(exit_vars[3]);
                                        e.door.cmd = Door.CmdToIndex(cmd);

                                        if (roomvnum > -1 && dir > -1)
                                            curr.SetExit(to, roomvnum, dir, false, e);

                                        break;
                                    // day & night desc
                                    case 'L':
                                        ReadUntil(file, '\n');
                                        curr.nightdesc = ReadUntil(file, '~').TrimEnd();
                                        ReadUntil(file, '\n');
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
        }
        #endregion

        #region Mobs (.Mob)
        public static void LoadMob(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            using (StreamReader file = new StreamReader(filename, EncodingType.GetType(filename)))
            {
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
                        curr.values[C.mv_align] = ToInt(desc[2]);
                        curr.values[C.mv_type] = Mob.TypeToIndex(desc[3]);
                        //if (curr.values[C.mv_type] == C.mt_multi_attacks || curr.values[C.mv_type] == C.mt_sound || curr.values[C.mv_type] == C.mt_unbashable)
                        if (desc.Length > 4)
                            curr.values[C.mv_attacks] = ToInt(desc[4]);

                        // second numbers line
                        desc = ReadUntil(file, '\n').Split(' ');
                        curr.values[C.mv_level] = ToInt(desc[0]);
                        curr.values[C.mv_thac0] = ToInt(desc[1]);
                        curr.values[C.mv_ac] = ToInt(desc[2]);
                        curr.values[C.mv_hpbonus] = ToInt(desc[3]);
                        curr.damage = desc[4].TrimEnd();

                        // third numbers line
                        desc = ReadUntil(file, '\n').Split(' ');
                        // cos'e' desc[0] ?
                        curr.values[C.mv_gold] = ToInt(desc[1]);
                        curr.values[C.mv_xpbonus] = ToInt(desc[2]);
                        curr.values[C.mv_race] = ToInt(desc[3]);

                        // fourth numbers line
                        desc = ReadUntil(file, '\n').Split(' ');
                        curr.values[C.mv_loadpos] = ToInt(desc[0]);
                        curr.values[C.mv_defaultpos] = ToInt(desc[1]);
                        //
                        if (ToInt(desc[2]) > 2) // da risolvere questione gender > 2
                            curr.values[C.mv_sex] = ToInt(desc[2]) - 3;
                        else
                            curr.values[C.mv_sex] = ToInt(desc[2]);
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
                        if (curr.values[C.mv_type] == C.mt_sound)
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
                                    curr.values[C.mv_spellpower] = ToInt(ReadUntil(file, '\n'));
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
                                            int index = C.gt_charset.IndexOf(buffer[0]);
                                            s = ReadUntil(file, ' ');
                                            curr.gems[index].percent = ToInt(s);
                                            curr.gems[index].dice = ReadUntil(file, '\n').TrimEnd();
                                            break;
                                    }
                                    break;
                                case 'R': ReadUntil(file, '\n');
                                    file.Read(buffer, 0, 1);
                                    switch (buffer[0])
                                    {
                                        case 'B':
                                            file.Read();
                                            curr.values[C.mv_red_blunt] = ToInt(ReadUntil(file, '\n'));
                                            break;
                                        case 'S':
                                            file.Read();
                                            curr.values[C.mv_red_slash] = ToInt(ReadUntil(file, '\n'));
                                            break;
                                        case 'P':
                                            file.Read();
                                            curr.values[C.mv_red_pierce] = ToInt(ReadUntil(file, '\n'));
                                            break;
                                    }
                                    break;
                                case 'E':
                                    ReadUntil(file, '\n');
                                    desc = ReadUntil(file, '\n').Split(' ');
                                    curr.epic_talents[ToInt(desc[0])] = ToInt(desc[1]);
                                    break;
                                case 'Q': // mob dialogs v1.1.6
                                case 'A':
                                    MobDialogue d = new MobDialogue(null);

                                    if (buffer[0] == 'A')
                                        d.type = MobDialogueType.Risposta;

                                    ReadUntil(file, '\n');
                                    desc = ReadUntil(file, '\n').Split(' ');

                                    try
                                    {
                                        // number of additional rows
                                        int num_check = 0;
                                        int num_roll = 0;
                                        int num_onSuccess = 0;
                                        int num_onFail = 0;

                                        d.vnum = ToInt(desc[0]);
                                        d.next = ToInt(desc[1]);
                                        num_check = ToInt(desc[2]);
                                        num_roll = ToInt(desc[3]);
                                        num_onSuccess = ToInt(desc[4]);
                                        num_onFail = ToInt(desc[5]);

                                        string[] data;
                                        int stat;

                                        for (int i = 0; i < num_check; i++)
                                        {
                                            data = ReadUntil(file, '\n').Split(' ');
                                            stat = C.ms_to_char.IndexOf(data[0][0]);
                                            MDCheckData check = new MDCheckData(null);
                                            check.amount = ToInt(data[1]);
                                            d.checkData.Add(stat, check);
                                        }

                                        for (int i = 0; i < num_roll; i++)
                                        {
                                            data = ReadUntil(file, '\n').Split(' ');
                                            stat = C.ms_to_char.IndexOf(data[0][0]);
                                            MDRollData roll = new MDRollData(null);
                                            roll.amount = ToInt(data[1]);
                                            roll.fail_next = ToInt(data[2]);
                                            d.rollData.Add(stat, roll);
                                        }

                                        if (num_onSuccess > 0)
                                        {
                                            data = ReadUntil(file, '\n').Split(' ');
                                            d.onSuccess.vnum = ToInt(data[0]);
                                            if (data[1] != null)
                                                d.onSuccess.param1 = data[1];
                                            if (data[2] != null)
                                                d.onSuccess.param2 = data[2];
                                        }

                                        if (num_onFail > 0)
                                        {
                                            data = ReadUntil(file, '\n').Split(' ');
                                            d.onFail.vnum = ToInt(data[0]);
                                            if (data[1] != null)
                                                d.onFail.param1 = data[1];
                                            if (data[2] != null)
                                                d.onFail.param2 = data[2];
                                        }

                                        /* //multiple functions ready to go
                                        for (int i = 1; i <= num_onSuccess + num_onFail; i++)
                                        {
                                            data = ReadUntil(file, '\n').Split(' ');
                                            MDFuncData func = new MDFuncData(null);
                                            func.id = ToInt(data[0]);
                                            if (data[1] != null)
                                                func.param1 = data[1];
                                            if (data[2] != null)
                                                func.param2 = data[2];

                                            if (i <= num_onSuccess)
                                                d.onSuccess = func;
                                            else d.onFail = func;
                                        }*/

                                        for (int i = 0; i <= C.mdd_end; i++)
                                        {
                                            d.descriptions[i] = ReadUntil(file, '~').TrimEnd();
                                            ReadUntil(file, '\n');
                                        }
                                    }
                                    catch { MessageBox.Show("Errore nel caricamento dei valori nei dialoghi del mob " + curr.shortdesc + "."); }

                                    curr.dialogues.Add(d);
                                    break;
                            }
                        }
                    }
                                        
                }

                file.Close();
            }
        }
        #endregion

        #region Objects (.Obj)
        public static void LoadObj(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            using (StreamReader file = new StreamReader(filename, EncodingType.GetType(filename)))
            {
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
                        curr.properties[C.op_type] = ToInt(desc[0]);

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
                        curr.properties[C.op_weight] = ToInt(desc[0]);
                        curr.properties[C.op_value] = ToInt(desc[1]);
                        curr.properties[C.op_rent] = ToInt(desc[2]);

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
                                case 'A':
                                    try
                                    {
                                        ReadUntil(file, '\n');
                                        desc = ReadUntil(file, '\n').Split(' ');
                                        int key = ToInt(desc[0]);
                                        int val = ToInt(desc[1]);

                                        // affects > 69 hack
                                        //if (key > 69)
                                        //    key -= 1;

                                        curr.SetAffect(key, val);
                                    }
                                    catch { }
                                    break;
                              // complex affect
                                case 'S':
                                    try
                                    {
                                        ReadUntil(file, '\n');
                                        desc = ReadUntil(file, '\n').Split(' ');
                                        int key = ToInt(desc[0]);
                                        int val1 = ToInt(desc[1]);
                                        int val2 = ToInt(desc[2]);
                                        int val3 = ToInt(desc[3]);
                                        int val4 = ToInt(desc[4]);

                                        curr.SetAffect(key, val1, val2, val3, val4);
                                    }
                                    catch { }
                                    break;
                            }
                        }
                    }
                }

                file.Close();
            }
        }
        #endregion

        #region Shops (.Shp)
        public static void LoadShop(Area area, string filename)
        {
            if (!File.Exists(filename))
                return;

            using (StreamReader file = new StreamReader(filename, EncodingType.GetType(filename)))
            {
                // clear the current objects list
                area.shops.Clear();
                char[] buffer = new char[1];
                string[] desc;
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

                        for (int i = C.shp_objtosell0; i <= C.shp_objtosell4; i++)
                            curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                        curr.mul_sell = ToDecimal(ReadUntil(file, '\n'));
                        curr.mul_buy = ToDecimal(ReadUntil(file, '\n'));

                        // hotfix, bad parses in older versions
                        while (curr.mul_sell >= 4)
                            curr.mul_sell /= 10;
                        while (curr.mul_buy >= 4)
                            curr.mul_buy /= 10;

                        for (int i = C.shp_objtobuy0; i <= C.shp_objtobuy4; i++)
                            curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                        for (int i = 0; i <= C.shp_speech_end; i++)
                        {
                            curr.speech[i] = ReadUntil(file, '~');
                            ReadUntil(file, '\n');
                        }
                        curr.properties[C.shp_react_indigence] = ToInt(ReadUntil(file, '\n'));
                        curr.properties[C.shp_react_attack] = ToInt(ReadUntil(file, '\n'));
                        curr.properties[C.shp_mob] = ToInt(ReadUntil(file, '\n'));

                        ReadUntil(file, '\n'); // che valore e'?

                        curr.properties[C.shp_room] = ToInt(ReadUntil(file, '\n'));

                        for (int i = C.shp_open0; i <= C.shp_close1; i++)
                            curr.properties[i] = ToInt(ReadUntil(file, '\n'));

                        while (!file.EndOfStream && file.Peek() != '#')
                        {
                            file.Read(buffer, 0, 1);
                            switch (buffer[0])
                            {
                                case 'I':
                                    try
                                    {
                                        ReadUntil(file, '\n');
                                        desc = ReadUntil(file, '\n').Split(' ');
                                        int iVNum = ToInt(desc[0]);
                                        string iStock = desc[1];

                                        SoldItem si = new SoldItem();
                                        si.vnum = iVNum;
                                        si.shortdesc = iStock;
                                        curr.soldItemList.Add(si);
                                    }
                                    catch { }
                                    break;
                            }
                        }
                    }

                }

                file.Close();
            }
        }
        #endregion

        #region HoG Visual Properties (.Hog)
        public static void LoadHog(Area area, string filename)
        {
            if (!File.Exists(filename))
            {
                area.SetVisuals();
                return;
            }

            using (StreamReader file = new StreamReader(filename, EncodingType.GetType(filename)))
            {
                // clear the current mobs list
                foreach (Room room in area.rooms)
                    room.visual.Clear();

                char[] buffer = new char[1];
                string[] desc;
                char lastchar = '\n';
                Room r = null;
                Obj o = null;
                Zone z = null;
                string s = "";

                int e_index = C.i_room;

                while (!file.EndOfStream)
                {
                    file.Read(buffer, 0, 1);

                    if (buffer[0] == '@' && lastchar == '\n')
                    {
                        s = ReadUntil(file, '\n');
                        if (s == "Objects")
                            e_index = C.i_obj;
                        else if (s == "Zones")
                            e_index = C.i_zone;
                    }

                    if (buffer[0] == '#' && lastchar == '\n')
                        switch (e_index)
                        {
                            case C.i_room:
                                s = ReadUntil(file, '\n');
                                r = area.Get<Room>(ToInt(s));

                                if (r == null)
                                    continue;

                                desc = ReadUntil(file, '\n').Split(' ');
                                r.visual.rect.X = ToInt(desc[0]);
                                r.visual.rect.Y = ToInt(desc[1]);
                                r.visual.rect.Width = ToInt(desc[2]);
                                r.visual.rect.Height = ToInt(desc[3]);

                                desc = ReadUntil(file, '\n').Split(' ');
                                r.visual.floor = ToInt(desc[0]);
                                r.visual.visible = ToInt(desc[1]) == 0 ? false : true;

                                break;
                            case C.i_obj:
                                s = ReadUntil(file, '\n');
                                o = area.Get<Obj>(ToInt(s));

                                if (r == null)
                                    continue;

                                desc = ReadUntil(file, '\n').Split(' ');
                                if (desc[0] == "1")
                                    o.HTMLExportable = true;
                                break;
                            case C.i_zone:
                                s = ReadUntil(file, '\n');
                                z = area.Get <Zone>(ToInt(s));

                                if (z == null)
                                    continue;

                                desc = ReadUntil(file, '\n').Split(' ');
                                if (desc[0] == "1")
                                    z.hogdata_manual_vunm_limits = true;
                                break;
                        }
                }

                file.Close();
            }
        }
        #endregion

        #endregion

        #region Save

        #region Encoding
        private static Encoding GetEncoding()
        {
            switch (Options.data.file_format)
            {
                case 1: return new UTF8Encoding(false);
                default: return Encoding.GetEncoding(1252); // ANSI
            }
        }
        #endregion

        #region Writes
        private static string NewLine
        { get { return "\n"; } }

        private static string WriteFlags(BitVector32 bv, int length)
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

        private static void write(StreamWriter f, int i)
        {
            f.Write(i.ToString().Replace("\r", ""));
        }

        private static void write(StreamWriter f, string s)
        {
            f.Write(s.Replace("\r", ""));
        }
        #endregion

        #region Zone (.Zon)
        #region Write Inits
        private static void WriteInits(StreamWriter f, List<Init> list)
        {
            if (list == null || list.Count <= 0)
                return;

            foreach (Init i in list)
            {
                if (i.values[C.iv_type] == C.it_comment)
                {
                    if (i.shortdesc != "")
                        write(f, L.separators[i.separator] + " " + i.shortdesc + NewLine);
                    else write(f, " " + NewLine);
                    continue;
                }

                write(f, Init.IndexToChar(i.values[C.iv_type]) + " ");
                write(f, Init.PercentToValue(i, i.values[C.iv_percent]) + " ");
                int numvalues = i.values[C.iv_type] == C.it_obj_add ? 3 : 2;
                for (int j = 0; j <= numvalues; j++)
                {
                    if (i.values[C.iv_type] == C.it_door_init && j == 1 && i.values[j] == C.dir_special)
                    {
                        write(f, i.se_name + " ");
                    } else
                    write(f, i.values[j] + " ");
                }
                write(f, L.separators[i.separator] + " " + i.shortdesc + NewLine);
            }
        }
        #endregion

        // Archie - usare limit_xp come punto di riferimento per coordinate nella zona
        // Provare e verificare che la linea salvata sia la seguente:
        // (su Scratch.zon : 99 1 0 5 10 0 300 300 (map x y)
        
        public static void SaveZon(List<Zone> zones, string filename)
        {
            if (zones.Count <= 0)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {
                List<Zone> SortedList = zones.OrderBy(x => x.vnum).ToList();

                foreach (Zone z in SortedList)
                {
                    write(file, "#" + z.vnum + NewLine);
                    write(file, z.shortdesc + "~" + NewLine);

                    if (Options.data.format_helpzon)
                    {
                        for (int i = 1; i <= C.zd_end; i++)
                            write(file, z.descriptions[i] + "~" + NewLine);
                    }

                    write(file, z.vnum_max + " ");
                    write(file, z.repop_interval + " ");

                    write(file, z.flags.Data);

                   // Li scrivo sempre, perche' non dovrei scriverli?
                   // if (z.limit_xp > 0 || z.limit_gems > 0)
                   // {
                        write(file, " " + z.limit_xp);
                        write(file, " " + z.limit_gems);
                        write(file, " " + z.map_id);
                        write(file, " " + z.map_x);
                        write(file, " " + z.map_y);
                   // }

                    write(file, NewLine);

                    WriteInits(file, z.inits);

                    write(file, "S" + NewLine);
                }
                file.Close();
            }
        }
        #endregion

        #region Rooms (.Wld)
        public static void SaveWld(List<Room> rooms, string filename)
        {
            if (rooms.Count <= 0)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {
                List<Room> SortedList = rooms.OrderBy(x => x.vnum).ToList();

                foreach (Room r in SortedList)
                {
                    write(file, "#" + r.vnum + NewLine);
                    write(file, r.shortdesc + "~" + NewLine);
                    write(file, r.longdesc + NewLine + "~" + NewLine);

                    write(file, r.zone + " ");

                    write(file, WriteFlags(r.flags, C.rf_end) + " ");
                    write(file, r.sect == C.rs_teleport ? -1 : r.sect);

                    if (r.sect == C.rs_teleport)
                    {
                        write(file, " " + r.tel_time);
                        write(file, " " + r.tel_toroom);
                        write(file, " " + WriteFlags(r.tel_flags, C.tf_end));
                        if (r.tel_flags[1 << C.tf_count])
                            write(file, " " + r.tel_counter);
                        write(file, " " + r.tel_sect);
                    }
                    else if (r.sect == C.rs_waternoswim || r.sect == C.rs_underwater)
                    {
                        write(file, " " + r.water_current_vel);
                        write(file, " " + r.water_current_dir);
                    }

                    if (r.flags[1 << C.rf_tunnel])
                        write(file, " " + r.max_pc);

                    write(file, NewLine);

                    List<Exit> SortedExits = r.exits.OrderBy(x => x.dir).ToList();

                    foreach (Exit e in SortedExits)
                    {
                        switch (e.dir)
                        {
                            case C.dir_special:
                                write(file, "R ");
                                write(file, e.name + "~");
                                write(file, e.nameinlist + "~");
                                write(file, e.str_to + "~");
                                write(file, e.str_from + "~");
                                write(file, e.inverse + "~" + NewLine);
                                write(file, e.desc + NewLine + "~" + NewLine);
                                write(file, e.door.keys + "~" + NewLine);
                                write(file, e.doorDescription + "~" + NewLine);
                                write(file, e.openToChar + "~" + NewLine);
                                write(file, e.openToRoom + "~" + NewLine);
                                write(file, e.closeToChar + "~" + NewLine);
                                write(file, e.closeToRoom + "~" + NewLine);
                                // Skill Diffs line
                                write(file, e.pickDiff + " ");
                                write(file, e.bashDiff + " ");
                                write(file, e.knockDiff + " ");
                                write(file, e.climbDiff + " ");
                                write(file, e.percDiff + " " + NewLine);
                                // Stat Diffs line
                                write(file, e.strDiff + " ");
                                write(file, e.dexDiff + " ");
                                write(file, e.intDiff + " ");
                                write(file, e.wisDiff + " ");
                                write(file, e.conDiff + " ");
                                write(file, e.chrDiff + " " + NewLine);
                                write(file, WriteFlags(e.flags, C.df_end) + " ");
                                write(file, e.door.objkey + " ");
                                write(file, e.room + " ");
                                write(file, Door.IndexToCmd(e.door.cmd) + " " + NewLine);
                                break;
                            default:
                                write(file, "R" + e.dir + NewLine);
                                write(file, e.desc + NewLine + "~" + NewLine);
                                write(file, e.door.keys + "~" + NewLine);
                                write(file, e.doorDescription + "~" + NewLine);
                                write(file, e.openToChar + "~" + NewLine);
                                write(file, e.openToRoom + "~" + NewLine);
                                write(file, e.closeToChar + "~" + NewLine);
                                write(file, e.closeToRoom + "~" + NewLine);
                                // Skill Diffs line
                                write(file, e.pickDiff + " ");
                                write(file, e.bashDiff + " ");
                                write(file, e.knockDiff + " ");
                                write(file, e.climbDiff + " ");
                                write(file, e.percDiff + " " + NewLine);
                                // Stat Diffs line
                                write(file, e.strDiff + " ");
                                write(file, e.dexDiff + " ");
                                write(file, e.intDiff + " ");
                                write(file, e.wisDiff + " ");
                                write(file, e.conDiff + " ");
                                write(file, e.chrDiff + " " + NewLine);
                                write(file, WriteFlags(e.flags, C.df_end) + " ");
                                write(file, e.door.objkey + " ");
                                write(file, e.room + " ");
                                write(file, Door.IndexToCmd(e.door.cmd) + " " + NewLine);
                                break;
                        }
                    }


                    foreach (ExtraDesc ed in r.extras)
                    {
                        write(file, "E" + NewLine);
                        write(file, ed.keys + "~" + NewLine);
                        write(file, ed.desc + NewLine + "~" + NewLine);
                    }

                    if (r.daydesc != "" || r.nightdesc != "")
                    {
                        write(file, "L" + NewLine);
                        write(file, r.nightdesc + NewLine + "~" + NewLine);
                        write(file, r.daydesc + NewLine + "~" + NewLine);
                    }

                    if (r.flags[1 << C.rf_saveroom])
                    {
                        write(file, "O" + NewLine);
                        write(file, r.max_obj + NewLine);
                    }

                    write(file, "S" + NewLine);
                }
                file.Close();
            }
        }
        #endregion

        #region Mobs (.Mob)
        public static void SaveMob(List<Mob> mobs, string filename)
        {
            if (mobs.Count <= 0)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {

                List<Mob> SortedList = mobs.OrderBy(x => x.vnum).ToList();

                foreach (Mob m in SortedList)
                {
                    write(file, "#" + m.vnum + NewLine);
                    write(file, m.keys + "~" + NewLine);
                    write(file, m.shortdesc + "~" + NewLine);
                    write(file, m.longdesc + NewLine + "~" + NewLine);
                    write(file, m.description + NewLine + "~" + NewLine);

                    // first numbers line
                    write(file, WriteFlags(m.flags, C.mf_end) + " ");
                    write(file, WriteFlags(m.affects, C.ma_end) + " ");
                    write(file, m.values[C.mv_align] + " ");
                    write(file, Mob.IndexToType(m.values[C.mv_type]));
                    if (m.values[C.mv_type] == C.mt_multi_attacks || m.values[C.mv_type] == C.mt_sound || m.values[C.mv_type] == C.mt_unbashable)
                        write(file, " " + m.values[C.mv_attacks]);

                    write(file, NewLine);

                    // second numbers line
                    write(file, m.values[C.mv_level] + " ");
                    write(file, m.values[C.mv_thac0] + " ");
                    write(file, m.values[C.mv_ac] + " ");
                    write(file, m.values[C.mv_hpbonus] + " ");
                    write(file, m.damage + NewLine);

                    // third numbers line
                    write(file, "-1" + " "); // cos'e'?
                    write(file, m.values[C.mv_gold] + " ");
                    write(file, m.values[C.mv_xpbonus] + " ");
                    write(file, m.values[C.mv_race] + NewLine);

                    // fourth numbers line
                    write(file, m.values[C.mv_loadpos] + " ");
                    write(file, m.values[C.mv_defaultpos] + " ");

                    if (m.resistances.Data > 0 || m.susceptibles.Data > 0 || m.immunities.Data > 0)
                    {
                        write(file, m.values[C.mv_sex] + 3);
                        write(file, " " + WriteFlags(m.resistances, C.dt_end) + " ");
                        write(file, WriteFlags(m.immunities, C.dt_end) + " ");
                        write(file, WriteFlags(m.susceptibles, C.dt_end));
                    }
                    else write(file, m.values[C.mv_sex]);

                    if (m.values[C.mv_type] == C.mt_sound)
                    {
                        write(file, NewLine);
                        write(file, m.samesound + NewLine + "~" + NewLine);
                        write(file, m.adjacentsound + NewLine + "~");
                    }

                    if (m.values[C.mv_spellpower] > 0)
                    {
                        write(file, NewLine);
                        write(file, "X" + NewLine);
                        write(file, m.values[C.mv_spellpower]);
                    }

                    for (int i = 0; i <= C.gt_end; i++)
                        if (m.gems[i].percent > 0)
                        {
                            write(file, NewLine);
                            write(file, "G" + NewLine);
                            write(file, C.gt_charset[i] + " " + m.gems[i].percent + " " + m.gems[i].dice);
                        }

                    if (m.fame > 0)
                    {
                        write(file, NewLine);
                        write(file, "F" + NewLine);
                        write(file, m.fame);
                    }

                    for (int i = 1; i <= C.et_end; i++)
                        if (m.epic_talents[i] > 0)
                        {
                            write(file, NewLine);
                            write(file, "E" + NewLine);
                            write(file, i + " " + m.epic_talents[i]);
                        }

                    if (m.values[C.mv_red_blunt] > 0)
                    {
                        write(file, NewLine);
                        write(file, "R" + NewLine);
                        write(file, 'B' + " " + m.values[C.mv_red_blunt]);
                    }
                    if (m.values[C.mv_red_slash] > 0)
                    {
                        write(file, NewLine);
                        write(file, "R" + NewLine);
                        write(file, 'S' + " " + m.values[C.mv_red_slash]);
                    }
                    if (m.values[C.mv_red_pierce] > 0)
                    {
                        write(file, NewLine);
                        write(file, "R" + NewLine);
                        write(file, 'P' + " " + m.values[C.mv_red_pierce]);
                    }

                    write(file, NewLine);

                    foreach (MobDialogue md in m.dialogues)
                    {
                        int chkcount = md.checkData.Count();
                        int rollcount = md.rollData.Count();
                        int hasSuccess = md.onSuccess.vnum > -1 ? 1 : 0;
                        int hasFail = md.onFail.vnum > -1 ? 1 : 0;
                        write(file, (md.type == MobDialogueType.Domanda ? "Q" : "A") + NewLine);
                        write(file, md.vnum + " " + md.next + " " + chkcount + " " + rollcount +
                                    " " + hasSuccess + " " + hasFail + NewLine);

                        foreach (KeyValuePair<int, MDCheckData> chk in md.checkData)
                            write(file, C.ms_to_char[chk.Key] + " " + chk.Value.amount + NewLine);

                        foreach (KeyValuePair<int, MDRollData> chk in md.rollData)
                            write(file, C.ms_to_char[chk.Key] + " " + chk.Value.amount + " " + chk.Value.fail_next + NewLine);

                        if (hasSuccess > 0)
                        {
                            string paramOneToSave = md.onSuccess.param1;
                            string paramTwoToSave = md.onSuccess.param2;

                            if (string.IsNullOrEmpty(paramOneToSave))
                                paramOneToSave = "0";

                            if (string.IsNullOrEmpty(paramTwoToSave))
                                paramTwoToSave = "0";

                            write(file, md.onSuccess.vnum + " " + paramOneToSave + " " + paramTwoToSave + NewLine);
                        }

                        if (hasFail > 0)
                        {
                            string paramOneToSave = md.onFail.param1;
                            string paramTwoToSave = md.onFail.param2;

                            if (string.IsNullOrEmpty(paramOneToSave))
                                paramOneToSave = "0";

                            if (string.IsNullOrEmpty(paramTwoToSave))
                                paramTwoToSave = "0";

                            write(file, md.onFail.vnum + " " + paramOneToSave + " " + paramTwoToSave + NewLine);
                        }

                        for (int i = 0; i <= C.mdd_end; i++)
                            write(file, md.descriptions[i] + "~" + NewLine);
                    }
                }
                file.Close();
            }
        }
        #endregion

        #region Objects (.Obj)
        public static void SaveObj(List<Obj> objects, string filename)
        {
            if (objects.Count <= 0)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {

                List<Obj> SortedList = objects.OrderBy(x => x.vnum).ToList();

                foreach (Obj o in SortedList)
                {
                    write(file, "#" + o.vnum + NewLine);
                    write(file, o.keys + "~" + NewLine);
                    write(file, o.shortdesc + "~" + NewLine);
                    write(file, o.description + "~" + NewLine);
                    write(file, o.actiondesc + NewLine + "~" + NewLine);

                    //first numbers line
                    write(file, o.properties[C.op_type] + " ");
                    write(file, WriteFlags(o.flags, C.of_end) + " ");
                    write(file, WriteFlags(o.wearpos, C.ow_end) + NewLine);

                    //second numbers line
                    for (int i = 0; i <= 3; i++)
                        write(file, o.values[i] + (i == 3 ? NewLine : " "));

                    // third numbers line
                    write(file, o.properties[C.op_weight] + " ");
                    write(file, o.properties[C.op_value] + " ");
                    write(file, o.properties[C.op_rent] + NewLine);

                    foreach (ExtraDesc ed in o.extras)
                    {
                        write(file, "E" + NewLine);
                        write(file, ed.keys + "~" + NewLine);
                        write(file, ed.desc + NewLine + "~" + NewLine);
                    }

                    for (int i = 0; i <= 4; i++)
                        if (o.affects[i].index > 0)
                            if (!ObjAffect.isComplex(o.affects[i].index))
                            {
                                write(file, "A" + NewLine);
                                // spell focus hack
                                int key = o.affects[i].index; //+ (o.affects[i].index >= 69 ? 1 : 0);
                                write(file, key + " " + o.affects[i].value + NewLine);
                            }
                    else // 17/10/21 Saregon - affects
                            {
                                write(file, "S" + NewLine);
                                // spell focus hack
                                int key = o.affects[i].index; //+ (o.affects[i].index >= 69 ? 1 : 0);
                                write(file, key + " " + o.affects[i].value + " " + o.affects[i].value2 + " " + o.affects[i].value3 + " " + o.affects[i].value4 + NewLine);
                            }
                        //
                }

                file.Close();
            }
        }
        #endregion

        #region Shops (.Shp)
        public static void SaveShop(List<Shop> shops, string filename)
        {
            if (shops.Count <= 0)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {
                List<Shop> SortedList = shops.OrderBy(x => x.vnum).ToList();

                foreach (Shop sh in SortedList)
                {
                    write(file, "#" + sh.vnum + NewLine);
                    for (int i = C.shp_objtosell0; i <= C.shp_objtosell4; i++)
                        write(file, sh.properties[i] + NewLine);

                    write(file, sh.mul_sell.ToString().Replace(",", ".") + NewLine);
                    write(file, sh.mul_buy.ToString().Replace(",", ".") + NewLine);
                    //write(file,  sh.mul_sell + NewLine);
                    //write(file,  sh.mul_buy + NewLine);

                    for (int i = C.shp_objtobuy0; i <= C.shp_objtobuy4; i++)
                        write(file, sh.properties[i] + NewLine);

                    for (int i = 0; i <= C.shp_speech_end; i++)
                        write(file, sh.speech[i] + "~" + NewLine);

                    write(file, sh.properties[C.shp_react_indigence] + NewLine);
                    write(file, sh.properties[C.shp_react_attack] + NewLine);
                    write(file, sh.properties[C.shp_mob] + NewLine);

                    write(file, 0 + NewLine); // che valore e'?

                    write(file, sh.properties[C.shp_room] + NewLine);

                    for (int i = C.shp_open0; i <= C.shp_close1; i++)
                        write(file, sh.properties[i] + NewLine);

                    foreach (SoldItem si in sh.soldItemList)
                    {
                        write(file, "I" + NewLine);
                        write(file, si.vnum.ToString() + " " + si.shortdesc + NewLine);
                    }
                }

                file.Close();
            }
        }
        #endregion

        #region HoG Visual Properties (.Hog)
        public static void SaveHog(Area area, string filename)
        {
            if (area.rooms.Count <= 0 || Options.data.dontsave_hogfile)
                return;

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {

                List<Room> SortedRooms = area.rooms.OrderBy(x => x.vnum).ToList();

                foreach (Room r in SortedRooms)
                {
                    write(file, "#" + r.vnum + NewLine);
                    write(file, r.visual.rect.X + " " + r.visual.rect.Y + " " + r.visual.rect.Width + " " + r.visual.rect.Height + NewLine);
                    write(file, r.visual.floor + " " + (r.visual.visible ? 1 : 0) + NewLine);
                }

                write(file, NewLine + "@Objects" + NewLine);
                List<Obj> SortedObjs = area.objects.OrderBy(x => x.vnum).ToList();

                foreach (Obj o in SortedObjs)
                    if (o.HTMLExportable)
                    {
                        write(file, "#" + o.vnum + NewLine);
                        write(file, "1" + NewLine);
                    }

                write(file, NewLine + "@Zones" + NewLine);
                List<Zone> SortedZones = area.zones.OrderBy(x => x.vnum).ToList();

                foreach (Zone z in SortedZones)
                    if (z.hogdata_manual_vunm_limits)
                    {
                        write(file, "#" + z.vnum + NewLine);
                        write(file, "1" + NewLine);
                    }

                file.Close();
            }
        }
        #endregion

        #endregion

        #region HTML Export

        public static void ObjToHTML(Area ParentArea, string filename, List<bool> exportable)
        {
             if (ParentArea.objects.Count <= 0)
                return;

            string nl = Environment.NewLine;
            bool first = true;
            string s = "";

            using (StreamWriter file = new StreamWriter(filename, false, GetEncoding()))
            {
                List<Obj> SortedList = ParentArea.objects.OrderBy(x => x.vnum).ToList();

                file.Write("<html>" + nl + "<head>" + nl + "<link rel=\"stylesheet\" type=\"text/css\" href=\"armory.css\">" + nl);
                file.Write("<title>" + (ParentArea.zones.Count > 0 ? ParentArea.zones[0].shortdesc : "") + "</title>" + nl + "</head>" + nl + nl + "<body style=\"background-color:#444444\">" + nl + "<div id=\"page-wrap\">" + nl + nl);
                if (ParentArea.zones.Count > 0)
                    file.Write(" <div id=\"title\">" + ParentArea.zones[0].shortdesc + "</div>" + nl);
                file.Write("<div id=\"titlelink\">" + nl + "<a href=\"http://www.leu.it\">Torna al sito</a>" + nl);
                file.Write("<a href=\"http://leu.uaiz.it/wp/armoryhome/index.html\">Torna alla Pagina Principale dell'Armory</a>" + nl + "</div>" + nl + nl);
                file.Write("<div id=\"popup\">" + nl + nl);

                int counter = 0;
                foreach (Obj o in SortedList)
                {
                    if (exportable[counter])
                    {
                        string sdesc = "";
                        foreach (colorindexer ci in utils.ProcessTextColorCodes(o.shortdesc, Color.LightGray))
                        {
                            if (ci.c != Color.LightGray)
                            {
                                sdesc += "<div id=\"col";
                                if (ci.c == Color.Black) sdesc += "0";
                                else if (ci.c == Color.DarkRed) sdesc += "1";
                                else if (ci.c == Color.DarkGreen) sdesc += "2";
                                else if (ci.c == Color.DarkKhaki) sdesc += "3";
                                else if (ci.c == Color.Blue) sdesc += "4";
                                else if (ci.c == Color.Indigo) sdesc += "5";
                                else if (ci.c == Color.DarkCyan) sdesc += "6";
                                // no need of default color
                                else if (ci.c == Color.DimGray) sdesc += "8";
                                else if (ci.c == Color.Red) sdesc += "9";
                                else if (ci.c == Color.Lime) sdesc += "10";
                                else if (ci.c == Color.Yellow) sdesc += "11";
                                else if (ci.c == Color.DodgerBlue) sdesc += "12";
                                else if (ci.c == Color.Fuchsia) sdesc += "13";
                                else if (ci.c == Color.Cyan) sdesc += "14";
                                else if (ci.c == Color.White) sdesc += "15";

                                sdesc += "\">" + ci.s + "</div>";
                            }
                        }
                        if (sdesc == "")
                            sdesc = o.shortdesc;
                        file.Write("<a href=\"#\">" + sdesc + nl);
                        file.Write("<span>Oggetto '" + o.keys + "', Tipo: <div id=\"white\">" + L.Get(L.object_types, o.properties[C.op_type]).ToUpper() + "</div>" + nl);

                        first = true;
                        s = "";
                        for (int i = 0; i <= C.of_end; i++)
                            if (o.flags[1 << i])
                                if (first)
                                {
                                    s += L.Get(L.object_flags, i);
                                    first = false;
                                }
                                else s += " " + L.Get(L.object_flags, i);
                        file.Write("<br>" + nl + "<br>L'oggetto e`: <div id=\"white\"> " + s.ToUpper() + "</div>" + nl);

                        first = true;
                        s = "";
                        for (int i = 0; i <= C.ow_end; i++)
                            if (o.wearpos[1 << i] && i != C.ow_personal && i != C.ow_take)
                                if (first)
                                {
                                    s += L.Get(L.object_wear_ita, i);
                                    first = false;
                                }
                                else s += " " + L.Get(L.object_wear_ita, i);
                        file.Write("<br>Puo' essere indossato su : <div id=\"white\">" + s.ToUpper() + "</div>" + nl);
                        file.Write("<br>Peso: " + o.properties[C.op_weight] + ", Valore: " + o.properties[C.op_value] + ", Costo di rent: " + o.properties[C.op_rent] + (o.properties[C.op_rent] > 20000 ? "  <div id=\"rare\">[RARO]</div>" : "") + nl);

                        if (o.properties[C.op_type] == C.ot_armor)
                            file.Write("<br>Il modificatore della AC e' <div id=\"damage\">" + o.values[1] + "</div>" + nl);

                        if (o.properties[C.op_type] == C.ot_weapon || o.properties[C.op_type] == C.ot_missile)
                            file.Write("<br>Il dado dei danni e` '<div id=\"damage\">" + o.values[1] + "d" + o.values[2] + "</div>'" + nl);

                        s = "";
                        foreach (ObjAffect aff in o.affects)
                            if (aff.index > 0)
                                s += nl + "<div id=\"affect\">Effetto : " + L.Get(L.object_affects, aff.index).ToUpper() + " by " + aff.ValueToString(true) + "</div>";

                        if (s != "")
                            file.Write("<br>Ecco i suoi effetti:" + s);

                        file.Write("<br>" + nl);

                        if (o.extras.Find(x => x.keys.Contains("crstl_value")) != null)
                            file.Write("<div id=\"colorcrystal\">Questo oggetto ha un valore in monete di cristallo.</div>" + nl);

                        if (o.extras.Find(x => x.keys.Contains("storia")) != null)
                            file.Write("<div id=\"colorstory\">Questo oggetto ha una storia da raccontare...</div>" + nl);

                        if (o.wearpos[1 << C.ow_personal])
                            file.Write("<div id=\"colorpersonal\">Questo oggetto verra' personalizzato su chi lo indossa per primo.</div>" + nl);

                        file.Write("</span>" + nl + "</a>" + nl + nl);
                    }
                    counter++;
                }

                file.Write("</div>" + nl + nl + "</div>" + nl + "</body>");
                file.Close();
            }
        }

        #endregion
    }
}
