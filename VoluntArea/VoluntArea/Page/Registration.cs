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
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Имя пользователя", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Дата рождения", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Пароль", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Повторите пароль", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            //stackOfRegForm.Children.Add(new Label
            //{
            //    TextDecorations = TextDecorations.Underline,
            //    FontAttributes = FontAttributes.Bold,
            //    FontSize = 19,
            //    Text = "Контактная информация"
            //});
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "Телефон (+7...)", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfRegForm.Children.Add(CreateNewSrack(-1, "E-mail", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            //stackOfRegForm.Children.Add(CreateNewSrack(-1, "Адрес", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));

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

        //завершение регистрации 
        private void EndOfRegistrationEvent(object sender, EventArgs e)
        {
            Entry login = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[1]).Children[1];
            Entry name = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[2]).Children[1];
            Entry birthDt = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[3]).Children[1];
            Entry password1 = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[4]).Children[1];
            Entry password2 = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[5]).Children[1];
            Entry phoneNumber = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[6]).Children[1];
            Entry email = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[7]).Children[1];


            if(manager.CheckPhoneNumberFormat(phoneNumber.Text)&& manager.CheckPassword(password1.Text,password2.Text)
                && manager.CheckDates(birthDt.Text) != null)
            {
                DateTime correctBirthDate = manager.CheckDates(birthDt.Text) ?? DateTime.Now;
                if(manager.CheckUserUnfo(login.Text, name.Text, correctBirthDate, password1.Text, email.Text, phoneNumber.Text))
                {
                    // нужно сообщение, что успешно зарегались
                    LogInPage();
                }
                // сообщение об ошибке, если в условии выше false

            }

            //здесь тоже сообщение об ошибке
        }
    }
}
