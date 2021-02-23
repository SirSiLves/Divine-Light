using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private Color color;
    [SerializeField] private string nickName;

    public Color GetColor()
    {
        return color;
    }

    public string GetNickName()
    {
        return nickName;
    }

}
