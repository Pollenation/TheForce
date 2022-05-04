using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TheForce.Character
{
    public partial class AddAtt : ContentPage
    {
        public AddAtt()
        {
            InitializeComponent();
        }

        async void save_Clicked(System.Object sender, System.EventArgs e)
        {
            var tosave = (Att)BindingContext;
            if (tosave == null)
            {
                tosave = new Att();
                tosave.ID = 0;
            }
            tosave.Name = name.Text;
            tosave.Level = Convert.ToInt16(level.Text);
            await App.diedb.SaveAttAsync(tosave);
            await Navigation.PopAsync();
        }

        async void delete_Clicked(System.Object sender, System.EventArgs e)
        {
            var tosave = (Att)BindingContext;
            if (tosave != null)
            {
                await App.diedb.DeleteAttAsync(tosave);
            }
            await Navigation.PopAsync();
        }
    }
}
