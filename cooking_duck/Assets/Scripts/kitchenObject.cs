using UnityEngine;

public class kitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObjectSO;
    }
}