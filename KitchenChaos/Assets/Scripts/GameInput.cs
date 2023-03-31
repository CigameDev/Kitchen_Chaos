using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();//? ?�y l� new (khai b�o ch? kh�ng ph?i l� getcomponent)
        playerInputAction.Player.Enable();
        /*
         * canceled ???c g?i khi input action b? h?y b? tr??c khi ho�n th�nh
         * started ???c g?i khi input action b?t ??u ???c k�ch ho?t
         * performed ???c g?i khi input action ?� ho�n th�nh
         * th?c ra b�n trong m?i h�nh ??ng ??u l� 1 action c#
         */
        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //OnInteractAction(this, EventArgs.Empty);
        OnInteractAction?.Invoke(this,EventArgs.Empty);//ki?m tra ! null
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }    
}
