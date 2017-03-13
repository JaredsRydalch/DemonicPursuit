using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book_of_the_Dead
{
    public class Hook
    {
        public System.Windows.Forms.Label label;
        public Form2 parentform;
        public int timer = 0;
        public System.Drawing.Rectangle rectangle;
        public bool Seeking = false;
        public Player Target;
        public bool awatingTarget = true;
        public System.Drawing.Point Home;
        public bool hooked = false;
        public bool redhook = false;
        public bool yellowup = false;
        public Player hookedplayer;
        public System.Drawing.Point pointtarget;
        public Hook(Form2 parentform, int xspawn, int yspawn, bool red) 
        {
            this.parentform = parentform;
            this.label = new System.Windows.Forms.Label();
            this.label.Location = new System.Drawing.Point(xspawn, yspawn);
            this.Home = new System.Drawing.Point(xspawn, yspawn);
            this.label.BackColor = red == true ? System.Drawing.Color.Red : System.Drawing.Color.Silver;
            this.label.Size = new System.Drawing.Size(4, 4);
            this.rectangle = new System.Drawing.Rectangle();
            this.rectangle.Location = this.label.Location;
            this.rectangle.Size = this.label.Size;
            this.redhook = red;
            Random rnd = new Random();
            if (rnd.Next(0, 11) == 0)
            {
                this.yellowup = true;
                this.label.BackColor = System.Drawing.Color.Yellow;
            }
            parentform.Controls.Add(this.label);
        }
        public void Move() 
        {
            int xvary = 0;
            int yvary = 0;
            bool straight = false;
            if (this.Seeking == true)
            {
                if (this.redhook != true)
                {
                    if (this.Target.rectangle.X + 8 > this.label.Location.X)
                    {
                        xvary = 1;
                    }
                    else if (this.Target.rectangle.X + 8 < this.label.Location.X)
                    {
                        xvary = -1;
                    }
                    if (this.Target.rectangle.Y + 8 > this.label.Location.Y)
                    {
                        yvary = 1;
                    }
                    else if (this.Target.rectangle.Y + 8 < this.label.Location.Y)
                    {
                        yvary = -1;
                    }
                    if (yvary == 0)
                    {
                        xvary *= 3;
                        straight = true;
                    }
                    if (xvary == 0)
                    {
                        yvary *= 3;
                        straight = true;
                    }
                    if (!straight)
                    {
                        xvary *= 2;
                        yvary *= 2;
                    }
                    this.timer += 1;
                }
                else 
                {
                    if (pointtarget.X > this.label.Location.X)
                    {
                        xvary = 1;
                    }
                    else if (pointtarget.X < this.label.Location.X)
                    {
                        xvary = -1;
                    }
                    if (pointtarget.Y > this.label.Location.Y)
                    {
                        yvary = 1;
                    }
                    else if (pointtarget.Y < this.label.Location.Y)
                    {
                        yvary = -1;
                    }
                    if (yvary == 0)
                    {
                        xvary *= 3;
                        straight = true;
                    }
                    if (xvary == 0)
                    {
                        yvary *= 3;
                        straight = true;
                    }
                    if (!straight)
                    {
                        xvary *= 2;
                        yvary *= 2;
                    }
                    this.timer += 1;
                }
            }
            else
            {
                if (this.Home.X > this.label.Location.X)
                {
                    xvary = 1;
                }
                else if (this.Home.X < this.label.Location.X)
                {
                    xvary = -1;
                }
                if (this.Home.Y > this.label.Location.Y)
                {
                    yvary = 1;
                }
                else if (this.Home.Y < this.label.Location.Y)
                {
                    yvary = -1;
                }
                if (yvary == 0)
                {
                    xvary *= 3;
                    straight = true;
                }
                if (xvary == 0)
                {
                    yvary *= 3;
                    straight = true;
                }
                if (!straight)
                {
                    xvary *= 2;
                    yvary *= 2;
                }
            }
            if (this.redhook) 
            {
                xvary *= 3;
                yvary *= 3;
            }
            if (this.yellowup)
            {
                xvary *= 2;
                yvary *= 2;
            }
            if (hooked) 
            {
                hookedplayer.label.Location = new System.Drawing.Point(this.label.Location.X + xvary, this.label.Location.Y + yvary);
                hookedplayer.rectangle.Location = hookedplayer.label.Location;
                Random rnd = new Random();
                if (rnd.Next(0, 51) == 10)
                {
                    hooked = false;
                    hookedplayer.hooked = false;
                    hookedplayer.hookedby = null;
                    hookedplayer = null;
                }
            }
            this.label.Location = new System.Drawing.Point(this.label.Location.X + xvary, this.label.Location.Y + yvary);
            this.rectangle.Location = this.label.Location;
            if (this.redhook == true && this.Seeking == true && CloseEnough(pointtarget))
                Seeking = false;
            if (Seeking == false && CloseEnough(Home)) 
            {
                this.awatingTarget = true;
            }
        }
        private bool CloseEnough(System.Drawing.Point targetpoint) 
        {
            bool xclose = false;
            bool yclose = false;
            if (this.redhook)
            {
                if (Math.Abs(this.rectangle.Location.X - targetpoint.X) <= (yellowup == true ? 10: 6))
                    xclose = true;
                if (Math.Abs(this.rectangle.Location.Y - targetpoint.Y) <= (yellowup == true ? 10 : 6))
                    yclose = true;
                if (xclose && yclose)
                {
                    this.rectangle.Location = targetpoint;
                    this.label.Location = targetpoint;
                    return true;
                }
                else if (xclose) 
                {
                    this.rectangle.Location = new System.Drawing.Point(targetpoint.X, this.rectangle.Location.Y);
                    this.label.Location = this.rectangle.Location;
                }
                else if (yclose)
                {
                    this.rectangle.Location = new System.Drawing.Point(this.rectangle.Location.X, targetpoint.Y);
                    this.label.Location = this.rectangle.Location;
                }
            }
            else 
            {
                if (Math.Abs(this.rectangle.Location.X - targetpoint.X) <= 3 && Math.Abs(this.rectangle.Location.Y - targetpoint.Y) <= 3)
                {
                    this.rectangle.Location = targetpoint;
                    this.label.Location = targetpoint;
                    return true;
                }
            }
            return false;
        }
    }
}
