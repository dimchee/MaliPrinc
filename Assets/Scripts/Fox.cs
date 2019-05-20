using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : MonoBehaviour
{
    public float len;
    private GameObject fox;
    private GameObject body;
    private Queue<Vector3> q;
    private static Material _mat;
    private static Material mat
    {
        get 
        {
            if (_mat == null)
                _mat = Resources.Load("fox")
                    as Material;
            return _mat;
        }
    }
    void Start()
    {
        q = new Queue<Vector3>();
        fox = new GameObject(
            "Fox",
            typeof(CircleCollider2D)
        );
        fox.GetComponent<CircleCollider2D>().radius = 0.1F;
        //q.Enqueue(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //body = Loader.DrawLine("body", q.ToArray(), 0.1F, mat, fox.GetComponent<Transform>());
        //fox.layer = 10;
    }

    void FixedUpdate() 
    {
        fox.transform.position = Vector3.MoveTowards(
            fox.transform.position, 
            Camera.main.ScreenToWorldPoint(Input.mousePosition), 
            0.2F
        );   
    }    
    void Update()
    {

        Drawer.Update(fox.transform.position);
        //q.Enqueue(Camera.main.ScreenToWorldPoint(Input.mousePosition)); if(q.Count > 50) q.Dequeue();
        //Vector2[] col;
        //body.GetComponent<MeshFilter>().mesh = Loader.MakeMesh(q.ToArray(), 0.1F, out col);
        //body.GetComponent<PolygonCollider2D>().SetPath(0, col);
        //body.GetComponent<Transform>().SetParent(fox.GetComponent<Transform>());
    }
}
