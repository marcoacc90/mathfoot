using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

/*!
 * - It controls the game.
 * - It creates the numbered balls.
 * - It generates an arithmetic problem.
 * - The player loses if he touches an incorrect ball.
 * ![Game] (image/GameControl.png)
 */
public class GameControl : MonoBehaviour {
	public static int CountBalls = 0;  //!< Current number of balls.
	public static int ANS = 4;  	   //!< Answer to the problem.
	/*!
	 * - Easy (0): characteristics of numbers.
	 * 		- Multiples (0).
	 * 		- Pairs (1).
	 * 		- Odd (2).
	 * - Medium (1): arithmetic problems.
	 * 		- Sums (0).
	 * 		- Subtractions (1).
	 * 		- Multiplications (2).
	 * 		- Divisions (3).
	 * - Difficult (2): complex arithmetic problems.
	 */
	public static int TEST; //!< Kind of question according to the level.
	/*!
	 * 
	 * - Unanswered (-1).
	 * - Incorrect (0).
	 * - Correct (1).
	 */ 
	public static int correct = -1;		//!< Show if an answer was correct.
	/*!
	 *  - Juan (0).
	 *  - Marco (1).
	 *  - Andrea (2).
	 *  - Antonio (3).
	*/
	public static int Player = 3;   	//!< Player id.   
	/*! 
	 * 	- Easy (0).
	 *  - Medium (1).
	 *  - Difficult (2).
	*/
	public static int Level = 2;       	//!< Difficulty of the game.
	public static string usersolution;  //!< Solution to the problem given by the user.
	public static int score = -1;		//!< Score of the user (in seconds).
	public static bool ScoreReady = false; //!< The score is ready.
	public GameObject Player0; 		   //!< 3D model of the player Juan. ![Juan](image/Juan.png)
	public GameObject Player1; 		   //!< 3D model of the player Marco. ![Marco](image/Marco.png)
	public GameObject Player2; 		   //!< 3D model of the player Andrea. ![Andrea](image/Andrea.png)
	public GameObject Player3; 		   //!< 3D model of the player Antonio. ![Antonio](image/Antonio.png)
	public GUIStyle	bPause;				//!< GUI style for the pause button.
	public GUIStyle	bPlay;				//!< GUI style for the play button.
	public GUIStyle bPrevious;			//!< GUI style for the back button.
	public GUIStyle guiProblem;			//!< GUI style for the problem.
	public GUIStyle guiButton;			//!< GUI style for the button with text.
	public GUIStyle guiLabel;			//!< GUI style for the labels.

	/*!
	 * List of the planes with the score. 
	 * When the user completes the level, he can observe, using the pointer, the feedback for each problem.
	 * ![feedback1](image/feedback1.png)
	 * ![feedback2](image/feedback2.png)
	 */ 
	public GameObject []LPlane;	  

	public float TIME_MAX = 300;  //!< The player has five minutes to pass a level.   
	public int PTIME = 60;		  //!< Penalization factor for wrong answer. 

	int MAX_ANS = 5;			  //!< Maximum number of answers.
	float TIME_NEW_BALL = 2.0f;	  //!< Generate balls each TIME_NEW_BALL seconds.
	int SPEED = 10; 			  //!< Speed of the balls.
	float RADIUS = 0.3f; 		  //!< Size of the balls.
	int YMIN = 3; 				  //!< Lower limit of the y-coordinate.
	int YMAX = 10; 				  //!< Upper limit of the y-coordinate.
	float XMIN = -2.0f; 		  //!< Lower limit of the x-coordinate.
	float XMAX = 2.0f; 			  //!< Upper limit of the x-coordinate.
	int N;					 	  //!< Number of balls to generate.
	int CountANS;			 	  //!< Current number of answers.
	float MyTime;				  //!< Current time in seconds.
	float TimeNewBall;	  		  //!< Current time used to generate a new ball.
	/*!
	 * Validate if the questions are correct: 
	 * - Unanswered (-1).
	 * - Incorrect (0).
	 * - Correct (1).
	 */
	int[]validate;				  
	bool pause; 				  //!< Pause the game.
	string PROBLEM = "2+2"; 	  //!< Arithmetic problem.
	int NC = 0;					  //!< Number of correct answers. 
	bool AdvanceLevel;			  //!< The player advanced the level.
	bool FeedBack;				  //!< The player does not advance the level.
	float Tg;					  //!< Time used by the player.
	string []LPROBLEM;			  //!< List of problems.
	string []LANS;				  //!< List of answers.
	string []LUANS;				  //!< List of user answers.
	GameObject[] LBALL;			  //!< List of balls.

	void DestroyAllBall() {
		for(int i = 0; i < LBALL.Length; i++)
			Destroy (LBALL[i]);
	}

	void Start () {
		ScoreReady = false;
		AdvanceLevel = false;
		FeedBack = false;
		Tg = 0.0f;
		NC = 0;
		correct = -1;
		N = 6;
		CountANS = 0;
		MyTime = 0.0f;
		TimeNewBall = TIME_NEW_BALL;
		PROBLEM = Problem(Level);
		validate = new int[MAX_ANS];
		for(int i = 0; i < MAX_ANS; i++)
			validate[i] = -1;

		LPROBLEM = new string[MAX_ANS];
		LANS = new string[MAX_ANS];
		LUANS = new string[MAX_ANS];
		LPROBLEM [CountANS] = PROBLEM;
		LANS [CountANS] = ANS.ToString ();
		LBALL = new GameObject[N];

		score = -1;

		switch (Player) {
		case 0:
			Player0.SetActive(true);
			Player1.SetActive(false);
			Player2.SetActive(false);
			Player3.SetActive(false);
			break;
		case 1:
			Player0.SetActive(false);
			Player1.SetActive(true);
			Player2.SetActive(false);
			Player3.SetActive(false);
			break;
		case 2:
			Player0.SetActive(false);
			Player1.SetActive(false);
			Player2.SetActive(true);
			Player3.SetActive(false);
			break;
		default:
			Player0.SetActive(false);
			Player1.SetActive(false);
			Player2.SetActive(false);
			Player3.SetActive(true);
			break;
		}


	}

	void showScore() {
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;

		score = (MAX_ANS - NC) * PTIME + (int)MyTime;
		ScoreReady = true;
		if (ScoreScript.show)
			return;
		GUI.Label(new Rect(Screen.width/2-0.7f*wTitle,Screen.height/2-1.8f*hTitle,1.4f*wTitle,1.8f*hTitle),"MARCADOR\n" + "Respuestas incorrectas: "+ (MAX_ANS-NC) + "x"+PTIME+"="+((MAX_ANS-NC)*PTIME)+" s\n"+
		          "Tiempo: " + (int)MyTime +" s\n" +
		          "Total: "+ score +" s",guiLabel);
	}

	void DrawScore(){
		for(int i = 0; i < MAX_ANS; i++) {
			if(validate[i] == 1)
				LPlane[i].renderer.material.mainTexture = Resources.Load("OnGUI/Correct",typeof(Texture2D)) as Texture2D;
			else if(validate[i] == 0)
				LPlane[i].renderer.material.mainTexture = Resources.Load("OnGUI/Wrong",typeof(Texture2D)) as Texture2D;
			else
				LPlane[i].renderer.material.mainTexture = Resources.Load("OnGUI/Interrogation",typeof(Texture2D)) as Texture2D;
		}
	}

	bool IsWinner() {
		for(int i = 0; i < MAX_ANS; i++) {
			if(validate[i] == 0 || validate[i] == -1)
				return false;
		}

		return true;
	}

	void ResetProblem() {
		NC = 0;
		correct = -1;
		MyTime = 0.0f;
		Tg = 0.0f;
		CountBalls = 0;
		CountANS = 0;
		PROBLEM = Problem(Level);
		LPROBLEM [CountANS] = PROBLEM;
		LANS [CountANS] = ANS.ToString ();
		score = -1;
		ScoreReady = false;
		for(int i = 0; i < MAX_ANS; i++)
			validate[i] = -1;
		Time.timeScale = 1;
		FeedBack = false;
		AdvanceLevel = false;
	}
	
	void BallGenerate() {
		int id;
		float RandX;
		int RandY;
		string path;
		float prob;

		for(int k = 0; k < N; k++) {
			if(LBALL[k] != null)
				continue;
			if(Level > 0) {
				prob = Random.Range(0.0f,1.0f);
				if(prob <= 0.3f)
					id = ANS;
				else
					id = Random.Range(0,100);
			}
			else
				id = Random.Range(0,100);
			RandX = Random.Range(XMIN,XMAX);
			RandY = Random.Range(YMIN,YMAX);
			path = "number/";
			LBALL[k] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			LBALL[k].transform.localScale = new Vector3(RADIUS,RADIUS,RADIUS);
			LBALL[k].transform.Rotate(new Vector3(0.0f,115.0f,0.0f));
			LBALL[k].transform.position = new Vector3 (RandX, RandY,2.5F);
			LBALL[k].AddComponent("Rigidbody");
			LBALL[k].rigidbody.drag = SPEED;
			LBALL[k].transform.name = id.ToString();
			path=path+id.ToString();
			LBALL[k].renderer.material.mainTexture = Resources.Load(path) as Texture;
		}
	}


	string Problem(int l) {
		string p;

		switch(l) {
		case 0:
			p = ProblemEasy();
			break;
		case 1:
			p = ProblemMedium();
			break;
		default:
			p = ProblemDificult();
			break;
		}

		return p;
	}
	
	string ProblemEasy() {
		string op = "";

		TEST = Random.Range (0, 3);
		switch(TEST) {
		case 0:				
			op = "MULTIPLO DEL ";
			ANS = Random.Range(2,10);
			break;			
		case 1:				
			op = "NUMERO PAR";
			break;
		default:
			op = "NUMERO IMPAR";
			break;
		}

		return (TEST == 0)?op + ANS.ToString():op;
	}

	string ProblemMedium() {
		int a;
		int b;
		string op = "";

		TEST = Random.Range (0, 4);
		switch(TEST){
		case 0:
			a = Random.Range(0,10);
			b = Random.Range(0,10);
			ANS = a+b;
			op = "+";	
			break;
		case 1:
			do {
				a = Random.Range(0,10);
				b = Random.Range(0,10);
			} while(a < b);
			ANS = a-b;
			op = "-";	
			break;
		case 2:
			a = Random.Range(0,9);
			b = Random.Range(0,9);
			ANS = a*b;
			op = "x";	
			break;
		default:
			do {
				a = Random.Range(0,10);
				b = Random.Range(1,10);
			} while(a%b != 0 || a/b > 10);
			ANS = a/b;
			op = "/";	
			break;
		}
		return a.ToString() + op + b.ToString();
	}

	string ProblemDificult() {
		string A,B;
		string op = "";
		int rA, rB;

		TEST = Random.Range (0, 4);
		switch(TEST){
		case 0:
			do {
			A = ProblemMedium(); rA = ANS;
			B = ProblemMedium(); rB = ANS;
			}while(rA+rB > 99);
			op = "+";
			ANS = rA+rB;
			break;
		case 1:
			do {
				A = ProblemMedium(); rA = ANS;
				B = ProblemMedium(); rB = ANS;
			}while(rA < rB || rA-rB > 99);
			ANS = rA-rB;
			op = "-";	
			break;
		case 2:
			do {
				A = ProblemMedium(); rA = ANS;
				B = ProblemMedium(); rB = ANS;
			}while(rA*rB > 99);
			ANS = rA*rB;
			op = "x";	
			break;
		default:
			do {
				A = ProblemMedium(); rA = ANS;
				do {
				B = ProblemMedium(); rB = ANS;
				}while(rB == 0);
			} while(rA%rB != 0 || rA/rB > 99);
			ANS = rA/rB;
			op = "/";	
			break;
		}

		return "(" +A+ ")"+op+"("+B+")";
	}
	
	void Update () {
		MyTime += Time.deltaTime;
		Tg += Time.deltaTime;
		TimeNewBall += Time.deltaTime;
		
		if (TimeNewBall >= TIME_NEW_BALL) {
			TimeNewBall = 0.0f;
			BallGenerate();
		}
		if(CountANS == MAX_ANS) {
			if(IsWinner())
				AdvanceLevel = true;
			else
				FeedBack = true;			
			Time.timeScale = 0;
			CountANS = 0;
			correct = -1;
		}
		
		switch(correct) {
		case 0:
			DestroyAllBall();
			if(CountANS < MAX_ANS) {
				LUANS[CountANS] = usersolution;
				validate[CountANS] = 0;
				CountANS++;
				if(CountANS < MAX_ANS) {
					LPROBLEM [CountANS] = PROBLEM;
					LANS [CountANS] = ANS.ToString ();
				}
			}
			correct = -1;
			break;
		case 1:
			DestroyAllBall();
			if(CountANS < MAX_ANS) {
				LUANS[CountANS] = usersolution;
				validate[CountANS] = 1;
				PROBLEM = Problem(Level);
				CountANS++;
				if(CountANS < MAX_ANS) {
					LPROBLEM [CountANS] = PROBLEM;
					LANS [CountANS] = ANS.ToString ();
				}
				NC++;
			}
			correct = -1;
			break;
		default:
			break;
		}
		DrawScore();
	}
	
	void OnGUI() {
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;
		float wBox = 0.05f*Screen.width; 
		float hBox = 0.09f*Screen.height;
		
		guiProblem.fontSize = wTitle/7;
		guiButton.fontSize = wTitle / 10;
		guiLabel.fontSize = wTitle / 13;
		string Texto="Balones: "+CountBalls+ "\nTiempo: "+string.Format("{0:0.#} s",TIME_MAX-MyTime);
		GUI.Label(new Rect (wTitle/10,wTitle/10,0.7f*wTitle,1.1f*hTitle),Texto,guiLabel);
		for (int i=0; i <PROBLEM.Length; i++) 
			GUI.Box (new Rect (wBox*i+(Screen.width/2-wBox*PROBLEM.Length/2),0.95f*Screen.height-hBox,wBox,hBox),PROBLEM[i].ToString(),guiProblem);
		if(pause) {
			if(!AdvanceLevel && !FeedBack) {
				GUI.Label(new Rect(Screen.width/2-wTitle/2,Screen.height/2-hTitle/2,wTitle,hTitle),"JUEGO EN PAUSA",guiLabel);
				if(GUI.Button (new Rect(wTitle/10,0.3f*Screen.height+1.4f*wTitle/8,wTitle/8,wTitle/8),"",bPlay)) {
					pause = false;
					Time.timeScale = 1;
				}
			}
		}
		else {
			if(!AdvanceLevel && !FeedBack) {
				if(GUI.Button (new Rect(wTitle/10,0.3f*Screen.height+1.4f*wTitle/8,wTitle/8,wTitle/8),"",bPause)) {
					pause = true;
					Time.timeScale = 0;
				}
			}
		}
		if(GUI.Button (new Rect(wTitle/10,0.3f*Screen.height,wTitle/8,wTitle/8),"",bPrevious)) {
			Time.timeScale = 1;
			Application.LoadLevel("Menu");
		}
		if(AdvanceLevel) {
			showScore();
			if(GUI.Button(new Rect(Screen.width/2-0.65f*wTitle,0.65f*Screen.height,0.6f*wTitle,0.8f*hTitle),"SIGUIENTE",guiButton)) {
				Level++;
				if(Level > 2 && NC == MAX_ANS) {
					Time.timeScale = 1;
					Application.LoadLevel("Win");
				}
				else
					ResetProblem();
			}
		}
		if(FeedBack) {
			showScore();
			if(GUI.Button(new Rect(Screen.width/2-0.65f*wTitle,0.65f*Screen.height,0.6f*wTitle,0.8f*hTitle),"REINICIAR",guiButton))
				ResetProblem();
		}

		if(AdvanceLevel || FeedBack) {
			int plane = -1;
			string [] msg;

			msg = new string[MAX_ANS];
			for(int i = 0; i < LPlane.Length; i++) {
				if(LPlane[i].GetComponent<PlaneInterfaz>( ).OnOver)
					plane = i;
			}
			for(int i = 0; i < MAX_ANS; i++) {
				if(validate[i] == 1)
					msg[i] = "Correcto ";
				if(Level == 0) {
					if(validate[i] == 1) {
						msg[i] += "el ";
						msg[i] += LUANS[i];
						msg[i] += " es ";
					}
					else {
						msg[i] += "El ";
						msg[i] += LUANS[i];
						msg[i] += " no es ";
					}
					msg[i] += LPROBLEM[i];
				}
				else {
					if(validate[i] == 1)
						msg[i] += (LPROBLEM[i] + " es igual a " + LANS[i]);
					else {
						msg[i] += (LPROBLEM[i] + " no es igual a " + LUANS[i]);
						msg[i] += "\nLa respuesta es " + LANS[i]; 
					}
				}
			}

			if(plane != -1)
				GUI.Label(new Rect (0.75f*Screen.width,0.2f*Screen.height,0.24f*Screen.width,0.2f*Screen.height),msg[plane],guiLabel);
		}
	}

}
