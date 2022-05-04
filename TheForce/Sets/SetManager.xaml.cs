using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheForce.Sets
{
    public partial class SetManager : ContentPage
    {
        public SetManager()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            Setlist.ItemsSource = await App.diedb.GetSetAsync();
        }

        async void AddSet(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddSet
            {
                BindingContext = new Set()
            });
        }

        async void ConfigSet(object sender, EventArgs e)
        {
            if (Setlist.SelectedItem != null)
            {
                var set = Setlist.SelectedItem as Set;
                await Navigation.PushAsync(new AddSet
                {
                    BindingContext = set
                });
            }
            else
            {
                await DisplayAlert("Select Set", "Please select a set", "OK");
            }
        }

        async void SelectSet(object sender, EventArgs e)
        {
            var set = Setlist.SelectedItem as Set;
            ActiveSet activeSet = new ActiveSet();
            activeSet.refID = set.ID;
            activeSet.Name = set.Name;
            await App.diedb.SaveActiveSetAsync(activeSet);
            App.diedb.ResetRollDieAsync();
            await Navigation.PopAsync();
        }
    }
}
