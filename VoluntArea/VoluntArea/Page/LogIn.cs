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
        //страница входа
        private void LogInPage()
        {
            Clear();
            CentralWindow.BackgroundColor = Color.White;

            Frame frame = new Frame
            {
                CornerRadius = 20,
                HasShadow = true,
                BackgroundColor = StyleColor.color3,
                Margin = new Thickness(40, 30, 40, 40),
            };

            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            CentralWindow.Children.Add(stack);
            stack.Children.Add(new Frame
            {
                Padding = 0,
                HasShadow = true,
                Content = new Label
                {
                    Text = "    VoluntArea",
                    TextColor = Color.White,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Fill,
                    FontSize = 45,
                    BackgroundColor = StyleColor.color1,
                    VerticalTextAlignment = TextAlignment.Center,
                    HeightRequest = 150

                }
            });

            StackLayout stEnt = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Fill
            };

            frame.Content = stEnt;

            stack.Children.Add(frame);
            stEnt.Children.Add(new Label
            {
                Text = "Войти",
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 25,
                FontAttributes = FontAttributes.Bold
            });
            stEnt.Children.Add(CreateNewSrack(-1, "Логин", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stEnt.Children.Add(CreateNewSrack(-1, "Пароль", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));

            StackLayout butst = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = new Thickness(15, 10, 10, 5)
            };

            stEnt.Children.Add(butst);

            Frame frameR = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = StyleColor.color2,
                HasShadow = true,
                Padding = 0
            };

            Frame frameE = new Frame
            {
                CornerRadius = 10,
                BackgroundColor = StyleColor.color2,
                HasShadow = true,
                Padding = 0
            };

            Button regBut = new Button
            {
                Text = "Регистрация",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = StyleColor.color2
            };

            frameR.Content = regBut;

            Button entBut = new Button
            {
                Text = "Войти",
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = StyleColor.color2
            };

            frameE.Content = entBut;

            regBut.Clicked += RegButtonEvent;
            entBut.Clicked += EntButtonEvent;
            butst.Children.Add(frameR);
            butst.Children.Add(frameE);
        }


        //Событие перехода на страницу регистрации
        private void RegButtonEvent(object sender, EventArgs e)
        {
            RegistrationPage();
        }

        //Событие входа
        private void EntButtonEvent(object sender, EventArgs e)
        {
            Entry login = (Entry)((StackLayout)(((StackLayout)(((StackLayout)(((Frame)((Button)sender).Parent).Parent)).Parent)).Children[1])).Children[1];
            Entry password = (Entry)((StackLayout)(((StackLayout)(((StackLayout)(((Frame)((Button)sender).Parent).Parent)).Parent)).Children[1])).Children[1];

            CentralPage(); //этот метод должен вызваться при правильных данных
        }
    }
}

