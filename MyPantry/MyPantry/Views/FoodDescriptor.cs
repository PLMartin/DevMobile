using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Newtonsoft.Json;
using Square.Picasso;
using MyPantry.Models;

namespace MyPantry.Views
{
    [Activity(Label = "Description")]
    public class FoodDescriptor : Activity
    {
        ImageView image;
        TextView name;
        Button del;
        Food selected;
    
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FoodDescriptor);

            image = FindViewById<ImageView>(Resource.Id.image);
            name = FindViewById<TextView>(Resource.Id.name);
            this.SetDelButton();
            selected = JsonConvert.DeserializeObject<Food>(Intent.Extras.GetString("selected"));
            name.Text = selected.Name;
            Picasso.With(this).Load(selected.Image).Into(image);

        }

        private void SetDelButton()
        {
            del = FindViewById<Button>(Resource.Id.del);
            del.Click += delegate
            {
                MainActivity.dao.Delete(selected);
                this.Finish();
            };

        }
    }
}