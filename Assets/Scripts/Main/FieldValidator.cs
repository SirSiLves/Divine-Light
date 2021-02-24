using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class FieldValidator : MonoBehaviour
{

    private Matrix matrix;
    private Cell[] cells;
    private List<Cell> lastPossibleFields;


    private void Start()
    {
        matrix = FindObjectOfType<GameManager>().matrix;
        cells = FindObjectsOfType<Cell>();
        lastPossibleFields = new List<Cell>();
    }


    public List<Cell> GetLastValidatedCells()
    {
        return lastPossibleFields;
    }


    public List<Cell> CollectPossibleFields(Piece movingFigure)
    {
        lastPossibleFields.Clear();

        int mFigureX = (int) Math.Round(movingFigure.transform.position.x);
        int mFigureY = (int) Math.Round(movingFigure.transform.position.y);
        int[][] rawMatrix = matrix.GetMatrix();
        int cellId = 0;

        for (int y = 0; y < rawMatrix.Length; y++)
        {
            for (int x = 0; x < rawMatrix[y].Length; x++)
            {
                if(Validate(mFigureY, mFigureX, y, x, movingFigure))
                {
                    Cell highlightCell = Array.Find(cells, cell => cell.GetCellId() == cellId);
                    lastPossibleFields.Add(highlightCell);
                }

                cellId++;
            }
        }


        return lastPossibleFields;
    }


    private bool Validate(int mFigureY, int mFigureX, int y, int x, Piece movingFigure)
    {
        if (!ValidateCellsAround(mFigureX, mFigureY, y, x)) { return false; }
        else if (!ValidateReplace(y, x, movingFigure)) { return false; }
        else { return true; };
    }


    private bool ValidateCellsAround(int mFigureX, int mFigureY, int y, int x)
    {
        return mFigureY + 1 == y && mFigureX - 1 == x
        || mFigureY + 1 == y && mFigureX + 0 == x
        || mFigureY + 1 == y && mFigureX + 1 == x
        || mFigureY == y && mFigureX - 1 == x
        || mFigureY == y && mFigureX + 1 == x
        || mFigureY - 1 == y && mFigureX - 1 == x
        || mFigureY - 1 == y && mFigureX + 0 == x
        || mFigureY - 1 == y && mFigureX + 1 == x;
    }


    private bool ValidateReplace(int y, int x, Piece movingFigure)
    {
        int cellOccupied = matrix.GetMatrix()[y][x];

        //empy cell
        if(cellOccupied == 0) { return true; }

        //reflactor is allowed to replace, but only with an angler or wall
        return movingFigure.id % 10 == 4 && (cellOccupied % 10 == 3 || cellOccupied % 10 == 5);
    }









}
