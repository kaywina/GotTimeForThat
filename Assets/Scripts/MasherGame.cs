using UnityEngine;
using System.Collections;

public class MasherGame : MonoBehaviour {
	
	public GUISkin blueSkin;
	public GUISkin greenSkin;
	public GUISkin redSkin;
	public GUISkin blackSkin;
	public GUISkin whiteSkin;
	
	public AudioSource music;
	private bool playMusic = true;
	private int score = 0;
	private int scoreModifier = 0;
	private int presses;
	
	private Timer time;
	
	int boxSizeX = 0;
	int boxSizeY = 0;
	int boxPosX = 0;
	int boxPosY = 0;
	
	int randomButtonSizeX = 0;
	int randomButtonSizeY = 0;
	int randomButtonPosX = 0;
	int randomButtonPosY = 0;
	
	private string[] sayings = { "Oooh", "We're", "no", "strangers", "to", "love.", 
								 "You", "know", "the", "rules", "and", "so", "do", "I.",
								 "A", "full", "commitment's", "what", "I'm", "thinking", "of.",
								 "You", "wouldn't", "get", "this", "from", "any", "other", "guy.",
		
							 	 "I", "just", "wanna", "tell", "you", "how", "I'm", "feeling.", 
								 "Gotta", "make", "you", "understand", "...",
		
								 "Never", "gonna", "give", "you", "up.",
								 "Never", "gonna", "let", "you", "down.",
								 "Never", "gonna", "run", "around", "and", "desert", "you.",
								 "Never", "gonna", "make", "you", "cry.",
								 "Never", "gonna", "say", "goodbye.",
								 "Never", "gonna", "tell", "a", "lie", "and", "hurt", "you.",
		
								 "We've", "known", "each", "other", "for", "so", "long.",
								 "Your", "heart's", "been", "aching", "but",
								 "you're", "too", "shy", "to", "say", "it.",
								 "Inside", "we", "both", "know", "what's", "been", "going", "on.",
								 "We", "know", "the", "game", "and", "we're", "gonna", "play", "it.",
		
								 "Never", "gonna", "give", "you", "up.",
								 "Never", "gonna", "let", "you", "down.",
								 "Never", "gonna", "run", "around", "and", "desert", "you.",
								 "Never", "gonna", "make", "you", "cry.",
								 "Never", "gonna", "say", "goodbye.",
								 "Never", "gonna", "tell", "a", "lie", "and", "hurt", "you." };

	
	private int sayingsIndex = 0;
	
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("Music") == "Off") { playMusic = false; music.Pause (); }
		if (PlayerPrefs.GetInt ("Count") == 0) { PlayerPrefs.SetInt("Count", 10); }
		if (PlayerPrefs.GetString ("Difficulty") == "") { PlayerPrefs.SetString ("Difficulty", "Normal"); }
		
		// Initialize these variables so original random location can be set
		boxSizeX = Screen.width / 20 * 16;
		boxSizeY = Screen.height / 20 * 16;
		boxPosX = Screen.width / 2 - boxSizeX / 2;
		boxPosY = Screen.height / 20 * 2;
		randomButtonSizeX = Screen.width / 12 * 3;
		randomButtonSizeY = Screen.height / 9;
		randomButtonPosX = Random.Range(boxPosX, boxSizeX - randomButtonSizeX);
		randomButtonPosY = Random.Range(boxPosY + randomButtonSizeY, boxSizeY - randomButtonSizeY);
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; }
		
		time = new Timer();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
		
		time.UpdateTimer();
		if (time.IsFinished()) { GameOver(); }
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; }
	}
	
	void OnGUI() {
		
		boxSizeX = Screen.width / 20 * 16;
		boxSizeY = Screen.height / 20 * 16;
		boxPosX = Screen.width / 2 - boxSizeX / 2;
		boxPosY = Screen.height / 20 * 2;
		randomButtonSizeX = Screen.width / 12 * 3;
		randomButtonSizeY = Screen.height / 9;
		
		if (PlayerPrefs.GetString ("GUI") == "Blue") { GUI.skin = blueSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Green") { GUI.skin = greenSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Red") { GUI.skin = redSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "Black") { GUI.skin = blackSkin; }
		else if (PlayerPrefs.GetString ("GUI") == "White") { GUI.skin = whiteSkin; }
		
		var e = Event.current; // Used to get keyboard inputs
		
		// GUI Styles
		GUIStyle bigStyle = new GUIStyle(GUI.skin.label);
		bigStyle.fontSize = 16;
		bigStyle.fontStyle = FontStyle.Bold;
		bigStyle.normal.textColor = Color.white;
		GUIStyle centeredStyle = new GUIStyle(GUI.skin.label);
		centeredStyle.fontSize = 16;
		centeredStyle.fontStyle = FontStyle.Bold;
		centeredStyle.alignment = TextAnchor.MiddleCenter;
		centeredStyle.normal.textColor = Color.white;
		GUIStyle randomButtonStyle = new GUIStyle(GUI.skin.button);
		randomButtonStyle.fontSize = 16;
		
		// Title
		GUI.Label (new Rect(Screen.width / 2 - 100, 5, 200, 20), "10 Second Button Masher", centeredStyle);
		
		// Menu Box
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), "Press all the buttons ASAP");
		
		// Random-Placed Button
		if (GUI.Button (new Rect(randomButtonPosX, randomButtonPosY, randomButtonSizeX, randomButtonSizeY), sayings[sayingsIndex], randomButtonStyle)) {
			score += 1 * scoreModifier; // add a point
			randomButtonPosX = Random.Range(boxPosX, boxSizeX - randomButtonSizeX);
			randomButtonPosY = Random.Range(boxPosY + randomButtonSizeY, boxSizeY - randomButtonSizeY);
			sayingsIndex++;
			if (sayingsIndex >= sayings.Length) { sayingsIndex = 0; }
			if (PlayerPrefs.GetString("Difficulty") == "Normal") { time.AdjustTimer(0.5f); }
			else if (PlayerPrefs.GetString("Difficulty") == "Hard") { time.AdjustTimer(0.25f); }
			else if (PlayerPrefs.GetString("Difficulty") == "Extreme") { time.AdjustTimer(0.1f); }
			presses++;
		}
		
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
		
		// Timer Label
		// the * 100.0f and / 100.0f is to round to 2 significant digits, for 3 (etc.) use 1000.0f instead
		string timeString = "Time = " + (Mathf.Round(time.GetTimeLeft () * 100.0f) / 100.0f).ToString ();
		GUI.Label (new Rect(Screen.width / 2 - 35, Screen.height - 21, 140, 30), timeString, bigStyle);
		
		// Score Label
		GUIStyle scoreStyle = new GUIStyle(GUI.skin.label);
		scoreStyle.fontStyle = FontStyle.Bold;
		scoreStyle.fontSize = 16;
		scoreStyle.normal.textColor = Color.white;
		int scorePadding = 4;
		int scoreSizeX = 200;
		int scoreSizeY = 30;
		GUI.Label (new Rect(0 + scorePadding, Screen.height - (20 +  scorePadding), scoreSizeX, scoreSizeY), "Score = " + score, scoreStyle);
		
		// Difficulty Button
		if (GUI.Button (new Rect(Screen.width - 121, Screen.height - 31, 120, 30), PlayerPrefs.GetString ("Difficulty"))
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.S)) { // 'S' hotkey) {
			if (PlayerPrefs.GetString ("Difficulty") == "Normal") {
				PlayerPrefs.SetString ("Difficulty", "Hard");
			}
			else if (PlayerPrefs.GetString ("Difficulty") == "Hard") {
				PlayerPrefs.SetString ("Difficulty", "Extreme");
			}
			else if (PlayerPrefs.GetString ("Difficulty") == "Extreme") {
				PlayerPrefs.SetString ("Difficulty", "Normal");
			}
		}
		
		// Quit Button		
		if (GUI.Button (new Rect (1, 1, 60, 30), "Back")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)) { // 'Escape' hotkey
			Application.LoadLevel("MainMenu");
		}
		
	}
	
	public void GameOver() {
		PlayerPrefs.SetInt ("LastPresses", presses);
		PlayerPrefs.SetInt ("Score", score);
		Application.LoadLevel ("MasherGameResults");
	}
}
