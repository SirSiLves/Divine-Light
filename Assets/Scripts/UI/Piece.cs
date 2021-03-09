﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    public int id { get; set; }
    public int character { get; set; }
    public int playerIndex { get; private set; }


    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        PieceHandler.Instance.OnMoveEvent += Instance_OnMoveEvent;
        PieceHandler.Instance.OnRotateEvent += Instance_OnRotateEvent;
    }

    private void OnDestroy()
    {
        PieceHandler.Instance.OnMoveEvent -= Instance_OnMoveEvent;
        PieceHandler.Instance.OnRotateEvent -= Instance_OnRotateEvent;
    }

    private void Instance_OnRotateEvent(int id, int degrees)
    {
        if (id == this.id)
        {
            transform.rotation = Quaternion.Euler(0, 0, degrees);
        }
    }

    private void Instance_OnMoveEvent(int id, Vector2 toPsition)
    {
        if (id == this.id)
        {
            transform.position = new Vector2(toPsition.x, toPsition.y);
        }
    }

    public void DrawPiece(int y, int x)
    {
        //player
        playerIndex = PlayerHandler.GetPlayerIndex(character);

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
        int rotation = RotateValidator.GetDegrees(character);
        transform.Rotate(0, 0, rotation);
    }




}

