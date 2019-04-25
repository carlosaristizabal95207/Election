using Election.Common.Models;
using System.Collections.ObjectModel;

namespace Election.UIForms.ViewModels
{
    public class EventsViewModel
    {
        public ObservableCollection<Event> Events { get; set; }
    }
}
