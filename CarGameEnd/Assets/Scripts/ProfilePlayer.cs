using Profile;
using Tools;
using UnityEngine.Advertisements;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar, UnityAdsTools _unityAdsTools)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        AnalyticTools = new UnityAnalyticTools();

        AdsShower = _unityAdsTools;
        AdsListener = _unityAdsTools;
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }

    public IAnalyticTools AnalyticTools { get; }

    public IAdsShower AdsShower { get; }

    public IUnityAdsListener AdsListener { get; }
}


