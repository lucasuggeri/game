using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movespeed = 2f;
   [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {
      
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
