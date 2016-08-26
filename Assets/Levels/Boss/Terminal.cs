using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Terminal : MonoBehaviour {

	public Text terminalText;
	int i;

	string[] texts = {
		"echo \"Hello, World!\"\n>Hello, World!",
		"set Toast",
		"Toast kill"
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
			//changeText (1);	
		}
	}
		
	public void changeText(int index) {
		currText = texts [index];
		i = 0;
		terminalText.text += "\n>";

	} 
}
