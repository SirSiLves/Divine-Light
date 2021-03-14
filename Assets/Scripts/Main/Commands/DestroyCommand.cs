using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCommand : ICommand
{

    public int fromCellId { get; private set; }
    public int characterValue { get; private set; }
    private int[][] formerMatrix { get; set; }


    public DestroyCommand(int pieceCellId)
    {
        fromCellId = pieceCellId;

        formerMatrix = Matrix.Clone(Matrix.Instance.GetMatrix());
        characterValue = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), pieceCellId);
    }


    public void Execute()
    {
        // remove piece from matrix
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, 0);
    }


    public void Revert()
    {
        // add piece on matrix
        //Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, characterValue);

        Matrix.Instance.SetMatrix(formerMatrix);
    }

    public string GetDescription()
    {
        // destroy, cell, piece
        return "D:" + " C:" + fromCellId.ToString() + " P:" + characterValue;
    }

    public int[][] GetFormerMatrix()
    {
        return formerMatrix;
    }

}
