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
            initialDegree = RotateValidator.GetDegrees(rotatingPieceId);
            isRotating = true;
        }

        int currentDegrees = RotateValidator.GetDegrees(rotatingPieceId);
        int newDegrees = RotateValidator.GetNewDegrees(rotatingPieceId, currentDegrees);

        rotatingPieceId = RotateValidator.GetNewPieceId(rotatingPieceId, newDegrees);

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



}
