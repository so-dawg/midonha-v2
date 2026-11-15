using UnityEngine;

public class CuttingCounter : BaseCounter
{
  [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      // There is no KitchenObject here
      if (player.HasKitchenObject())
      {
        // Player is carrying something
        player.GetKitchenObject().SetKitchenObjectParrent(this);
      }
    }
    else
    {
      // There is a KitchenObject here
      if (!player.HasKitchenObject())
      {
        // Player is not carrying anything
        GetKitchenObject().SetKitchenObjectParrent(player);
      }
    }

  }

  public override void InteractAlternate(Player player)
  {
    if (HasKitchenObject())
    {
      // There is KitchenObject here
      GetKitchenObject().DestroySelf();

      kitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
    }
  }
}
