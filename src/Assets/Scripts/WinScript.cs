using UnityEngine;
using System.Collections;

/*! This script shows a mathematical phrase to the winner.
 * ![Winner](image/win.png)
 */
public class WinScript : MonoBehaviour {
	public GUIStyle guiButton;	//!< GUI Style of the buttons.
	public GUIStyle guiLabel;	//!< GUI Style of the labels.
	public GUIStyle guiBox;		//!< GUI Style of the boxes.

	ReadConf Data;			//!< Collection of phrases.
	string []Phrase;		//!< Set of phrases.
	int id;					//!< Phrase to be shown.
	

	void Start () {
		Data = new ReadConf("collection");
		Phrase = Data.GetPhrases ();
		id = Random.Range (0, Phrase.Length);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("Game");

	}

	void OnGUI() {	
		int wButton = Screen.width/6;
		int hButton = Screen.height/9;
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;
		
		guiButton.fontSize = wTitle/10;
		guiLabel.fontSize = wTitle/14;
		guiBox.fontSize = wTitle/7;
		GUI.Box(new Rect(0.3f*Screen.width-wTitle/2,hTitle/4,wTitle,hTitle),"¡GANASTE!",guiBox);
		GUI.Label (new Rect (0.05f*Screen.width,Screen.height/6,Screen.width/2,Screen.height/2),Phrase[id]+"\n\n"+
		           "¡Practica continuamente para mejorar tus habilidades matématicas!",guiLabel);
		if(GUI.Button (new Rect (0.3f*Screen.width-1.2f*wButton,(5*Screen.height/6)-hButton/2,wButton,hButton),"Menú",guiButton))
			Application.LoadLevel("Menu");
		if (GUI.Button (new Rect (0.3f*Screen.width+0.2f*wButton,(5*Screen.height/6)-hButton/2,wButton,hButton), "Salir",guiButton))
			Application.Quit();
	}
}
