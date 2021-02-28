﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : ICommand
{

    private Piece touchedPiece;
    private int degrees;
    private Matrix matrix;


    public RotationCommand(Piece touchedPiece, int degrees, Matrix matrix)
    {
        this.touchedPiece = touchedPiece;
        this.degrees = degrees;
        this.matrix = matrix;
    }


    public void Execute()
    {
        // matrix with rotation value
        int newRotationId = GetNewRotationId();

        int sourceCellId = matrix.GetCellId(touchedPiece.transform.position.y, touchedPiece.transform.position.x);
        matrix.ChangePiece(sourceCellId, touchedPiece.id);

        // draw new rotation to board
        touchedPiece.transform.rotation = Quaternion.Euler(0, 0, degrees);

    }

    public void Revert()
    {

    }


    private int GetRotationId(int degrees)
    {
        switch (degrees)
        {
            case 0:
                return 0;
            case 90:
                return 1;
            case 180:
                return 2;
            case 270:
                return 3;
            default:
                Debug.LogError("No mapping for degrees value " + degrees);
                return 0;
        }
    }


    private int GetNewRotationId()
    {
        int rotateId = touchedPiece.id % 100;
        touchedPiece.id -= rotateId;

        rotateId = GetRotationId(degrees);


        return 0;
    }
}
