using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TheForce.Dice;

namespace TheForce
{
    public partial class App : Application
    {
        static ReadDice DieDB;

        public static ReadDice diedb
        {
            get
            {
                if (DieDB == null)
                {
                    DieDB = new ReadDice(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Dice.db3"));
                }
                return DieDB;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MasterNav())
            {
                BarBackgroundColor = Color.DarkRed,
                BarTextColor = Color.White
            };
        }

    }
}
