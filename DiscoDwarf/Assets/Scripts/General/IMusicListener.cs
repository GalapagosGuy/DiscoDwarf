using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMusicListener
{
    void OnBeatStart();
    void OnBeatCenter();
    void OnBeatFinished();
}
