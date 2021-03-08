using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    private PrepareMove prepareMove;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandlePrepare();
        }
    }

    private void HandlePrepare()
    {
        if(PlayerHandler.Instance.isLightOn) { return; }

        Vector2 clickPosition = GetClickedGridPos();
        if(clickPosition.x < 0 || clickPosition.y < 0) { return; } // click is outside from grid

        CellHandler.Instance.ResetMarkup();

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



    private static Vector2 GetClickedGridPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        return gridPos;
    }




}
