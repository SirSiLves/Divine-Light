using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    private int id;


    private void Start()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CellHandler.Instance.OnMarkupEvent += Instance_OnMarkupEvent;
    }

    private void OnDestroy()
    {
        CellHandler.Instance.OnMarkupEvent -= Instance_OnMarkupEvent;
    }

    private void Instance_OnMarkupEvent(int cellId, Color color)
    {
        if (cellId == this.id)
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }


    public void SetCellId(int y, int x)
    {
        this.id = Int32.Parse(y + "" + x);
    }

    public int GetCellId()
    {
        return id;
    }







}
