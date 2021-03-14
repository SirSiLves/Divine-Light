using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveCommand : ICommand
{

    public int characterValue { get; private set; }
    public int fromCellId { get; private set; }
    public int toCellId { get; private set; }
    private int[][] formerMatrix { get; set; }


    public MoveCommand(int fromCellId, int toCellId)
    {
        this.fromCellId = fromCellId;
        this.toCellId = toCellId;

        formerMatrix = Matrix.Clone(Matrix.Instance.GetMatrix());
        characterValue = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), fromCellId);
    }


    public void Execute()
    {
        // update matrix with new position
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, 0);
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), toCellId, characterValue);
    }


    public void Revert()
    {
        Matrix.Instance.SetMatrix(formerMatrix);
    }

    public string GetDescription()
    {
        //move, cell, piece
        return "M:" + " C:" + fromCellId.ToString() + ":" + toCellId + " P:" + characterValue;
    }

    public int[][] GetFormerMatrix()
    {
        return formerMatrix;
    }

}
