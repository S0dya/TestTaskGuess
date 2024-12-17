using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField]
    private string _identifier;

    [SerializeField]
    private CardVisual _cardVisual;

    public string Identifier => _identifier;

    public CardVisual CardVisual => _cardVisual;
}
