using UnityEngine;
using System.Collections;
using System.Net;

public class InputLine : MonoBehaviour
{
    private string input;
    private string url;
    private string imgUrl;

    void Start()
    {
        input = "Insert a keyword.";
        url = null;
    }

    void ResearchImage()
    {
        if (getInput() != null && getInput() != "Insert a keyword.")
        {
            url = "http://www.google.de/search?q=" + input + "&tbm=isch&gbv=2&sei=53lcV8h2wqiABuC0paAN&gws_rd=ssl"; //"http://www.google.de/search?q=tree&tbm=isch&biw=1366&bih=623&gbv=1&sei=S25cV7LeLuqUgAbZ2pKgDw";
        }
        print(url);

        string xmlStr;
        using (var wc = new WebClient())
        {
            xmlStr = wc.DownloadString(url);
        }

        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
        print("Downloading string");
        // There are various options, set as needed
        htmlDoc.OptionFixNestedTags = true;

        // filePath is a path to a file containing the html
        htmlDoc.LoadHtml(xmlStr);

        imgUrl = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"ires\"]/table/tr[1]/td[1]/a/img").GetAttributeValue("src", null);// ("//*[@id=\"rg_s\"/div[1]/a/img");
        print("imgUrl assigned");
        StartCoroutine(PrintTexture());
    }

    IEnumerator PrintTexture()
    {
        print("Printing");
        // Start a download of the given URL
        WWW www = new WWW(imgUrl);
        print("Waiting");

        // Wait for download to complete
        yield return www;
        print("Assigning");

        // Instantiate new cube & light
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        cube.AddComponent<Rigidbody>();
        cube.transform.position = new Vector3(Random.Range(-5.0f, 10.0f), -0.1f, Random.Range(-15.0f, 0f));
        BoxCollider coll = cube.AddComponent<BoxCollider>();
        coll.size = new Vector3(cube.transform.position.x + 0.0007f, cube.transform.position.y, cube.transform.position.z + 0.0007f);
        coll.isTrigger = true;
        cube.AddComponent<PlayerCollision>();
        

        // assign texture
        cube.GetComponent<Renderer>().material.mainTexture = www.texture;
        print("Assigned");
     }

    void OnGUI()
    {
        input = GUI.TextField(new Rect(10, Screen.height - 30, 200, 20), input, 25);
    }

    string getInput()
    {
        return input;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print(input);
            ResearchImage();
            //ResearchAudio();
        }
    }

    void ResearchAudio()
    {

    }

    void CreateCollider()
    {

    }
}
