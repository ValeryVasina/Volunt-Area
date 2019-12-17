using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VoluntArea
{
    public partial class MainPage : ContentPage
    {
        private void EventPage()
        {
            ClearWorkPlace();
            WorkPlace.Children.Add(new Label
            {
                Text = "Страница с мероприятиями"
            });
        }
    }
}
