using UnityEngine;
using System.Collections;

/*!
 * Show a register with the best scores per level. 
 * ![score](image/score.png)
 */
public class ScoreScript : MonoBehaviour {
	public GUIStyle guiLabel;				//!< GUI Style of the labels.
	public GUIStyle guiLabel2;				//!< GUI Style of the labels.
	public GUIStyle guiButton;				//!< GUI Style of the buttons.
	public static bool show = false;		//!< Show the score.

	public int NR = 5; 		//!< Number of elements of the register (top elements per level).
	string []Name; 			//!< Names of the avatars.
	bool flag;   			//!< Flag used to update the score.
	bool update; 			//!< The register was updated.
	int id;					 //!< Position of the new score. 
	
	void DeleteScore() {
		for(int i = 0; i < NR; i++) {
			if(PlayerPrefs.HasKey(i+"EScore"))
				PlayerPrefs.DeleteKey(i+"EScore");
			if(PlayerPrefs.HasKey(i+"EScoreName"))
				PlayerPrefs.DeleteKey(i+"EScoreName");
			if(PlayerPrefs.HasKey(i+"MScore"))
				PlayerPrefs.DeleteKey(i+"MScore");
			if(PlayerPrefs.HasKey(i+"MScoreName"))
				PlayerPrefs.DeleteKey(i+"MScoreName");
			if(PlayerPrefs.HasKey(i+"DScore"))
				PlayerPrefs.DeleteKey(i+"DScore");
			if(PlayerPrefs.HasKey(i+"DScoreName"))
				PlayerPrefs.DeleteKey(i+"DScoreName");
		}
	}

	void CreateScore() {
		int value = -1;
	
		for(int i = 0; i < NR; i++) {
			//!< Create the register for the easy level
			if(!PlayerPrefs.HasKey(i+"EScore"))
				PlayerPrefs.SetInt(i+"EScore",value);
			if(!PlayerPrefs.HasKey(i+"EScoreName"))
				PlayerPrefs.SetString(i+"EScoreName","");
			//!< Create the register for the medium level
			if(!PlayerPrefs.HasKey(i+"MScore"))
				PlayerPrefs.SetInt(i+"MScore",value);
			if(!PlayerPrefs.HasKey(i+"MScoreName"))
				PlayerPrefs.SetString(i+"MScoreName","");
			//!< Create the register for the difficult level
			if(!PlayerPrefs.HasKey(i+"DScore"))
				PlayerPrefs.SetInt(i+"DScore",value);
			if(!PlayerPrefs.HasKey(i+"DScoreName"))
				PlayerPrefs.SetString(i+"DScoreName","");
		}
	}

	string GetLevel(int l) {
		string slev;

		switch(l) {
		case 0:
			slev = "EScore";
			break;
		case 1:
			slev = "MScore";
			break;
		default:
			slev = "DScore";
			break;
		}

		return slev;
	}

	void UpdateScore(int level, string user, int score) {
		int pscore,nscore;
		string pname,nname;
		string slevel;
		update = false;

		slevel = GetLevel (level);
		for(int i = 0; i < NR; i++) {
			if(PlayerPrefs.GetInt(i+slevel) == -1) {
				PlayerPrefs.SetInt(i+slevel,score);
				PlayerPrefs.SetString(i+slevel+"Name",user);
				update = true;
				id = i+1;
				break;
			}
			else {
				if(score < PlayerPrefs.GetInt(i+slevel)) {
					pscore = PlayerPrefs.GetInt(i+slevel);
					pname = PlayerPrefs.GetString(i+slevel+"Name");
					for(int j=i+1; j < NR; j++) {
						if(pscore == -1)
							break;
						nscore = PlayerPrefs.GetInt(j+slevel);
						nname = PlayerPrefs.GetString(j+slevel+"Name");
						PlayerPrefs.SetInt(j+slevel,pscore);
						PlayerPrefs.SetString(j+slevel+"Name",pname);
						pscore = nscore;
						pname = nname;
					}
					PlayerPrefs.SetInt(i+slevel,score);
					PlayerPrefs.SetString(i+slevel+"Name",user);
					update = true;
					id = i+1;
					break;
				}
			}
		}
	}

	void Start () {
		Name = new string[4];
		Name[0] = "Juan";
		Name[1] = "Marco";
		Name[2] = "Andrea";
		Name[3] = "Antonio";
		//DeleteScore ();
		CreateScore();
		update = false;
		flag = false;
		show = false;
	}
	
	void Update () {
		if(!GameControl.ScoreReady) {
			flag = false;
			show = false;
		}
		if(!flag && GameControl.ScoreReady) {
			UpdateScore(GameControl.Level,Name[GameControl.Player],GameControl.score);
			flag = true;
		}
	}

	void OnGUI() {
		int wTitle = Screen.width/3;
		int hTitle = Screen.height/8;
		string cmd;

		guiButton.fontSize = wTitle / 10;

		switch(GameControl.Level) {
		case 0:
			cmd = "Fácil";
			break;
		case 1:
			cmd = "Media";
			break;
		default:
			cmd = "Difícil";
			break;
		}
		cmd = "TOP 5" + " ("+cmd+") \n"; 
		for(int i = 0; i < NR; i++) {
			if(PlayerPrefs.GetInt(i+GetLevel(GameControl.Level)) == -1)
			   break;
			cmd += ((i+1)+") " + PlayerPrefs.GetString(i+GetLevel(GameControl.Level)+"Name") + 
				" "+ PlayerPrefs.GetInt(i+GetLevel(GameControl.Level)));
			if(i < NR-1)
				cmd += "\n";
		}
		guiLabel.fontSize = wTitle/15;
		guiLabel2.fontSize = wTitle/15;
		if(GameControl.ScoreReady) {
			if(GUI.Button(new Rect(0.52f*Screen.width,0.65f*Screen.height,0.6f*wTitle,0.8f*hTitle),"TOP 5",guiButton))
				show = !show;
		}
		if(show)
			GUI.Label(new Rect(Screen.width/2-0.35f*wTitle,Screen.height/2-2.0f*hTitle,0.7f*wTitle,2.0f*hTitle),cmd,guiLabel);
		if(GameControl.ScoreReady && update)
			GUI.Label(new Rect(Screen.width/2-0.7f*wTitle,0.525f*Screen.height,1.4f*wTitle,0.8f*hTitle),
			          "¡Felicidades tu marcador está en el No. " + id+"!",guiLabel2);
	}
}
