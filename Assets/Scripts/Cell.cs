using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    private int id;

    public void SetCellId(int y, int x)
    {
        this.id = Int32.Parse(y + "" + x);
    }

    public int GetCellId()
    {
        return id;
    }

    private void OnMouseDown()
    {
        MoveHandler moveHandler = FindObjectOfType<GameManager>().GetMoveHandler();
        moveHandler.ValidateMove(this);
    }


}
