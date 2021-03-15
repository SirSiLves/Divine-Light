using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewindHandler : MonoBehaviour
{

    [SerializeField]
    private bool rewindEnabled = true;

    [SerializeField]
    private GameObject buttonTemplate;

    [SerializeField]
    private int buttonSpaceCount = 7;

    private Transform revertButton, buttonListContent;
    private RectTransform buttonListContentRect;
    private Scrollbar scrollbar;
    private int historySize;

    public bool isRewinding { get; private set; }


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
        // TODO Serialized Fields..
        revertButton = transform.Find("Revert Button");
        buttonListContent = transform.Find("History Canvas").Find("HistoryButtonScrollList").Find("ButtonListViewport").Find("ButtonListContent");
        buttonListContentRect = buttonListContent.GetComponent<RectTransform>();
        scrollbar = FindObjectOfType<Scrollbar>();
        buttonTemplate.SetActive(false);
    }

    void Update()
    {
        DisplayHistoryList();
        DisableRevertButton();
    }

    private void DisplayHistoryList()
    {
        int commandCount = Executor.Instance.GetCommands().Count;

        if (historySize == commandCount) { return; }

        List<HistoryButton> historyButtons = FindObjectsOfType<HistoryButton>().ToList();
        List<ICommand> commands = Executor.Instance.GetCommands();

        // remove all buttons
        Array.ForEach(historyButtons.ToArray(), b => Destroy(b.transform.gameObject));

        // create buttons
        int moveCount = 1;
        Array.ForEach(commands.ToArray(), command => {
            string text = moveCount + ". " + command.GetDescription();
            CreateHistoryButton(command, text);
            moveCount++;
        });

        historySize = commandCount;

        AlignTextToBottom(moveCount);
    }

    public void DisableRevertButton()
    {
        if (!rewindEnabled || PlayerHandler.Instance.isLightOn || RotationHandler.Instance.isRotating)
        {
            revertButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            revertButton.GetComponent<Button>().interactable = true;
        }
    }

    public void CreateHistoryButton(ICommand historyCommand, string buttonText)
    {
        GameObject button = Instantiate(buttonTemplate, buttonListContent) as GameObject;

        HistoryButton hButton = button.GetComponent<HistoryButton>();
        hButton.displayText = buttonText;
        hButton.command = historyCommand;

        button.transform.SetParent(buttonTemplate.transform.parent);
        button.transform.localPosition = buttonTemplate.transform.parent.localScale;
        button.SetActive(true);
    }

    private void AlignTextToBottom(int count)
    {
        if (count > buttonSpaceCount)
        {
            buttonListContentRect.pivot = new Vector2(0f, 0f);
            scrollbar.value = 0f;
        }
        else
        {
            buttonListContentRect.pivot = new Vector2(0f, 1f);
        }
    }

    public void Rewind()
    {
        ICommand command = Executor.Instance.GetLastCommand();
        if (command == null) { return; }
        else if (command.GetType() == typeof(InitializeCommand)) { return; }
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


    public void EnableHistoryMode(ICommand command)
    {
        CellHandler.Instance.ResetMarkup();
        CellHandler.Instance.MarkupHistoryField(command);

        PieceHandler.Instance.DrawMatrix(command.GetFormerMatrix());
        PieceHandler.Instance.VisualizeCommand(command);

        isRewinding = true;
    }


    public void DisableHistoryMode()
    {
        ICommand command = Executor.Instance.GetLastCommand();
        PieceHandler.Instance.DrawMatrix(command.GetFormerMatrix());
        PieceHandler.Instance.VisualizeCommand(command);

        isRewinding = false;
    }

}
