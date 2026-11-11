using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movespeed = 2f;
    private PlayerInputReader inputReader;
    private Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {
        inputReader = GetComponent<PlayerInputReader>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        Vector2 direction = inputReader.MoveInput;
        
        rb.linearVelocity = direction.normalized * movespeed;
    }
}
