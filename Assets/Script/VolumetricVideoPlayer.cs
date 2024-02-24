using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumetricVideoPlayer : MonoBehaviour
{
    public Transform frameParent;
    [Range(1, 100)]
    public float frameRate;
    public string directory;
    public int startFrame;
    public int endFrame;
    public bool loop;

    private List<GameObject> frames;
    private bool displayingFrame;
    private int currentListIndex;

    void Start()
    {
        frames = new List<GameObject>();
        frames.AddRange(Resources.LoadAll<GameObject>(directory));

        currentListIndex = startFrame;
    }

    void Update()
    {
        if (!displayingFrame)
        {
            if (!loop && currentListIndex + 1 >= endFrame)
                return;

            if (currentListIndex + 1 >= endFrame)
                currentListIndex = startFrame;
            else
                currentListIndex++;

            StartCoroutine(DisplayFrame(currentListIndex));
        }
    }

    IEnumerator DisplayFrame(int index)
    {
        GameObject instantiatedFrame = Instantiate(frames[index], frameParent);
        displayingFrame = true;

        yield return new WaitForSeconds(1/frameRate);

        displayingFrame = false;
        Destroy(instantiatedFrame);
    }
}
