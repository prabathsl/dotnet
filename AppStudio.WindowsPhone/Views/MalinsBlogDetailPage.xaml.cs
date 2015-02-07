using AppStudio.Services;
using AppStudio.ViewModels;

using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppStudio.Views
{
    public sealed partial class MalinsBlogDetail : Page
    {
        private NavigationHelper _navigationHelper;

        private DataTransferManager _dataTransferManager;

        public MalinsBlogDetail()
        {
            this.InitializeComponent();
            _navigationHelper = new NavigationHelper(this);

            MalinsBlogModel = new MalinsBlogViewModel();
        }

        public MalinsBlogViewModel MalinsBlogModel { get; private set; }

        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            _navigationHelper.OnNavigatedTo(e);

            if (MalinsBlogModel != null)
            {
                await MalinsBlogModel.LoadItemsAsync();
                MalinsBlogModel.SelectItem(e.Parameter);

                MalinsBlogModel.ViewType = ViewTypes.Detail;
            }
            DataContext = this;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _navigationHelper.OnNavigatedFrom(e);
            _dataTransferManager.DataRequested -= OnDataRequested;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (MalinsBlogModel != null)
            {
                MalinsBlogModel.GetShareContent(args.Request);
            }
        }
    }
}
