using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : ICommand
{

    public int oldCharacter { get; private set; }
    public int fromCellId { get; private set; }
    public int newCharacter { get; private set; }

    public Matrix matrix { get; private set; }


    public RotationCommand(int fromCellId, int newCharacter)
    {
        this.fromCellId = fromCellId;
        this.newCharacter = newCharacter;

        matrix = Matrix.Instance;
        oldCharacter = matrix.GetCharacter(fromCellId);
    }


    public void Execute()
    {
        // upate matrix with rotation value
        matrix.ChangePiece(fromCellId, newCharacter);
    }


    public void Revert()
    {

    }



}
