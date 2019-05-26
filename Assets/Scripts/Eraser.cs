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
        if(!obj) 
        {
            Controller.player.eraserCap -= 10; 
            Object.Destroy(gameObject); return; 
        }
    	else if(Unutra)
    	{
            Controller.player.eraserCap -= 5;
    		Controller.player.lvlCount++;
    		Object.Destroy(gameObject);	
    	}
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