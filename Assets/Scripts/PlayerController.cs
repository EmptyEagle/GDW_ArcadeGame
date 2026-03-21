using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public KeyCode key_MoveLeft;
    public KeyCode key_MoveRight;
    public KeyCode key_Jump;
    // directionalInput will be either -1, 0, or 1; this signifies the direction that the player will move
    public int directionalInput;
    private Rigidbody2D playerRb;
    // This is a value that accumulates when a direction is held and deteriorates when no direction is held
    private float timeBeforeSlowdown;
    private bool isGrounded;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        directionalInput = 0;
        isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Get directional input
        if (Input.GetKeyDown(key_MoveLeft))
        {
            directionalInput--;
        }
        else if (Input.GetKeyUp(key_MoveLeft))
        {
            directionalInput++;
        }
        if (Input.GetKeyDown(key_MoveRight))
        {
            directionalInput++;
        }
        else if (Input.GetKeyUp(key_MoveRight))
        {
            directionalInput--;
        }

        if (Input.GetKeyDown(key_Jump) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Move player based on directional input
        if (directionalInput == 0)
        {
            if (timeBeforeSlowdown > 0)
                timeBeforeSlowdown -= 0.5f;
            else
            {
                timeBeforeSlowdown = 0;
                playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, 0);
            }
        }
        else
        {
            playerRb.AddForce(Vector3.right * directionalInput * movementSpeed);
            if (timeBeforeSlowdown < 3)
                timeBeforeSlowdown += 0.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("Touched ground");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("Left ground");
        }
    }
}
