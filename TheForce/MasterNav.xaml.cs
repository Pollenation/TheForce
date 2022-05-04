using System;
using System.IO;
using System.Collections.Generic;
using TheForce.Dice;
using Xamarin.Forms;
using SQLite;
using TheForce.Results;
using TheForce.Character;
using TheForce.Sets;
using System.Linq;

namespace TheForce
{
    public partial class MasterNav : ContentPage
    {
        public static List<ResultView> grres = new List<ResultView>();
        public static List<ResultView> calcres = new List<ResultView>();

        public MasterNav()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var die = await App.diedb.GetDieAsync();
            List<Die> setdie = new List<Die>();
            var setname = await App.diedb.GetActiveSetAsync();
            foreach (Die i in die)
            {
                if (i.Set == setname[0].Name)
                {
                    setdie.Add(i);
                }
            }
            List<Die> sortdie = setdie.OrderBy(X => X.Name).ToList();
            DieList.ItemsSource = sortdie;

            var rolldie = await App.diedb.GetRollDieAsync();
            List<RollDie> sortrolldie = rolldie.OrderBy(X => X.Name).ToList();
            ToRollList.ItemsSource = sortrolldie;
        }

        async void OpenConfig(object sender, EventArgs e) => await Navigation.PushAsync(new DiceManager { });

        async void DieAdd(object sender, ItemTappedEventArgs e)
        {
            var die = e.Item as Die;
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

            List<RollDie> test = await App.diedb.GetRollDieAsync();
            List<Die> testdie = await App.diedb.GetDieAsync();

            ToRollList.ItemsSource = await App.diedb.GetRollDieAsync();
        }

        async void DeleteDie(object sender,ItemTappedEventArgs e)
        {
            var rolldie = e.Item as RollDie;
            if (rolldie.Quantity > 1)
            {
                rolldie.Quantity -= 1;
                await App.diedb.SaveRollDieAsync(rolldie);
            }
            else
            {
                await App.diedb.DeleteRollDieAsync(rolldie);
            }

            ToRollList.ItemsSource = await App.diedb.GetRollDieAsync();
        }

        async void Roll_Clicked(System.Object sender, System.EventArgs e)
        {
            Result newres = new Result();
            var toroll = await App.diedb.GetRollDieAsync();
            foreach (RollDie i in toroll)
            {
                for (int o = 0; o < i.Quantity; o++)
                {
                    Die die = await App.diedb.GetDieAsync(i.refID);
                        var Rand = new Random();
                        int resultnum = Rand.Next(1, die.Facenum+1);
                        if (die.numeric == false)
                        {
                            //parse die faces
                            string face = die.Faces;
                            string[] allfaces = face.Split(',');
                            string unformresult = allfaces[resultnum];
                            string[] results = unformresult.Split('+');
                            if (results.Length > 1)
                            {
                                for (int y = 1; y < results.Length; y++)
                                {
                                    newres.Add(results[y]);
                                }
                            }
                        }
                        else
                        {
                            newres.Add(die.Name, resultnum);
                        }
                }
            }
            grres.Clear();
            try
            {
                grres = newres.getresults();
                calcres = await newres.getcalcresults();
            }
            catch (KeyNotFoundException)
            {
                await DisplayAlert("Bad Configuration", "A rolled face no longer exists. Please correct the configuration", "OK");
            }
            List<ResultView> sortgrres = grres.OrderBy(X => X.Name).ToList();
            List<ResultView> sortcalcres = calcres.OrderBy(X => X.Name).ToList();
            results.ItemsSource = sortgrres;
            calcresults.ItemsSource = sortcalcres;
        }

        async void char_Clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new SheetMan { });

        async void Set_clicked(System.Object sender, System.EventArgs e) => await Navigation.PushAsync(new SetManager { });
    }
}
