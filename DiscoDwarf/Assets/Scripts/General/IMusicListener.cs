using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMusicListener : MonoBehaviour
{
    public abstract void OnBeatStart();
    public abstract void OnBeatCenter();
    public abstract void OnBeatFinished();
}
