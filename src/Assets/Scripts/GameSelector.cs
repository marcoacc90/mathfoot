using UnityEngine;
using System.Collections;

/*!
 * Selection of the mode for the game.
 */
public class GameSelector : MonoBehaviour {
	/*!
	 * Mode:
	 * - False -> Game.
	 * - True  -> Tutorial.
	 */
	public static bool mode = false;

	void Start () {
		if(mode){
			GetComponent<GameControl>().enabled = true;
			GetComponent<GameTutorial>().enabled = true;
		}
		else {
			GetComponent<GameControl>().enabled = true;
			GetComponent<GameTutorial>().enabled = false;
		}
	}
	
	void Update () {

	}
}
