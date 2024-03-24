using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public Transform cam;

    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    Vector3 velovity;

    private bool isJumping;
    public bool isPoweredUp;
    public float powerBounceStrength;
    public float powerupTime = 7f;
    
    [SerializeField] private LayerMask ground;

    private Rigidbody playerRB;

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.6f);
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

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Powerup"))
        {
            isPoweredUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupTime);
        isPoweredUp = false;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if( movement.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            //float moveHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            //float moveVertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            //transform.Translate(moveHorizontal, 0, moveVertical);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            playerRB.AddForce(moveDir * speed * Time.deltaTime);
        }
       
    }

    private void Jump()
    {
        if (isGrounded() == true)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isPoweredUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();

            //position of the enemy collision - the player position get direction
            Vector3 bounceDir = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(bounceDir * powerBounceStrength, ForceMode.Impulse);
        }
    }

}
