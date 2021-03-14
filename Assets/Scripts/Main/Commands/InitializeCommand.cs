using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeCommand : ICommand
{

    private int[][] formerMatrix { get; set; }
    public int startPlayingIndex { get; private set; }


    public InitializeCommand(int startPlayingIndex)
    {
        this.startPlayingIndex = startPlayingIndex;
        formerMatrix = Matrix.Clone(Matrix.Instance.GetMatrix());
    }


    public void Execute()
    {
        // nothing to execute
    }


    public void Revert()
    {
        Matrix.Instance.SetMatrix(formerMatrix);
    }

    public string GetDescription()
    {
        return "I:" + " P:" + startPlayingIndex;
    }

    public int[][] GetFormerMatrix()
    {
        return formerMatrix;
    }


}
