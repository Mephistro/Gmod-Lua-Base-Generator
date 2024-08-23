using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GLua_Project_Generator
{
    public partial class Form2 : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        public static Form2 instance;
        public TextBox ptbAddonName;
        public TextBox ptbAuthorName;
        public TextBox ptbVersion;
        public TextBox ptbLastUpdate;
        public TextBox ptbAuthorContact;
        public TextBox ptbInfo;

        public Form2()
        {
            InitializeComponent();
            instance = this;

            ptbAddonName = tbAddonName;
            ptbAuthorName = tbAuthorName;
            ptbVersion = tbVersion;
            ptbLastUpdate = tbLastUpdate;
            ptbAuthorContact = tbAuthorContact;
            ptbInfo = tbInfo;

            tbAddonName.Text = Form1.instance.Data["name"];
            tbAuthorName.Text = Form1.instance.Data["author_name"];
            tbVersion.Text = Form1.instance.Data["version"];
            tbLastUpdate.Text = Form1.instance.Data["up_date"];
            ptbAuthorContact.Text = Form1.instance.Data["author_contact"];
            tbInfo.Text = Form1.instance.Data["info"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // FIX DATA TRANSFER Form2 to Form1
            //Data.Add("name", textBox1.Text);
            Form1.instance.Data["name"] = tbAddonName.Text;
            Form1.instance.Data["author_name"] = tbAuthorName.Text;
            Form1.instance.Data["version"] = tbVersion.Text;
            Form1.instance.Data["up_date"] = tbLastUpdate.Text;
            Form1.instance.Data["author_contact"] = tbAuthorContact.Text;
            Form1.instance.Data["info"] = tbInfo.Text;

            this.Close();
        }
    }
}
