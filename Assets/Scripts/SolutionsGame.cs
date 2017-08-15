using UnityEngine;
using System;
using System.Collections;

public class SolutionsGame : MonoBehaviour {
	
	public GUISkin blueSkin;
	public GUISkin greenSkin;
	public GUISkin redSkin;
	public GUISkin blackSkin;
	public GUISkin whiteSkin;
	
	public AudioSource music;
	public GameObject headbangers;
	private bool playMusic = true;
	private bool showResetButton = false;
	private bool showMotivation = false;
	private int score = 0;
	private int level = 1; // assigned to starting level
	private int scoreModifier; // modifies score depending on difficulty
	private string playerSolution = "";
	private Timer time;
	private RandomEquation equation;
	
	
	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("Music") == "Off") { playMusic = false; music.Pause (); }
		if (PlayerPrefs.GetInt ("Count") == 0) { PlayerPrefs.SetInt("Count", 10); }
		if (PlayerPrefs.GetString ("Difficulty") == "") { PlayerPrefs.SetString ("Difficulty", "Normal"); }
		
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; RandomEquation.SetHighestVariable(10); }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; RandomEquation.SetHighestVariable(100); }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; RandomEquation.SetHighestVariable(1000); }
		
		time = new Timer();
		equation = new RandomEquation(level);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
		
		time.UpdateTimer();
		if (time.IsFinished()) { GameOver(); }
		if (PlayerPrefs.GetString ("Difficulty") == "Normal") { scoreModifier = 1; RandomEquation.SetHighestVariable(10); }
		if (PlayerPrefs.GetString ("Difficulty") == "Hard") { scoreModifier = 2; RandomEquation.SetHighestVariable(100); }
		if (PlayerPrefs.GetString ("Difficulty") == "Extreme") { scoreModifier = 3; RandomEquation.SetHighestVariable(1000); }
	}
	
	void OnGUI() {
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

		// Title
		GUI.Label (new Rect(Screen.width / 2 - 125, 5, 250, 20), "10 Second Solutions", centeredStyle);
		
		// Menu Box
		int boxSizeX = Screen.width / 20 * 16;
		int boxSizeY = Screen.height / 20 * 16;
		int boxPosX = Screen.width / 2 - boxSizeX / 2;
		int boxPosY = Screen.height / 20 * 2;
			
		GUI.Box (new Rect(boxPosX, boxPosY, boxSizeX, boxSizeY), "Solve equation to reset timer.");
		
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
		
		// Timer Label
		// the * 100.0f and / 100.0f is to round to 2 significant digits, for 3 (etc.) use 1000.0f instead
		string timeString = "Time = " + (Mathf.Round(time.GetTimeLeft () * 100.0f) / 100.0f).ToString ();
		GUI.Label (new Rect(boxPosX + 5, boxPosY + 5, 120, 30), timeString, bigStyle);
		
		// Display Calculator-Style Buttons
		int numberButtonSizeX = boxSizeX / 5;
		int numberButtonSizeY = boxSizeY / 6;
		int buttonPadding = 5;
		int numberButtonPosX = boxSizeX - numberButtonSizeX * 3 + buttonPadding * 2;
		int numberButtonPosY = boxSizeY - numberButtonSizeY * 4 + buttonPadding * 3;
		
		GUIStyle numberButtonStyle = new GUIStyle(GUI.skin.button);
		numberButtonStyle.fontSize = 20;
		numberButtonStyle.fontStyle = FontStyle.Bold;
		
		// 7, 8, and 9
		if (GUI.Button (new Rect (numberButtonPosX, numberButtonPosY, numberButtonSizeX, numberButtonSizeY), "7", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha7)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad7)) { // '7' hotkey
			ProcessInput("7");
		}
		if (GUI.Button (new Rect (numberButtonPosX + numberButtonSizeX + buttonPadding, numberButtonPosY, numberButtonSizeX, numberButtonSizeY), "8", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha8)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad8)) { // '8' hotkey
			ProcessInput("8");
		}
		if (GUI.Button (new Rect (numberButtonPosX + (numberButtonSizeX * 2) + (buttonPadding * 2), numberButtonPosY, numberButtonSizeX, numberButtonSizeY), "9", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha9)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad9)) { // '9' hotkey
			ProcessInput("9");
		}
		
		// 4, 5, and 6
		if (GUI.Button (new Rect (numberButtonPosX, numberButtonPosY + numberButtonSizeY + buttonPadding, numberButtonSizeX, numberButtonSizeY), "4", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha4)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad4)) { // '4' hotkey
			ProcessInput("4");
		}
		if (GUI.Button (new Rect (numberButtonPosX + numberButtonSizeX + buttonPadding, numberButtonPosY + numberButtonSizeY + buttonPadding, numberButtonSizeX, numberButtonSizeY), "5", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha5)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad5)) { // '5' hotkey
			ProcessInput("5");
		}
		if (GUI.Button (new Rect (numberButtonPosX + (numberButtonSizeX * 2) + (buttonPadding * 2), numberButtonPosY + numberButtonSizeY + buttonPadding, numberButtonSizeX, numberButtonSizeY), "6", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha6)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad6)) { // '6' hotkey
			ProcessInput("6");
		}
		
		// 3, 2, and 1
		if (GUI.Button (new Rect (numberButtonPosX, numberButtonPosY + (numberButtonSizeY * 2) + (buttonPadding * 2), numberButtonSizeX, numberButtonSizeY), "1", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha1)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad1)) { // '1' hotkey
			ProcessInput("1");
		}
		if (GUI.Button (new Rect (numberButtonPosX + numberButtonSizeX + buttonPadding, numberButtonPosY + (numberButtonSizeY * 2) + (buttonPadding * 2), numberButtonSizeX, numberButtonSizeY), "2", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha2)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad2)) { // '2' hotkey
			ProcessInput("2");
		}
		if (GUI.Button (new Rect (numberButtonPosX + (numberButtonSizeX * 2) + (buttonPadding * 2), numberButtonPosY + (numberButtonSizeY * 2) + (buttonPadding * 2), numberButtonSizeX, numberButtonSizeY), "3", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha3)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad3)) { // '3' hotkey
			ProcessInput("3");
		}
		
		// Negative Button, Zero, and Clear Button
		if (GUI.Button (new Rect (numberButtonPosX, numberButtonPosY + (numberButtonSizeY * 3) + (buttonPadding * 3), numberButtonSizeX, numberButtonSizeY), "-", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Minus)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.KeypadMinus)) { // '-' hotkey
			ProcessInput("-");
		}
		if (GUI.Button (new Rect (numberButtonPosX + numberButtonSizeX + buttonPadding, numberButtonPosY + (numberButtonSizeY * 3) + (buttonPadding * 3), numberButtonSizeX, numberButtonSizeY), "0", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Alpha0)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Keypad0)) { // '0' hotkey
			ProcessInput("0");
		}
		if (GUI.Button (new Rect (numberButtonPosX + (numberButtonSizeX * 2) + (buttonPadding * 2), numberButtonPosY + (numberButtonSizeY * 3) + (buttonPadding * 3), numberButtonSizeX, numberButtonSizeY), "Clear", numberButtonStyle)
			|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Backspace)) { // 'Clear' hotkey
			ProcessInput("Clear");
		}
		
		// Reset Timer Button
		int resetButtonPosX = boxPosX + 5;
		int resetButtonPosY = boxPosY + 30;
		int resetButtonSizeX = numberButtonSizeX;
		int resetButtonSizeY = numberButtonSizeY;
		
		// Button
		if (showResetButton) {
			if (GUI.Button (new Rect (resetButtonPosX, resetButtonPosY, resetButtonSizeX, resetButtonSizeY), "Reset")
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.Return) // 'Return' hotkey
				|| (e.type == EventType.KeyDown && e.keyCode == KeyCode.R)) { // 'R' hotkey
				LevelUp();
				equation = new RandomEquation(level);
				playerSolution = "";
				time.StartTimer();
				score += 1 * scoreModifier;
				showResetButton = false;
			}
		}
		
		// Is the answer correct? Only show the reset button if it is
		int solved = 0;
		bool result = Int32.TryParse(playerSolution, out solved);
		if (result) { // Only check if valid input
			if (int.Parse(playerSolution) == equation.GetSolution()) { // Check for correct answer
				showResetButton = true; // show reset button
			}
			else { // do not show button if the answer is incorrect
				showResetButton = false;
			}
		}
		
		// Display Equation Label and Solution Field
		int equationLabelSizeX = numberButtonSizeX * 2;
		int equationLabelSizeY = 60;
		int equationLabelPosX = numberButtonPosX;
		int equationLabelPosY = numberButtonPosY - 100;
		if (level <= 5) { equationLabelPosY = numberButtonPosY - 35; }
		else if (level > 5 && level <= 10) { equationLabelPosY = numberButtonPosY - 45; }
		else if (level > 10) { equationLabelPosY = numberButtonPosY - 55; }
			
		GUI.Label (new Rect(equationLabelPosX, equationLabelPosY, equationLabelSizeX, equationLabelSizeY), equation.GetEquation(), bigStyle);
		playerSolution = GUI.TextField(new Rect(equationLabelPosX + equationLabelSizeX + 10, equationLabelPosY - 2, numberButtonSizeX, 25), playerSolution);
		
		// Display motivational message
		if (showMotivation) {
			GUI.Label (new Rect(boxPosX + 5, equationLabelPosY + 45, 100, 30), "Good Job!", centeredStyle);
			GUI.Label (new Rect(boxPosX + 5, equationLabelPosY + 65, 100, 30), "Level " + level.ToString(), centeredStyle);
		}
		
	}
	
	private void ProcessInput(string input) {
		if (input == "Clear") {
			playerSolution = "";
		}
		else {playerSolution += input.ToString (); }
	}
	
	private void LevelUp() {
		
		// Change level
		if (score == 10) { level++; ShowMotivationMessage(true); }
		if (score == 20) { level++; ShowMotivationMessage(true); }
		if (score == 30) { level++; ShowMotivationMessage(true); }
		if (score == 40) { level++; ShowMotivationMessage(true); }
		if (score == 50) { level++; ShowMotivationMessage(true); }
		if (score == 60) { level++; ShowMotivationMessage(true); }
		if (score == 70) { level++; ShowMotivationMessage(true); }
		if (score == 80) { level++; ShowMotivationMessage(true); }
		if (score == 90) { level++; ShowMotivationMessage(true); }
		if (score == 100) { level++; ShowMotivationMessage(true); }
		if (score == 110) { level++; ShowMotivationMessage(true); }
		if (score == 120) { level++; ShowMotivationMessage(true); }
		if (score == 130) { level++; ShowMotivationMessage(true); }
		if (score == 140) { level++; ShowMotivationMessage(true); }
		
		if (score == 11) { ShowMotivationMessage(false); }
		if (score == 21) { ShowMotivationMessage(false); }
		if (score == 31) { ShowMotivationMessage(false); }
		if (score == 41) { ShowMotivationMessage(false); }
		if (score == 51) { ShowMotivationMessage(false); }
		if (score == 61) { ShowMotivationMessage(false); }
		if (score == 71) { ShowMotivationMessage(false); }
		if (score == 81) { ShowMotivationMessage(false); }
		if (score == 91) { ShowMotivationMessage(false); }
		if (score == 101) { ShowMotivationMessage(false); }
		if (score == 111) { ShowMotivationMessage(false); }
		if (score == 121) { ShowMotivationMessage(false); }
		if (score == 131) { ShowMotivationMessage(false); }
		if (score == 141) { ShowMotivationMessage(false); }
	}
	
	private void ShowMotivationMessage(bool toSet) {
		headbangers.SetActive(toSet);
		showMotivation = toSet; 
	}
	
	private void GameOver() {
		PlayerPrefs.SetInt ("Level", level);
		PlayerPrefs.SetInt ("Score", score);
		Application.LoadLevel ("SolutionsGameResults");	
	}
}
