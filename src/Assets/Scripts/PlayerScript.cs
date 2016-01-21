using UnityEngine;
using System.Collections;

/*! Selection of the player.
 * ![players] (image/players.png)
 */
public class PlayerScript : MonoBehaviour {
	public GameObject BasePlayer0; //!< Base of the player Juan. 
	public GameObject BasePlayer1; //!< Base of the player Marco. 
	public GameObject BasePlayer2; //!< Base of the player Andrea. 
	public GameObject BasePlayer3; //!< Base of the player Antonio.

	void Start () {
	
	}
	
	void Update () {

	}

	void OnMouseDown() {
		MenuScript.isSelected = true;
		if(gameObject.name == "Player0")
		{
			GameControl.Player = 0;
			BasePlayer0.renderer.material.color = new Color(0,0,180.0f/255.0f);
			BasePlayer1.renderer.material.color = Color.gray;
			BasePlayer2.renderer.material.color = Color.gray;
			BasePlayer3.renderer.material.color = Color.gray;
		}
		if(gameObject.name == "Player1")
		{
			GameControl.Player = 1;
			BasePlayer0.renderer.material.color = Color.gray;
			BasePlayer1.renderer.material.color = new Color(0,0,180.0f/255.0f);
			BasePlayer2.renderer.material.color = Color.gray;
			BasePlayer3.renderer.material.color = Color.gray;
		}
		if(gameObject.name == "Player2")
		{
			GameControl.Player = 2;
			BasePlayer0.renderer.material.color = Color.gray;
			BasePlayer1.renderer.material.color = Color.gray;
			BasePlayer2.renderer.material.color = new Color(0,0,180.0f/255.0f);
			BasePlayer3.renderer.material.color = Color.gray;
		}
		if(gameObject.name == "Player3")
		{
			GameControl.Player = 3;
			BasePlayer0.renderer.material.color = Color.gray;
			BasePlayer1.renderer.material.color = Color.gray;
			BasePlayer2.renderer.material.color = Color.gray;
			BasePlayer3.renderer.material.color = new Color(0,0,180.0f/255.0f);
		}
	}
}
