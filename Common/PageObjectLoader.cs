using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Flipkart;
using Flipkart.PageObjects;

namespace Flipkart.PageObjects
{
    //This class will read the PageObjects from PageObjects File. Every webpage will have a json file which has the identifiers filled with it.
    //This exposes methods which will read the json files and insert into objects.
   public class PageObjectLoader
    {
		Utils utils = new Utils();
        //Sample implementation
        public void LoadPageObjects()
        {
			try
			{
				LoginPageObject.LoginScreenMap = SetPageObject("Login.json");
			}catch(Exception ex)
            {
				utils.Log.Error("Class: PageObjectController | Method: loadPageConfigs | Exception desc : " + ex.Message);
            }
        }

        private Dictionary<String,String> SetPageObject(String fileName)
        {
			Dictionary<String, String> dict = new Dictionary<string, string>();
			try
			{
				//String AppDataFolder = System.Configuration.
					//ConfigurationManager.AppSettings["JsonFilePath"];
				string AppDataFolder = "D:/flipkart/Flipkart/TestData";
				String FilePath = System.IO.Path.Combine(AppDataFolder,  fileName);
				//open the file
				String data = System.IO.File.ReadAllText(FilePath);
				//data= data.Replace("\r\n", "");
				//var jObject = JObject.Parse(System.IO.File.ReadAllText(FilePath));
				dynamic d = JsonConvert.DeserializeObject(data);
				JArray eles = d.elements;
				foreach (JToken token in eles)
				{
					dict.Add(token.ElementAt(0).ToString(), token.ElementAt(1) + "+" + token.ElementAt(2));
				}
			}
			catch(Exception ex)
            {
				utils.Log.Error("Class: PageObjectController | Method: getElements | Exception desc : " + ex.Message);
            }
			return dict;
			
        }
		

	}
}
