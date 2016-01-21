using UnityEngine;
using System.Collections;

/*! Check if the head of the Avatar is in collision with a ball in the start scene.
 * ![Head](image/head.png)
 */
public class HeadCollisionStart : MonoBehaviour 
{
	public Transform explosion; //!< Explosion animation. 

	void OnCollisionEnter(Collision obj_colision) {
		Destroy(obj_colision.gameObject);
		Instantiate(explosion,obj_colision.transform.position,Quaternion.identity);
	}
}
