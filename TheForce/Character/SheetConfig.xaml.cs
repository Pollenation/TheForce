using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace TheForce.Character
{
    public partial class SheetConfig : ContentPage
    {
        public SheetConfig()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var skills = await App.diedb.GetSkillAsync();
            List<Skill> sortskill = skills.OrderBy(X => X.Name).ToList();
            skill.ItemsSource = sortskill;

            var atts = await App.diedb.GetAttAsync();
            List<Att> sortatts = atts.OrderBy(X => X.Name).ToList();
            att.ItemsSource = sortatts;
        }

        async void add_Clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new AddSkill());

        async void skill_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new AddSkill
                {
                    BindingContext = e.Item as Skill
                });
            }
        }

        async void att_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new AddAtt
                {
                    BindingContext = e.Item as Att
                });
            }
        }

        async void addAtt_Clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new AddAtt());

        async void Select_Clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new configdie());
    }
}
