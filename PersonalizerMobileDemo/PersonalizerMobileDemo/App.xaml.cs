using Xamarin.Forms;

namespace PersonalizerMobileDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Views.UserView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
