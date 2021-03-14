using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : ICommand
{

    public int fromCellId { get; private set; }
    public int oldCharacter { get; private set; }
    public int newCharacter { get; private set; }
    private int[][] formerMatrix { get; set; }


    public RotationCommand(int fromCellId, int newCharacter)
    {
        this.fromCellId = fromCellId;
        this.newCharacter = newCharacter;

        formerMatrix = Matrix.Clone(Matrix.Instance.GetMatrix());
        oldCharacter = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), fromCellId);
    }


    public void Execute()
    {
        // upate matrix with rotation value
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, newCharacter);
    }


    public void Revert()
    {
        // upate matrix with rotation value
        //Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, oldCharacter);

        Matrix.Instance.SetMatrix(formerMatrix);
    }


    public string GetDescription()
    {
        // rotate, cell, piece
        return "R:" + " C:" + fromCellId.ToString() + " P:" + oldCharacter + ":" + newCharacter;
    }


    public int[][] GetFormerMatrix()
    {
        return formerMatrix;
    }


}
