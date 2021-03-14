using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewindHandler : MonoBehaviour
{

    [SerializeField] bool rewindEnabled = true;
    private Text textList;
    private Transform revertButton, textForm, textContainer;
    private RectTransform textRect, textContainerRect;


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
        revertButton = transform.Find("Revert Button");
        textContainer = transform.Find("History Area").Find("TextContainer");
        textForm = textContainer.Find("Text");
        textContainerRect = textContainer.GetComponent<RectTransform>();
        textRect = textForm.GetComponent<RectTransform>();
        textList = textForm.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        WriteHistory();
        DisableRevertButton();
    }

    public void DisableRevertButton()
    {
        if (!rewindEnabled || PlayerHandler.Instance.isLightOn || RotationHandler.Instance.isRotating)
        {
            revertButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            revertButton.GetComponent<Button>().interactable = true;
        }

    }

    private void WriteHistory()
    {
        textList.text = "";

        List<ICommand> commands = Executor.Instance.GetCommands();

        int moveCount = 1;
        Array.ForEach(commands.ToArray(), command => {
            textList.text += moveCount + ". " +command.GetDescription() + "\n";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "TESCHTTESCHTTESCHTTESCHTTESCHT";
            //textList.text += "ENDE \n";

            moveCount++;
        }
        );

        AlignTextToBottom();
    }

    private void AlignTextToBottom()
    {
        if (textRect.rect.height > textContainerRect.rect.height)
        {
            textRect.pivot = new Vector2(0f, 0f);
        }
        else
        {
            textRect.pivot = new Vector2(0f, 1f);
        }
    }

    public void Rewind()
    {
        ICommand command = Executor.Instance.GetLastCommand();
        if (command == null) { return; }
        else if (command.GetType() == typeof(DestroyCommand))
        {
            RewindLoop(2);
        }
        else
        {
            RewindLoop(1);
        }

        PlayerHandler.Instance.ChangePlayer();
    }


    private void RewindLoop(int times)
    {
        while (times > 0)
        {
            ICommand command = Executor.Instance.GetLastCommand();
            PieceHandler.Instance.HandleRevert(command);

            times--;
        }
    }


}
