using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] BoardFactory boardFactory;

    public Matrix matrix { get; private set; }
    public Executor executor { get; private set; }


    #region GAME_MANAGER_SINGLETON_SETUP
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gO = new GameObject("Game Manager");
                gO.AddComponent<GameManager>();
            }

            return _instance;
        }
    }
    #endregion


    private void Awake()
    {
        _instance = this;
        matrix = Matrix.Instance;

        boardFactory.CreateBoard(matrix);
        boardFactory.CreateDefaultSetUp(matrix);

        executor = Executor.Instance;
    }









}
