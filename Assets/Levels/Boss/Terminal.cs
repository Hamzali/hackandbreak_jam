using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Terminal : MonoBehaviour {
	public GameObject Computer;
	public Text terminalText;
	int i, lineCount;

	string[] texts = {
		"echo \"Hello, World!\"\n>Hello, World! \n>Computer.Protect();",
		"Toaster.SetActive(true);",
		"BobsPC.InTheFace();",
		"echo \"Oh my..\"\n>Oh my..",
		"BurnInCoffee();",
		"Clear();"
	};

	string currText;

	// Use this for initialization
	void Start () {
		terminalText.text = ">";
		currText = texts [0];
		i = 0; 
	}
	
	// Update is called once per frame
	void Update () {
		printText ();
	}


	void printText () {
		if (i < currText.Length) {
			terminalText.text += currText[i].ToString();	
			i++;
		} else {
			//Behey
		}

		if (lineCount > 15)
			clearText ();

		if (Computer == null)
			texts [2] = "NullReferanceException: Object referance not set to an instance of an object!";
	}
		
	public void changeText(int index) {
		currText = texts [index];
		i = 0;
		terminalText.text += "\n>";
		lineCount++;
	} 

	void clearText(){
		changeText (5);
		terminalText.text = ">";
		lineCount = 0;
	}
}
