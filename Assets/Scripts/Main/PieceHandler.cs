using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{

    public event Action<int, Vector2> OnMoveEvent;
    public event Action<int, int> OnRotateEvent;
    public event Action<int, bool> OnDestroyEvent;


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

    #region UI INPUT HANDLING
    internal void HanldeMove(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int toCellId = Matrix.ConvertPostionToCellId(prepareMove.toPosition);

        DoMove(fromCellId, toCellId);
        Visualize(false);
    }

    internal void HandleReplace(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int toCellId = Matrix.ConvertPostionToCellId(prepareMove.toPosition);

        DoReplace(fromCellId, toCellId);
        Visualize(false);
    }

    internal void HandleRotate(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int newCharacterValue = prepareMove.characterValue;

        DoRotate(fromCellId, newCharacterValue);
        Visualize(false);
    }

    internal void HandleDestroy(Piece piece)
    {
        int pieceCellId = Matrix.ConvertPostionToCellId(piece.transform.position);

        DoDestroy(pieceCellId);
        Visualize(false);
    }

    internal void HandleRevert()
    {
        Visualize(true);
        DoRevert();
    }
    #endregion


    #region VISUALIZE LAST MOVE FROM COMMAND HISTORY
    public void Visualize(bool revert)
    {
        ICommand command = Executor.Instance.GetLastCommand();
        if (command == null) { return; }

        if (command.GetType() == typeof(MoveCommand))
        {
            MoveCommand moveCommand = (MoveCommand)command;
            if(revert) { VisualizeMove(moveCommand.toCellId, moveCommand.fromCellId); }
            else { VisualizeMove(moveCommand.fromCellId, moveCommand.toCellId); }
        }
        else if (command.GetType() == typeof(ReplaceCommand))
        {
            ReplaceCommand replaceCommand = (ReplaceCommand)command;
            if (revert) { VisualizeReplace(replaceCommand.toCellId, replaceCommand.fromCellId); }
            else { VisualizeReplace(replaceCommand.fromCellId, replaceCommand.toCellId); }
        }
        else if (command.GetType() == typeof(RotationCommand))
        {
            RotationCommand rotationCommand = (RotationCommand)command;
            if (revert) { VisualizeRotate(rotationCommand.fromCellId, rotationCommand.oldCharacter); }
            else { VisualizeRotate(rotationCommand.fromCellId, rotationCommand.newCharacter); }
        }
        else if(command.GetType() == typeof(DestroyCommand))
        {
            DestroyCommand destroyCommand = (DestroyCommand)command;
            if (revert) { VisualizeDestroy(destroyCommand.pieceCellId, destroyCommand.characterValue, false); }
            else { VisualizeDestroy(destroyCommand.pieceCellId, destroyCommand.characterValue, true); }
        }
        else
        {
            throw new Exception("Visualize command was not found: " + command.GetType());
        }
    }

    private void VisualizeMove(int fromCellid, int toCellId)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(fromCellid);
        int[] toXY = Matrix.Instance.GetCoordinates(toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
    }

    private void VisualizeReplace(int fromCellid, int toCellId)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(fromCellid);
        int[] toXY = Matrix.Instance.GetCoordinates(toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);
        Piece targetPiece = GetPieceFromPosition(toPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
        OnMoveEvent?.Invoke(targetPiece.id, fromPosition);
    }

    public void VisualizeRotate(int fromCellId, int characterValue)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(fromCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);

        int degrees = RotateValidator.GetDegrees(characterValue);

        touchedPiece.character = characterValue;

        OnRotateEvent?.Invoke(touchedPiece.id, degrees);
    }

    public void VisualizeDestroy(int pieceCellId, int characterValue, bool destroy)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(pieceCellId);

        Vector2 piecePosition = new Vector2(fromXY[0], fromXY[1]);

        if(destroy)
        {
            Piece destroyedPiece = GetPieceFromPosition(piecePosition);
            OnDestroyEvent?.Invoke(destroyedPiece.id, destroy);
        }
        else
        {
            //revive
            Piece destroyedPiece = GetDisabledPieces(piecePosition, characterValue);
            OnDestroyEvent?.Invoke(destroyedPiece.id, destroy);
        }
    }
    #endregion


    #region DO MOVE ON MATRIX WIHTOUT VISUALIZE
    private void DoMove(int fromCellId, int toCellId)
    {
        Executor.Instance.Execute(new MoveCommand(fromCellId, toCellId));
    }

    private void DoReplace(int fromCellId, int toCellId)
    {
        Executor.Instance.Execute(new ReplaceCommand(fromCellId, toCellId));
    }

    private void DoRotate(int fromCellId, int newCharacter)
    {
        Executor.Instance.Execute(new RotationCommand(fromCellId, newCharacter));
    }

    private void DoRevert()
    {
        Executor.Instance.Revert();
    }
    private void DoDestroy(int pieceCellId)
    {
        Executor.Instance.Execute(new DestroyCommand(pieceCellId));
    }
    #endregion



    private static Piece GetPieceFromPosition(Vector2 clickedPosition)
    {
        Piece[] pieces = FindObjectsOfType<Piece>();

        return Array.Find(pieces, piece =>
            piece.transform.position.y == clickedPosition.y && piece.transform.position.x == clickedPosition.x
        );
    }


    private static Piece GetDisabledPieces(Vector2 piecePosition, int character)
    {
        return Array.Find(Resources.FindObjectsOfTypeAll<Piece>().ToArray(), piece =>
            !piece.gameObject.activeSelf && piece.character == character &&
            piece.transform.position.y == piecePosition.y &&
            piece.transform.position.x == piecePosition.x
        );
    }



}


