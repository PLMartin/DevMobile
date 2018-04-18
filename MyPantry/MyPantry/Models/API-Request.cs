using System;
using System.IO;
using System.Net;


namespace MyPantry.Models
{
    class API_Request
    {
        public static string GetContentOfAPI(string code)
        {
            var request = HttpWebRequest.Create(string.Concat("http://fr.openfoodfacts.org/api/v0/produit/", code));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    return content;
                }
            }
        }
    }
}