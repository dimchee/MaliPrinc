using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Tool tool;
	private bool Unutra;
    
    public void Update()
    {
        if(!Unutra) return;
        if(!Input.GetKeyDown(KeyCode.F)) return; 
        if(Cursor.HasSprite()) return;
        Cursor.Set(
       		GetComponent<SpriteRenderer>().sprite,
            tool
        ); Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player") return;
        Unutra = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player") return;
        Unutra = false;
    }
}
