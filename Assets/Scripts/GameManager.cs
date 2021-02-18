using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    private Matrix matrixObj;

    // Start is called before the first frame update
    void Start()
    {
        matrixObj = FindObjectOfType<Matrix>();

        boardFactory.CreateBoard(matrixObj);
        boardFactory.CreateDefaultSetUp(matrixObj);

    }



    private void removeCurrentCells()
    {
        CellFactory[] cells = FindObjectsOfType<CellFactory>();

        if (cells.Length > 0)
        {
            foreach (CellFactory cell in cells)
            {
                DestroyImmediate(cell);
            }
        }
    }

}
