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
    public partial class Form1 : Form
    {
       public string name;
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (radioButtonPhar.Checked == true)
            {
                Form2 Phar = new Form2(2, this, getMonster());
                Phar.Show();
                this.Hide();
            }

            if (radioButtonNoble.Checked == true)
            {
                Form2 Noble = new Form2(3, this, getMonster());
                Noble.Show();
                this.Hide();
            }

            if (radioButtonSlave.Checked == true)
            {
                Form2 Slave = new Form2(4, this, getMonster());
                Slave.Show();
                this.Hide();
            }
        }
        public Monster getMonster() 
        {
            if (radioButton2.Checked == true)
            {
                return new Monster(-1, 120, .025f, .005f, -1, -1, Monster.Monster_Type.Normal);
            }
            if (radioButton3.Checked == true)
            {
                return new Monster(32, 60, .015f, -1, -1, -1, Monster.Monster_Type.Glutton);
            }
            if (radioButton4.Checked == true)
            {
                return new Monster(-1, 60, .025f, .005f, -1, -1, Monster.Monster_Type.Ghost);
            }
            if (radioButton5.Checked == true)
            {
                return new Monster(Monster.Monster_Type.Posessor);
            }
            if (radioButton6.Checked == true)
            {
                return new Monster(-1, 0, .03f, .02f, -1, -1, Monster.Monster_Type.Normal);
            }
            if (radioButton7.Checked == true)
            {
                return new Monster(-1, -1, -1, .02f, 50, 50, Monster.Monster_Type.Normal);
            }
            if (radioButton8.Checked == true)
            {
                return new Monster(Monster.Monster_Type.Hook);
            }
            return new Monster(Monster.Monster_Type.Normal);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
