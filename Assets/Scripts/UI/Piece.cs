﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int id { get; set; }
    public int playerIndex { get; private set; }


    public void DrawPiece(int y, int x)
    {
        //player
        playerIndex = PlayerHandler.GetPlayerIndex(id);

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
        int rotation = RotateValidator.GetDegrees(id);
        transform.Rotate(0, 0, rotation);
    }




}

