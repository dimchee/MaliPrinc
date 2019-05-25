using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public enum Mode { Editor, Game };
public class Controller : MonoBehaviour
{
    public Mode Mode;
    public static Player player;
    public static CamMove cam;
    public static bool isPaused;

    private static GameObject _pM;
    public static GameObject pauseMenu
    {
        get{ if(!_pM) _pM = GameObject.FindWithTag("PauseMenu"); return _pM; }
    }

    void Start()
    {
        player = GetComponent<Player>();
        cam = Camera.main.GetComponent<CamMove>();
        switch(Mode)
        {
            case Mode.Game:

                break;
            case Mode.Editor:
                Destroy(player.player);
                Destroy(player);
                Cursor.cur.GetComponent<Collider2D>().isTrigger = true;
                break;
        }

        Pause(); Pause();
        // Pause();
    }

    public static void Pause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Debug.Log("Here");
    }
    
    //if(DayNight.isDay())Debug.Log("Day!");
    //if(DayNight.isNight())Debug.Log("Night!");
    void Update()
    {
        if(player.health == 0.0F) SceneManager.LoadScene(0); 
        switch(Mode)
        {
            case Mode.Game:
                if(Input.GetKeyDown("space"))
                    player.Jump();
                if(Input.GetKeyDown(KeyCode.LeftShift))
                    Portal.enable = true;
                player.Move(Input.GetAxisRaw("Horizontal"));
                cam.phi += 0.0005F * Mathf.Pow(Vector2.SignedAngle(cam.tr.position, player.tr.position), 3.0F);
                if (Input.GetKeyDown(KeyCode.Escape))
                    Pause();
                break;
            case Mode.Editor:
                cam.phi -= Input.GetAxisRaw("Horizontal")*0.1F;
                if(Input.GetKeyDown(KeyCode.P))
                    Loader.Save("test.lvl");
                //if(Input.GetKeyDown(KeyCode.E))
                //    Tools.ChTool(Tool.eraser);
                //if(Input.GetKeyDown(KeyCode.Q))
                //    Tools.ChTool(Tool.pen);
                break;
        }
    }
}