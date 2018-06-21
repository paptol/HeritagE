using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse")]
public class PlayerCamera : MonoBehaviour
{

    CursorLockMode cursorMode;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -80F;
    public float maximumY = 70F;
    float rotationY = 0F;
    float rotationX = 0f;

    bool isPaused;

    void Start ()
    {
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        if(InputController.m_GameState == InputController.enum_GameState.Playing)
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(0, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            { 
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

        }

    }

    public float GetXRotation()
    {
        return rotationX;
    }

    public float GetYRotation()
    {
        return rotationY;
    }
}


