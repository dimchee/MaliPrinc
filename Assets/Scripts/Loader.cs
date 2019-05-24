using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class Loader
{
    static private PhysicsMaterial2D _ph;
    static private PhysicsMaterial2D ph
    {
        get{ if(!_ph) _ph = Resources.Load<PhysicsMaterial2D>("planet"); return _ph; }
    }
    static string fold = "Assets/Lvls/";
    static string cur;
    static private string[] splitLine(string s)
    {
        return s.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);
    }

    static public void Delete(string name)
    {
        if(File.Exists(@fold + name)) File.Delete(@fold + name);
    }
    static public void Open(string name)
    {
        if(!File.Exists(@fold + name)) return;
        string[] code = File.ReadAllLines(@fold + name);
        float width = 0F; Mat mat;
        List<Vector3> vec = new List<Vector3>();
        var tr = (new GameObject(name)).GetComponent<Transform>();
        foreach(var line in code)
        {
            string[] s = splitLine(line);
            if(s.Length == 3) vec.Add(new Vector3(
                float.Parse(s[0]),
                float.Parse(s[1]),
                float.Parse(s[2])
            ));
            if(s.Length == 2) 
            {
                mat = (Mat)float.Parse(s[0]);
                width = float.Parse(s[1]);
                if(vec.Count != 0)
                {
                    DrawLine("plat", vec.ToArray(), width, Tools.Mater(mat), tr);
                    vec.Clear();
                }
            }
        }
    }
    static public void Save(string name) 
    {
        StreamWriter sw;
        if (!File.Exists(@fold + name)) sw = File.CreateText(@fold + name);
        else                            sw = File.AppendText(@fold + name);
        sw.Write(cur);
    }
    static public void Update(Vector3[] vec, float width, Tool t)
    {
        foreach(var a in vec) cur += a.x + " " + a.y + " " + a.z + "\n";
        cur += (int)t + " " + width + "\n\n\n";
    }
    static public GameObject DrawLine(string name, Vector3[] vec, float width, Material mat, Transform p = null)
    {
        var block = new GameObject(
            name,
            typeof(MeshRenderer),
            typeof(MeshFilter),
            typeof(PolygonCollider2D),
            typeof(Eraser)
        );

        Vector2[] col;
        block.GetComponent<MeshFilter>().mesh = MakeMesh(vec, width, out col);
        block.GetComponent<MeshRenderer>().material = mat;
        block.GetComponent<PolygonCollider2D>().SetPath(0, col);
        block.GetComponent<PolygonCollider2D>().sharedMaterial = ph; 
        block.GetComponent<Transform>().SetParent(p);
        block.layer = 10;
        return block;
    }
    static public Mesh MakeMesh(Vector3[] vec, float width, out Vector2[] col) // need to be better
    {
        var vert = new Vector3[2 * vec.Length];
        var uv   = new Vector2[2 * vec.Length];
        col  = new Vector2[2 * vec.Length]; 
        var tri  = new int[6 * vec.Length - 6];
        if(vec.Length==1) return null;
        Vector3 norm; for(int i=0; i<vec.Length; i++)
        {
            if(i+1==vec.Length) norm = new Vector3(
                 (vec[i]-vec[i-1]).y,
                -(vec[i]-vec[i-1]).x
            ).normalized;
            else if(i==0) norm = new Vector3(
                 (vec[i+1]-vec[i]).y,
                -(vec[i+1]-vec[i]).x
            ).normalized;
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
        mesh.vertices = vert;
        mesh.triangles = tri;
        mesh.uv = uv;
        return mesh;
    }
}
