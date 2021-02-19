using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



public class PieceFactory : MonoBehaviour
{

    [SerializeField] Piece sun, king, reflector, wall, angler;
    [SerializeField] Color player1, player2;


    private readonly Dictionary<int, int> defaultSet = new Dictionary<int, int>
    {
        {2, 15}, // field 0,2 - angler - 90° - p1
        {3, 3}, // field 0,3 - wall - 0° - p1
        {4, 2}, // field 0,4 - king - 0° - p1
        {5, 3}, // field 0,5 - wall - 0° - p1
        {9, 1}, // field 0,9 - sun - 0° - p1
        {17, 5}, // field 1,7 - angler 0° - p1
        {26, 135}, // field 2,6 - angler 270° - p2
        {30, 135}, // field 3,0 - angler 270° - p2
        {32, 15}, // field 3,2 - angler 90° - p1
        {34, 4}, // field 3,4 - reflector 0° - p1
        {35, 134}, // field 3,5 - reflector 270° - p2
        {37, 105}, // field 3,7 - angler - 0° - p2
        {39, 25}, // field 3,9 - angler - 180° p1
        {40, 105}, // field 4,0 - angler - 0° - p2
        {42, 25}, // field 4,2 - angler - 180° - p1
        {44, 34}, // field 4,4 - reflector - 270° - p1
        {45, 104}, // field 4,5 - reflector - 0° - p2
        {47, 135}, // field 4,7 - angler - 270° - p2
        {49, 15}, // field 4,9 - angler - 90° - p1
        {53, 15}, // field 5,3 - angler - 90° - p1
        {62, 125}, // field 6,2 - angler - 180° - p2
        {70, 121}, // field 7,0 - angler - 180° - p2
        {74, 123}, // field 7,4 - wall - 180° - p2
        {75, 122}, // field 7,5 - king - 180° - p2
        {76, 123}, // field 7,6 - wall - 180° - p2
        {77, 135}, // field 77 - angler - 270° - p2
    };

    public Dictionary<int, int> GetDefaultSet()
    {
        return defaultSet;
    }

    public void InstantiatePiece(int y, int x, int pieceValue)
    {

        Piece newPiece = null;

        // validate type of piece

        string name = "";
        int type = pieceValue % 10;
        switch (type)
        {
            case 1:
                newPiece = Instantiate(sun, transform);
                name = "sun";
                break;
            case 2:
                newPiece = Instantiate(king, transform);
                name = "king";
                break;
            case 3:
                newPiece = Instantiate(wall, transform);
                name = "wall";
                break;
            case 4:
                newPiece = Instantiate(reflector, transform);
                name = "reflector";
                break;
            case 5:
                newPiece = Instantiate(angler, transform);
                name = "angler";
                break;
            default:
                Debug.LogError("no valid piece found");
                break;
        }

        // set player color
        newPiece.transform.GetComponentInChildren<SpriteRenderer>().color = pieceValue < 100 ? player1 : player2;

        // set name
        newPiece.name = name + " - " + y + "," + x;

        // show it on board
        newPiece.DrawPiece(y, x, pieceValue);
    }



}
