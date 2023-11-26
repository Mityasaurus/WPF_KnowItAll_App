using Newtonsoft.Json;
using System;
using System.IO;

namespace WPF_KnowItAll_App.App_layer.Services
{
    public static class JsonService
    {
        public static bool Save<T>(string filename, T obj)
        {
            try
            {
                string json = JsonConvert.SerializeObject(obj);
                File.WriteAllText(filename, json);
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public static T? Load<T>(string filename)
        {
            string json = File.ReadAllText(filename);
            T? result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
