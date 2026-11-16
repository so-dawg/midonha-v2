using UnityEngine;
using System;

public class ContainerCounter : BaseCounter
{

  public event EventHandler OnPlayerGrabbedObject;

  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!player.HasKitchenObject())
    {
      // Player is not carrying anything
      kitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

      OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
  }
}
