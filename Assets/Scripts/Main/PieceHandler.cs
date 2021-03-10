using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{

    public event Action<int, Vector2> OnMoveEvent;
    public event Action<int, int> OnRotateEvent;


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


    internal void HandleRevert()
    {
        Visualize(true);
        DoRevert();
    }
    #endregion


    #region VISUALIZE LAST MOVE FROM HISTORY
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

        Piece touchedPiece = GetClickedPiece(fromPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
    }

    private void VisualizeReplace(int fromCellid, int toCellId)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(fromCellid);
        int[] toXY = Matrix.Instance.GetCoordinates(toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetClickedPiece(fromPosition);
        Piece targetPiece = GetClickedPiece(toPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
        OnMoveEvent?.Invoke(targetPiece.id, fromPosition);
    }

    public void VisualizeRotate(int fromCellId, int characterValue)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(fromCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);

        Piece touchedPiece = GetClickedPiece(fromPosition);

        int degrees = RotateValidator.GetDegrees(characterValue);

        touchedPiece.character = characterValue;

        OnRotateEvent?.Invoke(touchedPiece.id, degrees);
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
    #endregion


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


