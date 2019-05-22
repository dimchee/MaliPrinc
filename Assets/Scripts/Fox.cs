using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float speed;
    public float strength = 1.0F;
    public float gravity = 2.0F;
    public float jumpCD = 1.0F;
    private GameObject _fox;
    private GameObject fox
    {
        get { if(!_fox) _fox = GameObject.FindWithTag("Fox"); return _fox; }
    }
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get { if(!_rb) _rb = fox.GetComponent<Rigidbody2D>(); return _rb; }
    }
    void Start() {}
    private float lastJump;
    private Vector2 jump;
    public void Jump() 
    {
        if(Time.time - lastJump > jumpCD)
        {
            jump = rb.position.normalized; 
            lastJump=Time.time;
        } 
    }
    void FixedUpdate() 
    {
        rb.MoveRotation(Vector3.SignedAngle(Vector3.up, rb.position, Vector3.forward));
        rb.AddForce(
            -gravity * rb.position.normalized, 
            ForceMode2D.Force
        );// gravity
        rb.AddForce(
            (Camera.main.ScreenToWorldPoint(Input.mousePosition) - fox.transform.position).normalized * speed,
            ForceMode2D.Force
        ); // left right
        rb.AddForce(
            jump * strength,
            ForceMode2D.Impulse
        ); // jump
        jump = Vector2.zero;
        //rb.velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - fox.transform.position).normalized*20 * speed;
    }    
    void Update()
    {
        Drawer.Update(fox.transform.position);
    }
}
