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
        // draw piece to new position
        movedFigure.transform.position = new Vector2(targetCell.transform.position.x, targetCell.transform.position.y);

        // update matrix with new position
        matrix.ChangePiece(targetCell.GetCellId(), movedFigure.GetPieceValue());
    }



    public void Revert()
    {

    }



}
