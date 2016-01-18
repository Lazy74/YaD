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
            arrayTT[0] = new TT("Порт", "C:\\YandexDisk\\TT_04");
            arrayTT[1] =new TT("Точка 05", "C:\\YandexDisk\\TT_05");
            arrayTT[2] =new TT("Точка 17", "C:\\YandexDisk\\TT_17");

            for (int i = 0; i < 3; i++)
            {
                dataGridView1.Rows.Add(arrayTT[i].NameTT, arrayTT[i].DirTT);
                //MessageBox.Show(arrayTT[i].DirTT);
                //File.Create(arrayTT[i].DirTT + "\\123.txt");
                if (File.Exists(Convert.ToString(arrayTT[i].DirTT + "\\Fl*.ttxt"))) 
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
