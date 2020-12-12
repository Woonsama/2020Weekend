using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomItem : MonoBehaviour
{
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
        public System.Action<LobbyCharacter.AccessorieType, string> onClick;
    }

    UIData _uiData;

    System.Action<LobbyCharacter.AccessorieType, string> _onClickAction;

    public void Init(UIData data)
    {
        _uiData = data;
        _image.sprite = Resources.Load<Sprite>("Prefab/Acc/" + data.iconName);
        _text.text = data.itemName;
        _onClickAction = data.onClick;
    }

    public void OnClickItem()
    {
        if(_onClickAction != null)
        {
            _onClickAction(_uiData.accType, _uiData.prefabName);
        }
    }

}
