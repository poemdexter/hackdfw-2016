using UnityEngine;
using System.Collections;

public class closeCanvas : MonoBehaviour {

	public GameObject mainMenuCanvas;

	public void closeCanvasView()
	{
		mainMenuCanvas.SetActive (false);
	}
}
