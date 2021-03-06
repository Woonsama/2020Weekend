﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomItem : MonoBehaviour
{
    Color _highlightColor = new Color(255, 255, 0);
    Color _normalColor = new Color(255, 255, 255);

    [SerializeField]
    UnityEngine.UI.Image _image;

    [SerializeField]
    TMPro.TextMeshProUGUI _text;

    public class UIData
    {
        public string iconName;
        public string prefabName;
        public int index;
        public LobbyCharacter.AccessorieType accType;
        public string itemName;
        public System.Action<LobbyCharacter.AccessorieType, string,int> onClick;
    }

    UIData _uiData;

    System.Action<LobbyCharacter.AccessorieType, string,int> _onClickAction;

    public void Init(UIData data)
    {
        _uiData = data;
        _image.sprite = Resources.Load<Sprite>("Sprite/" + data.iconName);
        _text.text = data.itemName;
        _onClickAction = data.onClick;
    }

    public void OnClickItem()
    {
        if(_onClickAction != null)
        {
            _onClickAction(_uiData.accType, _uiData.prefabName,_uiData.index);
        }
    }

    public void HighlightItem()
    {
        _image.color = _highlightColor;
    }

    public void DisHighlightItem()
    {
        _image.color = _normalColor;
    }

}
