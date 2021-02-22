using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;
    [SerializeField] float lightTimer = 5f;


    // Singleton's
    public Matrix matrix { get; set; } 
    public Player isPlaying { get; set; } 
    public bool isLightOn { get; set; }


    void Awake()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);
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

        this.ChangePlayer();
        this.isLightOn = false;
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
