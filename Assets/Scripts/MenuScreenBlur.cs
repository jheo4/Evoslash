using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MenuScreenBlur : MonoBehaviour
{
    // Instance fields
    private PostProcessVolume volume;
    private bool animatingIn;
    private bool animatingOut;
    private float animationDuration = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        this.volume = GetComponent<PostProcessVolume>();
        this.animatingIn = false;
        this.animatingOut = false;
        this.volume.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.animatingIn)
        {
            this.volume.weight = Mathf.Clamp(this.volume.weight + (Time.unscaledDeltaTime / this.animationDuration), 0f, 1f);
            if (Mathf.Abs(this.volume.weight - 1f) < Mathf.Epsilon)
            {
                this.animatingIn = false;
                this.volume.weight = 1f;
            }
        }

        if (this.animatingOut)
        {
            this.volume.weight = Mathf.Clamp(this.volume.weight - (Time.unscaledDeltaTime / this.animationDuration), 0f, 1f);
            if (Mathf.Abs(this.volume.weight) < Mathf.Epsilon)
            {
                this.animatingOut = false;
                this.volume.weight = 0f;
            }
        }
    }

    // Blur can be called to unblur the screen
    public void Blur(float duration)
    {
        this.animatingIn = true;
        this.animatingOut = false;
        this.animationDuration = duration;
    }

    // Unblur can be called to unblur the screen
    public void Unblur(float duration)
    {
        this.animatingIn = false;
        this.animatingOut = true;
        this.animationDuration = duration;
    }
}
