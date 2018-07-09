using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MapInfo;

using MaterialSkin.Controls;

namespace CBS_Project
{
     using System.Collections.Generic;
     using System.Net.Security;

     using MaterialSkin;
     using System.Globalization;

     public partial class Form1 : MaterialForm
     {
          public static MapInfoApplication mi;

          Callback callb;

          public InfoForm iform = new InfoForm();

          public string thematicColumn = "oran";

          public string addedColumnName;

          public string table_panel1;

          public string table_panel3;

          public string table_panel4;

          public static string windows1_id;

          public static string windows3_id;

          public static string windows4_id;

          // our panels borders
          private static string panel1_border;

          private static string panel2_border;

          private static string panel3_border;

          private static string panel4_border;

          private static string panel5_border;

          private static string panel6_border;

          private static string cmstr;

          private static string selectedTable;

          private static string selectedLevel;

          private static string selectedYear;

          private Process[] processes;

          private string procName = "MapInfow";

          public static int x = 1;

          [DllImport("user32.dll")]
          private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

          public Form1()
          {
               InitializeComponent();
               callb = new Callback(this);

               // set the theme
               var skinManager = MaterialSkinManager.Instance;
               skinManager.AddFormToManage(this);
               skinManager.Theme = MaterialSkinManager.Themes.DARK;
               skinManager.ColorScheme = new ColorScheme(
                    Primary.BlueGrey800,
                    Primary.BlueGrey900,
                    Primary.BlueGrey500,
                    Accent.Red400,
                    TextShade.WHITE);
               processes = Process.GetProcessesByName(procName);
          }

          private void Form1_Load(object sender, EventArgs e)
          {
               mi = Activator.CreateInstance(Type.GetTypeFromProgID("Mapinfo.Application")) as MapInfoApplication;

               panel1.Parent = tabPage1;
               panel2.Parent = tabPage1;
               panel3.Parent = tabPage2;
               panel4.Parent = tabPage2;
               panel5.Parent = tabPage2;
               panel6.Parent = tabPage2;

               // getting the border of each panel
               panel1_border = panel1.Handle.ToString();
               panel2_border = panel2.Handle.ToString();
               panel3_border = panel3.Handle.ToString();
               panel4_border = panel4.Handle.ToString();
               panel5_border = panel5.Handle.ToString();
               panel6_border = panel6.Handle.ToString();

               createWorkspace(panel1_border, ref windows1_id);
               createWorkspace(panel3_border, ref windows3_id);
               createWorkspace(panel4_border, ref windows4_id);

               // ----------part than handles the form---------------------------//
               int p = panel1.Handle.ToInt32();
               mi.Do("dim p as object");
               mi.Do("set map CoordSys Earth Projection 1, 28");
               mi.SetCallback(callb);

               // callback'taki info cagiriyor.   create info butotn in the background
               mi.Do("create buttonpad \"a\" as toolbutton calling OLE \"info\" id 2001");
          }

          // this block is used to set in which panel the workspace will work
          // take the id of the window where the workspace is running. 
          // We need windows_id after to create a legend and a thematic in that window.
          public void createWorkspace(string tab_panel_border, ref string windows_id)
          {
               try
               {
                    mi.Do(
                         "set next document parent " + tab_panel_border
                         + "style 1"); // set the document to be the child window of the panel
                    mi.Do("set application window " + tab_panel_border);
                // AppDomain.CurrentDomain.BaseDirectory is ./bin/debug
                    mi.Do("run application \"" + AppDomain.CurrentDomain.BaseDirectory + "/workspace1.WOR" + "\""); 
                    windows_id = mi.Eval("frontwindow()");
                    mi.Do("Set Map Window FrontWindow() Zoom Entire Layer 1"); // zoom on the entire layer
               }
               catch (Exception e)
               {
                    MessageBox.Show("Başka bir MAPINFO uygulaması açık.");
                    Close();
               }
          }

          public void selectQuery(string tc, string tableName)
          {
               mi.Do("Select " + tc + " from " + tableName + " order by " + tc + " into sel noselect");
          }

          public static void  addColumnQuery(string thematicColumn,string tableName, string addedColumnName)
          {
               try
               {
                    // add column oran ( nameoftheColumn float)
                    // Add Column "Iller" (ORAN_4 Float)From Issizlik_il_2013 Set To ORAN Where COL2 = COL1  Dynamic
                    if (tableName.Contains("bolge"))
                    {
                         // is the tableName is Bolge
                         mi.Do(
                              "Add Column il_bolge (" + addedColumnName + " float) From " + tableName + " Set To "
                              + thematicColumn + " Where COL3 = COL1 Dynamic"); // add oran to il_bolge
                         mi.Do(
                              "Add Column Iller (" + addedColumnName + " float) From il_bolge Set To "
                              + addedColumnName // add oran to iller
                              + " Where COL2 = COL1"); // Do not put Dynamic!!
                    }
                    else
                    {
                         mi.Do(
                              "Add Column Iller (" + addedColumnName + " float) From " + tableName + " Set To "
                              + thematicColumn ////thematicColumn = "oran"
                              + " Where COL2 = COL1 Dynamic"); // col2 from iller, col1 from tableName
                    }
               }
               catch(Exception exception)
               {
                    MessageBox.Show(exception.GetBaseException().ToString());
               }
          }

          public void shadeWindow(string windows_id, string addedColumnName)
          {
               mi.Do(
                    "shade window " + windows_id + " iller with " + addedColumnName
                    + " ranges apply all use color Brush (2 , 16711680 , 16777215) "
                    + cmstr); // changing color brush does not change color
          }

          public void createLegend(string windows_id, string panel_border)
          {
               mi.Do("Set Next Document Parent " + panel_border + "Style 1");
               mi.Do("Create Cartographic Legend From Window " + windows_id + " Behind Frame From Layer 1");
          }

          public void createRange(TextBox textBox)
          {
               int n = Convert.ToInt16(textBox.Text); // get the value of number of range
               int c_range = Convert.ToInt16(255 / n);
               int rowNumber = Convert.ToInt16(mi.Eval("int(tableinfo(sel,8)/" + Convert.ToString(n) + ")"));

               mi.Do("fetch first from sel"); // pointing to the first row
               string minString = Convert.ToString(mi.Eval("sel.col1"));
               mi.Do("fetch last from sel"); // pointing to the last row
               string maxString = Convert.ToString(mi.Eval("sel.col1"));

               decimal min = decimal.Parse(minString, CultureInfo.InvariantCulture.NumberFormat);
               min = Math.Round((decimal)min, 2, MidpointRounding.AwayFromZero) ;//.ToEven-->1.95 become 1.9
               decimal max = decimal.Parse(maxString, CultureInfo.InvariantCulture.NumberFormat);
               max = Math.Round((decimal)max, 2, MidpointRounding.ToEven);//.wayFromZero--> 1,95 become 2
               max = max + 0.1M;// we add 0.1 cause for example 4.675 become 4.6

               decimal range = (max - min ) / n;
               range = Math.Round((decimal)range, 1, MidpointRounding.ToEven);

               decimal lowRange = min;
               decimal upperRange = lowRange +range;

               string rgb = string.Empty;
               cmstr = string.Empty;
               if (n<3)
               {
                    MessageBox.Show("n must be at least 3!");
               }
               for (int i = 1; i < n; i++)
               {  
                    rgb = Convert.ToString(
                         Form1.mi.Eval(//change the blue and green channel
                              "RGB(10," + Convert.ToString((n - i) * c_range) + ","
                              + Convert.ToString((n - i) * c_range) + ")"));
                    cmstr = cmstr + lowRange.ToString().Replace(',' , '.') + ":" + upperRange.ToString().Replace(',', '.') + "brush(2," + rgb + ",16777215), ";
                    lowRange = lowRange + range;
                    upperRange = upperRange + range;
               }
               cmstr = cmstr + lowRange.ToString().Replace(',', '.') + ":" + max.ToString().Replace(',', '.') + " brush(2," + rgb + ",16777215)";
//               MessageBox.Show(cmstr);
          }



          public void removetematik(string windows_id)
          {
               mi.Do("Set Window " + windows_id + " Front");
               for (int k = Convert.ToInt16(mi.Eval("mapperinfo(" + windows_id + ",9)")); k > 0; k = k - 1)
                    if (Convert.ToInt16(mi.Eval("layerinfo(" + windows_id + "," + Convert.ToString(k) + ",24)")) == 3)
                    {
                         try
                         {
                              mi.Do(
                                   "remove map layer \""
                                   + mi.Eval("layerinfo(" + windows_id + "," + Convert.ToString(k) + ",1)") + "\"");
                         }
                         catch (Exception exception)
                         {
                              MessageBox.Show(exception.GetBaseException().ToString());
                         }
                    }
          }

          private void Form1_FormClosing(object sender, FormClosingEventArgs e)
          {
               try
               {
                    processes = Process.GetProcessesByName(procName);
                    foreach (Process proc in processes)
                    {
                         proc.CloseMainWindow();
                         proc.Kill();
                         proc.WaitForExit();
                    }
               }
               catch (System.NullReferenceException)
               {
                    MessageBox.Show("MapInfo uygulaması zaten kapatılmış.");
               }

               Dispose();
          }

          private void button3_Click(object sender, EventArgs e)
          {
               // create thematic
               addedColumnName = "oran" + x.ToString();
               selectedTable = comboBox1.Text;
               selectedLevel = comboBox2.SelectedIndex == 0 ? "il" : "bolge";
               selectedYear = comboBox3.Text;
               table_panel1 = selectedTable + "_" + selectedLevel + "_" + selectedYear;
//               MessageBox.Show("table name in panel1 :  " + table_panel1);
               try
               {
                    selectQuery(thematicColumn, table_panel1);
//                    MessageBox.Show("addedColumnName: " + addedColumnName);
                    addColumnQuery(thematicColumn, table_panel1, addedColumnName);
               }
               catch (Exception exception)
               {
                    MessageBox.Show("in button3: " + exception.GetBaseException().ToString());
               }

               createRange(textBox1);
               removetematik(windows1_id);
               shadeWindow(windows1_id, addedColumnName);
               createLegend(windows1_id, panel2_border);
               x++;
          }

          private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
          {
               comboBox2.Items.Clear();
               comboBox2.Text = string.Empty;
               comboBox3.Items.Clear();
               comboBox3.Text = string.Empty;
               switch (comboBox1.SelectedIndex)
               {
                    case 0:
                         comboBox2.Items.AddRange(new object[] { "Illere gore", "Bolgelere gore" });
                         break;
                    case 1:
                         comboBox2.Items.AddRange(new object[] { "Illere gore" });
                         break;
                    case 2:
                         comboBox2.Items.AddRange(new object[] { "Illere gore" });
                         break;
                    default: break;
               }
          }

          private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
          {
               this.comboBox3.Items.Clear();
               comboBox3.Text = string.Empty;
               switch (comboBox2.SelectedIndex)
               {
                    case 0: // illere gore
                         if (comboBox1.SelectedIndex == 0) // issizlik_illeregore
                              comboBox3.Items.AddRange(new object[] { "2010", "2011", "2012", "2013" });
                         else if (comboBox1.SelectedIndex == 1) // egitim_illeregore
                              comboBox3.Items.AddRange(new object[] { "2010", "2011", "2012", "2013", "2014", "2015" });
                         else if (comboBox1.SelectedIndex == 2) // goc_illeregore
                              comboBox3.Items.AddRange(
                                   new object[] { "2010", "2011", "2012", "2013", "2014", "2015", "2016" });
                         break;
                    case 1: // bolgelere gore
                         if (comboBox1.SelectedIndex == 0) // issizlik_bolgeleregore
                              comboBox3.Items.AddRange(new object[] { "2014", "2015", "2016" });
                         else if (comboBox1.SelectedIndex == 1) // egitim_bolgeleregore
                              comboBox3.Items.Clear();
                         else if (comboBox1.SelectedIndex == 2) // goc_bolgeleregore
                              comboBox3.Items.Clear();
                         break;
                    default: break;
               }
          }

          private void button4_Click(object sender, EventArgs e)
          {
               // Thematic 1
               addedColumnName = "oran" + x.ToString();
               x++;
               selectedTable = comboBox1.Text;
               selectedLevel = comboBox2.SelectedIndex == 0 ? "il" : "bolge";
               selectedYear = comboBox3.Text;
               table_panel3 = selectedTable + "_" + selectedLevel + "_" + selectedYear;
//               MessageBox.Show("table name in panel3 :  " + table_panel3);
               try
               {
                    selectQuery(thematicColumn, table_panel3);
                    addColumnQuery(thematicColumn, table_panel3, addedColumnName);
               }
               catch (Exception exception)
               {
                    MessageBox.Show("in button4: " + exception.GetBaseException().ToString());
               }

               createRange(textBox1);
               removetematik(windows3_id);
               shadeWindow(windows3_id, addedColumnName);
               createLegend(windows3_id, panel5_border);
               x++;
          }

          private void button5_Click(object sender, EventArgs e)
          {
               // Thematic 2
               addedColumnName = "oran" + x.ToString();
               selectedTable = comboBox1.Text;
               selectedLevel = comboBox2.SelectedIndex == 0 ? "il" : "bolge";
               selectedYear = comboBox3.Text;
               table_panel4 = selectedTable + "_" + selectedLevel + "_" + selectedYear;
//               Console.WriteLine("table name in panel4 :  " + table_panel4);

               try
               {
                    selectQuery(thematicColumn, table_panel4);
                    addColumnQuery(thematicColumn, table_panel4, addedColumnName);
               }
               catch (Exception exception)
               {
                    MessageBox.Show("in button5: " + exception.GetBaseException().ToString());
               }

               createRange(textBox1);
               removetematik(windows4_id);
               shadeWindow(windows4_id, addedColumnName);
               createLegend(windows4_id, panel6_border);
               materialTabControl1.SelectedIndex = 1;
               x++;
          }

          private void button6_Click(object sender, EventArgs e)
          {
               mi.Do("run menu command id 2001");
          }

  
     }
}