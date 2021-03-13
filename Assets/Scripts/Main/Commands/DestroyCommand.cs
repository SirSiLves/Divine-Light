using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCommand : ICommand
{

    public int fromCellId { get; private set; }
    public int characterValue { get; private set; }

    private Matrix matrix;


    public DestroyCommand(int pieceCellId)
    {
        fromCellId = pieceCellId;

        matrix = Matrix.Instance;
        characterValue = matrix.GetCharacter(pieceCellId);
    }


    public void Execute()
    {
        // remove piece from matrix
        matrix.ChangePiece(fromCellId, 0);
    }


    public void Revert()
    {
        // add piece on matrix
        matrix.ChangePiece(fromCellId, characterValue);
    }

    public string GetDescription()
    {
        // destroy, cell, piece
        return "D:" + " C:" + fromCellId.ToString() + " P:" + characterValue;
    }

}
