﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [HideInInspector]
    public float rad;
    [HideInInspector]
    public float phi;
    private Transform _tr;
    public Transform tr
    {
        get { if(!_tr) _tr = GetComponent<Transform>(); return _tr; }
    }
    void Start()
    {
        rad = tr.position.magnitude;
        phi = Mathf.Atan2(tr.position.y, tr.position.x);
    }

    void Update()
    {
        tr.position = new Vector3(-Mathf.Sin(Mathf.Deg2Rad*phi)*rad, Mathf.Cos(Mathf.Deg2Rad*phi)*rad, 0);
        tr.rotation = Quaternion.Euler(0, 0, phi);
    }
}
