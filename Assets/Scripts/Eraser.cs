using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
	public bool obj = false;
	private bool Unutra = false;
    void OnMouseDown() 
    {
        if(Cursor.Tool != Tool.Eraser) return;
        if(!obj) { Object.Destroy(gameObject); return; }
    	if(Unutra)
    	{
    		Controller.player.lvlCount++;
    		Object.Destroy(gameObject);	
    	}
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