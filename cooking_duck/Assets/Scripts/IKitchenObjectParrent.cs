using UnityEngine;

public interface IKitchenObjectParrent
{

  public Transform GetKitchenObjectFollowTranform();

  public void SetKitchenObject(kitchenObject kitchenObject);

  public kitchenObject GetKitchenObject();

  public void ClearKitchenObject();

  public bool HasKitchenObject();

}
