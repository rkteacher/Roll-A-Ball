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



    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // float forwardInput = Input.GetAxis("Vertical");

        //   playerRB.AddForce(Vector3.forward  * speed * forwardInput * Time.deltaTime);

        if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        playerRB.AddForce(movement * speed * Time.deltaTime);
    }

    private void Jump()
    {
        velovity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
    }

}
