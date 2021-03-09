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


    public void MarkupPossibleFields(PrepareMove prepareMove)
    {
        // Markup possible cells
        Array.ForEach(prepareMove.possibleCells.ToArray(), id =>
        {
            SendMarkupEvent(id, cellFactory.possibleFields);
        });
    }

    public void MarkupTouchedField(PrepareMove prepareMove)
    {
        // Markup clicked cell
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
