using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class ReplaceCommand : ICommand
{

    private Piece touchedPiece, targetPiece;
    private Cell targetCell;
    private Matrix matrix;
    private Cell[] cells;

    public ReplaceCommand(Piece touchedPiece, Piece targetPiece, Cell targetCell, Cell[] cells, Matrix matrix)
    {
        this.touchedPiece = touchedPiece;
        this.targetPiece = targetPiece;
        this.targetCell = targetCell;
        this.cells = cells;
        this.matrix = matrix;
    }


    public void Execute()
    {
        int cellId = matrix.GetCellId(touchedPiece.transform.position.y, touchedPiece.transform.position.x);
        Cell touchedCells = Array.Find(cells.ToArray(), cell => cell.GetCellId() == cellId);

        // update matrix with new position
        matrix.ChangePiece(targetCell.GetCellId(), touchedPiece.id);
        matrix.ChangePiece(touchedCells.GetCellId(), targetPiece.id);


        // draw pieces to new position
        UpdatePosition(touchedPiece.transform, targetCell.transform);
        UpdatePosition(targetPiece.transform, touchedCells.transform);
    }


    public void Revert()
    {

    }


    private void UpdatePosition(Transform sourceTransform, Transform targetTransform)
    {
        sourceTransform.position = new Vector2(
            targetTransform.transform.position.x, targetTransform.transform.position.y
            );
    }

}
