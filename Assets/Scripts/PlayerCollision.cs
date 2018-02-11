using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour
{
    private reJSON rejson;
    private AudioSource audioFile;

    void Start()
    {
        rejson = reJSON.getInstance();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            WWW www = new WWW(rejson.thomas.results[0].data.url);
            audioFile = GetComponent<AudioSource>();
            audioFile.clip = www.audioClip;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            print("Collision exit.");
        }
    }
}
