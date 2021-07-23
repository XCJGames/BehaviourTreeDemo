using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;

    public bool IsLocked { get => isLocked; set => isLocked = value; }
}
