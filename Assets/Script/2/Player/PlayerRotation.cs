using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput == 1 && verticalInput == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 135f);
        }
        else if (horizontalInput == 0 && verticalInput == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (horizontalInput == -1 && verticalInput == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 225f);
        }
        else if (horizontalInput == -1 && verticalInput == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 270f);
        }
        else if (horizontalInput == -1 && verticalInput == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 315f);
        }
        else if (horizontalInput == 0 && verticalInput == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (horizontalInput == 1 && verticalInput == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }
        else if (horizontalInput == 1 && verticalInput == 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
    }
}
