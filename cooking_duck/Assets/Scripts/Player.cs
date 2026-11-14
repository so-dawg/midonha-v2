using UnityEngine;

public class Player : MonoBehaviour
{

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private float rotateSpeed = 7f;
  [SerializeField] private GameInput gameInput;
  [SerializeField] private LayerMask countersLayerMask;

  private bool isWalking;
  private Vector3 lastInteractionDir; 

  private void Update()
  {
    HandleMovement();
    HandleInteraction();
  }

   public bool IsWalking()
  {
    return isWalking;
  }

  private void HandleInteraction() {
    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    if (moveDir != Vector3.zero) {
      lastInteractionDir = moveDir;
    }

    float interactDistance = 2f;

    if (Physics.Raycast(transform.position, moveDir,out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
      if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
        //has counter
        clearCounter.Interact();
      }

    }else {
      Debug.Log("-");
    }
  }

  private void HandleMovement() {

    Vector2 inputVector = gameInput.GetMovementVectorNormalized();

    Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

    float playerRadius = .7f;
    float playerHeight = 2f;
    float moveDistance = moveSpeed  * Time.deltaTime;

    bool canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

    if (!canMove) {
      // Attemp only x movement
      Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
      canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

      if (canMove) {
        //can mode only on the x
        moveDir = moveDirX;
      }else {
        // Cannot move only x 

        // Attempt move only z 
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        canMove = !Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

        if (canMove) {
          // can move only on the z 
          moveDir = moveDirZ;
        }else {
          // Cannot move in any direction 
        }

      }

    }
    if (canMove) {
      transform.position += moveDir * moveDistance;
    }

    isWalking = moveDir != Vector3.zero;

    if (moveDir != Vector3.zero)
    {
      transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

  }
}
