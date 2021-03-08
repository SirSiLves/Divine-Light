using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.Linq;



public class FieldHandler : MonoBehaviour
{

    //private Matrix matrix;
    //private Cell[] cells;
    //private List<Cell> lastPossibleFields;
    //internal UnityEvent markupEvent, removeMarkupEvent, markupTouchedEvent;


    //private void Start()
    //{
    //    matrix = FindObjectOfType<GameManager>().matrix;
    //    cells = FindObjectsOfType<Cell>();
    //    lastPossibleFields = new List<Cell>();

    //    markupEvent = new UnityEvent();
    //    markupEvent.AddListener(MarkupPossibleFields);

    //    removeMarkupEvent = new UnityEvent();
    //    removeMarkupEvent.AddListener(RemoveMarkup);

    //    markupTouchedEvent = new UnityEvent();
    //    markupTouchedEvent.AddListener(MarkupTouchedField);
    //}


    //private void MarkupTouchedField()
    //{
    //    Piece touchedPiece = FindObjectOfType<ClickHandlerOLD>().touchedPiece;

    //    Color markupColor = FindObjectOfType<CellFactory>().highlightedFields;

    //    int cellId = matrix.GetCellId(touchedPiece.transform.position.y, touchedPiece.transform.position.x);

    //    List<Cell> touchedCells = new List<Cell>
    //    {
    //        Array.Find(cells.ToArray(), cell => cell.GetCellId() == cellId)
    //    };

    //    FindObjectOfType<GameManager>().executor.Execute(new MarkupFieldsCommand(touchedCells, markupColor));
    //}


    //private void MarkupPossibleFields()
    //{
    //    Piece touchedPiece = FindObjectOfType<ClickHandlerOLD>().touchedPiece;

    //    Color markupColor = FindObjectOfType<CellFactory>().possibleFields;

    //    lastPossibleFields = CollectPossibleFields(touchedPiece);

    //    FindObjectOfType<GameManager>().executor.Execute(new MarkupFieldsCommand(lastPossibleFields, markupColor));
    //}


    //public void RemoveMarkup()
    //{
    //    Color markupColor = FindObjectOfType<CellFactory>().defaultFields;

    //    FindObjectOfType<GameManager>().executor.Execute(new MarkupFieldsCommand(cells.OfType<Cell>().ToList(), markupColor));

    //    lastPossibleFields.Clear();
    //}


    //public List<Cell> GetLastValidatedCells()
    //{
    //    return lastPossibleFields;
    //}


    //public List<Cell> CollectPossibleFields(Piece movingFigure)
    //{
    //    lastPossibleFields.Clear();

    //    int mFigureX = (int) Math.Round(movingFigure.transform.position.x);
    //    int mFigureY = (int) Math.Round(movingFigure.transform.position.y);
    //    int[][] rawMatrix = matrix.GetMatrix();
    //    int cellId = 0;

    //    for (int y = 0; y < rawMatrix.Length; y++)
    //    {
    //        for (int x = 0; x < rawMatrix[y].Length; x++)
    //        {
    //            if(Validate(mFigureY, mFigureX, y, x, movingFigure))
    //            {
    //                Cell highlightCell = Array.Find(cells, cell => cell.GetCellId() == cellId);
    //                lastPossibleFields.Add(highlightCell);
    //            }

    //            cellId++;
    //        }
    //    }

    //    return lastPossibleFields;
    //}


    //private bool Validate(int mFigureY, int mFigureX, int y, int x, Piece movingFigure)
    //{
    //    int playerIndex = movingFigure.GetPlayer() == FindObjectOfType<PlayerChanger>().player1 ? 1 : 0;

    //    if (movingFigure.restrictedMove) { return false; } // not allowed to move
    //    else if (!ValidateCellsAround(mFigureX, mFigureY, y, x)) { return false; } // cell is not around the moved figure
    //    else if (!ValidateReplace(y, x, movingFigure)) { return false; } // not allowed to replace
    //    else if (ValidateSafeZone(y, x, playerIndex)) { return false; } // target cell is a safe zone from other player
    //    else { return true; };
    //}


    //private bool ValidateCellsAround(int mFigureX, int mFigureY, int y, int x)
    //{
    //    return mFigureY + 1 == y && mFigureX - 1 == x
    //    || mFigureY + 1 == y && mFigureX + 0 == x
    //    || mFigureY + 1 == y && mFigureX + 1 == x
    //    || mFigureY == y && mFigureX - 1 == x
    //    || mFigureY == y && mFigureX + 1 == x
    //    || mFigureY - 1 == y && mFigureX - 1 == x
    //    || mFigureY - 1 == y && mFigureX + 0 == x
    //    || mFigureY - 1 == y && mFigureX + 1 == x;
    //}


    //private bool ValidateReplace(int y, int x, Piece movingFigure)
    //{
    //    int cellOccupied = matrix.GetMatrix()[y][x];

    //    //empy cell
    //    if(cellOccupied == 0) { return true; }

    //    //reflactor is allowed to replace, but only with an angler or wall
    //    return movingFigure.id % 10 == 4 && (cellOccupied % 10 == 3 || cellOccupied % 10 == 5);
    //}


    //public static bool ValidateSafeZone(int y, int x, int playerIndex)
    //{
    //    if ((x == 0 || (x == 8 && (y == 0 || y == 7))) && playerIndex == 1) {
    //        return true;
    //    }
    //    else if ((x == 9 || (x == 1 && (y == 0 || y == 7))) && playerIndex == 0)
    //    {
    //        return true;
    //    }

    //    return false;
    //}






}
