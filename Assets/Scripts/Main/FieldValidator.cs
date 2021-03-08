using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class FieldValidator
{

    public static List<int> CollectPossibleCellIds(int fromCellId)
    {
        List<int> cellIds = new List<int>();

        // player is rotating
        if (RotationHandler.Instance.isRotating) { return cellIds; }

        int pieceId = Matrix.Instance.GetPieceId(fromCellId);

        // no piece found
        if (pieceId == 0) { return cellIds; }

        // other players turn
        if (PlayerHandler.Instance.isPlayingIndex == 0 && pieceId >= 100) { return cellIds; }
        if (PlayerHandler.Instance.isPlayingIndex == 1 && pieceId < 100) { return cellIds; }

        int[][] rawMatrix = Matrix.Instance.GetMatrix();
        int cellId = 0;

        for (int y = 0; y < rawMatrix.Length; y++)
        {
            for (int x = 0; x < rawMatrix[y].Length; x++)
            {
                if (Validate(fromCellId, pieceId, y, x))
                {
                    cellIds.Add(cellId);
                }

                cellId++;
            }
        }

        return cellIds;
    }


    private static bool Validate(int fromCellId, int pieceId, int y, int x)
    {
        int[] xy = Matrix.Instance.GetCoordinates(fromCellId);
        int xFromCellId = xy[0];
        int yFromCellId = xy[1];

        // cell is not around the touched figure
        if (!ValidateCellsAround(xFromCellId, yFromCellId, y, x)) { return false; }

        // not allowed to replace
        if (!ValidateReplace(y, x, pieceId)) { return false; }

        // target cell is a safe zone from other player
        int enemyIndex = pieceId >= 100 ? 0 : 1;
        if (ValidateSafeZone(y, x, enemyIndex)) { return false; }

        //TODO handle restricted move

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


    private static bool ValidateReplace(int y, int x, int pieceId)
    {
        int cellOccupied = Matrix.Instance.GetMatrix()[y][x];

        //empy cell
        if (cellOccupied == 0) { return true; }

        //reflactor is allowed to replace, but only with an angler or wall
        return pieceId % 10 == 4 && (cellOccupied % 10 == 3 || cellOccupied % 10 == 5);
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
