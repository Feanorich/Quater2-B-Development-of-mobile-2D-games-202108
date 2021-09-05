using Profile;
using Tools;
using UnityEngine.Advertisements;

public class ProfilePlayer : IProfilePlayer
{
    public ProfilePlayer(float speedCar, UnityAdsTools adsShower)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        AnalyticTools = new UnityAnalyticTools();
        AdsShower = adsShower;
        AdsListener = adsShower;
    }

    public IReadOnlySubscriptionProperty<GameState> CurrentState { get; }

    public ICar CurrentCar { get; }

    public IAnalyticTools AnalyticTools { get; }

    public IAdsShower AdsShower { get; }

    public IUnityAdsListener AdsListener { get; }
}

