using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]
    private Image _cardImage;

    [SerializeField]
    private Button _guessButton;
    
    public void Init(Sprite sprite, Action guessAction, bool isRight, bool animate)
    {
        _cardImage.sprite = sprite;

        _guessButton.onClick.AddListener(() => 
        { 
            guessAction?.Invoke(); 
            if (isRight)
            {

            }
        });

        if (animate)
        {

        }
    }
}
