using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RotationHandler : MonoBehaviour
{

    public bool isRotating { get; set; }
    private int rotatingCharacter;
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
            rotatingCharacter = Matrix.GetCharacter(Matrix.Instance.GetMatrix(), ClickHandler.Instance.prepareMove.FromCellId());
            initialDegree = RotateValidator.GetDegrees(rotatingCharacter);
            isRotating = true;
        }

        int currentDegrees = RotateValidator.GetDegrees(rotatingCharacter);
        int newDegrees = RotateValidator.GetNewDegrees(rotatingCharacter, currentDegrees);
        rotatingCharacter = RotateValidator.GetNewCharacter(rotatingCharacter, newDegrees);

        UpdateBoard(rotatingCharacter);

        HandleMenuButtons(newDegrees);
    }

    private void UpdateBoard(int characterValue)
    {
        PrepareMove prepareMove = ClickHandler.Instance.prepareMove;
        prepareMove.characterValue = characterValue;

        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int newCharacterValue = prepareMove.characterValue;

        PieceHandler.Instance.VisualizeRotate(fromCellId, newCharacterValue);
    }

    private void HandleMenuButtons(int degrees)
    {
        if (degrees == initialDegree)
        {
            Cancel();
        }
        else
        {
            SetButtonState(true);
        }
    }


    public void SetButtonState(bool enabled)
    {
        confirm.GetComponent<Button>().interactable = enabled;
        abbort.GetComponent<Button>().interactable = enabled;
    }


    public void Confirm()
    {
        DisableRotation();

        // update matrix and command list
        PrepareMove prepareMove = ClickHandler.Instance.prepareMove;
        int fromCellId = Matrix.ConvertPostionToCellId(prepareMove.fromPosition);
        int newCharacterValue = prepareMove.characterValue;
        PieceHandler.Instance.DoRotate(fromCellId, newCharacterValue);

        PlayerHandler.Instance.TogglePlaying();
    }


    public void Cancel()
    {
        UpdateBoard(RotateValidator.GetNewCharacter(rotatingCharacter, initialDegree));

        CellHandler.Instance.MarkupPossibleFields(ClickHandler.Instance.prepareMove);

        SetButtonState(false);

        isRotating = false;
    }

    public void ActivateRotate()
    {
        rotation.GetComponent<Button>().interactable = true;
    }


    public void DisableRotation()
    {
        CellHandler.Instance.ResetMarkup();

        rotation.GetComponent<Button>().interactable = false;

        SetButtonState(false);

        isRotating = false;
        initialDegree = 0;
        rotatingCharacter = 0;
    }



}
