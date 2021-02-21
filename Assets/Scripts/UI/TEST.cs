using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LightController lightController;

    private void Start()
    {
        lightController.SetUpLine(points);
    }
}
