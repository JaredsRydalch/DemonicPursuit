using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Book_of_the_Dead
{
    public partial class Form2 : Form
    {

        private void Player_Reset(int players)
        {
            Player1Label.Location = new Point(320, 256);
            Player2Label.Location = new Point(640, 512);
            if (players > 2)
                Player3Label.Location = new Point(640, 256);
            if (players > 3)
                Player4Label.Location = new Point(320, 512);
        }

        public Player[] playerarray;
        public Player it;
        public Form initiatorform;
        public Monster monster;
        public List<Hook> hlist = new List<Hook>();
        public List<Powerup> plist = new List<Powerup>();
        public int possessorDemonTimer = 200;
        public int possessorDemonStartTimer = 201;
        public Player possessed = new Player();
        public Random rnd = new Random();
        public Form2(int numofplayers, Form initiator, Monster m)
        {
            InitializeComponent();
            //this.TopMost = true;
            //this.WindowState = FormWindowState.Maximized;
            playerarray = new Player[numofplayers];
            monster = m;
            initiatorform = initiator;
            if (numofplayers > 4)
                this.Close();
            for (int i = 0; i < numofplayers; i++) 
            {
                playerarray[i] = new Player();
                switch (i) 
                {
                    case 0:
                        playerarray[i].label = Player1Label;
                        Player1Label.Show();
                        playerarray[i].playing = true;
                        playerarray[i].rectangle = new Rectangle(Player1Label.Location, Player1Label.Size);
                        break;
                    case 1:
                        playerarray[i].label = Player2Label;
                        Player2Label.Show();
                        playerarray[i].playing = true;
                        playerarray[i].rectangle = new Rectangle(Player2Label.Location, Player2Label.Size);
                        break;
                    case 2:
                        playerarray[i].label = Player3Label;
                        Player3Label.Show();
                        playerarray[i].playing = true;
                        playerarray[i].rectangle = new Rectangle(Player3Label.Location, Player3Label.Size);
                        break;
                    case 3:
                        playerarray[i].label = Player4Label;
                        Player4Label.Show();
                        playerarray[i].playing = true;
                        playerarray[i].rectangle = new Rectangle(Player4Label.Location, Player4Label.Size);
                        break;
                }
                playerarray[i].nonitcolor = playerarray[i].label.BackColor;
            }
            if (monster.type != Monster.Monster_Type.Posessor)
            {
                MonsterLabel.Size = new System.Drawing.Size(monster.size * 2, monster.size * 2);
                monster.monsterLabel = MonsterLabel;
                monster.monsterRectangle = new Rectangle(MonsterLabel.Location, MonsterLabel.Size);
            }
            else 
            {
                MonsterLabel.Hide();
                possessed.rectangle = new Rectangle(-200, -200, 1, 1);
                possessed.label = new Label();
            }
            Random rnd = new Random();
            if (monster.type != Monster.Monster_Type.Posessor & monster.type != Monster.Monster_Type.Hook)
            {
                it = playerarray[rnd.Next(0, numofplayers)];
                // it.label.BackColor = Color.Red;
                it.it = true;
                it.label.BorderStyle = BorderStyle.Fixed3D;
            }
            ControlBox.Focus();
            Player_Reset(4);
            MonsterLabel.Location = new Point((this.Size.Width - 27) / 2, (this.Size.Height - 50) / 2);
            monster.start_Position = MonsterLabel.Location;
            monster.monsterRectangle.Location = MonsterLabel.Location;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            initiatorform.Show();
        }

        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            foreach (Powerup p in plist)
            {
                foreach (Player player in playerarray)
                    if (!p.acquired && player.rectangle.IntersectsWith(p.rectangle))
                    {
                        p.label.Location = new Point(-200, -200);
                        p.rectangle.Location = new Point(-200, -200);
                        p.picked_up(player);
                        p.acquired = true;
                        this.Controls.Remove(p.label);
                    }
            }
            for (int i = plist.Count - 1; i >= 0; i--) {
                if (plist[i].acquired == true)
                    plist.Remove(plist[i]);
            }
            #region NormalTick
            if (monster.type != Monster.Monster_Type.Posessor)
            {
                foreach (Player p in playerarray)
                {
                    if (p != null && p.playing)
                    {
                        p.Move(this.Size);
                        if (monster.type != Monster.Monster_Type.Hook)
                        {
                            if (!p.it && p.immunetimer <= 0)
                            {
                                if (Rectangle.Intersect(p.rectangle, it.rectangle) != Rectangle.Empty)
                                {
                                    p.it = true;
                                    it.it = false;
                                    it.label.BorderStyle = BorderStyle.None;
                                    it.immunetimer = 50;
                                    it.label.BackColor = it.nonitcolor;
                                    it = p;
                                    // p.label.BackColor = Color.Red;
                                    p.label.BorderStyle = BorderStyle.Fixed3D;
                                }
                            }
                            else if (Rectangle.Intersect(p.rectangle, monster.monsterRectangle) != Rectangle.Empty)
                                Reset(p);
                        }

                    }
                }
                if (monster.delay >= 1)
                {
                    monster.delay -= 1;
                    if (monster.type == Monster.Monster_Type.Hook)
                        monster.delay = 0;
                }
                else if (monster.type != Monster.Monster_Type.Hook)
                {
                    if (it.rectangle.Location.X + 8 > monster.monsterRectangle.Location.X + 16)
                    {
                        if (monster.speed_x > 0)
                            monster.speed_x += monster.total_acceleration;
                        else
                            monster.speed_x += 2 * monster.total_acceleration;
                    }
                    else
                    {
                        if (monster.speed_x < 0)
                            monster.speed_x -= monster.total_acceleration;
                        else
                            monster.speed_x -= 2 * monster.total_acceleration;
                    }
                    if (it.rectangle.Location.Y + 8 > monster.monsterRectangle.Location.Y + 16)
                    {
                        if (monster.speed_y > 0)
                            monster.speed_y += monster.total_acceleration;
                        else
                            monster.speed_y += 2 * monster.total_acceleration;
                    }
                    else
                    {
                        if (monster.speed_y < 0)
                            monster.speed_y -= monster.total_acceleration;
                        else
                            monster.speed_y -= 2 * monster.total_acceleration;
                    }
                    if (Math.Abs((it.rectangle.Location.X + 8) - (monster.monsterRectangle.Location.X + 16)) <= monster.jump_distance && Math.Abs((it.rectangle.Location.Y + 8) - (monster.monsterRectangle.Location.Y + 16)) <= monster.jump_distance)
                    {
                        if (it.rectangle.Location.X + 8 > monster.monsterRectangle.Location.X + 16)
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X + 4, MonsterLabel.Location.Y);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                        else
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X - 4, MonsterLabel.Location.Y);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }

                        if (it.rectangle.Location.Y + 8 > monster.monsterRectangle.Location.Y + 16)
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X, MonsterLabel.Location.Y + 4);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                        else
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X, MonsterLabel.Location.Y - 4);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                    }
                    if (monster.acceleration_delay >= 1)
                    {
                        monster.acceleration_delay -= 1;
                    }
                    else
                        monster.Increment();
                    MonsterLabel.Location = new Point(MonsterLabel.Location.X + (int)monster.speed_x, MonsterLabel.Location.Y + (int)monster.speed_y);
                    monster.monsterRectangle.Location = MonsterLabel.Location;
                    if (monster.special == true)
                    {
                        switch (monster.type)
                        {
                            case Monster.Monster_Type.Ghost:
                                if (monster.monsterLabel.Visible == true)
                                    monster.monsterLabel.Hide();
                                if (monster.misctimer <= 0)
                                {
                                    monster.monsterLabel.Show();
                                    monster.misctimer = 10;
                                }
                                else
                                {
                                    monster.misctimer -= 1;
                                }
                                break;
                            case Monster.Monster_Type.Glutton:
                                if (monster.acceleration_delay == 0)
                                {
                                    monster.Increment();
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                #region HookTick
                else if (monster.type == Monster.Monster_Type.Hook)
                {
                    monster.misctimer += 1;
                    if (monster.misctimer >= 150)
                    {
                        hlist.Add(new Hook(this, MonsterLabel.Location.X + monster.size, MonsterLabel.Location.Y + monster.size, true));
                        //if (flipflop >= 2)
                        //  flipflop = 0;
                        monster.misctimer = 0;
                    }
                    foreach (Player p in playerarray)
                    {
                        if (p != null && p.playing == true)
                        {
                            if (monster.monsterRectangle.IntersectsWith(p.rectangle))
                            {
                                Reset(p);
                                if (p.it || p.label.BorderStyle == BorderStyle.Fixed3D)
                                {
                                    p.label.BorderStyle = BorderStyle.None;
                                }
                                foreach (Hook h in hlist)
                                {
                                    this.Controls.Remove(h.label);
                                }
                                hlist.Clear();
                            }
                        }
                    }
                    foreach (Hook h in hlist)
                    {
                        foreach (Player p in playerarray)
                        {
                            if (p != null && p.playing == true)
                            {
                                if (h.rectangle.IntersectsWith(p.rectangle))
                                {
                                    h.Seeking = false;
                                    h.hooked = true;
                                    h.hookedplayer = p;
                                    p.hooked = true;
                                    p.hookedby = h;
                                }
                            }
                        }
                        h.Move();
                        if (h.timer >= (h.redhook == true ? 200 : 500))
                        {
                            h.Seeking = false;
                            h.timer = 0;
                        }
                        if (h.awatingTarget == true)
                        {
                            Random rnd = new Random();
                        restart:
                            int j = rnd.Next(0, playerarray.Length);
                            if (playerarray[j] != null && playerarray[j].playing == true)
                            {
                                if (h.redhook)
                                    h.pointtarget = playerarray[j].label.Location;
                                else
                                    h.Target = playerarray[j];
                                h.Seeking = true;
                                h.awatingTarget = false;
                            }
                            else
                            {
                                goto restart;
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region PosessorTick
            else if (monster.type == Monster.Monster_Type.Posessor)
            {
                #region pushcode
                /*if ((Math.Abs((it.rectangle.Location.X + 8) - (monster.monsterRectangle.Location.X + monster.monstersize)) + Math.Abs((it.rectangle.Location.Y + 8) - (monster.monsterRectangle.Location.Y + monster.monstersize)) <= monster.jumpdistance))
                {
                    if (Math.Abs(it.rectangle.Location.X + 8 - monster.monsterRectangle.Location.X) > monster.monstersize / 2)
                    {
                        if (it.rectangle.Location.X + 8 > monster.monsterRectangle.Location.X + monster.monstersize)
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X - 4, MonsterLabel.Location.Y);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                        else
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X + 4, MonsterLabel.Location.Y);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                    }
                    if (Math.Abs(it.rectangle.Location.Y + 8 - monster.monsterRectangle.Location.Y) > monster.monstersize / 2)
                    {
                        if (it.rectangle.Location.Y + 8 > monster.monsterRectangle.Location.Y + monster.monstersize)
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X, MonsterLabel.Location.Y - 4);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                        else
                        {
                            MonsterLabel.Location = new Point(MonsterLabel.Location.X, MonsterLabel.Location.Y + 4);
                            monster.monsterRectangle.Location = MonsterLabel.Location;
                        }
                    }
                }
                if (MonsterLabel.Location.X < 0)
                {
                    MonsterLabel.Location = new Point(0, MonsterLabel.Location.Y);
                }
                if (MonsterLabel.Location.Y < 0)
                {
                    MonsterLabel.Location = new Point(MonsterLabel.Location.X, 0);
                }
                if (MonsterLabel.Location.X > this.Size.Width - (19 + monster.monstersize))
                {
                    MonsterLabel.Location = new Point(this.Size.Width - (19 + monster.monstersize), MonsterLabel.Location.Y);
                }
                if (MonsterLabel.Location.Y > this.Size.Height - (42 + monster.monstersize))
                {
                    MonsterLabel.Location = new Point(MonsterLabel.Location.X, this.Size.Height - (42 + monster.monstersize));
                }*/
                #endregion
                if (possessorDemonStartTimer >= 1)
                {
                    possessorDemonStartTimer -= 1;
                }
                foreach (Player p in playerarray)
                {
                    if (p != null && p.playing)
                    {
                        p.Move(this.Size);
                        if (possessorDemonStartTimer == 0)
                        {
                            if (p.it != true)
                            {
                                if (Rectangle.Intersect(p.rectangle, possessed.rectangle) != Rectangle.Empty)
                                {
                                    p.playing = false;
                                    p.label.Hide();
                                    int numofplayers = 0;
                                    foreach (Player player in playerarray)
                                        if (player.playing == true)
                                            numofplayers += 1;
                                    if (numofplayers <= 1)
                                    {
                                        switch (possessed.label.Name)
                                        {
                                            case "Player1Label":
                                                MessageBox.Show("Player 1 Won!");
                                                break;
                                            case "Player2Label":
                                                MessageBox.Show("Player 2 Won!");
                                                break;
                                            case "Player3Label":
                                                MessageBox.Show("Player 3 Won!");
                                                break;
                                            case "Player4Label":
                                                MessageBox.Show("Player 4 Won!");
                                                break;
                                        }
                                        this.Close();
                                        initiatorform.Show();
                                    }
                                }
                            }
                        }
                    }
                }
                if (possessorDemonTimer >= 1)
                    possessorDemonTimer -= 1;
                else
                {
                    Random rnd = new Random();
                    possessorDemonTimer = rnd.Next(100, 301);
                    possessed.label.BorderStyle = BorderStyle.None;
                    possessed.label.BackColor = possessed.nonitcolor;
                    possessed.it = false;
                restart:
                    int j = rnd.Next(0, playerarray.Length);
                    if (playerarray[j] != null && playerarray[j].playing == true)
                    {
                        possessed = playerarray[j];
                        //playerarray[j].label.BackColor = Color.Red;
                        playerarray[j].it = true;
                        possessed.label.BorderStyle = BorderStyle.Fixed3D;
                        possessed.label.BackColor = Color.Maroon;
                    }
                    else
                    {
                        goto restart;
                    }
                }
            }
            #endregion
        }

        private void Reset(Player killed)
        {
            foreach (Player p in playerarray)
            {
                if (p != null)
                {
                    p.it = false;
                    p.label.BorderStyle = BorderStyle.None;
                }
            }
            Player_Reset(4);
            killed.playing = false;
            killed.label.Hide();
            Random rnd = new Random();
            int players_remaining = 0;
            foreach (Player p in playerarray){
                if (p != null && p.playing == true)
                    players_remaining++;
            }
            if (players_remaining > 1)
            {
                if (monster.type != Monster.Monster_Type.Posessor & monster.type != Monster.Monster_Type.Hook)
                {
                    int j = rnd.Next(0, players_remaining);
                    if (playerarray[j] != null && playerarray[j].playing == true)
                    {
                        it = playerarray[j];
                        //playerarray[j].label.BackColor = Color.Red;
                        playerarray[j].it = true;
                        it.label.BorderStyle = BorderStyle.Fixed3D;
                    }
                    monster.reset();
                }
            }
            else
            {
                foreach (Player p in playerarray)
                {
                    if (p != null && p.playing == true)
                        switch (p.label.Name)
                        {
                            case "Player1Label":
                                MessageBox.Show("Player 1 Won!");
                                break;
                            case "Player2Label":
                                MessageBox.Show("Player 2 Won!");
                                break;
                            case "Player3Label":
                                MessageBox.Show("Player 3 Won!");
                                break;
                            case "Player4Label":
                                MessageBox.Show("Player 4 Won!");
                                break;
                        }
                }
                this.Close();
                initiatorform.Show();
            }
        }

        private void ControlBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].upkey = true;
                    break;
                case Keys.S:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].downkey = true;
                    break;
                case Keys.A:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].leftkey = true;
                    break;
                case Keys.D:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].rightkey = true;
                    break;
                case Keys.I:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].upkey = true;
                    break;
                case Keys.K:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].downkey = true;
                    break;
                case Keys.J:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].leftkey = true;
                    break;
                case Keys.L:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].rightkey = true;
                    break;
                case Keys.Up:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].upkey = true;
                    break;
                case Keys.Down:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].downkey = true;
                    break;
                case Keys.Left:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].leftkey = true;
                    break;
                case Keys.Right:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].rightkey = true;
                    break;
                case Keys.NumPad5:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].upkey = true;
                    break;
                case Keys.NumPad2:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].downkey = true;
                    break;
                case Keys.NumPad1:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].leftkey = true;
                    break;
                case Keys.NumPad3:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].rightkey = true;
                    break;
                default:
                    break;
            }
        }

        private void ControlBox_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].upkey = false;
                    break;
                case Keys.S:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].downkey = false;
                    break;
                case Keys.A:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].leftkey = false;
                    break;
                case Keys.D:
                    if (playerarray[1] != null && playerarray[1].playing)
                        playerarray[1].rightkey = false;
                    break;
                case Keys.I:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].upkey = false;
                    break;
                case Keys.K:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].downkey = false;
                    break;
                case Keys.J:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].leftkey = false;
                    break;
                case Keys.L:
                    if (playerarray[2] != null && playerarray[2].playing)
                        playerarray[2].rightkey = false;
                    break;
                case Keys.Up:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].upkey = false;
                    break;
                case Keys.Down:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].downkey = false;
                    break;
                case Keys.Left:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].leftkey = false;
                    break;
                case Keys.Right:
                    if (playerarray[0] != null && playerarray[0].playing)
                        playerarray[0].rightkey = false;
                    break;
                case Keys.NumPad5:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].upkey = false;
                    break;
                case Keys.NumPad2:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].downkey = false;
                    break;
                case Keys.NumPad1:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].leftkey = false;
                    break;
                case Keys.NumPad3:
                    if (playerarray[3] != null && playerarray[3].playing)
                        playerarray[3].rightkey = false;
                    break;
                default:
                    break;
            }
        }

        private void PowerUpTimer_Tick(object sender, EventArgs e)
        {
            if (rnd.Next() % 2 == 0) {
                Powerup p = new Powerup(0, new Point(rnd.Next(100, this.Size.Width - 140), rnd.Next(100, this.Size.Height - 140)));
                plist.Add(p);
                this.Controls.Add(p.label);
            }
        }

    }
}
