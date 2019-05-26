using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trnje : MonoBehaviour
{
    private bool Unutra;

    public void FixedUpdate()
    {
        if(!Unutra) return;
        Controller.player.health = 
        Mathf.Max( 0, 
            Controller.player.health - 
            Time.deltaTime*Controller.player.ranjivost
        );
        Player.jump = Vector2.zero;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player") return;
        other.gameObject.GetComponent<Rigidbody2D>().drag = 30.0F;
        Unutra = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player") return;
        other.gameObject.GetComponent<Rigidbody2D>().drag = 0.3F;
        Unutra = false;
    }
}
