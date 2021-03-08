using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int id { get; set; }
    public int playerIndex { get; private set; }
    public bool exchangeable { get; set; }
    public bool restrictedMove { get; set; }


    public void DrawPiece(int y, int x)
    {
        //player
        playerIndex = id < 100 ? 0 : 1;

        //color
        Color playerColor = playerIndex == 0 ? PlayerHandler.Instance.player1.GetFigure() : PlayerHandler.Instance.player2.GetFigure();
        transform.GetComponentInChildren<SpriteRenderer>().color = playerColor;

        // position
        transform.position = new Vector2(transform.position.x + x, transform.position.y + y);

        // size
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // position in opject tree
        transform.parent = transform;

        // rotation
        int rotation = RotationHandler.Instance.GetDegrees(id);
        transform.Rotate(0, 0, rotation);
    }




}

