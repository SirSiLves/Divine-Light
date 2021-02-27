using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationController : MonoBehaviour
{

    bool rotationClicked {get; set;}
    [SerializeField] Color disabled, active;
    Transform rotation, confirm, abbort;


    public void Start()
    {
        rotationClicked = false;
        rotation = transform.Find("Rotation Button");
        confirm = transform.Find("Ok Button");
        abbort = transform.Find("Cancel Button");

        DisableRoation();



    }


    public void Rotate()
    {
        FindObjectOfType<ClickValidator>().RevertMarkup();

        SetActive();

        //FindObjectOfType<FieldValidator>().GetLastValidatedCells().Clear();

        print("Rotate!!!");
    }

    private void SetActive()
    {
        rotation.GetComponent<Image>().color = active;

        confirm.GetComponent<Text>().color = active;
        confirm.GetComponent<Button>().enabled = true;

        abbort.GetComponent<Text>().color = active;
        abbort.GetComponent<Button>().enabled = true;
    }

    public void Confirm()
    {

        ClickValidator clickValidator = FindObjectOfType<ClickValidator>();


        print("Confirm!!!");

    }


    public void Cancel()
    {

        DisableRoation();

        print("Cancel!!");
    }

    public void ActivateRotation()
    {
        rotation.GetComponent<Image>().color = active;
        rotation.GetComponent<Button>().enabled = true;

    }


    public void DisableRoation()
    {
        rotation.GetComponent<Image>().color = disabled;
        rotation.GetComponent<Button>().enabled = false;

        confirm.GetComponent<Text>().color = disabled;
        confirm.GetComponent<Button>().enabled = false;

        abbort.GetComponent<Text>().color = disabled;
        abbort.GetComponent<Button>().enabled = false;
    }
}
