using UnityEngine;
using System.Collections;

/*!  Selection of the player and the level of the game.
 * ![Menu](image/Menu.png)
 */
public class MenuScript : MonoBehaviour 
{	
	public static bool isSelected;    //!< The player is selected.
	public GUIStyle button;		   	  //!< GUI Style of the buttons.
	public GUIStyle title;		   	  //!< GUI Style of the title.
	public GUIStyle bBack;			  //!< GUI Style of the button to return.

	void Start ()  {
		GameControl.Player = 0;
		isSelected = false;
		GameSelector.mode = MenuSelector.mode;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel("Start");
	}
	
	void OnGUI() {
		int wButton = Screen.width/6;
		int hButton = Screen.height/9;
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;

		button.fontSize = wTitle/10;
		title.fontSize = wTitle/7;
		GUI.Label(new Rect(Screen.width/2-wTitle/2,hTitle/50,wTitle,hTitle),"Selecciona tu avatar",title);
		if (isSelected) {
			GUI.Label(new Rect(Screen.width/2-wTitle/2,0.7f*Screen.height,wTitle,hTitle),"Dificultad",title);
			if (GUI.Button (new Rect (Screen.width/4-wButton/2, (5 * Screen.height / 6),wButton,hButton), "Fácil",button)) {
				GameControl.Level = 0;
				Application.LoadLevel("Game");
			}
			if (GUI.Button (new Rect (Screen.width/2-wButton/2,(5 * Screen.height / 6),wButton,hButton), "Media",button)) {
				GameControl.Level = 1;
				Application.LoadLevel("Game");

			}
			if (GUI.Button (new Rect (3*Screen.width/4-wButton/2,(5 * Screen.height / 6),wButton,hButton), "Difícil",button)) {
				GameControl.Level = 2;
				Application.LoadLevel("Game");
			}
		}
		if(GUI.Button (new Rect(0.89f*Screen.width,0.1f*Screen.height,wTitle/8,wTitle/8),"",bBack))
			Application.LoadLevel("Start");
	}
}
