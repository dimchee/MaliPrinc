using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float tDana = 24.0F;
    private Transform bg;
    void Start()
    {
        bg = GameObject.FindWithTag("Background").GetComponent<Transform>();
        if(tDana == 0.0F) Debug.Log("trajanje dana 0!");
    }
    void Update()
    {
        bg.RotateAround(Vector3.zero, Vector3.forward, 2*Mathf.PI * Time.deltaTime/tDana);
    }
}
