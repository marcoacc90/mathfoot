using UnityEngine;
using System.Collections;

/*!
 * Selection of the mode for the menu.
 */
public class MenuSelector : MonoBehaviour {
	/*!
	 * Mode:
	 * - False -> Game.
	 * - True -> Tutorial.
	 */
	public static bool mode = false;

	void Start () {
		if(mode){
			GetComponent<TutorialScript>().enabled = true;
			GetComponent<MenuScript>().enabled = true;
		}
		else {
			GetComponent<TutorialScript>().enabled = false;
			GetComponent<MenuScript>().enabled = true;
		}
	}
	
	void Update () {

	}
}
