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

        public Form1()
        {
            InitializeComponent();

            //for (int i = 0; i < 20; i++)
            //{
            //    if (i + 1 < 9)
            //    {
            //        arrayTT[i] = new TT(@"C:\YandexDisk\Лавка\TT_0" + i,"");
            //    }
            //    else
            //    {
            //        arrayTT[i] = new TT(@"C:\YandexDisk\Лавка\TT_" + i, "");
            //    }
            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            arrayTT[0] = new TT("C:\\YandexDisk\\Лавка\\TT_04", "Порт");
            arrayTT[1] =new TT("C:\\YandexDisk\\Лавка\\TT_05", "Точка 05");
            arrayTT[2] =new TT("C:\\YandexDisk\\Лавка\\TT_17", "Точка 17");

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows.Add(arrayTT[i].NameTT, arrayTT[i].DirTT);
                if (File.Exists(arrayTT[0].DirTT + "\\FL*.ttxt"))
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
