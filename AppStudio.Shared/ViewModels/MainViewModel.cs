using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.NetworkInformation;

using Windows.UI.Xaml;

using AppStudio.Services;
using AppStudio.Data;

namespace AppStudio.ViewModels
{
    public class MainViewModel : BindableBase
    {
       private AboutViewModel _aboutModel;
       private PrabathsBlogViewModel _prabathsBlogModel;
       private JaliyasBlogViewModel _jaliyasBlogModel;
       private ChamindasBlogViewModel _chamindasBlogModel;
       private MalinsBlogViewModel _malinsBlogModel;
        private PrivacyViewModel _privacyModel;

        private ViewModelBase _selectedItem = null;

        public MainViewModel()
        {
            _selectedItem = AboutModel;
            _privacyModel = new PrivacyViewModel();

        }
 
        public AboutViewModel AboutModel
        {
            get { return _aboutModel ?? (_aboutModel = new AboutViewModel()); }
        }
 
        public PrabathsBlogViewModel PrabathsBlogModel
        {
            get { return _prabathsBlogModel ?? (_prabathsBlogModel = new PrabathsBlogViewModel()); }
        }
 
        public JaliyasBlogViewModel JaliyasBlogModel
        {
            get { return _jaliyasBlogModel ?? (_jaliyasBlogModel = new JaliyasBlogViewModel()); }
        }
 
        public ChamindasBlogViewModel ChamindasBlogModel
        {
            get { return _chamindasBlogModel ?? (_chamindasBlogModel = new ChamindasBlogViewModel()); }
        }
 
        public MalinsBlogViewModel MalinsBlogModel
        {
            get { return _malinsBlogModel ?? (_malinsBlogModel = new MalinsBlogViewModel()); }
        }

        public void SetViewType(ViewTypes viewType)
        {
            AboutModel.ViewType = viewType;
            PrabathsBlogModel.ViewType = viewType;
            JaliyasBlogModel.ViewType = viewType;
            ChamindasBlogModel.ViewType = viewType;
            MalinsBlogModel.ViewType = viewType;
        }

        public ViewModelBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                UpdateAppBar();
            }
        }

        public Visibility AppBarVisibility
        {
            get
            {
                return SelectedItem == null ? AboutVisibility : SelectedItem.AppBarVisibility;
            }
        }

        public Visibility AboutVisibility
        {

         get { return Visibility.Visible; }
        }

        public void UpdateAppBar()
        {
            OnPropertyChanged("AppBarVisibility");
            OnPropertyChanged("AboutVisibility");
        }

        /// <summary>
        /// Load ViewModel items asynchronous
        /// </summary>
        public async Task LoadDataAsync(bool forceRefresh = false)
        {
            var loadTasks = new Task[]
            { 
                AboutModel.LoadItemsAsync(forceRefresh),
                PrabathsBlogModel.LoadItemsAsync(forceRefresh),
                JaliyasBlogModel.LoadItemsAsync(forceRefresh),
                ChamindasBlogModel.LoadItemsAsync(forceRefresh),
                MalinsBlogModel.LoadItemsAsync(forceRefresh),
            };
            await Task.WhenAll(loadTasks);
        }

        //
        //  ViewModel command implementation
        //
        public ICommand RefreshCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadDataAsync(true);
                });
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateToPage("AboutThisAppPage");
                });
            }
        }

        public ICommand PrivacyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    NavigationServices.NavigateTo(_privacyModel.Url);
                });
            }
        }
    }
}
