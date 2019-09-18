using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour
{
    // solid color (alpha is 1)
    public Color solidColor = Color.white;

    // clear color (alpha is 0)
    public Color clearColor = new Color (1f, 1f, 1f, 0f);

    // delay before iTweening
    public float delay = 0.5f;

    // time for iTween animation
    public float timeToFade = 2f;

    // ease in-out for iTween animation
    public iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

    // reference to Maskable Graphic component
    MaskableGraphic graphic;

    void Awake()
    {
    	// cache the graphic
        graphic = GetComponent<MaskableGraphic>();
    }

	// use this Update the color during the iTween.ValueTo
    void UpdateColor(Color newColor)
    {
        graphic.color = newColor;
    }

   	// use the iTween.ValueTo method to transition from solid color to clear color
    public void FadeOff()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", solidColor,
            "to", clearColor,
            "time", timeToFade,
            "delay", delay,
            "easetype", easeType,
            "onupdatetarget", gameObject,
            "onupdate", "UpdateColor"
        ));
    }

	// use the iTween.ValueTo method to transition from clear color to solid color
	public void FadeOn()
	{
		iTween.ValueTo(gameObject, iTween.Hash(
            "from", clearColor,
            "to", solidColor,
			"time", timeToFade,
			"delay", delay,
			"easetype", easeType,
			"onupdatetarget", gameObject,
			"onupdate", "UpdateColor"
		));
	}
}
