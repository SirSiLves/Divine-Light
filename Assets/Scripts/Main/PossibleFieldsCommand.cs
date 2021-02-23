using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PossibleFieldsCommand : ICommand
{

    private Cell[] cells;
    private Piece movingFigure;
    private Matrix matrix;


    public PossibleFieldsCommand(Cell[] cells, Piece movingFigure, Matrix matrix)
    {
        this.cells = cells;
        this.movingFigure = movingFigure;
        this.matrix = matrix;
    }


    public void Execute()
    {

        int startFieldId = matrix.GetCellId(movingFigure.transform.position.y, movingFigure.transform.position.x);

        Cell startField = Array.Find(cells, cell => cell.GetCellId() == startFieldId);


        Array.ForEach(cells, cell =>
        {

            if (startField.GetCellId() - 1 == cell.GetCellId()
            || startField.GetCellId() + 1 == cell.GetCellId()
            || startField.GetCellId() - 10 == cell.GetCellId()
            || startField.GetCellId() + 10 == cell.GetCellId()
            || startField.GetCellId() + 9 == cell.GetCellId()
            || startField.GetCellId() - 9 == cell.GetCellId()
            || startField.GetCellId() - 11 == cell.GetCellId()
            || startField.GetCellId() + 11 == cell.GetCellId()
            )
            {
                cell.transform.GetComponent<SpriteRenderer>().color = Color.green;
            }




        });





    }


    public void Revert()
    {
        //TODO remove markup
    }






}
