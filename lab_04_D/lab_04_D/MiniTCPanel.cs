using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;

namespace lab_04_D
{
    public partial class MiniTCPanel : UserControl
    {

        public event Action<MiniTCPanel> LoadDrivers;
        public MiniTCPanel()
        {
            InitializeComponent();
        }

        public string CurrentPath
        {
            get { return textBox1.Text;  }
        }

        public string[] Drivers
        {
            set
            {
                if (value != null)
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.AddRange(value);
                }
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            if (LoadDrivers != null)
                LoadDrivers(this);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.TabIndex != -1)
                textBox1.Text = comboBox1.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("..");
            try
            {
                if (Directory.GetDirectories(CurrentPath) != null)
                {
                    string[] directories = Directory.GetDirectories(CurrentPath).ToArray();
                    for (int i = 0; i < directories.Length; i++)
                    {
                        int index = directories[i].LastIndexOf("\\");
                        if (index > 0)
                            directories[i] = directories[i].Substring(index+1,directories[i].Length-index-1);
                        directories[i] = "(D) " + directories[i];
                        if (directories[i][4] != '$')
                        {
                            listBox1.Items.Add(directories[i]);
                        }
                    }
                    string[] files = Directory.GetFiles(CurrentPath).ToArray();
                    for (int i = 0; i < files.Length; i++)
                    {
                        int index = files[i].LastIndexOf("\\");
                        if (index > 0)
                            files[i] = files[i].Substring(index + 1, files[i].Length - index - 1);
                        if (files[i][0] != '$')
                        {
                            listBox1.Items.Add(files[i]);
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                if(ex is DirectoryNotFoundException)
                    Console.WriteLine("Directory not found: " + ex.Message);
                else if(ex is UnauthorizedAccessException)
                    MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String thing = listBox1.SelectedItem.ToString();

            if (!thing.Equals(".."))
            {
                if (thing.StartsWith("(D)"))
                {
                    thing = thing.Remove(0, 4);
                }

                Console.WriteLine(CurrentPath);
                String path = CurrentPath + '\\' + thing;

                try
                {
                    FileAttributes attr = File.GetAttributes(path);

                    if (attr.HasFlag(FileAttributes.Directory))
                        textBox1.Text = path;
                    else
                        System.Diagnostics.Process.Start(path);
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException)
                        Console.WriteLine("Directory not found: " + ex.Message);
                    /*else if (ex is UnauthorizedAccessException)
                    {
                        Console.WriteLine("Couldn't access the file: " + ex.Message);
                        MessageBox.Show("Nie można wyświetlić zawartości folderu. Brak uprawnień.");
                    }*/
                        
                }
            }
            else
            {
                int count = CurrentPath.Split('\\').Length - 1;
                if (count > 1)
                {
                    int index = CurrentPath.LastIndexOf("\\");
                    if (index > 0)
                        textBox1.Text = textBox1.Text.Substring(0, index);
                }
            }

        }
    }
}
