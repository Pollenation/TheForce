using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;

namespace TheForce.Face
{
    public partial class ConfigFacePool : ContentPage
    {

        public ConfigFacePool()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            base.OnAppearing();

            var face = await App.diedb.GetFaceAsync();
            List<FacePool> sortskill = face.OrderBy(X => X.Name).ToList();
            facepool.ItemsSource = sortskill;
        }

        async void newface(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFace());
        }

        async void changeface(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null)
            {
                await Navigation.PushAsync(new AddFace
                {
                    BindingContext = e.Item as FacePool
                });
            }
        }
    }
}
