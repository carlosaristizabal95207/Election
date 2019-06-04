﻿namespace Election.Common.ViewModel
{
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    public class EventsViewModel : MvxViewModel
    {
        private List<Event> events;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private MvxCommand<Event> itemClickCommand;

        public EventsViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
        }

        public ICommand ItemClickCommand
        {
            get
            {
                this.itemClickCommand = new MvxCommand<Event>(this.OnItemClickCommand);
                return itemClickCommand;
            }
        }

        public List<Event> Events
        {
            get => this.events;
            set => this.SetProperty(ref this.events, value);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
            this.LoadEvents();
        }

        private async void OnItemClickCommand(Event eventE)
        {
            await this.navigationService.Navigate<EventsDetailViewModel, NavigationArgs>(
                new NavigationArgs { Event = eventE });
        }


        private async void LoadEvents()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Event>(
                "https://shopzulu.azurewebsites.net",
                "/api",
                "/Events",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Events = (List<Event>)response.Result;
            this.Events = this.Events.OrderBy(p => p.Name).ToList();
        }
    }
}