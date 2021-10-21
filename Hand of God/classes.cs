using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Linq;

namespace HandofGod
{
    #region Area
    public class Area : IDisposable
    {
        public List<Zone> zones;
        public List<Room> rooms;
        public List<Mob> mobs;
        public List<Obj> objects;
        public List<Shop> shops;

        #region Constructor
        public Area()
        {
            zones = new List<Zone>();
            rooms = new List<Room>();
            mobs = new List<Mob>();
            objects = new List<Obj>();
            shops = new List<Shop>();
        }
        #endregion
        #region Dispose
        public void Dispose()
        { }
        #endregion

        #region Fields
        public int vnum_min
        {
            get
            {
                if (zones.Count <= 0)
                    return 0;

                int min = int.MaxValue;
                foreach (Zone z in zones)
                    if (z.vnum < min)
                        min = z.vnum;
                return min * 100;
            }
        }

        public int vnum_max
        {
            get
            {
                if (zones.Count <= 0)
                    return 10000000;

                int max = 0;
                foreach (Zone z in zones)
                    if (z.vnum_max > max) 
                        max = z.vnum_max;
                return max;
            }
        }
        #endregion

        #region Utils
        public Array RetrieveList(int i)
        {
            switch (i)
            {
                case C.i_zone: return zones.ToArray();
                case C.i_room: return rooms.ToArray();
                case C.i_mob: return mobs.ToArray();
                case C.i_obj: return objects.ToArray();
                case C.i_shop: return shops.ToArray();
                default: return null;
            }
        }

        public List<T> RetrieveList<T>()
        {
            if (typeof(T) == typeof(Zone)) return zones as List<T>;
            if (typeof(T) == typeof(Room)) return rooms as List<T>;
            if (typeof(T) == typeof(Mob)) return mobs as List<T>;
            if (typeof(T) == typeof(Obj)) return objects as List<T>;
            if (typeof(T) == typeof(Shop)) return shops as List<T>;
            else return null;
        }

        public bool Exists<T>(int vnum)
        { return RetrieveList<T>().Find(e => (e as area_element).vnum == vnum) != null; }

        public T Get<T>(int vnum)
        { return RetrieveList<T>().Find(e => (e as area_element).vnum == vnum); }

        /// <summary>
        /// Returns the *NEXT* vnum used starting from Start in the given list type.
        /// if decrement == true then returns the *PREVIOUS* instead
        /// </summary>
        public T GetNextUsed<T>(int start, bool decrement = false)
        {
            T result = default(T);
            List<T> list = RetrieveList<T>();
            
            int i = start;
            int real_min = typeof(T) == typeof(Zone) ? vnum_min / 100 : vnum_min;
            int real_max = typeof(T) == typeof(Zone) ? vnum_max / 100 : vnum_max;

            while ((decrement ? i >= real_min : i < real_max) && result == null)
            {
                i = i + (decrement ? -1 : 1);
                result = list.Find(e => (e as area_element).vnum == i);
            }
            return result;
        }
        #endregion

        #region Methods
        public void Clear()
        {
            zones.Clear();
            rooms.Clear();
            mobs.Clear();
            objects.Clear();
            shops.Clear();
        }

        public area_element Create<T>()
        {
            int dvnum = utils.GetFirstAvailableVnum<T>(this, typeof(T) == typeof(Zone) ? 0 : vnum_min, RetrieveList<T>());

            if (dvnum == -1)
                return null;

            area_element result = null;

            if (typeof(T) == typeof(Zone))
            {
                result = new Zone();
                zones.Add(result as Zone);
            }
            else if (typeof(T) == typeof(Room))
            {
                result = new Room();
                rooms.Add(result as Room);
            }
            else if (typeof(T) == typeof(Mob))
            {
                result = new Mob();
                mobs.Add(result as Mob);
            }
            else if (typeof(T) == typeof(Obj))
            {
                result = new Obj();
                objects.Add(result as Obj);
            }
            else if (typeof(T) == typeof(Shop))
            {
                result = new Shop();
                shops.Add(result as Shop);
            }

            result.vnum = dvnum;
            result.Clear(); 

            if (typeof(T) == typeof(Room))
                (result as Room).SetZone(this);

            return result;
        }

        public Room CreateRoom(int vnum = -1, Room data = null)
        {
            if (rooms.Find(r => r.vnum == vnum) != null)
                return null;

            if (vnum == -1)
                    for (int i = vnum_min; i <= vnum_max; i++)
                    if (rooms.Find(r => r.vnum == i) == null)
                    {
                        vnum = i;
                        break;
                    }

            if (vnum == -1)
                return null;

            Room result = new Room(data);
            result.exits = new List<Exit>();
            result.vnum = vnum;
            rooms.Add(result);
            return result;
        }

        public void RemoveElement(area_element el)
        {
            if (el == null)
                return;

            int lastvnum = el.vnum;

            if (el.GetType() == typeof(Zone))
                Dialogs.ConfirmAndDelete<Zone>(el as Zone, zones, "Eliminare la zona #" + el.vnum + "?");

            if (el.GetType() == typeof(Room))
            {
                foreach (Exit e in (el as Room).exits)
                    e.RemoveDoorInit(this);

                if (Dialogs.ConfirmAndDelete<Room>(el as Room, rooms, "Eliminare la stanza #" + el.vnum + "?"))
                    UpdateVNumReferences<Room>(lastvnum, -1);
            }

            if (el.GetType() == typeof(Mob))
                if (Dialogs.ConfirmAndDelete<Mob>(el as Mob, mobs, "Eliminare il mob #" + el.vnum + "?"))
                     UpdateVNumReferences<Mob>(lastvnum, -1);

            if (el.GetType() == typeof(Obj))
                if (Dialogs.ConfirmAndDelete<Obj>(el as Obj, objects, "Eliminare l'oggetto #" + el.vnum + "?"))
                         UpdateVNumReferences<Obj>(lastvnum, -1);

            if (el.GetType() == typeof(Shop))
                Dialogs.ConfirmAndDelete<Shop>(el as Shop, shops, "Eliminare il negozio #" + el.vnum + "?");
        }

        #region Procedural Map Generation
        public void SetVisuals()
        {
            if (rooms.Count <= 0)
                return;

            foreach (Room r in rooms)
            {
                r.visual.SetPosition(0, 0, 0);
                r.visual.rect.Width = VisualProperties.def_width;
                r.visual.rect.Height = VisualProperties.def_height;
            }

            List<Room> done = new List<Room>();
            List<Room> todo = new List<Room>();

            foreach (Room r in rooms)
                todo.Add(r);

            todo = todo.OrderBy(x => x.vnum).ToList();

            Room curr = null;
            Room lastset = null;
            while (todo.Count > 0)
            {
                curr = todo[0];

                Exit e = null;
                
                for (int i = 0; i <= C.dir_down; i++)
                    if ((e = curr.GetExit(i)) != null)
                    {
                        Room d = todo.Find(x => x.vnum == e.room);
                        if (d != null && d != curr && todo.Contains(d))
                        {
                            d.visual.rect.X = curr.visual.rect.X;
                            d.visual.rect.Y = curr.visual.rect.Y;
                            d.visual.floor = curr.visual.floor;
                            d.visual.SetPosition(this, i, true);
                        }
                    }

                if (curr.visual.rect.X == 0 && curr.visual.rect.Y == 0 && done.Count > 0)
                {
                    if (lastset == null)
                    {
                        curr.visual.SetPosition(this, C.dir_west, true);
                        curr.visual.rect.X = curr.visual.rect.X - curr.visual.rect.Width * 10;
                    }
                    else
                    {
                        curr.visual.rect.X = lastset.visual.rect.X;
                        curr.visual.rect.Y = lastset.visual.rect.Y;
                        curr.visual.SetPosition(this, C.dir_south, true);
                    }
                    lastset = curr;
                }

                todo.Remove(curr);
                done.Add(curr);
            }

            //foreach (Room d in done)
                //foreach (Room r in done.Where<Room>(x => x != d && x.visual.floor == d.visual.floor && x.visual.rect.X == d.visual.rect.X && x.visual.rect.Y == d.visual.rect.Y))
        }
        #endregion

        #endregion

        #region PostLoad
        public void PostLoad(Area ParentArea)
        {
            if (ParentArea == null)
                return;

            foreach (Room r in rooms)
                foreach (Exit e in r.exits)
                    if (e.dir <= C.dir_down)
                        e.SetDoorStatus(ParentArea);
        }
        #endregion

        #region Update VNum References
        public void UpdateVNumReferences<T>(int vnum, int newvnum, bool silent = false)
        {
            if (Options.data.updatevnums_auto && !silent)
            {
                if (newvnum == -1 && MessageBox.Show("Eliminare tutti i riferimenti all'elemento *" + typeof(T).Name + "* #" + vnum + "?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;

                if (newvnum > -1 && MessageBox.Show("Cambiare tutti i riferimenti dal vecchio vnum #" + vnum + " al nuovo vnum #" + newvnum + "?", "Conferma", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    return;
            }
   
            foreach (Room r in rooms)
                r.UpdateVNumReferences<T>(this, vnum, newvnum);

            foreach (Shop sh in shops)
                sh.UpdateVNumReferences<T>(this, vnum, newvnum);

            foreach (Zone z in zones)
                z.UpdateVNumReferences<T>(this, vnum, newvnum);
        }
        #endregion
    }
    #endregion

    #region Generic area element (zone, room, mob, obj)
    public class area_element
    {
        public int vnum;
        public string shortdesc;

        public area_element()
        { }

        public virtual void Clear()
        { }

        public virtual void CopyFrom(area_element data)
        { }

        #region Edit
        // edit form
        public virtual bool Edit(object p1, object p2, bool deleteifcancel = false, bool fixedvnum = false)
        {
            area_element selected = this;
            int lastvnum = vnum;
            bool result = true;
            Area ParentArea = p1 as Area;
            List<Init> ParentList = p2 as List<Init>;

            frm_area_element form = null;
            if (GetType() == typeof(Zone)) form = new frm_Zone();
            else if (GetType() == typeof(Room)) form = new frm_Room();
            else if (GetType() == typeof(Mob)) form = new frm_Mob();
            else if (GetType() == typeof(Obj)) form = new frm_Obj();
            else if (GetType() == typeof(Shop)) form = new frm_Shop();

            if (form == null)
                return false;

            form.ParentArea = ParentArea;
            form.original = this;
            form.data.CopyFrom(this);
            form.SetVnumRange();
            form.Data2Widgets();
            if (fixedvnum)
                form.SetFixedVnum();
            form.SetNotModified();
            DialogResult r = form.ShowDialog();
            if (r == DialogResult.OK)
            {
                // the user can select other elements without closing the form
                selected = form.original;
                lastvnum = form.original.vnum;
                form.Widgets2Data();
                selected.CopyFrom(form.data);
            }
            else if (r == DialogResult.Retry) // force refresh
            {
                form.Dispose();
                return true;
            }
            else if (deleteifcancel)
            {
                if (GetType() == typeof(Zone)) ParentArea.zones.Remove(this as Zone);
                else if (GetType() == typeof(Room)) ParentArea.rooms.Remove(this as Room);
                else if (GetType() == typeof(Mob)) ParentArea.mobs.Remove(this as Mob);
                else if (GetType() == typeof(Obj)) ParentArea.objects.Remove(this as Obj);
                else if (GetType() == typeof(Shop)) ParentArea.shops.Remove(this as Shop);
                else if (GetType() == typeof(Init)) ParentList.Remove(this as Init);
                result = false;
            }
            else result = false;

            if (result && selected.vnum != lastvnum)
                if (GetType() == typeof(Zone)) ParentArea.UpdateVNumReferences<Zone>(lastvnum, selected.vnum);
                else if (GetType() == typeof(Room)) ParentArea.UpdateVNumReferences<Room>(lastvnum, selected.vnum);
                else if (GetType() == typeof(Mob)) ParentArea.UpdateVNumReferences<Mob>(lastvnum, selected.vnum);
                else if (GetType() == typeof(Obj)) ParentArea.UpdateVNumReferences<Obj>(lastvnum, selected.vnum);
                else if (GetType() == typeof(Shop)) ParentArea.UpdateVNumReferences<Shop>(lastvnum, selected.vnum);
                else if (GetType() == typeof(Init)) ParentArea.UpdateVNumReferences<Init>(lastvnum, selected.vnum);

            form.Dispose();
            return result;
        }
        #endregion

        #region Update VNum References
        public virtual void UpdateVNumReferences<T>(Area ParentArea, int vnum, int newvnum)
        { }
        #endregion
    }
    #endregion

    #region Init
    public class Init : area_element
    {
        public int separator; // comment separator
        public string se_name; // special exit hack
        public int[] values = new int[C.iv_end + 1];
        
        public int type
        { get { return values[C.iv_type]; }
          set { values[C.iv_type] = value; } }

        public int sublevel
        {
            get
            {
                switch (type)
                {
                    case C.it_comment:
                    case C.it_obj_put:
                    case C.it_obj_give:
                    case C.it_obj_wear:
                    case C.it_mob_fear:
                    case C.it_mob_hate:
                    case C.it_follower_add: return 2;

                    default: return 1;
                }
            }
        }

        #region Statics
        // "MKFHCPEGORD";
        public static int CharToIndex(char c)
        {
            int i = C.it_charset.IndexOf(c);
            return i > -1 ? i : C.it_comment;
        }

        public static char IndexToChar(int i)
        {
            if (i < 0 || i >= C.it_charset.Length)
                return ' ';
            return C.it_charset[i];
        }

        public static int ValueToPercent(int i)
        {
            if (i <= 1)
                return 100;

            int result = i;

            if (i % 2 == 1)
                result -= 1;
            result /= 2;

            return result;
        }

        public static int PercentToValue(int t, int i)
        {
            int result = i;

            if (i == 100)
                result = 0;
            else result = i * 2;

            switch (t)
            {
                case C.it_mob_add:
                case C.it_mob_rem:
                case C.it_obj_add:
                case C.it_obj_rem:
                case C.it_door_init:
                case C.it_comment:
                    break;
                default: result += 1;
                    break;
            }
            return result;
        }

        public static int PercentToValue(Init init, int i)
        { return PercentToValue(init.type, i); }

        public static string[] GetFearHateValue1(int value0)
        {
            const string value_spec_charset = "_XR_cnnm";

            if (value0 < 0 || value0 > value_spec_charset.Length)
                return null;

            switch (value_spec_charset[value0])
            {
                case 'c': return L.classlist;
                case 'X': return L.mob_gender;
                case 'R': return L.mob_races;
                default: return null;
            }
        }

        public static string[] GetFearHateValue1(Init i)
        { return GetFearHateValue1(i.values[0]); }
        #endregion

        #region Constructor
        public Init(Init data = null)
        {
            if (data != null)
                CopyFrom(data);
            else Clear();
        }
        #endregion

        #region Methods
        /// <summary>
        /// returns i_room, i_mob or i_obj to determine which area_element is related to the given value
        /// </summary>
        public int GetElementType(int i)
        {
            switch (type)
            {
                case C.it_mob_add:
                case C.it_follower_add: return i == 0 ? C.i_mob : i == 2 ? C.i_room : -1;
                case C.it_mob_rem: return i == 0 ? C.i_mob : i == 1 ? C.i_room : -1;
                case C.it_obj_rem: return i == 0 ? C.i_obj : i == 1 ? C.i_room : -1;
                case C.it_obj_put: return i == 0 || i == 2 ? C.i_obj : -1;
                case C.it_obj_add: return i == 0 ? C.i_obj : i == 2 ? C.i_room : -1;
                case C.it_obj_wear:
                case C.it_obj_give: return i == 0 ? C.i_obj : -1;
                case C.it_door_init: return i == 0 ? C.i_room : -1;
            }
            return -1;
        }

        public override void Clear()
        {
            separator = 0;
            shortdesc = "";
            for (int i = 0; i <= C.iv_end; i++)
                values[i] = 0;

            values[C.iv_percent] = 100;
        }

        public override void CopyFrom(area_element data)
        {
            Init init = data as Init;

            if (init == null)
                return;

            Clear();

            separator = init.separator;
            shortdesc = init.shortdesc;
            se_name = init.se_name;

            for (int i = 0; i <= C.iv_end; i++)
                values[i] = init.values[i];
        }

        public string GenerateComment(Area ParentArea)
        {
            const string value_spec_charset = "_XR_cnnm";
            string arg0 = null, arg1 = null, arg2 = null, arg3 = null;
            area_element who;
            area_element where;

            string result = "";

            if (values[C.iv_percent] < 100)
                result += "[" + values[C.iv_percent] + "%] ";

            switch (type)
            {
                case C.it_mob_add:
                case C.it_follower_add:
                    who = ParentArea.Get<Mob>(Convert.ToInt32(values[C.iv_value0]));
                    where = ParentArea.Get<Room>(Convert.ToInt32(values[C.iv_value2]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    arg2 = values[C.iv_value2].ToString();
                    arg3 = where != null ? where.shortdesc : "non trovata";
                    break;
                case C.it_mob_rem:
                    who = ParentArea.Get<Mob>(Convert.ToInt32(values[C.iv_value0]));
                    where = ParentArea.Get<Room>(Convert.ToInt32(values[C.iv_value1]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    arg2 = values[C.iv_value1].ToString();
                    arg3 = where != null ? where.shortdesc : "non trovata";
                    break;
                case C.it_mob_fear:
                case C.it_mob_hate:
                    arg0 = L.Get(L.fear_types, values[C.iv_value0]);
                    if (values[C.iv_value0] < 0 || values[C.iv_value0] >= value_spec_charset.Length)
                        arg1 = "0";
                    else
                    switch (value_spec_charset[values[C.iv_value0]])
                    {
                        case 'c':
                            arg1 = L.Get(L.classlist, Convert.ToInt32(values[C.iv_value1]));
                            break;
                        case 'X':
                            arg1 = L.Get(L.mob_gender, Convert.ToInt32(values[C.iv_value1]));
                            break;
                        case 'R':
                            arg1 = L.Get(L.mob_races, Convert.ToInt32(values[C.iv_value1]));
                            break;
                        case '_':
                            break;
                        default:
                            arg1 = values[C.iv_value1].ToString();
                            break;
                    }
                    break;
                case C.it_obj_add:
                    who = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value0]));
                    where = ParentArea.Get<Room>(Convert.ToInt32(values[C.iv_value2]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    arg2 = values[C.iv_value2].ToString();
                    arg3 = where != null ? where.shortdesc : "non trovata";
                    break;
                case C.it_obj_rem:
                    who = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value0]));
                    where = ParentArea.Get<Room>(Convert.ToInt32(values[C.iv_value1]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    arg2 = values[C.iv_value1].ToString();
                    arg3 = where != null ? where.shortdesc : "non trovata";
                    break;
                case C.it_obj_give:
                     who = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value0]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    break;
                case C.it_obj_put:
                    who = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value0]));
                    where = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value2]));
                    arg0 = values[C.iv_value0].ToString();
                    arg1 = who != null ? who.shortdesc : "non trovato";
                    arg2 = values[C.iv_value2].ToString();
                    arg3 = where != null ? where.shortdesc : "non trovato";
                    break;
                case C.it_obj_wear:
                    arg0 = L.Get(L.init_wear, Convert.ToInt32(values[C.iv_value2]));
                    who = ParentArea.Get<Obj>(Convert.ToInt32(values[C.iv_value0]));
                    arg1 = values[C.iv_value0].ToString();
                    arg2 = who != null ? who.shortdesc : "non trovato";
                    break;
                case C.it_door_init:
                    arg0 = values[C.iv_value1] < C.dir_special ? L.Get(L.directions, Convert.ToInt32(values[C.iv_value1])) : se_name;
                    where = ParentArea.Get<Room>(Convert.ToInt32(values[C.iv_value0]));
                    arg1 = values[C.iv_value0].ToString() + (where != null ?  " " + where.shortdesc : "non trovata");
                    arg2 = "";
                    if (where != null)
                    {
                        Exit e = (where as Room).GetExit(Convert.ToInt32(values[C.iv_value1]));
                        if (e != null)
                        {
                            Room dest = ParentArea.Get<Room>(e.room);
                            arg2 = " --> " + (dest != null ? dest.vnum.ToString() + " " + dest.shortdesc : "non trovata");
                        }
                    }
                    arg3 = L.Get(L.door_status, Convert.ToInt32(values[C.iv_value2]));
                    break;
            }

            if (type >= L.generic_comments.Length || type < 0)
                result += shortdesc;
            else result += utils.CutColorCodes(String.Format(L.Get(L.generic_comments, type), arg0, arg1, arg2, arg3));

            return result;
        }
        #endregion

        #region Update VNum References
        public bool IsALoadingReference<T>(int vnum)
        {
            if (typeof(T) == typeof(Mob))
            {
                return (type == C.it_mob_add && values[C.iv_value0] == vnum) ||
                       (type == C.it_mob_rem && values[C.iv_value0] == vnum);
            }

            if (typeof(T) == typeof(Obj))
            {
                return (type == C.it_obj_add && values[C.iv_value0] == vnum) ||
                       (type == C.it_obj_rem && values[C.iv_value0] == vnum);
            }

            return false;
        }

        public override void UpdateVNumReferences<T>(Area ParentArea, int oldvnum, int newvnum)
        {
            if (typeof(T) == typeof(Room))
                switch (type)
                {
                    case C.it_mob_add:
                    case C.it_follower_add:
                    case C.it_obj_add:
                        if (values[C.iv_value2] == oldvnum)
                            values[C.iv_value2] = newvnum;
                        break;
                    case C.it_mob_rem:
                    case C.it_obj_rem:
                        if (values[C.iv_value1] == oldvnum)
                            values[C.iv_value1] = newvnum;
                        break;
                    case C.it_door_init:
                        if (values[C.iv_value0] == oldvnum)
                            values[C.iv_value0] = newvnum;
                        break;
                }

            if (typeof(T) == typeof(Mob))
                switch (type)
                {
                    case C.it_mob_add:
                    case C.it_follower_add:
                    case C.it_mob_rem:
                        if (values[C.iv_value0] == oldvnum)
                            values[C.iv_value0] = newvnum;
                        break;
                    case C.it_mob_fear:
                    case C.it_mob_hate:
                        if (values[C.iv_value0] == C.ifh_vnum &&
                            values[C.iv_value1] == oldvnum)
                            values[C.iv_value1] = newvnum;
                        break;
                }

            if (typeof(T) == typeof(Obj))
                switch (type)
                {
                    case C.it_obj_wear:
                    case C.it_obj_give:
                    case C.it_obj_add:
                    case C.it_obj_rem:
                        if (values[C.iv_value0] == oldvnum)
                            values[C.iv_value0] = newvnum;
                        break;
                    case C.it_obj_put:
                        if (values[C.iv_value0] == oldvnum)
                            values[C.iv_value0] = newvnum;
                        if (values[C.iv_value2] == oldvnum)
                            values[C.iv_value2] = newvnum;
                        break;
                }
        }
        #endregion
    }
    #endregion

    #region Zone
    public class Zone : area_element
    {
        public string filename { get; set; }
        public int vnum_max { get; set; }
        public int repop_type { get; set; }
        public int repop_interval { get; set; }
        public int limit_xp { get; set; }
        public int limit_gems { get; set; }
        public int map_id { get; set; }
        public int map_x { get; set; }
        public int map_y { get; set; }
        public BitVector32 flags = new BitVector32();
        public string[] descriptions = new string[C.zd_end + 1];
        // .hog data
        public bool hogdata_manual_vunm_limits;

        public List<Init> inits;

        #region Constructor
        public Zone()
        {
            inits = new List<Init>();
            Clear();
        }
        #endregion

        #region Methods
        public override void Clear()
        {
            shortdesc = "una nuova zona";
            vnum_max = vnum * 100 + 99;
            filename = "";
            repop_type = 0;
            repop_interval = 20;
            limit_xp = 0;
            limit_gems = 0;
            map_id = -1;
            map_x = -1;
            map_y = -1;

            // Inits
            inits.Clear();

            foreach (Init i in inits)
                i.type = C.it_comment;
            //

            for (int i = 0; i <= C.zf_end; i++)
                flags[1 << i] = false;

            for (int i = 0; i <= C.zd_end; i++)
                descriptions[i] = "";

            hogdata_manual_vunm_limits = true;
        }

        public override void CopyFrom(area_element data)
        {
            Zone z = data as Zone;

            if (z == null)
                return;

            vnum = z.vnum;
            shortdesc = z.shortdesc;
            filename = z.filename;
            vnum_max = z.vnum_max;
            repop_type = z.repop_type;
            repop_interval = z.repop_interval;
            limit_xp = z.limit_xp;
            limit_gems = z.limit_gems;
            map_id = z.map_id;
            map_x = z.map_x;
            map_y = z.map_y;

            inits.Clear();
            foreach (Init i in z.inits)
                inits.Add(i);

            for (int i = 0; i <= C.zf_end; i++)
                flags[1 << i] = z.flags[1 << i];

            for (int i = 0; i <= C.zd_end; i++)
                descriptions[i] = z.descriptions[i];

            hogdata_manual_vunm_limits = z.hogdata_manual_vunm_limits;
        }
        #endregion

        #region Update VNum References
        public void RemoveInit(Init i, bool children = false)
        {
            //debug
            //Console.WriteLine("Init " + i.shortdesc + " rimossa");

            if (children)
            {
                int j = inits.IndexOf(i) + 1;
                while (j < inits.Count() && inits[j].sublevel > 1)
                    if (inits[j].type != C.it_comment)
                    {
                        //Console.WriteLine("- rimuovo child " + inits[j].shortdesc);
                        inits.RemoveAt(j);
                    }
                    else j = j + 1;
            }

            inits.Remove(i);
        }

        public override void UpdateVNumReferences<T>(Area ParentArea, int oldvnum, int newvnum)
        {
            if (newvnum > -1)
            {
                foreach (Init i in inits)
                    i.UpdateVNumReferences<T>(ParentArea, oldvnum, newvnum);
            }
            else if (Options.data.delete_vnum_refs) // delete references
            {
                List<Init> toremove = new List<Init>();
                foreach (Init i in inits)
                    if (i.IsALoadingReference<T>(oldvnum))
                    {
                        Console.WriteLine("to remove " + oldvnum + " " + i.shortdesc);
                        toremove.Add(i);
                    }

                foreach (Init i in toremove)
                    RemoveInit(i, true);
            }
        }
        #endregion
    }
    #endregion

    #region Exit
    public class Exit
    {
        public int dir;
      // special exit vars
        public string name;
        public string nameinlist;
        public string str_from;
        public string str_to;
        public string inverse;
        public string desc;
      //
        public Room parent;
        public int room; // target room
        public Door door;
        public BitVector32 flags = new BitVector32();

        #region Constructor
        public Exit(Room p, Exit data = null)
        {
            parent = p;
            door = new Door();
            if (data != null)
                CopyFrom(data);
            else Clear();
        }
        #endregion

        #region Methods
        public void Clear()
        {
            dir = 0;
            room = -1;
            name = "";
            nameinlist = "";
            str_from = "";
            str_to = "";
            inverse = "";
            door.Clear();
            desc = "";
            parent = null;
 
            for (int i = 0; i <= C.df_end; i++)
                flags[1 << i] = false;
        }

        public void CopyFrom(Exit e)
        {
            Clear();

            if (e == null)
                return;

            parent = e.parent;
            dir = e.dir;
            room = e.room;
            door.CopyFrom(e.door);
            name = e.name;
            nameinlist = e.nameinlist;
            str_from = e.str_from;
            str_to = e.str_to;
            inverse = e.inverse;

            desc = e.desc;

            for (int i = 0; i <= C.df_end; i++)
                flags[1 << i] = e.flags[1 << i];
        }
        #endregion

        #region Door Inits
        public void SetDoorInit(Area ParentArea, int index = -1)
        {
            Init init = GetDoorInit(ParentArea);
            Zone z = ParentArea.Get<Zone>(parent.zone);

            if (z == null)
                return;

            if (!flags[1 << C.df_door] || (!Options.data.init_opendoors && door.status == 0))
            {
                z.RemoveInit(init);
                init = null;
                return;
            }

            if (init == null)
            {
                Init i = new Init();
                i.type = C.it_door_init;
                i.values[C.iv_value0] = parent.vnum;
                i.values[C.iv_value1] = dir;
                i.se_name = name;
                i.values[C.iv_value2] = door.status;
                if (Options.data.init_automatic_comment)
                    i.shortdesc = i.GenerateComment(ParentArea);
                if (index == -1)
                    z.inits.Add(i);
                else z.inits.Insert(index, i);
                init = i;
            }
            
            init.values[C.iv_value0] = parent.vnum;
            init.values[C.iv_value1] = dir;
            init.se_name = name;
            init.values[C.iv_value2] = door.status;

            if (Options.data.init_automatic_comment)
                init.shortdesc = init.GenerateComment(ParentArea);
        }

        public void RemoveDoorInit(Area ParentArea)
        {
            Zone z = ParentArea.Get<Zone>(parent.zone);

            if (z == null)
                return;

            Init i = GetDoorInit(ParentArea);
            z.RemoveInit(i);
        }

        public Init GetDoorInit(Area ParentArea)
        {
            Zone z = ParentArea.Get<Zone>(parent.zone);

            if (z == null)
                return null;

            return z.inits.Find( x => x.type == C.it_door_init && 
                                      x.values[C.iv_value0] == parent.vnum &&
                                      (dir < C.dir_special ? x.values[C.iv_value1] == dir : x.se_name == name) );
        }

        public void SetDoorStatus(Area ParentArea)
        {
            Init i = GetDoorInit(ParentArea);
            if (i != null)
                door.status = i.values[C.iv_value2];
        }
        #endregion

        #region Edit Form
        public bool Edit(Area ParentArea, Room ParentRoom, bool deleteifcancel = false)
        {
            if (ParentRoom == null)
                return false;

            Zone z = ParentArea.Get<Zone>(parent.zone);
            int init_index = z != null ? z.inits.IndexOf(GetDoorInit(ParentArea)) : -1;

            using (frm_Exit form = new frm_Exit())
            {
                form.ParentRoom = ParentRoom;
                form.ParentArea = ParentArea;
                SetDoorStatus(ParentArea);
                RemoveDoorInit(ParentArea);
                form.Data.CopyFrom(this);
                form.Data2Widgets();
                form.SetNotModified();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    form.Widgets2Data();
                    Exit exists = ParentRoom.GetExit(form.Data.dir, form.Data.name);
                    if (exists != null && exists != this && !Dialogs.ConfirmAndDelete<Exit>(exists, ParentRoom.exits, "Un'uscita in quella direzione esiste già, vuoi eliminare quella esistente?"))
                    {
                        if (deleteifcancel)
                            ParentRoom.exits.Remove(this);
                        return false;
                    }
                    CopyFrom(form.Data);
                    SetDoorInit(ParentArea, init_index);
                    return true;
                }
                else if (deleteifcancel)
                    ParentRoom.exits.Remove(this);
            }
            SetDoorInit(ParentArea, init_index);
            return false;
        }
        #endregion
    }
    #endregion

    #region Door
    public class Door
    {   
        public string keys;     //door keys 
        public int objkey;      //key object vnum
        public int cmd;         //open-close/pull/lift/twist/dig/etc..
        public int status;      //open/closed/locked

        #region Statics
        public static int IndexToCmd(int i)
        {
            switch (i)
            {
                case 1: return 224;
                case 2:
                case 3:
                case 4:
                case 5: return i + 369;
                case 6: return 495;
                case 7: return 496;
                default: return -1;
            }
        }

        public static int CmdToIndex(int cmd)
        {
            switch (cmd)
            {
                case 224: return 1;
                case 371:
                case 372:
                case 373:
                case 374: return cmd - 369;
                case 495: return 6;
                case 496: return 7;
                default: return 0;
            }
        }
        #endregion

        #region Constructor
        public Door(Door data = null)
        {
            if (data != null)
            {
                keys = data.keys;
                objkey = data.objkey;
                cmd = data.cmd;
                status = data.status;
            }
            else Clear();
        }
        #endregion

        public void Clear()
        {
            keys = "";
            objkey = -1;
            cmd = -1;
            status = 0;
        }

        public void CopyFrom(Door d)
        {
            keys = d.keys;
            objkey = d.objkey;
            cmd = d.cmd;
            status = d.status;
        }
    }
    #endregion

    #region Extra Description
    public class ExtraDesc
    {
        public string keys, desc;

        public ExtraDesc(ExtraDesc data = null)
        {
            if (data != null)
            {
                keys = data.keys;
                desc = data.desc;
            }
        }
    }
    #endregion

    #region Visual Properties
    public class VisualProperties
    {
        public const int def_width = 32;
        public const int def_height = 32;

        public Room Parent;
        public Rectangle rect;
        public int floor;
        public bool visible;

        /// <summary>
        /// returns the difference for positioning reasons
        /// </summary>
        public int SetWidth(int w)
        {
            int lastw = rect.Width;
            rect.Width = Math.Max(10, w);
            return rect.Width - lastw;
        }

        /// <summary>
        /// returns the difference for positioning reasons
        /// </summary>
        public int SetHeight(int h)
        {
            int lasth = rect.Height;
            rect.Height = Math.Max(10, h);
            return rect.Height - lasth;
        }

        #region Constructor
        public VisualProperties(Room parent)
        {
            Parent = parent;
            rect = new Rectangle(0, 0, def_width, def_height);
            Clear();
        }
        #endregion

        #region Clear / CopyFrom
        public void Clear()
        {
            rect.X = 0;
            rect.Y = 0;
            rect.Width = def_width;
            rect.Height = def_height;
            floor = 0;
            visible = true;
        }

        public void CopyFrom(VisualProperties data)
        {
            rect.X = data.rect.X;
            rect.Y = data.rect.Y;
            rect.Width = data.rect.Width;
            rect.Height = data.rect.Height;
            floor = data.floor;
            visible = data.visible;
        }
        #endregion

        #region Methods
        public void SetPosition(Area area, int dir, bool offset)
        {
            int k = VisualProperties.def_width * 2;
            bool flag = false;

            // find a free spot
            if (offset)
                while (!flag)
                {
                    flag = true;
                    foreach (Room r in area.rooms)
                        if (r.visual.floor == floor)
                            switch (dir)
                            {
                                case C.dir_north: if (r.visual.rect.X == rect.X && r.visual.rect.Y == rect.Y - k)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_height * 2;
                                    } break;
                                case C.dir_east: if (r.visual.rect.X == rect.X + k && r.visual.rect.Y == rect.Y)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_width * 2;
                                    } break;
                                case C.dir_south: if (r.visual.rect.X == rect.X && r.visual.rect.Y == rect.Y + k)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_height* 2;
                                    } break;
                                case C.dir_west: if (r.visual.rect.X == rect.X - k && r.visual.rect.Y == rect.Y)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_width * 2;
                                    } break;
                                case C.dir_up: if (r.visual.rect.X == rect.X + k && r.visual.rect.Y == rect.Y && r.visual.floor == floor + 1)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_width * 2;
                                    } break;
                                case C.dir_down: if (r.visual.rect.X == rect.X - k && r.visual.rect.Y == rect.Y && r.visual.floor == floor - 1)
                                    {
                                        flag = false;
                                        k = k + VisualProperties.def_width * 2;
                                    } break;
                            }
                }

            switch (dir)
            {
                case C.dir_north: rect.Y = rect.Y - k; break;
                case C.dir_south: rect.Y = rect.Y + k; break;
                case C.dir_east: rect.X = rect.X + k; break;
                case C.dir_west: rect.X = rect.X - k; break;
                case C.dir_up: rect.X = rect.X + k;
                    rect.Y = rect.Y - k;
                    floor++; break;
                case C.dir_down: rect.X = rect.X - k;
                    rect.Y = rect.Y + k;
                    floor--; break;
            }
        }

        public void SetPosition(Point pos, int floor)
        {
            rect.X = pos.X;
            rect.Y = pos.Y;
        }

        public void SetPosition(int x, int y, int f)
        {
            rect.X = x;
            rect.Y = y;
            floor = f;
        }

        public Room GenerateExit(Area ParentArea, int dir, bool inverse = false, bool offset = false, Room data = null)
        {
            Room result = ParentArea.Create<Room>() as Room;

            if (result == null)
                return null;

            int vnum = result.vnum;
            if (data != null)
                result.CopyFrom(data);
            result.vnum = vnum;

            result.visual.SetPosition(rect.X, rect.Y, floor);
            result.visual.SetPosition(ParentArea, dir, true);

            Parent.SetExit(result, result.vnum, dir, inverse);

            return result;
        }
        #endregion
    }
    #endregion

    #region Room
    public class Room : area_element
    {
        public VisualProperties visual;
        public int sect;
        public int zone;
        public string longdesc;
        public string daydesc;
        public string nightdesc;
        // teleport vars
        public int tel_time;
        public int tel_counter;
        public int tel_sect;
        public int tel_toroom;
        public BitVector32 tel_flags = new BitVector32();
        //
        public int max_obj;
        public int max_pc;
        public int water_current_dir;
        public int water_current_vel;
        public List<Exit> exits = new List<Exit>();
        public List<ExtraDesc> extras = new List<ExtraDesc>();
        public BitVector32 flags = new BitVector32();

        #region Constructor
        public Room(Room data = null)
        {
            visual = new VisualProperties(this);
            Clear();
            if (data != null)
                CopyFrom(data);
        }
        #endregion

        #region Set Zone
        public void SetZone(Area ParentArea)
        {
            int flooredvnum = (int)(Math.Floor((decimal)vnum / 100));
            foreach (Zone z in ParentArea.zones)
                if (z.vnum == flooredvnum)
                {
                    zone = z.vnum;
                    return;
                }

            if (ParentArea.zones.Count > 0)
                zone = ParentArea.zones[0].vnum;
        }
        #endregion

        #region Methods
        public override void Clear()
        {
            visual.Clear();
            zone = 0;
            sect      = C.rs_inside;
            shortdesc = "una nuova stanza";
            longdesc = "descrizione";
            daydesc = "";
            nightdesc = "";
            max_obj = 50;
            max_pc = 1;
            water_current_dir = 0;
            water_current_vel = 0;
            tel_counter = 0;
            tel_sect = 0;
            tel_time = 0;
            tel_toroom = -1;
            for (int i = 0; i <= C.tf_end; i++)
                tel_flags[1 << i] = false;

            for (int i = 0; i <= C.rf_end; i++)
                flags[1 << i] = false;

            extras.Clear();
            exits.Clear();
        }

        public override void CopyFrom(area_element data)
        {
            Room r = data as Room;

            if (r == null)
                return;

            Clear();
            vnum = r.vnum;
            visual.CopyFrom(r.visual);
            sect = r.sect;
            zone = r.zone;
            shortdesc = r.shortdesc;
            longdesc = r.longdesc;
            daydesc = r.daydesc;
            nightdesc = r.nightdesc;
            max_obj = r.max_obj;
            max_pc = r.max_pc;
            water_current_dir = r.water_current_dir;
            water_current_vel = r.water_current_vel;
            tel_counter = r.tel_counter;
            tel_sect = r.tel_sect;
            tel_time = r.tel_time;
            tel_toroom = r.tel_toroom;
            for (int i = 0; i <= C.tf_end; i++)
                tel_flags[1 << i] = r.tel_flags[1 << i];

            foreach (ExtraDesc ed in r.extras)
                extras.Add(new ExtraDesc(ed));

            for (int i = 0; i <= C.rf_end; i++)
                flags[1 << i] = r.flags[1 << i];

            foreach (Exit e in r.exits)
                exits.Add(new Exit(this, e));
        }

        public int GetFreeDir()
        {
            for (int i = 0; i <= C.dir_down; i++)
                if (exits.Find(x => x.dir == i) == null)
                    return i;
            return C.dir_special;
        }

        public Exit GetExit(int dir, string specialname = "")
        {
            if (dir == C.dir_special) // special exit
                return exits.Find(e => (e.dir == dir && e.name == specialname));
            else
                return exits.Find(e => e.dir == dir);
        }

        /// <summary>
        /// Creates a new exit, in both ways if inverse == true
        /// </summary>
        public Exit SetExit(Room to, int rvnum, int dir, bool inverse = false, Exit data = null)
        {
            Exit exists = GetExit(dir, data != null ? data.name : "");
            if (exists != null)
            {
                //MessageBox.Show(String.Format(Dialogs.msg_exitexists, vnum, Localization.directions[dir], dir == constants.dir_special ? " (" + exists.door.keys + ")" : ""));
                return null;
            }

            Exit e = data != null ? data : new Exit(this);
            e.dir = dir;
            e.room = rvnum;
            e.parent = this;

            exits.Add(e);

            if (inverse && to != null)
                to.SetExit(this, vnum, utils.GetOppositeDirection(dir));

            return e;
        }
        #endregion

        #region Update VNum References
        /// <summary>
        /// note: this method is called before the vnum changes are applied to the new room
        /// </summary>
        /// <param name="newvnum"> -1 to delete references and loading inits </param>
        public override void UpdateVNumReferences<T>(Area ParentArea, int oldvnum, int newvnum)
        {
            if (typeof(T) == typeof(Room))
            {
                List<Exit> toremove = new List<Exit>();

                foreach (Exit e in exits.Where(x => x.room == oldvnum))
                {
                    //if (ParentArea.Get<Room>(newvnum) == null)
                    if (newvnum == -1)
                        toremove.Add(e);
                    else e.room = newvnum;
                }

                foreach (Exit e in toremove)
                {
                    e.RemoveDoorInit(ParentArea);
                    exits.Remove(e);
                }

                if (tel_toroom == oldvnum)
                    tel_toroom = newvnum;
            }

            if (typeof(T) == typeof(Obj))
            {
                foreach (Exit e in exits)
                {
                    if (e.door.objkey == oldvnum)
                        e.door.objkey = newvnum;
                }
            }

            if (typeof(T) == typeof(Zone))
            {
                if (zone == oldvnum)
                    zone = newvnum;
            }
        }
        #endregion
    }
    #endregion

    #region Gem
    public struct Gem
    {
        public int percent;
        public string dice;
    }
    #endregion

    #region Mob Dialogue
    //mob dialog answer types
    public enum MobDialogueType { Domanda, Risposta };

    public class MDRollData
    {
        public int amount;
        public int fail_next;

        public MDRollData(MDRollData data = null)
        {
            if (data != null)
                CopyFrom(data);
            else Clear();
        }

        public void Clear()
        {
            amount = 0;
            fail_next = 0;
        }

        public void CopyFrom(MDRollData data)
        {
            amount = data.amount;
            fail_next = data.fail_next;
        }
    }

    public class MDCheckData
    {
        public int amount;

        public MDCheckData(MDCheckData data = null)
        {
            if (data != null)
                CopyFrom(data);
            else Clear();
        }

        public void Clear()
        {
            amount = 0;
        }

        public void CopyFrom(MDCheckData data)
        {
            amount = data.amount;
        }
    }

    public class MDFuncData : area_element
    {
        // vnum, shortdesc as ID, NAME
        public string desc; // func desc
        public string desc1; // param1 desc
        public string desc2; // param2 desc
        public string param1; // param1 value
        public string param2; // param2 value

        public MDFuncData(MDFuncData data = null)
        {
            if (data != null)
                CopyFrom(data);
            else Clear();
        }

        public override void Clear()
        {
            vnum = -1;
            shortdesc = "";
            desc = "";
            desc1 = "";
            desc2 = "";
            param1 = "";
            param2 = "";
        }

        public void CopyFrom(MDFuncData data)
        {
            vnum = data.vnum;
            shortdesc = data.shortdesc;
            desc = data.desc;
            desc1 = data.desc1;
            desc2 = data.desc2;
            param1 = data.param1;
            param2 = data.param2;
        }
    }

    public class MobDialogue : area_element
    {
        public MobDialogueType type;
        public int next;
        public string[] descriptions; // dialog, unspoken, before_act (to_char, to_vict, to_room), after_act (to_char, to_vict, to_room)
        public Dictionary<int, MDRollData> rollData;
        public Dictionary<int, MDCheckData> checkData;
        public MDFuncData onSuccess;
        public MDFuncData onFail;

        #region Constructor
        public MobDialogue(MobDialogue data = null)
        {
            descriptions = new string[8];
            rollData = new Dictionary<int, MDRollData>();
            checkData = new Dictionary<int, MDCheckData>();
            onSuccess = new MDFuncData();
            onFail= new MDFuncData();

            if (data != null)
                CopyFrom(data);
            else Clear();
        }

        public override void Clear()
        {
            type = MobDialogueType.Domanda;
            vnum = 0;
            next = 0;

            for (int i = 0; i <= C.mdd_end; i++)
                descriptions[i] = "";

            rollData.Clear();
            checkData.Clear();

            onSuccess.Clear();
            onFail.Clear();
        }

        public void CopyFrom(MobDialogue data, bool id = true)
        {
            type = data.type;
            if (id)
                vnum = data.vnum;
            next = data.next;

            for (int i = 0; i <= C.mdd_end; i++)
                descriptions[i] = data.descriptions[i];

            rollData.Clear();
            foreach (KeyValuePair<int, MDRollData> r in data.rollData)
                rollData.Add(r.Key, new MDRollData(r.Value));

            checkData.Clear();
            foreach (KeyValuePair<int, MDCheckData> r in data.checkData)
                checkData.Add(r.Key, new MDCheckData(r.Value));

            onSuccess.CopyFrom(data.onSuccess);
            onFail.CopyFrom(data.onFail);
        }
        #endregion

        public int FindFirstVNumAvailable(List<MobDialogue> list)
        {
            int i = 0;
            while (list.Find(x => x.vnum == i) != null)
                i++;
            return i;
        }

        #region Edit Form
        public void updateVNum(List<MobDialogue> list, int oldVNum, int newVNum)
        {
            int i = 0;
            while (i < list.Count())
            {
                if (list[i].vnum == newVNum)
                    list.RemoveAt(i);
                else
                {
                    if (list[i].vnum == oldVNum/* && list[i].type == MobDialogueType.Risposta*/)
                        list[i].vnum = newVNum;
                    i++;
                }
            }
            vnum = newVNum;
        }

        public bool Edit(Area ParentArea, Mob ParentMob, bool deleteifcancel = false)
        {
            if (ParentMob == null)
                return false;

            using (frm_MobDialogue form = new frm_MobDialogue())
            {
                form.ParentMob = ParentMob;
                form.ParentArea = ParentArea;
                form.Original = this;
                form.Data.CopyFrom(this);
                form.Data2Widgets();
                form.SetNotModified();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    form.Widgets2Data();

                    if (form.Data.vnum != vnum)
                    {
                        int newVnum = form.Data.vnum;
                        MobDialogue existing = ParentMob.dialogues.Find(x => x != this && x.type == MobDialogueType.Domanda && x.vnum == newVnum);
                        if (existing != null)
                        {
                            if (Dialogs.Confirm("Cambiare l'ID comporterà la sovrascrizione della frase\n\"" + existing.descriptions[0] + "\"\nContinuare?"))
                                updateVNum(ParentMob.dialogues, vnum, newVnum);
                        }
                        else updateVNum(ParentMob.dialogues, vnum, newVnum);
                    }

                    CopyFrom(form.Data, false);
                    return true;
                }
                else if (deleteifcancel)
                    ParentMob.dialogues.Remove(this);
            }
            return false;
        }
        #endregion
    }
    #endregion

    #region Mob
    public class Mob : area_element
    {
        public string keys;
        public string longdesc;
        public string damage;
        public string description;
        public string samesound;
        public string adjacentsound;

        public List<MobDialogue> dialogues;

        public Gem[] gems = new Gem[4];
        public int fame;
        public int[] epic_talents = new int[C.et_end + 1];
        public int[] values = new int[C.mv_end + 1];
        public BitVector32 flags = new BitVector32();
        public BitVector32 affects = new BitVector32();
        public BitVector32 resistances = new BitVector32();
        public BitVector32 immunities = new BitVector32();
        public BitVector32 susceptibles = new BitVector32();

        #region Statics
        public static int TypeToIndex(string t)
        {
            switch (t)
            {
                case "A": return C.mt_multi_attacks;
                case "B": return C.mt_unbashable;
                case "L": return C.mt_sound;
                case "S": return C.mt_simple;
                case "D": return C.mt_detailed;
                default : return C.mt_new;
            }
        }

        public static string IndexToType(int i)
        {
            switch (i)
            {
                case C.mt_multi_attacks : return "A";
                case C.mt_unbashable : return "B";
                case C.mt_sound: return "L";
                case C.mt_simple: return "S";
                case C.mt_detailed: return "D";
                default: return "N";
            }
        }
        #endregion

        #region Constructor
        public Mob(Mob data = null)
        {
            dialogues = new List<MobDialogue>();

            Clear();
            if (data != null)
              CopyFrom(data);
        }
        #endregion

        #region Methods
        public override void Clear()
        {
            //vnum = 0;
            keys = "nuovo mob";
            shortdesc = "un nuovo mob";
            longdesc = "un nuovo mob si trova qui";
            damage = "1d1+0";
            description = "";
            samesound = "";
            adjacentsound = "";
            fame = 0;
            for (int i = 0; i <= C.gt_end; i++)
            {
                gems[i].dice = "1d1+0";
                gems[i].percent = 0;
            }
            for (int i = 0; i <= C.et_end; i++)
                epic_talents[i] = 0;
            for (int i = 0; i <= C.mv_end; i++)
                values[i] = 0;
            for (int i = 0; i <= C.mf_end; i++)
                flags[1 << i] = false;
            for (int i = 0; i <= C.ma_end; i++)
                affects[1 << i] = false;
            for (int i = 0; i <= C.dt_end; i++)
                resistances[1 << i] = false;
            for (int i = 0; i <= C.dt_end; i++)
                immunities[1 << i] = false;
            for (int i = 0; i <= C.dt_end; i++)
                susceptibles[1 << i] = false;

            values[C.mv_level] = 1;
            values[C.mv_type] = 1;
            values[C.mv_attacks] = 1;
            values[C.mv_defaultpos] = C.mp_standing;
            values[C.mv_loadpos] = C.mp_standing;
            values[C.mv_race] = C.race_human;

            dialogues.Clear();
        }

        public override void CopyFrom(area_element data)
        {
            Mob m = data as Mob;

            if (m == null)
                return;

            Clear();
            vnum = m.vnum;
            keys = m.keys;
            shortdesc = m.shortdesc;
            longdesc = m.longdesc;
            damage = m.damage;
            description = m.description;
            samesound = m.samesound;
            adjacentsound = m.adjacentsound;
            fame = m.fame;
            
            for (int i = 0; i <= C.gt_end; i++)
            {
                gems[i].percent = m.gems[i].percent;
                gems[i].dice = m.gems[i].dice;
            }
            for (int i = 0; i <= C.et_end; i++)
                epic_talents[i] = m.epic_talents[i];
            for (int i = 0; i <= C.mv_end; i++)
                values[i] = m.values[i];
            for (int i = 0; i <= C.mf_end; i++)
                flags[1 << i] = m.flags[1 << i];
            for (int i = 0; i <= C.ma_end; i++)
                affects[1 << i] = m.affects[1 << i];
            for (int i = 0; i <= C.dt_end; i++)
                resistances[1 << i] = m.resistances[1 << i];
            for (int i = 0; i <= C.dt_end; i++)
                immunities[1 << i] = m.immunities[1 << i];
            for (int i = 0; i <= C.dt_end; i++)
                susceptibles[1 << i] = m.susceptibles[1 << i];

            foreach (MobDialogue md in (data as Mob).dialogues)
                dialogues.Add(new MobDialogue(md));
        }
        #endregion
    }
    #endregion

    #region Spell
    public class Spell : area_element
    { }
    #endregion

    #region ObjAffect
    public class ObjAffect
    {
        public int index;
        public int value;
        public int value2;
        public int value3;

        public static bool isComplex(int id)
        {
            return C.objaffects[id] == 'Q';
        }

        public void Clear()
        {
            index = 0;
            value = 0;
        }

        private string getflagsstring(string[] lines)
        {
            string result = "[";
            bool first = true;
            BitVector32 bv = new BitVector32();
            bv[value] = true;

            for (int i = 0; i < lines.Length; i++)
                if (bv[1 << i])
                {
                    result = result + (first ? "" : " ") + lines[i];
                    first = false;
                }

            result = result + "]";

            return result;
        }

        public string ValueToString(bool longversion = false)
        {
            try
            {
                switch (C.objaffects[index])
                {
                    case 'X': return L.Get(L.mob_gender, value);
                    case 'E': return getflagsstring(longversion ? L.damage_types : L.damage_types_short);
                    case 'A': return getflagsstring(longversion ? L.mob_affects : L.mob_affects_short);
                    case 'S': return Database.GetSpell(value).shortdesc;
                    case 'R': return L.Get(L.mob_races, value);
                    case 'G': return L.Get(L.alignment_names, value);
                    // 17/10/21 Saregon - affects
                    case 'Q': return value.ToString() + " - " + value2.ToString() + " - " + value3.ToString();
                    default: return value.ToString();
                }
            }
            catch
            {
                return value.ToString();
            }
        }
    }
    #endregion

    #region Obj
    public class Obj : area_element
    {
        #region Vars
        public string keys;
        public string description;
        public string actiondesc;
        public bool HTMLExportable;
        
        public int[] properties = new int[4];
        public int[] values = new int[4];
        public ObjAffect[] affects = new ObjAffect[5];
        public BitVector32 wearpos = new BitVector32();
        public BitVector32 flags = new BitVector32();
        public List<ExtraDesc> extras = new List<ExtraDesc>();
        #endregion

        #region Constructor
        public Obj(Obj data = null)
        {
            for (int i = 0; i <= 4; i++)
                affects[i] = new ObjAffect();

            Clear();
            if (data != null)
                CopyFrom(data);
        }
        #endregion

        #region Methods
        public override void Clear()
        {
            //vnum = 0;
            keys = "nuovo oggetto";
            shortdesc = "un nuovo oggetto";
            description = "un nuovo oggetto si trova qui";
            actiondesc = "";
        
            for (int i = 0; i <= C.op_end; i++)
                properties[i] = 0;

            for (int i = 0; i <= 3; i++)
                values[i] = 0;

            for (int i = 0; i <= 4; i++)
                affects[i].Clear();

            for (int i = 0; i <= C.of_end; i++)
                flags[1 << i] = false;

            for (int i = 0; i <= C.ow_end; i++)
                wearpos[1 << i] = false;

            properties[C.op_type] = C.ot_armor;

            extras.Clear();
            HTMLExportable = false;
        }

        public override void CopyFrom(area_element data)
        {
            Obj o = data as Obj;

            if (o == null)
                return;

            Clear();

            vnum = o.vnum;
            keys = o.keys;
            shortdesc = o.shortdesc;
            description = o.description;
            actiondesc = o.actiondesc;

            for (int i = 0; i <= C.op_end; i++)
                properties[i] = o.properties[i];

            for (int i = 0; i <= 3; i++)
                values[i] = o.values[i];

            for (int i = 0; i <= 4; i++)
            {
                affects[i].index = o.affects[i].index;
                affects[i].value = o.affects[i].value;
                // 17/10/21 Saregon - affects
                affects[i].value2 = o.affects[i].value2;
                affects[i].value3 = o.affects[i].value3;
                //
            }

            for (int i = 0; i <= C.of_end; i++)
                flags[1 << i] = o.flags[1 << i];

            for (int i = 0; i <= C.ow_end; i++)
                wearpos[1 << i] = o.wearpos[1 << i];

            foreach (ExtraDesc ed in o.extras)
                extras.Add(new ExtraDesc(ed));

            HTMLExportable = o.HTMLExportable;
        }
        #endregion

        #region Affects
        public bool SetAffect(int key, int value, int val2 = 0, int val3 = 0)
        {
            int i = 0;
            while (affects[i].index != 0 && i < 5)
                i++;

            if (i == 5)
                return false;

            affects[i].index = key;
            affects[i].value = value;
            // 17/10/21 Saregon - affects
            if (val2 != 0)
            {
                affects[i].value2 = val2;
                affects[i].value3 = val3;
            }
            //
            return true;
        }
        #endregion
    }
    #endregion

    #region Shop
    public class Shop : area_element
    {
        public int[] properties = new int[18];
        public decimal mul_buy;
        public decimal mul_sell;
        public string[] speech = new string[7];

        #region Constructor
        public Shop(Shop data = null)
        {
              Clear();
              if (data != null)
                CopyFrom(data);
        }
        #endregion

        #region Methods
        public override void Clear()
        {
            for (int i = 0; i <= C.shp_props_end; i++)
                switch (i)
                {
                    case C.shp_mob:
                    case C.shp_room:
                    case C.shp_objtosell0:
                    case C.shp_objtosell1:
                    case C.shp_objtosell2:
                    case C.shp_objtosell3:
                    case C.shp_objtosell4: properties[i] = -1; break;
                    case C.shp_open0: properties[i] = 9; break;
                    case C.shp_close0: properties[i] = 13; break;
                    case C.shp_open1: properties[i] = 15; break;
                    case C.shp_close1: properties[i] = 19; break;
                    case C.shp_react_attack:
                    case C.shp_react_indigence: properties[i] = 2; break;
                    default: properties[i] = 0; break;
                }

            for (int i = 0; i <= C.shp_speech_end; i++)
                switch (i)
                {
                    case C.shp_speech_vendor_noobj: speech[i] = "%s Mi spiace ma non vendo quel tipo di oggetto."; break;
                    case C.shp_speech_buyer_noobj: speech[i] = "%s Non mi sembra che lei abbia nulla del genere."; break;
                    case C.shp_speech_nosell: speech[i] = "%s Mi dispiace ma non tratto questo tipo di oggetti."; break;
                    case C.shp_speech_vendor_nomoney: speech[i] = "%s Mi dispiace ma non ho abbastanza denaro al momento."; break;
                    case C.shp_speech_buyer_nomoney: speech[i] = "%s Forse dovrebbe orientarsi su qualcosa di meno costoso."; break;
                    case C.shp_speech_sell: speech[i] = "%s Per sole %d monete, lei ha fatto un ottimo affare."; break;
                    case C.shp_speech_buy: speech[i] = "%s Ecco a lei %d monete."; break;
                }
            mul_buy = 1.10M;
            mul_sell = 0.30M;
        }

        public override void CopyFrom(area_element data)
        {
            Shop s = data as Shop;

            if (s == null)
                return;

            Clear();
            vnum = s.vnum;
            for (int i = 0; i <= C.shp_props_end; i++)
                properties[i] = s.properties[i];

            for (int i = 0; i <= C.shp_speech_end; i++)
                speech[i] = s.speech[i];

            mul_buy = s.mul_buy;
            mul_sell = s.mul_sell;
        }
        #endregion

        #region Update VNum References
        public override void UpdateVNumReferences<T>(Area ParentArea, int oldvnum, int newvnum)
        {
            if (typeof(T) == typeof(Room))
            {
                if (properties[C.shp_room] == oldvnum)
                    properties[C.shp_room] = newvnum;
            }

            if (typeof(T) == typeof(Mob))
            {
                if (properties[C.shp_mob] == oldvnum)
                    properties[C.shp_mob] = newvnum;
            }

            if (typeof(T) == typeof(Obj))
            {
                for (int i = C.shp_objtosell0; i <= C.shp_objtosell4; i++)
                if (properties[i] == oldvnum)
                    properties[i] = newvnum;
            }
        }
        #endregion
    }
    #endregion

    #region Camera
    class Camera
    {
        public Point pos;
        public int floor;
        public float zoom;

        public Camera()
        {
            pos = new Point();
            pos.X = 0;
            pos.Y = 0;
            floor = 0;
            zoom = 1;
        }

        public void Update()
        {
            zoom = Math.Min(zoom, 3);
            zoom = Math.Max(0.4f, zoom);
        }

        public void Center(NoFlickerPanel panel, Room r)
        {
            //int dist = floor - r.visual.floor * (r.visual.rect.Height + r.visual.rect.Width / 2);
            pos.X = (int)(r.visual.rect.X - panel.Width / (2 * zoom));
            pos.Y = (int)(r.visual.rect.Y - panel.Height / (2 * zoom));
            floor = r.visual.floor;
        }

        public Point Transform(Point p)
        {
            p.X = (int)(p.X * zoom);
            p.Y = (int)(p.Y * zoom);
            return p;
        }

        public Rectangle Transform(Rectangle r)
        {
            r.X = (int)(r.X * zoom);
            r.Y = (int)(r.Y * zoom);
            r.Width = (int)(r.Width * zoom);
            r.Height = (int)(r.Height * zoom);
            return r;
        }
    }
    #endregion

    #region MUD Preview Pseudo-Elements
    public class PElement
    {
        public int vnum, percent;
        public string shortdesc, longdesc, keys;
    }

    public class PObj : PElement
    {
        public Obj parent;
        public List<PObj> objects;
        //public List<ExtraDesc> extras;

        public void AddObj(PObj po)
        {
            if (objects == null)
                objects = new List<PObj>();
            objects.Add(po);
        }
    }

    public class PMob : PElement
    {
        public Mob parent;
        public string description;
        public List<PObj> inventory;
        public Dictionary<int, PObj> equipped;

        public void AddInventory(PObj po)
        {
            if (inventory == null)
                inventory = new List<PObj>();

            inventory.Add(po);
        }

        public bool AddEquipped(int slot, PObj po)
        {
            if (equipped == null)
                equipped = new Dictionary<int,PObj>();

            if (equipped.ContainsKey(slot))
                return false;

            equipped.Add(slot, po);
            return true;
        }
    }
    #endregion
}
