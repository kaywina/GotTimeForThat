using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEquationOld {
	
	private int level;
	private int solution;
	private List<int> variables;
	private List<string> operators;
	private int HIGHEST_SOLUTION = 10;
	private string equation = "";
	private int tempOperatorFlag = 0;
	
	public RandomEquationOld (int level) {
		this.level = level;
		variables = new List<int>();
		operators = new List<string>();
		GenerateRandomSolution ();
		GenerateEquation ();
	}
	
	public int GetLevel() {
		return level;
	}
	
	public int GetSolution() {
		return solution;
	}
	
	private void GenerateRandomSolution() {
		solution = ThreadSafeRandom.ThisThreadsRandom.Next(HIGHEST_SOLUTION);
	}
	
	private void GenerateEquation() {
		
		int x = 0;
		int y = 0;
		int previousX = 0;
		int previousY = 0;
		
		for (int i = 0; i < level; i++) { // 2 variables for level 1, 3 for level 2, etc...
			
			int numberOfOperators = 2;
			tempOperatorFlag = ThreadSafeRandom.ThisThreadsRandom.Next(numberOfOperators); // this number will represent a random operator
			
			if (tempOperatorFlag == 0) { // Case: Addition
				if (i == 0) { 
					x = ThreadSafeRandom.ThisThreadsRandom.Next(solution);
					y = solution - x;
					variables.Add (x);
					variables.Add (y);
				}
				else { 
					x = ThreadSafeRandom.ThisThreadsRandom.Next(previousY);
					y = previousY - x;
					variables.Add (y);
				}
				operators.Add(" + ");
				previousX = x;
				previousY = y;
			}
			
			else if (tempOperatorFlag == 1) { // Case: Subtraction
				if (i == 0) { 
					x = ThreadSafeRandom.ThisThreadsRandom.Next(solution);
					y = solution + x;
					variables.Add (y); // note reversed order of adding to list compared to addition
					variables.Add (x);
				}
				else {
					x = ThreadSafeRandom.ThisThreadsRandom.Next(previousY);
					y = previousY + x; // note the change in variable
					variables.Add (y);
				}
				operators.Add(" - ");
				previousX = x;
				previousY = y;
			}
			
			Debug.Log ("PreviousX = " + previousX);
			Debug.Log ("PreviousY = " + previousY);
			
		}
		Debug.Log ("solution = " + solution);
		
	}
	
	public string GetEquation() {
		for (int i = 0; i < level + 1; i++) {
			if (i == 0) { equation = variables[i].ToString (); }
			else { equation += (operators[i-1] + variables[i].ToString ());}
		}
		return equation;
	}
	
	public string GetVariables() {
		string output = "";
		for (int i = 0; i < variables.Count; i++) {
			output += variables[i].ToString() + " ";
		}
		return output;	
	}
}

