using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;
    [SerializeField] float lightTimer = 3f;


    // Singleton's
    public Matrix matrix { get; set; } 
    public Player isPlaying { get; set; } 
    public bool isLightOn { get; set; }
    public List<Piece> piecesToDestroy;


    void Awake()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);

        piecesToDestroy = new List<Piece>();
    }



    public void TogglePlaying()
    {
        StartCoroutine("TurnOnLight");
        StartCoroutine("TurnOffLight");
    }


    IEnumerator TurnOnLight()
    {
        yield return new WaitForSeconds(0.2f);

        this.isLightOn = true;

        LightController[] lightControllers = Resources.FindObjectsOfTypeAll<LightController>();

        Array.ForEach(lightControllers, controller =>
        {
            Piece piece = controller.transform.parent.GetComponent<Piece>();
            if (piece.GetPlayer() == this.isPlaying)
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
            Piece piece = controller.transform.parent.GetComponent<Piece>();
            controller.transform.gameObject.SetActive(false);
        });

        this.RemoveDestroyed();
        this.ChangePlayer();
        this.isLightOn = false;
    }


    private void RemoveDestroyed()
    {
        Array.ForEach(piecesToDestroy.ToArray(), piece =>
        {
            // remove piece from matrix
            int cellId = matrix.GetCellId(piece.GetPieceId());
            matrix.ChangePiece(cellId, 0);

            Destroy(piece.gameObject);
        });

        piecesToDestroy.Clear();

        Matrix.PrintMatrixToConsole(matrix.GetMatrix());
    }


    private void ChangePlayer()
    {
        Player[] players = FindObjectsOfType<Player>();
        isPlaying = players[1] == isPlaying ? players[0] : players[1];

        UpdatePlayingDisplay();
    }

     
    public void UpdatePlayingDisplay()
    {
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        Transform textform = canvas.transform.Find("Playing Player");
        Text textComponent = textform.GetComponent<Text>();

        textComponent.text = "PLAYING " + isPlaying.GetNickName();
        textComponent.color = isPlaying.GetColor();
    }
}
