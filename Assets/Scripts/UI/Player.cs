using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private Color figure, safeZone;
    [SerializeField] private string nickName;

    public Color GetFigure()
    {
        return figure;
    }

    public Color GetSafeZone()
    {
        return safeZone;
    }

    public string GetNickName()
    {
        return nickName;
    }

}
