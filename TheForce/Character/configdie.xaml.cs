using System;
using System.Collections.Generic;
using TheForce.Dice;

using Xamarin.Forms;

namespace TheForce.Character
{
    public partial class configdie : ContentPage
    {
        public configdie()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            upgradedie.ItemsSource = await App.diedb.GetDieAsync();
            basedie.ItemsSource = await App.diedb.GetDieAsync();
        }

        async void done_Clicked(System.Object sender, System.EventArgs e)
        {
            if (upgradedie.SelectedItem != null && basedie.SelectedItem != null)
            {
                var upgrade = upgradedie.SelectedItem as Die;
                AttDice att = new AttDice();
                att.ID = 1;
                att.dieID = upgrade.ID;
                await App.diedb.SaveAttDieAsync(att);

                var bdie = basedie.SelectedItem as Die;
                SkillDice skill = new SkillDice();
                skill.ID = 1;
                skill.dieID = bdie.ID;
                await App.diedb.SaveSkillDieAsync(skill);

                await Navigation.PopAsync();
            }
        }
    }
}
