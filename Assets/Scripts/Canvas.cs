using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
	private const int invNum = 5;

	public Image healthBar;
	public Image penBar;
	public Image eraserBar;

	public Text beerCountText;
	public Text crownCountText;
	public Text lampCountText;

	public Image[] placeHolders = new Image[invNum];
	private bool[] taken = new bool[invNum];
    private Tool[] tool  = new Tool[invNum];

    private Sprite _blank;
    public Sprite blank
    {
        get { if(!_blank) _blank = Resources.Load<Sprite>("blank"); return _blank; }
    }
    private static Player player { get => Controller.player; }

    void Start()
    {
        for (int i = 0; i < invNum; i++)
        	taken[i] = false;
    }

    void Update()
    {
    	healthBar.fillAmount = Controller.player.health / 100;
        penBar.fillAmount = player.penCap / 100;
        eraserBar.fillAmount = player.eraserCap / 100;

        beerCountText.text = player.lvlCount + " / 3";
    }

    public static void Pause()
    {
    	Controller.isPaused = !Controller.isPaused;
    	GameObject.FindWithTag("PauseMenu").SetActive(Controller.isPaused);
    }


    public void Clicked(int index) 
    {
        if (taken[index] && !Cursor.HasSprite()) 
        {
            Cursor.Set(placeHolders[index].sprite, tool[index]);
            taken[index] = false;
            placeHolders[index].sprite = blank;
        }
        else if (!taken[index] && Cursor.HasSprite())
        {
            taken[index] = true;
            placeHolders[index].sprite = Cursor.Del(out tool[index]);
        }
    }
}