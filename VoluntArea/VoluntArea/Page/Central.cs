﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VoluntArea
{
    public partial class MainPage : ContentPage
    {
        //временные поля
        private ColumnDefinition MenuColumn = new ColumnDefinition {Width = 0 };
        private StackLayout WorkPlace = new StackLayout
        {
            VerticalOptions = LayoutOptions.FillAndExpand,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            BackgroundColor = Color.White
        };

        private void CentralPage()
        {
            Clear();
            CloseMenu();
            ClearWorkPlace();

            Grid centralGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            RowDefinitionCollection rd = centralGrid.RowDefinitions;
            rd.Add(new RowDefinition
            {
                Height = new GridLength(0.1, GridUnitType.Star),
            });

            rd.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star),
            });

            CentralWindow.Children.Add(centralGrid);

            StackLayout topStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = StyleColor.color1,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,

            };

            AddTopStack(topStack);

            Grid.SetRow(topStack, 0);

            centralGrid.Children.Add(topStack);

            centralGrid.Children.Add(CreateGridWithMenu());

            MainMenu();
        }

        private void MainMenu()
        {
            ClearWorkPlace();

            WorkPlace.Children.Add(new Image
            {
                Source = "main.jpg"
            });
            WorkPlace.Children.Add(new Label
            {
                BackgroundColor = StyleColor.color3,
                FontSize = 22,
                Text = "Чувствуй сердцем Думай головой. Стань волонтером, Все страхи долой!"
            });
            WorkPlace.Children.Add(CreateTiteForPage("Ближайшие мероприятия", 35));

            foreach (Event ev in manager.activeEvents)
               WorkPlace.Children.Add(FormForEvent(ev));
        }

        private Grid CreateGridWithMenu()
        {
            Grid grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
            };

            Grid.SetRow(grid, 1);

            ColumnDefinitionCollection cd = grid.ColumnDefinitions;
            cd.Add(MenuColumn);
            cd.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            grid.Children.Add(CreateMenuStack());

            ScrollView scroll = new ScrollView
            {
                Content = WorkPlace
            };

            Grid.SetColumn(scroll, 1);

            grid.Children.Add(scroll);

            return grid;
        }
        

        private void AddTopStack(StackLayout stack)
        {
            Button but = new Button
            {
                Text = "M",
                BackgroundColor = StyleColor.color1
            };

            but.Clicked += ClickMenuButton;

            stack.Children.Add(but);

            stack.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                VerticalTextAlignment = TextAlignment.Center,
                Text = "VoluntArea",
                FontSize = 30
            });

            but = new Button
            {
                CornerRadius = 10,
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(10),
                Text = CurrentUser.Name.Split(' ')[0],
                BackgroundColor = StyleColor.color2
            };
            but.Clicked += ClickPersonalAccount;
            stack.Children.Add(but);
        }

        private StackLayout CreateMenuStack()
        {
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                BackgroundColor = StyleColor.color1
            };

            Button button = new Button
            {
                BackgroundColor = StyleColor.color1,
            };
            button.Text = "Главное меню";
            button.Clicked += GoToCentral;
            stack.Children.Add(button);
            button = new Button
            {
                BackgroundColor = StyleColor.color1,
                Text = "Личный кабинет"
            };
            button.Clicked += ClickPersonalAccount;
            stack.Children.Add(button);

            button = new Button
            {
                BackgroundColor = StyleColor.color1,
                Text = "Мероприятия"
            };
            button.Clicked += GoToEventPage;
            stack.Children.Add(button);

            button = new Button
            {
                BackgroundColor = StyleColor.color1,
                Text = "Организатор"
            };
            button.Clicked += GoToOrganizerPage;
            stack.Children.Add(button);

            button = new Button
            {
                BackgroundColor = StyleColor.color1,
                Text = "Выйти"
            };
            button.Clicked += ClickExit;
            stack.Children.Add(button);
            return stack;
        }

        private void ClickPersonalAccount(object sender, EventArgs e)
        {
            PersonalAccountPage();
            CloseMenu();
        }

        private void ClickMenuButton(object sender, EventArgs e)
        {
            if (MenuColumn.Width.Value == 0)
            {
                MenuColumn.Width = 200;
            }
            else
            {
                MenuColumn.Width = 0;
            }
        }

        private void GoToCentral(object sender, EventArgs e)
        {
            MainMenu();
            CloseMenu();
        }

        private void GoToEventPage(object sender, EventArgs e)
        {
            EventPage();
            CloseMenu();
        }

        private void GoToOrganizerPage(object sender, EventArgs e)
        {
            OrganizerPage();
            CloseMenu();
        }

        private void ClearWorkPlace()
        {
            WorkPlace.Children.Clear();
        }

        private void ClickExit(object sender, EventArgs e)
        {
            LogInPage();
        }

        private void CloseMenu()
        {
            MenuColumn.Width = 0;
        }
    }
}
