using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress {

  public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
  public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
  public class OnStateChangedEventArgs : EventArgs {
    public State state;
  }

  public enum State {
    Idle,
    Frying,
    Fried,
    Burned,
  }

  [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
  [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

  private State state;
  private float fryingTimer;
  private FryingRecipeSO fryingRecipeSO;
  private float burningTimer;
  private BurningRecipeSO burningRecipeSO;

  private void Start() {
    state =  State.Idle;
  }

  private void Update() {


    if (HasKitchenObject()) {
      switch (state) {
        case State.Idle:
          break;
        case State.Frying:
          fryingTimer += Time.deltaTime;

          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
              progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
          });

          if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
            // Fried

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

            burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            state = State.Fried;
            burningTimer = 0f;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                state = state
            });

            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
            });
          }
          break;
        case State.Fried:
          if (burningRecipeSO == null) {
            break;
          }
          burningTimer += Time.deltaTime;

          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
              progressNormalized = burningTimer / burningRecipeSO.BurningTimerMax
          });

          if (burningTimer > burningRecipeSO.BurningTimerMax) {
            // Fried

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);


            state = State.Burned;

            OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                state = state
            });
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
                progressNormalized = 0f
            });
          }
          break;
        case State.Burned:
          break;
      }
      Debug.Log(state);
    }
  }

  public override void Interact(Player player) {
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

          fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
          state  = State.Frying;
          fryingTimer = 0f;
          OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
              state = state
          });
          OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
              progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
          });
        }
      }else {
        // player not carrying anything
      }
    } else {
      //there is a KitchenObject here
      if (player.HasKitchenObject()) {
        //Player is carrying something
      }else {
        //Player is not carrying anything
        GetKitchenObject().SetKitchenObjectParrent(player);
        state = State.Idle;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs {
            state = state
        });
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {
            progressNormalized = 0f
        });
      }
    }
  }

  private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

    return fryingRecipeSO != null;
  }

  private KitchenObjectSO GetInputForOutput(KitchenObjectSO inputKitchenOjectSO)
  {
    FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenOjectSO);

    if (fryingRecipeSO != null)
    {
      return fryingRecipeSO.output;
    }
    else
    {
      return null;
    }
  }

  private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenOjectSO)
  {
    foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
    {
      if (burningRecipeSO.input == inputKitchenOjectSO)
      {
        return burningRecipeSO;
      }
    }
    return null;
  }

  private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenOjectSO)
  {
    foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
    {
      if (fryingRecipeSO.input == inputKitchenOjectSO)
      {
        return fryingRecipeSO;
      }
    }
    return null;
  }
}
