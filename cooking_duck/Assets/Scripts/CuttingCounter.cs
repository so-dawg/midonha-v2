using UnityEngine;

public class CuttingCounter : BaseCounter
{
  [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

  public override void Interact(Player player)
  {
    if (!HasKitchenObject())
    {
      // There is no KitchenObject here
      if (player.HasKitchenObject())
      {
        // Player is carrying something
        if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
          // Player carrying something that can be Cut
          player.GetKitchenObject().SetKitchenObjectParrent(this);
        }
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
    if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
    {
      // There is a KitchenObject here AND it can be cut
      KitchenObjectSO outputKitchenObjectSO = GetInputForOutput(GetKitchenObject().GetKitchenObjectSO());
      GetKitchenObject().DestroySelf();

      kitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenObjectSO)
      {
        return true;
      }
    }
    return false;
  }

  private KitchenObjectSO GetInputForOutput(KitchenObjectSO inputKitchenOjectSO)
  {
    foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
    {
      if (cuttingRecipeSO.input == inputKitchenOjectSO)
      {
        return cuttingRecipeSO.output;
      }
    }

    return null;
  }
}
