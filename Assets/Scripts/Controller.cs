using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum Mode { Editor, Game };
public class Controller : MonoBehaviour
{
    public Mode Mode;
    private Player player;
    private Cursor curs;
    private CamMove cam;
    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main.GetComponent<CamMove>();
        curs = GetComponent<Cursor>();
        switch(Mode)
        {
            case Mode.Game:

                break;
            case Mode.Editor:
                Destroy(player.player);
                Destroy(player);
                curs.cur.GetComponent<Collider2D>().isTrigger = true;
                break;
        }
    }
    void Update()
    {
        Drawer.Update(curs.cur.transform.position);
        if(DayNight.isDay())Debug.Log("Day!");
        if(DayNight.isNight())Debug.Log("Night!");
    }
    void FixedUpdate()
    {
        switch(Mode)
        {
            case Mode.Game:
                if(Input.GetKeyDown(KeyCode.E))
                    Tools.ChTool(Tool.eraser);
                if(Input.GetKeyDown(KeyCode.Q))
                    Tools.ChTool(Tool.ground);
                if(Input.GetKeyDown("space"))
                    player.Jump();
                if(Input.GetKeyDown(KeyCode.LeftShift))
                    Portal.enable = true;
                player.Move(new Vector2(
                    Input.GetAxisRaw("Horizontal"), 
                    0 //Input.GetAxisRaw("Vertical")
                ));
                cam.phi += 0.0001F * Mathf.Pow(Vector2.SignedAngle(cam.tr.position, player.tr.position), 3.0F);
                break;
            case Mode.Editor:
                cam.phi -= Input.GetAxisRaw("Horizontal")*0.1F;
                if(Input.GetKeyDown(KeyCode.P))
                    Loader.Save("test.lvl");
                if(Input.GetKeyDown(KeyCode.E))
                    Tools.ChTool(Tool.eraser);
                if(Input.GetKeyDown(KeyCode.Q))
                    Tools.ChTool(Tool.ground);
                if(Input.GetKeyDown(KeyCode.T))
                    Tools.ChTool(Tool.trnje);
                break;
        }
    }
}