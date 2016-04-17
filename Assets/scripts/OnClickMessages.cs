using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

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
    public AudioSource buttonPressSound;
	public EventSystem _eventsystem;
	GameObject button;
	GameObject buttonParent;
	float speed = 1;
	//public GameObject hand;
	Vector3 handStartingPosition;

	void Start()
	{
		_eventsystem = EventSystem.current;
		//handStartingPosition = hand.transform.position;
	}

	public void DepressButton()
	{
		button = _eventsystem.currentSelectedGameObject;
		buttonParent = button.transform.parent.gameObject;

		float step = speed * Time.deltaTime;

		//buttonToPressDown.transform.position = Vector3.MoveTowards (transform.position, buttonPressedState.transform.position, step);

        iTween.MoveBy(buttonParent, iTween.Hash("y", 0.05, "easeType", "easeOutElastic", "speed", .5, "oncomplete", "ResetButtonPosition", "oncompletetarget", gameObject));

		//StartCoroutine (MoveHand ());

	}

    public void ResetButtonPosition()
    {
        iTween.MoveBy(buttonParent, iTween.Hash("y", -0.05, "easeType", "easeOutElastic", "speed", .5));
    }

	//IEnumerator MoveHand()
	//{
	//	float step = speed * Time.deltaTime;
	
	//	hand.transform.position = Vector3.MoveTowards (transform.position, buttonToPressDown.transform.position, step);

	//	yield return new WaitForSeconds (.2f);

	//	hand.transform.position = Vector3.MoveTowards (transform.position, handStartingPosition, step);

	//}

	public void InputCommand(string buttonClicked)
	{
		if (numberOfCommandsEnteredThisExecution >= 6) {
			tooManyCommandsMessage.SetActive (true);
			return;

		}
		_robotGrid.NewInput (buttonClicked);
	}

	public void ExecuteCommands()
	{
        buttonPressSound.Play();
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
        buttonPressSound.Play();
		_robotGrid.ClearQueue ();
		numberOfCommandsEnteredThisExecution = 0;
		foreach (GameObject command in Commands) 
		{
			command.GetComponent<Image>().enabled = false;
		}
	}

	public void DisplayCommand(string buttonClicked)
	{
		if (numberOfCommandsEnteredThisExecution >= 6)
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