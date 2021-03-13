using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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


        Canvas canvas = Array.Find(FindObjectsOfType<Canvas>().ToArray(), c => c.name.ToString() == "Cell Canvas");
        Transform textform = canvas.transform.Find("ID");
        Text textComponent = textform.GetComponent<Text>();
        textComponent.text = id.ToString();
    }

    public int GetCellId()
    {
        return id;
    }







}
