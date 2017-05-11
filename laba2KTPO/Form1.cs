using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2KTPO
{
    public partial class Form1 : Form
    {
        bool q = false;
        bool z = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 100 || numericUpDown2.Value > 100)
            {
                MessageBox.Show("Вводите числа от 1 до 100", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numericUpDown1.Value = 0;
                numericUpDown2.Value = 0;
            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.ColumnCount = 0;
                dataGridView1.RowCount = 1;

                dataGridView2.ColumnCount = 0;

                dataGridView1.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
                dataGridView1.RowCount = Convert.ToInt32(numericUpDown2.Value);

                q = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            z = false;
            try
            {

                try
                {

                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.RowCount; j++)
                        {
                            if (Convert.ToInt32(dataGridView1[i, j].Value) > 2147483647 || Convert.ToInt32(dataGridView1[i, j].Value) < -2147483648)
                            {

                            }
                        }
                    }
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Элементы матрицы должны быть в диапазоне от -2147483648 до 2147483647", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    z = true;
                }
            }
            catch
            {
                MessageBox.Show("Элементы матрицы должны быть только цифрами", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                z = true;
            }
            if (z == false)
            {
                if (numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && q == true)
                {
                    dataGridView2.ColumnCount = Convert.ToInt32(numericUpDown1.Value);
                    dataGridView2.RowCount = Convert.ToInt32(numericUpDown2.Value);

                }
                else
                {
                    dataGridView2.ColumnCount = dataGridView1.ColumnCount;
                    dataGridView2.RowCount = dataGridView1.RowCount;
                }
                if (numericUpDown1.Value != 0 && numericUpDown2.Value != 0 && q == true)
                {
                    for (int i = 0; i < numericUpDown1.Value; i++)
                    {
                        for (int j = 0; j < numericUpDown2.Value; j++)
                            if (i % 2 == 0)
                                dataGridView2[i, j].Value = dataGridView1[i, j].Value;
                            else
                            {
                                for (int h = Convert.ToInt32(numericUpDown2.Value) - 1; h >= 0; h--)
                                    dataGridView2[i, Convert.ToInt32(numericUpDown2.Value) - 1 - h].Value = dataGridView1[i, h].Value;
                            }
                    }
                }
                else
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.RowCount; j++)
                            if (i % 2 == 0)
                                dataGridView2[i, j].Value = dataGridView1[i, j].Value;
                            else
                            {
                                for (int h = dataGridView1.RowCount - 1; h >= 0; h--)
                                    dataGridView2[i, dataGridView1.RowCount - 1 - h].Value = dataGridView1[i, h].Value;
                            }
                    }
                }
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            q = false;
            dataGridView1.DataSource = null;
            dataGridView1.ColumnCount = 0;

            dataGridView2.ColumnCount = 0;

            OpenFileDialog gg = new OpenFileDialog();
            if (gg.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add("score");
                string qwe = gg.FileName;
                StreamReader pgg = new StreamReader(qwe);
                string head = pgg.ReadLine();
                if (head == null)
                {
                    MessageBox.Show("Пустой файл!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    string[] col = System.Text.RegularExpressions.Regex.Split(head, " ");
                for (int i = 0; i < col.Length; i++)
                {
                    ds.Tables[0].Columns.Add();
                }
                StreamReader omgsyka = new StreamReader(qwe);
                string row = omgsyka.ReadLine();
                    while (row != null)
                    {
                        string[] rvalue = System.Text.RegularExpressions.Regex.Split(row, " ");
                        ds.Tables[0].Rows.Add(rvalue);
                        row = pgg.ReadLine();
                    }
                    if (ds.Tables[0].Columns.Count > 100 || ds.Tables[0].Rows.Count > 100)
                    {
                        MessageBox.Show("Размеры превышают допустимые(100х100)", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dataGridView1.DataSource = ds.Tables[0];
                    }
                }
            }
            
       }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog save = new OpenFileDialog();
            save.Filter = "Text|*.txt|All|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                using(StreamWriter sav = new StreamWriter(save.FileName, false, System.Text.Encoding.Default))
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        if(i!=0)
                            sav.WriteLine();
                        for (int j = 0; j < dataGridView2.ColumnCount; j++)
                        {
                           sav.Write(Convert.ToString(dataGridView2[j, i].Value) + " ");
                        }
                    }
            }
        }
    }
}
