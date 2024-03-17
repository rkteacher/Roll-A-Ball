using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    Vector3 velovity;

    private bool isJumping;
    
    [SerializeField] private LayerMask ground;

    private Rigidbody playerRB;

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.5f);
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        Debug.DrawRay(transform.position, -Vector3.up, Color.red);
        // float forwardInput = Input.GetAxis("Vertical");

        //   playerRB.AddForce(Vector3.forward  * speed * forwardInput * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(isGrounded());
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //transform.Translate(moveHorizontal, 0, moveVertical);

        playerRB.AddForce(movement * speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded() == true)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
       
    }


    
}
