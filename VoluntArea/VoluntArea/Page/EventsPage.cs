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
        private void EventPage()
        {
            ClearWorkPlace();

            WorkPlace.Children.Add(CreateTiteForPage("Мероприятия", 50));

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(CreateButtonStackForEvent());
            
            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(CreateTiteForPage("Все мероприятия", 35));

            WorkPlace.Children.Add(CreateRedLine());

            WorkPlace.Children.Add(CreateStackWithEvent(EventType.Детские_дома, 1));

        }

        private StackLayout CreateButtonStackForEvent()
        {
            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(5)
            };

            Button button = CreateButtonForStack("Все мероприятия");

            stack.Children.Add(button);

            StackLayout st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            stack.Children.Add(st);

            button = CreateButtonForStack("Субботник");
            button.Clicked += ShowEventType;
            st.Children.Add(button);

            button = CreateButtonForStack("Праздник");
            button.Clicked += ShowEventType;
            st.Children.Add(button);

            st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            stack.Children.Add(st);

            button = CreateButtonForStack("Старики");
            button.Clicked += ShowEventType;
            st.Children.Add(button);

            button = CreateButtonForStack("Детские дома");
            button.Clicked += ShowEventType;
            st.Children.Add(button);


            st = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };

            stack.Children.Add(st);

            button = CreateButtonForStack("Животные");
            button.Clicked += ShowEventType;
            st.Children.Add(button);

            button = CreateButtonForStack("Форумы");
            button.Clicked += ShowEventType;
            st.Children.Add(button);
            return stack;
        }

        private void ShowEventType(object sender, EventArgs e)
        {
            RemoveLastWorkPlaceChild();
            string text = ((Button)sender).Text;
            WorkPlace.Children.Add(CreateTiteForPage(text, 35));
            WorkPlace.Children.Add(CreateRedLine());
            if (text == "Все мероприятия")
                WorkPlace.Children.Add(CreateStackWithEvent(EventType.Форумы_встречи_конференции, 1));
            else
            {
                WorkPlace.Children.Add(CreateStackWithEvent(FindTypeOfEvent(text), 0));
            }
        }

        private EventType? FindTypeOfEvent(string text)
        {
            if (text == "Форумы")
                return EventType.Форумы_встречи_конференции;
            else if (text == "Субботник")
                return EventType.Субботник;
            else if (text == "Праздник")
                return EventType.Праздник;
            else if (text == "Старики")
                return EventType.Помощь_престарелым;
            else if (text == "Детские дома")
                return EventType.Детские_дома;
            else if (text == "Животные")
                return EventType.Приюты_для_животных;
            return null;
        }

        private StackLayout CreateStackWithEvent(EventType? eventType, int type)
        {
            StackLayout stack = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            if (type == 1)
            {
                foreach (Event ev in manager.activeEvents)
                    stack.Children.Add(FormForEvent(ev));
            }
            else if (eventType == null)
            {
                stack.Children.Add(CreateTiteForPage("Для данной категории нет событий", 20));
            }
            else
            {
                foreach (Event ev in manager.activeEvents)
                    if (ev.Type == eventType)
                        stack.Children.Add(FormForEvent(ev));
                if (stack.Children.Count == 0)
                    stack.Children.Add(CreateTiteForPage("Для данной категории нет событий", 20));
            }
            return stack;
        }
    }
}
