using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    // example: sourceField -> 5,9 / targetField -> 1,3
    private int[] sourceField, targetField;
    private string movedPiece, targetPiece;



    public void SetTargetPiece(string targetPiece)
    {
        this.targetPiece = targetPiece;
    }

    public void SetMovedPiece(string movedPiece)
    {
        this.movedPiece = movedPiece;
    }

    public void SetTargetField(int[] targetField)
    {
        this.targetField = targetField;
    }

    public void SetSourceField(int[] sourceField)
    {
        this.sourceField = sourceField;
    }

    public int[] GetTargetField()
    {
        return targetField;
    }

    public int[] GetSourceField()
    {
        return sourceField;
    }

    public string GetMovedPiece()
    {
        return movedPiece;
    }

    public string GetTargetPiece()
    {
        return targetPiece;
    }

}
