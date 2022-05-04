using System;
using System.Collections.Generic;
using TheForce.Dice;
using Xamarin.Forms;
using System.Linq;

namespace TheForce.Character
{
    public partial class SheetMan : ContentPage
    {
        public SheetMan()
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

        async void charconfig_Clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new SheetConfig());

        async void att_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var atttoroll = e.Item as Att;
            App.diedb.ResetRollDieAsync();
            var dieID = await App.diedb.GetSkillDieAsync();
            var die = await App.diedb.GetDieAsync(dieID.dieID);

            for (int o = 0; o < atttoroll.Level; o++)
            {
                List<RollDie> Pool = await App.diedb.GetRollDieAsync();
                Boolean present = false;
                foreach (RollDie i in Pool)
                {
                    if (i.refID == die.ID)
                    {
                        RollDie update = i;
                        update.Quantity += 1;
                        await App.diedb.SaveRollDieAsync(update);
                        present = true;
                        break;
                    }
                }
                if (present == false)
                {
                    RollDie add = new RollDie();
                    add.Name = die.Name;
                    add.Quantity = 1;
                    add.refID = die.ID;
                    await App.diedb.SaveRollDieAsync(add);
                }
            }

            await Navigation.PopAsync();
        }

        async void skill_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var skilltoroll = e.Item as Skill;
            var atttoroll = await App.diedb.GetAttAsync(skilltoroll.attID);
            var dieID = await App.diedb.GetSkillDieAsync();
            var updieID = await App.diedb.GetAttDieAsync();
            var die = await App.diedb.GetDieAsync(dieID.dieID);
            var updie = await App.diedb.GetDieAsync(updieID.dieID);

            int greater = new int();
            int lesser = new int();
            try
            {
                if (skilltoroll.Level == atttoroll.Level)
                {
                    greater = skilltoroll.Level;
                }
                else if (skilltoroll.Level < atttoroll.Level)
                {
                    greater = skilltoroll.Level;
                    lesser = atttoroll.Level - skilltoroll.Level;
                }
                else if (skilltoroll.Level > atttoroll.Level)
                {
                    greater = atttoroll.Level;
                    lesser = skilltoroll.Level - atttoroll.Level;
                }


                App.diedb.ResetRollDieAsync();

                for (int o = 0; o < lesser; o++)
                {
                    List<RollDie> Pool = await App.diedb.GetRollDieAsync();
                    Boolean present = false;
                    foreach (RollDie i in Pool)
                    {
                        if (i.refID == die.ID)
                        {
                            RollDie update = i;
                            update.Quantity += 1;
                            await App.diedb.SaveRollDieAsync(update);
                            present = true;
                            break;
                        }
                    }
                    if (present == false)
                    {
                        RollDie add = new RollDie();
                        add.Name = die.Name;
                        add.Quantity = 1;
                        add.refID = die.ID;
                        await App.diedb.SaveRollDieAsync(add);
                    }
                }

                for (int o = 0; o < greater; o++)
                {
                    List<RollDie> Pool = await App.diedb.GetRollDieAsync();
                    Boolean present = false;
                    foreach (RollDie i in Pool)
                    {
                        if (i.refID == updie.ID)
                        {
                            RollDie update = i;
                            update.Quantity += 1;
                            await App.diedb.SaveRollDieAsync(update);
                            present = true;
                            break;
                        }
                    }
                    if (present == false)
                    {
                        RollDie add = new RollDie();
                        add.Name = updie.Name;
                        add.Quantity = 1;
                        add.refID = updie.ID;
                        await App.diedb.SaveRollDieAsync(add);
                    }
                }

                await Navigation.PopAsync();
            }
            catch (Exception)
            {
                await DisplayAlert("Bad Configuration", "The selected skill uses an attribute that is no longer available.", "OK");
            }
        }
    }
}
