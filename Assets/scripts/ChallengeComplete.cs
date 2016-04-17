using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeComplete : MonoBehaviour 
{
	public GameObject challengeCompleteCanvas;
	GameObject executionCount;
	GameObject commandCount;
	public Text executionsFinalScore;
	public Text commandsFinalScore;
	public Text finalScore;
	float finalScoreFloat;

	void Start()
	{
		executionCount = GameObject.Find ("ExecutionCount");
		commandCount = GameObject.Find ("CommandCount");
	}

	public void ChallengeCompleted()
	{
		Debug.Log (executionCount);
		executionsFinalScore.text = executionCount.GetComponent<Text> ().text;
		commandsFinalScore.text = commandCount.GetComponent<Text> ().text;

		int commandInt = int.Parse(commandsFinalScore.text.Substring (16));
		int executionInt = int.Parse(executionsFinalScore.text.Substring (18));

		Debug.Log (commandInt);
		Debug.Log (executionInt);


		challengeCompleteCanvas.SetActive (true);
		finalScoreFloat = (((10f/executionInt) * (10f/commandInt)) * 1000);
		Debug.Log (finalScoreFloat);
		finalScore.text = "Score: " + (int)finalScoreFloat;
	}
}
