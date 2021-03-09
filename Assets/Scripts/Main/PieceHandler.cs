using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{

    #region PIECE_HANDLER_SINGLETON_SETUP
    private static PieceHandler _instance;

    public static PieceHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Piece Handler");
                gO.AddComponent<PieceHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    internal void HanldeMove(PrepareMove prepareMove)
    {
        Piece touchedPiece = GetClickedPiece(prepareMove.fromPosition);

        VisualizeMove(touchedPiece, prepareMove.toPosition);
        DoMove(touchedPiece, prepareMove.fromPosition, prepareMove.toPosition);
    }

    internal void HandleReplace(PrepareMove prepareMove)
    {
        Piece touchedPiece = GetClickedPiece(prepareMove.fromPosition);
        Piece targetPiece = GetClickedPiece(prepareMove.toPosition);

        VisualizeReplace(touchedPiece, targetPiece, prepareMove.fromPosition, prepareMove.toPosition);
        DoReplace(touchedPiece, targetPiece, prepareMove.fromPosition, prepareMove.toPosition);
    }

    internal void HandleRotate(PrepareMove prepareMove, int newPieceId)
    {
        Piece touchedPiece = GetClickedPiece(prepareMove.fromPosition);

        DoRotate(touchedPiece, prepareMove.fromPosition, touchedPiece.id, newPieceId);
    }



    private void DoReplace(Piece touchedPiece, Piece targetPiece, Vector2 fromPosition, Vector2 toPosition)
    {
        Executor.Instance.Execute(new ReplaceCommand(touchedPiece, targetPiece, fromPosition, toPosition));
    }

    private void DoMove(Piece piece, Vector2 fromPosition, Vector2 toPosition)
    {
        Executor.Instance.Execute(new MoveCommand(piece, fromPosition, toPosition));
    }

    private void DoRotate(Piece touchedPiece, Vector2 fromPosition, int oldPieceId, int newPieceId)
    {
        Executor.Instance.Execute(new RotationCommand(touchedPiece, fromPosition, oldPieceId, newPieceId));
    }



    public void VisualizeMove(Piece touchedPiece, Vector2 targetPosition)
    {
        touchedPiece.transform.position = new Vector2(targetPosition.x, targetPosition.y);
    }

    public void VisualizeReplace(Piece touchedPiece, Piece targetPiece, Vector2 fromPosition, Vector2 toPosition)
    {
        touchedPiece.transform.position = new Vector2(toPosition.x, toPosition.y);
        targetPiece.transform.position = new Vector2(fromPosition.x, fromPosition.y);
    }

    public void VisualizeRotate(Vector2 clickedPosition, int degrees)
    {
        Piece touchedPiece = GetClickedPiece(clickedPosition);
        touchedPiece.transform.rotation = Quaternion.Euler(0, 0, degrees);
    }





    //TODO
    private void Destroy()
    {

    }



    private static Piece GetClickedPiece(Vector2 clickedPosition)
    {
        Piece[] pieces = FindObjectsOfType<Piece>();

        return Array.Find(pieces, piece =>
            piece.transform.position.y == clickedPosition.y && piece.transform.position.x == clickedPosition.x
        );
    }

}


