using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryButton : MonoBehaviour
{
    public ICommand command { get; set; }
    public string displayText { get; set; }

    private void Start()
    {
        Text text = transform.GetComponent<Text>();
        text.text = displayText;
    }

    private void Update()
    {
        DisableButton();
    }

    private void DisableButton()
    {
        if (PlayerHandler.Instance.isLightOn || RotationHandler.Instance.isRotating)
        {
            transform.GetComponent<Button>().interactable = false;
        }
        else
        {
            transform.GetComponent<Button>().interactable = true;
        }
    }

    public void DisplayHistory()
    {
        RewindHandler.Instance.EnableHistoryMode(command);
    }

}
