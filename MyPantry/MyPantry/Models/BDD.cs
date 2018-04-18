using MyPantry.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace MyPantry.Views
{
    class BDD
    {
        private SQLiteConnection connection;

        private const string filename = "stock.db";

        public BDD()
        {
            try
            {
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string path = System.IO.Path.Combine(folder, filename);
                connection = new SQLiteConnection(path);
                connection.CreateTable<Food>();
                //Console.Out.WriteLine("Database created");
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
        }


        public void Insert(Food food)
        {
            try
            {
                connection.Insert(food, typeof(Food));
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("Insert error : {0}", ex.Message);
            }
        }

        public void Delete(Food food)
        {
            try
            {
                connection.Delete(food);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Insert error : {0}", ex.Message);
            }
        }

        public List<Food> GetList()
        {
            List<Food> listFood = new List<Food>();
            var stock = this.connection.Table<Food>();
            foreach (Food food in stock)
            {
                listFood.Add(food);
            }
            return listFood;
        }
    }
}