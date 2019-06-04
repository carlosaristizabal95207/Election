namespace Election.UICrossAndroid.Views
{
    using Election.Common.ViewModel;
    using global::Android.App;
    using global::Android.OS;
    using MvvmCross.Platforms.Android.Views;

    [Activity(Label = "@string/app_name")]
    public class CandidateView : MvxActivity<CandidatesViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.CandidatesPage);
        }
    }

}