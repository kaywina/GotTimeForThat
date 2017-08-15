using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionLibrary : MonoBehaviour {
	
	List<List<string>> questions;
	List<List<string>> questionsShuffled;
	
	// Use this for initialization
	void Start () {
		questions = new List<List<string>>();
		questionsShuffled = new List<List<string>>();
		
		List<string> actions10 = new List<string>() { "Tell them you care.", "Build an ark.", "Lead your people to safety.", "Call the police." };
		List<string> actions9 = new List<string>() { "Be thankful.", "Go into a tizzy.", "Bargain for your life.", "Write an autobiography." };
		List<string> actions8 = new List<string>() { "Bless your family.", "Write a last will and testament.", "Curse the injustice.", "Deny it's happening." };
		List<string> actions7 = new List<string>() { "Look outside.", "Try to understand why.", "Blame society.", "Call your parents." };
		List<string> actions6 = new List<string>() { "Forgive your debts.", "Hold onto a grudge.", "Hyperventilate.", "List your regrets." };
		List<string> actions5 = new List<string>()  { "Relax your shoulders.", "Wipe tears from your eyes.", "Sigh heavily.", "Despair." };
		List<string> actions4 = new List<string>() { "Squeeze their hand.", "Check your finances.", "Worry about tomorrow.", "Find fault with others." };
		List<string> actions3 = new List<string>() { "Hope for the best.", "Fix your hair.", "Have a beer.", "Smoke a cigarette." };
		List<string> actions2 = new List<string>() { "Close your eyes.", "Pee your pants.", "Start a fire.", "Plan for retirement." };
		List<string> actions1 = new List<string>() { "Kiss them.", "Scream.", "Thrash violently.", "Work on weekends." };
		List<string> actions0 = new List<string>() { "", "", "", "" }; // this is a buffer to hack around an error in GotTimeGame
		
		questions.Add (actions10);
		questions.Add (actions9);
		questions.Add (actions8);
		questions.Add (actions7);
		questions.Add (actions6);
		questions.Add (actions5);
		questions.Add (actions4);
		questions.Add (actions3);
		questions.Add (actions2);
		questions.Add (actions1);
		questions.Add (actions0); // buffer hack
		
		List<string> shuffled10 = new List<string>(actions10);
		shuffled10.Shuffle ();
		List<string> shuffled9 = new List<string>(actions9);
		shuffled9.Shuffle ();
		List<string> shuffled8 = new List<string>(actions8);
		shuffled8.Shuffle ();
		List<string> shuffled7 = new List<string>(actions7);
		shuffled7.Shuffle ();
		List<string> shuffled6 = new List<string>(actions6);
		shuffled6.Shuffle ();
		List<string> shuffled5 = new List<string>(actions5);
		shuffled5.Shuffle ();
		List<string> shuffled4 = new List<string>(actions4);
		shuffled4.Shuffle ();
		List<string> shuffled3 = new List<string>(actions3);
		shuffled3.Shuffle ();
		List<string> shuffled2 = new List<string>(actions2);
		shuffled2.Shuffle ();
		List<string> shuffled1 = new List<string>(actions1);
		shuffled1.Shuffle ();
		
		questionsShuffled.Add (shuffled10);
		questionsShuffled.Add (shuffled9);
		questionsShuffled.Add (shuffled8);
		questionsShuffled.Add (shuffled7);
		questionsShuffled.Add (shuffled6);
		questionsShuffled.Add (shuffled5);
		questionsShuffled.Add (shuffled4);
		questionsShuffled.Add (shuffled3);
		questionsShuffled.Add (shuffled2);
		questionsShuffled.Add (shuffled1);
		questionsShuffled.Add (actions0); // buffer hack
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public List<List<string>> GetQuestions() { return questions; }
	public List<List<string>> GetQuestionsShuffled() { return questionsShuffled; }
}
