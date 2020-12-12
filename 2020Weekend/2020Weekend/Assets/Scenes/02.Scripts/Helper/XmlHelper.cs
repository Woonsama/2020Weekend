using System.Xml;
using UnityEngine;

public class XmlHelper : MonoBehaviour
{

    public static XmlDocument LoadXML(string fileName)
    {
        TextAsset txtAsset = (TextAsset)Resources.Load("Table/" + fileName);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtAsset.text);
        return xmlDoc;
    }

}
