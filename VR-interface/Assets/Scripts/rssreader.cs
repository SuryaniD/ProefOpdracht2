using UnityEngine;
using System.Collections;
using System.Xml;
using UnityEngine.Networking;
using UnityEngine.UI;

public class rssreader : MonoBehaviour
{

    public string url = "http://www.nu.nl/rss/Games";

    public GameObject scherm1;
    public Image plaatje1;
    public string urlPlaatje = "";

    void Start() {
        StartCoroutine(News());
    }

    public IEnumerator News() {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.isDone)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.downloadHandler.text);

            XmlNodeList xmlNodeList = xmlDoc.SelectNodes("rss/channel/item/title");

            XmlNodeList xmlNodeList2 = xmlDoc.SelectNodes("rss/channel/item/enclosure/@url");

            for (int i = 0; i < xmlNodeList.Count; i++) {
                Debug.Log(xmlNodeList[i].InnerText);
                Debug.Log(xmlNodeList2[i].InnerText);
            }

            scherm1.GetComponent<Text>().text = xmlNodeList[1].InnerText;
            urlPlaatje = xmlNodeList2[1].InnerText;

            StartCoroutine(Plaatje());

            Debug.Log("Complete");

        }
    }

    public IEnumerator Plaatje() {
        WWW www = new WWW(urlPlaatje);
        yield return www;
        plaatje1.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    }


}