using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindListeners : MonoBehaviour
{
    void Start()
    {
        AudioListener[] myListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];

        int totalListeners = 0;//find out how many listeners are actually active

        foreach (AudioListener thisListener in myListeners)
        {
            if (thisListener.enabled) { totalListeners++; }
        }

        if (totalListeners > 1)
        {
            //turn off my audioListener component
            AudioListener al = GetComponent<AudioListener>();
            al.enabled = false;
        }
        else
        {
            //turn on my audioListener component
            AudioListener al = GetComponent<AudioListener>();
            al.enabled = true;
            //print ("turn on audio "+name);
        }
    }

}
