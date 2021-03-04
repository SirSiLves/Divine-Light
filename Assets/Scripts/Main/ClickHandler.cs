using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ClickHandler: MonoBehaviour
{

    public Piece touchedPiece { get; set; }
    private PlayerChanger playerChanger;
    private RotationHandler rotationHandler;
    private Cell[] cells;
        


    private void Start()
    {
        playerChanger = FindObjectOfType<PlayerChanger>();
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
            Matrix matrix = FindObjectOfType<GameManager>().matrix;

            int pieceId = matrix.GetPieceId(targetCell.GetCellId());

            if (pieceId == 0)
            {
                DoMove(targetCell, matrix);
            }
            else
            {
                DoReplace(targetCell, matrix);
            }

            RevertMarkup();

            return;
        }

        MovePreparation(clickedPiece);
    }


    private bool MovePreparation(Piece clickedPiece)
    {
        if (touchedPiece == null && clickedPiece == null) { return true; }

        if(touchedPiece == null && playerChanger.firstTouched &&
            clickedPiece != null && clickedPiece.GetPlayer() != playerChanger.isPlaying) {

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
        FindObjectOfType<FieldHandler>().markupEvent.Invoke();
        FindObjectOfType<FieldHandler>().markupTouchedEvent.Invoke();
    }


    public void RevertMarkup()
    {
        FindObjectOfType<FieldHandler>().removeMarkupEvent.Invoke();
    }


    private void DoMove(Cell targetCell, Matrix matrix)
    {
        if(targetCell == null) { return; }

        FindObjectOfType<GameManager>().executor.Execute(new MoveCommand(touchedPiece, targetCell, matrix));


        MoveDone();
    }


    private void MoveDone()
    {
        touchedPiece = null;

        rotationHandler.DisableRotation();

        playerChanger.TogglePlaying();
    }


    private void DoReplace(Cell targetCell, Matrix matrix)
    {
        Piece targetPiece = Array.Find(FindObjectsOfType<Piece>(), p =>
        {
            return p.transform.position.y == targetCell.transform.position.y
            && p.transform.position.x == targetCell.transform.position.x;
        });

        FindObjectOfType<GameManager>().executor.Execute(new ReplaceCommand(touchedPiece, targetPiece, targetCell, cells, matrix));

        MoveDone();
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
