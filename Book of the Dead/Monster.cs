using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Book_of_the_Dead
{
    public class Monster
    {
        public enum Monster_Type { Normal = -1, Ghost = 0, Glutton = 1, Posessor = 2, Hook = 3 }
        public System.Windows.Forms.Label monsterLabel;
        //Stats for the monster
        public int size = 16;
        public bool special;
        public int jump_distance = 80;
        private float base_acceleration = .05f;
        public int acceleration_delay = 100;
        private int acceleration_delay_backup = 100;
        public float acceleration_increment = .01f;
        public int delay = 100;
        private int delay_backup = 100;
        public Monster_Type type;

        //Vars used in physics
        public System.Drawing.Point start_Position;
        public System.Drawing.Rectangle monsterRectangle;
        public float speed_x;
        public float speed_y;
        public float total_acceleration = .05f;      
        
        public int misctimer = 0;

        public Monster(Monster_Type m) {
            type = m;
            if (type != Monster_Type.Normal)
                special = true;
        }

        ///Enter each field, -1 sets the stat to its default value
        public Monster(int size, int jump_dist, float base_accel, float accel_incr, int accel_delay, int delay, Monster_Type m) {
            if (size != -1)
                this.size = size;
            if (jump_dist != -1)
                this.jump_distance = jump_dist;
            if (base_accel != -1)
            {
                this.base_acceleration = base_accel;
                this.total_acceleration = base_accel;
            }
            if (accel_delay != -1)
            {
                this.acceleration_delay = accel_delay;
                this.acceleration_delay_backup = accel_delay;
            }
            if (accel_incr != -1)
                this.acceleration_increment = accel_incr;
            if (delay != -1) {
                this.delay = delay;
                this.delay_backup = delay;
            }
            type = m;
            if (type != Monster_Type.Normal)
                special = true;
        }

        public void Increment() {
            if (this.type == Monster_Type.Glutton)
            {
                this.size += 2;
                acceleration_delay = acceleration_delay_backup;
                this.monsterLabel.Size = new System.Drawing.Size(monsterLabel.Size.Width + 4, monsterLabel.Size.Height + 4);
                monsterRectangle.Size = monsterLabel.Size;
            }
            else
            {
                total_acceleration += acceleration_increment;
                acceleration_delay = acceleration_delay_backup;
            }
        }

        public void reset() {
            delay = delay_backup;
            total_acceleration = base_acceleration;
            speed_x = 0;
            speed_y = 0;
            monsterLabel.Location = start_Position;
            monsterRectangle.Location = start_Position;
        }
    }
}
