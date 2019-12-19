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

            WorkPlace.Children.Add(CreateVoluentCard(CurrentUser));

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(new Label
            {
                Text = "Мои мероприятия",
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center
            });

            WorkPlace.Children.Add(CreateButtonPanelInPA());

            WorkPlace.Children.Add(CreateRedLine());
        }

        private Frame CreateVoluentCard(User CurrentUser)
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
                        new Image
                        {
                            Source = "ava.png", HeightRequest = 100, Margin = 5
                        },
                        new Label
                        {
                            Text = "Имя: " + CurrentUser.Name
                        },
                        new Label
                        {
                            Text = "Логин: " + CurrentUser.Login
                        },
                        new Label
                        {
                            Text = "Дата рождения: " + CurrentUser.BirthDate.ToString()
                        },
                        new Label
                        {
                            Text = "Почта: " + CurrentUser.Email
                        },
                        new Label
                        {
                            Text = "Рейтинг: "
                        },
                        new Label
                        {
                            Text = "Телефон: " + CurrentUser.PhoneNumber
                        },
                    }
                }
            };
            return frame;
        }

        private StackLayout CreateButtonPanelInPA()
        {
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 10, 0, 10),
                Orientation = StackOrientation.Horizontal

            };

            Button button = CreateButtonForStack("Текущие");
            stack.Children.Add(button);
            button = CreateButtonForStack("Завершенные");
            stack.Children.Add(button);
            button = CreateButtonForStack("Организация");
            button.Clicked += GoToOrganizerPage;
            stack.Children.Add(button);
            
            return stack;
        }
    }
}
