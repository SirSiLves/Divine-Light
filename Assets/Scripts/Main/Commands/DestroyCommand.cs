using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCommand : ICommand
{

    private Piece destroyedPiece;
    private Matrix matrix;


    public DestroyCommand(Piece destroyedPiece, Matrix matrix)
    {
        this.destroyedPiece = destroyedPiece;
        this.matrix = matrix;
    }



    public void Execute()
    {
        // remove piece from matrix
        int cellId = matrix.GetCellId(destroyedPiece.transform.position.y, destroyedPiece.transform.position.x);
        matrix.ChangePiece(cellId, 0);

        destroyedPiece.transform.gameObject.SetActive(false);
    }


    public void Revert()
    {

    }


}
