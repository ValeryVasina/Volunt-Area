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
        private void OrganizerPage()
        {
            ClearWorkPlace();

            WorkPlace.Children.Add(CreateTitePage("Организация событий", 50));

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(CreateButtonPanelForOrg());

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(new Label());
        }

        private StackLayout CreateButtonPanelForOrg()
        {
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0, 10, 0, 10),
                Orientation = StackOrientation.Horizontal
            };

            Button button = CreateButtonForStack("Текущие");
            button.Clicked += ViewCurrentOrgEvent;
            stack.Children.Add(button);
            button = CreateButtonForStack("Организовать новое");
            button.Clicked += CreateNewEvent;    
            stack.Children.Add(button);

            return stack;
        }

        private void ViewCurrentOrgEvent(object sender, EventArgs e)
        {
            RemoveLastWorkPlaceChild();
        }

        private void CreateNewEvent(object sender, EventArgs e)
        {
            RemoveLastWorkPlaceChild();
        }

        private bool TryViewCurrentOrgEvent()
        {
            //нужно написать проверку есть ли органзуемые мероприятия
            return true;
        }

        private bool TryCreateNewEvent()
        {
            //Надо проверить хватает рейтинга чтобы организовывать мероприятия
            return true;
        }
    }
}
