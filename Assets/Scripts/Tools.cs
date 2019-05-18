using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Tool { eraser, ground };

public static class Tools
{
    private static Tool tool;
    private static Material _mat;
    private static Material mat
    {
        get 
        {
            if (Tools._mat == null)
                Tools._mat = Resources.Load("block") 
                    as Material; 
            return Tools._mat; 
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

        var vert = new Vector3[2 * vec.Length];
        var uv   = new Vector2[2 * vec.Length];
        var col  = new Vector2[2 * vec.Length]; 
        var tri  = new int[6 * vec.Length - 6];
        Vector3 norm; for(int i=0; i<vec.Length; i++)
        {
            if(i==0 || i+1==vec.Length) norm = Vector3.down;
            else norm = new Vector3(
                 (vec[i+1]-vec[i-1]).y, 
                -(vec[i+1]-vec[i-1]).x
            ).normalized;

            vert[2*i+0] = vec[i] + norm*width;
            vert[2*i+1] = vec[i] - norm*width;
        }
        for(int i=0; i<vec.Length; i++) 
        {
            col[i] = vert[2*i];
            col[2*vec.Length-i-1] = vert[2*i+1];
        }

        int[] din = {0, 1, 3, 0, 3, 2};
        for(int i=0; i<vec.Length-1; i++) for(int j=0; j<6; j++)
            tri[6*i + j] = 2*i + din[j];

        float dist = 0.0F; for(int i=0; i<vec.Length-1; i++)
        {
            uv[2*i+0] = new Vector2(dist, 0); 
            uv[2*i+1] = new Vector2(dist, 1);
            dist += (vec[i+1]-vec[i]).magnitude*5.0F;
        }

        var mesh = new Mesh();
        var block = new GameObject(
            "Platform",
            typeof(MeshRenderer),
            typeof(MeshFilter),
            typeof(PolygonCollider2D),
            typeof(Eraser)
        );

        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.uv = uv;

        block.GetComponent<MeshFilter>().mesh = mesh;
        block.GetComponent<MeshRenderer>().material = mat;
        block.GetComponent<PolygonCollider2D>().SetPath(0, col); 
    }
}
