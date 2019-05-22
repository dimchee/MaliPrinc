using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    [HideInInspector]
    public float rad;
    [HideInInspector]
    public float phi;
    private Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
        rad = tr.position.magnitude;
        phi = Mathf.Atan2(tr.position.y, tr.position.x);
    }

    void Update()
    {
        tr.position = new Vector3(-Mathf.Sin(Mathf.Deg2Rad*phi)*rad, Mathf.Cos(Mathf.Deg2Rad*phi)*rad, 0);
        tr.rotation = Quaternion.Euler(0, 0, phi);
    }
    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(
            Vector3.zero, 
            Vector3.forward, 
            GetComponent<Transform>().position.magnitude
        );
    }
}
