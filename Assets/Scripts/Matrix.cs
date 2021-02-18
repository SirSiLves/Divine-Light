using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Matrix : MonoBehaviour
{

    private int[][] matrix;


    public void SetMatrix(int[][] newMatrix)
    {
        matrix = newMatrix;
    }

    public int[][] GetMatrix()
    {
        return matrix;
    }


    public static void PrintMatrixToConsole(int[][] matrix)
    {
        string line = "";
        int indexY = 0;

        Array.Reverse(matrix);
        Array.ForEach(matrix, column =>
        {
            int indexX = 0;
            Array.ForEach(column, row => {
                if (row >= 100)
                {
                    line += matrix[indexY][indexX] + "|";
                }
                else if (row >= 10)
                {
                    line += "0" + matrix[indexY][indexX] + "|";
                }
                else
                {
                    line += "00" + matrix[indexY][indexX] + "|";
                }

                indexX++;
            });
            line += "\n";

            indexY++;
        });

        Debug.Log(line);
    }


}
