using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Web;

namespace AntivirusTesting.Utility
{
    public class Testing
    {
        public string Execute(Dictionary<string, string> mylist)
        {
            int count = 0;
            using (var ps = PowerShell.Create())
            {
                //var results =  ps.AddScript(command).Invoke();
                var results = new System.Collections.ObjectModel.Collection<PSObject>();
                var infectedstatus =  "";
                using (StreamWriter outfile = new StreamWriter(@"C:\Users\Satya.swain\Source\Repos\Satya-hub\AVScan\AntivirusTesting\TextFile1.txt", true))
                {
                    foreach (var item in mylist)
                    {
                        String str = "" + "\n\n";
                        var command = @"D:\ClamAV\clamscan " + item.Value;
                        results = ps.AddScript(command).Invoke();
                        foreach (var result in results)
                        {
                            str += result.ToString();
                            Debug.Write(result.ToString());
                            
                        }
                        infectedstatus = results[7].BaseObject.ToString() + " , " + results[6].BaseObject.ToString() + " , " + results[8].BaseObject.ToString();
                        count++;
                        outfile.Write(str.ToString());
                    }
                    
                }
                return infectedstatus;
                //await void.ConfigureAwait(false);
                //var resultProperty = void.GetType().GetProperty("Result");
                //var results = resultProperty.GetValue(void);


            }
        }
    }
}