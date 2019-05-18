using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0F;
    public float strength = 1.0F;
    public float onGround = 0.4F;
    public float gravity = 2.0F;
    private Rigidbody2D rb;
    private Vector2 input;
    private Vector2 jump;

    void Start() { rb = GetComponent<Rigidbody2D>(); }
    void FixedUpdate() 
    {
        rb.AddForce(
            -gravity * rb.position.normalized,
            ForceMode2D.Force
        ); // gravity
        rb.AddForce(
            input * speed, 
            ForceMode2D.Force
        ); // left right
        rb.AddForce(
            jump * strength,
            ForceMode2D.Impulse
        ); // jump
        jump = Vector2.zero;
    }

    private bool IsGrounded() 
    {
        Debug.DrawRay(
            rb.position + Vector2.down * 0.21f 
                        + Vector2.left * 0.15f,
            Vector2.down * onGround,
            Color.red
        );
        Debug.DrawRay(
            rb.position + Vector2.down  * 0.21f 
                        + Vector2.right * 0.15f, 
            Vector2.down * onGround,
            Color.red
        );
        RaycastHit2D hit1 = Physics2D.Raycast(
            rb.position + Vector2.down  * 0.21f 
                        + Vector2.right * 0.15f, 
            Vector2.down, 
            onGround
        );
        RaycastHit2D hit2 = Physics2D.Raycast(
            rb.position + Vector2.down * 0.21f 
                        + Vector2.left * 0.15f,
            Vector2.down, 
            onGround
        );


        if(hit1.collider != null) return  true;
        if(hit2.collider != null) return  true;
        else                      return false;
    }

    public void Move(Vector2 vec) { input = vec; }
    public void Jump() { if (IsGrounded()) jump = rb.position.normalized; }
}
