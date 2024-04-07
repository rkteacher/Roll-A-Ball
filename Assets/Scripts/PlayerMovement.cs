using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    PlayerInputActions playerInputActions;

    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    public float sizeInterval;
    public float minSize = 1f;
    public float maxSize = 10f;
    public float currentSize;

    private float raycastLength = 0.6f;

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, raycastLength);
    }

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Jump.performed += Jump;
        //playerInputActions.Movement.Move.performed += Movement_performed;

       
    }

    private void Start()
    {
        currentSize = 1f;
        transform.localScale = new Vector3(minSize, minSize, minSize);
    }

    private void Update()
    {

        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

       

        // float forwardInput = Input.GetAxis("Vertical");

        //   playerRB.AddForce(Vector3.forward  * speed * forwardInput * Time.deltaTime);

        /*if(Input.GetButtonDown("Jump"))
        {
            Jump();
        }*/

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(isGrounded());
        }
    }

    public void Grow()
    {
        
        if(currentSize < maxSize)
        {
            currentSize++;
            raycastLength = raycastLength + 0.6f;
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);
        }
    }

    public void Shrink()
    {
        
        if (currentSize > minSize)
        {
            currentSize--;
            raycastLength = raycastLength - 0.6f;
            transform.localScale = new Vector3(currentSize, currentSize, currentSize);
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

        if (other.CompareTag("Orb"))
        {
            Destroy(other.gameObject);
            Grow();
        }

        

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupTime);
        isPoweredUp = false;
    }

    private void FixedUpdate()
    {  
        Move();
     
    }

    private void Move()
    {
        Vector2 inputVector = playerInputActions.Movement.Move.ReadValue<Vector2>();
        float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        //checks if any value in input is greater than 0. This will only happen if an input is being pressed. 
        //Without this the player will always move.
        if (inputVector.magnitude > 0.1)
        {
            playerRB.AddForce(moveDir * speed * Time.deltaTime);
        }
    }



    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded() == true && context.performed)
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

        if (collision.gameObject.CompareTag("Spike"))
        {
            Shrink();
        }
    }

}
