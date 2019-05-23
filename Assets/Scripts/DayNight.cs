using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float tDana = 24.0F;
    private static Transform _bg;
    public static Transform bg
    {
        get { if(!_bg) _bg = GameObject.FindWithTag("Background").GetComponent<Transform>(); return _bg; }
    }
    public static float rot
    {
        get => bg.rotation.ToEuler().z;
    }
    void Start() { if(tDana == 0.0F) Debug.Log("trajanje dana 0!"); }
    public static bool isDay()
    {
        return rot < 0.0F;
    }
    public static bool isNight()
    {
        return rot > 0.0F;
    }
    void Update()
    {
        bg.RotateAround(Vector3.zero, Vector3.forward, 2*Mathf.PI * Time.deltaTime/tDana);
    }
}
