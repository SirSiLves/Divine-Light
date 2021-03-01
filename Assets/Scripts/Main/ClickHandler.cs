using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickHandler: MonoBehaviour
{

    public Piece touchedPiece { get; set; }
    private PlayerChanger playerChanger;
    private FieldHandler fieldHandler;
    private RotationHandler rotationHandler;
    private Cell[] cells;
        


    private void Start()
    {
        playerChanger = FindObjectOfType<PlayerChanger>();
        fieldHandler = FindObjectOfType<FieldHandler>();
        rotationHandler = FindObjectOfType<RotationHandler>();
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
        if (rotationHandler.isRotating) { return; }

        Piece clickedPiece = GetClickedPiece();
        
        Cell targetCell = GetClickedCell(cells);
        List<Cell> lastValidated = FindObjectOfType<FieldHandler>().GetLastValidatedCells();

        if (MoveIsReady(lastValidated, targetCell)) {
            RevertMarkup();
            DoMove(targetCell);
            return;
        }

        if (MovePreparation(clickedPiece)) { return; }
    }


    private bool MovePreparation(Piece clickedPiece)
    {
        if (touchedPiece == null && clickedPiece == null) { return true; }

        if(touchedPiece == null && playerChanger.firstTouched && clickedPiece != null && clickedPiece.GetPlayer() != playerChanger.isPlaying) {
            RevertMarkup();
            return true;
        }

        if (touchedPiece == null)
        {
            touchedPiece = clickedPiece;
            MarkupFields();

            if (!playerChanger.firstTouched) { playerChanger.FirstTouched(touchedPiece); }

            return true;
        }

        if (touchedPiece == clickedPiece)
        {
            touchedPiece = null;
            RevertMarkup();
            rotationHandler.DisableRotation();
            return true;
        }

        if (clickedPiece != null && touchedPiece != clickedPiece && clickedPiece.GetPlayer() == playerChanger.isPlaying)
        {
            RevertMarkup();
            touchedPiece = clickedPiece;
            MarkupFields();
            return true;
        }

        return false;
    }


    private bool MoveIsReady(List<Cell> lastValidated, Cell targetCell)
    {
        if(touchedPiece == null) { return false; }

        if (lastValidated.Contains(targetCell) && touchedPiece.GetPlayer() == playerChanger.isPlaying)
        {
            return true;
        }

        return false;
    }


    public void MarkupFields()
    {
        rotationHandler.ActiateRotate();
        fieldHandler.markupEvent.Invoke();
    }


    public void RevertMarkup()
    {
        fieldHandler.removeMarkupEvent.Invoke();
    }


    private void DoMove(Cell targetCell)
    {
        Matrix matrix = FindObjectOfType<GameManager>().matrix;

        if(targetCell == null) { return; }

        MoveCommand moveCommand = new MoveCommand(touchedPiece, targetCell, matrix);
        new Drawer(moveCommand).Draw();

        MoveDone();
    }


    private void MoveDone()
    {
        touchedPiece = null;

        rotationHandler.DisableRotation();

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
