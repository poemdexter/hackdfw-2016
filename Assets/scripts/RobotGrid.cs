using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotGrid : MonoBehaviour
{
    public int width = 4;
    public int height = 4;

    private Queue<string> commandQueue;

    void Start()
    {
        commandQueue = new Queue<string>();
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
        foreach(string command in commandQueue)
        {
            // robot do thing
        }
    }
}
