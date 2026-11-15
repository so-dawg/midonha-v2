using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParrent
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;
  [SerializeField] private Transform counterTopPoint;
  private kitchenObject kitchenObject;

  public override void Interact(Player player)
  {
    if (kitchenObject == null)
    {
      Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
      kitchenObjectTransform.GetComponent<kitchenObject>().SetKitchenObjectParrent(this);
    }
    else
    {
      kitchenObject.SetKitchenObjectParrent(player);
    }
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
