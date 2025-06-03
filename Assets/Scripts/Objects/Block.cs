using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    protected float Speed;
    public float GetSpeed() => Mathf.CeilToInt(Speed);
}
