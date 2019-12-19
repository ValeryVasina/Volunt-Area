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
        private User CurrentUser;
        VolunteerManager manager = new VolunteerManager();

        public MainPage()
        {
            InitializeComponent();

            LogInPage();
        }

        //Очистка главного окна от содержимого
        public void Clear()
        {
            CentralWindow.Children.Clear();
        }

        public static Label CreateRedLine()
        {
            return new Label
            {
                HeightRequest = 5,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Red
            };
        }

        private Button CreateButtonForStack(string text)
        {
            Button button = new Button
            {
                Text = text,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = StyleColor.color5
            };
            return button;
        }

        private void RemoveLastWorkPlaceChild()
        {
            WorkPlace.Children.Remove(WorkPlace.Children[WorkPlace.Children.Count - 1]);
            WorkPlace.Children.Remove(WorkPlace.Children[WorkPlace.Children.Count - 1]);
            WorkPlace.Children.Remove(WorkPlace.Children[WorkPlace.Children.Count - 1]);
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

        private Label CreateTiteForPage(string title, int size)
        {
            return new Label
            {
                Text = title,
                FontSize = size,
                HorizontalOptions = LayoutOptions.Fill,
                HorizontalTextAlignment = TextAlignment.Center
            };
        }

        private Frame FormForEvent(Event ev)
        {
            Frame frame = new Frame
            {
                CornerRadius = 20,
                HasShadow = true,
                Margin = new Thickness(30, 40, 30, 30),
                BackgroundColor = StyleColor.color3
            };

            StackLayout stackOfCreForm = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 5,
                Children =
                {
                    new Label
                    {
                        Text = ev.EventId.ToString()
                    },
                    new Label
                    {
                        Text = ev.EventName
                    },
                    new Label
                    {
                        Text = ev.EventDt.ToString()
                    },
                    new Label
                    {
                        Text = ev.Planner.Name
                    },
                    new Label
                    {
                        Text = ev.Town
                    },
                    new Label
                    {
                        Text = ev.Address
                    },
                    new Label
                    {
                        Text = ev.Type.ToString()
                    },
                    new Label
                    {
                        Text = ev.DurationHours.ToString()
                    },
                    new Label
                    {
                        Text = ev.RequiredPeopleNumber.ToString()
                    },
                    new Label
                    {
                        Text = ev.Description
                    },
                }
            };

            frame.Content = stackOfCreForm;



            return frame;
        }
    }
}
