using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Web;

namespace AntivirusTesting.Utility
{
    public static class Testing
    {
        public static void Execute(Dictionary<string, string> mylist)
        {
            int count = 0;
            using (var ps = PowerShell.Create())
            {
                //var results =  ps.AddScript(command).Invoke();
                var results = new System.Collections.ObjectModel.Collection<PSObject>();
                String str = "" + "\n\n";
                using (StreamWriter outfile = new StreamWriter(@"C:\Users\Satya.Swain\Source\Repos\Satya-hub\AVScan\AntivirusTesting\TextFile1.txt", true))
                {
                    foreach (var item in mylist)
                    {
                        var command = @"C:\Users\Satya.Swain\Downloads\clamav-0.101.0-win-x64-portable\clamscan --recursive " + item.Value;
                        results = ps.AddScript(command).Invoke();
                        foreach (var result in results)
                        {
                            str += result.ToString();
                            Debug.Write(result.ToString());
                            count++;
                        }
                    }
                    outfile.Write(str.ToString());
                }
                //await void.ConfigureAwait(false);
                //var resultProperty = void.GetType().GetProperty("Result");
                //var results = resultProperty.GetValue(void);


            }
        }
    }
}