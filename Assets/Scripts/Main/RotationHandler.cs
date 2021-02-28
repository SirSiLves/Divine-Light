using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class RotationHandler : MonoBehaviour
{

    public bool isRotating { get; set; }
    private int initialRotation;
    [SerializeField] Color disabled, active;
    Transform rotation, confirm, abbort;
    private Matrix matrix;


    private void Start()
    {
        isRotating = false;

        rotation = transform.Find("Rotation Button");
        confirm = transform.Find("Ok Button");
        abbort = transform.Find("Cancel Button");

        matrix = FindObjectOfType<GameManager>().matrix;
    }



    public void Rotate()
    {
        isRotating = true;

        FindObjectOfType<ClickHandler>().RevertMarkup();

        Piece touchedPiece = FindObjectOfType<ClickHandler>().touchedPiece;

        int degrees = Mathf.RoundToInt(touchedPiece.transform.rotation.eulerAngles.z);

        if(touchedPiece.restrictedRotation)
        {
            //TODO set restricted values on sun piece
        }
        else
        {
            degrees = degrees == 0 ? 270 : degrees -= 90;
        }

        RotationCommand rotationCommand = new RotationCommand(touchedPiece, degrees, matrix);
        new Drawer(rotationCommand).Draw();

        ActivateOkAndCancelButton();
    }

    private void ActivateOkAndCancelButton()
    {
        confirm.GetComponent<Text>().color = active;
        confirm.GetComponent<Button>().enabled = true;

        abbort.GetComponent<Text>().color = active;
        abbort.GetComponent<Button>().enabled = true;
    }

    public void Confirm()
    {
        DisableRotation();
        FindObjectOfType<ClickHandler>().touchedPiece = null;
        FindObjectOfType<PlayerChanger>().TogglePlaying();

        initialRotation = 0;
    }


    public void Cancel()
    {
        Piece touchedPiece = FindObjectOfType<ClickHandler>().touchedPiece;

        RotationCommand rotationCommand = new RotationCommand(touchedPiece, initialRotation, matrix);
        new Drawer(rotationCommand).Draw();

        DisableRotation();
        FindObjectOfType<ClickHandler>().MarkupFields();
    }


    public void ActivateRotateButton()
    {
        rotation.GetComponent<Image>().color = active;
        rotation.GetComponent<Button>().enabled = true;

        SetInitialValue();
    }


    private void SetInitialValue()
    {
        float rotationRawValue = FindObjectOfType<ClickHandler>().touchedPiece.transform.rotation.eulerAngles.z;
        initialRotation = Mathf.RoundToInt(rotationRawValue);
    }


    public void DisableRotation()
    {
        rotation.GetComponent<Image>().color = disabled;
        rotation.GetComponent<Button>().enabled = false;

        confirm.GetComponent<Text>().color = disabled;
        confirm.GetComponent<Button>().enabled = false;

        abbort.GetComponent<Text>().color = disabled;
        abbort.GetComponent<Button>().enabled = false;

        isRotating = false;
    }

}
