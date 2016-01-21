using UnityEngine;
using System.Collections;


/*! Check if the head of a player touches a ball.
 * It checks if the ball is the correct response.
 * ![Head](image/head.png)
 */
public class HeadCollision : MonoBehaviour 
{
	public Transform explosion; 		//!< Effect of the collision.

	void OnCollisionEnter(Collision obj) {
		int result;

		GameControl.CountBalls += 1;
		Destroy(obj.gameObject);
		Instantiate(explosion, obj.transform.position,Quaternion.identity);
		GameControl.usersolution = obj.gameObject.name;
		switch (GameControl.Level) {
		case 0:
			int.TryParse(obj.gameObject.name,out result);
			if(GameControl.TEST == 0 && result%GameControl.ANS == 0 
			   || GameControl.TEST == 1 && result%2 == 0 
			   || GameControl.TEST == 2 && result%2 != 0)
			   GameControl.correct = 1;
			else
				GameControl.correct = 0;
			break;
		default:
			if(GameControl.ANS.ToString() == obj.gameObject.name)
				GameControl.correct = 1;
			else
				GameControl.correct = 0;
			break;
		}
	}
}
