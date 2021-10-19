using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using Microsoft.Azure.CognitiveServices.Personalizer.Models;

using PersonalizerMobileDemo.Models;
using PersonalizerMobileDemo.Services;

namespace PersonalizerMobileDemo.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private User user;

        public User User
        {
            get => user; 
            set => SetProperty(ref user, value); 
        }

        private string recommendation;

        public string Recommendation
        {
            get => recommendation; 
            set => SetProperty(ref recommendation, value); 
        }

        private bool userLike;

        public bool UserLike
        {
            get => userLike; 
            set => SetProperty(ref userLike, value); 
        }

        public ObservableCollection<string> Times { get; }
        public ObservableCollection<string> Tastes { get; }

        private RankResponse food;

        public ICommand GetRecommendationCommand { get; }
        public ICommand GiveFeedbackCommand { get; }

        private async Task GetRecommendation()
        {
            IsBusy = true;

            food = PersonalizerService.GetRecommendation(user);
            Recommendation = food.RewardActionId;
            await App.Current.MainPage.DisplayAlert("Result", food.RewardActionId, "OK");

            IsBusy = false;
        }

        private async Task GiveFeedback()
        {
            var reward = userLike ? 1.0f : 0.0f;
            PersonalizerService.SendFeedback(food, reward);
            await App.Current.MainPage.DisplayAlert("Feedback", "Thanks for your feedback!", "OK");
        }

        public UserViewModel()
        {
            User = new User();
            food = new RankResponse();
            Recommendation = string.Empty;

            Times = new ObservableCollection<string>(OptionsService.GetTimes());
            Tastes = new ObservableCollection<string>(OptionsService.GetTastes());

            GiveFeedbackCommand = new Command(async () => await GiveFeedback());
            GetRecommendationCommand = new Command(async () => await GetRecommendation());
        }
    }
}