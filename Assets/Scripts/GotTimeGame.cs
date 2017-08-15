using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GotTimeGame : MonoBehaviour {
	
	public GUISkin blueSkin;
	public GUISkin greenSkin;
	public GUISkin redSkin;
	public GUISkin blackSkin;
	public GUISkin whiteSkin;
	
	public GameObject headBanger;
	public GameObject headShaker;
	public QuestionLibrary questionLibrary;
	public AudioSource music;
	
	private int secondsLeft = 10;
	private int turn = 0;
	private int score = 0;
	private int incorrectGuesses = 0;
	private int scoreModifier = 1;
	
	private bool showChoices = true;
	private bool correct = false;
	private bool endGame = false;
	private bool playMusic = true;
	private bool showCredits = false;
	
	private string header = "";
	
	private List<string> ranks;
	
	
	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.GetString ("Music") == "Off") { playMusic = false; music.Pause (); }
		if (PlayerPrefs.GetInt ("Count") == 0) { PlayerPrefs.SetInt("Count", 10); }
		if (PlayerPrefs.GetString ("Difficulty") == "") { PlayerPrefs.SetString ("Difficulty", "Normal"); }
		
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; RandomEquation.SetHighestVariable(10); }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; RandomEquation.SetHighestVariable(100); }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; RandomEquation.SetHighestVariable(1000); }
		
		ranks = new List<string>();
		ranks.Add("Totally Unsure"); // rank 0
		ranks.Add("Very Unsure"); // rank 1 
		ranks.Add("Heavily Conflicted"); // rank 2
		ranks.Add("Mostly Unsure"); // rank 3
		ranks.Add("Fairly Conflicted"); // rank 4
		ranks.Add("Somewhat Unsure"); // rank 5
		ranks.Add("Slightly Conflicted"); // rank 6
		ranks.Add("Somewhat Prepared"); // rank 7
		ranks.Add("Mostly Prepared"); // rank 8
		ranks.Add("Nearly Prepared"); // rank 9
		ranks.Add("Completely Prepared"); // rank 10
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; }
	}
	
	void OnGUI() {
		
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; }
		
		
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
		GUI.Label (new Rect(Screen.width / 2 - 75, 5, 150, 20), "Got time for this?", centeredStyle);
		
		// Menu Box
		int boxSizeX = Screen.width / 20 * 16;
		int boxSizeY = Screen.height / 20 * 16;
		int boxPosX = Screen.width / 2 - boxSizeX / 2;
		int boxPosY = Screen.height / 20 * 2;
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), header);
		
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
		if (GUI.Button (new Rect (1, 1, 60, 30), "Back")
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Escape)) { // 'Escape' hotkey
			Application.LoadLevel("MainMenu");
		}
		
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
		
		// Swap Header message with number of seconds and end game display
		if (!endGame) { header = secondsLeft + " seconds until doomsday..."; }
		if (endGame) { header = "It's the end of the world as we know it!"; }
		
		// End Game Dialog
		if (endGame == true && showCredits == false) {
			
			headBanger.SetActive(true);
			headShaker.SetActive(true);
			
			// Get rank
			string rank = "";
			if (score == 10) { rank = ranks[10]; }
			else if (score == 9) { rank = ranks[9]; }
			else if (score == 8) { rank = ranks[8]; }
			else if (score == 7) { rank = ranks[7]; }
			else if (score == 6) { rank = ranks[6]; }
			else if (score == 5) { rank = ranks[5]; }
			else if (score == 4) { rank = ranks[4]; }
			else if (score == 3) { rank = ranks[3]; }
			else if (score == 2) { rank = ranks[2]; }
			else if (score == 1) { rank = ranks[1]; }
			else if (score <= 0) { rank = ranks[0]; }
			
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 105, 300, 30), "Game Over - Did you score?", centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 75, 300, 30), "Rank: " + rank, centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 45, 300, 30), "Score: " + score, centeredStyle);
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 15, 300, 30), "Incorrect guesses: " + incorrectGuesses, centeredStyle);
			if (GUI.Button (new Rect (Screen.width /2 - 100, Screen.height / 2 + 25, 200, 50), "Restart Game")
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'Return' hotkey
				Application.LoadLevel ("GotTimeGame");
			}
		}
		
		// Correct Choice Dialog
		if (showChoices == false && correct == true && endGame == false && showCredits == false) {
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 45, 300, 30), "Yeah y'all got time for that!", centeredStyle);
			headBanger.SetActive(true);
			string nextButtonText = "Next Question";
			if (turn == 10) { nextButtonText = "View Results"; } // if last turn of game
			if (GUI.Button (new Rect (Screen.width /2 - 100, Screen.height / 2, 200, 50), nextButtonText)
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'Return' hotkey
				headBanger.SetActive(false);
				secondsLeft -= 1;
				if (turn == 10) { turn = 0; endGame = true; } // if last turn of game
				else { showChoices = true; }
				
			}
		}
		
		// Incorrect Choice Dialog
		if (showChoices == false && correct == false && endGame == false && showCredits == false) {
			GUI.Label (new Rect(Screen.width /2 - 150, Screen.height / 2 - 45, 300, 30), "Ain't nobody got time for that!", centeredStyle);
			headShaker.SetActive(true);
			if (GUI.Button (new Rect (Screen.width /2 - 100, Screen.height / 2, 200, 50), "Retry")
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return)) { // 'Return' hotkey
				showChoices = true;
				headShaker.SetActive(false);
			}
		}
		
		// Choice Buttons Dialog
		int choiceButtonSizeX = Screen.width / 5 * 3;
		int choiceButtonSizeY = Screen.height / 7 * 1;
		int choiceButtonPosX = Screen.width / 2 - choiceButtonSizeX / 2;
		int choiceButtonPosY = Screen.height / 2 + 20;
		GUIStyle choiceButtonStyle = new GUIStyle(GUI.skin.button);
		choiceButtonStyle.fontSize = 16;
		
		// Blink the choices buttons at a different rate depending on difficulty
		double blinkModulator = 0;
		double showModulator = 0;
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") {
			blinkModulator = 1;
			showModulator = 1;
		}
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") {
			blinkModulator = 1.5;
			showModulator = 1;
		}
		
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") {
			blinkModulator = 1;
			showModulator = 0.5;
		}
		
		// Choices Buttons
		if (Time.time % blinkModulator < showModulator) {
			if (showChoices == true && endGame == false && turn < 10 && showCredits == false) {
				GUI.Label (new Rect(Screen.width /2 - 50, Screen.height / 2 - 100, 200, 30), "What do you do?");
				if (GUI.Button (new Rect (choiceButtonPosX, choiceButtonPosY - (choiceButtonSizeY * 2 + 20), choiceButtonSizeX, choiceButtonSizeY), "1. " + questionLibrary.GetQuestionsShuffled()[turn][0], choiceButtonStyle)
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha1) 
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad1)) { // '1' hotkey
					ProcessChoice(0);
				}
				if (GUI.Button (new Rect (choiceButtonPosX, choiceButtonPosY - (choiceButtonSizeY * 1 + 10), choiceButtonSizeX, choiceButtonSizeY), "2. " + questionLibrary.GetQuestionsShuffled()[turn][1], choiceButtonStyle)
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha2) 
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad2)) { // '2' hotkey
					ProcessChoice(1);
				}
				if (GUI.Button (new Rect (choiceButtonPosX, choiceButtonPosY, choiceButtonSizeX, choiceButtonSizeY), "3. " + questionLibrary.GetQuestionsShuffled()[turn][2], choiceButtonStyle)
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha3) 
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad3)) { // '3' hotkey
					ProcessChoice(2);
				}
				if (GUI.Button (new Rect (choiceButtonPosX, choiceButtonPosY + (choiceButtonSizeY * 1 + 10), choiceButtonSizeX, choiceButtonSizeY), "4. " + questionLibrary.GetQuestionsShuffled()[turn][3], choiceButtonStyle)
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha4) 
					|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad4)) { // '4' hotkey
					ProcessChoice(3);
				}
			}
		}
	}
	
	// Determines if a choice is correct or not and sets triggers for correct dialog
	private void ProcessChoice (int choice) {
		if (questionLibrary.GetQuestionsShuffled()[turn][choice] == questionLibrary.GetQuestions()[turn][0]) {
			score += 1 * scoreModifier;
			showChoices = false;
			correct = true;
			turn += 1;
		}
		else {
			score -= 1 * scoreModifier;
			showChoices = false;
			correct = false;
			incorrectGuesses += 1;
		}
	}
}
