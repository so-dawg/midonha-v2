using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

  public event EventHandler OnRecipeSpawned;
  public event EventHandler OnRecipeCompleted;
  public static DeliveryManager Instance { get; private set; }
  public event EventHandler OnRecipeSuccess;
  public event EventHandler OnRecipeFailed;

  [SerializeField] private RecipeListSO recipeListSO;
  private List<RecipeSO> waitingRecipeSOList;
  private float spawnRecipeTimer;
  private float spawnRecipeTimerMax = 4f;
  private int waitingRecipeMax = 4;

  private void Awake()
  {
    Instance = this;
    waitingRecipeSOList = new List<RecipeSO>();
  }

  private void Update()
  {
    spawnRecipeTimer -= Time.deltaTime;
    if (spawnRecipeTimer <= 0f)
    {
      spawnRecipeTimer = spawnRecipeTimerMax;

      if (waitingRecipeSOList.Count < waitingRecipeMax)
      {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
        waitingRecipeSOList.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
      }

    }
  }

  public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
  {
    for (int i = 0; i < waitingRecipeSOList.Count; i++)
    {
      RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

      if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
      {
        bool plateContentMatchesRecipe = true;
        // has the same number of ingredients
        foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
        {
          //cycling through all ingredient in the recipe
          bool ingredientFound = false;
          foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
          {
            //cycling through all ingredient in the plate
            if (plateKitchenObjectSO == recipeKitchenObjectSO)
            {
              ingredientFound = true;
              break;
            }

          }
          if (!ingredientFound)
          {
            // the recipe not found on the plate
            plateContentMatchesRecipe = false;
          }
        }
        if (plateContentMatchesRecipe)
        {
          // player deliver correct recipe
          Debug.Log("Player delivered the correct recipe");
          waitingRecipeSOList.RemoveAt(i);

          OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
          OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
          return;
        }
      }
    }
    // no matches found 
    // player did not correct the recipe
    OnRecipeFailed?.Invoke(this, EventArgs.Empty);
  }

  public List<RecipeSO> GetWaitingRecipeSOList()
  {
    return waitingRecipeSOList;
  }
}
