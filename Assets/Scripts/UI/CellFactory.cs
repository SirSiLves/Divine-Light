using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    [SerializeField] Cell cell;
    [SerializeField] public Color possibleFields, defaultFields, highlightedFields;
    [SerializeField] internal Player player1, player2;


    internal void Create(int y, int x)
    {
        Cell newCell = Instantiate(cell, transform);
        newCell.transform.parent = transform;
        newCell.transform.position = new Vector3(
            transform.position.x + x, transform.position.y + y, newCell.transform.parent.position.z
            );
        newCell.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        newCell.transform.name = y.ToString() + ',' + x.ToString();
        newCell.SetCellId(y, x);

        if (FieldHandler.ValidateSafeZone(y, x, 0)) {
            CreateSafeZone(newCell, player1.GetSafeZone());
        }
        else if (FieldHandler.ValidateSafeZone(y, x, 1))
        {
            CreateSafeZone(newCell, player2.GetSafeZone());
        }

        cell.transform.GetComponentInChildren<SpriteRenderer>().color = defaultFields;

    }


    private void CreateSafeZone(Cell cell, Color color)
    {
        Transform textform = cell.transform.Find("SafeZone");
        textform.gameObject.SetActive(true);
        textform.GetComponentInChildren<SpriteRenderer>().color = color;
    }

}
