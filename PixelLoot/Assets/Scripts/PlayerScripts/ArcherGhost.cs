using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherGhost : MonoBehaviour
{
    public float ghostDelay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyGhost", ghostDelay);
    }

    void DestroyGhost()
    {
        Destroy(gameObject);
    }
}
