using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float speed;
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

    void FixedUpdate() 
    {
        rb.velocity = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - fox.transform.position).normalized*20 * speed;
    }    
    void Update()
    {
        Drawer.Update(fox.transform.position);
    }
}
