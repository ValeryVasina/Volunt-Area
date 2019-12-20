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

            WorkPlace.Children.Add(CreateTiteForPage("Организация событий", 50));
        
            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(CreateButtonPanelForOrg());

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(new Label());
            WorkPlace.Children.Add(new Label());
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
            if (WorkPlace.Children.Count > 5)
                RemoveLastWorkPlaceChild();

            WorkPlace.Children.Add(CreateTiteForPage("Текущие организуемые вами мероприятия", 35));
            WorkPlace.Children.Add(CreateRedLine());

            if (TryViewCurrentOrgEvent() == true)
            {
                WorkPlace.Children.Add(CreateStackForOrgEvent());
            }
            else
            {
                WorkPlace.Children.Add(CreateTiteForPage("В данный момент вы не являетесь организатором ни одно мероприятия", 20));
            }
        }

        private void CreateNewEvent(object sender, EventArgs e)
        {
            if (WorkPlace.Children.Count > 5)
                RemoveLastWorkPlaceChild();

            WorkPlace.Children.Add(CreateTiteForPage("Организовать новое мероприятие", 35));
            WorkPlace.Children.Add(CreateRedLine());

            if (TryCreateNewEvent() == true)
            {
                WorkPlace.Children.Add(FormForCreateNewEvent());
            }
            else
            {
                WorkPlace.Children.Add(CreateTiteForPage("К сожалению, вы недостаточно опытны для самостоятельной ог=рганизации мероприятия", 20));
            }
        }

        private bool TryViewCurrentOrgEvent()
        {
            //нужно написать проверку есть ли органзуемые мероприятия

            // возвращает тру, если ивенты для определенного юзера есть
            return manager.GetActiveEventsForUserAsPlanner(CurrentUser).Count != 0;
            
        }

        private bool TryCreateNewEvent()
        {
            //Надо проверить хватает рейтинга чтобы организовывать мероприятия

            return CurrentUser.Rating.CheckUserRatingToPlan();
        }

        private StackLayout CreateStackForOrgEvent()
        {

            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand    
            };
            List<Event> list = manager.GetActiveEventsForUserAsPlanner(CurrentUser);

            foreach (Event ev in list)
            {
                stack.Children.Add(FormForEvent(ev));
            }

            return stack;
        }
        

        private Frame FormForCreateNewEvent()
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
                Padding = 5
            };

            frame.Content = stackOfCreForm;

            stackOfCreForm.Children.Add(new Label
            {
                TextDecorations = TextDecorations.Underline,
                FontAttributes = FontAttributes.Bold,
                FontSize = 19,
                Text = "Данные о мероприятии"
            });
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Название", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Дата", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Город", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Адресс", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Тип", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Продолжительность", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Мест", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));
            stackOfCreForm.Children.Add(CreateNewSrack(-1, "Описание", new Entry { HorizontalOptions = LayoutOptions.FillAndExpand }));

            Button button = new Button
            {
                CornerRadius = 10,
                BackgroundColor = StyleColor.color2,
                Text = "Завершить"
            };

            button.Clicked += EndOfCreateNewEvent;
            stackOfCreForm.Children.Add(button);
            return frame;
        }

        private void EndOfCreateNewEvent(object sender, EventArgs e)
        {
            Entry name = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[1]).Children[1];
            Entry Date = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[2]).Children[1];
            Entry town = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[3]).Children[1];
            Entry address = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[4]).Children[1];
            Entry type = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[5]).Children[1]; //пока вводим руками потом сделаю выпадающий список
            Entry duration = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[6]).Children[1];
            Entry countplace = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[7]).Children[1];
            Entry description = (Entry)((StackLayout)((StackLayout)(((Button)sender).Parent)).Children[8]).Children[1];

            if(manager.CheckDates(Date.Text) != null
                && manager.CheckDurationForEvent(duration.Text) && manager.CheckPeopleNumber(countplace.Text))
            {
                DateTime eventDate = manager.CheckDates(Date.Text) ?? DateTime.Now;
                int correctDuration = Int32.Parse(duration.Text);
                int correctPeopleNumber = Int32.Parse(countplace.Text);
                if(manager.CheckEventInfoAndAdd(CurrentUser, name.Text,town.Text, address.Text, eventDate, correctDuration,
                    correctPeopleNumber, description.Text))
                {
                    // не прописан type 
                    RemoveLastWorkPlaceChild(); //метод вызываемый после соханений данных
                }
                
                //сообщение об ошибке
            }
            // сообщение об ошибке
        }
    }
}
