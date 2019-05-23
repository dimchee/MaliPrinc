using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float speed = 1.0F;
    public float rad = 0.2F;
    private GameObject _cur;
    public GameObject cur
    {
        get 
        {
            if(!_cur) 
            {
                _cur = new GameObject(
                    "Cursor",
                    typeof(Rigidbody2D),
                    typeof(CircleCollider2D)
                );
                _cur.GetComponent<CircleCollider2D>().radius = rad;
                _cur.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                _cur.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                _cur.GetComponent<Rigidbody2D>().gravityScale = 0.0F;
                _cur.GetComponent<Rigidbody2D>().mass = 0.0F;
            }
            return _cur;
        }
    }
    private Rigidbody2D _rb;
    private Rigidbody2D rb
    {
        get { if(!_rb) _rb = cur.GetComponent<Rigidbody2D>(); return _rb; }
    }
    void FixedUpdate() 
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec = ( camPos - cur.transform.position );
        rb.velocity = vec.normalized  * Mathf.Min(Mathf.Pow(vec.magnitude, 3.0F) * speed/rad, 100.0F);
    }
}
