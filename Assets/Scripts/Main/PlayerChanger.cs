using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;




public class PlayerChanger : MonoBehaviour
{

    [SerializeField] float lightTimer = 3f;

    public Player isPlaying { get; set; }
    public bool isLightOn { get; set; }
    public bool firstTouched { get; set; }
    private List<Piece> piecesToDestroy;



    private void Start()
    {
        piecesToDestroy = new List<Piece>();
        firstTouched = false;
    }


    public List<Piece>  GetPiecesToDestroy()
    {
        return piecesToDestroy;
    }


    public void FirstTouched(Piece movingFigure)
    {
        if (isPlaying == null && movingFigure != null)
        {
            isPlaying = movingFigure.GetPlayer();
            UpdatePlayingDisplay();

            firstTouched = true;
        }
    }


    public void TogglePlaying()
    {
        StartCoroutine("TurnOnLight");
        StartCoroutine("TurnOffLight");
    }


    IEnumerator TurnOnLight()
    {
        yield return new WaitForSeconds(0.2f);

        isLightOn = true;

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
            controller.transform.gameObject.SetActive(false);
        });

        RemoveDestroyed();
        ChangePlayer();
        isLightOn = false;
    }


    private void RemoveDestroyed()
    {
        Matrix matrix = FindObjectOfType<GameManager>().matrix;

        Array.ForEach(piecesToDestroy.ToArray(), piece =>
        {
            FindObjectOfType<GameManager>().executor.Execute(new DestroyCommand(piece, matrix));
        });

        piecesToDestroy.Clear();

        Matrix.PrintMatrixToConsole(matrix.GetMatrix());
    }


    public void ChangePlayer()
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
