using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{

    public GameObject cellPrefab;

    public Cell[,] cellArray = new Cell[10, 10];
    const int BOARD_DIMENSION = 10;


    public void Create()
    {
        removeCurrentCells();

        //Create
        for (int y = 0; y < BOARD_DIMENSION; y++)
        {
            for (int x = 0; x < BOARD_DIMENSION; x++)
            {
                // Create the cell
                GameObject newCell = Instantiate(cellPrefab, transform);

                // Position
                RectTransform rectTransform = newCell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2((x * 100) + 50, (y * 100) + 50);

                // Setup
                cellArray[x, y] = newCell.GetComponent<Cell>();
                cellArray[x, y].Setup(new Vector2Int(x, y), this);
            }
        }


        //Color
        //for (int x = 0; x < BOARD_DIMENSION; x += 2)x
    }

    private void removeCurrentCells()
    {
        Cell[] cells = FindObjectsOfType<Cell>();
        if (cells.Length > 0)
        {
            foreach (Cell cell in cells)
            {
                DestroyImmediate(cell);
            }
        }
    }


}
