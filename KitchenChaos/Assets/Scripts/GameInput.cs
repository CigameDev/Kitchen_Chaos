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
        playerInputAction = new PlayerInputAction();//day la new (khai khai bao chu khong phai getcomponent)
        playerInputAction.Player.Enable();
        /*
         * canceled duoc goi khi input action bi huy truoc khi hoan thanh
         * started duoc goi khi input action bat dau duoc kich hoat
         * performed duoc goi khi input action da hoan thanh
         * thuc ra ben trong moi hanh  la 1 action c#
         */
        playerInputAction.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //OnInteractAction(this, EventArgs.Empty);
        OnInteractAction?.Invoke(this,EventArgs.Empty);//kiem tra ! null
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputAction.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }    
}
