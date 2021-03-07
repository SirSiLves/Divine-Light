using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindHandler : MonoBehaviour
{

    private Executor executor;
    private Text textArea;


    void Start()
    {
        executor = FindObjectOfType<GameManager>().executor;
        textArea = gameObject.GetComponent<Text>();
    }

    void Update()
    {
        WriteHistory();


        
    }


    private void WriteHistory()
    {
        textArea.text = "";

        Array.ForEach(executor.GetCommands().ToArray(), command => {
            textArea.text += command.ToString() + "\n";
        }
        );
    }



}
