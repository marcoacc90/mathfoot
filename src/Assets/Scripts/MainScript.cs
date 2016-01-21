using UnityEngine;
using System.Collections;

/*! Start scene: Welcome to the game.
 * ![Start](image/Start.png)
 */
public class MainScript : MonoBehaviour {
	public GUIStyle iconInfo;			//!< GUI Style of the credit icon.
	public GUIStyle iconGame;			//!< GUI Style of the game icon.
	public GUIStyle iconExit;			//!< GUI Style of the exit icon.
	public GUIStyle iconTutorial;		//!< GUI Style of the tutorial icon.
	public GUIStyle credit;				//!< GUI Style of the credits.
	public GUIStyle title;				//!< GUI Style of the title.
	bool visible;						//!< Show the credits of the application.
	bool info;							//!< The credit button is pressed.
	float letterPause = 0.05f;			//!< Time in appearing a new word in the credits.
	string message;						//!< Message of the credits.
	string text;						//!< Current text of the credit message.
	
	void Start () {
		info = false;
		message = "\"MathFoot\" \nUniversidad de Guanajuato, DICIS, LaViRIA\n" +
			"Desarrollador:\n" +
			"Marco A. Contreras Cruz\n" +
			"Material Externo:\n" +
			 "- Jugadores por Mixamo\n" + 
			"Audio:\n" +
				"- Youtube Audio Library: Dancing on Green Grass, London Bridge, Space Adventure " +
				" This Old Man por The Green Orbs";
		visible = true;
	}

	IEnumerator TypeText() {
		text = "";
		foreach (char letter in message.ToCharArray()) {
			text += letter;
			if(!info) {
				text = "";
				break;
			}
			yield return new WaitForSeconds (letterPause);
		}
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			info = false;
			visible = true;
		}
	}
	
	void OnGUI() {
		int heightB = Screen.height/9;
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;
		
		title.fontSize = wTitle/5;
		credit.fontSize = wTitle/18;
		GUI.Box(new Rect(Screen.width/2-150,10,300,60)," MathFoot ",title);
		if(GUI.Button (new Rect (Screen.width/4-heightB/2,(5*Screen.height/6)-heightB/2,heightB,heightB),"",iconGame)) {
			MenuSelector.mode = false;
			Application.LoadLevel("Menu");
		}
		if(GUI.Button (new Rect (2*Screen.width/4-heightB/2,(5*Screen.height/6)-heightB/2,heightB,heightB),"",iconTutorial)) {
			MenuSelector.mode = true;
			Application.LoadLevel("Menu");
		}
		if (GUI.Button (new Rect (3*Screen.width/4-heightB/2,(5*Screen.height/6)-heightB/2,heightB,heightB),"",iconExit))
			Application.Quit();
		if(info)
			GUI.Label(new Rect (0.5f*Screen.width-wTitle/2,Screen.height/3-hTitle/2,wTitle,3.2f*hTitle),text,credit); 
		if(visible) {
			if(GUI.Button(new Rect(0.9f*Screen.width,0.1f*Screen.height,wTitle/8,wTitle/8),"",iconInfo)) {
				info = (info)?false:true;
				if(info)
					StartCoroutine(TypeText()); 
			}
		}
	}

}
