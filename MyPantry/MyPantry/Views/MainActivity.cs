using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZXing.Mobile;

using MyPantry.Controllers;
using MyPantry.Models;

using static Android.Widget.AdapterView;

namespace MyPantry.Views
{
    [Activity(Label = "Inventaire", MainLauncher = true)]
    class MainActivity : Activity
    {
        public static BDD dao;

        private ListView listView;

        private Food scanned;

        FoodViewAdapter foodAdapter;

        private Button addButton, delButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            listView = this.FindViewById<ListView>(Resource.Id.productList);
            listView.ItemClick += ItemClicked;

            dao = new BDD();
            this.UpdateList();
            this.SetAddButton();
            this.SetDelButton();
        }

        private void ItemClicked(object sender, ItemClickEventArgs e)
        {
            Food selected = foodAdapter.GetItem(e.Position);
            
            string extra = JsonConvert.SerializeObject(selected);
            Bundle bundle = new Bundle();
            bundle.PutString("selected", extra);
            
            Intent intent = new Intent(this, typeof(FoodDescriptor));
            intent.PutExtras(bundle);
            StartActivity(intent);
        }

        private void SetAddButton()
        {
            addButton = this.FindViewById<Button>(Resource.Id.ajouter);
            addButton.Click += async delegate
            {
                MobileBarcodeScanner.Initialize(Application);
                var scanner = new MobileBarcodeScanner
                {
                    UseCustomOverlay = false,
                    TopText = "Scanner votre produit"
                };
                var result = await scanner.Scan();
                if (result != null)
                { 
                    //string test = "3468570270037";
                    this.scanned = JSON2Food.GetFoodFromJSON(API_Request.GetContentOfAPI(result.Text), result.Text);
                    dao.Insert(scanned);
                    UpdateList();
                }
            };
        }

        private void SetDelButton()
        {
            delButton = this.FindViewById<Button>(Resource.Id.supprimer);
            delButton.Click += async delegate
            {
                MobileBarcodeScanner.Initialize(Application);
                var scanner = new MobileBarcodeScanner
                {
                    UseCustomOverlay = false,
                    TopText = "Scanner votre produit"
                };
                var result = await scanner.Scan();
                if (result != null)
                {
                    //string test = "3468570270037";
                    this.scanned = JSON2Food.GetFoodFromJSON(API_Request.GetContentOfAPI(result.Text), result.Text);
                    dao.Delete(scanned);
                    UpdateList();
                }
            };
        }



        public void UpdateList()
        {
            List<Food> listFood = dao.GetList();
            //Console.Out.WriteLine("Liste vide ? {0}", listFood.Count == 0);
            foodAdapter = new FoodViewAdapter(this, Resource.Layout.Main, listFood);
            foodAdapter.NotifyDataSetChanged();
            listView.Adapter = foodAdapter;

            /*foreach(Food food in listFood)
            {
                Console.Out.WriteLine("Nom : {0} | Categorie : {1}", food.Name, food.Category);
            }*/

        }

        protected override void OnResume()
        {
            base.OnResume();
            UpdateList();
        }


    }
}

