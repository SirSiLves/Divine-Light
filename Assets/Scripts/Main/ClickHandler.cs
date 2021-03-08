using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    public PrepareMove prepareMove { get; private set; }

    #region CLICK_HANDLER_SINGLETON_SETUP
    private static ClickHandler _instance;

    public static ClickHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Click Handler");
                gO.AddComponent<ClickHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CellHandler.Instance.ResetMarkup();

            HandlePrepare();
        }
    }

    private void HandlePrepare()
    {
        if (PlayerHandler.Instance.isLightOn) { return; }

        Vector2 clickPosition = GetClickedGridPos();
        if (clickPosition.x < 0 || clickPosition.y < 0 || clickPosition.x > 9 || clickPosition.y > 7) { return; } // click is outside from grid

        if (prepareMove == null)
        {
            prepareMove = new PrepareMove(clickPosition);
        }
        else
        {
            prepareMove.toPosition = clickPosition;
        }

        if (prepareMove.possibleCells.Contains(prepareMove.toCellId()))
        {
            //move can be executed
            PieceHandler.Instance.HanldeMove(prepareMove);
            prepareMove = null;
        }
        else
        {
            prepareMove = new PrepareMove(clickPosition);
            CellHandler.Instance.Markup(prepareMove);
        }
    }


    private bool ValidateClick()
    {
        int pieceId = Matrix.Instance.GetPieceId(prepareMove.fromCellId());

        // no piece found
        if (pieceId == 0) { return false; }

        // other players turn
        if (PlayerHandler.Instance.isPlayingIndex == 0 && pieceId >= 100) { return false; }
        if (PlayerHandler.Instance.isPlayingIndex == 1 && pieceId < 100) { return false; }

        return true;
    }


    private static Vector2 GetClickedGridPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        return gridPos;
    }




}
