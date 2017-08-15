using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEquation {
	
	private static int HIGHEST_VARIABLE = 9;
	private string[] OPERATORS = { " + ", " - "};
	
	private int numberOfVariables = 0;
	private List<int> variables;
	private List<string> operators;
	private int solution = 0;
	
	public RandomEquation (int level) {
		numberOfVariables = level + 1;
		variables = new List<int>();
		operators = new List<string>();
		GenerateEquation ();
	}
	
	public void GenerateEquation() {
		// Create variables according to level
		for (int i = 0; i < numberOfVariables; i++) {
			int random = ThreadSafeRandom.ThisThreadsRandom.Next(HIGHEST_VARIABLE + 1);
			variables.Add(random);
		}
		// Create operators according to level - 1
		for (int i = 0; i < numberOfVariables - 1; i++) {
			int random = ThreadSafeRandom.ThisThreadsRandom.Next(OPERATORS.Length);
			operators.Add(OPERATORS[random]);
		}
		
		solution = SolveLeftToRight();
	}
	
		public int GetSolution() {
		return solution;
	}
	
	private int SolveLeftToRight() {
		int total = 0;
		for (int i = 0; i < variables.Count; i++) {
			if (i == 0) { total += variables[i]; }
			else if (operators[i-1] == " + " ) { total += variables[i]; }
			else if (operators[i-1] == " - " ) { total -= variables[i]; }
			
			//Debug.Log ("variables[i] = " + variables[i].ToString());
			//Debug.Log ("total = " + total.ToString());
		}
		return total;
	}
	
	
	public string GetEquation() {
		string equation = "";
		for (int i = 0; i < variables.Count; i++) {
			equation += variables[i];
			if (i < operators.Count) { equation += operators[i]; }
		}
		
		return equation;
	}
	
	public string GetVariables() {
		string toReturn = "";
		foreach (int variable in variables) {
			toReturn += variable.ToString() + " ";
		}
		return toReturn;
	}
	
	public string GetOperators() {
		string toReturn = "";
		foreach (string anOperator in operators) {
			toReturn += anOperator + " ";
		}
		return toReturn;
	}
	
	public static void SetHighestVariable(int toSet) {
		HIGHEST_VARIABLE = toSet;
	}
}
