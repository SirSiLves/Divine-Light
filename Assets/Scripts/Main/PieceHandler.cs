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
        Visualize();
    }

    internal void HandleReplace(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int toCellId = Matrix.ConvertPostionToCellId(prepareMove.toPosition);

        DoReplace(fromCellId, toCellId);
        Visualize();
    }

    internal void HandleRotate(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int newCharacterValue = prepareMove.characterValue;

        DoRotate(fromCellId, newCharacterValue);
        Visualize();
    }
    #endregion


    #region VISUALIZE LAST MOVE FROM HISTORY
    public void Visualize()
    {
        ICommand command = Executor.Instance.GetLastCommand();

        if (command.GetType() == typeof(MoveCommand))
        {
            VisualizeMove((MoveCommand)command);
        }
        else if (command.GetType() == typeof(ReplaceCommand))
        {
            VisualizeReplace((ReplaceCommand)command);
        }
        else if (command.GetType() == typeof(RotationCommand))
        {
            VisualizeRotate((RotationCommand) command);
        }
        else
        {
            throw new Exception("Visualize command was not found: " + command.GetType());
        }
    }

    private void VisualizeMove(MoveCommand moveCommand)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(moveCommand.fromCellId);
        int[] toXY = Matrix.Instance.GetCoordinates(moveCommand.toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetClickedPiece(fromPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
    }

    private void VisualizeReplace(ReplaceCommand replaceCommand)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(replaceCommand.fromCellId);
        int[] toXY = Matrix.Instance.GetCoordinates(replaceCommand.toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetClickedPiece(fromPosition);
        Piece targetPiece = GetClickedPiece(toPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
        OnMoveEvent?.Invoke(targetPiece.id, fromPosition);
    }

    public void VisualizeRotate(RotationCommand rotationCommand)
    {
        int[] fromXY = Matrix.Instance.GetCoordinates(rotationCommand.fromCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);

        Piece touchedPiece = GetClickedPiece(fromPosition);

        int degrees = RotateValidator.GetDegrees(rotationCommand.newCharacter);

        touchedPiece.character = rotationCommand.newCharacter;

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


