using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class GameInput : MonoBehaviour
{
  public static GameInput Instance { get; private set;}

  public event EventHandler OnInteractAction;
  public event EventHandler OnInteractAlternateAction;

  private InputSystem_Actions inputSystem_Actions;
  public event EventHandler OnPauseGame;

  private void Awake()
  {
    Instance = this; 

    inputSystem_Actions = new InputSystem_Actions();
    inputSystem_Actions.Player.Enable();

    inputSystem_Actions.Player.Interact.performed += Interact_performed;
    inputSystem_Actions.Player.InteractAlternate.performed += InteractAlternate_performaned;
    inputSystem_Actions.Player.Pause.performed += Pause_performed;
  }

  private void OnDestory()
  {
    inputSystem_Actions.Player.Interact.performed -= Interact_performed;
    inputSystem_Actions.Player.InteractAlternate.performed -= InteractAlternate_performaned;
    inputSystem_Actions.Player.Pause.performed -= Pause_performed;

    inputSystem_Actions.Dispose();
  }
  private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {

    OnInteractAction?.Invoke(this, EventArgs.Empty);
  }

  private void InteractAlternate_performaned(UnityEngine.InputSystem.InputAction.CallbackContext obj)
  {
    OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
  }

  private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        
    OnPauseGame?.Invoke(this, EventArgs.Empty);
  }

  public Vector2 GetMovementVectorNormalized()
  {

    Vector2 inputVector = inputSystem_Actions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;

    return inputVector;
  }


  private void OnDestroy()
  {
    if (inputSystem_Actions != null)
    {
      inputSystem_Actions.Player.Interact.performed -= Interact_performed;
      inputSystem_Actions.Player.InteractAlternate.performed -= InteractAlternate_performaned;

      inputSystem_Actions.Dispose();
    }
  }
}