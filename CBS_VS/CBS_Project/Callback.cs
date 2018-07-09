using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CBS_Project
{
     [ComVisible(true)]
     public class Callback
     {
          Form1 mf;
          public static double x, y;
          public Callback(Form1 mf)
          {
               this.mf = mf;
          }

          public void info(string a)
          {
               int k = Convert.ToInt32(Form1.mi.Eval("searchpoint(frontwindow(),commandinfo(1),commandinfo(2))"));
               string tabloadi = "";
               for (int i = 1; i <= k; i++)
               {
                    tabloadi = Form1.mi.Eval("SearchInfo(" + i.ToString() + ",1)");
                    String row_id = Form1.mi.Eval("SearchInfo(" + i.ToString() + ",2)");
                    Form1.mi.Do("Fetch rec " + row_id + " From " + tabloadi);
                    if (tabloadi == "Iller")
                    {
                         mf.Invoke(new mapinfo(mf.iform.fill_form));
                    }
               }
          }
          private void getCoordinates()
          {
               try
               {
                    x = Convert.ToDouble(Form1.mi.Eval("commandinfo(1)"));
                    y = Convert.ToDouble(Form1.mi.Eval("commandinfo(2)"));
               }
               catch
               {
                    Form1.mi.Do("p = commandinfo(1)");
               }
          }

          public delegate void mapinfo();

     }
}
