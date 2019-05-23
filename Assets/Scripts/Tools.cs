using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Tool { eraser, ground, trnje };

public static class Tools
{
    private static Tool tool;
    private static Material _block;
    public static Material block
    {
        get 
        {
            if (_block == null)
                _block = Resources.Load<Material>("block"); 
            return _block; 
        }
    }
    private static Material _trnje;
    public static Material trnje
    {
        get 
        {
            if (_trnje == null)
                _trnje = Resources.Load<Material>("trnje"); 
            return _trnje; 
        }
    }
    public static Material Mat(Tool t)
    {
        switch(t)
        {
            case Tool.ground: return block;
            case Tool.trnje:  return trnje;
        }
        return null;
    }
    public static void ChTool(Tool t) { tool = t; }
    public static void Erase(GameObject x)
    { 
        if(tool != Tool.eraser) return; 
        Object.Destroy(x); 
    }
    public static void Draw(Vector3[] vec, float width)
    {
        switch(tool)
        {
            case Tool.ground:
                Loader.Update(vec, width, tool);
                Loader.DrawLine("platform", vec, width, block);
                break;
            case Tool.trnje:
                Loader.Update(vec, width, tool);
                var obj = Loader.DrawLine("trnje", vec, width, trnje);
                obj.GetComponent<Collider2D>().isTrigger = true;
                obj.AddComponent<Trnje>();
                break;
        }
    }
}
