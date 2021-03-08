using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : ICommand
{

    public Piece touchedPiece { get; private set; }
    public Vector2 fromPosition { get; private set; }
    public int oldPieceId { get; private set; }
    public int newPieceId { get; private set; }
    public Matrix matrix { get; private set; }


    public RotationCommand(Piece touchedPiece, Vector2 fromPosition, int oldPieceId, int newPieceId)
    {
        this.touchedPiece = touchedPiece;
        this.fromPosition = fromPosition;
        this.oldPieceId = oldPieceId;
        this.newPieceId = newPieceId;
        this.matrix = Matrix.Instance;
    }


    public void Execute()
    {
        touchedPiece.id = newPieceId;

        // matrix with rotation value
        matrix.ChangePiece(Matrix.ConvertPostionToCellId(fromPosition), touchedPiece.id);


        //// draw new rotation to board
        //touchedPiece.transform.rotation = Quaternion.Euler(0, 0, toDegrees);
    }


    public void Revert()
    {

    }



}
