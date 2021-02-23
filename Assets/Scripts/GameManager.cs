using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;


    // Singleton's
    public Matrix matrix { get; set; } 



    void Awake()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);
    }    




}
