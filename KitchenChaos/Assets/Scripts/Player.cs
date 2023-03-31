using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }
    [SerializeField]private float moveSpeed =7;
    [SerializeField]private GameInput gameInput;
    [SerializeField] private LayerMask countersLayermask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;//quay don dep duoc chon

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if(selectedCounter !=null)
        {
            selectedCounter.Interact();
        }    
        
    }

    void Update()
    {
        HandleMovement();
        HandleOInteractions();
    }
    public bool IsWalking()
    {
        return isWalking;
    }    
    private void HandleOInteractions()//xu ly cac tuong tac
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if(moveDir !=Vector3.zero)
        {
            lastInteractDir = moveDir;
        }    
        float interactDistance = 2f;

        if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance,countersLayermask))
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //has clear counter

                //clearCounter.Interact();
                if(clearCounter != selectedCounter)
                {
                    //selectedCounter = clearCounter;
                    //OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
                    //{
                    //    selectedCounter = this.selectedCounter
                    //    //selectedCounter dau tien la cua class OnSelected
                    //    //SelectedCounter thu 2 la khai bao ban dau cua class player
                    //}) ;

                    SetSelectedCounter(clearCounter);
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
        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.4f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        //nhi?u khi b? dính t??ng không di chuy?n ???c,c?n check thêm
        if (!canMove)
        {
            //cannot move towards modir

            //attemp only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Can move only on the X
                moveDir = moveDirX;
            }
            else
            {
                //cannot move only on the X
                //attemp only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    //can move only on the Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }
        if (canMove)
        {
            this.transform.position += moveDir * moveDistance;
        }

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }    
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = this.selectedCounter
        });
    }    
}
