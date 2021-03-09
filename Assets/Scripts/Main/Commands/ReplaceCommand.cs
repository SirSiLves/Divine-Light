﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class ReplaceCommand : ICommand
{

    public int characterValue1 { get; private set; }
    public int characterValue2 { get; private set; }
    public int fromCellId { get; private set; }
    public int toCellId { get; private set; }

    private Matrix matrix { get; set; }


    public ReplaceCommand(int fromCellId, int toCellId)
    {
        this.fromCellId = fromCellId;
        this.toCellId = toCellId;

        matrix = Matrix.Instance;
        characterValue1 = matrix.GetCharacter(fromCellId);
        characterValue2 = matrix.GetCharacter(toCellId);
    }


    public void Execute()
    {
        // update matrix with new position
        matrix.ChangePiece(toCellId, characterValue1);
        matrix.ChangePiece(fromCellId, characterValue2);
    }


    public void Revert()
    {

    }


}
