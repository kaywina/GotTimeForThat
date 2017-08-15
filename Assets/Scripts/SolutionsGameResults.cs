using UnityEngine;
using System.Collections;

public class SolutionsGameResults : MonoBehaviour {
	
	public GUISkin blueSkin;
	public GUISkin greenSkin;
	public GUISkin redSkin;
	public GUISkin blackSkin;
	public GUISkin whiteSkin;
	
	private string rank = "";
	public AudioSource music;
	
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("Music") == "Off") { music.Pause (); }
		if (PlayerPrefs.GetInt ("Level") == 1) { rank = "Newb"; }
		else if (PlayerPrefs.GetInt ("Level") == 2) { rank = "Rookie"; }
		else if (PlayerPrefs.GetInt ("Level") == 3) { rank = "Learner"; }
		else if (PlayerPrefs.GetInt ("Level") == 4) { rank = "Blaster"; }
		else if (PlayerPrefs.GetInt ("Level") == 5) { rank = "Cruncher"; }
		else if (PlayerPrefs.GetInt ("Level") == 6) { rank = "Muncher"; }
		else if (PlayerPrefs.GetInt ("Level") == 7) { rank = "Junior"; }
		else if (PlayerPrefs.GetInt ("Level") == 8) { rank = "Sophomore"; }
		else if (PlayerPrefs.GetInt ("Level") == 9) { rank = "Senior"; }
		else if (PlayerPrefs.GetInt ("Level") == 10) { rank = "Expert"; }
		else if (PlayerPrefs.GetInt ("Level") == 11) { rank = "Pro"; }
		else if (PlayerPrefs.GetInt ("Level") == 12) { rank = "Savant"; }
		else if (PlayerPrefs.GetInt ("Level") == 13) { rank = "Genius"; }
		else if (PlayerPrefs.GetInt ("Level") == 14) { rank = "Master"; }
		else if (PlayerPrefs.GetInt ("Level") >= 15) { rank = "Marvel"; }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
	
	void OnGUI() {
		
		if (PlayerPrefs.GetString ("GUI") == "Blue") { GUI.skin = blueSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Green") { GUI.skin = greenSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Red") { GUI.skin = redSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Black") { GUI.skin = blackSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "White") { GUI.skin = whiteSkin; }
		
		var e = Event.current; // Used to get keyboard inputs
		// GUI Style
		GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
		centeredStyle.alignment = TextAnchor.MiddleCenter;
		centeredStyle.fontStyle = FontStyle.Bold;
		centeredStyle.fontSize = 16;
		centeredStyle.normal.textColor = Color.white;
		
		// Title
		GUI.Label (new Rect(Screen.width / 2 - 100, 5, 200, 20), "10 Second Solutions", centeredStyle);
		
		// Menu Box
		int boxSizeX = Screen.width / 20 * 16;
		int boxSizeY = Screen.height / 20 * 16;
		int boxPosX = Screen.width / 2 - boxSizeX / 2;
		int boxPosY = Screen.height / 20 * 2;
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), "OMG you didn't press the button");
		
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 75, 400, 30), "Congratulations!", centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 30), "You scored: " + PlayerPrefs.GetInt ("Score").ToString (), centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 25, 400, 30), "You made it to level: " + PlayerPrefs.GetInt ("Level").ToString (), centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 0, 400, 30), "You are a Math " + rank, centeredStyle);
		
		// Restart Button
		if (GUI.Button (new Rect(Screen.width / 2 - 100, Screen.height / 2 + 35, 200, 50), "Play Again")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'C' hotkey
			Application.LoadLevel ("SolutionsGame");
		}
	
		// Quit Button		
		if (GUI.Button (new Rect (1, 1, 60, 30), "Back")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
