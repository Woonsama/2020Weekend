using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames;
using Photon.Pun;
using System.Xml;
public class LobbyCustomView2D : MonoBehaviour 
{
    [SerializeField]
    GameObject _characterRoot;

    [SerializeField]
    GameObject _gridObj;

    LobbyCharacter _lobbyCharacter;

    int _currentSelectItemIndex = -1;
    Dictionary<int, CharacterCustomItem> _itemDic = new Dictionary<int, CharacterCustomItem>();

    private void Awake()
    {
        InitAccListItems();
        CreateCharacter();
    }

    public void CreateCharacter(string prefabName = "")
    {
        var curUserData = UserManager.Instance.GetCurUserData();
        if (prefabName == "")
        {
            prefabName = curUserData.setCharPrefabName;
        }

        foreach (Transform transform in _characterRoot.transform)
        {
            Destroy(transform.gameObject);
        }

        GameObject loadObj = Resources.Load("Prefab/" + prefabName) as GameObject;
        GameObject gameObj = GameObject.Instantiate(loadObj);
        LobbyCharacter lobbyCharacter = gameObj.GetComponent<LobbyCharacter>();
        gameObj.transform.SetParent(_characterRoot.transform);
        gameObj.transform.localScale = Vector3.one;
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        lobbyCharacter.SetAnim(LobbyCharacter.AnimType.Dance);

        if(curUserData.setHatPrefabName != "")
        {
            lobbyCharacter.CreateAcc(LobbyCharacter.AccessorieType.Hat, curUserData.setHatPrefabName);
        }

        UserManager.Instance.SetCharData(prefabName);
        _lobbyCharacter = lobbyCharacter;
    }

    public void InitAccListItems()
    {
        var curUserData = UserManager.Instance.GetCurUserData();
        List<CharacterCustomItem.UIData> datas = new List<CharacterCustomItem.UIData>();
        var xmlDoc = XmlHelper.LoadXML("AccTable");
        XmlNodeList all_nodes = xmlDoc.SelectNodes("read/ACC");
        foreach (XmlNode node in all_nodes)
        {
            int pindex = -1;
            int.TryParse(node.SelectSingleNode("index").InnerText, out pindex);
            int acType = 0;
            int.TryParse(node.SelectSingleNode("accType").InnerText, out acType);
            var uiData = new CharacterCustomItem.UIData
            {
                iconName = node.SelectSingleNode("listImageName").InnerText,
                prefabName = node.SelectSingleNode("prefabName").InnerText,
                index = pindex,
                accType = (LobbyCharacter.AccessorieType)acType,
                itemName = node.SelectSingleNode("itemName").InnerText,
                onClick = OnClickAccItem
            };
            

            GameObject loadObj = Resources.Load("Prefab/CharacterCustomItem") as GameObject;
            GameObject gameObj = GameObject.Instantiate(loadObj, _gridObj.transform);
            var cci = gameObj.GetComponent<CharacterCustomItem>();
            cci.Init(uiData);

            _itemDic[uiData.index] = cci;

            if(uiData.prefabName == curUserData.setHatPrefabName)
            {
                cci.HighlightItem();
                _currentSelectItemIndex = uiData.index;
            }

        }

       


    }

    public void OnClickAccItem(LobbyCharacter.AccessorieType acType, string prefabName,int index)
    {
        if(_currentSelectItemIndex != -1)
        {
            _itemDic[_currentSelectItemIndex].DisHighlightItem();
        }

        if(_lobbyCharacter == null)
        {
            foreach(Transform tr in _characterRoot.transform)
            {
                var lobbyChar = tr.GetComponent<LobbyCharacter>();
                if (lobbyChar != null)
                {
                    _lobbyCharacter = lobbyChar;
                    break;
                }

            }
        }

        if (_lobbyCharacter != null)
        {
            _lobbyCharacter.CreateAcc(acType, prefabName);
            UserManager.Instance.SetHatData(prefabName);
        }

        _currentSelectItemIndex = index;
        _itemDic[_currentSelectItemIndex].HighlightItem();
    }

    public void OnResponseLeftArrow()
    {
        if(_lobbyCharacter !=null)        
            _lobbyCharacter.transform.Rotate(new Vector3(0,20,0));
    }

    public void OnResponseRightArrow()
    {
        if (_lobbyCharacter != null)
            _lobbyCharacter.transform.Rotate(new Vector3(0, -20, 0));
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
            OnResponseLeftArrow();

        if (Input.GetKey(KeyCode.RightArrow))
            OnResponseRightArrow();

        if (Input.GetKeyDown("escape"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
