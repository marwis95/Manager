using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listComBox.Items.Add("Partycja systemowa");
            listComBox.SelectedIndex = 0;

            
            //========================Dodawanie obiektow do listy============================

            string nt = Environment.OSVersion.ToString();  //pełna nazwa systemu
            string windows; //nazwa systemu po ludzku (xp, win7, win10)

            Console.WriteLine();
            Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());
            label2.Text = nt;


            if (nt.Contains("5.1")) windows = "Xp";
            else if (nt.Contains("6.1")) windows = "Win7";
            else if (nt.Contains("10.0")) windows = "Win10";
            else windows = "Nie obsługiwany!";

            label4.Text = windows;

            //========================Rozpoznawanie wersji systemu===========================

        }

        private void onBtn_Click(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("REG ADD HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer /v NoDrives /t REG_DWORD /d 00000000 /f");
            cmd.StandardInput.WriteLine("taskkill /f /im explorer.exe");
            cmd.StandardInput.WriteLine("explorer.exe");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

        }

        private void offBtn_Click(object sender, EventArgs e)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("REG ADD HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer /v NoDrives /t REG_DWORD /d 00000004 /f");
            cmd.StandardInput.WriteLine("taskkill /f /im explorer.exe");
            cmd.StandardInput.WriteLine("explorer.exe");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            
        }
    }
}
