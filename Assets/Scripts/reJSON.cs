using UnityEngine;
using System.Collections;
using System.IO;
using LitJson;
using System.Net;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

public class Result
{
    public ResultObject[] results;
}

public class ResultObject
{
    public Data data;
}

public class Data
{
    public string url;
}

public class reJSON : MonoBehaviour {
    private string jsonString;
    string line;
    public Result thomas;

    //public virtual WebHeaderCollection Headers = { "contentType": "application/Json" };
    WebRequest myRequest = WebRequest.Create("https://dubhack.dubsmash.com/snips/search?term=icecream");
    // Use this for initialization
    void Start ()
    {
        myRequest.ContentType = "application/json";
        System.Net.ServicePointManager.ServerCertificateValidationCallback +=
        delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                System.Security.Cryptography.X509Certificates.X509Chain chain,
                                System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
             return true; // **** Always accept
        };
        WebResponse response = myRequest.GetResponse();
        //itemData = JsonMapper.ToObject(response);
        //Debug.Log(itemData["results"][KEYWORD][url]);
        using (Stream stream = response.GetResponseStream())
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string responseString = reader.ReadToEnd();
            print(responseString);

           thomas = JsonMapper.ToObject<Result>(responseString);

            print(thomas.results[0].data.url);
        }
    }

    static reJSON instance = null;
    public static reJSON getInstance()
    {
        if (instance == null)
        {
            instance = new reJSON();
        }
        return instance;
    }
}
