using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trnje : MonoBehaviour
{
    public static bool enable;
    public GameObject dest;
    private GameObject _pl;
    private GameObject player
    {
        get { if(!_pl) _pl = GameObject.FindWithTag("Player"); return _pl; }
    }

    void Update()
    {
        
    }
    public void OnTrigger2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player") return;
        // player.hp--;
    }
}
