
using UnityEngine;
using System.Collections;

/*! Check if a ball is in collision with the field.
 * It increases a counter with the number of balls.
 * ![Field](image/field.png)
 */
public class GroundCollision : MonoBehaviour 
{
	public Transform explosion; //!< Explosion of the ball.

	void OnCollisionEnter(Collision obj) {		
		Destroy(obj.gameObject);
		Instantiate(explosion,obj.transform.position,Quaternion.identity);
		GameControl.CountBalls += 1;		
	}
}
