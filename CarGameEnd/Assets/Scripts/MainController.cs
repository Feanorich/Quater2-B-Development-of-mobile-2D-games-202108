using Profile;
using System.Collections.Generic;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, IProfilePlayer profilePlayer, 
        List<ItemConfig> itemsConfig)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private IMainMenuController _mainMenuController;
    private IGameController _gameController;
    private IInventoryController _inventoryController;
    private readonly Transform _placeForUi;
    private readonly IProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _inventoryController = new InventoryController(_itemsConfig);
                _inventoryController.ShowInventory();

                _gameController = new GameController(_profilePlayer);
                _mainMenuController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
