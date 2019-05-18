using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            Tools.ChTool(Tool.eraser);
        if(Input.GetKeyDown(KeyCode.Q))
            Tools.ChTool(Tool.ground);
        if(Input.GetKeyDown("space"))
            player.Jump();
        player.Move(new Vector2(
            Input.GetAxisRaw("Horizontal"), 
            0 //Input.GetAxisRaw("Vertical")
        ));
    }
}
