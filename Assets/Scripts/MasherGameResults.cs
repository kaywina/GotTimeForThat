using UnityEngine;
using System.Collections;

public class MasherGameResults : MonoBehaviour {

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
		if (PlayerPrefs.GetInt ("Score") <= 10) { rank = "Newb"; }
		else if (PlayerPrefs.GetInt ("Score") <= 25) { rank = "Rookie"; }
		else if (PlayerPrefs.GetInt ("Score") <= 50) { rank = "Learner"; }
		else if (PlayerPrefs.GetInt ("Score") <= 100) { rank = "Blaster"; }
		else if (PlayerPrefs.GetInt ("Score") <= 150) { rank = "Cruncher"; }
		else if (PlayerPrefs.GetInt ("Score") <= 200) { rank = "Muncher"; }
		else if (PlayerPrefs.GetInt ("Score") <= 300) { rank = "Junior"; }
		else if (PlayerPrefs.GetInt ("Score") <= 400) { rank = "Sophomore"; }
		else if (PlayerPrefs.GetInt ("Score") <= 500) { rank = "Senior"; }
		else if (PlayerPrefs.GetInt ("Score") <= 600) { rank = "Expert"; }
		else if (PlayerPrefs.GetInt ("Score") <= 700) { rank = "Pro"; }
		else if (PlayerPrefs.GetInt ("Score") <= 800) { rank = "Savant"; }
		else if (PlayerPrefs.GetInt ("Score") <= 900) { rank = "Genius"; }
		else if (PlayerPrefs.GetInt ("Score") <= 1000) { rank = "Master"; }
		else if (PlayerPrefs.GetInt ("Score") > 1000) { rank = "Marvel"; }
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
		GUI.Label (new Rect(Screen.width / 2 - 100, 5, 200, 20), "10 Second Button Masher", centeredStyle);
		
		// Menu Box
		int boxSizeX = Screen.width / 20 * 16;
		int boxSizeY = Screen.height / 20 * 16;
		int boxPosX = Screen.width / 2 - boxSizeX / 2;
		int boxPosY = Screen.height / 20 * 2;
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), "WOW you press a lot of buttons");
		
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 75, 400, 30), "Congratulations!", centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 30), "You scored: " + PlayerPrefs.GetInt ("Score").ToString (), centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 25, 400, 30), "You pressed: " + PlayerPrefs.GetInt ("LastPresses").ToString () + " buttons", centeredStyle);
		GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 0, 400, 30), "You are a Button Mashing " + rank, centeredStyle);
		
		// Restart Button
		if (GUI.Button (new Rect(Screen.width / 2 - 100, Screen.height / 2 + 35, 200, 50), "Play Again")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'C' hotkey
			Application.LoadLevel ("MasherGame");
		}
	
		// Quit Button		
		if (GUI.Button (new Rect (1, 1, 60, 30), "Back")) {
			Application.LoadLevel("MainMenu");
		}
	}
}
