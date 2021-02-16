using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour
{

    [SerializeField] Board board;


    // Start is called before the first frame update
    void Start()
    {
        board.Create();
    }


}
