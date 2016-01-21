using UnityEngine;
using System.Collections;

/*!
 * Interfaz with the mouse for the game object.
 */
public class PlaneInterfaz : MonoBehaviour  {
	public bool OnOver;		//!< The mouse is on over the game object.
	
	void Start () {
		OnOver = false;	
	}

	void OnMouseExit() {
		OnOver = false;
	}
	
	void OnMouseOver() {
		OnOver = true;
	}
}
