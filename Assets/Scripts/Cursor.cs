using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float speed = 1.0F;
    public static float rad = 0.2F;
    public static Tool Tool;
    private static GameObject _cur;
    public static GameObject cur
    {
        get 
        {
            if(!_cur) 
            {
                _cur = new GameObject(
                    "Cursor",
                    typeof(Rigidbody2D),
                    typeof(CircleCollider2D),
                    typeof(SpriteRenderer)
                );
                _cur.GetComponent<CircleCollider2D>().radius = rad;
                _cur.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                _cur.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
                _cur.GetComponent<Rigidbody2D>().gravityScale = 0.0F;
                _cur.GetComponent<Rigidbody2D>().mass = 0.0F;
                _cur.GetComponent<Rigidbody2D>().freezeRotation = true;
                _cur.GetComponent<CircleCollider2D>().enabled = false;
            }
            return _cur;
        }
    }
    void Start() { Tool = Tool.None; }
    private static SpriteRenderer _curRend;
    public static SpriteRenderer curRend
    {
        get{ if(!_curRend) _curRend = cur.GetComponent<SpriteRenderer>(); return _curRend; }
    }
    private static Rigidbody2D _rb;
    private static Rigidbody2D rb
    {
        get { if(!_rb) _rb = cur.GetComponent<Rigidbody2D>(); return _rb; }
    }
    void FixedUpdate() 
    {
        Vector3 camPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 vec = ( camPos - cur.transform.position );
        rb.velocity = vec.normalized  * Mathf.Min(Mathf.Pow(vec.magnitude, 3.0F) * speed/rad, 100.0F);
    }
    void Update()
    {
        if(Tools.HasInk()) Drawer.Update(cur.transform.position);
        if(Drawer.Mline != null) cur.GetComponent<CircleCollider2D>().enabled = true;
        else                     cur.GetComponent<CircleCollider2D>().enabled = false;
    }

    public static bool HasSprite() { return curRend.sprite != null; }
    public static void Set(Sprite s, Tool t) { curRend.sprite = s; Tool = t; }
    public static Sprite Del(out Tool t)
    {
        var s = curRend.sprite;
        curRend.sprite = null;
        t = Tool; Tool = Tool.None; return s;
    }
}
