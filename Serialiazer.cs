using BeezupAPI.Models;
using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace BeezupAPI
{
    public static class Serialiazer
    {
        public static string JsonSerialize(List<Values> vals)
        {
            JsonSerializerSettings jsonSerializer = new JsonSerializerSettings();
            jsonSerializer.NullValueHandling = NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(vals, jsonSerializer);
        }

        public static string TextSerialize(List<Values> vals)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Values values in vals)
            {
                stringBuilder.AppendLine(values.ToString());
            }

            return stringBuilder.ToString();
        }

        public static string XMLSerialize(List<Values> vals)
        {
            XmlSerializer xMLSerialize = new XmlSerializer(typeof(List<Values>));
            using (StringWriter textWriter = new StringWriter())
            {
                xMLSerialize.Serialize(textWriter, vals);
                return textWriter.ToString();
            }
        }

        public static string CSVSerialize(List<Values> vals)
        {
            String result = String.Empty;
            using (MemoryStream memoryStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(memoryStream))
            using (CsvWriter csvWriter = new CsvWriter(writer))
            {

                csvWriter.WriteHeader<Values>();
                csvWriter.WriteRecords(vals);

                writer.Flush();
                result = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return result;
        }
    }
}