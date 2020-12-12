using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowBump : IMusicListener
{
    public MaterialsSettings[] materialsToBump;

    [SerializeField]
    private float decreasingSpeed = 0.5f;

    private void Start()
    {
        for (int i = 0; i < materialsToBump.Length; i++)
        {
            materialsToBump[i].currentIntensity = materialsToBump[i].intensity;
        }
    }

    public override void OnBeatStart()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnBeatCenter()
    {
        for (int i = 0; i < materialsToBump.Length; i++)
        {
            materialsToBump[i].currentIntensity = materialsToBump[i].intensity;
        }
    }

    public override void OnBeatFinished()
    {
        //throw new System.NotImplementedException();
    }

    private void Update()
    {
        for (int i = 0; i < materialsToBump.Length; i++)
        {
            materialsToBump[i].currentIntensity -= Time.deltaTime * decreasingSpeed;

            materialsToBump[i].material.SetVector("_EmissionColor", materialsToBump[i].originalColor * materialsToBump[i].currentIntensity);
        }
    }
}


[System.Serializable]
public struct MaterialsSettings
{
    public Color originalColor;
    public Material material;
    public float intensity;
    public float currentIntensity;
}

