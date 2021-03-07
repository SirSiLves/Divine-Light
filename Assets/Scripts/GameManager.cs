using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    #region GAME_MANAGER
    //AFADFSDFSDF
    #endregion

    // Singleton's
    public Matrix matrix { get; set; }
    public Executor executor { get; set; }



    private void Awake()
    {
        matrix = new Matrix();
        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);

        executor = new Executor();
    }




}
