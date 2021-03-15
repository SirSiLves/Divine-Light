using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CellHandler : MonoBehaviour
{

    public event Action<int, Color> OnMarkupEvent;

    private CellFactory cellFactory;


    #region CELL_HANDLER_SINGLETON_SETUP
    private static CellHandler _instance;

    public static CellHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Cell Handler");
                gO.AddComponent<CellHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion


    private void Start()
    {
        cellFactory = FindObjectOfType<CellFactory>();
    }


    public void MarkupHistoryField(ICommand command)
    {
        // markup last field from history
        if (command == null) { return; }
        if (command.GetType() == typeof(InitializeCommand)) { return; }
        if (command.GetType() == typeof(MoveCommand))
        {
            MoveCommand moveCommand = (MoveCommand)command;
            SendMarkupEvent(moveCommand.fromCellId, cellFactory.historyFromFields);
            SendMarkupEvent(moveCommand.toCellId, cellFactory.historyToFields);
        }
        else if (command.GetType() == typeof(SwapCommand))
        {
            SwapCommand swapCommand = (SwapCommand)command;
            SendMarkupEvent(swapCommand.fromCellId, cellFactory.historyFromFields);
            SendMarkupEvent(swapCommand.toCellId, cellFactory.historyToFields);
        }
        else if (command.GetType() == typeof(RotationCommand))
        {
            RotationCommand rotationCommand = (RotationCommand)command;
            SendMarkupEvent(rotationCommand.fromCellId, cellFactory.historyFromFields);
        }
        else if (command.GetType() == typeof(DestroyCommand))
        {
            DestroyCommand destroyCommand = (DestroyCommand)command;
            SendMarkupEvent(destroyCommand.fromCellId, cellFactory.deathFields);

            ICommand commandBefore = Executor.Instance.GetCommandBefore(command);
            MarkupHistoryField(commandBefore);
        }
    }

    public void MarkupPossibleFields(PrepareMove prepareMove)
    {
        // markup possible cells
        Array.ForEach(prepareMove.possibleCells.ToArray(), id =>
        {
            SendMarkupEvent(id, cellFactory.possibleFields);
        });
    }

    public void MarkupTouchedField(PrepareMove prepareMove)
    {
        // markup clicked cell
        SendMarkupEvent(prepareMove.FromCellId(), cellFactory.highlightedFields);
    }

    public void ResetMarkup()
    {
        int cellId = 0;
        Array.ForEach(Matrix.Instance.GetMatrix(), y =>
        {
            Array.ForEach(y, x =>
            {
                SendMarkupEvent(cellId, cellFactory.defaultFields);
                cellId++;
            });
        });
    }


    private void SendMarkupEvent(int cellId, Color color)
    {
        OnMarkupEvent?.Invoke(cellId, color);
    }

}
