using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace CBS_Project
{
     using MaterialSkin.Controls;

     public partial class InfoForm : MaterialForm
     {
          public static string graphTable;
          public static int turn = 0;

          public static List<string> tableYears = new List<string>();
          public static List<string> tableNames = new List<string>(new string[] {"issizlik","egitim","goc"});
//          public static List<string> commonYears = new List<string>(new string[] { "2010", "2011", "2012","2013" });
          public static List<string> chartColumns = new List<string>();

          public static int f = 1 ;
          public InfoForm()
          {
               InitializeComponent();
          }

          public void fill_form()
          {
                   new InfoForm(). ShowDialog();
          }

          private void button1_Click(object sender, EventArgs e)
          {
               chart1.Series.Clear();
               chart1.Series.Add("chart");

               if (turn == 0)
               {
                    label4.Text = "Yillara gore "+comboBox1.Text+ " orani ";
                    if (!label4.Visible) label4.Visible = true;
                    foreach (var tableYear in tableYears)
                    {
                         graphTable = comboBox1.Text + "_il_" + tableYear;
                         Form1.addColumnQuery("ORAN" ,  graphTable ,  "_" + f.ToString() + tableYear); //1
                         chartColumns.Add(tableYear);
                         //                    MessageBox.Show("graphtable" + graphTable);
                         string data = Form1.mi.Eval("iller._" + f.ToString() + tableYear); //2
                         f++;
                         //                    MessageBox.Show("data: "+data.ToString());
                         chart1.Series["chart"].Points.AddXY(tableYear, data);
                    }
                    tableYears.Clear();
               }
               if (turn == 1)
               {
                    label4.Text = comboBox2.Text + " yilinda  Issizlik, Egitim, ve Goc orani";
                    if (!label4.Visible) label4.Visible = true;
                    foreach (var tableName in tableNames)
                    {
                         graphTable = tableName + "_il_" + comboBox2.Text;
                         Form1.addColumnQuery("ORAN"  ,  graphTable  , "_" + f.ToString() + tableName); //1
                         chartColumns.Add(tableName);
                         string data = Form1.mi.Eval("iller." + "_" + f.ToString() + tableName); //2
                         f++;
                         chart1.Series["chart"].Points.AddXY(tableName, data);
                    }
               }
          }

          private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
          {
               turn = 0;
               comboBox2.Text = string.Empty;
               switch (comboBox1.SelectedIndex)
               {
                    case 0: //issizlik_il
                         tableYears.Clear();
                         tableYears.AddRange(new string[] { "2010", "2011", "2012", "2013" });
                         break;
                    case 1: //egitim_il
                         tableYears.Clear();
                         tableYears.AddRange(new string[] { "2010", "2011", "2012", "2013", "2014", "2015" });
                         break;
                    case 2: //goc_il
                         tableYears.Clear();
                         tableYears.AddRange(new string[] { "2010", "2011", "2012", "2013", "2014", "2015", "2016" });
                         break;
                    default: break;
               }
          }

          private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
          {
               turn = 1;
//               comboBox1.Items.Clear();
               comboBox1.Text = string.Empty;
          }

          private void InfoForm_Load(object sender, EventArgs e)
          {

          }
     }
}
