using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GLua_Project_Generator
{
    public partial class Form1 : Form
    {

        public Dictionary<string, string> Data = new Dictionary<string, string>();
        List<data> _data = new List<data>();

        private bool mouseIsDown = false;
        private Point firstPoint;
        public static Form1 instance;
        public static TreeNode Root;

        public static TreeNode LuaRoot;
        public static TreeNode AutorunRoot;
        public static TreeNode EffectRoot;
        public static TreeNode EntitiesRoot;
        public static TreeNode VguiRoot;
        public static TreeNode WeaponsRoot;

        public class data
        {
            public string name { get; set; }
            public string author_name { get; set; }
            public string version { get; set; }
            public string up_date { get; set; }
            public string author_contact { get; set; }
            public string info { get; set; }

        }


        public Form1()
        {
            InitializeComponent();

            instance = this;

            Data["name"] = " ";
            Data["author_name"] = " ";
            Data["version"] = " ";
            Data["up_date"] = " ";
            Data["author_contact"] = " ";
            Data["info"] = " ";
            var adnData = Data;

            Root = treeView1.SelectedNode = treeView1.Nodes[0];

            Root.Nodes.Add("lua");
            Root.ExpandAll();

            LuaRoot = Root.Nodes[0];
            
            CheckName();

        }

        private void CheckName()
        {
            if (textBox2.Text.Length <= 0)
            {
                checkBox1.Enabled = false;
                checkedListBox1.Enabled = false;
                checkedListBox2.Enabled = false;

                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;

                button4.Enabled = false;
            }
            else
            {

                Root.Expand();
                Root.Text = textBox2.Text;

                checkBox1.Enabled = true;
                checkedListBox1.Enabled = true;
                checkedListBox2.Enabled = true;

                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;

                button4.Enabled = true;
            }
        }

        private void RemoveNode(TreeNode root, string nodeName)
        {
            foreach (TreeNode node in root.Nodes)
            {
                if (node.Text == nodeName)
                {
                    node.Remove();
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckName();
                e.SuppressKeyPress = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Root.Nodes.Add("addons.txt");
                treeView1.Nodes[0].Expand();
                linkLabel1.Enabled = true;
            }
            else
            {
                foreach (TreeNode node in Root.Nodes)
                {
                    if (node.Text == "addons.txt")
                    {
                        node.Remove();
                    }
                }
                linkLabel1.Enabled = false;
            }
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

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    if (e.NewValue == CheckState.Checked)
                    {
                        AutorunRoot = LuaRoot.Nodes.Add("autorun");

                        TreeNode arClient = AutorunRoot.Nodes.Add("Client");
                        TreeNode arSever = AutorunRoot.Nodes.Add("Server");

                        arClient.Nodes.Add(textBox3.Text + "init.lua");
                        arSever.Nodes.Add(textBox4.Text + "init.lua");

                        AutorunRoot.Nodes.Add(textBox5.Text + "init.lua");

                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(LuaRoot, "autorun");
                    }

                    break;

                case 1:
                    if (e.NewValue == CheckState.Checked)
                    {
                        EffectRoot = LuaRoot.Nodes.Add("effects");

                        EffectRoot.Nodes.Add("effect_default.lua");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(LuaRoot, "effects");
                    }

                    break;

                case 2:
                    if (e.NewValue == CheckState.Checked)
                    {
                        EntitiesRoot = LuaRoot.Nodes.Add("entities");

                        TreeNode DefaultEntity = EntitiesRoot.Nodes.Add("DefaultEntity");

                        DefaultEntity.Nodes.Add(textBox3.Text + "init.lua");
                        DefaultEntity.Nodes.Add(textBox4.Text + "init.lua");
                        DefaultEntity.Nodes.Add(textBox5.Text + "shared.lua");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(LuaRoot, "entities");
                    }
                    break;

                case 3:
                    if (e.NewValue == CheckState.Checked)
                    {
                        VguiRoot = LuaRoot.Nodes.Add("vgui");

                        VguiRoot.Nodes.Add("default_vgui.lua");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(LuaRoot, "vgui");
                    }
                    break;

                case 4:
                    if (e.NewValue == CheckState.Checked)
                    {
                        WeaponsRoot = LuaRoot.Nodes.Add("weapons");

                        TreeNode DefaultSwep = WeaponsRoot.Nodes.Add("swep_default");

                        DefaultSwep.Nodes.Add("defaultswep.lua");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(LuaRoot, "weapons");
                    }
                    break;
            }

        }

        private void checkedListBox2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    if (e.NewValue == CheckState.Checked)
                    {
                        Root.Nodes.Add("materials");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(Root, "materials");
                    }

                    break;

                case 1:
                    if (e.NewValue == CheckState.Checked)
                    {
                        Root.Nodes.Add("models");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(Root, "models");
                    }
                    break;

                case 2:
                    if (e.NewValue == CheckState.Checked)
                    {
                        Root.Nodes.Add("particles");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(Root, "particles");
                    }
                    break;

                case 3:
                    if (e.NewValue == CheckState.Checked)
                    {
                        Root.Nodes.Add("sounds");
                    }
                    else if (e.NewValue == CheckState.Unchecked)
                    {
                        RemoveNode(Root, "sounds");
                    }
                    break;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0) 
            {
                DirectoryInfo RootFolder = Directory.CreateDirectory(textBox1.Text +"\\"+ textBox2.Text);
                if (RootFolder.Exists)
                {
                    DirectoryInfo LuaRootFolder = Directory.CreateDirectory(RootFolder.FullName + "\\lua");

                    if (LuaRootFolder.Exists)
                    {
                        for (int i = 0; i < checkedListBox1.Items.Count; i++) 
                        { 
                            switch (checkedListBox1.GetItemCheckState(i) ) 
                            {
                                case CheckState.Checked:

                                    switch (i)
                                    {
                                        case 0:
                                            DirectoryInfo arRootFolder = Directory.CreateDirectory(LuaRootFolder.FullName + "\\autorun");

                                            if (arRootFolder.Exists)
                                            {
                                                DirectoryInfo arClientRoot = Directory.CreateDirectory(arRootFolder.FullName + "\\client");
                                                File.Create(arClientRoot.FullName + "\\" + textBox3.Text + "init.lua").Close();
                                                File.WriteAllText(arClientRoot.FullName + "\\" + textBox3.Text + "init.lua", "print('client loaded!')");

                                                DirectoryInfo arServerRoot = Directory.CreateDirectory(arRootFolder.FullName + "\\server");
                                                File.Create(arServerRoot.FullName + "\\" + textBox4.Text + "init.lua").Close();
                                                File.WriteAllText(arServerRoot.FullName + "\\" + textBox4.Text + "init.lua", "print('server loaded!')");

                                                File.Create(arRootFolder.FullName + "\\" + textBox5.Text + "init.lua").Close();
                                                File.WriteAllText(arRootFolder.FullName + "\\" + textBox5.Text + "init.lua", "print('shared loaded!')");
                                            }
                                            break;

                                        case 1:
                                            DirectoryInfo effectsRootFolder = Directory.CreateDirectory(LuaRootFolder.FullName + "\\effects");

                                            if (effectsRootFolder.Exists)
                                            {
                                                File.Create(effectsRootFolder.FullName + "\\effect_default.lua").Close();
                                            }
                                            break;

                                        case 2:
                                            DirectoryInfo entitiesRootFolder = Directory.CreateDirectory(LuaRootFolder.FullName + "\\entities");

                                            if(entitiesRootFolder.Exists)
                                            {
                                                DirectoryInfo defaultEntityFolder = Directory.CreateDirectory(entitiesRootFolder.FullName + "\\DefaultEntity");

                                                if (defaultEntityFolder.Exists)
                                                {
                                                    File.Create(defaultEntityFolder.FullName + "\\" + textBox3.Text + "init.lua").Close();
                                                    File.WriteAllText(defaultEntityFolder.FullName + "\\" + textBox3.Text + "init.lua", "print('[client] entity loaded')");

                                                    File.Create(defaultEntityFolder.FullName + "\\" + textBox4.Text + "init.lua").Close();
                                                    File.WriteAllText(defaultEntityFolder.FullName + "\\" + textBox4.Text + "init.lua", "print('[server] entity loaded')");

                                                    File.Create(defaultEntityFolder.FullName + "\\" + textBox5.Text + "shared.lua").Close();
                                                    File.WriteAllText(defaultEntityFolder.FullName + "\\" + textBox5.Text + "shared.lua", "print('[shared] entity loaded')");
                                                }
                                            }
                                            break;

                                        case 3:
                                            DirectoryInfo vguiRootFolder = Directory.CreateDirectory(LuaRootFolder.FullName + "\\vgui");

                                            if (vguiRootFolder.Exists)
                                            {
                                                File.Create(vguiRootFolder.FullName + "\\default_vgui.lua").Close();
                                            }
                                            break;

                                        case 4:
                                            DirectoryInfo SwepRootFolder = Directory.CreateDirectory(LuaRootFolder.FullName + "\\weapons");
                                            
                                            if (SwepRootFolder.Exists) 
                                            {
                                                DirectoryInfo defaultSwepFolder = Directory.CreateDirectory(SwepRootFolder.FullName + "\\swep_default");

                                                if (defaultSwepFolder.Exists) 
                                                {
                                                    File.Create(defaultSwepFolder.FullName + "\\swep_default.lua").Close();
                                                    File.WriteAllText(defaultSwepFolder.FullName + "\\swep_default.lua", "print('[swep] File loaded')");
                                                }
                                            }
                                            break;
                                    }
                                break;
                            }
                        }
                    }

                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        switch (checkedListBox2.GetItemCheckState(i))
                        {
                            case CheckState.Checked:

                                switch (i)
                                {
                                    case 0:
                                        Directory.CreateDirectory(RootFolder.FullName + "\\materials");
                                        break;

                                    case 1:
                                        Directory.CreateDirectory(RootFolder.FullName + "\\models");
                                        break;

                                    case 2:
                                        Directory.CreateDirectory(RootFolder.FullName + "\\particles");
                                        break;

                                    case 3:
                                        Directory.CreateDirectory(RootFolder.FullName + "\\sound");
                                        break;

                                }

                            break;
                        }
                    }

                    if (checkBox1.Checked)
                    {
                        _data.Add(new data()
                        {
                            name = Data["name"],
                            author_name = Data["author_name"],
                            version = Data["version"],
                            up_date = Data["up_date"],
                            author_contact = Data["author_contact"],
                            info = Data["info"]
                        });

                        File.Create(RootFolder.FullName + "\\addon.txt").Close();
                        File.WriteAllText(RootFolder.FullName + "\\addon.txt", JsonSerializer.Serialize(_data));
                    }

                }
                MessageBox.Show("Successfully generated your project files!!", "Success!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show("Please show a path before generating your project files!", "Error generating", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
