using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YaD
{
    public partial class Form1 : Form
    {
        TT[] arrayTT = new TT[20];
        private static string FileMask = "Fl*.ttxt";        //Маска файла

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            arrayTT[0] = new TT("Порт", "C:\\YandexDisk\\TT_04");
            arrayTT[1] =new TT("Точка 05", "C:\\YandexDisk\\TT_05");
            arrayTT[2] =new TT("Точка 17", "C:\\YandexDisk\\TT_17");

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows.Add(arrayTT[i].NameTT, arrayTT[i].DirTT);
                
                if (CheckFiles(arrayTT[i].DirTT)!=0)
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Green;
                }
            }
            timerUpdate.Start();
        }

        //Поиск файлов 
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

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (CheckFiles(arrayTT[i].DirTT) != 0)
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[2].Style.BackColor = System.Drawing.Color.Green;
                }
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
