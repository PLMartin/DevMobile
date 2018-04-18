using System;
using MyPantry.Models;

namespace MyPantry.Controllers
{
    class JSON2Food
    {
        public static Food GetFoodFromJSON(string content, string code)
        {
            Food res = new Food();
            Org.Json.JSONObject json = new Org.Json.JSONObject(content);
            Org.Json.JSONObject product = json.GetJSONObject("product");
            string name = product.Get("product_name_fr").ToString();
            string category = product.Get("generic_name_fr").ToString();
            string imageURL = product.Get("image_url").ToString();

            res.Code = code;
            res.Name = name;
            res.Category = category;
            res.Image = imageURL;
            //Console.Out.WriteLine("Name: {0} \nCategory: {1}", name, category);

            return res;
        }
    }
}