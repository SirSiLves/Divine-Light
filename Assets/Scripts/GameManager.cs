using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    private MoveHandler moveHandler;
    private Matrix matrix;



    void Start()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);

        moveHandler = new MoveHandler();
    }




    public MoveHandler GetMoveHandler()
    {
        return moveHandler;
    }

    public Matrix GetMatrix()
    {
        return matrix;
    }

    


    //private void removeCurrentCells()
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
