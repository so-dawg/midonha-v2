using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParrent
{

  [SerializeField] private Transform counterTopPoint;

  private kitchenObject kitchenObject;

  public virtual void Interact(Player player)
  {
    Debug.Log("BaseCounter Interact");
  }

  public virtual void InteractAlternate(Player player)
  {
    Debug.Log("BaseCounter InteractAlternate");
  }

  public Transform GetKitchenObjectFollowTranform()
  {
    return counterTopPoint;
  }

  public void SetKitchenObject(kitchenObject kitchenObject)
  {
    this.kitchenObject = kitchenObject;
  }

  public kitchenObject GetKitchenObject()
  {
    return kitchenObject;
  }

  public void ClearKitchenObject()
  {
    kitchenObject = null;
  }

  public bool HasKitchenObject()
  {
    return kitchenObject != null;
  }
}
