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
        playerInputAction = new PlayerInputAction();//? ?ây là new (khai báo ch? không ph?i là getcomponent)
        playerInputAction.Player.Enable();
        /*
         * canceled ???c g?i khi input action b? h?y b? tr??c khi hoàn thành
         * started ???c g?i khi input action b?t ??u ???c kích ho?t
         * performed ???c g?i khi input action ?ã hoàn thành
         * th?c ra bên trong m?i hành ??ng ??u là 1 action c#
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
