using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveCommand : ICommand
{

    public int characterValue { get; private set; }
    public int fromCellId { get; private set; }
    public int toCellId { get; private set; }
    
    private Matrix matrix { get; set; }


    public MoveCommand(int fromCellId, int toCellId)
    {
        this.fromCellId = fromCellId;
        this.toCellId = toCellId;

        matrix = Matrix.Instance;
        characterValue = matrix.GetCharacter(fromCellId);
    }


    public void Execute()
    {
        // update matrix with new position
        matrix.ChangePiece(fromCellId, 0);
        matrix.ChangePiece(toCellId, characterValue);
    }


    public void Revert()
    {
        // update matrix with old position
        matrix.ChangePiece(toCellId, 0);
        matrix.ChangePiece(fromCellId, characterValue);
    }



}
