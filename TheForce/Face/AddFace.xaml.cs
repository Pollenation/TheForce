using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheForce.Face
{
    public partial class AddFace : ContentPage
    {

        public AddFace()
        {
            InitializeComponent();
        }

        async void saveface(object sender, EventArgs e)
        {
            if (Name.Text != "NA")
            {
                var tosave = (FacePool)BindingContext;
                if (tosave == null)
                {
                    tosave = new FacePool();
                    tosave.ID = 0;
                }
                tosave.Name = Name.Text;
                if (OpFace.Text == null)
                {
                    tosave.OpFace = "NA";
                }
                else
                {
                    tosave.OpFace = OpFace.Text;
                }
                await App.diedb.SaveFaceAsync(tosave);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Name", Name.Text + " is not a valid name", "OK");
            }
        }

        async void deleteface(object sender, EventArgs e)
        {
            if (Name.Text != "NA")
            {
                var face = (FacePool)BindingContext;
                face.Name = Name.Text;
                face.OpFace = OpFace.Text;
                await App.diedb.DeleteFaceAsync(face);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Name", Name.Text + " is not a valid name", "OK");
            }
        }
    }
}
