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
        private void CentralPage()
        {
            Clear();

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
        }

        private Grid CreateGridWithMenu()
        {
            Grid grid = new Grid
            {

            };
            return grid;
        }
        

        private void AddTopStack(StackLayout stack)
        {
            Button but = new Button
            {
                Text = "M",
                BackgroundColor = StyleColor.color2
            };

            stack.Children.Add(but);

            stack.Children.Add(new Label
            {
                Text = "Voluent Area",
                FontSize = 18
            });

            but = new Button
            {
                Text = "User",
                BackgroundColor = StyleColor.color2
            };

            stack.Children.Add(but);
        }

        private void toPersonalAccount(object sender, EventArgs e)
        {
            PersonalAccountPage();
        }
    }
}
