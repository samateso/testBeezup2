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

        public string step3(string type, string path, int? take, int? skip)
        {
            string result = String.Empty;
            switch (type)
            {
                case "json":
                    result = Serialiazer.JsonSerialize(step2(path).Skip(skip.Value).Take(take.Value).ToList());
                    break;
                case "text":
                    result = Serialiazer.TextSerialize(step2(path).Skip(skip.Value).Take(take.Value).ToList());
                    break;
                case "xml":
                    result = Serialiazer.XMLSerialize(step2(path).Skip(skip.Value).Take(take.Value).ToList());
                    break;
                case "csv":
                    result = Serialiazer.CSVSerialize(step2(path).Skip(skip.Value).Take(take.Value).ToList());
                    break;
                default:
                    result = Serialiazer.JsonSerialize(step2(path).Skip(skip.Value).Take(take.Value).ToList());
                    break;
            }
            return result;
        }
    }
}