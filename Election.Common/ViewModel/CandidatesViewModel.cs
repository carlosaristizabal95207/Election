namespace Election.Common.ViewModel
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class CandidatesViewModel : MvxViewModel<NavigationArgs>
    {
        private List<Candidate> candidates;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private Candidate candidate;
        private Event Event;
        private bool isLoading;

        public CandidatesViewModel(
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


        public List<Candidate> Candidates
        {
            get => this.Event.Candidates;
            set => this.SetProperty(ref this.candidates, value);
        }

        public Event eventE
        {
            get => this.eventE;
            set => this.SetProperty(ref this.Event, value);
        }

        public override void Prepare(NavigationArgs parameter)
        {
            this.Event = parameter.Event;
            this.candidate = parameter.Candidate;
        }

    }
}
