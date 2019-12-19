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
        private void PersonalAccountPage()
        {
            ClearWorkPlace();

            WorkPlace.Children.Add(CreateVoluentCard());
        }

        private Frame CreateVoluentCard()
        {
            Frame frame = new Frame
            {
                CornerRadius = 30,
                BackgroundColor = StyleColor.color4,
                Margin = new Thickness(20),
                Padding = new Thickness(0, 30, 0, 30),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    BackgroundColor = StyleColor.color2,
                    Padding = new Thickness(10),
                    Children =
                    {
                        new Label
                        {
                            Text = "Карточка волонтера",
                            FontSize = 18,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.Fill
                        },
                        new Label
                        {
                            Text = "Имя  "
                        },
                        new Label
                        {
                            Text = "Фамилия"
                        },
                        new Label
                        {
                            Text = "Другая инвормация"
                        }
                    }
                }
            };

            return frame; 
        }
    }
}
