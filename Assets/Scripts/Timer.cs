using UnityEngine;
using System.Collections;

public class Timer {
	
	float endTime; // used in timer countdown
	float timeLeft;
	
	public Timer() {		
		StartTimer ();
	}
	
	// Update is called to update the timer
	public void UpdateTimer () {
		// calculate time left in countdown
		timeLeft = endTime - Time.time; 
	}
	
	public float GetTimeLeft() {
		return timeLeft;	
	}
	
	public bool IsFinished() {
		// If timer is finished
		if (timeLeft <= 0) { return true; }
		else { return false; }
	}
	
	public void StartTimer() {
		// set up countdown timer
		timeLeft = PlayerPrefs.GetInt ("Count") + 1;
		endTime = Time.time + PlayerPrefs.GetInt ("Count");
	}
	
	public void AdjustTimer(float secondsToAdd) {
		// set up countdown timer
		timeLeft += secondsToAdd;
		endTime += secondsToAdd;
	}
}
