using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace HandofGod
{
    public partial class frm_AreaMap : Form
    {

        // temporaneo
        int counter = 0;

        #region Vars
        Camera camera; // used to navigate the area
        public Area Data;
        Room default_room;
        Room selected_room;
        public EventHandler areachanged;
        int selected_btn = 0;
        int btnswidth = 100;
        int btnsheight = 20;
        int btnscount = 5;
        #endregion

        #region Constructor & FormLoad
        private void resize(object sender, EventArgs e)
        {
            backgroundpanel.Refresh();
        }

        public frm_AreaMap()
        {
            InitializeComponent();
            camera = new Camera();
            Data = new Area();
            default_room = new Room();
            MouseWheel += new MouseEventHandler(mousewheel);
            Resize += new EventHandler(resize);
        }

        private void frm_editArea_Load(object sender, EventArgs e)
        {
            default_room.Clear();
            SelectRoom(null);
        }
        #endregion

        #region Draw
        private void backgroundpanel_Paint(object sender, PaintEventArgs e)
        {
            int left;
            int top;
            int alpha;
            int spacer;

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Brush b;
            // fare background image
            if (Data.rooms.Count > 0)
            using (Pen p = new Pen(Color.Transparent, 2))
                for (int k = -1; k <= 1; k++)
                    foreach (Room r in Data.rooms)
                        if (r.visual.visible && r.visual.floor > camera.floor - 2 && r.visual.floor < camera.floor + 2)
                        {
                            p.Color = Color.Transparent;

                            if (selected_room == r)
                                b = Brushes.Red;
                            else 
                                switch (r.sect)
                                {
                                    //case constants.rs_inside : room_color = Color.White; break;
                                    case C.rs_city: b = Brushes.Gray; break;
                                    case C.rs_field: b = Brushes.Olive; break;
                                    case C.rs_forest: b = Brushes.Green; break;
                                    case C.rs_hills: b = Brushes.Maroon; break;
                                    case C.rs_mountain: b = Brushes.Silver; break;
                                    case C.rs_waterswim: b = Brushes.Aqua; break;
                                    case C.rs_waternoswim: b = Brushes.Navy; break;
                                    case C.rs_air: b = Brushes.Azure; break;
                                    case C.rs_underwater: b = Brushes.Blue; break;
                                    case C.rs_desert: b = Brushes.Yellow; break;
                                    case C.rs_tree: b = Brushes.Lime; break;
                                    case C.rs_darkcity: b = Brushes.DarkGray; break;
                                    case C.rs_underdark: b = Brushes.White; break;
                                    case C.rs_dungeon: b = Brushes.White; break;
                                    case C.rs_cavern: b = Brushes.White; break;
                                    case C.rs_crypt: b = Brushes.White; break;
                                    case C.rs_castle: b = Brushes.White; break;
                                    case C.rs_manor: b = Brushes.White; break;
                                    case C.rs_prison: b = Brushes.White; break;
                                    case C.rs_temple_good: b = Brushes.White; break;
                                    case C.rs_temple_neutral: b = Brushes.White; break;
                                    case C.rs_temple_evil: b = Brushes.White; break;
                                    case C.rs_shop: b = Brushes.White; break;
                                    case C.rs_jungle: b = Brushes.White; break;
                                    case C.rs_shore: b = Brushes.White; break;
                                    case C.rs_beach: b = Brushes.White; break;
                                    case C.rs_plains: b = Brushes.White; break;
                                    case C.rs_swamp: b = Brushes.White; break;
                                    case C.rs_tundra: b = Brushes.White; break;
                                    case C.rs_taiga: b = Brushes.White; break;
                                    case C.rs_polar: b = Brushes.White; break;
                                    case C.rs_steppe: b = Brushes.White; break;
                                    case C.rs_savannah: b = Brushes.White; break;
                                    case C.rs_astral: b = Brushes.White; break;
                                    case C.rs_planar: b = Brushes.White; break;
                                    case C.rs_sigil: b = Brushes.White; break;
                                    case C.rs_vacuum: b = Brushes.White; break;
                                    case C.rs_unknown: b = Brushes.White; break;
                                    case C.rs_teleport: b = Brushes.Fuchsia; break;
                                    default: b = Brushes.White; break;
                                }

                            if (r.visual.floor != camera.floor)
                            {
                                b = Brushes.Transparent;
                                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                p.Color = r.visual.floor > camera.floor ? Color.LightCyan : Color.DarkGray;
                                p.Width = 1;
                            }

                            int dist = (int)(GetRoomOffset(r));
                            if (r.visual.floor != camera.floor && camera.floor - r.visual.floor == -k)
                            {
                                // draw room borders
                                
                                e.Graphics.DrawLine(p, camera.Transform(new Point(r.visual.rect.X - camera.pos.X, r.visual.rect.Y - camera.pos.Y)), camera.Transform(new Point(r.visual.rect.X - camera.pos.X + r.visual.rect.Width, r.visual.rect.Y - camera.pos.Y)));
                                e.Graphics.DrawLine(p, camera.Transform(new Point(r.visual.rect.X - camera.pos.X, r.visual.rect.Y - camera.pos.Y)), camera.Transform(new Point(r.visual.rect.X - camera.pos.X, r.visual.rect.Y - camera.pos.Y + r.visual.rect.Height)));
                                e.Graphics.DrawLine(p, camera.Transform(new Point(r.visual.rect.X - camera.pos.X + r.visual.rect.Width, r.visual.rect.Y - camera.pos.Y)), camera.Transform(new Point(r.visual.rect.X - camera.pos.X + r.visual.rect.Width, r.visual.rect.Y - camera.pos.Y + r.visual.rect.Height)));
                                e.Graphics.DrawLine(p, camera.Transform(new Point(r.visual.rect.X - camera.pos.X, r.visual.rect.Y - camera.pos.Y + r.visual.rect.Height)), camera.Transform(new Point(r.visual.rect.X - camera.pos.X + r.visual.rect.Width, r.visual.rect.Y - camera.pos.Y + r.visual.rect.Height)));
                            }

                            if (camera.floor == r.visual.floor)
                            {
                                p.Width = 2;
                                Rectangle rect = camera.Transform(new Rectangle(r.visual.rect.X - camera.pos.X - dist, r.visual.rect.Y - camera.pos.Y + dist, r.visual.rect.Width, r.visual.rect.Height));
                                // fill the room
                                e.Graphics.FillRectangle(b, rect);

                                // resize borders highlight
                                if (selected_room == r && isDragging == 3)
                                {
                                    if (dragborders[1 << 0])
                                    {
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2, rect.Top), new Point((rect.Left + rect.Right) / 2, rect.Top - 8));
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2 - 4, rect.Top - 3), new Point((rect.Left + rect.Right) / 2, rect.Top - 8));
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2 + 4, rect.Top - 3), new Point((rect.Left + rect.Right) / 2, rect.Top - 8));
                                    }
                                    if (dragborders[1 << 1])
                                    {
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Right, (rect.Top + rect.Bottom) / 2), new Point(rect.Right + 8, (rect.Top + rect.Bottom) / 2));
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Right + 3, (rect.Top + rect.Bottom) / 2 - 4), new Point(rect.Right + 8, (rect.Top + rect.Bottom) / 2));
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Right + 3, (rect.Top + rect.Bottom) / 2 + 4), new Point(rect.Right + 8, (rect.Top + rect.Bottom) / 2));
                                    }
                                    if (dragborders[1 << 2])
                                    {
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2, rect.Bottom), new Point((rect.Left + rect.Right) / 2, rect.Bottom + 8));
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2 - 4, rect.Bottom + 3), new Point((rect.Left + rect.Right) / 2, rect.Bottom + 8));
                                        e.Graphics.DrawLine(Pens.White, new Point((rect.Left + rect.Right) / 2 + 4, rect.Bottom + 3), new Point((rect.Left + rect.Right) / 2, rect.Bottom + 8));
                                    }
                                    if (dragborders[1 << 3])
                                    {
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Left, (rect.Top + rect.Bottom) / 2), new Point(rect.Left - 8, (rect.Top + rect.Bottom) / 2));
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Left - 3, (rect.Top + rect.Bottom) / 2 - 4), new Point(rect.Left - 8, (rect.Top + rect.Bottom) / 2));
                                        e.Graphics.DrawLine(Pens.White, new Point(rect.Left - 3, (rect.Top + rect.Bottom) / 2 + 4), new Point(rect.Left - 8, (rect.Top + rect.Bottom) / 2));
                                    }
                                }

                                // vnum
                                b = Brushes.Black;
                                e.Graphics.DrawString(r.vnum.ToString(), this.Font, b, rect, sf);

                                // draw exits
                                for (int i = 0; i <= C.dir_down; i++)
                                {
                                    Exit ex = r.GetExit(i);
                                    if (ex != null)
                                    {
                                        Room d = Data.Get<Room>(ex.room);
                                        if (d != null && Math.Abs(d.visual.floor - camera.floor) <= 1)
                                            // test distanza
                                            //if (Math.Abs(d.visual.rect.X - r.visual.rect.X) <= r.visual.rect.Width * 3 &&
                                            //    Math.Abs(d.visual.rect.Y - r.visual.rect.Y) <= r.visual.rect.Height * 4)
                                            if (true)
                                            {
                                                int dist2 = (int)(GetRoomOffset(d));
                                                // this room center
                                                Point rc = new Point(r.visual.rect.X + r.visual.rect.Width / 2 - camera.pos.X, r.visual.rect.Y + r.visual.rect.Height / 2 - camera.pos.Y);
                                                // dest room center
                                                Point dc = new Point(d.visual.rect.X + d.visual.rect.Width / 2 - camera.pos.X, d.visual.rect.Y + d.visual.rect.Height / 2 - camera.pos.Y);
                                                // half room X & Y 
                                                int hx = (int)(r.visual.rect.Width / 2);
                                                int hy = (int)(r.visual.rect.Height / 2);
                                                int hxt = (int)(d.visual.rect.Width / 2);
                                                int hyt = (int)(d.visual.rect.Height / 2);

                                                //p.Color = ex.flags[1 << C.df_secret] ? Color.Red : Math.Abs(d.visual.floor - camera.floor) == 1 ? Color.Cyan : Color.Lime;

                                                p.Color = Color.Lime;

                                                if (ex.flags[1 << C.df_door])
                                                {
                                                    if (ex.door.objkey > 0)
                                                    {
                                                        p.Color = Color.MidnightBlue;
                                                    }
                                                    else
                                                        p.Color = Color.DeepSkyBlue;
                                                }

                                                if (ex.flags[1 << C.df_secret])
                                                {
                                             
                                                    if (ex.door.objkey > 0)
                                                        p.Color = Color.Purple;
                                                    else
                                                        p.Color = Color.Red;
                                                }

                                                if (Math.Abs(d.visual.floor - camera.floor) != 0)
                                                {
                                                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                                    p.Width = 1;
                                                }
                                                else
                                                {
                                                    p.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                                                    p.Width = 2;
                                                }

                                                switch (i)
                                                {
                                                    case C.dir_north: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist, rc.Y + dist - hy)),
                                                                                                 camera.Transform(new Point(dc.X - dist2, dc.Y + dist2 + hyt)));
                                                        break;
                                                    case C.dir_south: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist, rc.Y + dist + hy)),
                                                                                                 camera.Transform(new Point(dc.X - dist2, dc.Y + dist2 - hyt)));
                                                        break;
                                                    case C.dir_east: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist + hx, rc.Y + dist)),
                                                                                                camera.Transform(new Point(dc.X - dist2 - hxt, dc.Y + dist2)));
                                                        break;
                                                    case C.dir_west: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist - hx, rc.Y + dist)),
                                                                                                camera.Transform(new Point(dc.X - dist2 + hxt, dc.Y + dist2)));
                                                        break;
                                                    case C.dir_up: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist + hx, rc.Y + dist - hy)),
                                                                                              camera.Transform(new Point(dc.X - hxt - dist, dc.Y + hyt + dist)));
                                                        break;
                                                    case C.dir_down: e.Graphics.DrawLine(p, camera.Transform(new Point(rc.X - dist - hx, rc.Y + dist + hy)),
                                                                                                camera.Transform(new Point(dc.X + hxt - dist, dc.Y - hyt + dist)));
                                                        break;
                                                }
                                            }
                                    }
                                }
                                         
                            }
                        }

            b = Brushes.Lime;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            string s = "Zoom: " + (Math.Floor(camera.zoom * 100) / 100).ToString() +
                       "\nPiano: " + camera.floor.ToString();
            e.Graphics.DrawString(s, this.Font, b, new Rectangle(0, Height - 100, 100, 100), sf);

            s = "Comandi: ZOOM - Rotellina del Mouse     RIDIMENSIONA STANZE - Shift + Trascinamento dal bordo";
            e.Graphics.DrawString(s, this.Font, b, new Rectangle(0, Height - 100, Width, 100), sf);

            #region Selected Room Preview
            if (selected_room != null)
            {
                Font font = new Font("Courier New", 9);
                left = 10;
                top = 10;
                alpha = 150;
                spacer = 0;

                sf = new StringFormat(StringFormat.GenericTypographic) { FormatFlags = StringFormatFlags.MeasureTrailingSpaces };
                int strheight = (int)e.Graphics.MeasureString(" ", font, Width, sf).Height;
 
                s = "";
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, Color.Cyan)))
                {
                    e.Graphics.DrawString("#" + selected_room.vnum + " ", font, sb, new Point(left + (int)e.Graphics.MeasureString(s, font, Width, sf).Width, top), sf);
                    s += "#" + selected_room.vnum + " ";
                }

                foreach (var x in utils.ProcessTextColorCodes(selected_room.shortdesc + " $c0007[" + L.Get(L.room_sectors_col, selected_room.sect) + "]", Color.Cyan))
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, x.c)))
                {
                    e.Graphics.DrawString(x.s, font, sb, new Point(left + (int)e.Graphics.MeasureString(s, font, Width, sf).Width, top), sf);
                    s += x.s;
                }
                spacer += strheight;

                Color def = Color.LightGray;
                string[] longdesc = selected_room.longdesc.Replace("\r", "").Split('\n');

                foreach (string l in longdesc)
                {
                    s = "";
                    foreach (var x in utils.ProcessTextColorCodes(l, def))
                        using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, x.c)))
                    {
                        def = x.c;                        
                        e.Graphics.DrawString(x.s, font, sb, new Point(left + (int)e.Graphics.MeasureString(s, font, Width, sf).Width, top + spacer), sf);
                        s += x.s;
                    }
                    spacer += strheight;
                }

                s = "";
                using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, Color.LightGray)))
                {
                    e.Graphics.DrawString("Uscite: ", font, sb, new Point(left + (int)e.Graphics.MeasureString(s, font).Width, top + spacer), sf);
                    s += "Uscite: ";
                }
                for (int i = 0; i <= C.dir_down; i++)
                if (selected_room.GetExit(i) != null)
                    using (SolidBrush sb = new SolidBrush(Color.FromArgb(alpha, C.dir_colors[i])))
                    {
                        e.Graphics.DrawString(L.Get(L.directions, i) + " ", font, sb, new Point(left + (int)e.Graphics.MeasureString(s, font).Width, top + spacer), sf);
                        s += L.Get(L.directions, i) + " ";
                    }
            }
            #endregion

            #region Buttons
            top = 10;
            left = Width - btnswidth - 20;
            spacer = 0;
            alpha = 200;
            for (int i = 0; i <= btnscount; i++)
            {
                Rectangle r = new Rectangle(left, top + spacer, btnswidth, btnsheight);
                if (i == selected_btn)
                    e.Graphics.FillRectangle(Brushes.Azure, r);
                else
                    e.Graphics.FillRectangle(Brushes.White, r);

                string l = "";

                switch (i)
                {
                    case 0: l = "Piano +"; break;
                    case 1: l = "Piano -"; break;
                    case 2: l = "Rigenera Area"; break;
                    case 3: l = "Modifica Template"; break;
                    case 4: l = "Resetta Template"; break;
                    case 5: l = "Chiudi"; break;
                }

                e.Graphics.DrawString(l, this.Font, Brushes.Black, r);
                spacer += btnsheight;
            }
            e.Graphics.DrawString(default_room != null ? default_room.shortdesc : "nuova stanza", this.Font, Brushes.Fuchsia, new Rectangle(left, top + spacer, btnswidth, btnsheight));
            #endregion

            counter++;
            //e.Graphics.DrawString("*Refresh counter: " + counter, this.Font, b, new Point(0, Height - 200), sf);
        }
        #endregion

        #region Menu Buttons
        private void edittemplate()
        {
            using (dlg_select_element form = new dlg_select_element())
            {
                form.SetElements<Room>(Data, C.i_room, Options.data.Templates.rooms);
  
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    default_room = form.result as Room;
            }
        }

        private bool MouseOveringMenuButtons(MouseEventArgs e)
        {
            return (e.X > Width - 20 - btnswidth && e.X < Width - 20 &&
                    e.Y > 10 && e.Y < 10 + btnsheight * (btnscount +1 ));
        }

        private void MenuButtonsClick()
        {
            switch (selected_btn)
            {
                case 0: camera.floor++; break;
                case 1: camera.floor--; break;
                case 2: Data.SetVisuals(); break;
                case 3: edittemplate(); break;
                case 4: default_room = null; break;
                case 5: Close(); break;
            }
            backgroundpanel.Refresh();
        }
        #endregion

        #region Methods
        private int GetRoomOffset(Room r)
        {
            return (camera.floor - r.visual.floor) * (VisualProperties.def_height + VisualProperties.def_width / 2);
        }

        private void SelectRoom(Room r)
        {
            if (r == null)
                return;

            selected_room = r;

            if (selected_room != null)
            {
                camera.floor = r.visual.floor;
                camera.Center(backgroundpanel, r);
            }
            backgroundpanel.Refresh();
        }
        #endregion

        #region Mouse & Keyboard Processes
        bool isKeyDown = false;
        private void frm_main_KeyDown(object sender, KeyEventArgs e)
        {
            int dir = -1;

            if (isKeyDown || selected_room == null)
                return;

            if (e.KeyCode != Keys.ShiftKey)
                isKeyDown = true;

            if (e.KeyCode == Keys.Delete)
            {
                Data.RemoveElement(selected_room);
                areachanged(null, null);
                Refresh();
            }

            switch (e.KeyCode)
            {
                case Keys.PageUp   : dir = C.dir_up; break;
                case Keys.PageDown : dir = C.dir_down; break;
                case Keys.Up: dir = C.dir_north; break;
                case Keys.Down: dir = C.dir_south; break;
                case Keys.Left: dir = C.dir_west; break;
                case Keys.Right: dir = C.dir_east; break;
                default: break;
            }

            if (e.Shift && dir > -1 && selected_room.GetExit(dir) == null)
            {
                Room r = selected_room.visual.GenerateExit(Data, dir, true, true, default_room);
                if (r != null)
                    SelectRoom(r);

                areachanged(null, null);
            }
            else
                if (selected_room.GetExit(dir) != null)
                    SelectRoom(Data.Get<Room>(selected_room.GetExit(dir).room));
        }

        private void frm_main_KeyUp(object sender, KeyEventArgs e)
        {
            isKeyDown = false;
        }

        int lastX = 0;
        int lastY = 0;
        int isDragging = 0;
        BitVector32 dragborders = new BitVector32();
        private void backgroundpanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseOveringMenuButtons(e))
            {
                MenuButtonsClick();
                return;
            }

            int dist = 0;
            Room targetroom = null;

            foreach (Room r in Data.rooms)
                if (r.visual.floor == camera.floor)
                {
                    dist = GetRoomOffset(r);

                    Rectangle rect = camera.Transform(new Rectangle(r.visual.rect.X - camera.pos.X - dist, r.visual.rect.Y - camera.pos.Y + dist, r.visual.rect.Width, r.visual.rect.Height));

                    if (r.visual.floor != camera.floor)
                    {
                        rect.X += dist;
                        rect.Y -= dist;
                    }

                    if (e.X >= rect.Left && e.X <= rect.Right &&
                       e.Y >= rect.Top && e.Y <= rect.Bottom &&
                        r.visual.floor == camera.floor)
                    {
                        targetroom = r;

                        for (int i = 0; i <= 3; i++)
                            dragborders[1 << i] = false;

                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            if (e.Y <= rect.Top + rect.Height / 4)
                            {
                                dragborders[1 << 0] = true;
                                isDragging = 3;
                            }
                            if (e.X >= rect.Right - rect.Width / 4)
                            {
                                dragborders[1 << 1] = true;
                                isDragging = 3;
                            }
                            if (e.Y >= rect.Bottom - rect.Height / 4)
                            {
                                dragborders[1 << 2] = true;
                                isDragging = 3;
                            }
                            if (e.X <= rect.Left + rect.Width / 4)
                            {
                                dragborders[1 << 3] = true;
                                isDragging = 3;
                            }
                        }
                        break;
                    }
                }

            if (targetroom != null)
            {
                isDragging = isDragging == 0 ? 1 : isDragging;
                if (targetroom != selected_room)
                    selected_room = targetroom;
                else
                    if (e.Button == MouseButtons.Left)
                    {
                        //camera.Center(backgroundpanel, selected_room);
                        if (e.Clicks > 1)
                        {
                            selected_room.Edit(Data, null);
                            areachanged(null, null);
                            isDragging = 0;
                        }
                    }
                backgroundpanel.Refresh();
            }
            else
            {
                lastX = e.X;
                lastY = e.Y;
                isDragging = isDragging == 0 ? 2 : isDragging;
            }
        }

        private void backgroundpanel_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = 0;

            if (e.Button == MouseButtons.Right && selected_room != null)
                contextMenuStrip1.Show(MousePosition);

            backgroundpanel.Refresh();
        }

        private void backgroundpanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastX == e.X && lastY == e.Y)
                return;

            if (MouseOveringMenuButtons(e))
            {
                selected_btn = (e.Y - 10) / btnsheight;
                backgroundpanel.Refresh();
            }

            switch (isDragging)
            {
                case 1:
                    selected_room.visual.rect.X = selected_room.visual.rect.X + (int)((e.X - lastX) / camera.zoom);
                    selected_room.visual.rect.Y = selected_room.visual.rect.Y + (int)((e.Y - lastY) / camera.zoom);
                    backgroundpanel.Refresh();
                    break;
                case 2:
                    camera.pos.X = camera.pos.X - (e.X - lastX);
                    camera.pos.Y = camera.pos.Y - (e.Y - lastY);
                    backgroundpanel.Refresh();
                    break;
                case 3:
                    if (dragborders[1 << 0])
                        selected_room.visual.rect.Y -= selected_room.visual.SetHeight(selected_room.visual.rect.Height - (int)((e.Y - lastY) / camera.zoom));
                    if (dragborders[1 << 1])
                        selected_room.visual.SetWidth(selected_room.visual.rect.Width + (int)((e.X - lastX) / camera.zoom));
                    if (dragborders[1 << 2])
                        selected_room.visual.SetHeight(selected_room.visual.rect.Height + (int)((e.Y - lastY) / camera.zoom));
                    if (dragborders[1 << 3])
                        selected_room.visual.rect.X -= selected_room.visual.SetWidth(selected_room.visual.rect.Width - (int)((e.X - lastX) / camera.zoom));

                    backgroundpanel.Refresh();
                    break;
            }

            lastX = e.X;
            lastY = e.Y;
        }

        public void mousewheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // wheel down
                camera.zoom += 0.01f;
            else camera.zoom -= 0.01f;

            camera.Update();
            backgroundpanel.Refresh();
        }
        #endregion

        #region Widgets' Events
        private void changeroomfloor(object sender, EventArgs e)
        {
            if (selected_room == null)
                return;

            if (sender == roomfloorplus)
                selected_room.visual.floor++;
            else selected_room.visual.floor--;
            camera.Center(backgroundpanel, selected_room);

            Refresh();
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
