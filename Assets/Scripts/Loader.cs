using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class Loader
{
    static string fold = "Assets/Lvls/";
    static string cur;
    static private string[] splitLine(string s)
    {
        return s.Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);
    }
    static public void Open(string name)
    {
        if(!File.Exists(@fold + name)) return;
        string[] code = File.ReadAllLines(@fold + name);
        float width = 0F; Tool t;
        List<Vector3> vec = new List<Vector3>();
        foreach(var line in code)
        {
            string[] s = splitLine(line);
            if(s.Length == 2) 
            {
                if(vec.Count != 0)
                {
                    Tools.Draw(vec.ToArray(), width);
                    vec.Clear();
                }
                t = (Tool)float.Parse(s[0]);
                width = float.Parse(s[1]);
            }
            if(s.Length == 3) vec.Add(new Vector3(
                float.Parse(s[0]),
                float.Parse(s[1]),
                float.Parse(s[2])
            ));
        }
        Tools.Draw(vec.ToArray(), width);
    }
    static public void Save(string name) 
    {
        StreamWriter sw;
        if (!File.Exists(@fold + name)) sw = File.CreateText(@fold + name);
        else                                  sw = File.AppendText(@fold + name);
        sw.Write(cur);
    }
    static public void Update(Vector3[] vec, float width, Tool t)
    {
        cur += (int)t + " " + width + "\n";
        foreach(var a in vec) cur += a.x + " " + a.y + " " + a.z + "\n";
    }
}
