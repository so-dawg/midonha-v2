using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set;}
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;
    private InputSystem_Actions inputSystem_Actions;

    private void Awake()
    {
        Instance = this;
        
        inputSystem_Actions = new InputSystem_Actions();
        inputSystem_Actions.Player.Enable();

        inputSystem_Actions.Player.Interact.performed += Interact_performed;
        inputSystem_Actions.Player.Pause.performed += Pause_preformed;

    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_preformed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

  public Vector2 GetMovementVectorNormalized() {

    Vector2 inputVector = inputSystem_Actions.Player.Move.ReadValue<Vector2>();

    inputVector = inputVector.normalized;

    return inputVector;
  }
}
