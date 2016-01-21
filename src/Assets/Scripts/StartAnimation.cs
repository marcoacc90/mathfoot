using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

/*! Script to animate the main scene, it creates the set of numbered balls at random positions. 
 */
public class StartAnimation : MonoBehaviour 
{
	int speed = 5; 		 	 //!< Speed of the balls.
	int ymin = 2; 		 	 //!< Lower limit in the y-coordinate to generate the balls.
	int ymax = 50; 		 	 //!< Upper limit in the y-coordinate to generate the balls. 
	float xmin = -2.0f; 	 //!< Lower limit in the x-coordinate to generate the balls.
	float xmax = 3.0f; 	 	 //!< Upper limit in the x-coordinate to generate the balls.
	float radius = 0.3f;	 //!< Radius of the balls.
	int MAX_FRAME = 150;	 //!< Generate balls each MAX_FRAMES.
	int frame;				 //!< Current number of frame.
	int N;					 //!< Number of balls to generate.
	
	void Start () {
		frame = MAX_FRAME;
		N = 10;
	}

	void Update () {
		if (frame == MAX_FRAME) {
			frame = 0;
			BallGenerate(N);
			N = 1;
		}
		frame++;		
	}

	void BallGenerate(int N) {
		string Num = "0123456789?";
		int id;
		float x;
		int y;
		string path;

		for(int k = 0; k < N; k++) {
			id = Random.Range(0,Num.Length);
			x = Random.Range(xmin,xmax);
			y = Random.Range(ymin,ymax);
			path = "number/";
			GameObject MySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			MySphere.transform.localScale=new Vector3(radius,radius,radius);
			MySphere.transform.Rotate(new Vector3(0.0f,115.0f,0.0f));
			MySphere.transform.position = new Vector3 (x,y,2.5F);
			MySphere.AddComponent("Rigidbody");
			MySphere.rigidbody.drag=speed;
			MySphere.transform.name=Num[id].ToString();
			path=path+Num[id];
			MySphere.renderer.material.mainTexture = Resources.Load(path) as Texture;
		}
	}

}




	
