using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movespeed = 2f;
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private Rigidbody2D rb;
    // Update is called once per frame

    private Vector2 facingDirection;

    private static readonly int FrontHash = Animator.StringToHash("Front");
    private static readonly int BackHash = Animator.StringToHash("Back");
    private static readonly int LeftHash = Animator.StringToHash("Left");
    private static readonly int RightHash = Animator.StringToHash("Right");
    private static readonly int NoneHash = Animator.StringToHash("None");
    [SerializeField] private Animator animator;
    private enum DirectionState
    {
        None,
        Front,
        Back,
        Left,
        Right
    }

    private DirectionState currentDirection;
    private DirectionState lastDirection;
    private void FixedUpdate()
    {
        Move();
      
    }

    private void Update()
    {
        UpdateAnimation();
    }
    private void Move()
    {
        Vector2 direction = inputReader.MoveInput;
        facingDirection = direction;
        if (direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                facingDirection = direction.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                facingDirection = direction.y > 0 ? Vector2.up : Vector2.down;
            }
        }

        rb.linearVelocity = direction.normalized * movespeed;
    }

    private void UpdateAnimation()
    {
        float horizontal = facingDirection.x;
        float vertical = facingDirection.y;
        if (inputReader.MoveInput == Vector2.zero)
        {
            currentDirection = DirectionState.None;

            animator.SetBool(FrontHash, currentDirection == DirectionState.Front);
            animator.SetBool(BackHash, currentDirection == DirectionState.Back);
            animator.SetBool(LeftHash, currentDirection == DirectionState.Left);
            animator.SetBool(RightHash, currentDirection == DirectionState.Right);
            animator.SetBool(NoneHash, currentDirection == DirectionState.None);

            return;

        }
            
        if (horizontal > 0)
            currentDirection = DirectionState.Right;
        else if (horizontal < 0)
            currentDirection = DirectionState.Left;
        else if (vertical > 0)
            currentDirection = DirectionState.Back;
        else if (vertical < 0)
            currentDirection = DirectionState.Front;

        if (currentDirection == lastDirection)
            return;

        lastDirection = currentDirection;

        animator.SetBool(FrontHash, currentDirection == DirectionState.Front);
        animator.SetBool(BackHash, currentDirection == DirectionState.Back);
        animator.SetBool(LeftHash, currentDirection == DirectionState.Left);
        animator.SetBool(RightHash, currentDirection == DirectionState.Right);
        animator.SetBool(NoneHash, currentDirection == DirectionState.None);
    }
}
