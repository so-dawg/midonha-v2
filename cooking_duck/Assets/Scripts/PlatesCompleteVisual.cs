using UnityEngine;
using System;
using System.Collections.Generic;

public class PlatesCompleteVisual : MonoBehaviour
{

  [Serializable]
  public struct KitchenObjectSO_GmaeObject
  {
    public KitchenObjectSO kitchenObjectSO;
    public GameObject gameObject;
  }

  [SerializeField] private PlateKitchenObject plateKitchenObject;
  [SerializeField] private List<KitchenObjectSO_GmaeObject> kitchenObjectSOGameOjectList;

  private void Start()
  {
    plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;

    // Hide all visual children so plate starts completely clean
    foreach (Transform child in transform)
    {
      child.gameObject.SetActive(false);
    }

    if (kitchenObjectSOGameOjectList != null)
    {
      foreach (KitchenObjectSO_GmaeObject kitchenObjectSOGameObject in kitchenObjectSOGameOjectList)
      {
        if (kitchenObjectSOGameObject.gameObject != null)
        {
          kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
      }
    }
  }

  private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
  {
    bool activated = false;

    if (kitchenObjectSOGameOjectList != null)
    {
      foreach (KitchenObjectSO_GmaeObject kitchenObjectSOGameObject in kitchenObjectSOGameOjectList)
      {
        if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
        {
          if (kitchenObjectSOGameObject.gameObject != null)
          {
            kitchenObjectSOGameObject.gameObject.SetActive(true);
            activated = true;
            Debug.Log($"[PlatesCompleteVisual] Activated GameObject '{kitchenObjectSOGameObject.gameObject.name}' for {e.kitchenObjectSO.objectName}");
          }
        }
      }
    }

    if (!activated)
    {
      // Fallback: search children matching name (case-insensitive, ignoring spaces)
      string targetClean = (e.kitchenObjectSO.name ?? "").Replace(" ", "").ToLower();
      string objectClean = (e.kitchenObjectSO.objectName ?? "").Replace(" ", "").ToLower();

      foreach (Transform child in transform)
      {
        string childClean = child.name.Replace(" ", "").ToLower();
        if (childClean.Contains(targetClean) || targetClean.Contains(childClean) ||
            childClean.Contains(objectClean) || objectClean.Contains(childClean))
        {
          child.gameObject.SetActive(true);
          activated = true;
          Debug.Log($"[PlatesCompleteVisual] Activated child '{child.name}' by name fallback for {e.kitchenObjectSO.objectName}");
          break;
        }
      }
    }

    if (!activated)
    {
      Debug.LogWarning($"[PlatesCompleteVisual] Could not find any visual GameObject on Plate for ingredient '{e.kitchenObjectSO.objectName}'!");
    }
  }

}
