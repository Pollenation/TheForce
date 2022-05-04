using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheForce.Sets
{
    public partial class AddSet : ContentPage
    {
        public AddSet()
        {
            InitializeComponent();
        }

        async void Save(object sender, EventArgs e)
        {
            var set = (Set)BindingContext;
            set.Name = name.Text;

            await App.diedb.SaveSetAsync(set);
            await Navigation.PopAsync();
        }

        async void Delete(object sender, EventArgs e)
        {
            var set = (Set)BindingContext;
            await App.diedb.DeleteSetAsync(set);
            await Navigation.PopAsync();
        }
    }
}
