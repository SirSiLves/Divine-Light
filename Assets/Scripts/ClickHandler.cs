using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickValidator
{

    private Piece movedFigure;
    private Matrix matrix = GameManager.FindObjectOfType<GameManager>().GetMatrix();
    private Cell[] cells = GameManager.FindObjectsOfType<Cell>();


    public void ValidateMove(Cell targetCell)
    {
       if(movedFigure == null) { return; }
        
        int startCellId = matrix.GetCellId(movedFigure.GetPieceValue());

        // find cell with clicked piece
        Array.ForEach(cells, cell =>
        {
            if (cell.GetCellId() == startCellId)
            {
                var move = new MoveCommand(movedFigure, targetCell, matrix);
                new Drawer(move).Draw();
                return;
            }
        });
    }

    internal void SetMovedFigure(Piece piece)
    {
        this.movedFigure = piece;

        this.ValidatePossibleFields();
    }


    private void ValidatePossibleFields()
    {




        var highlightFields = new PossibleFieldsCommand(cells, cells);
        new Drawer(highlightFields).Draw();
    }







}
