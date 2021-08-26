using System;
public interface IAdsShower
{
    void ShowInterstitial();
    void ShowVideo(Action successShow);
}

