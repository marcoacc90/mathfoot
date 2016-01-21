using UnityEngine;
using System.Collections;

/*! Tutorial mode.
 */
public class TutorialScript : MonoBehaviour {
	public GUIStyle guiLabel;  //!< GUI Style of the text.
	string text;			   //!< Text to be displayed.
	float TIME_MAX = 5.0f;	   //!< Time where the welcome message is displayed, in seconds.
	float MyTime;			   //!< Current time in seconds.
	int count;				   //!< Identification of the message.

	void Start () {
		MyTime = 0.0f;
		count = 0;
	}

	void Update() {
		if(MenuScript.isSelected) {
			count = 2;
			text = "Selecciona el nivel de dificultad del juego.";
		}
		else {
			if(MyTime < TIME_MAX) {
				count = 0;
				text = "¡Bienvenido al tutorial del juego \"MathFoot\"!";
				MyTime += Time.deltaTime;
			}
			else {
				count = 1;
				text = "Selecciona el jugador de tu preferencia, entre nuestros amigos: Juan, Marco, Andrea y Antonio.";
			}
		}
	}

	void OnGUI() {
		int wTitle = Screen.width/3;

		guiLabel.fontSize = wTitle/18;
		switch(count) {
			case 0:
				GUI.Label (new Rect (0.3f*Screen.width,0.63f*Screen.height,0.4f*Screen.width,0.15f*Screen.height),text,guiLabel);
				break;
			case 1:
				GUI.Label (new Rect (0.25f*Screen.width,0.65f*Screen.height,0.5f*Screen.width,0.18f*Screen.height),text,guiLabel);
				break;
			default:	
				if(MenuScript.isSelected)
					GUI.Label (new Rect (0.5f*Screen.width-0.25f*Screen.width,0.62f*Screen.height,0.5f*Screen.width,0.08f*Screen.height),text,guiLabel);
				break;
		}
	}

}