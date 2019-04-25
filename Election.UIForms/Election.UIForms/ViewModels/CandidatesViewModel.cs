

namespace Election.UIForms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.Models;
    using Common.Services;
    using Xamarin.Forms;
    public class CandidatesViewModel : BaseViewModel
    {
        private ApiService apiService;

        private ObservableCollection<Candidate> candidates;
        private bool isRefreshing;

        public ObservableCollection<Candidate> Candidates
        {
            get => this.candidates; 
            set => this.SetValue(ref this.candidates, value); 
        }
        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }


        public CandidatesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadCandidates();
        }

        private async void LoadCandidates()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Candidate>(
                "https://caristizprojects.azurewebsites.net",
                "/api",
                "/Candidates");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert
                    (
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myCandidates = (List<Candidate>)response.Result;
            this.Candidates = new ObservableCollection<Candidate>(myCandidates);

        }
    }
}
