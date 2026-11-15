using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour, IKitchenObjectParrent
{

  public static Player Instance { get; private set; }

  public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
  public class OnSelectedCounterChangedEventArgs : EventArgs
  {
    public BaseCounter selectedCounter;
  }
  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private float rotateSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;
  [SerializeField] private Transform kitchenObjectHoldPoint;

  private bool isWalking;
  private Vector3 lastInteractionDir;
  private BaseCounter selectedCounter;
  private kitchenObject kitchenObject;

  private void Awake()
  {
    if (Instance != null)
    {
      Debug.LogError("This is more than one Player instance");
    }
    Instance = this;
  }
  private void Start()
  {
    gameInput.OnInteractAction += GameInput_OninteractAction;
  }

  private void GameInput_OninteractAction(object sender, System.EventArgs e)
  {
    if (selectedCounter != null)
    {
      selectedCounter.Interact(this);

    }

  }

  private void Update()
  {
    HandleMovement();
    HandleInteraction();
  }


  public bool IsWalking()
  {
    return isWalking;
  }


  private void HandleInteraction()
  {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDir != Vector3.zero)
    {
      lastInteractionDir = moveDir;
    }

    float interactDistance = 2f;

    if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
    {
      if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
      {
        //has counter
        if (baseCounter != selectedCounter)
        {
          SetSelectedCounter(baseCounter);
        }
      }
      else
      {
        SetSelectedCounter(null);

      }
    }
    else
    {
      SetSelectedCounter(null);
    }
  }

  private void HandleMovement()
  {

    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    float playerRadius = .7f;
    float playerHeight = 2f;
    float moveDistance = moveSpeed * Time.deltaTime;

    bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    if (!canMove)
    {
      // Attemp only x movement
      Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
      canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

      if (canMove)
      {
        //can mode only on the x
        moveDir = moveDirX;
      }
      else
      {
        // Cannot move only x 

        // Attempt move only z 
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

        if (canMove)
        {
          // can move only on the z 
          moveDir = moveDirZ;
        }
        else
        {
          // Cannot move in any direction 
        }

      }

    }
    if (canMove)
    {
      transform.position += moveDir * moveDistance;
    }

    isWalking = moveDir != Vector3.zero;

    if (moveDir != Vector3.zero)
    {
      transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

  }
  private void SetSelectedCounter(BaseCounter selectedCounter)
  {
    this.selectedCounter = selectedCounter;
    OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
    {
      selectedCounter = selectedCounter
    });
  }

  public Transform GetKitchenObjectFollowTranform()
  {
    return kitchenObjectHoldPoint;
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
