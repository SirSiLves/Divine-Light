using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MoveCommand : ICommand
{

    private Piece movedFigure;
    private Cell targetCell;
    private Matrix matrix;


    public MoveCommand(Piece movedFigure, Cell targetCell, Matrix matrix)
    {
        this.movedFigure = movedFigure;
        this.targetCell = targetCell;
        this.matrix = matrix;
    }


    public void Execute()
    {
        // update matrix with new position
        int sourceCellId = matrix.GetCellId(movedFigure.transform.position.y, movedFigure.transform.position.x);
        matrix.ChangePiece(sourceCellId, 0);
        matrix.ChangePiece(targetCell.GetCellId(), movedFigure.id);

        // draw piece to new position
        movedFigure.transform.position = new Vector2(targetCell.transform.position.x, targetCell.transform.position.y);
    }



    public void Revert()
    {

    }



}
