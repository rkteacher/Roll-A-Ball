using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float rotationSpeed = 5f;
    

    void Update()
    {
        // this is the left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");

        //                                                            V variable   V Time.deltaTime
        // wrong way:  transform.Rotate(Vector3.up, horizontalInput * 33 * Time.time);

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
