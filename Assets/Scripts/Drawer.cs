using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Line : MonoBehaviour
{
    private LineRenderer line;
    void Start() { line = GetComponent<LineRenderer>(); }
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
public class Drawer : MonoBehaviour
{

    private Vector2 mPos;
    private Material _mat;
    private Material mat
    {
        get 
        {
            if (_mat == null)
                _mat = Resources.Load("line") 
                    as Material; 
            return _mat; 
        }
    }
    private string[] gcode;

    private GameObject MakeLine(string name, Vector2 pos)
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

    void Start()
    {
        //string path = "Assets/MaliPrinc.gcode";
        //gcode = System.IO.File.ReadAllLines(@path);
    }

    private Line Aline;
    private GameObject Mline;

    private static float toFloat(string s)
    {
        return float.Parse(s.Substring(1))/2000F;
    }

    void Update () 
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
            Mline = MakeLine("mouse", mPos);
        if(Input.GetMouseButton(0))
            Mline.GetComponent<Line>().Add(mPos);
        if(Input.GetMouseButtonUp(0))
        {
            var vec = Mline.GetComponent<Line>().Vert();
            Tools.Draw(vec, 0.1F); Object.Destroy(Mline);
        }

        /*
        if(Time.time > time && cur < gcode.Length)  
        {
            string[] cmd = gcode[cur++].Split(' ');
            if(cmd[0] == "M300")
                Aline = MakeLine("auto", mPos);
            if(cmd[0] == "G1")
            {
                Aline.GetComponent<Line>().Add(toFloat(cmd[1]), toFloat(cmd[2]));
                //time += toFloat(cmd[3])/100.0F;
            } 
            //if(cmd[0] == "G4") time += toFloat(cmd[1])/100.0F;
        }
        */
    }
}
