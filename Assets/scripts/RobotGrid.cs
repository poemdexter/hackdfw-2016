﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotGrid : MonoBehaviour
{
    public GameObject rockParticles;
    public AudioSource buttonPressSound;
    public AudioSource rockAnalyzeSound;
    public int gridWidth = 5;
    public int gridHeight = 5;
    public int robotX = 2;
    public int robotY = 0;
    public Transform robot;
    public Transform robotMouth;
    public Transform player;
    public Transform[] nodes;
    public double delayBetweenCommands = 1.0;
    public Vector2[] asteroidPositions;
    public Vector2 titaniumAsteroidPosition;

    private Queue<string> commandQueue;
    private bool executing;
    private double currentDelayTime;
	private GameObject challengeCompleteCanvas;

	public bool test;
	bool challengeCompleted;

    private Object rockParticlesInstance;

    void Start()
    {
        commandQueue = new Queue<string>();
	    challengeCompleteCanvas = GameObject.Find ("ChallengeCompleteCanvas");
    }

    public void Chomp()
    {
        iTween.MoveBy(robotMouth.gameObject, iTween.Hash("y", -0.3, "easeType", "easeOutElastic", "speed", 1, "oncomplete", "CheckForWin", "oncompletetarget", gameObject));
        
        Destroy(nodes[robotY * 5 + robotX].GetChild(0).gameObject);
        rockParticlesInstance = GameObject.Instantiate(rockParticles, nodes[robotY * 5 + robotX].position, Quaternion.identity);
    }

    public void CheckForWin()
    {
        rockAnalyzeSound.Play();
        Destroy(rockParticlesInstance);
        if (NodeContainsTiAsteroid())
        {
            challengeCompleteCanvas.GetComponent<Canvas>().enabled = true; ;
            challengeCompleteCanvas.transform.GetChild(6).GetComponent<ChallengeComplete>().ChallengeCompleted();
            challengeCompleted = true;
            executing = false;
        }
    }

    void Update()
    {	
		if (!challengeCompleted && test) {
			challengeCompleteCanvas.GetComponent<Canvas> ().enabled = true;
			challengeCompleteCanvas.transform.GetChild(6).GetComponent<ChallengeComplete> ().ChallengeCompleted ();
			challengeCompleted = true;
		}

        if (!challengeCompleted && executing)
        {
            if ((currentDelayTime += Time.deltaTime) >= delayBetweenCommands)
            {
                currentDelayTime = 0;
                if (commandQueue.Count > 0)
                {
                    string command = commandQueue.Dequeue();
                    Debug.Log(command);
                    if (IsValidCommand(command))
                    {
                        if (command == "Analyze")
                        {   
                            if (NodeContainsAsteroid())
                            {
                                robot.LookAt(player);
                                iTween.MoveBy(robotMouth.gameObject, iTween.Hash("y", 0.3, "easeType", "easeOutElastic", "speed", .3, "delay", .1, "oncomplete", "Chomp", "oncompletetarget", gameObject));
                            }
                        }
                        else
                        {
                            switch (command)
                            {
                                case "Up":
                                    robotY++;
                                    break;
                                case "Down":
                                    robotY--;
                                    break;
                                case "Left":
                                    robotX--;
                                    break;
                                case "Right":
                                    robotX++;
                                    break;
                            }

                            Transform toNode = nodes[robotY * 5 + robotX];
                            robot.LookAt(toNode.position);
							iTween.MoveTo(robot.gameObject, toNode.position, .5f);
                        }
                    }
                }
                else
                {
                    executing = false;
                }
            }
        }
    }

    private bool NodeContainsTiAsteroid()
    {
        return titaniumAsteroidPosition.x == robotX && titaniumAsteroidPosition.y == robotY;
    }

    private bool NodeContainsAsteroid()
    {
        foreach (Vector2 pos in asteroidPositions)
        {
            if (pos.x == robotX && pos.y == robotY)
            {
                return true;
            }
        }
        return false;
    }

    public void NewInput(string command)
    {
        commandQueue.Enqueue(command);
        buttonPressSound.Play();
    }

    public void ClearQueue()
    {
        commandQueue.Clear();
    }

    public void Execute()
    {
        executing = true;
    }

    private bool IsValidCommand(string command)
    {
        switch (command)
        {
            case "Up":
                return robotY + 1 < gridHeight;
            case "Down":
                return robotY - 1 >= 0;
            case "Left":
                return robotX - 1 >= 0;
            case "Right":
                return robotX + 1 < gridWidth;
        }
        return true;
    }
}
