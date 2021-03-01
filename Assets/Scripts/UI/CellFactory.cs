using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : MonoBehaviour
{
    [SerializeField] Cell cell;
    [SerializeField] public Color possibleFields, defaultFields, highlightedFields;


    internal void Create(int y, int x)
    {
        // standard cell
        Cell newCell = Instantiate(cell, transform);
        newCell.transform.parent = transform;
        newCell.transform.position = new Vector3(
            transform.position.x + x, transform.position.y + y, newCell.transform.parent.position.z
            );
        newCell.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        newCell.transform.name = y.ToString() + ',' + x.ToString();
        newCell.SetCellId(y, x);
        newCell.transform.GetComponentInChildren<SpriteRenderer>().color = defaultFields;
    }

}
