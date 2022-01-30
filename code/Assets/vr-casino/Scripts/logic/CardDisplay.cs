using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CardDisplay : MonoBehaviour
{
    private Material _material;
    //private Sprite _backCardSprite;

    public Card Card { get; private set; }

    private void Awake()
    {
        _material = GetComponent<Material>();

        //_backCardSprite = _material.sprite;
    }

    public void Reset()
    {
        //_material.sprite = _backCardSprite;

        Card = null;
    }

    public void SetCard(Card card)
    {
        Card = card;
    }

    public void FaceUp()
    {
        if (Card == null) {
            throw new Exception("The card was not defined!");
        }

        if (/*_material.sprite.GetInstanceID() == _backCardSprite.GetInstanceID()*/true) {
            _material = Card.Material;
        }
    }
}