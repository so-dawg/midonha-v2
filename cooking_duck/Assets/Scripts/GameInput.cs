using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class GameInput : MonoBehaviour
{
  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;

  private InputSystem_Actions inputSystem_Actions;

  private void Awake()
  {
    inputSystem_Actions = new InputSystem_Actions();
    inputSystem_Actions.Player.Enable();

    inputSystem_Actions.Player.Interact.performed += Interact_performed;
    inputSystem_Actions.Player.InteractAlternate.performed += InteractAlternate_performaned;
  }
  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {

    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  private void InteractAlternate_performaned(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized()
  {

    Vector2 inputVector = inputSystem_Actions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;

    return inputVector;
  }
}
