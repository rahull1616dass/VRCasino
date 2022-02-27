using System;
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
    private Material _material;
    //private Sprite _backCardSprite;
    private Transform _thisObjectTransform;

    public Card Card { get; private set; }

    private void Awake()
    {
        _material = GetComponent<Material>();
        
        FlipTheCard();
        //_backCardSprite = _material.sprite;
    }

    public void Reset()
    {
        FlipTheCard();

        Card = null;
    }

    private void FlipTheCard(EFlipType flipType=EFlipType.EFlipBack)
    {
        _thisObjectTransform.Rotate(new Vector3(_thisObjectTransform.rotation.eulerAngles.x, _thisObjectTransform.rotation.eulerAngles.y, 180f * (int)flipType));
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
        FlipTheCard(EFlipType.EFlipFront);
    }
}