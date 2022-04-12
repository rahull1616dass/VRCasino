﻿using System;
using UnityEngine;

public enum EFlipType
{
    None = 0,
    EFlipBack,
    EFlipFront
}
[RequireComponent(typeof(MeshRenderer))]
public class CardDisplay : MonoBehaviour
{
    //private Sprite _backCardSprite;
    private Transform _thisObjectTransform;

    public Card Card { get; private set; }

    private void Awake()
    {
        _thisObjectTransform = transform;
        FlipTheCard();
        //_backCardSprite = _material.sprite;
    }

    public void Reset()
    {
        FlipTheCard();

        Card = null;
    }

    private void FlipTheCard(EFlipType flipType = EFlipType.EFlipBack)
    {
        _thisObjectTransform.localEulerAngles = new Vector3(_thisObjectTransform.rotation.eulerAngles.x, _thisObjectTransform.rotation.eulerAngles.y, 180f * (int)flipType);
        _thisObjectTransform.GetComponent<Animator>().Play("flipCardAnim");
    }

    public void SetCard(Card card)
    {
        Card = card;
        GetComponent<Renderer>().material = card.Material;
    }

    public void FaceUp()
    {
        if (Card == null) {
            throw new Exception("The card was not defined!");
        }
        FlipTheCard(EFlipType.EFlipFront);
    }
}