using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;



public class PieceFactory : MonoBehaviour
{

    [SerializeField] Piece sun, king, reflector, wall, angler;
    [SerializeField] Player player1, player2;


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

    public void InstantiatePiece(int y, int x, int pieceId)
    {

        Piece newPiece;

        // validate type of piece

        int type = pieceId % 10;
        switch (type)
        {
            case 1:
                newPiece = CreatePiece(sun, false, true, true);
                break;
            case 2:
                newPiece = CreatePiece(king, true, false, false);
                break;
            case 3:
                newPiece = CreatePiece(wall, true, false, false);
                break;
            case 4:
                newPiece = CreatePiece(reflector, false, true, false);
                break;
            case 5:
                newPiece = CreatePiece(angler, true, false, false);
                break;
            default:
                throw new Exception("no valid piece found for piece value: " + pieceId);
        }

        // set player
        if(pieceId < 100)
        {
            newPiece.SetPlayer(player1);
        }
        else
        {
            newPiece.SetPlayer(player2);
        }

        // show it on board
        newPiece.id = pieceId;
        newPiece.DrawPiece(y, x);
    }

   
    private Piece CreatePiece(Piece piece, bool exchangeable, bool restrictedRotation, bool restrictedMove)
    {
        piece = Instantiate(piece, transform);

        piece.name = piece.ToString();
        piece.exchangeable = exchangeable;
        piece.restrictedRotation = restrictedRotation;
        piece.restrictedMove = restrictedMove;

        return piece;
    }




}
