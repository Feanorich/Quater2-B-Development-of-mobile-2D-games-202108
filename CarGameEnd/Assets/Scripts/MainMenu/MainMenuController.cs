﻿using Profile;
using UnityEngine;
using UnityEngine.Advertisements;

public class MainMenuController : BaseController, IMainMenuController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly IProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, IProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame);
    }
    
    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;
        
        _profilePlayer.AnalyticTools.SendMessage("start_game", ("time", Time.realtimeSinceStartup));
        
        _profilePlayer.AdsShower.ShowInterstitialVideo();
        Advertisement.AddListener(_profilePlayer.AdsListener);
    }
}
