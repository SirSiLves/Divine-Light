using UnityEngine;
using System;
using System.Collections.Generic;


public class BoardFactory : MonoBehaviour
{

    [SerializeField] PieceFactory pieceFactory;
    [SerializeField] CellFactory cellFactory;

    private const int BOARD_DIMENSION_Y = 8;
    private const int BOARD_DIMENSION_X = 10;


    public void CreateBoard(Matrix matrixObj)
    {
        int[][] matrix = new int[BOARD_DIMENSION_Y][]; // jagged array's have better performance

        for (int y = 0; y < BOARD_DIMENSION_Y; y++)
        {
            matrix[y] = new int[BOARD_DIMENSION_X];

            for (int x = 0; x < BOARD_DIMENSION_X; x++)
            {

                cellFactory.Create(y, x);

                // intialize matrix
                matrix[y][x] = 0;
            }
        }

        matrixObj.SetMatrix(matrix);
    }


    public void CreateDefaultSetUp(Matrix matrixObj)
    {
        Dictionary<int, int> defaultSet = pieceFactory.GetDefaultSet();

        int[][] matrix = matrixObj.GetMatrix();

        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                if (defaultSet.ContainsKey(index))
                {
                    int pieceValue = defaultSet[index];
                    matrix[y][x] = pieceValue;
                    pieceFactory.InstantiatePiece(y, x, pieceValue);
                }
                index++;
            }
        }

        Matrix.PrintMatrixToConsole(matrixObj.GetMatrix());

        
    }


}
