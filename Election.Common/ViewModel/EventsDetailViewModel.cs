using System;
using System.Collections.Generic;
using System.Text;

namespace Election.Common.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class EventsDetailViewModel : MvxViewModel<NavigationArgs>
    {
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private Event eventE;
        private bool isLoading;
        private MvxCommand updateCommand;
        private MvxCommand deleteCommand;

        public EventsDetailViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public Event Event
        {
            get => this.eventE;
            set => this.SetProperty(ref this.eventE, value);
        }

        public ICommand UpdateCommand
        {
            get
            {
                this.updateCommand = this.updateCommand ?? new MvxCommand(this.Update);
                return this.updateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                this.deleteCommand = this.deleteCommand ?? new MvxCommand(this.Delete);
                return this.deleteCommand;
            }
        }

        private void Delete()
        {
            this.dialogService.Confirm(
                "Confirm",
                "This action can't be undone, are you sure to delete the event?",
                "Yes",
                "No",
                () => { this.ConfirmDelete(); },
                null);
        }

        private async Task ConfirmDelete()
        {
            this.IsLoading = true;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.DeleteAsync(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Events",
                eventE.Id,
                "bearer",
                token.Token);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            await this.navigationService.Close(this);
        }

        private async void Update()
        {
            if (string.IsNullOrEmpty(this.Event.Name))
            {
                this.dialogService.Alert("Error", "You must enter a event name.", "Accept");
                return;
            }


            this.IsLoading = true;

            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            var response = await this.apiService.PutAsync(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Event",
                eventE.Id,
                eventE,
                "bearer",
                token.Token);

            this.IsLoading = false;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            await this.navigationService.Close(this);
        }

        public override void Prepare(NavigationArgs parameter)
        {
            this.eventE = parameter.Event;
        }
    }

}
