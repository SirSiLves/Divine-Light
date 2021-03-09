using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Matrix
{

    #region MATRIX_SINGLETON_SETUP
    private static Matrix _instance;

    public static Matrix Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Matrix();
            }

            return _instance;
        }
    }
    #endregion


    private int[][] matrix;


    public void SetMatrix(int[][] newMatrix)
    {
        matrix = newMatrix;
    }

    public int[][] GetMatrix()
    {
        return matrix;
    }


    public void ChangePiece(int cellId, int character)
    {
        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                // set piece on cell id
                if(index == cellId)
                {
                    matrix[y][x] = character;
                }

                index++;
            }
        }
    }

    public int[] GetCoordinates(int cellId)
    {
        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                if (index == cellId)
                {
                    return new int[] { x, y };
                }

                index++;
            }
        }

        throw new Exception("Cell id not found, something went wrong!");
    }


    public int GetCellId(float positionY, float positionX)
    {       

        int index = 0;
        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                if (y == (int) Math.Round(positionY) && x == (int) Math.Round(positionX))
                {
                    return index;
                }

                index++;
            }
        }

        throw new Exception("Position not found, something went wrong!");
    }

    public int GetCellId(int[] coordinate)
    {
        int x = coordinate[0];
        int y = coordinate[1];

        if (x < 0 || y < 0)
        {
            throw new Exception("Coordinate is outside from grid");
        }

        return Int32.Parse(y + "" + x);
    }


    public int GetCharacter(int cellId)
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

    public static int ConvertPostionToCellId(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        if (x < 0 || y < 0)
        {
            throw new Exception("Position is outside from grid");
        }

        return Int32.Parse(y + "" + x);
    }

}
