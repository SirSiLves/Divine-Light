using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateValidator
{


    public static int GetDegrees(int pieceId)
    {
        int rotationValue = (pieceId % 100) - (pieceId % 10);

        switch (rotationValue)
        {
            case 0:
                return 0;
            case 10:
                return 90;
            case 20:
                return 180;
            case 30:
                return 270;
            default:
                Debug.LogError("No mapping for rotation value " + rotationValue);
                return 0;
        }
    }

    public static int GetNewDegrees(int pieceId, int currentDegrees)
    {
        int pieceType = pieceId % 10;

        // set sun rotation
        if (pieceType == 1)
        {
            if (pieceId < 100)
            {
                return currentDegrees == 0 ? 90 : 0;
            }
            else
            {
                return currentDegrees == 180 ? 270 : 180;
            }
        }
        // set reflector rotation
        else if (pieceType == 4)
        {
            return currentDegrees == 0 ? 90 : 0;
        }
        // set king rotation
        else if(pieceType == 2)
        {
            return currentDegrees;
        }
        // set other figures
        else
        {
            return currentDegrees == 0 ? 270 : currentDegrees -= 90;
        }
    }

    public static int GetNewPieceId(int pieceId, int degrees)
    {
        int tempId = pieceId % 10;
        pieceId -= pieceId % 100;

        int rotateId = GetRotationId(degrees);

        pieceId += rotateId + tempId;

        return pieceId;
    }



    private static int GetRotationId(int degrees)
    {
        switch (degrees)
        {
            case 0:
                return 0;
            case 90:
                return 1 * 10;
            case 180:
                return 2 * 10;
            case 270:
                return 3 * 10;
            default:
                Debug.LogError("No mapping for degrees value " + degrees);
                return 0;
        }
    }


}
