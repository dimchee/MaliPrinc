using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Tool { None, Eraser, Pen };
public enum Mat  { Dirt, Line };

public static class Tools
{
    static Mat mat;
    public static void Draw(Vector3[] vec, float width)
    {
        if(Cursor.Tool == Tool.Pen)
        {
            Loader.Update(vec, width, Tool.Pen);
            Loader.DrawLine("line", vec, width, Mater(mat));
        }
    }
    public static Material Mater(Mat m)
    {
        switch(m)
        {
            case Mat.Dirt: return Resources.Load<Material>("Platforms/dirt");
            case Mat.Line: return Resources.Load<Material>("Platforms/line");
        }
        return null;
    }
}
