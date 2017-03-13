using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Book_of_the_Dead
{
    public class Powerup
    {
        public Rectangle rectangle;
        public System.Windows.Forms.Label label;
        public int duration;
        public int modifier;
        public bool acquired = false;
        public bool dead = false;
        public enum Type {Speed, Size, Trap, Forcefield }
        public Type pType;
        public Player owner;

        public void picked_up(Player p) 
        {
            p.myPowerups.Add(this);
            owner = p;
            switch (pType) 
            {
                case Type.Speed:
                    p.label.BackColor = this.label.BackColor;
                    p.acceleration += 4;
                    p.powerup = true;
                    break;
            }
        }

        public void expire() 
        {
            switch (pType) 
            {
                case Type.Speed:
                    owner.label.BackColor = owner.nonitcolor;
                    owner.acceleration -= 4;
                    owner.powerup = false;
                    break;
            }
            dead = true;
        }

        public Powerup(int t, Point p) 
        {
            rectangle = new Rectangle(p, new Size(10, 10));
            label = new System.Windows.Forms.Label();
            label.Size = rectangle.Size;
            label.Location = p;
            if (t == 0)
            {
                pType = Type.Speed;
                duration = 180;
                label.BackColor = Color.Teal;
            }


        }
    }
}
