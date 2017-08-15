using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
	private bool showCredits = false;
	public GUISkin blueSkin;
	public GUISkin greenSkin;
	public GUISkin redSkin;
	public GUISkin blackSkin;
	public GUISkin whiteSkin;
	public GameObject heads;
	
	private string headerText = "";
	private bool playMusic = true;
	public AudioSource music;
	
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("GUI") == "") { PlayerPrefs.SetString ("GUI", "Blue"); }
		if (PlayerPrefs.GetString ("Music") == "Off") { playMusic = false; music.Pause (); }
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
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
		GUI.Label (new Rect(Screen.width / 2 - 100, 5, 200, 20), "Got time for that?", centeredStyle);
		
		// Menu Box
		int boxSizeX = Screen.width;
		int boxSizeY = Screen.height;
		int boxPosX = Screen.width / 2 - boxSizeX / 2;
		int boxPosY = Screen.height / 20 * 2;
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), headerText);
	
		// Music Button
		if (GUI.Button (new Rect(Screen.width - 121, 1, 120, 30), "Toggle Music")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.M)) { // 'M' hotkey
			if (playMusic) {
				music.Pause();
				playMusic = false;
				PlayerPrefs.SetString ("Music", "Off");
			}
			else {
				music.Play ();
				playMusic = true;
				PlayerPrefs.SetString ("Music", "On");
			}
		}
		
		// Quit Button		
		if (GUI.Button (new Rect (1, 1, 60, 30), "Quit")) {
			Application.Quit();
		}
		
		int playButtonSizeX = Screen.width / 2;
		int playButtonSizeY = Screen.height / 8;
		int playButtonPadding = 15;
		int playButtonPosX = Screen.width / 2 - playButtonSizeX / 2;
		int playButtonPosY = Screen.height / 2;
		
		GUIStyle playButtonStyle = new GUIStyle(GUI.skin.button);
		playButtonStyle.fontSize = 16;
		
		if (!showCredits) {
			headerText = "Select a game";
			heads.SetActive (true); // show the head banger and shaker
			// Play Game 1		
			if (GUI.Button (new Rect (playButtonPosX, playButtonPosY - (playButtonSizeY + playButtonPadding), playButtonSizeX, playButtonSizeY), "10 Second Solutions", playButtonStyle)) {
				Application.LoadLevel("SolutionsGame");
			}
			// Play Game 2
			if (GUI.Button (new Rect (playButtonPosX, playButtonPosY, playButtonSizeX, playButtonSizeY), "10 Second Button Masher", playButtonStyle)) {
				Application.LoadLevel("MasherGame");
			}
			// Play 3 Button		
			if (GUI.Button (new Rect (playButtonPosX, playButtonPosY + (playButtonSizeY + playButtonPadding), playButtonSizeX, playButtonSizeY), "10 Seconds to Score", playButtonStyle)) {
				Application.LoadLevel("GotTimeGame");
			}
		}
		
		// Color Button
		if (GUI.Button (new Rect(Screen.width - 121, Screen.height - 31, 120, 30), PlayerPrefs.GetString ("GUI"))) {
			if (PlayerPrefs.GetString ("GUI") == "Blue") {
				PlayerPrefs.SetString ("GUI", "Green");
			}
			else if (PlayerPrefs.GetString ("GUI") == "Green") {
				PlayerPrefs.SetString ("GUI", "Red");
			}
			else if (PlayerPrefs.GetString ("GUI") == "Red") {
				PlayerPrefs.SetString ("GUI", "Black");
			}
			else if (PlayerPrefs.GetString ("GUI") == "Black") {
				PlayerPrefs.SetString ("GUI", "White");
			}
			else if (PlayerPrefs.GetString ("GUI") == "White") {
				PlayerPrefs.SetString ("GUI", "Blue");
			}
		}
		
		// Credits Button		
		if (GUI.Button (new Rect (0, Screen.height - 31, 120, 30), "Credits")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.C)) { // 'C' hotkey
			if (showCredits == false) { showCredits = true; }
			else { showCredits = false; }
		}
		
		if (showCredits == true) {
			headerText = "Credits:";
			heads.SetActive (false);
			
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 - 110, 480, 40), "\"Journey: Don't Stop Believin' (8-bit version)\" by AinSophAur33, MediaFire", centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 - 75, 480, 30), "\"Daft Punk - Get Lucky 8 Bit\" by FloatingPoint, NewGrounds" , centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 - 50, 480, 30), "\"8 bit Rickroll\" by PastorOfMuppetsFilms, MediaFire" , centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 - 25, 480, 30), "\"Sweet Brown\" by Raymony, SoundCloud" , centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 - 0, 480, 30), "\"Deep Emerald\" from PlayOnLoop" , centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 + 25, 480, 30), "\"Light GUI Skin - 5 pack by 3DConstruct" , centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 240, Screen.height / 2 + 50, 480, 30), "Ludum Dare 27 Coding/Production: AnachronicDesigns", centeredStyle);
			if (GUI.Button (new Rect (Screen.width / 2 - 50, Screen.height / 2 + 100, 100, 30), "Continue")
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'Return' hotkey
				showCredits = false;
			}
		}
		
	}
}
