﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace YaD
{
    public partial class Form1 : Form
    {
        TT[] arrayTT = new TT[20];
        private static string FileMask = "Fl*.ttxt";        //Маска файла
        public int N = 0;   //Количесткво ТТ

        public Form1()
        {
            InitializeComponent();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            Column1.MinimumWidth =100;
            Column2.MinimumWidth =100;
            Column3.MinimumWidth =50;

            
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.LimeGreen;       //Мение 5 минут (t<300 секунд)
            textBox2.BackColor = Color.Yellow;      //от 5 до 15 минут (300<=t<900)
            textBox3.BackColor = Color.Orange;      //От 15 до 30 минут (900<=t<1800)
            textBox4.BackColor = Color.Red;         //Более 30 минут (1800<=t)
            
            try
            {
                StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "\\Settings.txt", System.Text.Encoding.Default);
                string line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    string[] str = line.Split('|');
                    arrayTT[N] = new TT(str[0], str[1]);
                    N++;
                }
                sr.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл настоек/nПуть к файлу:  " + Directory.GetCurrentDirectory());
                Application.Exit();
                throw;
            }

            //Можно удалить
            //arrayTT[0] = new TT("Порт", "C:\\YandexDisk\\TT_04");
            //arrayTT[1] = new TT("Точка 05", "C:\\YandexDisk\\TT_05");
            //arrayTT[2] = new TT("Точка 17", "C:\\YandexDisk\\TT_17");

            for (int i = 0; i < N; i++)
            {
                dataGridView1.Rows.Add(arrayTT[i].NameTT,arrayTT[i].DirTT);
                
                if (CheckFiles(arrayTT[i].DirTT)!=0)
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.LimeGreen;
                }
            }
            dataGridView1.AutoResizeColumns();
            Column1.Width += 10;
            Column2.Width += 10;
            Column3.Width += 10;
            timerUpdate.Start();
            dataGridView1.Width = Column1.Width + Column2.Width + Column3.Width + 3;
            Width = dataGridView1.Width+16;
            dataGridView1.Height = 2+dataGridView1.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + dataGridView1.ColumnHeadersHeight;
            Height = dataGridView1.Height + 91;
        }

        //Поиск файлов. Возвращает время существования файла
        private int CheckFiles(string Dir)
        {
            int lifetime = 0;
            int nowtime = 0;
            //Проверка существования файла по маске "Fl*.ttxt"
            string[] files = System.IO.Directory.GetFiles(Dir, FileMask, System.IO.SearchOption.TopDirectoryOnly);
            if (files.Length > 0)
            {
                DateTime dateTime = File.GetCreationTime(files[0]);
                lifetime = dateTime.Second + dateTime.Minute*60 + dateTime.Hour*60*60;
                nowtime = DateTime.Now.Second + DateTime.Now.Minute*60 + DateTime.Now.Hour*60*60;
                //MessageBox.Show(Convert.ToString(nowtime-lifetime));
                return nowtime - lifetime;
            }
            else
            {
                return lifetime;
            }
        }

        private string ConvertSecondToTime(int second)
        {
            string time = "";
            if (second/3600 >= 1)
            {
                time = Convert.ToString(second/3600) + "ч ";
                second %= 3600;
            }
            if (second/60 >= 1)
            {
                time += Convert.ToString(second/60) + "м ";
                second %= 60;
            }
            time += Convert.ToString(second) + "c ";
            return time;
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < N; i++)
            {
                int lifetime = CheckFiles(arrayTT[i].DirTT);
                if (lifetime > 0)
                {
                    if (300<=lifetime & lifetime<900)
                    {
                        //от 5 до 15 минут (300<=t<900)
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                        dataGridView1.Rows[i].Cells[2].Value = ConvertSecondToTime(lifetime);
                    }
                    else if (900 <= lifetime & lifetime < 1800)
                    {
                        //От 15 до 30 минут (900<=t<1800)
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Orange;
                        dataGridView1.Rows[i].Cells[2].Value = ConvertSecondToTime(lifetime);
                    }
                    else if (1800 <= lifetime)
                    {
                        //Более 30 минут (1800<=t)
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        dataGridView1.Rows[i].Cells[2].Value = ConvertSecondToTime(lifetime);
                    }
                    else
                    {
                        //Мение 5 минут (t<300 секунд)
                        dataGridView1.Rows[i].Cells[2].Value = ConvertSecondToTime(lifetime);
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.LimeGreen;
                    }
                }
                else
                {
                    //файла нет
                    dataGridView1.Rows[i].Cells[2].Value = "Ок";
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.LimeGreen;
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                ((DataGridView)sender).SelectedCells[0].Selected = false;
            }
            catch { }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==1)
            {
                Process.Start(new ProcessStartInfo("explorer.exe", Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value)));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                FormBorderStyle = FormBorderStyle.SizableToolWindow;
                textBox1.Visible = false;
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                checkBox1.Location = new Point(2,2);
                checkBox2.Location = new Point(130,2);
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Width = Column1.Width + Column3.Width + 3;
                dataGridView1.Location = new Point(0,20);
                Height = dataGridView1.Height + 59;
                Width = dataGridView1.Width + 16;
            }
            else
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                checkBox1.Location = new Point(234, 2);
                checkBox2.Location = new Point(234, 28);
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Width = Column1.Width + Column2.Width + Column3.Width + 3;
                dataGridView1.Location = new Point(0, 52);
                Height = dataGridView1.Height + 91;
                Width = dataGridView1.Width + 16;
                FormBorderStyle = FormBorderStyle.Sizable;
            }
        }
    }

    class TT
    {
        public string NameTT { get; set; }
        public string DirTT { get; set; }

        public TT()
        {
            NameTT = "";
            DirTT = "";
        }

        public TT(string NewNameTT, string NewDirTT)
        {
            NameTT = NewNameTT;
            DirTT = NewDirTT;
        }
        
    }
}
