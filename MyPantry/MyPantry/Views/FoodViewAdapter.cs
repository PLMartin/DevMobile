using Android.Content;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

using MyPantry.Models;

namespace MyPantry.Views
{
    class FoodViewAdapter : ArrayAdapter<Food>
    {
        private int ResourceLayout;
        private List<Food> items;
        public FoodViewAdapter(Context context, int textViewResourceId, List<Food> objects) : base(context, textViewResourceId, objects)
        {
            ResourceLayout = textViewResourceId;
            items = objects;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            if (convertView == null)
                convertView = LayoutInflater.FromContext(Context).Inflate(Resource.Layout.ListItem, null, false);

            Food food = GetItem(position);
                        
            TextView name = convertView.FindViewById<TextView>(Resource.Id.name);
            TextView category = convertView.FindViewById<TextView>(Resource.Id.category);

            name.Text = food.Name;
            category.Text = food.Category;

            return convertView;

        }

    }
}