using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickValidator: MonoBehaviour
{

    private GameManager gameManager;
    private Piece movingFigure;
    private Matrix matrix;
    private Cell[] cells;
    private bool firstTouched = false;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        matrix = gameManager.matrix;
        cells = FindObjectsOfType<Cell>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameManager.isLightOn) { return; }
            if (!firstTouched) { FirstTouched(); }

            HandleMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //TODO arrow markup
            //HandleMarkup();
        }
    }

    private void FirstTouched()
    {
        if (gameManager.isPlaying == null && movingFigure != null)
        {
            gameManager.isPlaying = movingFigure.GetPlayer();
            gameManager.UpdatePlayingDisplay();

            this.firstTouched = true;
        }
    }

    private void HandleMove()
    {
        Piece clickedPiece = GetClickedPiece();


        if(movingFigure == null || clickedPiece != null)
        {
            movingFigure = clickedPiece;
        }
        else if (movingFigure.GetPlayer() == gameManager.isPlaying)
        {
            DoMove();
        }

    }


    private void DoMove()
    {
        Cell targetCell = GetClickedCell();

        if(targetCell == null) { return;  }

        MoveCommand move = new MoveCommand(movingFigure, targetCell, matrix);
        new Drawer(move).Draw();

        movingFigure = null;
        gameManager.TogglePlaying();
    }


    private Cell GetClickedCell()
    {
        Vector2 gridPos = GetClickedGridPos();

        return Array.Find(cells, cell =>
            cell.transform.position.y == gridPos.y && cell.transform.position.x == gridPos.x
        );
    }


    private Piece GetClickedPiece()
    {
        Piece[] pieces = FindObjectsOfType<Piece>();

        Vector2 gridPos = GetClickedGridPos();

        return Array.Find(pieces, piece =>
            piece.transform.position.y == gridPos.y && piece.transform.position.x == gridPos.x
        );
    }


    private static Vector2 GetClickedGridPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        return gridPos;
    }













}
