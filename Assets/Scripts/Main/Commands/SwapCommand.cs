using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class SwapCommand : ICommand
{

    public int characterValue1 { get; private set; }
    public int characterValue2 { get; private set; }
    public int fromCellId { get; private set; }
    public int toCellId { get; private set; }
    private int[][] formerMatrix { get; set; }


    public SwapCommand(int fromCellId, int toCellId)
    {
        this.fromCellId = fromCellId;
        this.toCellId = toCellId;

        formerMatrix = Matrix.Clone(Matrix.Instance.GetMatrix());
        characterValue1 = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), fromCellId);
        characterValue2 = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), toCellId);
    }


    public void Execute()
    {
        // update matrix with new position
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), toCellId, characterValue1);
        Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, characterValue2);
    }


    public void Revert()
    {
        // update matrix with old position
        //Matrix.ChangePiece(Matrix.Instance.GetMatrix(), fromCellId, characterValue1);
        //Matrix.ChangePiece(Matrix.Instance.GetMatrix(), toCellId, characterValue2);

        Matrix.Instance.SetMatrix(formerMatrix);
    }

    public string GetDescription()
    {
        // swap, cell, piece
        return "S:" + " C:" + fromCellId.ToString() + ":" + toCellId + " P:" + characterValue1 + ":" + characterValue2;
    }


    public int[][] GetFormerMatrix()
    {
        return formerMatrix;
    }

}
