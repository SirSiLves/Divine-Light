using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    private Matrix matrix; // Singleton


    void Awake()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);
    }


    public Matrix GetMatrix()
    {
        return matrix;
    }

    


    //private void removeCurrentCells()s
    //{
    //    CellFactory[] cells = FindObjectsOfType<CellFactory>();

    //    if (cells.Length > 0)
    //    {
    //        foreach (CellFactory cell in cells)
    //        {
    //            DestroyImmediate(cell);
    //        }
    //    }
    //}

}
