using System;

public interface IInventoryController : IDisposable
{
    void ShowInventory();
    void HideInventory();
}
