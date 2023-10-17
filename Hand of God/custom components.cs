using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.IO;

namespace HandofGod
{
    #region Minor
    public class NoselButton : Button
    {
        public NoselButton()
        { this.SetStyle(ControlStyles.Selectable, false); }
    }

    public class NoFlickerPanel : Panel
    {
        public NoFlickerPanel()
        {
            //Enable these styles to reduce flicker
            //1. Enable user paint.
            this.SetStyle(ControlStyles.UserPaint, true);
            //2. Enable double buffer.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //3. Ignore a windows erase message to reduce flicker.
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }
    }
    #endregion

    #region ListView Sorter
    class Sorter : System.Collections.IComparer
    {
        public int Column = 0;
        public SortOrder Order = SortOrder.Ascending;
        public int Compare(object x, object y) // IComparer Member
        {
            if (!(x is ListViewItem))
                return 0;
            if (!(y is ListViewItem))
                return 0;

            ListViewItem l1 = (ListViewItem)x;
            ListViewItem l2 = (ListViewItem)y;

            if (Column > l1.SubItems.Count - 1)
                Column = l1.SubItems.Count - 1;

            // totals line
            if (l1.SubItems[0] != null && l1.SubItems[0].Text == "Tot")
                return 0;
            if (l2.SubItems[0] != null && l2.SubItems[0].Text == "Tot")
                return 1;

            // mob dialogues start override
            //if (l1.SubItems[0] != null && l1.SubItems[0].Text == "Start")
                //return 0;
            //if (l2.SubItems[0] != null && l2.SubItems[0].Text == "Start")
                //return 1;

            if (l1.ListView.Columns[Column].Tag == null)
                l1.ListView.Columns[Column].Tag = "Text";

            if (l1.ListView.Columns[Column].Tag.ToString() == "Numeric")
            {
                try
                {
                    float fl1 = float.Parse(l1.SubItems[Column].Text);
                    float fl2 = float.Parse(l2.SubItems[Column].Text);
                    
                    return Order == SortOrder.Ascending ? fl1.CompareTo(fl2) : fl2.CompareTo(fl1);
                }
                catch 
                {
                    return 0;
                }
            }
            else
            {
                string str1 = l1.SubItems[Column].Text;
                string str2 = l2.SubItems[Column].Text;

                return Order == SortOrder.Ascending ? str1.CompareTo(str2) : str2.CompareTo(str1);
            }
        }
    }
    #endregion
    #region HoG ListView
    public partial class HoGListView : ListView
    {
        private Color exits_secret_invisible_color = Color.Pink;
        private Color exits_secret_color = Color.PeachPuff;
        private Color totals_color = Color.LightGreen;

        public string Filter { get; set; }
        private string flags_spacer = ", ";
        private int current_column = -1;
        private List<ListViewItem> data;
        private AreaElementTooltip tooltip;

        #region Constructor
        public void SetComponents()
        {
            tooltip.Parent = Parent;
            tooltip.BackColor = Color.Black;
            tooltip.BringToFront();
        }

        public HoGListView()
        {
            Sorter s = new Sorter();
            s.Column = 0;
            ListViewItemSorter = s;
            GridLines = true;
            FullRowSelect = true;
            MultiSelect = false;
            View = View.Details;
            ColumnClick += new ColumnClickEventHandler(list_ColumnClick);
            MouseMove += new MouseEventHandler(onmousemove);
            KeyDown += new KeyEventHandler(onkeydown);
            data = new List<ListViewItem>();
            Filter = "";
            tooltip = new AreaElementTooltip();
        }
        #endregion

        #region Events
        private void list_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sorter s = (Sorter)ListViewItemSorter;
            s.Column = e.Column;
            s.Order = s.Order == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            Sort();
            ReDrawRowsBackground();
        }

        private void onmousemove(object sender, MouseEventArgs e)
        {
            setTooltipData();
            TooltipCheck();
        }

        private void onkeydown(object sender, KeyEventArgs e)
        {
            TooltipCheck();
        }
        #endregion

        #region Columns Templates
        public bool SetColumns(int id)
        {
            data.Clear();

            if (id == current_column)
                return false;

            (ListViewItemSorter as Sorter).Column = 0;
            Clear();
            switch (id)
            {
                case C.i_zone: Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Zona", -2);
                    Columns.Add("# min", -2);
                    Columns[2].Tag = "Numeric";
                    Columns.Add("# max", -2);
                    Columns[3].Tag = "Numeric";
                    Columns.Add("Repop", -2);
                    Columns.Add("Intervallo", -2);
                    Columns[5].Tag = "Numeric";
                    Columns.Add("Flags", -2);
                    break;
                case C.i_report_deathrooms:
                case C.i_room: Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Settore", -2);
                    Columns.Add("Flags", -2);
                    break;
                case C.i_mob: Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Razza", -2);
                    Columns.Add("Acts", -2);
                    Columns.Add("Affects", -2);
                    break;
                case C.i_obj: Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Tipo", -2);
                    Columns.Add("Affects", -2);
                    break;
                case C.i_shop: Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Mob", -2);
                    Columns.Add("Stanza", -2);
                    break;
                case C.i_exit: Columns.Add("Dir", -2);
                    Columns.Add("Alla Stanza", -2);
                    Columns.Add("Stato", -2);
                    Columns.Add("Chiave", -2);
                    Columns.Add("Init", -2);
                    Columns.Add("Flags", -2);
                    break;
                case C.i_init_door: Columns.Add("Tipo", -2);
                    Columns.Add("Stanza", -2);
                    Columns.Add("Direzione", -2);
                    Columns.Add("Stato", -2);
                    Columns.Add("Percentuale", -2);
                    Columns.Add("Commento", -2);
                    break;
                case C.i_spell: Columns.Add("ID", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Spell", -2);
                    break;
               // reports
                case C.i_report_gems:
                     Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Inits", -2);
                    Columns.Add("Diamanti", -2);
                    Columns.Add("Smeraldi", -2);
                    Columns.Add("Rubini", -2);
                    Columns.Add("Zaffiri", -2);
                    break;
                case C.i_report_coinsfame:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Inits", -2);
                    Columns[2].Tag = "Numeric";
                    Columns.Add("Fama", -2);
                    Columns[3].Tag = "Numeric";
                    Columns.Add("Monete", -2);
                    Columns[4].Tag = "Numeric";
                    Columns.Add("Fama * Inits", -2);
                    Columns[5].Tag = "Numeric";
                    Columns.Add("Monete * Inits", -2);
                    Columns[6].Tag = "Numeric";
                    break;
                case C.i_report_treasures:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Inits", -2);
                    Columns[2].Tag = "Numeric";
                    Columns.Add("Monete", -2);
                    Columns[3].Tag = "Numeric";
                    Columns.Add("Monete * Inits", -2);
                    Columns[4].Tag = "Numeric";
                    break;
                case C.i_report_keys:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Tipo", -2);
                    Columns.Add("#", -2);
                    Columns[3].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Direzione", -2);
                    break;
               // deathrooms same as i_room
                case C.i_report_externalexits: 
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Short", -2);
                    Columns.Add("Dir", -2);
                    Columns.Add("#", -2);
                    Columns[3].Tag = "Numeric";
                    break;
                case C.i_mobdialog_Q:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Domanda", -2);
                    Columns.Add("Check", -2);
                    break;
                case C.i_mobdialog_A:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Risposta", -2);
                    Columns.Add("Check", -2);
                    break;
                case C.i_mobdialog_function:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Nome", -2);
                    Columns.Add("Descrizione", -2);
                    Columns.Add("Valore 1", -2);
                    Columns.Add("Valore 2", -2);
                    break;
                case C.i_shop_item_list:
                    Columns.Add("#", -2);
                    Columns[0].Tag = "Numeric";
                    Columns.Add("Stock", -2);
                    break;
            }

            current_column = id;
            return true;
        }
        #endregion

        #region Fill List
        private void FillList()
        {
            foreach (ListViewItem i in data)
                for (int j = 0; j < i.SubItems.Count; j++)
                    if (i.SubItems[j].Text.ToLower().IndexOf(Filter.ToLower()) > -1)
                    {
                        Items.Add(i);
                        break;
                    }
        }
        #endregion

        #region Add Item
        public ListViewItem AddItem(Area ParentArea, area_element el, bool singleitem = false)
        {
            if (el == null)
                return null;

            ListViewItem item = null;
            object tag = el;
            string[] s = new string[8];

            s[0] = el.vnum.ToString();
            s[1] = el.shortdesc;

            #region Zone
            if (el.GetType() == typeof(Zone))
            {
                Zone z = el as Zone;

                s[2] = (z.vnum * 100).ToString();
                s[3] = z.vnum_max.ToString();
                s[4] = L.Get(L.repop_types, z.repop_type);
                s[5] = z.repop_interval.ToString();
                s[6] = "";
                for (int i = 2; i <= C.zf_end; i++)
                    s[6] = s[6] + (z.flags[1 << i] ? L.Get(L.zone_flags, i) + flags_spacer : "");
            }
            #endregion
            
            #region Room
            else if (el.GetType() == typeof(Room))
            {
                Room r = el as Room;

                s[2] = L.Get(L.room_sectors, r.sect);
                s[3] = "";
                for (int i = 0; i <= C.rf_end; i++)
                    s[3] = s[3] + (r.flags[1 << i] ? L.Get(L.room_flags, i) + flags_spacer : "");
            }
            #endregion

            #region Mob
            else if (el.GetType() == typeof(Mob))
            {
                Mob m = el as Mob;

                Func<int> getinits = new Func<int>(() =>
                {
                    int result = 0;
                    foreach (Zone z in ParentArea.zones)
                        foreach (Init i in z.inits)
                        {
                            if ((i.type == C.it_mob_add || i.type == C.it_follower_add) && i.values[C.iv_value0] == m.vnum)
                                result++;
                        }
                    return result;
                });

                switch (current_column)
                {
                    case C.i_mob:
                        s[2] = L.Get(L.mob_races, m.values[C.mv_race]);
                        s[3] = "";
                        for (int i = 0; i <= C.mf_end; i++)
                            s[3] = s[3] + (m.flags[1 << i] ? L.Get(L.mob_acts, i) + flags_spacer : "");
                        s[4] = "";
                        for (int i = 0; i <= C.ma_end; i++)
                            s[4] = s[4] + (m.affects[1 << i] ? L.Get(L.mob_affects, i) + flags_spacer : "");
                        break;
                    case C.i_report_gems:
                        s[2] = getinits().ToString();
                        for (int i = 0; i <= C.gt_end; i++)
                            s[3 + i] = m.gems[i].percent > 0 ? m.gems[i].dice + " ( " + m.gems[i].percent + "% )" : "";
                        break;
                    case C.i_report_coinsfame:
                        int inits = getinits();
                        s[2] = inits.ToString();
                        s[3] = m.fame.ToString();
                        s[4] = m.values[C.mv_gold].ToString();
                        s[5] = (m.fame * inits).ToString();
                        s[6] = (m.values[C.mv_gold] * inits).ToString();
                        break;
                }
            }
            #endregion

            #region Obj
            else if (el.GetType() == typeof(Obj))
            {
                Obj o = el as Obj;

                Func<int> getinits = new Func<int>(() =>
                {
                    int result = 0;
                    foreach (Zone z in ParentArea.zones)
                        foreach (Init i in z.inits.Where<Init>(ix => (ix.type == C.it_obj_wear
                                                                 || ix.type == C.it_obj_put
                                                                 || ix.type == C.it_obj_give
                                                                 || ix.type == C.it_obj_add)
                                                                 && ix.values[C.iv_value0] == o.vnum))
                                result++;
                    return result;
                });

                switch (current_column)
                {
                    case C.i_report_treasures:
                        int inits = getinits();
                        s[2] = inits.ToString();
                        s[3] = o.values[0].ToString();
                        s[4] = (o.values[0] * inits).ToString();
                        break;
                    // container keys
                    // this is a little hack, we pass the container itself and use its key as the main element instead
                    case C.i_report_keys:
                        Obj key = ParentArea.Get<Obj>(o.values[2]);
                        s[0] = o.values[2].ToString();
                        s[1] = (key != null ? key.shortdesc : "non trovato");
                        s[2] = "Contenitore --> ";
                        s[3] = o.vnum.ToString();
                        s[4] = o.shortdesc;
                        tag = key;
                        break;
                    default:
                        s[2] = L.Get(L.object_types, o.properties[C.op_type]);
                        string d = "";
                        bool first = true;
                        for (int i = 0; i <= 4; i++)
                        {
                            if (o.affects[i].index > C.oa_none)
                            {
                                d = d + (first ? "" : ", ") + L.Get(L.object_affects, o.affects[i].index) + " (" + o.affects[i].ValueToString() + ")";
                                first = false;
                            }
                        }
                        s[3] = d;
                        break;
                }
            }
            #endregion

            #region Shop
            else if (el.GetType() == typeof(Shop))
            {
                Shop sh = el as Shop;
                area_element a0 = ParentArea.Get<Mob>(sh.properties[C.shp_mob]);
                area_element a1 = ParentArea.Get<Room>(sh.properties[C.shp_room]);

                s[1] = "[" + sh.properties[C.shp_mob] + "] " + (a0 != null ? a0.shortdesc : "");
                s[2] = "[" + sh.properties[C.shp_room] + "] " + (a1 != null ? a1.shortdesc : "");
            }

            else if (el.GetType() == typeof(SoldItem))
            {
                SoldItem si = el as SoldItem;
                s[0] = si.vnum.ToString();
                s[1] = si.shortdesc;
            }
            #endregion

            #region MobDialogue
            else if (el.GetType() == typeof(MobDialogue))
            {
                MobDialogue f = el as MobDialogue;

                if (f.type == MobDialogueType.Domanda)
                {
                    switch (f.vnum)
                    {
                        case 0 : s[0] = "Start"; break;
                        default: break;
                    }
                }
                s[1] = f.descriptions[0];
            }
            #endregion

            #region MobDialogue Function
            else if (el.GetType() == typeof(MDFuncData))
            {
                MDFuncData f = el as MDFuncData;

                s[2] = f.desc;
                s[3] = f.desc1;
                s[4] = f.desc2;
            }
            #endregion

            item = new ListViewItem(s);
            item.Tag = tag;
            data.Add(item);

            if (singleitem)
                Redraw();

            return item;
        }
        #endregion

        #region Draw
        private void ReDrawRowsBackground()
        {
            for (int i = 1; i < Items.Count; i += 2)
            {
                if (Items[i].BackColor != exits_secret_color && Items[i].BackColor != exits_secret_invisible_color && Items[i].BackColor != totals_color)
                    Items[i].BackColor = Color.AliceBlue;
                if (Items[i - 1].BackColor != exits_secret_color && Items[i - 1].BackColor != exits_secret_invisible_color && Items[i - 1].BackColor != totals_color)
                    Items[i - 1].BackColor = Color.White;
            }
        }

        public void Redraw()
        {
            BeginUpdate();
            Items.Clear();
            FillList();
            ReDrawRowsBackground();
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            if (Columns.Count > 0)
            {
                // set the last column's width to fit the listview's width if smaller
                int i = 0;
                foreach (ColumnHeader c in Columns)
                    i += c.Width;
                if (i < Width)
                    Columns[Columns.Count - 1].Width = Columns[Columns.Count - 1].Width + Width - i - 30;
            }
            EndUpdate();
            Refresh();
        }
        #endregion

        #region List Generation by Type
        #region Area Elements
        // da togliere none,(sotituito da top_values) - sare
        public void AddToList<T>(Area ParentArea, List<T> list, bool none = false, Predicate<T> filter = null, Dictionary<int, string> top_values = null, T omit = default(T), bool clearlist = true, bool totals = false)
        {
            List<T> filteredlist = filter != null ? list.FindAll(filter) : list;

            if (omit != null)
                filteredlist.Remove(omit);

            if (clearlist)
                data.Clear();

            // vedi sopra
            if (none)
            {
                ListViewItem item = new ListViewItem(new string[] { "-1", "Nessuno" });
                item.Tag = -1;
                data.Add(item);
            }

            if (top_values != null)
            {
                foreach (KeyValuePair<int, string> d in top_values)
                {
                    ListViewItem item = new ListViewItem(new string[] { d.Key != -2 ? d.Key.ToString() : "#", d.Value });
                    item.Tag = d.Key;
                    data.Add(item);
                }
            }

            foreach (var x in filteredlist)
            {
                if (typeof(T) == typeof(Zone)) AddItem(ParentArea, x as Zone);
                else if (typeof(T) == typeof(Room)) AddItem(ParentArea, x as Room);
                else if (typeof(T) == typeof(Mob)) AddItem(ParentArea, x as Mob);
                else if (typeof(T) == typeof(Obj)) AddItem(ParentArea, x as Obj);
                else if (typeof(T) == typeof(Init)) AddItem(ParentArea, x as Init);
                else if (typeof(T) == typeof(Shop)) AddItem(ParentArea, x as Shop);
                else if (typeof(T) == typeof(Spell)) AddItem(ParentArea, x as Spell);
                else if (typeof(T) == typeof(MDFuncData)) AddItem(ParentArea, x as MDFuncData);
                else if (typeof(T) == typeof(MobDialogue)) AddItem(ParentArea, x as MobDialogue);
                else if (typeof(T) == typeof(SoldItem)) AddItem(ParentArea, x as SoldItem);
            }

            if (totals)
            {
                string[] s = new string[7];

                s[0] = "Tot";
                s[1] = "Totale: ";
                s[2] = "";

                switch (current_column)
                {
                    case C.i_report_coinsfame:
                        int fame = 0;
                        int gold = 0;
                        int Ifame = 0;
                        int Igold = 0;
                        foreach (var o in filteredlist)
                        {
                            Mob el = o as Mob;
                            int numinits = 0;
                            foreach (Zone z in ParentArea.zones)
                                foreach (Init i in z.inits.Where<Init>( x => 
                                    (x.type == C.it_mob_add 
                                  || x.type == C.it_follower_add) && x.values[C.iv_value0] == el.vnum))
                                        numinits++;

                            fame += el.fame;
                            gold += el.values[C.mv_gold];
                            Ifame += el.fame * numinits;
                            Igold += el.values[C.mv_gold] * numinits;
                        }

                        s[3] = fame.ToString();
                        s[4] = gold.ToString();
                        s[5] = Ifame.ToString();
                        s[6] = Igold.ToString();
                        break;

                    case C.i_report_treasures:

                        int count = 0;
                        int Icount = 0;
                        foreach (var o in filteredlist)
                        {
                            Obj el = o as Obj;
                            int numinits = 0;
                            foreach (Zone z in ParentArea.zones)
                                foreach (Init i in z.inits.Where<Init>(x => (x.type == C.it_obj_wear
                                                                          || x.type == C.it_obj_put
                                                                          || x.type == C.it_obj_give
                                                                          || x.type == C.it_obj_add) && x.values[C.iv_value0] == el.vnum))
                                    numinits++;

                            count += el.values[0];
                            Icount += el.values[0] * numinits;
                        }

                        s[3] = count.ToString();
                        s[4] = Icount.ToString();       
                        break;
                    case C.i_report_gems:
                        float[] results = new float[4];

                        foreach (var m in filteredlist)
                        {
                            Mob el = m as Mob;
                            foreach (Zone z in ParentArea.zones)
                                foreach (Init i in z.inits.Where<Init>(x =>
                                                                            (x.type == C.it_mob_add || x.type == C.it_follower_add)
                                                                            && x.values[C.iv_value0] == el.vnum))
                                    for (int g = 0; g <= C.gt_end; g++)
                                    {
                                        if (el.gems[g].percent <= 0)
                                            continue;

                                        int x;
                                        int y;
                                        int bonus;

                                        string[] desc = el.gems[g].dice.Split('d');

                                        if (desc.Length <= 1)
                                            continue;

                                        int.TryParse(desc[0], out x);
                                        desc = desc[1].Split(desc[1].Contains('+') ? '+' : '-');

                                        int.TryParse(desc[0], out y);
                                        int.TryParse(desc[1], out bonus);

                                        float med = x * (y + 1) / 2 + bonus;
                                        med = med * el.gems[g].percent / 100;

                                        results[g] += med * i.values[C.iv_percent] / 100;
                                    }
                        }
                        s[1] = "Medie: ";
                        for (int i = 0; i <= C.gt_end; i++)
                            s[3 + i] = results[i].ToString();
                        break;
                    default:
                        s[1] += filteredlist.Count().ToString();
                        break;
                }

                ListViewItem item = new ListViewItem(s);
                item.BackColor = totals_color;
                data.Add(item);
            }

            Redraw();
        }
        #endregion

        #region Exit
        public void AddToList(Area ParentArea, List<Exit> exits, bool none = false, bool clearlist = true)
        {
            if (clearlist)
                data.Clear();

            string[] s = new string[6];
            ListViewItem item;
            if (none)
            {
                s[0] = "-1";
                s[1] = "<Nessuna>";
                item = new ListViewItem(s);
                Items.Add(item);
            }

            area_element a1 = null;
            area_element a2 = null;
            foreach (Exit e in exits)
            {
                object tag = null;

                switch (current_column)
                {
                    // container keys
                    // this is a little hack, we pass the exit itself and override its vnum and shortdesc with its key's
                    case C.i_report_keys:
                        Obj key = ParentArea.Get<Obj>(e.door.objkey);
                        s[0] = e.door.objkey.ToString();
                        s[1] = (key != null ? key.shortdesc : "non trovato");
                        s[2] = "Uscita --> ";
                        s[3] = e.parent != null ? e.parent.vnum.ToString() : "-1";
                        s[4] = e.parent != null ? e.parent.shortdesc : "non trovata";
                        s[5] = L.directions[e.dir] + " (" + e.door.keys + ")";
                        tag = key;
                        break;
                    case C.i_report_externalexits:
                        s[0] = e.parent != null ? e.parent.vnum.ToString() : "-1";
                        s[1] = e.parent != null ? e.parent.shortdesc : "non trovata";
                        s[2] = e.dir == C.dir_special ? e.name : L.Get(L.directions, e.dir) + " -->";
                        s[3] = "[ " + e.room.ToString() + " ]";
                        tag = e;
                        break;
                    default:
                        a1 = ParentArea.Get<Room>(e.room);
                        a2 = ParentArea.Get<Obj>(e.door.objkey);
                        bool isDoor = e.flags[1 << C.df_door];

                        s[0] = e.dir == C.dir_special ? e.name : L.Get(L.directions, e.dir);
                        s[1] = e.room == 0 ? "[0] Uscita Finta" : "[" + e.room + "] " + (a1 != null ? a1.shortdesc : "");
                        s[2] = e.flags[1 << C.df_door] ? L.Get(L.door_status, e.door.status) : "[Libera]";
                        s[3] = e.door.objkey > 0 ? "[" + e.door.objkey.ToString() + "]" + (a2 != null ? a2.shortdesc : "") : 
                            (e.door.objkey == -1 ? "[Nessuna Serratura]" : "[Serratura senza Chiave]");
                        Init init = e.GetDoorInit(ParentArea);
                        s[4] = init != null ? "Ok" : "N/A";
                        s[5] = "";
                        string sf = "";
                        for (int i = 0; i <= C.df_end; i++)
                            sf = sf + (e.flags[1 << i] ? L.Get(L.door_flags_short, i) : "");
                        s[5] = sf != "" ? "[" + sf + "]" : "";
                        tag = e;
                        break;
                }

                item = new ListViewItem(s);
                if (e.flags[1 << C.df_secret])
                    if (e.flags[1 << C.df_invisible])
                         item.BackColor = exits_secret_invisible_color;
                    else item.BackColor = exits_secret_color;

                item.Tag = tag;
                data.Add(item);
            }
            Redraw();
        }
        #endregion
        #endregion

        #region Tooltip
        private void TooltipCheck()
        {
            if (Control.ModifierKeys == Keys.Alt)
                ShowTooltip();
            else tooltip.Hide();
        }

        private void ShowTooltip()
        {
            Point p = PointToClient(MousePosition);
            tooltip.Left = p.X + 50;
            tooltip.Top = p.Y + 25;
            tooltip.Show();
        }

        ListViewItem lastselected = null;
        private void setTooltipData()
        {
            Point p = PointToClient(MousePosition);
            ListViewHitTestInfo ht = HitTest(p);

            tooltip.data = null;

            if (ht.Item == null)
                return;

            tooltip.data = ht.Item.Tag as area_element;

            if (ht.Item != lastselected)
            {
                tooltip.Hide();
                tooltip.GenerateString();
            }

            lastselected = ht.Item;
        }
        #endregion
    }
    #endregion

    #region Characters Counter ProgressBar
    public partial class CharCounterProgressBar : ProgressBar
    {
        Label lblcount;
        Label lblwarning;

        #region Constructor
        public CharCounterProgressBar()
        {
            #region Count Label
            lblcount = new Label();
            lblcount.AutoSize = true;
            lblcount.BackColor = Color.Transparent;
            lblcount.ForeColor = Color.Black;
            lblcount.Name = "lblwarning";
            lblcount.Size = new System.Drawing.Size(59, 13);
            lblcount.Resize += new EventHandler(label_Resize);
            lblcount.Text = "X Caratteri nella riga più lunga";
            #endregion

            #region Warning Label
            lblwarning = new Label();
            lblwarning.AutoSize = true;
            lblwarning.BackColor = Color.Transparent;
            lblwarning.ForeColor = Color.Red;
            lblwarning.Name = "lblwarning";
            lblwarning.Size = new Size(59, 13);
            lblwarning.Resize += new EventHandler(label_Resize);
            lblwarning.Text = "<Attenzione>";
            #endregion

            reset(null, null);
        }
        #endregion

        #region Set
        public void AddEvents(Control control)
        {
            if (control.GetType() != typeof(MudlikeRichTextBox))
                return;

            MudlikeRichTextBox r = control as MudlikeRichTextBox;
            r.TextChanged += new EventHandler(update);
            r.GotFocus += new EventHandler(update);
            r.LostFocus += new EventHandler(reset);
        }
        #endregion

        #region Methods
        private int GetRealLength(string s)
        {
            int result = s.Length;
            String strpattern = @"[$]c[0-9][0-9][0-9][0-9]";
            Regex regex = new Regex(strpattern);

            foreach (Match match in regex.Matches(s))
                result -= 6;

            return result > 0 ? result : 0;
        }

        private void update(object sender, EventArgs e)
        {
            if (!(sender as Control).Focused)
                return;

            string[] l;
            if (sender.GetType() == typeof(TextBox))
                l = (sender as TextBox).Lines;
            else
                l = (sender as RichTextBox).Lines;

            int i = 0;
            foreach (string s in l)
            {
                int len = GetRealLength(s);
                if (len > i)
                    i = len;
            }

            lblcount.Text = i.ToString() + " Caratteri nella riga più lunga";
            i = i * 100 / 80;

            if (i > 100)
            {
                Value = 100;
                lblwarning.Show();
            }
            else
            {
                Value = i;
                lblwarning.Hide();
            }
        }

        private void reset(object sender, EventArgs e)
        {
            lblcount.Text = "";
            Value = 0;
            lblwarning.Hide();
        }
        #endregion

        #region Events
        private void moveLabels()
        {
            lblwarning.Location = new Point(Right - lblwarning.Width - 10, Bottom);
            lblcount.Location = new Point(lblwarning.Left - lblcount.Width, lblwarning.Top);
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            lblwarning.Parent = Parent;
            lblcount.Parent = Parent;
        }

        private void label_Resize(object sender, EventArgs e)
        {
            moveLabels();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            moveLabels();
        }
        #endregion
    }
    #endregion

    #region Mudlike RichTextBox
    public struct colorindexer
    {
        public string s;
        public int i;
        public int l;
        public Color c;
    }

    public partial class MudlikeRichTextBox : RichTextBox
    {
        private string basetext = "";
        private bool mudlikemode;
        private ContextMenuStrip popupmenu;
        public Color DefaultCharacterColor { get; set; }

        private Font basefont;
        private Color basecolor;

        private Font mudlikefont;
        private Color mudlikecolor;

        public string GetText()
        {
            if (mudlikemode)
                return basetext;
            else return Text;
        }

        public void SetText(string s)
        {
            basetext = s;
            Text = s;
        }

        public void SetFont(Font font)
        {
            // not available for short descriptions
            if (!Multiline)
                return;

            basefont = font;
            if (!mudlikemode)
                Font = font;
        }

        #region Constructor
        public MudlikeRichTextBox()
        {
            DefaultCharacterColor = Color.LightGray;
            DetectUrls = false;

            basefont = Font;
            basecolor = BackColor;

            mudlikefont = new Font("Courier New", 9);
            mudlikecolor = Color.Black;

            // preview
            popupmenu = new ContextMenuStrip();
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = "Anteprima";
            item.Tag = 0;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // format to 80 chars
            item = new ToolStripMenuItem();
            item.Text = "Formatta a 80 Caratteri";
            item.Tag = 1;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // separator
            popupmenu.Items.Add("-");
            //cut
            item = new ToolStripMenuItem();
            item.Text = "Taglia";
            item.Click += new EventHandler(mnclick);
            item.Tag = 2;
            popupmenu.Items.Add(item);
            //copy
            item = new ToolStripMenuItem();
            item.Text = "Copia";
            item.Click += new EventHandler(mnclick);
            item.Tag = 3;
            popupmenu.Items.Add(item);
            //paste
            item = new ToolStripMenuItem();
            item.Text = "Incolla";
            item.Tag = 4;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // separator
            popupmenu.Items.Add("-");
            //font dialog
            item = new ToolStripMenuItem();
            item.Text = "Cambia Font..";
            item.Tag = 5;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);

            ContextMenuStrip = popupmenu;

            KeyDown += new KeyEventHandler(keydown);
            MultilineChanged += new EventHandler(multilinechanged);
            TextChanged += new EventHandler(textchanged);
        }
        #endregion

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            basefont = Font;
            SetFont(Options.data.descriptions_font);
            AutoWordSelection = false;
        }

        #region Events
        private void keydown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                Paste(DataFormats.GetFormat(DataFormats.Text));
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void multilinechanged(object sender, EventArgs e)
        {
            if (Multiline)
            {
                SetFont(Options.data.descriptions_font);
                popupmenu.Items[1].Visible = true;
                popupmenu.Items[6].Visible = true;
                popupmenu.Items[7].Visible = true;
            }
            else
            {
                if (basefont == null)
                    basefont = new System.Drawing.Font("Microsoft Sans Serif", 9F);
                popupmenu.Items[1].Visible = false;
                popupmenu.Items[6].Visible = false;
                popupmenu.Items[7].Visible = false;
            }
        }

        private void textchanged(object sender, EventArgs e)
        {   
            string txt = GetText();
            if (txt.IndexOf("~") > -1)
                SetText(txt.Replace("~", ""));

            if (mudlikemode)
                ProcessText();
        }

        private void mnclick(object sender, EventArgs e)
        {
            switch ((int)(sender as ToolStripMenuItem).Tag)
            {
                case 0: MudlikeMode(!mudlikemode); break;
                case 1: Format80Chars();
                        OnKeyDown(new KeyEventArgs(Keys.None)); // force set modified
                        break;
                case 2: Cut();
                        OnKeyDown(new KeyEventArgs(Keys.None)); // force set modified
                        break;
                case 3: Copy(); break;
                case 4: Paste(DataFormats.GetFormat(DataFormats.Text));
                        OnKeyDown(new KeyEventArgs(Keys.None)); // force set modified
                        break;
                case 5: 
                        Options.data.ChangeDescriptionsFont(); 
                        break;
            }
        }
        #endregion

        #region Methods

        public void Format80Chars(int charactersToWrapAt = 80)
        {
            if (!Multiline)
                return;

            string value = Text.Replace("\n", " ").Replace("  ", " ");

            String strpattern = @"[$]c[0-9][0-9][0-9][0-9]";
            Regex regex = new Regex(strpattern);
            List<colorindexer> colorcodes = new List<colorindexer>();
            foreach (Match m in regex.Matches(value))
                colorcodes.Add(new colorindexer() { s = m.Value, i = m.Index + 1 });

            value = utils.CutColorCodes(value);
            var words = value.Split(' ');
            string s = "";
            string currString = "";

            foreach (var word in words)
            {
                if (currString.Length + word.Length + 1 < charactersToWrapAt) // The + 1 accounts for spaces
                {
                    s += " " + word;
                    currString += " " + word;
                }
                else
                {
                    s += "\n" + word;
                    currString = " " + word;
                }
            }

            foreach (colorindexer ci in colorcodes)
            {
                s = s.Substring(0, ci.i) + ci.s + s.Substring(ci.i, s.Length - ci.i);
                //s.Insert(ci.i, );
            }
            Text = s.TrimStart().TrimEnd();
        }

        /// <summary>
        /// Toggles mudlike/normal mode
        /// </summary>
        /// <param name="index"></param>
        public void MudlikeMode(bool val)
        {
            mudlikemode = val;
            if (!val) // normal
            {
                Text = basetext;
                basetext = "";
                ReadOnly = false;
                BackColor = basecolor;
                Font = basefont;
                ResetTextColor();
            }
            else // mudlike
            {
                ReadOnly = true;
                basetext = Text;
                BackColor = Color.Black;
                Font = mudlikefont;
                ProcessText();
            }
        }

        private void ResetTextColor()
        {
            int oldStart = SelectionStart;
            int oldLength = SelectionLength;
            Select(0, TextLength);
            SelectionColor = Color.Black;
            Select(0, 0);
        }

        private void ProcessText()
        {
            TextChanged -= new EventHandler(textchanged);

            // store selection values
            // obsolete
            //int oldStart = SelectionStart;
            //int oldLength = SelectionLength;

            String strpattern = @"[$]c[0-9][0-9][0-9][0-9]";
            Regex regex = new Regex(strpattern);

            Color col = DefaultCharacterColor;
            int cursor = 0;

            Text = basetext;

            List<colorindexer> bp = new List<colorindexer>();

            Match m = null;
            while ((m = regex.Match(Text)).Success)
            {
                string s = m.Value;

                bp.Add(new colorindexer() { i = cursor, l = m.Index, c = col });
                cursor = m.Index;
                col = utils.CodeToColor("$c00" + s[4] + s[5]);

                // cut the color code
                Text = Text.Substring(0, m.Index) + Text.Substring(m.Index + 6, TextLength - (m.Index + 6));
                //if (oldStart > m.Index) oldStart -= 6;
            }

            bp.Add(new colorindexer() { i = cursor, l = TextLength - cursor, c = col } );
            foreach (var x in bp)
            {
                Select(x.i, x.l);
                SelectionColor = x.c;
            }

            bp.Clear();

            // restore selection (obsolete)
            //oldStart = Math.Max(oldStart, 0);

            // reset selection
            Select(0, 0);

            TextChanged += new EventHandler(textchanged);
        }
        #endregion
    }
    #endregion

    #region InitsListView
    public class InitsListView : ListView
    {
        public int separator { get; set; }
        private Area ParentArea;
        private List<Init> initsdata;
        private ListBox listbox;
        private TextBox edt;
        private int sel_item = -1;
        private int sel_sub = -1;
        private event ScrollEventHandler ScrollEH;
        private ContextMenu dummypopupmenu; // to replace the textbox base popupmenu
        private ContextMenuStrip popupmenu;
        private StatusStrip statusbar;
        private AreaElementTooltip tooltip;
        private List<ListViewItem> clipboard;

        #region Fields
        private int Sel_item
        {
            get { return sel_item; }
            set { sel_item = value > -1 && value <= Items.Count ? value : sel_item; }
        }
        private int Sel_sub
        {
            get { return sel_sub; }
            set { sel_sub = value > -1 && value <= Columns.Count ? value : sel_sub; }
        }
        #endregion

        #region Utils
        private bool sel_inbounds(bool columns = true)
        { return columns ? Sel_item > 0 && Sel_item < Items.Count && Sel_sub > 0 && Sel_sub < Columns.Count : Sel_item > 0 && Sel_item < Items.Count; }

        private int GetInitType(int index = -1)
        {
            int i = index > -1 ? index : Sel_item;
            if (i > 0 && i < Items.Count && Items[i].SubItems[0].Text.Length > 0)
                return Init.CharToIndex(Items[i].SubItems[0].Text[0]);
            else return C.it_comment;
        }

        private int GetValue(int value)
        {
            int tmp = 0;
            int.TryParse(Items[Sel_item].SubItems[value + 2].Text, out tmp);
            return tmp;
        }

        private bool MaxValueSelected()
        {
            switch (GetInitType())
            {
                case C.it_mob_add:
                case C.it_follower_add:
                case C.it_obj_wear:
                case C.it_obj_give:
                case C.it_obj_put:
                    return Sel_sub == 3;
                case C.it_obj_add:
                    return Sel_sub == 3 || sel_sub == 5;
                    
                default: return false;
            }
        }

        public void GenerateComment(ListViewItem item)
        {
            item.SubItems[7].Text = ReadInit(item).GenerateComment(ParentArea);
            item.SubItems[6].Text = L.Get(L.separators, separator);
        }
        #endregion

        #region OnScroll Event (WndProc)
        private void OnScroll(ScrollEventArgs e)
        {
            ScrollEventHandler handler = ScrollEH;
            if (handler != null) 
                handler(this, e);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115)
            { // Trap WM_VSCROLL
                OnScroll(new ScrollEventArgs((ScrollEventType)(m.WParam.ToInt32() & 0xffff), 0));
            }
        }
        #endregion

        #region List2Inits <-> Inits2List
        private Init ReadInit(ListViewItem item)
        {
            Init init = new Init();
            init.type = Init.CharToIndex(item.SubItems[0].Text.Length > 0 ? item.SubItems[0].Text[0] : ' ');
            if (init.type != C.it_comment)
            {
                int tmp = 0;
                int.TryParse(item.SubItems[1].Text, out tmp);
                init.values[C.iv_percent] = tmp;
                for (int i = 2; i <= 5; i++)
                {
                    // special exits hack
                    bool isNumber = int.TryParse(item.SubItems[i].Text, out tmp);
                    if (!isNumber && i - 2 == C.iv_value1 && GetInitType(item.Index) == C.it_door_init)
                    {
                        init.se_name = item.SubItems[i].Text;
                        init.values[i - 2] = C.dir_special;
                    }
                    else
                        init.values[i - 2] = tmp;
                }

                if (init.type != C.it_obj_add)
                    init.values[C.iv_value3] = 0;
            }
            else for (int i = C.iv_value0; i <= C.iv_value3; i++)
                    init.values[i] = 0;

            init.separator = item.SubItems[6].Text == ";" ? 1 : 0;
            init.shortdesc = item.SubItems[7].Text;
            return init;
        }

        public void ListToInits(List<Init> inits)
        {
            inits.Clear();
            Init curr = null;
            foreach (ListViewItem i in Items)
            {
                curr = ReadInit(i);
                inits.Add(curr);
            }
        }

        private void WriteInit(Init i)
        {
            string[] l = new string[8];
            l[0] = Init.IndexToChar(i.type).ToString();
            if (i.type != C.it_comment)
            {
                l[1] = i.values[C.iv_percent].ToString();
                l[2] = "";
                int numvalues = i.type == C.it_obj_add ? 3 : 2;
                for (int j = 2; j <= numvalues + 2; j++)
                {
                    if (i.type == C.it_door_init && j - 2 == C.iv_value1 && i.values[j - 2] == C.dir_special)
                        l[j] = l[j] + i.se_name;
                    else
                        l[j] = l[j] + i.values[j - 2].ToString();
                }
            }
            l[6] = L.Get(L.separators, i.separator);
            l[7] = i.shortdesc;

            ListViewItem item = new ListViewItem(l);
            Items.Add(item);
        }

        public void InitsToList(List<Init> inits)
        {
            initsdata = inits;
            Items.Clear();
            foreach (Init i in initsdata)
                WriteInit(i);

            Redraw();
        }
        #endregion

        #region Constructor
        // components
        public void SetComponents(Area a, ListBox l, TextBox e, StatusStrip s)
        {
            ParentArea = a;
            edt = e;
            edt.TextChanged += new EventHandler(edttextchanged);
            edt.KeyDown += new KeyEventHandler(edtkeydown);
            edt.MouseUp += new MouseEventHandler(onmouseup);
            dummypopupmenu = new ContextMenu();
            edt.ContextMenu = dummypopupmenu;
            edt.Hide();
            listbox = l;
            listbox.Hide();
            statusbar = s;
            PopupMenuInit();
            tooltip = new AreaElementTooltip();
            tooltip.Parent = Parent;
            tooltip.BackColor = Color.Black;
            tooltip.BringToFront();
        }

        // list
        public InitsListView()
        {
            MouseUp += new MouseEventHandler(onmouseup);
            KeyDown += new KeyEventHandler(onkeydown);
            Resize += new EventHandler(moveedt);
            ScrollEH += new ScrollEventHandler(moveedt);
            MouseMove += new MouseEventHandler(OnMouseMove);
            clipboard = new List<ListViewItem>();
        }
        #endregion

        #region Popup Menus
        private void PopupMenuInit()
        {
            // preview
            popupmenu = new ContextMenuStrip();
            popupmenu.ShowImageMargin = false;
            // insert
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = "Inserisci";
            item.Tag = 0;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // separator
            item = new ToolStripMenuItem();
            item.Text = "-";
            popupmenu.Items.Add(item);
            // cut
            item = new ToolStripMenuItem();
            item.Text = "Taglia";
            item.Tag = 5;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // copy
            item = new ToolStripMenuItem();
            item.Text = "Copia";
            item.Tag = 6;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // paste
            item = new ToolStripMenuItem();
            item.Text = "Incolla";
            item.Tag = 7;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // delete
            item = new ToolStripMenuItem();
            item.Text = "Elimina";
            item.Tag = 1;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // separator
            item = new ToolStripMenuItem();
            item.Text = "-";
            popupmenu.Items.Add(item);
            // generate comment
            item = new ToolStripMenuItem();
            item.Text = "Genera Commento";
            item.Tag = 2;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // update max values
            item = new ToolStripMenuItem();
            item.Text = "Aggiorna maxaggi a questo valore";
            item.Tag = 3;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
            // update max values
            item = new ToolStripMenuItem();
            item.Text = "Aggiorna maxaggi al numero di Init";
            item.Tag = 4;
            item.Click += new EventHandler(mnclick);
            popupmenu.Items.Add(item);
        }

        private void mnclick(object sender, EventArgs e)
        {
            int id = (int)(sender as ToolStripMenuItem).Tag;
            switch (id)
            {
                case 0: Do_Add(); break;
                case 1: Do_Del(); break;
                case 2: foreach (ListViewItem i in SelectedItems)
                            if (GetInitType() != C.it_comment)
                                GenerateComment(i); break;
                case 3:
                case 4: UpdateMax(id); break;
                case 5: Do_Cut(); break;
                case 6: Do_Copy(); break;
                case 7: Do_Paste(); break;
            }
        }
        #endregion

        #region Special Actions
        public void Do_Cut()
        {
            Do_Copy();
            Do_Del();
        }

        public void Do_Copy()
        {
            clipboard.Clear();
            foreach (ListViewItem item in SelectedItems)
                clipboard.Add(item);
        }

        public void Do_Paste()
        {
            if (clipboard.Count <= 0)
                return;

            int startid = SelectedIndices.Count > 0 ? SelectedIndices[SelectedIndices.Count - 1] + 1 : Items.Count;

            foreach (ListViewItem i in SelectedItems)
                i.Selected = false;

            int counter = 0;
            foreach (ListViewItem item in clipboard)
            {
                Do_Add(startid + counter, null, item.Clone() as ListViewItem, false);
                counter++;
            }
        }

        public void CreateContainersObjRem()
        {
            foreach (ListViewItem item in SelectedItems)
            {
                if (item.SubItems[0].Text == "O")
                {
                    int vnum;
                    int.TryParse(item.SubItems[2].Text, out vnum);
                    Obj o = ParentArea.Get<Obj>(vnum);

                    if (o == null || o.properties[C.op_type] != C.ot_container)
                        continue;
                    string[] l = new string[8];
                    l[0] = Init.IndexToChar(C.it_obj_rem).ToString();
                    l[1] = "100";
                    l[2] = vnum.ToString();
                    l[3] = item.SubItems[4].Text;
                        
                    ListViewItem rem = Do_Add(item.Index, l);
                }
            }
            Redraw();
        }

        public void UpdateMax(int id)
        {
            if (!MaxValueSelected())
                return;

            int result = 0;
            if (id == 3)
                int.TryParse(Items[Sel_item].SubItems[Sel_sub].Text, out result);
            else
                for (int i = 0; i < Items.Count; i++)
                {
                    if (GetInitType() == GetInitType(i) && Items[Sel_item].SubItems[2].Text == Items[i].SubItems[2].Text &&
                        Items[Sel_item].SubItems[4].Text == Items[i].SubItems[4].Text)
                        result++;
                }

                for (int i = 0; i < Items.Count; i++)
                    if (GetInitType() == GetInitType(i) && Items[Sel_item].SubItems[2].Text == Items[i].SubItems[2].Text &&
                        Items[Sel_item].SubItems[4].Text == Items[i].SubItems[4].Text)
                        Items[i].SubItems[Sel_sub].Text = result.ToString();
                Redraw();
        }

        public void TrimSelection()
        {
            if (SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem item in SelectedItems)
                if (item.SubItems[0].Text == " " && item.SubItems[7].Text == "")
                    Items.Remove(item);
            Redraw();
        }
        #endregion

        #region AddItem DelItem
        public ListViewItem Do_Add(int id = -1, string[] data = null, ListViewItem cc = null, bool clearselection = true)
        {
            int index = id;
            if (SelectedIndices.Count > 0 && index < 0)
                index = SelectedIndices[0] + 1;

            if (index < 0)
                index = 0;

            ListViewItem result = null;
            if (cc != null)
                result = cc;
            else
            {
                string[] l = null;
                if (data != null)
                    l = data;
                else
                    l = new string[8];

                for (int i = 0; i <= 7; i++)
                    if (l[i] == null)
                        l[i] = "";
            
                if (separator == 1)
                    l[6] = L.Get(L.separators, 1);
                else l[6] = L.Get(L.separators, 0);

                result = new ListViewItem(l);
            }

            if (clearselection)
                foreach (ListViewItem i in SelectedItems)
                    i.Selected = false;

            if (index < 0)
                index = 0;
            else if (index > Items.Count)
                index = Items.Count;
            Items.Insert(index, result);
            if (Options.data.init_automatic_comment)
                GenerateComment(result);

            result.Selected = true;
            result.EnsureVisible();

            return result;
        }

        public void Do_Del()
        {
            if (SelectedItems.Count <= 0)
                return;

            foreach (ListViewItem i in SelectedItems)
                i.Remove();
        }
        #endregion

        #region TextBox Management
        private void setEdt(int item, int subitem)
        {
            if (item < 0 || item > Items.Count || subitem < 0 || subitem > Columns.Count)
            {
                edt.Hide();
                Sel_item = -1;
                Sel_sub = -1;
                return;
            }

            ListViewItem.ListViewSubItem sub = Items[item].SubItems[subitem];
            Sel_item = item;
            Sel_sub  = subitem;
            edt.Tag = sub;
            edt.Text = sub.Text;
            listbox.Hide();
            edt.Show();
            moveedt(null, null);
            Items[item].Selected = true;
            Items[item].EnsureVisible();
            edt.SelectionStart = 0;
            edt.SelectAll();
            edt.Focus();
        }

        private void moveedt(object sender, EventArgs e)
        {
            if (edt == null || !edt.Visible || edt.Tag == null) return;

            ListViewItem.ListViewSubItem sub = (edt.Tag as ListViewItem.ListViewSubItem);

            edt.Bounds = new Rectangle(sub.Bounds.Left + Left, sub.Bounds.Top + Top, sub.Bounds.Width, sub.Bounds.Height);
            if (Sel_sub == 0) edt.Width = Columns[0].Width;

            listbox.Left = edt.Left;
            listbox.Top = edt.Bottom;
        }

        private void edttextchanged(object sender, EventArgs e)
        {
            if (edt.Tag == null) return;
            ListViewItem.ListViewSubItem item = (edt.Tag as ListViewItem.ListViewSubItem);
            item.Text = edt.Text;
            if (Sel_sub == 0 && item.Text != "")
                switch (Init.CharToIndex(item.Text[0]))
                {
                    case C.it_mob_add:
                    case C.it_mob_rem: item.BackColor = Color.FromArgb(255, 255, 230, 230); break;
                    case C.it_follower_add: item.BackColor = Color.FromArgb(255, 255, 245, 245); break;
                    case C.it_mob_fear:
                    case C.it_mob_hate: item.BackColor = Color.FromArgb(255, 245, 255, 245); break;
                    case C.it_obj_add:
                    case C.it_obj_rem: item.BackColor = Color.FromArgb(255, 230, 230, 255); break;
                    case C.it_obj_give:
                    case C.it_obj_put:
                    case C.it_obj_wear: item.BackColor = Color.FromArgb(255, 245, 245, 255); break;
                    case C.it_door_init: item.BackColor = Color.FromArgb(255, 240, 230, 230); break;
                    default: item.BackColor = Color.White; break;
                }
            if (Options.data.init_automatic_comment)
                GenerateComment(Items[Sel_item]);
            AutoResizeColumn(Sel_sub, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void edtkeydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.Right && edt.SelectionStart == edt.Text.Length))
            {
                if (listbox.Visible && e.KeyCode == Keys.Enter)
                {
                    SelectItem();
                }
                else {
                    Sel_sub++;
                    if (Sel_sub >= Columns.Count)
                    {
                        Sel_sub = 0;
                        Sel_item++;
                    }
                    setEdt(Sel_item, Sel_sub);
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Space && e.Control)
            {
                if (!listbox.Visible)
                    if (populateListBox())
                    {
                        listbox.BringToFront();
                        listbox.Show();
                    }
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Left && edt.SelectionStart == 0)
            {
                if (Sel_sub == 0)
                {
                    Sel_sub = Columns.Count - 1;
                    Items[Sel_item].Selected = false;
                    Sel_item--;
                }
                else Sel_sub--;
                setEdt(Sel_item, Sel_sub);
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (listbox.Visible)
                {
                    if (listbox.SelectedIndex > 0)
                        listbox.SelectedIndex--;

                    e.Handled = true;
                }
                else
                {
                    Items[Sel_item].Selected = false;
                    Sel_item--;
                    setEdt(Sel_item, Sel_sub);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (listbox.Visible)
                {
                    if (listbox.SelectedIndex < listbox.Items.Count - 1)
                        listbox.SelectedIndex++;

                    e.Handled = true;
                }
                else
                {
                    Items[Sel_item].Selected = false;
                    Sel_item++;
                    setEdt(Sel_item, Sel_sub);
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                if (listbox.Visible)
                {
                    if (listbox.SelectedIndex >= 10)
                        listbox.SelectedIndex -= 10;
                    else listbox.SelectedIndex = 0;

                    e.Handled = true;
                }
                else
                {
                    Items[Sel_item].Selected = false;
                    Sel_item = Math.Max(0, Sel_item - 10);
                    setEdt(Sel_item, Sel_sub);
                }
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                if (listbox.Visible)
                {
                    if (listbox.SelectedIndex < listbox.Items.Count - 10)
                        listbox.SelectedIndex += 10;
                    else listbox.SelectedIndex = listbox.Items.Count - 1;

                    e.Handled = true;
                }
                else
                {
                    Items[Sel_item].Selected = false;
                    Sel_item = Math.Min(Items.Count - 1, Sel_item + 10);
                    setEdt(Sel_item, Sel_sub);
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                listbox.Hide();
                e.Handled = true;
            }

        }
        #endregion

        #region Draw
        private void ReDrawRowsBackground()
        {
            for (int k = 0; k < Items.Count; k++)
            {
                if (Items[k].SubItems[0].Text.Length <= 0)
                    continue;

               switch (Init.CharToIndex(Items[k].SubItems[0].Text[0]))
               {
                   case C.it_mob_add:
                   case C.it_mob_rem: Items[k].BackColor = Color.FromArgb(255, 255, 230, 230); break;
                   case C.it_follower_add: Items[k].BackColor = Color.FromArgb(255, 255, 245, 245); break;
                   case C.it_mob_fear:
                   case C.it_mob_hate: Items[k].BackColor = Color.FromArgb(255, 245, 255, 245); break;
                   case C.it_obj_add:
                   case C.it_obj_rem: Items[k].BackColor = Color.FromArgb(255, 230, 230, 255); break;
                   case C.it_obj_give:
                   case C.it_obj_put:
                   case C.it_obj_wear: Items[k].BackColor = Color.FromArgb(255, 245, 245, 255); break;
                   case C.it_door_init: Items[k].BackColor = Color.FromArgb(255, 240, 230, 230); break;
                   default: Items[k].BackColor = Color.White; break;
               }
            }
        }

        public void Redraw()
        {
            BeginUpdate();
            ReDrawRowsBackground();
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            if (Columns.Count > 0)
            {
                // set the last column's width to fit the listview's width if smaller
                int i = 0;
                foreach (ColumnHeader c in Columns)
                    i += c.Width;
                if (i < Width)
                    Columns[Columns.Count - 1].Width = Columns[Columns.Count - 1].Width + Width - i - 30;
            }
            EndUpdate();
            Refresh();
        }
        #endregion

        #region Item Drag&Drop
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            base.OnItemDrag(e);
            DoDragDrop(SelectedItems, DragDropEffects.Move);
        }
 
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
            int len = drgevent.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (drgevent.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                    drgevent.Effect = DragDropEffects.Move;
            }
        }
 
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (SelectedItems.Count == 0)
                return;

            Point cp = PointToClient(new Point(drgevent.X, drgevent.Y));
            ListViewItem dragToItem = GetItemAt(cp.X, cp.Y);
            if (dragToItem == null || dragToItem.Selected)
                return;

            int dragIndex = dragToItem.Index;
            ListViewItem[] sel = new ListViewItem[SelectedItems.Count];
            for (int i = 0; i <= SelectedItems.Count - 1; i++)
                sel[i] = SelectedItems[i];

            int firstindex = sel[0].Index;
            for (int i = 0; i < sel.GetLength(0); i++)
            {
                ListViewItem dragItem = sel[i];
                int itemIndex = dragIndex;

                if (itemIndex == dragItem.Index)
                    return;

                if (dragItem.Index < itemIndex)
                    itemIndex += 1 + (dragItem.Index - firstindex);
                else
                    itemIndex = dragIndex + (dragItem.Index - firstindex);

                ListViewItem insertItem = (ListViewItem)dragItem.Clone();
                Items.Insert(itemIndex, insertItem);
                Items.Remove(dragItem);
                insertItem.Selected = true;
            }
        }
        #endregion

        #region List MouseUp
        ListViewItem.ListViewSubItem lastselected = null;
        private void onmouseup(object sender, MouseEventArgs e)
        {
            ListViewItem item = null;
            ListViewItem.ListViewSubItem sub = null;
            if (sender == edt)
            {
                item = Items[Sel_item];
                if (item == null)
                    return;
                sub = item.SubItems[Sel_sub];
            }
            else
            {
                edt.Hide();
                item = GetItemAt(e.X, e.Y);
                if (item == null)
                    return;
                sub = item.GetSubItemAt(e.X, e.Y);
            }

            if (sub == null)
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (sub != lastselected)
                {
                    lastselected = sub;
                    listbox.Hide();
                    return;
                }
                setEdt(Items.IndexOf(item), item.SubItems.IndexOf(sub));
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!item.Selected)
                    return;

                if (sub != lastselected)
                {
                    lastselected = sub;
                    listbox.Hide();
                }

                setEdt(Items.IndexOf(item), item.SubItems.IndexOf(sub));
                edt.Hide();

                if (Sel_sub == 7)
                {
                    popupmenu.Items[0].Visible = true;
                    popupmenu.Items[1].Visible = true;
                    popupmenu.Items[2].Visible = true;
                    popupmenu.Items[3].Visible = true;
                    popupmenu.Items[4].Visible = true;
                    popupmenu.Items[5].Visible = true;
                    popupmenu.Items[6].Visible = true;
                    popupmenu.Items[7].Visible = true;
                    popupmenu.Items[8].Visible = false;
                    popupmenu.Items[9].Visible = false;
                    popupmenu.Show(MousePosition);
                }
                else if (MaxValueSelected())
                {
                    popupmenu.Items[0].Visible = false;
                    popupmenu.Items[1].Visible = false;
                    popupmenu.Items[2].Visible = false;
                    popupmenu.Items[3].Visible = false;
                    popupmenu.Items[4].Visible = false;
                    popupmenu.Items[5].Visible = false;
                    popupmenu.Items[6].Visible = false;
                    popupmenu.Items[7].Visible = false;
                    popupmenu.Items[8].Visible = true;
                    popupmenu.Items[9].Visible = true;
                    popupmenu.Show(MousePosition);
                }
                else
                    if (populateListBox())
                    {
                        listbox.BringToFront();
                        listbox.Show();
                    }
                    else listbox.Hide();
            }
        }
        #endregion

        #region List KeyDown
        private void onkeydown(object sender, KeyEventArgs e)
        {
            if ((e.Shift && e.KeyCode == Keys.Insert) || (e.Control && e.KeyCode == Keys.V))
                Do_Paste();
            else if ((e.Shift && e.KeyCode == Keys.Delete) || (e.Control && e.KeyCode == Keys.X))
                Do_Cut();
            else if (e.Control && e.KeyCode == Keys.C)
                Do_Copy();
            else if (e.KeyCode == Keys.Insert)
                Do_Add();
            else if (e.KeyCode == Keys.Delete)
                Do_Del();

            TooltipCheck();
        }
        #endregion

        #region Initellisense (no, this isn't a typo)
        private string ShowElementsDlg<T>(int id, List<T> list, Predicate<area_element> filter = null)
        {
            string result = "";
            using (dlg_select_element form = new dlg_select_element())
            {
                form.SetElements<T>(ParentArea, id, list, false, filter);
                if (form.ShowDialog() == DialogResult.OK)
                {

                    if (result != null)
                     if (form.result.GetType() == typeof(int))
                            result = form.result.ToString();
                     else if (typeof(T) == typeof(Exit))
                     {
                         Exit e = form.result as Exit;
                         if (e.dir < C.dir_special)
                             result = e.dir.ToString();
                         else result = e.name;
                     }
                     else result = (form.result as area_element).vnum.ToString();
                }
            }
            return result;
        }

        private bool populateListBox()
        {
            string[] items = null;

            listbox.Items.Clear();

            string s = "";

            switch (Sel_sub)
            {
                case 0: // init type
                    items = L.init_types;
                    break;
                //case 1: break;  // percent
                case 2:  // value0
                    switch (GetInitType())
                    {
                        case C.it_mob_add:
                        case C.it_mob_rem:
                        case C.it_follower_add:
                            s = ShowElementsDlg<Mob>(C.i_mob, ParentArea.mobs);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        case C.it_mob_fear:
                        case C.it_mob_hate:
                            items = L.fear_types;
                            break;
                        case C.it_obj_add:
                        case C.it_obj_rem:
                        case C.it_obj_give:
                        case C.it_obj_wear:
                        case C.it_obj_put:
                            s = ShowElementsDlg<Obj>(C.i_obj, ParentArea.objects);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        case C.it_door_init:
                            s = ShowElementsDlg<Room>(C.i_room, ParentArea.rooms);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        default: return false;
                    }
                    break;
                case 3: // value1
                    switch (GetInitType())
                    {
                        case C.it_mob_fear:
                        case C.it_mob_hate:
                            items = Init.GetFearHateValue1(GetValue(0));
                            if (items == null)
                                return false;
                            break;
                        case C.it_mob_rem:
                        case C.it_obj_rem:
                            s = ShowElementsDlg<Room>(C.i_room, ParentArea.rooms);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        case C.it_door_init:
                            int vnum;
                            int.TryParse(Items[Sel_item].SubItems[2].Text, out vnum);
                            Room r = ParentArea.Get<Room>(vnum);
                            if (r != null)
                            {
                                s = ShowElementsDlg<Exit>(C.i_exit, r.exits);
                                if (s != "")
                                {
                                    SelectItem(s);
                                }
                                return false;
                            }

                            items = L.directions;
                            break;
                        default: return false;
                    }
                    break;
                case 4: //value2
                    switch (GetInitType())
                    {
                        case C.it_obj_wear:
                            items = L.init_wear;
                            break;
                        case C.it_mob_add:
                        case C.it_obj_add:
                        case C.it_follower_add:
                            s = ShowElementsDlg<Room>(C.i_room, ParentArea.rooms);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        case C.it_obj_put:
                            s = ShowElementsDlg<Obj>(C.i_obj, ParentArea.objects, x => (x as Obj).properties[C.op_type] == C.ot_container);
                            if (s != "")
                                SelectItem(s);
                            return false;
                        case C.it_door_init:
                            items = L.door_status;
                            break;
                        default: return false;
                    }
                    break;
                default: return false;
            }

            if (items == null) 
                return false;

            foreach (string s1 in items)
                listbox.Items.Add(s1);

            return true;
        }

        public void SelectItem(string result = null)
        {
            string fill = result;
            if (fill == null)
                switch (Sel_sub)
                {
                    case 0: fill = Init.IndexToChar(listbox.SelectedIndex).ToString(); break; // init types
                    default: fill = listbox.SelectedIndex.ToString(); break;
                }

            foreach (ListViewItem item in SelectedItems)
            {
                Init i = item.Tag as Init;
                if (i != null && Sel_sub > 0 && i.type == C.it_comment)
                {
                    // no values for comments
                }
                else item.SubItems[Sel_sub].Text = fill;
            }

            if (Sel_sub == 0)
                ReDrawRowsBackground();

            if (Options.data.init_automatic_comment)
                GenerateComment(Items[Sel_item]);

            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listbox.Hide();
        }
        #endregion

        #region Tooltip
        private void TooltipCheck()
        {
            if (Control.ModifierKeys == Keys.Alt)
                ShowTooltip();
            else tooltip.Hide();
        }

        private void ShowTooltip()
        {
            Point p = PointToClient(MousePosition);
            tooltip.Left = p.X + 30;
            tooltip.Top = p.Y + 25;
            tooltip.Show();
        }

        private void setTooltipData()
        {
            Point p = PointToClient(MousePosition);
            ListViewHitTestInfo ht = HitTest(p);

            if (ht.SubItem == null)
                return;

            int i = ht.Item.SubItems.IndexOf(ht.SubItem);

            tooltip.data = null;
            tooltip.Hide();
            int vnum;
            int.TryParse(ttsub.Text, out vnum);

            if (vnum <= 0)
                return;

            switch (i)
            {
                case 2:  // value0
                    switch (GetInitType(ht.Item.Index))
                    {
                        case C.it_mob_add:
                        case C.it_mob_rem:
                        case C.it_follower_add:
                            tooltip.data = ParentArea.Get<Mob>(vnum); break;
                        case C.it_obj_add:
                        case C.it_obj_rem:
                        case C.it_obj_give:
                        case C.it_obj_wear:
                        case C.it_obj_put:
                            tooltip.data = ParentArea.Get<Obj>(vnum); break;
                        case C.it_door_init:
                            tooltip.data = ParentArea.Get<Room>(vnum); break;
                        default: return;
                    }
                    break;
                case 3: // value1
                    switch (GetInitType(ht.Item.Index))
                    {
                        case C.it_mob_rem:
                        case C.it_obj_rem:
                            tooltip.data = ParentArea.Get<Room>(vnum); break;
                        default: return;
                    }
                    break;
                case 4: //value2
                    switch (GetInitType(ht.Item.Index))
                    {
                        case C.it_mob_add:
                        case C.it_obj_add:
                        case C.it_follower_add:
                            tooltip.data = ParentArea.Get<Room>(vnum); break;
                        case C.it_obj_put:
                            tooltip.data = ParentArea.Get<Obj>(vnum); break;
                        default: return;
                    }
                    break;
                default: return;
            }

            tooltip.GenerateString();
        }
        #endregion

        #region Events
        protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        {
            base.OnColumnWidthChanged(e);
            moveedt(null, e);
        }

        ListViewItem.ListViewSubItem ttsub = null;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo ht = HitTest(e.Location);
            if (ht.SubItem != null && ht.SubItem != ttsub)
            {
                int i = ht.Item.SubItems.IndexOf(ht.SubItem);

                statusbar.Items[0].Text = "";

                switch (i)
                {
                    case 0: statusbar.Items[0].Text = "Tipo"; break;
                    case 1: statusbar.Items[0].Text = "Percentuale (1 - 100)"; break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        statusbar.Items[0].Text = L.init_labels[GetInitType(ht.Item.Index), i - 2];
                        break;
                    case 6: statusbar.Items[0].Text = "Separatore"; break;
                    case 7: statusbar.Items[0].Text = "Commento"; break;
                }
                ttsub = ht.SubItem;

                setTooltipData();
            }

            TooltipCheck();
        }
        #endregion
    }
    #endregion

    #region SearchTextBox
    public partial class SearchTextBox : TextBox
    {
        const int spacer = 2;
        public Image ButtonImage { get; set; }
        Label lbl;
        Button btn;
        public HoGListView list { get; set; }

        #region Constructor
        public SearchTextBox()
        {
           // label
            lbl = new Label();
            lbl.Parent = Parent;
            lbl.AutoSize = true;
            lbl.BackColor = Color.Transparent;
            lbl.ForeColor = Color.Black;
            lbl.Name = Name + "_lbl";
            lbl.Left = Left - lbl.Width - spacer;
            lbl.Text = "Cerca: ";
           // button
            btn = new Button();
            btn.Parent = Parent;
            btn.Name = Name + "_btn";
            btn.Width = Height;
            btn.Height = Height;
            btn.Left = Right + spacer;
            btn.Top = spacer;
            btn.BackColor = SystemColors.Control;
            btn.Text = "";
            btn.Click += new EventHandler(resetsearch_Click);
            
            Resize += new EventHandler(resize);
            TextChanged += new EventHandler(searchbox_TextChanged);  
        }
        #endregion

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (ButtonImage != null)
                btn.Image = ButtonImage;
            else
                btn.Text = "X";
        }

        #region Events
        private void onresize()
        {
            lbl.Top = Top + spacer;
            lbl.Left = Left - lbl.Width - spacer;
            btn.Top = Top;
            btn.Left = Right + spacer;
        }

        private void searchbox_TextChanged(object sender, EventArgs e)
        {
            if (list == null)
                return;

            list.Filter = Text;
            list.Redraw();
        }

        private void resetsearch_Click(object sender, EventArgs e)
        {
            Text = "";
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            lbl.Parent = Parent;
            btn.Parent = Parent;
        }

        private void resize(object sender, EventArgs e)
        {
            onresize();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            onresize();
        }
        #endregion
    }
    #endregion

    #region Values & Affects Widget (ComboBox, NumericUpDown, Button overlapped)
    public partial class ValAffEditbox : NumericUpDown
    {
        #region Vars
        private char valuetype;
        private BitVector32 bv;
        public int specialData;
        private Area ParentArea;
        // Components
        // we don't need a numericupdown because *this* is the numericupdown
        private ComboBox combo;
        private Button btn;
        private ContextMenuStrip menu;
        // 17/10/21 Saregon - affects
        int baseSizeX = 109;
        NumericUpDown num2, num3, num4;
        public int val2, val3, val4;


        //
        #endregion
        // avoid overlapping of multiple menus
        public static ContextMenuStrip lastopenedmenu;

        #region Fields
        public void SetValue(int i)
        {
            Value = i;
            SetBitVector();
            RefreshComponents();
        }

        public char ValueType
        {
            get { return valuetype; }
            set
            {
                valuetype = value;
                if (hasmenu)
                    menu.Close();
                SetVisual();
                Minimum = int.MinValue;
                Maximum = int.MaxValue;
            }
        }

        /// <summary>
        /// </summary>
        /// <returns>-1 Nothing, 0 == NumericUpDown, 1 == ComboBox, 2 == Button</returns>
        private int GetWidget()
        {
            switch (valuetype)
            {
                case 'G':
                case 'X':
                case 'R':
                case 'M':
                case 'L':
                case 'Y': return 1;
                case 'F':
                case 'S':
                case 'A':
                case 'E':
                case 'C':
                case 'K':
                case 'I':
                case 'T': return 2;
                // 17/10/21 Saregon - affects
                case 'Q': return 3;
                default: return 0;
                case '_': return -1;
            }
        }

        private bool hascombo { get { return combo != null; } }
        private bool hasbtn { get { return btn != null; } }
        private bool hasmenu { get { return menu != null; } }
        // 17/10/21 Saregon - affects
        private bool hasnums { get { return num2 != null; } }

        public void SetArea(Area a)
        { if (ParentArea == null) ParentArea = a; }
        #endregion

        #region Utils
        private void SetBitVector()
        {
            if (!hasbtn)
                return;

            for (int j = 0; j <= 31; j++)
                bv[1 << j] = false;

            bv[Convert.ToInt32(Value)] = true;
        }

        private string[] getstrings(bool brief = false)
        {
            switch (valuetype)
            {
               // values & affects
                case 'C': return brief ? L.container_flags_short : L.container_flags;
                case 'K': return brief ? L.liquid_flags_short : L.liquid_flags;
                case 'G': return L.alignment_names;
                case 'E': return brief ? L.damage_types_short : L.damage_types;
                case 'A': return brief ? L.mob_affects_short : L.mob_affects;
                case 'X': return L.mob_gender;
                case 'L': return L.liquid_types;
                case 'I': return L.container_key;
                case 'R': return L.mob_races;
                case 'M': return L.melee_damage_types;
                case 'T': return brief ? L.trap_trigger_types_short : L.trap_trigger_types;
                case 'Y': return L.trap_damage_types;
                default: return null;
            }
        }
        #endregion

        #region Constructors
        public ValAffEditbox()
        {
            Resize += new EventHandler(onResize);
        }

        private void createcombo()
        {
            if (hascombo)
                return;

            combo = new ComboBox();
            combo.Parent = Parent;
            combo.Name = Name + "_combo";
            combo.Size = Size;
            combo.Location = Location;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            combo.SelectedIndexChanged += new EventHandler(combo_onValueChanged);
        }

        private void createbtn()
        {
            if (!hasbtn)
            {
                btn = new Button();
                btn.Parent = Parent;
                btn.Name = Name + "_btn";
                btn.Size = Size;
                btn.Location = Location;
                btn.BackColor = SystemColors.Control;
                btn.Click += new EventHandler(btn_onClick);
            }
            switch (valuetype)
            {
                case 'C':
                case 'K':
                case 'I': createmenu();
                    if (valuetype != 'I')
                        bv = new BitVector32();
                    break;
            }
        }
        
        // 17/10/21 Saregon - affects
        void createNums()
        {
            if (hasnums)
                return;

            num2= new NumericUpDown();
            num2.Parent = Parent;
            num2.Name = Name + "_numupdown2";
            num2.Size = Size;
            num2.Location = Location;
            num2.Tag = 2;
            num2.ValueChanged += new EventHandler(num_onValueChanged);

            num3 = new NumericUpDown();
            num3.Parent = Parent;
            num3.Name = Name + "_numupdown3";
            num3.Size = Size;
            num3.Location = Location;
            num3.Tag = 3;
            num3.ValueChanged += new EventHandler(num_onValueChanged);

            num4 = new NumericUpDown();
            num4.Parent = Parent;
            num4.Name = Name + "_numupdown4";
            num4.Size = Size;
            num4.Location = Location;
            num4.Tag = 4;
            num4.ValueChanged += new EventHandler(num_onValueChanged);
        }
        //

        private void createmenu()
        {
            if (hasmenu)
                return;

            menu = new ContextMenuStrip();
            menu.Name = Name + "_menu";
            for (int i = 0; i <= 6; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = menu.Name + "_item" + i;
                if (i <= 3)
                    item.Click += new EventHandler(menu_itemclick);
                menu.Items.Add(item);
            }
            menu.Items[4].Text = " ";
            menu.Items[5].Text = "Chiudi";
            menu.Items[5].Click += new EventHandler(menu_closeclick);
            menu.Items[6].Text = "Trova..";
            menu.Items[6].Click += new EventHandler(menu_finditemclick);
            fillmenu();
            menu.Opening += new System.ComponentModel.CancelEventHandler(menu_Opening);
        }
        #endregion

        #region Set Values
        /// <summary>
        /// fills the combo with the appropriate items
        /// </summary>
        private void fillcombo()
        {
            string[] lines = getstrings();
            combo.Items.Clear();
            foreach (string l in lines)
                if (l != null)
                    combo.Items.Add(l);
        }

        /// <summary>
        /// sets an appropriate combo.SelectedIndex
        /// </summary>
        private void setcombovalue()
        {
            if (!hascombo)
                return;

            if (valuetype == 'Y')
            {
                int i = C.trapDam_types.ToList().IndexOf((int)Value);
                combo.SelectedIndex = i > -1 ? i : 0;
                return;
            }
            combo.SelectedIndex = (Value >= 0 && Value < combo.Items.Count) ? Convert.ToInt32(Value) : 0;
        }

        private void fillmenu()
        {
            if (!hasmenu)
                return;

            string[] lines = getstrings();
            for (int i = 0; i <= 3; i++)
                if (i < lines.Length)
                {
                    menu.Items[i].Text = lines[i];
                    menu.Items[i].Visible = true;
                }
                else menu.Items[i].Visible = false;

            switch (valuetype)
            {
                case 'I': menu.Items[6].Visible = true;
                    menu.Items[4].Visible = false;
                    menu.Items[5].Visible = false;
                    menu.AutoClose = true;
                    break;
                case 'C':
                case 'K': menu.Items[6].Visible = false;
                    menu.Items[4].Visible = true;
                    menu.Items[5].Visible = true;
                    menu.AutoClose = false;
                    break;
            }
        }

        // 17/10/21 Saregon - affects
        void setnumsvalues()
        {
            if (!hasnums)
                return;

            switch (valuetype)
            {
                case 'Q':
                        num2.Value = val2;
                        num3.Value = val3;
                        num4.Value = val4;
                        break;
            }
        }


         void setbtntext()
        {
            if (!hasbtn)
                return;

            area_element el;

            switch (valuetype)
            {
                // mob dialog function
                case 'F':
                    MDFuncData f = Database.GetMDFunction(Convert.ToInt32(Value));
                    if (f != null)
                        btn.Text = f.shortdesc;
                    else btn.Text = Value.ToString();
                    break;
                case 'S':
                    el = Database.GetSpell(Convert.ToInt32(Value));
                    if (el != null)
                        btn.Text = (el as Spell).shortdesc;
                    else btn.Text = Value.ToString();
                    //else btn.Text = "non trovato";
                    break;
                case 'k':
                    el = ParentArea.Get<Obj>(Convert.ToInt32(Value));
                    if (el != null)
                        btn.Text = "#" + el.vnum + " - " + el.shortdesc;
                    else btn.Text = "#" + Value + " - non trovato";
                    break;
                case 'A':
                case 'E':
                case 'C':
                case 'K':
                case 'T': string[] lines = getstrings(true);

                    if (lines == null)
                        return;

                    string tmp = "";
                    bool found = false;
                    SetBitVector();
                    for (int i = 0; i < lines.Length; i++)
                        if (bv[1 << i])
                        {
                            tmp = tmp + (found ? " " : "") + lines[i];
                            found = true;
                        }
                    btn.Text = "[" + (found ? tmp : "Nessuno") + "]";
                    break;
                case 'I': if (Value == -1 || Value == 0)
                    {
                        btn.Text = L.Get(L.container_key, Convert.ToInt32(Value) + 1);
                        return;
                    }

                    if (ParentArea == null)
                        break;

                    el = ParentArea.Get<Obj>(Convert.ToInt32(Value));
                    if (el != null)
                        btn.Text = "#" + el.vnum + " - " + el.shortdesc;
                    else btn.Text = "#" + Value + " - non trovato";
                    break;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// sets components data and shows them
        /// </summary>
        private void SetVisual()
        {
            Hide();
            if (hascombo)
                combo.Hide();
            if (hasbtn)
                btn.Hide();
            if (hasnums)
            {
                num2.Hide();
                num3.Hide();
                num4.Hide();
            }

            Size = new Size(baseSizeX, Size.Height);

            switch (GetWidget())
            {
                case 1: createcombo();
                    fillcombo();
                    combo.Show();
                    break;
                case 2: createbtn();
                    btn.Show();
                    break;
                case 0: Show();
                    break;
                // 17/10/21 Saregon - affects
                case 3: createNums();
                    Size = new Size(baseSizeX / 3, Size.Height);
                    num2.Size = Size;
                    num3.Size = Size;
                    num4.Size = Size;
                    Show();
                    num2.Show();
                    num3.Show();
                    num4.Show();
                    break;
                default: break;
            }
            RefreshComponents();
        }

        /// <summary>
        /// updates used component value based on the main Value
        /// </summary>
        public void RefreshComponents()
        {
            switch (GetWidget())
            {
                case 1: setcombovalue();
                    break;
                case 2: setbtntext();
                    break;
                case 3: setnumsvalues();
                    break;
                default: break;
            }
        }
        #endregion

        #region OnClick Events
        private void btn_selectflagsclick(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                form.SetItems(getstrings(), bv.Data);
                if (form.ShowDialog() == DialogResult.OK)
                    SetValue((int)form.result);
            }
        }

        private void menu_finditemclick(object sender, EventArgs e)
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                form.SetElements<Obj>(ParentArea, C.i_obj, ParentArea.objects);
                if (form.ShowDialog() == DialogResult.OK)
                    SetValue(form.result.GetType() == typeof(Obj) ? (form.result as Obj).vnum : (int)form.result);
            }
        }

        private void btn_onClick(object sender, EventArgs e)
        {
            SetBitVector();

            switch (valuetype)
            {
                case 'I': for (int i = 0; i <= 3; i++)
                        (menu.Items[i] as ToolStripMenuItem).Checked = false;
                    break;
                case 'C':
                case 'K': for (int i = 0; i <= 3; i++)
                        (menu.Items[i] as ToolStripMenuItem).Checked = bv[1 << i];
                    break;  
            }

            switch (valuetype)
            {
                case 'F':
                    using (dlg_select_element form = new dlg_select_element())
                    {
                        form.SetElements<MDFuncData>(ParentArea, C.i_mobdialog_function, Database.md_functions);
                        if (form.ShowDialog() == DialogResult.OK)
                            SetValue(form.result.GetType() == typeof(MDFuncData) ? (form.result as MDFuncData).vnum : (int)form.result);
                    }
                    break;
                case 'S':
                            using (dlg_select_element form = new dlg_select_element())
                            {
                                form.SetElements<Spell>(ParentArea, C.i_spell, Database.spells);
                                if (form.ShowDialog() == DialogResult.OK)
                                    SetValue(form.result.GetType() == typeof(Spell) ? (form.result as Spell).vnum : (int)form.result);
                            }
                            break;
                case 'A':
                case 'E':
                case 'T':
                            btn_selectflagsclick(sender, e);
                            break;
                case 'I':
                case 'C':
                case 'K': 
                            menu.Show(MousePosition);
                            break;
            }
        }

        private void menu_itemclick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (sender as ToolStripMenuItem);
            int index = int.Parse(item.Name[item.Name.Length - 1].ToString());
            switch (valuetype)
            {
                case 'C':
                case 'K': item.Checked = !item.Checked;
                    bv[1 << index] = item.Checked;
                    SetValue(bv.Data);
                    break;
                case 'I': SetValue(index - 1);
                    break;
            }
        }
        #endregion
        #region Minor Events
        private void menu_closeclick(object sender, EventArgs e)
        {
            menu.Close();
        }

        private void menu_Opening(object sender, EventArgs e)
        {
            if (ValAffEditbox.lastopenedmenu != null)
                ValAffEditbox.lastopenedmenu.Close();
            ValAffEditbox.lastopenedmenu = sender as ContextMenuStrip;
        }

        private void num_onValueChanged(object sender, EventArgs e)
        {
            if (sender == num2)
                val2 = Convert.ToInt32(num2.Value);

            if (sender == num3)
                val3 = Convert.ToInt32(num3.Value);

            if (sender == num4)
                val4 = Convert.ToInt32(num4.Value);

            
        }

        private void combo_onValueChanged(object sender, EventArgs e)
        {
            switch (valuetype)
            {
                case 'Y':
                Value = C.trapDam_types[Math.Min(C.trapDam_types.Length, Math.Max(0, combo.SelectedIndex))];
                break;
                default:
                    Value = combo.SelectedIndex;
                    break;
            }
        }

        private void movecomponents()
        {
            if (hascombo)
                combo.Location = Location;
            if (hasbtn)
                btn.Location = Location;
            if (hasnums)
            {
                num2.Location = new Point(Location.X + baseSizeX / 3 + 10, Location.Y);
                num3.Location = new Point(Location.X + baseSizeX / 3 * 2 + 20, Location.Y);
                num4.Location = new Point(Location.X + baseSizeX / 3 * 3 + 30, Location.Y);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (hascombo)
                combo.Parent = Parent;
            if (hasbtn)
                btn.Parent = Parent;
            if (hasnums)
            {
                num2.Parent = Parent;
                num3.Parent = Parent;
                num4.Parent = Parent;
            }
        }

        private void onResize(object sender, EventArgs e)
        {
            movecomponents();
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            movecomponents();
        }
        #endregion
    }
    #endregion

    #region Area Element Tooltip
    public partial class AreaElementTooltip : NoFlickerPanel
    {
        public area_element data { get; set; }
        private StringFormat sf;
        private string output;

        #region Constructor
        public AreaElementTooltip()
        {
            output = "";
            sf = new StringFormat(StringFormat.GenericTypographic) { FormatFlags = StringFormatFlags.MeasureTrailingSpaces };
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            sf.Dispose();
            base.Dispose(disposing);
        }

        #region Methods
        public void GenerateString()
        {
            if (data == null)
                return;

            Width = 1;
            Height = 1;
            output = "";

            if (data.GetType() == typeof(Room))

            #region Room
            {
                Room room = data as Room;
                output += "$c0014#" + room.vnum + "\n";
                output += room.shortdesc + " $c0007[" + L.Get(L.room_sectors_col, room.sect) + "]\n$c0007";
                output += room.longdesc + "\n";
                output += "Uscite: \n";
                for (int i = 0; i <= C.dir_down; i++)
                    if (room.GetExit(i) != null)
                        output += L.Get(L.directions_colorcodes, i) + L.Get(L.directions, i) + " ";
            }
            #endregion

            else if (data.GetType() == typeof(Obj))

            #region Obj
            {
                Obj obj = data as Obj;
                output += "$c0014#" + obj.vnum + "\n$c0007";
                output += "Oggetto '" + obj.keys + "', Tipo: " + L.Get(L.object_types, obj.properties[C.op_type]) + "\n";
                string s = "";
                for (int i = 0; i <= C.of_end; i++)
                    s = s + (obj.flags[1 << i] ? L.Get(L.object_flags, i) + " " : "");
                output += "L'oggetto e': " + s + "\n";
                s = "";
                for (int i = 0; i <= C.ow_end; i++)
                    s = s + (obj.wearpos[1 << i] ? L.Get(L.object_wear, i) + " " : "");
                output += "Puo' essere indossato su: " + s + "\n";
                output += "Peso: " + obj.properties[C.op_weight] + ", Valore: " + obj.properties[C.op_value] + ", Costo di rent: " + obj.properties[C.op_rent] + "\n";

                if (obj.properties[C.op_type] == C.ot_armor)
                    output += "Il modificatore della AC e' " + obj.values[1] + "\n";

                if (obj.properties[C.op_type] == C.ot_weapon || obj.properties[C.op_type] == C.ot_missile)
                    output += "Il dado dei danni e' '" + obj.values[1] + "d" + obj.values[2] + "'\n";

                output += "Ecco i suoi effetti:\n";
                foreach (ObjAffect aff in obj.affects)
                    if (aff.index > 0)
                    {
                        output += "Effetto : " + L.Get(L.object_affects, aff.index) + " by " + aff.ValueToString() + "\n";
                    }
            }
            #endregion

            else if (data.GetType() == typeof(Mob))

            #region Mob
            {
                Mob mob = data as Mob;
                output += "$c0014#" + mob.vnum + "$c0007\n";
                output += "MOB - Sex: " + L.Get(L.mob_gender, mob.values[C.mv_sex]) + ", Name : " + mob.keys + "\n";
                output += "Short description: " + mob.shortdesc + "\n";
                output += "Long description: " + mob.longdesc + "\n";
                output += "Monster Class: " + L.Get(L.mob_types, mob.values[C.mv_type]) + "\n";
                output += "Level " + mob.values[C.mv_level] + ", Alignment[" + mob.values[C.mv_align] + "]\n";
                output += "AC[" + mob.values[C.mv_ac] + "], Coins: [" + mob.values[C.mv_gold] + "], XP Bonus: [" + mob.values[C.mv_xpbonus] + "]\n";
                output += "Thac0: [" + mob.values[C.mv_thac0] + "], SpellPower: [" + mob.values[C.mv_spellpower] + "]\n";
                output += "DamRedBlunt: [" + mob.values[C.mv_red_blunt] + "] DamRedSlash: [" + mob.values[C.mv_red_slash] + "] DamRedPierce: [" + mob.values[C.mv_red_pierce] + "]\n";
                output += "LoadPosition: " + L.Get(L.mob_positions, mob.values[C.mv_loadpos]) + ", Default position: " + L.Get(L.mob_positions, mob.values[C.mv_defaultpos]) + "\n";
                string s = "";
                for (int i = 0; i <= C.mf_end; i++)
                    s = s + (mob.flags[1 << i] ? L.Get(L.mob_acts, i) + " " : "");
                output += "NPC flags: " + s + "\n";
                output += "NPC Bare Hand Damage " + mob.damage + "\n";
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (mob.immunities[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") output += "Immune to: " + s + "\n";
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (mob.susceptibles[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") output += "Susceptible to: " + s + "\n";
                s = "";
                for (int i = 0; i <= C.dt_end; i++)
                    s = s + (mob.resistances[1 << i] ? L.Get(L.damage_types, i) + " " : "");
                if (s != "") output += "Resistant to: " + s + "\n";
                output += "Race: " + L.Get(L.mob_races, mob.values[C.mv_race]) + "\n";
                s = "Affected by: ";
                for (int i = 0; i <= C.ma_end; i++)
                    s = s + (mob.affects[1 << i] ? L.Get(L.mob_affects, i) + " " : "");
                output += s + "\n";
            }
            #endregion

            else data = null;
        }
        #endregion

        #region Events
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (data == null)
            {
                Width = 1;
                Height = 1;
                return;
            }

            using (Font font = new Font("Courier New", 9))
            {
                int alpha = 255;
                int border = 1;

                int strheight = (int)e.Graphics.MeasureString(" ", font, Parent.Width, sf).Height;

                string s = "";
                int spacer = 0;
                Color def = Color.LightGray;

                string[] desc = output.Replace("\r", "").Split('\n');

                foreach (string l in desc)
                {
                    s = "";
                    foreach (var x in utils.ProcessTextColorCodes(l, def))
                        using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, x.c)))
                        {
                            def = x.c;
                            e.Graphics.DrawString(x.s, font, sb, new Point(10 + (int)e.Graphics.MeasureString(s, font, Parent.Width, sf).Width, 10 + spacer), sf);
                            s += x.s;
                            Width = Math.Max((int)e.Graphics.MeasureString(s, font, Parent.Width, sf).Width + 20, Width);
                        }
                    spacer += strheight;
                }

                Height = spacer + 20;

                e.Graphics.DrawLine(Pens.Gold, new Point(border, border), new Point(Bounds.Width - border, border));
                e.Graphics.DrawLine(Pens.Gold, new Point(Bounds.Width - border, border), new Point(Bounds.Width - border, Bounds.Height - border));
                e.Graphics.DrawLine(Pens.Gold, new Point(Bounds.Width - border, Bounds.Height - border), new Point(border, Bounds.Height - border));
                e.Graphics.DrawLine(Pens.Gold, new Point(border, Bounds.Height - border), new Point(border, border));
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Hide();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Hide();
        }
        #endregion
    }
    #endregion
}