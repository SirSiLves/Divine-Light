using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarkupFieldsCommand : ICommand
{

    private List<Cell> cells;
    private Color color;



    public MarkupFieldsCommand(List<Cell> cells, Color color)
    {
        this.cells = cells;
        this.color = color;
    }


    public void Execute()
    {
        Array.ForEach(cells.ToArray(), cell => {
            cell.transform.GetComponent<SpriteRenderer>().color = color;
        });
    }


    public void Revert()
    {
        
    }






}
