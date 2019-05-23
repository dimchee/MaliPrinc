using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MaliPrinc : MonoBehaviour
{
    [MenuItem("MaliPrinc/Otvori Scenu")]
    static public void Create() { Loader.Open("test.lvl"); }
    [MenuItem("MaliPrinc/Obrisi Scenu")]
    static public void Obrisi() { Loader.Delete("test.lvl"); }
}
