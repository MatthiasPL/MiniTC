using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab_04_D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //miniTCPanel1.CurrentPath = @"c:\";
            miniTCPanel1.LoadDrivers += MiniTCPanel1_LoadDrivers;
            //DriveInfo
            //Path
            //Directory
            //File
        }

        private void MiniTCPanel1_LoadDrivers(MiniTCPanel obj)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<string> dyski = new List<string> {};
            foreach (DriveInfo d in allDrives)
            {
                /*if (!d.IsReady)
                {
                    Console.Write("Dysk {0} nie jest gotowy.", d.Name);
                }
                else
                {*/
                    dyski.Add(d.Name);
                //}
            }
                miniTCPanel1.Drivers = dyski.ToArray();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            miniTCPanel1.LoadDrivers += MiniTCPanel1_LoadDrivers;
        }
    }
}
