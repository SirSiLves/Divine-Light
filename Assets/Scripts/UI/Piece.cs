using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int id { get; set; }
    public int playerIndex { get; private set; }
    public bool exchangeable { get; set; }
    public bool restrictedRotation { get; set; }
    public bool restrictedMove { get; set; }


    public void DrawPiece(int y, int x)
    {
        //player
        playerIndex = id < 100 ? 0 : 1;

        //color
        Color playerColor = playerIndex == 0 ? PlayerHandler.Instance.player1.GetFigure() : PlayerHandler.Instance.player2.GetFigure();
        this.transform.GetComponentInChildren<SpriteRenderer>().color = playerColor;

        // position
        this.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

        // size
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // position in opject tree
        this.transform.parent = transform;

        // rotation
        int rotation = GetRotationDegrees(id);
        this.transform.Rotate(0, 0, rotation);
    }


    private int GetRotationDegrees(int pieceId)
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

}

