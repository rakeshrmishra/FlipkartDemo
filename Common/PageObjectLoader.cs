using System;
using System.Collections.Generic;

namespace Flipkart.PageObjects
{
    //This class will read the PageObjects from PageObjects File. Every webpage will have a json file which has the identifiers filled with it.
    //This exposes methods which will read the json files and insert into objects.
   public class PageObjectLoader
    {
		Utils utils = new Utils();
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
				
			}
			catch(Exception ex)
            {
            }
			return dict;
			
        }
		

	}
}
