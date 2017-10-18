using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System;
using System.IO;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //listComBox.Items.Add("Partycja systemowa");
            //listComBox.SelectedIndex = 0;

            
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

            
            using (StreamReader reader = new StreamReader("config.ini")) {
            richTextBox1.Text = reader.ReadToEnd();
            }

            int j=0;
           // string[] tab_klucze;
            string[] tab = richTextBox1.Lines;
            //MessageBox.Show(tab.Length.ToString());


            for (int i = 0; i < tab.Length; i++){
                if ((tab[i].Contains("[")) && (tab[i].Contains("]"))){
                    //MessageBox.Show(tab[i]);
                    richTextBox2.AppendText(tab[i] + "\n");
                    j++;
                }
            }

            string[] tab_klucze = richTextBox2.Lines;

            var MyIni = new IniFile("config.ini");
            //richTextBox1.Text = MyIni.Read("Nazwa", "Ukrywanie");

            string temp = "";

            for (int i = 0; i < j; i++){
                temp = tab_klucze[i].Substring(1,tab_klucze[i].Length-2);
                listComBox.Items.Add(MyIni.Read("Nazwa", temp));
                if((MyIni.Read("System", temp) == windows) || (MyIni.Read("System", temp) == "Uniwersalny")){
                checkedListBox1.Items.Add(MyIni.Read("Nazwa", temp));
                }
               // MessageBox.Show(temp);
                //MessageBox.Show(MyIni.Read("Nazwa", temp));
            }

            listComBox.SelectedIndex = 0;

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

        private void button1_Click(object sender, System.EventArgs e)
        {

            string[] tab_zaznaczone = new String[checkedListBox1.CheckedItems.Count];
            int i=0;

            //MessageBox.Show(checkedListBox1.Items.Count.ToString());
            //MessageBox.Show(checkedListBox1.CheckedItems.Count.ToString());

            foreach (object itemChecked in checkedListBox1.CheckedItems){
               // MessageBox.Show("Item with title: \"" + itemChecked.ToString() +
               //                 "\", is checked. Checked state is: " +
               //                 checkedListBox1.GetItemCheckState(checkedListBox1.Items.IndexOf(itemChecked)).ToString() + ".");
               
                tab_zaznaczone[i] = itemChecked.ToString();
                i++;
            }


            for (int j=0; j<tab_zaznaczone.Length; j++){
                MessageBox.Show(tab_zaznaczone[j]);
            }


        }
    }
}
