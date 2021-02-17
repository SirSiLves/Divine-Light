using UnityEngine;
using System;


public class BoardFactory : MonoBehaviour
{

    [SerializeField] CellFactory cellPrefab;

    private const int BOARD_DIMENSION_Y = 8;
    private const int BOARD_DIMENSION_X = 10;


    public int[][] Create()
    {
        int[][] matrix = new int[BOARD_DIMENSION_Y][]; // jagged array's have better performance

        for (int y = 0; y < BOARD_DIMENSION_Y; y++)
        {
            matrix[y] = new int[BOARD_DIMENSION_X];

            for (int x = 0; x < BOARD_DIMENSION_X; x++)
            {
                // standard cell
                CellFactory newCell = Instantiate(cellPrefab, transform);
                newCell.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);
                newCell.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                newCell.transform.name = y.ToString() + ',' + x.ToString();

                // intialize matrix
                matrix[y][x] = 0;
            }
        }

        PrintMatrixToConsole(matrix);
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
                line += row >= 100 ? line += matrix[indexY][indexX] + "|" : row >= 10 ? line += "0" + matrix[indexY][indexX] + "|"  : line += "00" + matrix[indexY][indexX] + "|";


                //if (row >= 100)
                //{
                //    line += matrix[indexY][indexX] + "|";
                //}
                //else if (row >= 10)
                //{
                //    line += "0" + matrix[indexY][indexX] + "|";
                //}
                //else
                //{
                //    line += "00" + matrix[indexY][indexX] + "|";
                //}

                indexX++;
            });
            line += "\n";

            indexY++;
        });

        Debug.Log(line);
    }





}
