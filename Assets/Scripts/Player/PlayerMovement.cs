using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    private Vector2 moveDirection;


    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Kontrol();
    }

    void ProcessInputs()
    {
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(0, moveY).normalized;
    }

    void Kontrol()
    {
        rb.velocity = new Vector2(0, moveDirection.y * moveSpeed);
    }
}