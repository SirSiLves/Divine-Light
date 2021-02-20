using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHandler
{

    private Piece movedFigure = null;


    public void ValidateMove(Cell targetCell)
    {
       if(movedFigure == null) { return; }

        Matrix matrix = GameManager.FindObjectOfType<GameManager>().GetMatrix();
        int startCellId = matrix.GetCellId(movedFigure.GetPieceValue());

        Cell[] cells = GameManager.FindObjectsOfType<Cell>();

        Array.ForEach(cells, cell =>
        {
            if (cell.GetCellId() == startCellId)
            {
                ExecuteMove(targetCell, matrix);
                return;
            }
        });



    }

    private void ExecuteMove(Cell targetCell, Matrix matrix)
    {
        // draw piece to new position
        movedFigure.transform.position = new Vector2(targetCell.transform.position.x, targetCell.transform.position.y);

        // update matrix with new position
        matrix.ChangePiece(targetCell.GetCellId(), movedFigure.GetPieceValue());
        return;
    }

    internal void SetMovedFigure(Piece piece)
    {
        this.movedFigure = piece;
    }







}
