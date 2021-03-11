using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;




public class PlayerHandler : MonoBehaviour
{

    [SerializeField] float lightTimer = 3f;
    [SerializeField] int isPlayingIndex = 0;

    internal Player player1, player2;
    public bool isLightOn { get; set; }

    private List<Piece> piecesToDestroy;


    #region PLAYER_HANDLER_SINGLETON_SETUP
    private static PlayerHandler _instance;

    public static PlayerHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Player Changer");
                gO.AddComponent<PlayerHandler>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        player1 = transform.Find("Player1").GetComponent<Player>();
        player2 = transform.Find("Player2").GetComponent<Player>();
    }
    #endregion


    private void Start()
    {
        piecesToDestroy = new List<Piece>();
        UpdatePlayingDisplay();
    }


    public List<Piece>  GetPiecesToDestroy()
    {
        return piecesToDestroy;
    }


    public void TogglePlaying()
    {
        StartCoroutine("TurnOnLight");
        StartCoroutine("TurnOffLight");

        Matrix.PrintMatrixToConsole();
    }


    IEnumerator TurnOnLight()
    {
        isLightOn = true;
        LightUpFrame(true);

        yield return new WaitForSeconds(0.2f);

        LightController[] lightControllers = Resources.FindObjectsOfTypeAll<LightController>();

        Array.ForEach(lightControllers, controller =>
        {
            Piece piece = controller.transform.parent.GetComponent<Piece>();
            if (piece.playerIndex == isPlayingIndex)
            {
                controller.transform.gameObject.SetActive(true);
            }
        });

    }


    IEnumerator TurnOffLight()
    {
        yield return new WaitForSeconds(lightTimer);

        LightController[] lightControllers = Resources.FindObjectsOfTypeAll<LightController>();

        Array.ForEach(lightControllers, controller =>
        {
            controller.transform.gameObject.SetActive(false);
        });

        RemoveDestroyed();
        ChangePlayer();
        LightUpFrame(false);
        isLightOn = false;
    }

    private void LightUpFrame(bool state)
    {
        BoardFactory boardFactory = FindObjectOfType<BoardFactory>();
        Transform textform = boardFactory.transform.Find("LightUpFrame");
        textform.gameObject.SetActive(state);
    }


    private void RemoveDestroyed()
    {
        Matrix matrix = Matrix.Instance;

        Array.ForEach(piecesToDestroy.ToArray(), piece =>
        {
            //FindObjectOfType<GameManager>().executor.Execute(new DestroyCommand(piece, matrix));
            PieceHandler.Instance.HandleDestroy(piece);
        });

        piecesToDestroy.Clear();

        Matrix.PrintMatrixToConsole();
    }


    public void ChangePlayer()
    {
        isPlayingIndex = isPlayingIndex == 0 ? 1 : 0;

        UpdatePlayingDisplay();
    }


    public void UpdatePlayingDisplay()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        Transform textform = canvas.transform.Find("Playing Player");
        Text textComponent = textform.GetComponent<Text>();

        textComponent.text = "PLAYING " + GetIsPlaying().GetNickName();
        textComponent.color = GetIsPlaying().GetFigure();
    }

    public int GetIsPlayingIndex()
    {
        return isPlayingIndex;
    }

    public Player GetIsPlaying()
    {
        if(isPlayingIndex == 0)
        {
            return player1;
        }
        else if (isPlayingIndex == 1)
        {
            return player2;
        }

        return null;
    }

    public static int GetPlayerIndex(int character)
    {
        return character < 100 ? 0 : 1;
    }

    public static int GetEnemyIndex(int character)
    {
        return character < 100 ? 1 : 0;
    }

}
