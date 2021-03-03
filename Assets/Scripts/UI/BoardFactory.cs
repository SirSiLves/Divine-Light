using UnityEngine;
using System;
using System.Collections.Generic;


public class BoardFactory : MonoBehaviour
{

    [SerializeField] PieceFactory pieceFactory;
    [SerializeField] CellFactory cellFactory;

    private const int BOARD_DIMENSION_Y = 8;
    private const int BOARD_DIMENSION_X = 10;


    public void CreateBoard(Matrix matrix)
    {
        int[][] matrixArray = new int[BOARD_DIMENSION_Y][]; // jagged array's have better performance

        for (int y = 0; y < BOARD_DIMENSION_Y; y++)
        {
            matrixArray[y] = new int[BOARD_DIMENSION_X];

            for (int x = 0; x < BOARD_DIMENSION_X; x++)
            {
                cellFactory.Create(y, x);

                // intialize matrix
                matrixArray[y][x] = 0;
            }
        }

        matrix.SetMatrix(matrixArray);
    }


    public void CreateDefaultSetUp(Matrix matrix)
    {
        Dictionary<int, int> defaultSet = pieceFactory.GetDefaultSet();

        int[][] matrixArray = matrix.GetMatrix();

        int index = 0;
        for (int y = 0; y < matrixArray.Length; y++)
        {
            for (int x = 0; x < matrixArray[y].Length; x++)
            {
                if (defaultSet.ContainsKey(index))
                {
                    int pieceId = defaultSet[index];
                    matrixArray[y][x] = pieceId;
                    pieceFactory.InstantiatePiece(y, x, pieceId);
                }
                index++;
            }
        }

        Matrix.PrintMatrixToConsole(matrix.GetMatrix());

        
    }


}
