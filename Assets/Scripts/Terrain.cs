using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public float rad = 15.0F;
    private static Material _mat;
    private static Material mat
    {
        get 
        {
            if (_mat == null)
                _mat = Resources.Load("mars") 
                    as Material; 
            return _mat; 
        }
    }
    Vector3 disp(Vector3 vec, float phi)
    {
        return vec *  (200 + Mathf.Sin(phi*30)) / 200;
    }
    void Start()
    {
        List<Vector3> v = new List<Vector3>();
        List<int> tri = new List<int>();
        v.Add(Vector3.zero); for(float phi=0F; phi<2*Mathf.PI; phi+=0.01F)
            v.Add(disp(new Vector3(rad*Mathf.Cos(phi), rad*Mathf.Sin(phi)), phi));
        v.Add(v[1]);
        Vector3[] vert = v.ToArray();
        Vector2[] col  = new Vector2[vert.Length];
        for(int i=0; i<vert.Length; i++) col[i]=vert[i];
        for(int i=2; i<vert.Length; i++)
        {
            tri.Add(0);
            tri.Add(i);
            tri.Add(i-1);
        }
        var mesh = new Mesh();
        var planet = new GameObject(
            "Planet",
            typeof(MeshRenderer),
            typeof(MeshFilter),
            typeof(PolygonCollider2D)
        );

        mesh.vertices = vert;
        mesh.triangles = tri.ToArray();
        Vector2[] uv = new Vector2[col.Length];
        var dis = new Vector2(0.0F, 0.5F);
        for(int i=0; i<col.Length; i++)
            uv[i] = col[i]/40 - dis;
        mesh.uv = uv;

        planet.GetComponent<MeshFilter>().mesh = mesh;
        planet.GetComponent<MeshRenderer>().material = mat;
        planet.GetComponent<PolygonCollider2D>().SetPath(0, col);
        planet.GetComponent<PolygonCollider2D>().sharedMaterial = Resources.Load<PhysicsMaterial2D>("planet"); 
        planet.layer = 10;
    }
}