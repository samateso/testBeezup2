using BeezupAPI.Models;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BeezupAPI
{
    public class FilterManager : IFilterManager
    {
        public List<Values> step2(string pathfile)
        {
            List<Values> vals = new List<Values>();
            if (pathfile != null)
            {
                using (var reader = new StreamReader(@pathfile))
                using (var csv = new CsvReader(reader))
                {
                    int count = 0;
                    csv.Read();
                    csv.ReadHeader();


                    while (csv.Read())
                    {
                        int colC = 0;
                        int colD = 0;

                        bool booleanColC = int.TryParse(csv.GetField("columnC"), out colC);
                        bool booleanColD = int.TryParse(csv.GetField("columnD"), out colD);

                        if (booleanColC && booleanColD)
                        {
                            int sum = colC + colD;

                            Values values = new Values();

                            if (sum > 100)
                            {
                                values.lineNumber = count;
                                values.sumCD = sum;
                                values.type = "ok";
                                values.concatAB = csv.GetField("columnA") + " " + csv.GetField("columnB");
                            }
                            else
                            {
                                values.lineNumber = count;
                                values.sumCD = sum;
                                values.type = "error";
                                values.errorMessage = "sum is less than 100";
                            }

                            vals.Add(values);
                        }
                    }
                }
            }
            return vals;
        }

        public void step3(string type, string path)
        {
            switch (type)
            {
                case "json":
                    Serialiazer.JsonSerialize(step2(path));
                    break;
                case "text":
                    Serialiazer.TextSerialize(step2(path));
                    break;
                case "xml":
                    Serialiazer.XMLSerialize(step2(path));
                    break;
                default:
                    Serialiazer.JsonSerialize(step2(path));
                    break;
            }
        }
    }
}