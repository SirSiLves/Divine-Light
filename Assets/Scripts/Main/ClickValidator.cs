using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            HandleAction();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //TODO arrow markup
            //HandleMarkup();
        }
    }

 
    private void HandleAction()
    {
        if (playerChanger.isLightOn) { return; }

        Piece clickedPiece = GetClickedPiece();
        
        Cell targetCell = GetClickedCell(cells);
        List<Cell> lastValidated = FindObjectOfType<FieldValidator>().GetLastValidatedCells();

        if (MoveIsReady(lastValidated, targetCell)) {
            DoMove(targetCell);
        }

        if (MovePreparation(clickedPiece)) { return; }
    }


    private bool MovePreparation(Piece clickedPiece)
    {
        if (movingFigure == null && clickedPiece == null) { return true; }

        if(movingFigure == null && playerChanger.firstTouched && clickedPiece != null && clickedPiece.GetPlayer() != playerChanger.isPlaying) {
            RevertMarkup();
            return true;
        }

        if (movingFigure == null)
        {
            movingFigure = clickedPiece;
            CollectPossibleFields();

            if (!playerChanger.firstTouched) { playerChanger.FirstTouched(movingFigure); }

            return true;
        }

        if (movingFigure == clickedPiece)
        {
            movingFigure = null;
            RevertMarkup();
            return true;
        }

        if (clickedPiece != null && movingFigure != clickedPiece && clickedPiece.GetPlayer() == playerChanger.isPlaying)
        {
            RevertMarkup();
            movingFigure = clickedPiece;
            CollectPossibleFields();
            return true;
        }

        return false;
    }


    private bool MoveIsReady(List<Cell> lastValidated, Cell targetCell)
    {
        if(movingFigure == null) { return false; }

        if (lastValidated.Contains(targetCell) && movingFigure.GetPlayer() == playerChanger.isPlaying)
        {
            return true;
        }

        return false;
    }


    private List<Cell> CollectPossibleFields()
    {
        List<Cell> possibleCells = FindObjectOfType<FieldValidator>().CollectPossibleFields(movingFigure);
        if(possibleCells.Count == 0) { return possibleCells; }

        Color markupColor = FindObjectOfType<CellFactory>().possibleFields;

        MarkupFieldsCommand markupCommand = new MarkupFieldsCommand(possibleCells, markupColor);
        new Drawer(markupCommand).Draw();

        return possibleCells;
    }


    private void RevertMarkup()
    {
        Color markupColor = FindObjectOfType<CellFactory>().defaultFields;
        MarkupFieldsCommand markupCommand = new MarkupFieldsCommand(cells.OfType<Cell>().ToList(), markupColor);
        new Drawer(markupCommand).Draw();
    }


    private void DoMove(Cell targetCell)
    {
        Matrix matrix = FindObjectOfType<GameManager>().matrix;

        if(targetCell == null) { return; }

        MoveCommand moveCommand = new MoveCommand(movingFigure, targetCell, matrix);
        new Drawer(moveCommand).Draw();

        MoveDone();
    }


    private void MoveDone()
    {
        movingFigure = null;
        playerChanger.TogglePlaying();
        RevertMarkup();
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
