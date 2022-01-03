using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchesLava : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //other.transform.position = new Vector3(0, 5, 0);
            SceneManager.LoadScene(0);
        }

    }
}
