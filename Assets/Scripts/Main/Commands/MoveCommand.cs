using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveCommand : ICommand
{

    public Piece touchedPiece { get; private set; }
    public Vector2 fromPosition { get; private set; }
    public Vector2 toPosition { get; private set; }
    public Matrix matrix { get; private set; }


    public MoveCommand(Piece touchedPiece, Vector2 fromPosition, Vector2 toPosition)
    {
        this.touchedPiece = touchedPiece;
        this.fromPosition = fromPosition;
        this.toPosition = toPosition;
        this.matrix = Matrix.Instance;
    }


    public void Execute()
    {
        // update matrix with new position
        matrix.ChangePiece(Matrix.ConvertPostionToCellId(fromPosition), 0);
        matrix.ChangePiece(Matrix.ConvertPostionToCellId(toPosition), touchedPiece.id);

        //// draw piece to new position
        //touchedPiece.transform.position = new Vector2(toPosition.x, toPosition.y);
    }



    public void Revert()
    {

    }



}
