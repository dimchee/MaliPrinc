using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Tool { eraser, ground };

public static class Tools
{
    private static Tool tool;
    private static Material _mat;
    public static Material mat
    {
        get 
        {
            if (_mat == null)
                _mat = Resources.Load("block") 
                    as Material; 
            return _mat; 
        }
    }
    public static bool Use(Tool t) { return  tool==t; }
    public static void ChTool(Tool t) { tool = t; }
    public static void Erase(GameObject x)
    { 
        if(!Use(Tool.eraser)) return; 
        Object.Destroy(x); 
    }
    public static void Draw(Vector3[] vec, float width)
    {
        if(!Use(Tool.ground)) return;
        Loader.Update(vec, width, tool);
        Loader.DrawLine("platform", vec, width, mat);
    }
}
