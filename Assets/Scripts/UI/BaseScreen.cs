using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseScreen : MonoBehaviour
{
    public virtual void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideSceen()
    {
        gameObject.SetActive(false);
    }
}
