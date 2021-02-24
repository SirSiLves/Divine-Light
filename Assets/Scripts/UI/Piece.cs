using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    private Player player;

    public int id { get; set; }
    public bool exchangeable { get; set; }
    public bool restrictedRotation { get; set; }

    public void SetPlayer(Player player)
    {
        this.player = player;
        transform.GetComponentInChildren<SpriteRenderer>().color = player.GetColor();
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void DrawPiece(int y, int x)
    {
        // position
        this.transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

        // size
        this.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // position in opject tree
        this.transform.parent = transform;

        // rotation
        int rotation = this.GetRotation(id);
        this.transform.Rotate(0, 0, rotation);
    }

    private int GetRotation(int pieceId)
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

