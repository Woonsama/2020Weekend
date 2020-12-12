using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames;
using Photon.Pun;
using System.Xml;
public class LobbyView2D : MonoBehaviour 
{
    [SerializeField]
    GameObject _characterRoot;

    [SerializeField]
    GameObject _gridObj;
    readonly string _defaultUnitName = "char_bear_brown";
    LobbyCharacter _lobbyCharacter;

    private void Awake()
    {
        CreateCharacter();
        InitAccListItems();
    }

    public void CreateCharacter(string prefabName = "")
    {
        if (prefabName == "")
        {
            prefabName = _defaultUnitName;
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

        _lobbyCharacter = lobbyCharacter;
    }

    public void InitAccListItems()
    {
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
            gameObj.GetComponent<CharacterCustomItem>().Init(uiData);

        }

       


    }

    public void OnClickAccItem(LobbyCharacter.AccessorieType acType, string prefabName)
    {
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
        }
    }


    private void Update()
    {
        if (_lobbyCharacter != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                _lobbyCharacter.transform.Rotate(Vector3.up * 100f * Time.deltaTime);

            if (Input.GetKey(KeyCode.RightArrow))
                _lobbyCharacter.transform.Rotate(-Vector3.up * 100f * Time.deltaTime);
        }
    }
}
