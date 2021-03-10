using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindHandler : MonoBehaviour
{

    private Text textArea;


    #region REWIND_HANDLER_SINGLETON_SETUP
    private static RewindHandler _instance;

    public static RewindHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Rewind Handler");
                gO.AddComponent<RewindHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion


    void Start()
    {
        textArea = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        WriteHistory();
    }


    private void WriteHistory()
    {
        textArea.text = "";

        Array.ForEach(Executor.Instance.GetCommands().ToArray(), command => {
            textArea.text += command.ToString() + "\n";
        }
        );
    }


    public void Rewind()
    {
        PieceHandler.Instance.HandleRevert();
    }


}
