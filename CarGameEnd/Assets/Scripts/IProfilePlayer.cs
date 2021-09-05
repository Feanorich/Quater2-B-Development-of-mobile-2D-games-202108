using Profile;
using Tools;
using UnityEngine.Advertisements;

public interface IProfilePlayer
{
    IUnityAdsListener AdsListener { get; }
    IAdsShower AdsShower { get; }
    IAnalyticTools AnalyticTools { get; }
    ICar CurrentCar { get; }
    IReadOnlySubscriptionProperty<GameState> CurrentState { get; }
}