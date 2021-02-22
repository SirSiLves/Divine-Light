using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickValidator: MonoBehaviour
{

    private Piece movingFigure;
    private Matrix matrix;
    private Piece[] pieces;
    private Cell[] cells;


    private void Start()
    {
        matrix = FindObjectOfType<GameManager>().GetMatrix();
        pieces = FindObjectsOfType<Piece>();
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
        if (movingFigure != null)
        {
            Piece collidedPiece = GetClickedPiece();
            Cell targetCell = GetClickedCell();

            if (collidedPiece != null)
            {
                //TODO handle replace
            }
            else
            {
                MoveCommand move = new MoveCommand(movingFigure, targetCell, matrix);
                new Drawer(move).Draw();

                movingFigure = null;
            }
        }
        else
        {
            movingFigure = GetClickedPiece();
        }

    }

    private Cell GetClickedCell()
    {
        Vector2 gridPos = GetGridPos();

        return Array.Find(cells, cell =>
            cell.transform.position.y == gridPos.y && cell.transform.position.x == gridPos.x
        );
    }

    private Piece GetClickedPiece()
    {
        Vector2 gridPos = GetGridPos();

        return Array.Find(pieces, piece =>
            piece.transform.position.y == gridPos.y && piece.transform.position.x == gridPos.x
        );
    }

    private static Vector2 GetGridPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        return gridPos;
    }













}
