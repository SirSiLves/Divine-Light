using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveCommand : ICommand
{

    private Piece touchedPiece;
    private Cell targetCell;
    private Matrix matrix;


    public MoveCommand(Piece touchedPiece, Cell targetCell, Matrix matrix)
    {
        this.touchedPiece = touchedPiece;
        this.targetCell = targetCell;
        this.matrix = matrix;
    }


    public void Execute()
    {
        // update matrix with new position
        int sourceCellId = matrix.GetCellId(touchedPiece.transform.position.y, touchedPiece.transform.position.x);
        matrix.ChangePiece(sourceCellId, 0);
        matrix.ChangePiece(targetCell.GetCellId(), touchedPiece.id);

        // draw piece to new position
        touchedPiece.transform.position = new Vector2(targetCell.transform.position.x, targetCell.transform.position.y);
    }



    public void Revert()
    {

    }



}
