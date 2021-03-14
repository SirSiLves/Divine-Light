using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class MoveValidator
{

    public static List<int> CollectPossibleCellIds(int fromCellId)
    {
        List<int> cellIds = new List<int>();
        int[][] matrix = Matrix.Instance.GetMatrix();

        int character = Matrix.GetCharacter(matrix, fromCellId);


        //// player is rotating
        //if (RotationHandler.Instance.isRotating) { return cellIds; }

        //// no piece found
        //if (character == 0) { return cellIds; }

        //// other players turn
        //if (PlayerHandler.Instance.isPlayingIndex == 0 && character >= 100) { return cellIds; }
        //if (PlayerHandler.Instance.isPlayingIndex == 1 && character < 100) { return cellIds; }


        int cellId = 0;

        for (int y = 0; y < matrix.Length; y++)
        {
            for (int x = 0; x < matrix[y].Length; x++)
            {
                if (Validate(matrix, fromCellId, character, y, x))
                {
                    cellIds.Add(cellId);
                }

                cellId++;
            }
        }

        return cellIds;
    }


    private static bool Validate(int[][] matrix, int fromCellId, int character, int y, int x)
    {
        int[] xy = Matrix.GetCoordinates(matrix, fromCellId);
        int xFromCellId = xy[0];
        int yFromCellId = xy[1];

        // not allowed to move
        if (ValidateRestrictedMove(character)) { return false; }

        // cell is not around the touched figure
        if (!ValidateCellsAround(xFromCellId, yFromCellId, y, x)) { return false; }

        // not allowed to swap
        if (!ValidateSwap(y, x, character)) { return false; }

        // target cell is a safe zone from other player
        int enemyIndex = PlayerHandler.GetEnemyIndex(character);
        if (ValidateSafeZone(y, x, enemyIndex)) { return false; }

        return true;
    }


    private static bool ValidateCellsAround(int fromPositionX, int fromPositionY, int y, int x)
    {
        return fromPositionY + 1 == y && fromPositionX - 1 == x
        || fromPositionY + 1 == y && fromPositionX + 0 == x
        || fromPositionY + 1 == y && fromPositionX + 1 == x
        || fromPositionY == y && fromPositionX - 1 == x
        || fromPositionY == y && fromPositionX + 1 == x
        || fromPositionY - 1 == y && fromPositionX - 1 == x
        || fromPositionY - 1 == y && fromPositionX + 0 == x
        || fromPositionY - 1 == y && fromPositionX + 1 == x;
    }

    public static bool ValidateRestrictedMove(int character)
    {
        int pieceType = character % 10;

        // sun
        if (pieceType == 1)
        {
            return true;
        }

        return false;
    }


    private static bool ValidateSwap(int y, int x, int character)
    {
        int cellOccupied = Matrix.Instance.GetMatrix()[y][x];

        //empy cell
        if (cellOccupied == 0) { return true; }

        //reflactor is allowed to swap, but only with an angler or wall
        return character % 10 == 4 && (cellOccupied % 10 == 3 || cellOccupied % 10 == 5);
    }


    public static bool ValidateSafeZone(int y, int x, int playerIndex)
    {
        if ((x == 0 || (x == 8 && (y == 0 || y == 7))) && playerIndex == 1)
        {
            return true;
        }
        else if ((x == 9 || (x == 1 && (y == 0 || y == 7))) && playerIndex == 0)
        {
            return true;
        }

        return false;
    }

}
