using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotGrid : MonoBehaviour
{
    public int gridWidth = 4;
    public int gridHeight = 4;
    public int robotX = 0;
    public int robotY = 0;
    public Transform robot;
    public Transform[] nodes;
    public double delayBetweenCommands = 1.0;

    private Queue<string> commandQueue;
    private bool executing;
    private double currentDelayTime;

    void Start()
    {
        commandQueue = new Queue<string>();
    }

    void Update()
    {
        if (executing)
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
                            // todo: analyze thing
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

                            Transform toNode = nodes[robotY * 4 + robotX];
                            robot.LookAt(toNode.position);
                            robot.Translate(Vector3.forward);
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

    public void NewInput(string command)
    {
        commandQueue.Enqueue(command);
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
