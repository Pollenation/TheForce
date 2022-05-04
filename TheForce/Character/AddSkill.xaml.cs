using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheForce.Character
{
    public partial class AddSkill : ContentPage
    {
        static List<Att> attributes { get; set; }

        public AddSkill()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var skill = (Skill)BindingContext;
            attributes = await App.diedb.GetAttAsync();
            attpick.ItemsSource = attributes;
        }

        async void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            var tosave = (Skill)BindingContext;
            if (tosave == null)
            {
                tosave = new Skill();
                tosave.ID = 0;
            }
            tosave.Name = Name.Text;
            tosave.Level = Convert.ToInt16(Level.Text);
            if (attpick.SelectedItem != null)
            {
                Att att = attpick.SelectedItem as Att;
                tosave.attID = att.ID;
                await App.diedb.SaveSkillAsync(tosave);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Attribute", "Please select an Attribute", "OK");
            }
        }

        async void Delete_Clicked(System.Object sender, System.EventArgs e)
        {
            var tosave = (Skill)BindingContext;
            if (tosave != null)
            {
                await App.diedb.DeleteSkillAsync(tosave);
            }
            await Navigation.PopAsync();
        }
    }
}
