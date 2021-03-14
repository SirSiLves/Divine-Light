using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceHandler : MonoBehaviour
{

    public event Action<int, Vector2> OnMoveEvent;
    public event Action<int, int> OnRotateEvent;
    public event Action<int> OnDestroyEvent;


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

    internal void HandleSwap(PrepareMove prepareMove)
    {
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int toCellId = Matrix.ConvertPostionToCellId(prepareMove.toPosition);

        DoSwap(fromCellId, toCellId);
        Visualize();
    }

    internal void HandleDestroy(Piece piece)
    {
        int pieceCellId = Matrix.ConvertPostionToCellId(piece.transform.position);

        DoDestroy(pieceCellId);
        Visualize();
    }

    internal void HandleRevert(ICommand command)
    {
        DoRevert();
        DrawMatrix(command.GetFormerMatrix());
    }
    #endregion


    #region VISUALIZE LAST MOVE FROM COMMAND HISTORY
    public void Visualize()
    {
        ICommand command = Executor.Instance.GetLastCommand();
        if (command == null) { return; }

        if (command.GetType() == typeof(MoveCommand))
        {
            MoveCommand moveCommand = (MoveCommand)command;
            VisualizeMove(moveCommand.fromCellId, moveCommand.toCellId);
        }
        else if (command.GetType() == typeof(SwapCommand))
        {
            SwapCommand swapCommand = (SwapCommand)command;
            VisualizeSwap(swapCommand.fromCellId, swapCommand.toCellId);
        }
        else if (command.GetType() == typeof(RotationCommand))
        {
            RotationCommand rotationCommand = (RotationCommand)command;
            VisualizeRotate(rotationCommand.fromCellId, rotationCommand.newCharacter);
        }
        else if(command.GetType() == typeof(DestroyCommand))
        {
            DestroyCommand destroyCommand = (DestroyCommand)command;
            VisualizeDestroy(destroyCommand.fromCellId, destroyCommand.characterValue);
        }
        else
        {
            throw new Exception("Visualize command was not found: " + command.GetType());
        }
    }

    private void VisualizeMove(int fromCellid, int toCellId)
    {
        Matrix matrix = Matrix.Instance;

        int[] fromXY = Matrix.GetCoordinates(matrix.GetMatrix(), fromCellid);
        int[] toXY = Matrix.GetCoordinates(matrix.GetMatrix(), toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
    }

    private void VisualizeSwap(int fromCellid, int toCellId)
    {
        Matrix matrix = Matrix.Instance;

        int[] fromXY = Matrix.GetCoordinates(matrix.GetMatrix(), fromCellid);
        int[] toXY = Matrix.GetCoordinates(matrix.GetMatrix(), toCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);
        Vector2 toPosition = new Vector2(toXY[0], toXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);
        Piece targetPiece = GetPieceFromPosition(toPosition);

        OnMoveEvent?.Invoke(touchedPiece.id, toPosition);
        OnMoveEvent?.Invoke(targetPiece.id, fromPosition);
    }

    internal void VisualizeRotate(int fromCellId, int characterValue)
    {
        Matrix matrix = Matrix.Instance;

        int[] fromXY = Matrix.GetCoordinates(matrix.GetMatrix(), fromCellId);

        Vector2 fromPosition = new Vector2(fromXY[0], fromXY[1]);

        Piece touchedPiece = GetPieceFromPosition(fromPosition);

        int degrees = RotateValidator.GetDegrees(characterValue);

        touchedPiece.character = characterValue;

        OnRotateEvent?.Invoke(touchedPiece.id, degrees);
    }

    private void VisualizeDestroy(int pieceCellId, int characterValue)
    {
        Matrix matrix = Matrix.Instance;

        int[] fromXY = Matrix.GetCoordinates(matrix.GetMatrix(), pieceCellId);

        Vector2 piecePosition = new Vector2(fromXY[0], fromXY[1]);

        Piece destroyedPiece = GetPieceFromPosition(piecePosition);
        OnDestroyEvent?.Invoke(destroyedPiece.id);
    }
    #endregion


    #region DO MOVE ON MATRIX WIHTOUT VISUALIZE
    private void DoMove(int fromCellId, int toCellId)
    {
        Executor.Instance.Execute(new MoveCommand(fromCellId, toCellId));
    }

    private void DoSwap(int fromCellId, int toCellId)
    {
        Executor.Instance.Execute(new SwapCommand(fromCellId, toCellId));
    }

    internal void DoRotate(int fromCellId, int newCharacter)
    {
        Executor.Instance.Execute(new RotationCommand(fromCellId, newCharacter));
    }

    private void DoDestroy(int pieceCellId)
    {
        Executor.Instance.Execute(new DestroyCommand(pieceCellId));
    }

    private void DoRevert()
    {
        Executor.Instance.Revert();
    }

    #endregion


    #region VISUALIZE BOARD FROM MATRIX
    public void DrawMatrix(int[][] matrix)
    {
        List<Piece> pieces = Resources.FindObjectsOfTypeAll<Piece>().ToList();
        List<Cell> cells = FindObjectsOfType<Cell>().ToList();

        // disable all
        Array.ForEach(pieces.ToArray(), p => p.gameObject.SetActive(false));

        // load matrix
        Array.ForEach(cells.ToArray(), cell =>
        {
            int character = Matrix.GetCharacter(matrix, cell.GetCellId());
            if (character != 0)
            {

                Piece piece = pieces.Find(p =>
                {
                    // reset rotation value
                    int pieceNormalized = p.character - p.character % 100 + p.character % 10;
                    int characterNormalized = character - character % 100 + character % 10;

                    return pieceNormalized == characterNormalized;
                });

                piece.gameObject.SetActive(true);

                OnMoveEvent?.Invoke(piece.id, cell.transform.position);
                OnRotateEvent?.Invoke(piece.id, RotateValidator.GetDegrees(character));

                pieces.Remove(piece); // to prevent move same figure twice
            }
        });

        //// disable all destroyed
        //if (pieces.Count > 0)
        //{
        //    Array.ForEach(pieces.ToArray(), p => p.gameObject.SetActive(false));
        //}
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


