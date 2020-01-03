using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MainCamera");
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

         DontDestroyOnLoad(this.gameObject);
    }
}
