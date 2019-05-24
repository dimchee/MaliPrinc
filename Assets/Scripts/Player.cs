using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float penCap;
    public float eraserCap;

    public int lvlCount;
    public Sprite lvlSprite;

    public float jumpCD;
    public float ranjivost = 1.0F;
    public float speed = 1.0F;
    public float strength = 1.0F;
    public float onGround = 0.4F;
    public float gravity = 2.0F;
    public float camMoveRad = 3.0F;
    private GameObject _pl;
    public GameObject player
    {
        get { if(!_pl) _pl = GameObject.FindWithTag("Player"); return _pl; }
    }
    private Rigidbody2D rb;
    private Transform _tr;
    public Transform tr
    {
        get { if(!_tr) _tr = player.GetComponent<Transform>(); return _tr; }
    }
    private float input;
    private Vector2 jump;
    private Transform cam;
    private CamMove camMove;
    private float last;

    void Start() 
    {
        cam = Camera.main.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody2D>();
        camMove = Camera.main.GetComponent<CamMove>();
        input = 0.0F; jump = Vector2.zero;
        last = 0.0F;
    }
    public void Move(float vec) { input = vec; }
    public void Jump() { jump = rb.position.normalized; }

    void FixedUpdate() 
    {
        rb.MoveRotation(Vector3.SignedAngle(Vector3.up, rb.position, Vector3.forward));
        rb.AddForce(
            -gravity * rb.position.normalized, 
            ForceMode2D.Force
        );// gravity
        rb.velocity = input*cam.right*speed 
        + (rb.velocity.x*cam.up.x + rb.velocity.y*cam.up.y)*cam.up;
        if(IsGrounded() && (Time.time - last > jumpCD))
        {
          	rb.AddForce(
                jump * strength,
                ForceMode2D.Impulse
            ); // jump
            jump = Vector2.zero;
            last = Time.time;
        }
    }
    private bool IsGrounded() 
    {
        Vector2 down = -tr.up;
        Vector2[] vec = {tr.right, -tr.right};
        foreach(Vector2 a in vec)
            if(Physics2D.Raycast(
                rb.position + down * 0.651f 
                            + a * 0.15f,
                down, onGround
            ).collider != null) return  true;
        return false;
    }
    void OnDrawGizmosSelected()
    {
        Vector2 down = -tr.up;
        Vector2[] vec = {tr.right, -tr.right};
        Vector2 pos  = tr.position;
        foreach(Vector2 a in vec) 
            Debug.DrawRay(
                pos + down * 0.651f 
                    + a * 0.15f,
                down*onGround, 
                Color.red
            );
    }
}