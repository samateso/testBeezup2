using BeezupAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BeezupAPI
{
    public static class Serialiazer
    {
        public static void JsonSerialize(List<Values> vals)
        {
            JsonSerializerSettings jsonSerializer = new JsonSerializerSettings();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            Console.WriteLine(JsonConvert.SerializeObject(vals, jsonSerializer));
        }

        public static void TextSerialize(List<Values> vals)
        {
            
            foreach (Values values in vals)
            {
                Console.WriteLine(values.ToString());
            }
        }

        public static void XMLSerialize(List<Values> vals)
        {
            XmlSerializer xMLSerialize = new XmlSerializer(typeof(List<Values>));
            xMLSerialize.Serialize(Console.Out, vals);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}