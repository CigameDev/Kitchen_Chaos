using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed =7;

    private bool isWalking;
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }
        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        this.transform.position += moveDir*moveSpeed*Time.deltaTime;
        //this.transform.forward = moveDir;
        //su dung noi suy de cho xoay nhan vat khong qua dot ngot
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward,moveDir, Time.deltaTime *rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }    
}
