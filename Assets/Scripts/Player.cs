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
    private Transform _tr;
    private Transform tr
    {
        get { if(!_tr) _tr = GetComponent<Transform>(); return _tr; }
    }
    private Vector2 input;
    private Vector2 jump;
    private Transform cam;

    void Start() 
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Move(Vector2 vec) { input = vec; }
    public void Jump() { jump = rb.position.normalized; }

    void FixedUpdate() 
    {
        rb.AddForce(
            -gravity * rb.position.normalized,
            ForceMode2D.Force
        ); // gravity
        if(IsGrounded())
        {
            rb.AddForce(
                input.x * speed * cam.right, 
                ForceMode2D.Force
            ); // left right
            rb.AddForce(
                jump * strength,
                ForceMode2D.Impulse
            ); // jump
            jump = Vector2.zero;
        }
    }
    private bool IsGrounded() 
    {
        Vector2[] bas = {tr.up, -tr.up};
        Vector2[] vec = {tr.right, -tr.right};
        foreach(Vector2 a in bas) foreach (Vector2 b in vec)
            if(Physics2D.Raycast(
                rb.position + a * 0.21f 
                            + b * 0.15f,
                a, onGround
            ).collider != null) return  true;
        foreach(Vector2 a in vec) foreach (Vector2 b in bas)
            if(Physics2D.Raycast(
                rb.position + a * 0.21f 
                            + b * 0.15f,
                a, onGround
            ).collider != null) return  true;
        return false;
    }
    void OnDrawGizmosSelected()
    {
        Vector2[] bas = {tr.up, -tr.up};
        Vector2[] vec = {tr.right, -tr.right};
        Vector2 pos  = tr.position;
        foreach(Vector2 a in bas) foreach (Vector2 b in vec)
            Debug.DrawRay(
                pos + a * 0.21f 
                    + b * 0.15f,
                a*onGround, 
                Color.red
            );
         foreach(Vector2 a in vec) foreach (Vector2 b in bas)
            Debug.DrawRay(
                pos + a * 0.21f 
                    + b * 0.15f,
                a*onGround, 
                Color.red
            );
    }
}