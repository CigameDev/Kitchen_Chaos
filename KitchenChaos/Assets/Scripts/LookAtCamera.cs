using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// script nay gan cho ProgressBar de dieu chinh huong
/// mac dinh mode la Mode.CameraFoward
/// Chua dung cac  mode khac
/// </summary>
public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraFoward,
        CameraFowardInverted,

    }
    [SerializeField] private Mode mode;
   
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        switch(mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraFoward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraFowardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }    
    }
}
