using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PossibleFieldsCommand : ICommand
{

    private Cell[] cells, possibleCells;

    public PossibleFieldsCommand(Cell[] cells, Cell[] possibleCells)
    {
        this.cells = cells;
        this.possibleCells = possibleCells;
    }


    public void Execute()
    {

        //TODO markup possible fields other remove markup
    }


    public void Revert()
    {

    }
}
