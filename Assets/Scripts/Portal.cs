using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
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
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!enable) return;
        if(other.gameObject.tag != "Player") return;
        player.transform.position = new Vector3(
            dest.transform.position.x,
            dest.transform.position.y,
            0.0F
        ); enable = false; 
    }
}
