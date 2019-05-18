 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEditor;

public class Debugger : Editor
{
    private CamMove cam;
    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera").GetComponent<CamMove>();
    }
    void Update()
    {
        
    }
}
