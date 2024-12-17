using System;
using UnityEngine;

[Serializable]
public class CardVisual
{
    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private bool _rotates;

    public Sprite Sprite => _sprite;

    public bool Rotates => _rotates;
}
