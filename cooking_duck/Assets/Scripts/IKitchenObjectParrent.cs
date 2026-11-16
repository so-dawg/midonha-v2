using UnityEngine;

public interface IKitchenObjectParrent
{

  public Transform GetKitchenObjectFollowTranform();

  public void SetKitchenObject(KitchenObject kitchenObject);

  public KitchenObject GetKitchenObject();

  public void ClearKitchenObject();

  public bool HasKitchenObject();

}
