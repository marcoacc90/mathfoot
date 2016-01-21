using UnityEngine;
using System.Collections;

/*!
 * Game tutorial. The application displays the explanation of its elements
 * ![Tutorial] (image/Tutorial.png)
 */
public class GameTutorial : MonoBehaviour {
	public GUIStyle guiLabel;	//!< GUI Style of the text.
	string text;				//!< Text to be shown.
	int count;					//!< Identification of the message.

	void Start () {
		StartCoroutine(DisapearBoxAfter());
	}

	IEnumerator DisapearBoxAfter() { 
		count = 0;
		text = "El objetivo del juego es resolver el problema que aparece en la parte inferior. " +
			      "Debes utilizar la cabeza del jugador para capturar la respuesta.";
		yield return new WaitForSeconds(6.0f);
		count = 1;
		text = "Debes resolver cinco problemas para avanzar de nivel. Tienes 3 minutos para completar las actividades.";
		yield return new WaitForSeconds(6.0f);
		count = 2;
	}

	void OnGUI()
	{
		int wTitle = Screen.width/3;
		guiLabel.fontSize = wTitle/18;
		switch(count)
		{
			case 0:	
				GUI.Label (new Rect (0.55f*Screen.width,0.5f*Screen.height,0.4f*Screen.width,0.3f*Screen.height),text,guiLabel);
				break;
			case 1:	
				GUI.Label (new Rect (0.55f*Screen.width,0.4f*Screen.height,0.4f*Screen.width,0.3f*Screen.height),text,guiLabel);
				break;
			default:	
				break;
		}
	}
}