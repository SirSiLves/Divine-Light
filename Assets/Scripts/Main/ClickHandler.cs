using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{

    internal PrepareMove prepareMove { get; set; }

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
            HandlePrepare();
        }
    }

    private void HandlePrepare()
    {
        Vector2 clickPosition = GetClickedGridPos();

        if (PlayerHandler.Instance.isLightOn) { return; }
        if (RotationHandler.Instance.isRotating) { return; }
        if (clickPosition.x < 0 || clickPosition.y < 0 || clickPosition.x > 9 || clickPosition.y > 7) { return; } // click is outside from grid

        if (prepareMove == null)
        {
            InitializePrepare(clickPosition);
        }
        else
        {
            prepareMove.toPosition = clickPosition;

            if (prepareMove.possibleCells.Contains(prepareMove.ToCellId()))
            {
                //move can be executed
                CellHandler.Instance.ResetMarkup();
                RotationHandler.Instance.DisableRotation();

                int character = Matrix.Instance.GetCharacter(Matrix.ConvertPostionToCellId(clickPosition));

                // replace
                if (character != 0)
                {
                    PieceHandler.Instance.HandleReplace(prepareMove);
                }
                // normal move
                else
                {
                    PieceHandler.Instance.HanldeMove(prepareMove);
                }

                PlayerHandler.Instance.TogglePlaying();
                prepareMove = null;
            }
            else
            {
                InitializePrepare(clickPosition);
            }

        }

    }

    private void InitializePrepare(Vector2 clickPosition)
    {
        RotationHandler.Instance.DisableRotation();
        CellHandler.Instance.ResetMarkup();

        int character = Matrix.Instance.GetCharacter(Matrix.ConvertPostionToCellId(clickPosition));

        // no piece found
        if (character == 0) { return; }

        // other players turn
        if (PlayerHandler.Instance.GetIsPlayingIndex() == 0 && character >= 100) { return; }
        if (PlayerHandler.Instance.GetIsPlayingIndex() == 1 && character < 100) { return; }

        prepareMove = new PrepareMove(clickPosition);

        RotationHandler.Instance.ActivateRotate();

        CellHandler.Instance.MarkupTouchedField(prepareMove);
        CellHandler.Instance.MarkupPossibleFields(prepareMove);
    }



    private static Vector2 GetClickedGridPos()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(worldPos.x), Mathf.RoundToInt(worldPos.y));

        return gridPos;
    }




}
