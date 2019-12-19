using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoluntArea.Interfaces;
using Xamarin.Forms;

namespace VoluntArea
{
    public partial class MainPage : ContentPage
    {
        //VolunteerManager manager = new VolunteerManager();
        public MainPage()
        {
            InitializeComponent();
            LogInPage();
        }

        private StackLayout CreateNewSrack(int row, string textForLabel, Entry entry)
        {
            StackLayout stLog = new StackLayout { Orientation = StackOrientation.Horizontal };
            if (row != -1)
            {
                Grid.SetColumn(stLog, 1);
                Grid.SetRow(stLog, row);
            }
            stLog.Children.Add(new Label
            {
                Text = textForLabel,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            });
            stLog.Children.Add(entry);
            return stLog;
        }

        //Очистка главного окна от содержимого
        public void Clear()
        {
            CentralWindow.Children.Clear();
        }
    }
}
