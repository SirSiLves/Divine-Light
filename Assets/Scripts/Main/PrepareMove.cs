using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PrepareMove
{
    public Vector2 fromPosition { get; set; }
    public Vector2 toPosition { get; set; }
    public List<int> possibleCells { get; set; }

    public PrepareMove(Vector2 fromPosition)
    {
        this.fromPosition = fromPosition;
        toPosition = new Vector2(9999f, 9999f); // unreal value to avoid 0
        possibleCells = MoveValidator.CollectPossibleCellIds(FromCellId());
    }

    public int FromCellId()
    {
        return Matrix.ConvertPostionToCellId(fromPosition);
    }

    public int ToCellId()
    {
        return Matrix.ConvertPostionToCellId(toPosition);
    }


}
