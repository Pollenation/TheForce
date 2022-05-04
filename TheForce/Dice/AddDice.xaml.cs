using System;
using System.IO;
using Xamarin.Forms;
using System.Collections.Generic;
using TheForce.Face;
using TheForce.Sets;

namespace TheForce.Dice
{

    public partial class AddDice : ContentPage
    {

        public AddDice()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            setlist.ItemsSource = await App.diedb.GetSetAsync();
        }

        async void DeleteDie(object sender, EventArgs e)
        {
            var die = (Die)BindingContext;
            await App.diedb.DeleteDieAsync(die);
            await Navigation.PopAsync();
        }

        async void ConfigFace(object sender, EventArgs e)
        {
            if (Name.Text == null)
            {
                await DisplayAlert("Name", "Please enter a name", "OK");
            }
            else if (Faces.Text == "0")
            {
                await DisplayAlert("Face Number", "Please enter a number of faces not equal to 0", "OK");
            }
            else if (setlist.SelectedItem == null)
            {
                await DisplayAlert("Set", "Please select a set", "OK");
            }
            else
            {
                var die = (Die)BindingContext;
                die.Name = Name.Text;
                die.Facenum = Convert.ToInt16(Faces.Text);
                var set = setlist.SelectedItem as Set;
                die.Set = set.Name;
                await App.diedb.SaveDieAsync(die);
                await Navigation.PushAsync(new ConfigureFaces
                {
                    BindingContext = die as Die
                });
            }
        }
    }
}
