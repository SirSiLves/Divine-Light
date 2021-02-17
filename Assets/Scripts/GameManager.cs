using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    private int[][] matrix;


    // Start is called before the first frame update
    void Start()
    {
        matrix = boardFactory.Create();

        PieceFactory pieceFactory = new PieceFactory();
        pieceFactory.Create(matrix);

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
