namespace Election.Common.ViewModel
{
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;
    using System;
    using System.Windows.Input;

    public class AddEventViewModel : MvxViewModel
    {
        private string name;
        private string startEvent;
        private string endEvent;
        private MvxCommand addEventCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private bool isLoading;

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public string StartEvent
        {
            get => this.startEvent;
            set => this.SetProperty(ref this.startEvent, value);
        }

        public string EndEvent
        {
            get => this.endEvent;
            set => this.SetProperty(ref this.endEvent, value);
        }

        public ICommand AddEventCommand
        {
            get
            {
                this.addEventCommand = this.addEventCommand ?? new MvxCommand(this.AddEvent);
                return this.addEventCommand;
            }
        }

        public AddEventViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        private async void AddEvent()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                this.dialogService.Alert("Error", "You must enter a event name.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.StartEvent))
            {
                this.dialogService.Alert("Error", "You must enter a event start.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.EndEvent))
            {
                this.dialogService.Alert("Error", "You must enter a event end.", "Accept");
                return;
            }

            var startEvent = DateTime.Parse(this.StartEvent);
            var endEvent = DateTime.Parse(this.EndEvent);
            if (startEvent <= DateTime.Now.Date)
            {
                this.dialogService.Alert("Error", "The start event must be a date greather than date of present", "Accept");
                return;
            }
            if (endEvent <= DateTime.Now.Date)
            {
                this.dialogService.Alert("Error", "The end event must be a date greather than date of present.", "Accept");
                return;
            }
            if (endEvent <= startEvent)
            {
                this.dialogService.Alert("Error", "The end event must be a date greather than start event.", "Accept");
                return;
            }

            this.IsLoading = true;

            //TODO: Image pending
            var eventE = new Event
            {
                Name = this.Name,
                StartEvent = startEvent,
                EndEvent = endEvent,
                User = new User { UserName = Settings.UserEmail },
            };

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.PostAsync(
                "https://caristizprojects.azurewebsites.net",
                "/api",
                "/Events",
                eventE,
                "bearer",
                token.Token);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            await this.navigationService.Navigate<EventsViewModel>();
        }
    }

}
