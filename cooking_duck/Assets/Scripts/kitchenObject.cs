using UnityEngine;

public class kitchenObject : MonoBehaviour
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

  public static kitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParrent kitchenObjectParrent)
  {
    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
    kitchenObject kitchenObject = kitchenObjectTransform.GetComponent<kitchenObject>();
    kitchenObject.SetKitchenObjectParrent(kitchenObjectParrent);

    return kitchenObject;
  }
}
