
using UnityEngine;
using System.Collections;

/*! Check if a ball is in collision with the field in the start scene.
 * ![field](image/field.png)
 */
public class GroundCollisionStart : MonoBehaviour 
{
	public Transform explosion; //!< Explosion animation.

	void OnCollisionEnter(Collision obj_colision) {		
		Destroy(obj_colision.gameObject);
		Instantiate(explosion,obj_colision.transform.position,Quaternion.identity);
	}
}
