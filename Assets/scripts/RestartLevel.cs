using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour 
{
	public void restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
