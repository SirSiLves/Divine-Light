using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class ReplaceCommand : ICommand
{

    public Piece touchedPiece { get; private set; }
    public Piece targetPiece { get; private set; }
    public Vector2 fromPosition { get; private set; }
    public Vector2 toPosition { get; private set; }
    public Matrix matrix { get; private set; }


    public ReplaceCommand(Piece touchedPiece, Piece targetPiece, Vector2 fromPosition, Vector2 toPosition)
    {
        this.touchedPiece = touchedPiece;
        this.targetPiece = targetPiece;
        this.fromPosition = fromPosition;
        this.toPosition = toPosition;
        this.matrix = Matrix.Instance;
    }


    public void Execute()
    {
        // update matrix with new position
        matrix.ChangePiece(Matrix.ConvertPostionToCellId(toPosition), touchedPiece.id);
        matrix.ChangePiece(Matrix.ConvertPostionToCellId(fromPosition), targetPiece.id);

        //// draw pieces to new position
        //UpdatePosition(touchedPiece.transform, toPosition);
        //UpdatePosition(targetPiece.transform, fromPosition);
    }


    public void Revert()
    {

    }


    private void UpdatePosition(Transform sourceTransform, Vector2 position)
    {
        sourceTransform.position = new Vector2(position.x, position.y);
    }

}
