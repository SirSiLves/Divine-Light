using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RotationHandler : MonoBehaviour
{

    [SerializeField] Color disabled, active;
    public bool isRotating { get; set; }
    private int rotatingPieceId;
    private int initialDegree;
    private Transform rotation, confirm, abbort;


    #region ROTATION_HANDLER_SINGLETON_SETUP
    private static RotationHandler _instance;

    public static RotationHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Rotation Handler");
                gO.AddComponent<RotationHandler>();
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
        isRotating = false;

        rotation = transform.Find("Rotation Button");
        confirm = transform.Find("Ok Button");
        abbort = transform.Find("Cancel Button");
    }



    public void Rotate()
    {
        CellHandler.Instance.ResetMarkup();
        CellHandler.Instance.MarkupTouchedField(ClickHandler.Instance.prepareMove);

        if (!isRotating)
        {
            rotatingPieceId = Matrix.Instance.GetPieceId(ClickHandler.Instance.prepareMove.FromCellId());
            initialDegree = GetDegrees(rotatingPieceId);
            isRotating = true;
        }

        int currentDegrees = GetDegrees(rotatingPieceId);
        int newDegrees = GetNewDegrees(rotatingPieceId, currentDegrees);

        rotatingPieceId = GetNewPieceId(rotatingPieceId, newDegrees);

        PieceHandler.Instance.VisualizeRotate(ClickHandler.Instance.prepareMove.fromPosition, newDegrees);


        HandleMenuButtons(newDegrees);
    }


    private void HandleMenuButtons(int degrees)
    {
        if (degrees == initialDegree)
        {
            Cancel();
        }
        else
        {
            SetButtonState(active, true);
        }
    }


    public void SetButtonState(Color color, bool enabled)
    {
        confirm.GetComponent<Text>().color = color;
        confirm.GetComponent<Button>().enabled = enabled;
        abbort.GetComponent<Text>().color = color;
        abbort.GetComponent<Button>().enabled = enabled;
    }


    public void Confirm()
    {
        DisableRotation();

        PieceHandler.Instance.HandleRotate(ClickHandler.Instance.prepareMove, rotatingPieceId);

        initialDegree = 0;
    }


    public void Cancel()
    {
        PieceHandler.Instance.VisualizeRotate(ClickHandler.Instance.prepareMove.fromPosition, initialDegree);

        CellHandler.Instance.MarkupPossibleFields(ClickHandler.Instance.prepareMove);

        SetButtonState(disabled, false);

        isRotating = false;
    }

    public void ActivateRotate()
    {
        rotation.GetComponent<Image>().color = active;
        rotation.GetComponent<Button>().enabled = true;
    }


    public void DisableRotation()
    {
        CellHandler.Instance.ResetMarkup();

        rotation.GetComponent<Image>().color = disabled;
        rotation.GetComponent<Button>().enabled = false;

        SetButtonState(disabled, false);

        isRotating = false;
    }



    public int GetDegrees(int pieceId)
    {
        int rotationValue = (pieceId % 100) - (pieceId % 10);

        switch (rotationValue)
        {
            case 0:
                return 0;
            case 10:
                return 90;
            case 20:
                return 180;
            case 30:
                return 270;
            default:
                Debug.LogError("No mapping for rotation value " + rotationValue);
                return 0;
        }
    }


    private int GetNewDegrees(int pieceId, int currentDegrees)
    {
        int pieceType = pieceId % 10;

        // set sun rotation
        if (pieceType == 1)
        {
            if (pieceId < 100)
            {
                return currentDegrees == 0 ? 90 : 0;
            }
            else
            {
                return currentDegrees == 180 ? 270 : 180;
            }
        }
        // set reflector rotation
        else if (pieceType == 4)
        {
            return currentDegrees == 0 ? 90 : 0;
        }
        // set other figures
        else
        {
            return currentDegrees == 0 ? 270 : currentDegrees -= 90;
        }

    }

    private int GetNewPieceId(int pieceId, int degrees)
    {
        int tempId = pieceId % 10;
        pieceId -= pieceId % 100;

        int rotateId = GetRotationId(degrees);

        pieceId += rotateId + tempId;

        return pieceId;
    }


    private int GetRotationId(int degrees)
    {
        switch (degrees)
        {
            case 0:
                return 0;
            case 90:
                return 1 * 10;
            case 180:
                return 2 * 10;
            case 270:
                return 3 * 10;
            default:
                Debug.LogError("No mapping for degrees value " + degrees);
                return 0;
        }
    }





}
