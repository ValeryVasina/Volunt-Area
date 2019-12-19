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
        //страница регистрации
        private void RegistrationPage()
        {
            Clear();
            CentralWindow.Children.Add(new Label
            {
                Text = "    VoluntArea",
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Fill,
                FontSize = 45,
                BackgroundColor = StyleColor.color1,
                VerticalTextAlignment = TextAlignment.Center,

                HeightRequest = 150
            });

            Frame frame = new Frame
            {
                CornerRadius = 20,
                HasShadow = true,
                Margin = new Thickness(30, 40, 30, 30),
                BackgroundColor = StyleColor.color3
            };

            StackLayout stackOfRegForm = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5
            };

            frame.Content = stackOfRegForm;

            stackOfRegForm.Children.Add(new Label
            {
                TextDecorations = TextDecorations.Underline,
                FontAttributes = FontAttributes.Bold,
                FontSize = 19,
                Text = "Персональные данные"
            });

            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Логин", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Пароль", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Повторите Пароль", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(new Label
            {
                TextDecorations = TextDecorations.Underline,
                FontAttributes = FontAttributes.Bold,
                FontSize = 19,
                Text = "Контактная информация"
            });
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Телефон", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "E-mail", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Адрес", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));

            Button button = new Button
            {
                CornerRadius = 10,
                BackgroundColor = StyleColor.color2,
                Text = "Завершить"
            };

            button.Clicked += EndOfRegistrationEvent;

            stackOfRegForm.Children.Add(button);
            CentralWindow.Children.Add(frame);
        }

        //завершение реистрации 
        private void EndOfRegistrationEvent(object sender, EventArgs e)
        {
            Entry login = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[1]).Children[1];   
            LogInPage();
        }
    }
}
