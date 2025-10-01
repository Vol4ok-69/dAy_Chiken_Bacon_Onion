using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private InputSystem_Actions inputActions;
    [Header("Скорость")]
    public float speed = 5f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 lastMoveDir;
    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDir = moveInput.normalized;
            spriteRenderer.flipX = lastMoveDir.x < 0;
        }
        animator.SetFloat("MoveX", lastMoveDir.x);
        animator.SetFloat("MoveY", lastMoveDir.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        var targetPosition = rb.position + moveInput * (speed * Time.fixedDeltaTime);
        rb.MovePosition(targetPosition);
    }

}
