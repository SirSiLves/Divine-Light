using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{



    internal void DrawPiece(int y, int x, int pieceValue)
    {

        // position
        this.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

        // size
        this.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // position in opject tree
        this.transform.parent = transform;

        // rotation
        int rotationMapping = (pieceValue % 100) - (pieceValue % 10);
        int rotation = this.GetRotation(rotationMapping);
        this.transform.Rotate(0, 0, rotation);

        // update name
        string player = pieceValue < 100 ? "p1" : "p2";
        this.name += " - " + player + " - " + rotation;
    }


    private int GetRotation(int rotation)
    {

        switch (rotation)
        {
            case 0:
                return 0;
            case 10:
                return -90;
            case 20:
                return -180;
            case 30:
                return -270;
            default:
                Debug.LogError("No mapping for rotation value " + rotation);
                return 0;
        }
    }
}

