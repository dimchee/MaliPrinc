using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    void OnMouseDown() { Tools.Erase(gameObject); }
}