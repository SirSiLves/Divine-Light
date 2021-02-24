using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Matrix
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


    public void ChangePiece(int cellId, int pieceId)
    {
        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                // set piece on cell id
                if(index == cellId)
                {
                    matrix[y][x] = pieceId;
                }

                index++;
            }
        }
    }


    public int GetCellId(float pieceY, float pieceX)
    {       

        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                if (y == (int) Math.Round(pieceY) && x == (int) Math.Round(pieceX))
                {
                    return index;
                }

                index++;
            }
        }

        throw new Exception("No piece value found, something went wrong!");
    }



    //public int GetCellId(int pieceId)
    //{
    //    int index = 0;
    //    for (int y = 0; y < matrix.Length; y++)
    //    {
    //        for (int x = 0; x < matrix[y].Length; x++)
    //        {
    //            if (matrix[y][x] == pieceId)
    //            {
    //                return index;
    //            }

    //            index++;
    //        }
    //    }

    //    throw new Exception("No piece value found, something went wrong!");
    //}


    public int GetPieceId(int cellId)
    {
        int index = 0;
        for(int y = 0; y < matrix.Length; y++)
        {
            for(int x = 0; x < matrix[y].Length; x++)
            {
                if(index == cellId)
                {
                    return matrix[y][x];
                }

                index++;
            }
        }

        return 0;
    }


    public static void PrintMatrixToConsole(int[][] matrix)
    {
        string line = "";

        for(int y = matrix.Length - 1; y >= 0; y--)
        {
            for(int x = 0; x < matrix[y].Length; x++)
            {
                if (matrix[y][x] >= 100)
                {
                    line += matrix[y][x] + "|";
                }
                else if (matrix[y][x] >= 10)
                {
                    line += "0" + matrix[y][x] + "|";
                }
                else
                {
                    line += "00" + matrix[y][x] + "|";
                }
            }
            line += "\n";
        }

        Debug.Log(line);
    }


}
