using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickValidator: MonoBehaviour
{

    private Piece movingFigure;
    private PlayerChanger playerChanger;
    private Cell[] cells;



    private void Start()
    {
        playerChanger = FindObjectOfType<PlayerChanger>();
        cells = FindObjectsOfType<Cell>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMove();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //TODO arrow markup
            //HandleMarkup();
        }
    }

 
    private void HandleMove()
    {
        if (playerChanger.isLightOn) { return; }
        if (!playerChanger.firstTouched) { playerChanger.FirstTouched(movingFigure); }

        Piece clickedPiece = GetClickedPiece();

        if(movingFigure == null || clickedPiece != null)
        {
            movingFigure = clickedPiece;

            CollectPossibleFields();
        }
        else if (movingFigure.GetPlayer() == playerChanger.isPlaying)
        {
            DoMove();
        }

    }


    private void CollectPossibleFields()
    {
        Matrix matrix = FindObjectOfType<GameManager>().matrix;

        PossibleFieldsCommand pFieldCommand = new PossibleFieldsCommand(cells, movingFigure, matrix);
        new Drawer(pFieldCommand).Draw();
    }


    private void DoMove()
    {
        Matrix matrix = FindObjectOfType<GameManager>().matrix;

        Cell targetCell = GetClickedCell(cells);

        if(targetCell == null) { return; }

        MoveCommand moveCommand = new MoveCommand(movingFigure, targetCell, matrix);
        new Drawer(moveCommand).Draw();

        movingFigure = null;
        playerChanger.TogglePlaying();
    }


    private static Cell GetClickedCell(Cell[] cells)
    {
        Vector2 gridPos = GetClickedGridPos();

        return Array.Find(cells, cell =>
            cell.transform.position.y == gridPos.y && cell.transform.position.x == gridPos.x
        );
    }


    private static Piece GetClickedPiece()
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
