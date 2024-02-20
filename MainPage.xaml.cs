using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            BindingContext = new MainPageViewModel();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ((MainPageViewModel)BindingContext).SessionProgression.AddElapsedSeconds(1);
        }
    }

    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private TimeProgression _sessionProgression = new TimeProgression();

        public MainPageViewModel()
        {
            SessionProgression.Reset(TimeSpan.FromSeconds(10));
            SessionProgression.ElapsedTime = TimeSpan.FromSeconds(5);
        }
    }
}
