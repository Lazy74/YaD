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
        private string FileMask = "Fl*.ttxt";

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
                //Проверка существования файла по маске "Fl*.ttxt"
                string[] files = System.IO.Directory.GetFiles(arrayTT[i].DirTT, FileMask, System.IO.SearchOption.TopDirectoryOnly);
                if (files.Length > 0)
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
