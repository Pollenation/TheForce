using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

using Xamarin.Forms;

namespace TheForce.Dice
{
    public partial class DiceManager : ContentPage
    {
        public DiceManager()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var die = await App.diedb.GetDieAsync();
            List<Die> sortdie = die.OrderBy(X => X.Name).ToList();
            DieList.ItemsSource = sortdie;
        }



        async void ConfigDie(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddDice
            {
                BindingContext = new Die()
            }) ;

        }

        async void SelectDie(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new AddDice
                {
                    BindingContext = e.Item as Die
                });
            }
        }
    }
}
