using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCommand : ICommand
{

    public int pieceCellId { get; private set; }
    public int characterValue { get; private set; }

    private Matrix matrix;


    public DestroyCommand(int pieceCellId)
    {
        this.pieceCellId = pieceCellId;

        matrix = Matrix.Instance;
        characterValue = matrix.GetCharacter(pieceCellId);
    }


    public void Execute()
    {
        // remove piece from matrix
        matrix.ChangePiece(pieceCellId, 0);
    }


    public void Revert()
    {
        // add piece on matrix
        matrix.ChangePiece(pieceCellId, characterValue);
    }


}
