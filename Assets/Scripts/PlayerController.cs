using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashDuration = 0.2f;

    [Header("References")]
    public Rigidbody2D rb;
    public GameObject gameHandler;

    private Vector2 moveInput;
    private bool isDashing = false;
    public int HP = 3;

    public Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pulse
        if (IsDead())
        {
            Debug.Log("- - - - Restart due to player death - - - - ");
            AskForReset();
        }
        // Get movement input
        HandleMovement();

        // Rotate towards the mouse
        RotateTowardsMouse();

        // Dash Input
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void HandleMovement()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // originally checked if moveInput is null, didn't work
        // since its a vector 2, its never null
        // it is at least (0, 0)
        // (moveInput.magnitude > 0.01f) can also be used 
        if (moveInput != Vector2.zero)
        {
            animator.SetBool("isRunning", true);
            // Debug.Log($"Player is running");
        }
        else
        {
            animator.SetBool("isRunning", false);
            // Debug.Log($"Player stopped");
        }

    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            Vector2 newPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition); // Ensures smooth movement with collision
        }
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    System.Collections.IEnumerator Dash()
    {
        isDashing = true;
        Vector2 dashDirection = moveInput; // Dash in movement direction
        if (dashDirection == Vector2.zero) dashDirection = transform.right; // Default to facing direction if no input

        rb.linearVelocity = dashDirection * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    bool IsDead()
    {
        return (HP <= 0);
    }

    float CountLifetime()
    {
        // store player uptime in seconds 
        return 0;
    }
    public void AskForReset()
    {
        GameHandler resetScript = gameHandler.GetComponent<GameHandler>();
        resetScript.ReturnMenu();
    }

    void takeDamage(int dmgValue)
    {
        HP -= dmgValue;
        Debug.Log("- - - - Player takes damage - - - - ");
        // put i-frames here
        if (HP <= 0)
        {
            AskForReset();
            Debug.Log("- - - - Player DEATH - - - - ");
        }
        return;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            takeDamage(1);
        }
    }
}
