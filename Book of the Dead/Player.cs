using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book_of_the_Dead
{
    public class Player
    {
        public List<Powerup> myPowerups = new List<Powerup>();
        public bool upkey = false;
        public bool downkey = false;
        public bool leftkey = false;
        public bool rightkey = false;
        public System.Windows.Forms.Label label;
        public System.Drawing.Rectangle rectangle;
        public bool it = false;
        public bool playing = false;
        public System.Drawing.Color nonitcolor;
        public int immunetimer = 0;
        public int acceleration = 4;
        public bool hooked = false;
        public bool powerup = false;
        public Hook hookedby;
        public void Move(System.Drawing.Size fieldsize)
        {
            foreach (Powerup p in myPowerups) {
                p.duration -= 1;
                if (p.duration <= 0)
                    p.expire();
            }
            for (int i = myPowerups.Count - 1; i >= 0; i--)
            {
                if (myPowerups[i].dead == true)
                    myPowerups.Remove(myPowerups[i]);
            }
            if (it & acceleration < 6)
            {
                acceleration = 6;
            }
            else if (powerup == false)
                acceleration = 4;
            if (upkey == true && label.Location.Y >= 0)
            {
                if (label.Location.Y <= acceleration)
                    label.Location = new System.Drawing.Point(label.Location.X, 0);
                else
                    label.Location = new System.Drawing.Point(label.Location.X, label.Location.Y - acceleration);
            }
            if (downkey == true && label.Location.Y <= (fieldsize.Height - label.Size.Height - 48))
                label.Location = new System.Drawing.Point(label.Location.X, label.Location.Y + acceleration);
            if (leftkey == true && label.Location.X >= 0)
            {
                if (label.Location.X <= acceleration)
                    label.Location = new System.Drawing.Point(0, label.Location.Y);
                else
                    label.Location = new System.Drawing.Point(label.Location.X - acceleration, label.Location.Y);
            }
            if (rightkey == true && label.Location.X <= (fieldsize.Width - 38))
                label.Location = new System.Drawing.Point(label.Location.X + acceleration, label.Location.Y);
                
            rectangle.Location = label.Location;
            if (immunetimer >= 1)
                immunetimer -= 1;
        }
        public void Move(int x, int y) 
        {
            label.Location = new System.Drawing.Point(x, y);
            rectangle.Location = label.Location;
        }
    }
}
