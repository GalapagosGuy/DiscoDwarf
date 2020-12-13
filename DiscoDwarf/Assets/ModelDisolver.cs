using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelDisolver : MonoBehaviour
{
    public List<SpriteRenderer> renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>().ToList();

        foreach(SpriteRenderer r in renderers)
        {
            r.material = new Material(r.material);
        }
    }

    public IEnumerator Dissolve(float time)
    {
        float t = 0;
        float currentTime = 0;
        while(t < 1)
        {
            currentTime += Time.deltaTime;
            t = currentTime / time;

            foreach(SpriteRenderer r in renderers)
            {
                r.material.SetFloat("_DissolveAmount", t);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator Undissolve(float time)
    {
        float t = 1;
        float currentTime = time;
        while (t > 0)
        {
            currentTime -= Time.deltaTime;
            t = currentTime / time;

            foreach (SpriteRenderer r in renderers)
            {
                r.material.SetFloat("_DissolveAmount", t);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
