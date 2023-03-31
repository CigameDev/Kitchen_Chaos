using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed =7;
    [SerializeField]private GameInput gameInput;

    private bool isWalking;

   
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
       
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.4f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*playerHeight,playerRadius, moveDir, moveDistance);
        //nhi?u khi b? dính t??ng không di chuy?n ???c,c?n check thêm
        if(!canMove)
        {
            //cannot move towards modir

            //attemp only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if(canMove)
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
                if(canMove)
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
        if(canMove)
        {
            this.transform.position += moveDir * moveDistance;
        }    
        
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir, Time.deltaTime *rotateSpeed);
    }
    public bool IsWalking()
    {
        return isWalking;
    }    
}
