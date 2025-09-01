using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    GameObject gameObject { get; }
    bool IsAlive { get; }
}

