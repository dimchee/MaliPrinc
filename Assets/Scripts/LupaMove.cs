using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LupaMove : MonoBehaviour
{
    private Transform cam;
    private Transform tr;
    private Vector3 dis;
    void Start()
    {
        tr = GetComponent<Transform>();
        cam = Camera.main.GetComponent<Transform>();
        dis = cam.position - tr.position;
    }

    void Update()
    {
        tr.position = cam.position - dis;
    }
}
