using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite cardBack;

    public int cardIndex;

    public void ToggleFace(bool shouldShowFace)
    {
        if (shouldShowFace) spriteRenderer.sprite = faces[cardIndex];
        else spriteRenderer.sprite = cardBack;
    }

    void Awake(){
       // GetComponent<>
    }
}
