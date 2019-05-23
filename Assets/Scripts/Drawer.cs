using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Line : MonoBehaviour
{
    private LineRenderer _line;
    private LineRenderer line
    {
        get { if(!_line) _line = GetComponent<LineRenderer>(); return _line; }
    }
    public void Start() { line.Simplify(1000000000.0F); }
    public void Add(Vector2 pos)
    {
        if(line) line.SetPosition(line.positionCount++, pos);
    }
    public void Add(float a, float b) { Add(new Vector2(a, b)); }

    public Vector3[] Vert()
    {
        var vec = new Vector3[line.positionCount];
        line.GetPositions(vec);
        return vec;
    }
}
public static class Drawer
{
    private static Material _mat;
    private static Material mat
    {
        get 
        {
            if (_mat == null)
                _mat = Resources.Load("line") 
                    as Material; 
            return _mat; 
        }
    }
    public static GameObject MakeLine(string name, Vector2 pos)
    {
        var line = new GameObject(
            name,
            typeof(LineRenderer),
            typeof(Line)
        );
        line.transform.position = pos;
        var rend = line.GetComponent<LineRenderer>(); 
        rend.positionCount = 0;
        rend.material = mat;
        rend.widthCurve = new AnimationCurve(new Keyframe(0.0F, 0.05F));
        var grad = new Gradient(); grad.SetKeys(
            new GradientColorKey[] { 
                new GradientColorKey(Color.green, 0.0F),
                new GradientColorKey(Color.magenta, 0.15F),
                new GradientColorKey(Color.red, 0.3F),
                new GradientColorKey(Color.yellow, 0.55F),
                new GradientColorKey(Color.magenta, 0.8F),
                new GradientColorKey(Color.cyan, 1.0F)
            },
            new GradientAlphaKey[] { 
                new GradientAlphaKey(1, 0.0F), 
                new GradientAlphaKey(1, 1.0F)
            }
        );

        rend.colorGradient = grad;
        return line;
    }
    private static GameObject Mline;
    public static void Update(Vector2 mPos) 
    {
        if(Input.GetMouseButtonDown(0))
            Mline = MakeLine("mouse", mPos);
        if(Input.GetMouseButton(0))
            Mline.GetComponent<Line>().Add(mPos);
        if(Input.GetMouseButtonUp(0))
        {
            var vec = Mline.GetComponent<Line>().Vert();
            Object.Destroy(Mline); Tools.Draw(vec, 0.1F); 
        }
    }
}
