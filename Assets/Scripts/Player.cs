using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("left/right speeed")]
    public float speed = 1.0F;
    [Tooltip("Jump strength")]
    public float strength = 1.0F;
    public float onGround = 0.4F;
    public float gravity = 2.0F;
    [Tooltip("Not used yet...")]
    public float balance = 0.5F;
    public float camMoveRad = 3.0F;
    private GameObject _pl;
    private GameObject player
    {
        get { if(!_pl) _pl = GameObject.FindWithTag("Player"); return _pl; }
    }
    private Rigidbody2D rb;
    private Transform _tr;
    private Transform tr
    {
        get { if(!_tr) _tr = player.GetComponent<Transform>(); return _tr; }
    }
    private Vector2 input;
    private Vector2 jump;
    private Transform cam;
    private CamMove camMove;

    void Start() 
    {
        cam = Camera.main.GetComponent<Transform>();
        rb = player.GetComponent<Rigidbody2D>();
        camMove = Camera.main.GetComponent<CamMove>();
    }
    public void Move(Vector2 vec) { input = vec; }
    public void Jump() { jump = rb.position.normalized; }

    void FixedUpdate() 
    {
        rb.MoveRotation(Vector3.SignedAngle(Vector3.up, rb.position, Vector3.forward));
        rb.AddForce(
            -gravity * rb.position.normalized, 
            ForceMode2D.Force
        );// gravity
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
        camMove.phi += 0.0001F * Mathf.Pow(Vector2.SignedAngle(cam.position, player.transform.position), 3.0F);
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