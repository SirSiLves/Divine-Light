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
        Piece targetPiece = GetClickedPiece(prepareMove.toPosition);

        if (touchedPiece && targetPiece)
        {
            DoReplace(touchedPiece, targetPiece, prepareMove.fromPosition, prepareMove.toPosition);
        }
        else
        {
            DoMove(touchedPiece, prepareMove.fromPosition, prepareMove.toPosition);
        }
    }

    private void DoReplace(Piece touchedPiece, Piece targetPiece, Vector2 fromPosition, Vector2 toPosition)
    {
        Executor.Instance.Execute(new ReplaceCommand(touchedPiece, targetPiece, fromPosition, toPosition));
    }

    private void DoMove(Piece piece, Vector2 fromPosition, Vector2 toPosition)
    {
        Executor.Instance.Execute(new MoveCommand(piece, fromPosition, toPosition));
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


