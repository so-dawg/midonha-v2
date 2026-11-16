using UnityEngine;

public class KitchenObject : MonoBehaviour
{
  [SerializeField] private KitchenObjectSO kitchenObjectSO;

  private IKitchenObjectParrent kitchenObjectParrent;

  public KitchenObjectSO GetKitchenObjectSO()
  {
    return kitchenObjectSO;
  }

  public void SetKitchenObjectParrent(IKitchenObjectParrent kitchenObjectParrent)
  {
    if (this.kitchenObjectParrent != null)
    {
      this.kitchenObjectParrent.ClearKitchenObject();
    }

    if (kitchenObjectParrent.HasKitchenObject())
    {
      Debug.LogError("Counter already has kitchenObject");
    }

    this.kitchenObjectParrent = kitchenObjectParrent;
    kitchenObjectParrent.SetKitchenObject(this);

    transform.parent = kitchenObjectParrent.GetKitchenObjectFollowTranform();
    transform.localPosition = Vector3.zero;
  }

  public IKitchenObjectParrent GetClearCounter()
  {
    return kitchenObjectParrent;
  }

  public void DestroySelf()
  {
    kitchenObjectParrent.ClearKitchenObject();

    Destroy(gameObject);
  }

  public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
  {
    if (this is PlateKitchenObject)
    {
      plateKitchenObject = this as PlateKitchenObject;
      return true;
    }
    else
    {
      plateKitchenObject = null;
      return false;
    }
  }

  public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParrent kitchenObjectParrent)
  {
    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
    KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
    kitchenObject.SetKitchenObjectParrent(kitchenObjectParrent);

    return kitchenObject;
  }
}
