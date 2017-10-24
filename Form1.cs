﻿using System;
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

        string nt = Environment.OSVersion.ToString();  //pełna nazwa systemu
        string windows; //nazwa systemu po ludzku (xp, win7, win10)


        public Form1()
        {
            InitializeComponent();
            //listComBox.Items.Add("Partycja systemowa");
            //listComBox.SelectedIndex = 0;

            
            //========================Dodawanie obiektow do listy============================



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

            foreach (object itemChecked in checkedListBox1.CheckedItems){
                tab_zaznaczone[i] = itemChecked.ToString();
                i++;
            }


            for (int j=0; j<tab_zaznaczone.Length; j++){
                MessageBox.Show(tab_zaznaczone[j]);
            }


        }

        private void button2_Click(object sender, System.EventArgs e)
        {


            string[] tab_do_zapisu = new String[checkedListBox1.CheckedItems.Count];
            int i = 0;

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                tab_do_zapisu[i] = itemChecked.ToString();
                i++;
            }


            for (int j = 0; j < tab_do_zapisu.Length; j++)
            {
                MessageBox.Show(tab_do_zapisu[j]);
            }

            SaveFileDialog save = new SaveFileDialog();

            save.FileName = "Plik.ini";

            save.Filter = "Ini File | *.ini";

            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.OpenFile());
                       
                writer.WriteLine("[Info]");
                writer.Write("System = ");
                writer.WriteLine(windows);
                writer.Write("Data = ");
                writer.WriteLine(DateTime.Now);

                for (int k = 0; k < tab_do_zapisu.Length; k++)
                {
                    writer.WriteLine("");
                    writer.WriteLine("[" + tab_do_zapisu[k] + "]");
                }

                    writer.Dispose();
                writer.Close();
             }




        }

        private void button3_Click(object sender, System.EventArgs e)
        {


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                //MessageBox.Show(sr.ReadToEnd());
                richTextBox3.Text = sr.ReadToEnd();
                sr.Close();
            }

            var MyIni = new IniFile(openFileDialog1.FileName);
            //MessageBox.Show(MyIni.Read("System", "Info"));
            //MessageBox.Show(label4.Text);

            if(MyIni.Read("System", "Info") == label4.Text){
            MessageBox.Show("Klucz jest poprawny");

            string[] tab = richTextBox3.Lines;
            string temp = "";


            for (int i = 0; i < richTextBox3.Lines.Count(); i++)
            {
                if ((tab[i].Contains("[")) && (tab[i].Contains("]")) && (tab[i] != "[Info]"))
                {
                    temp = tab[i].Substring(1, tab[i].Length - 2);
                    MessageBox.Show(temp);
                    MessageBox.Show(checkedListBox1.Items.Count.ToString());
                    MessageBox.Show("item" + checkedListBox1.Items.IndexOf("Ukryj").ToString());

                    for (int j = 0; j < tab.Length; j++){
                        if (checkedListBox1.Items.IndexOf(temp) >= 0)
                        {
                            checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(temp), CheckState.Checked);
                        }
                    }

                }
            }

            }else{
                if (MyIni.KeyExists("System", "Info") == true)
                {
                    MessageBox.Show("Wczytany klucz nie pasuje pod ten system operacyjny! \nWczytaj inny klucz");
                }
                else
                {
                    MessageBox.Show("Wczytany plik jest bez sensu");
                }
            }




        }
    }
}
