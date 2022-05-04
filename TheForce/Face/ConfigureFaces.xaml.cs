using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheForce.Face;
using Xamarin.Forms;
using System.Linq;

namespace TheForce.Dice
{
    public partial class ConfigureFaces : ContentPage
    {
        ObservableCollection<DieFace> dielist = new ObservableCollection<DieFace>();
        public ObservableCollection<DieFace> Dielist { get; set; }

        public ConfigureFaces()
        {
            InitializeComponent();

            facelist.ItemsSource = dielist;
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();
            //need to find a better way to retrive this data
            var die = (Die)BindingContext;
            string faces = die.Faces;
            if (faces == null)
            {
                faces = ",1;";
            }
            string[] allfaces = faces.Split(',');
            App.diedb.ResetDieFaceAsync();
            for (int i = 0; i < die.Facenum; i++)
            {
                DieFace tosave = new DieFace();
                if (allfaces.Length - 1 > i)
                {
                    string[] facelist = allfaces[i + 1].Split(';');
                    tosave.Faces = facelist[1];
                }
                else
                {
                    tosave.Faces = "";
                }
                await App.diedb.SaveDieFaceAsync(tosave);
            }
            var listofdielist = await App.diedb.GetDieFaceAsync();
            dielist.Clear();
            foreach (DieFace i in listofdielist)
            {
                dielist.Add(new DieFace { ID = i.ID, Faces = i.Faces, refID = i.refID });
            }

            var face = await App.diedb.GetFaceAsync();
            List<FacePool> sortface = face.OrderBy(X => X.Name).ToList();
            facepool.ItemsSource = sortface;
        }

        async void configpool(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigFacePool());
        }

        async void facepool_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (facelist.SelectedItem != null)
            {
                DieFace die = facelist.SelectedItem as DieFace;
                FacePool face = e.Item as FacePool;
                die.Faces += "+" + face.Name;
                await App.diedb.SaveDieFaceAsync(die);

                var listofdielist = await App.diedb.GetDieFaceAsync();
                dielist.Clear();
                foreach (DieFace i in listofdielist)
                {
                    dielist.Add(new DieFace { ID = i.ID, Faces = i.Faces, refID = i.refID });
                }
            }
        }

        async void Reset_Clicked(System.Object sender, System.EventArgs e)
        {
            if (facelist.SelectedItem != null)
            {
                DieFace die = facelist.SelectedItem as DieFace;
                die.Faces = "";
                await App.diedb.SaveDieFaceAsync(die);

                var listofdielist = await App.diedb.GetDieFaceAsync();
                dielist.Clear();
                foreach (DieFace i in listofdielist)
                {
                    dielist.Add(new DieFace { ID = i.ID, Faces = i.Faces, refID = i.refID });
                }
            }
        }

        async void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            var die = (Die)BindingContext;
            die.Faces = "";
            die.numeric = numeric.IsChecked;
            if (die.numeric == false)
            {
                foreach (DieFace i in dielist)
                {
                    die.Faces += "," + i.ID + ";" + i.Faces;
                }
            }
            else
            {
                foreach (DieFace i in dielist)
                {
                    die.Faces = "";
                }
            }
            await App.diedb.SaveDieAsync(die);

            await Navigation.PopToRootAsync();
        }

        void numeric_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            if (numeric.IsChecked == true)
            {
                facelist.IsEnabled = false;
                facepool.IsEnabled = false;
            }
            else
            {
                facelist.IsEnabled = true;
                facepool.IsEnabled = true;
            }
        }
    }
}
