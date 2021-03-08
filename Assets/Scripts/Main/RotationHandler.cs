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
    private int initialRotation;
    private Transform rotation, confirm, abbort;
    private Matrix matrix;


    private void Start()
    {
        isRotating = false;

        rotation = transform.Find("Rotation Button");
        confirm = transform.Find("Ok Button");
        abbort = transform.Find("Cancel Button");

        matrix = Matrix.Instance;
    }



    //public void Rotate()
    //{
    //    isRotating = true;

    //    FindObjectOfType<ClickHandlerOLD>().RevertMarkup();

    //    Piece touchedPiece = FindObjectOfType<ClickHandlerOLD>().touchedPiece;
    //    int currentDegrees = Mathf.RoundToInt(touchedPiece.transform.rotation.eulerAngles.z);

    //    int newDegrees = touchedPiece.restrictedRotation ? HandleRestrictedRotate(touchedPiece, currentDegrees) :
    //        currentDegrees == 0 ? 270 : currentDegrees -= 90;

    //    FindObjectOfType<GameManager>().executor.Execute(new RotationCommand(touchedPiece, newDegrees, matrix));

    //    FindObjectOfType<FieldHandler>().markupTouchedEvent.Invoke();

    //    HandleMenuButtons(newDegrees);
    //}


    //private int HandleRestrictedRotate(Piece touchedPiece, int currentDegrees)
    //{
    //    int pieceType = touchedPiece.id % 10;

    //    // set sun rotation
    //    if (pieceType == 1)
    //    {
    //        if (touchedPiece.id < 100)
    //        {
    //            return currentDegrees == 0 ? 90 : 0;
    //        }
    //        else
    //        {
    //            return currentDegrees == 180 ? 270 : 180;
    //        }
    //    }
    //    // set reflector rotation
    //    else if (pieceType == 4)
    //    {
    //        return currentDegrees == 0 ? 90 : 0;
    //    }

    //    throw new Exception("Restricted rotation mapping is missed");
    //}


    //private void HandleMenuButtons(int degrees)
    //{
    //    if (degrees == initialRotation)
    //    {
    //        Cancel();
    //    }
    //    else
    //    {
    //        SetButtonState(active, true);
    //    }
    //}


    //public void SetButtonState(Color color, bool enabled)
    //{
    //    confirm.GetComponent<Text>().color = color;
    //    confirm.GetComponent<Button>().enabled = enabled;
    //    abbort.GetComponent<Text>().color = color;
    //    abbort.GetComponent<Button>().enabled = enabled;
    //}


    //public void Confirm()
    //{
    //    FindObjectOfType<FieldHandler>().removeMarkupEvent.Invoke();

    //    DisableRotation();

    //    FindObjectOfType<ClickHandlerOLD>().touchedPiece = null;
    //    FindObjectOfType<PlayerChanger>().TogglePlaying();

    //    initialRotation = 0;
    //}


    //public void Cancel()
    //{
    //    Piece touchedPiece = FindObjectOfType<ClickHandlerOLD>().touchedPiece;

    //    FindObjectOfType<GameManager>().executor.Execute(new RotationCommand(touchedPiece, initialRotation, matrix));

    //    DisableRotation();
    //    FindObjectOfType<ClickHandlerOLD>().MarkupFields();
    //}

    ////TODO rename
    //public void ActiateRotate()
    //{
    //    rotation.GetComponent<Image>().color = active;
    //    rotation.GetComponent<Button>().enabled = true;

    //    SetInitialValue();
    //}


    //private void SetInitialValue()
    //{
    //    float rotationRawValue = FindObjectOfType<ClickHandlerOLD>().touchedPiece.transform.rotation.eulerAngles.z;
    //    initialRotation = Mathf.RoundToInt(rotationRawValue);
    //}


    //public void DisableRotation()
    //{
    //    rotation.GetComponent<Image>().color = disabled;
    //    rotation.GetComponent<Button>().enabled = false;

    //    SetButtonState(disabled, false);

    //    isRotating = false;
    //}

}
