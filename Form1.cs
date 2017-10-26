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
using Microsoft.Win32;
using System.Threading;





namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        string nt = Environment.OSVersion.ToString();  //pełna nazwa systemu
        string windows; //nazwa systemu po ludzku (xp, win7, win10)
        bool b = false;
        List<String> sectionList = new List<String>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Menadżer komponentów systemu Windows";
            richTextBox1.Hide();
            richTextBox2.Hide();
            richTextBox3.Hide();
            //========================Dodawanie obiektow do listy============================



            Console.WriteLine();
            Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());
            label1.Text = "Wersja NT: " + nt;


            if (nt.Contains("5.1")) windows = "Xp";
            else if (nt.Contains("6.1")) windows = "Win7";
            else if (nt.Contains("10.0")) windows = "Win10";
            else windows = "Nie obsługiwany!";

            label3.Text = "Wersja Windows: " + windows;

            //========================Rozpoznawanie wersji systemu===========================

            

            using (StreamReader reader = new StreamReader("config.ini")) {
            richTextBox1.Text = reader.ReadToEnd();
            }

            //========================Ladowanie pliku config.ini=============================

            int j=0;
            string[] tab = richTextBox1.Lines;


            for (int i = 0; i < tab.Length; i++){
                if ((tab[i].Contains("[")) && (tab[i].Contains("]"))){
                    richTextBox2.AppendText(tab[i] + "\n");
                    j++;
                }
            }

            string[] tab_klucze = richTextBox2.Lines;

            //========================Tworzenie tablicy z nazwami dostepnych kluczy=========

            var MyIni = new IniFile("config.ini");

            string section = "";
            string item = "";

            for (int i = 0; i < j; i++)
            {
                section = tab_klucze[i].Substring(1,tab_klucze[i].Length-2);
                item = MyIni.Read("Nazwa", section);
                if ((MyIni.Read("System", section) == windows) || (MyIni.Read("System", section) == "Uniwersalny"))
                {//Dodawanie do listy checkbox tylko tych opcji które pasują pod dany system opracyjny
                    sectionList.Add(section);
                    checkedListBox1.Items.Add(item);
                }
            }


            //=========================Analiza pliku config.ini===========================

        }

        
        
        private void button1_Click(object sender, System.EventArgs e)
        {
            var MyIni = new IniFile("config.ini");
            foreach (object item in checkedListBox1.Items)
            {//Przelatuje w pętli po kolei przez każdą opcję w checkbox list
                if (checkedListBox1.GetItemCheckState(checkedListBox1.Items.IndexOf(item)).ToString().Equals("Checked"))
                {//Jeżeli dana opcja jest zaznaczona to bede wprowadzał ją do systemu
                    foreach (string section in sectionList)
                    {
                        if (MyIni.Read("Nazwa", section).Equals(checkedListBox1.GetItemText(item)))
                        {//Wyszukuję w pliku config zbioru polecen jakie należy wykonać aby wprowadzić zmianę o podanej nazwie
                            int ileS = 1, ileN = 1, ileW = 1;
                            while(MyIni.KeyExists("On_sciezka_" + ileS, section))
                            {//Ile ścieżek
                                ileS++;
                            }
                            while (MyIni.KeyExists("On_nazwa_" + ileN, section))
                            {//Ile nazw
                                ileN++;
                            }
                            while (MyIni.KeyExists("On_wartosc_" + ileW, section))
                            {//Ile wartosci
                                ileW++;
                            }
                            if(ileS == ileN && ileS == ileW && ileS > 1)
                            {
                                for (int i = 1; i < ileS; i++)
                                {
                                    string sciezka = MyIni.Read("On_sciezka_" + i, section);
                                    string nazwa = MyIni.Read("On_nazwa_" + i, section);
                                    string wartosc = MyIni.Read("On_wartosc_" + i, section);
                                    Registry.SetValue(sciezka, nazwa, wartosc);
                                }
                            }//===============Wprowadzanie zmian do rejestru=====================
                            else
                            {
                                MessageBox.Show("Błąd pliku konfiguracyjnego");
                            }
                        }
                    }
                }
                else
                {
                    foreach (string section in sectionList)
                    {
                        if (MyIni.Read("Nazwa", section).Equals(checkedListBox1.GetItemText(item)))
                        {
                            int ileS = 1, ileN = 1, ileW = 1;
                            while (MyIni.KeyExists("Off_sciezka_" + ileS, section))
                            {
                                ileS++;
                            }
                            while (MyIni.KeyExists("Off_nazwa_" + ileN, section))
                            {
                                ileN++;
                            }
                            while (MyIni.KeyExists("Off_wartosc_" + ileW, section))
                            {
                                ileW++;
                            }
                            if (ileS == ileN && ileS == ileW && ileS > 1)
                            {
                                for (int i = 1; i < ileS; i++)
                                {
                                    string sciezka = MyIni.Read("Off_sciezka_" + i, section);
                                    string nazwa = MyIni.Read("Off_nazwa_" + i, section);
                                    string wartosc = MyIni.Read("Off_wartosc_" + i, section);
                                    Registry.SetValue(sciezka, nazwa, wartosc);
                                }
                            }//======Wycofywanie zmian z rejestru (mechanizm działania taki sam jak wprowadzanie)=========
                            else
                            {
                                MessageBox.Show("Błąd pliku konfiguracyjnego");
                            }
                        }
                    }
                }
            }
            //
            Thread.Sleep(1000);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("taskkill /f /im explorer.exe");
            cmd.StandardInput.WriteLine("explorer.exe");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            //================================Resetowanie explorer exe========================================
        }

        private void button2_Click(object sender, System.EventArgs e)
        {


            string[] tab_do_zapisu = new String[checkedListBox1.CheckedItems.Count];
            int i = 0;

            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                tab_do_zapisu[i] = itemChecked.ToString();
                i++;
            }//listowanie zaznaczonych elementów


            SaveFileDialog save = new SaveFileDialog();

            save.FileName = Environment.MachineName + "_" + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year + ".ini";
            //Tworzenie nazwy (nazwa komputera + data) pliku ini
            save.Filter = "Ini File | *.ini";

            if (save.ShowDialog() == DialogResult.OK)
            {

                StreamWriter writer = new StreamWriter(save.OpenFile());
                       
                writer.WriteLine("[Info]");
                writer.Write("System = ");
                writer.WriteLine(windows);
                writer.Write("Data = ");
                writer.WriteLine(DateTime.Now);
                writer.Write("Nazwa komputera = ");
                writer.WriteLine(Environment.MachineName);
                writer.Write("Nazwa uzytkownika = ");
                writer.WriteLine(Environment.UserName);

                for (int k = 0; k < tab_do_zapisu.Length; k++)
                {
                    writer.WriteLine("");
                    writer.WriteLine("[" + tab_do_zapisu[k] + "]");
                }

                    writer.Dispose();
                writer.Close();
             }
            //=============================Tworzenie pliku z wlasnymi kluczami=====================




        }

        private void button3_Click(object sender, System.EventArgs e)
        {


            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                richTextBox3.Text = sr.ReadToEnd();
                sr.Close();
            }

            //==============================Wczytywanie klucza wlasnego============================

            var MyIni = new IniFile(openFileDialog1.FileName);
            

            if (MyIni.Read("System", "Info").Equals(windows))
            {
            MessageBox.Show("Klucz jest poprawny");

            string[] tab = richTextBox3.Lines;
            string temp = "";


            for (int i = 0; i < richTextBox3.Lines.Count(); i++)
            {
                if ((tab[i].Contains("[")) && (tab[i].Contains("]")) && (tab[i] != "[Info]"))
                {
                    temp = tab[i].Substring(1, tab[i].Length - 2);

                    for (int j = 0; j < tab.Length; j++){
                        if (checkedListBox1.Items.IndexOf(temp) >= 0)
                        {
                            checkedListBox1.SetItemCheckState(checkedListBox1.Items.IndexOf(temp), CheckState.Checked);
                        }
                    }

                }
            }//=============================Zaznaczanie opcji w checklist====================================

            }else{
                if (MyIni.KeyExists("System", "Info") == true)
                {
                    MessageBox.Show("Wczytany klucz nie pasuje pod ten system operacyjny! \nWczytaj inny klucz");
                }
                else
                {
                    MessageBox.Show("Klucz jest niepoprawny");
                }
            }




        }

        private void checkAllBtn_Click(object sender, EventArgs e)
        {
            if(b)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                b = !b;
            }//=======================Odznacz wszystko======================
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                b = !b;
            }//=======================Zaznacz wszystko======================
            
        }
    }
}
