using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OnClickMessages : MonoBehaviour 
{
	public RobotGrid _robotGrid;
	public List<GameObject> Commands = new List<GameObject>();
	int numberOfCommandsEnteredThisExecution = 0;
	public Text executionCounter;
	public Text commandCounter;
	public GameObject tooManyCommandsMessage;
	public Sprite upSprite;
	public Sprite downSprite;
	public Sprite leftSprite;
	public Sprite rightSprite;
	public Sprite analyzeSprite;

	public void InputCommand(string buttonClicked)
	{
		if (numberOfCommandsEnteredThisExecution >= 7) {
			tooManyCommandsMessage.SetActive (true);
			return;

		}
		_robotGrid.NewInput (buttonClicked);
	}

	public void ExecuteCommands()
	{
		_robotGrid.Execute ();
		ScoreCounter.totalExecutionsThisChallenge ++;
		ScoreCounter.totalCommandsThisChallenge += numberOfCommandsEnteredThisExecution;
		executionCounter.text = "Total Executions: " + ScoreCounter.totalExecutionsThisChallenge.ToString();
		commandCounter.text = "Total Commands: " + ScoreCounter.totalCommandsThisChallenge.ToString ();
		numberOfCommandsEnteredThisExecution = 0;
		foreach (GameObject command in Commands) 
		{
			command.GetComponent<Image>().enabled = false;
		}
	}

	public void ClearCommands()
	{
		_robotGrid.ClearQueue ();
		numberOfCommandsEnteredThisExecution = 0;
		foreach (GameObject command in Commands) 
		{
			command.GetComponent<Image>().enabled = false;
		}
	}

	public void DisplayCommand(string buttonClicked)
	{
		if (numberOfCommandsEnteredThisExecution >= 14)
			return;
		
		Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().enabled = true;

		if (buttonClicked == "Up")
			Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().sprite = upSprite;
		if(buttonClicked == "Down")
			Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().sprite = downSprite;
		if(buttonClicked == "Left")
			Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().sprite = leftSprite;
		if(buttonClicked == "Right")
			Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().sprite = rightSprite;
		if(buttonClicked == "Analyze")
			Commands [numberOfCommandsEnteredThisExecution].GetComponent<Image> ().sprite = analyzeSprite;
		
		numberOfCommandsEnteredThisExecution++;
	}

	public void ClosePopUp()
	{
		tooManyCommandsMessage.SetActive (false);
	}
}