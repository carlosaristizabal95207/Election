namespace Election.UIForms.ViewModels
{
   public class MainViewModel
    {

        private static MainViewModel instance;

        public LoginViewModel Login { get; set; }

        public CandidatesViewModel Candidates { get; set; }

        public EventsViewModel Events { get; set; }

        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInsance()
        {
            if(instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
