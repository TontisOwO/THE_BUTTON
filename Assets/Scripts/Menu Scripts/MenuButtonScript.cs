using NUnit.Framework;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class MenuButtonScript : MonoBehaviour
{
    [SerializeField] bool inflate;
    [SerializeField] float lerpAmount;

    [SerializeField] Vector2 enterSize;
    [SerializeField] Vector2 originalSize;
    RectTransform myTransform;

    void Start()
    {
        myTransform = GetComponent<RectTransform>();
        enterSize.x = myTransform.localScale.x * 1.1f;
        enterSize.y = myTransform.localScale.y * 1.1f;
        originalSize = myTransform.localScale;
    }


    void Update()
    {
        if (inflate)
        {
            myTransform.localScale = Vector2.Lerp(enterSize, myTransform.localScale, Mathf.Pow(0.5f, Time.deltaTime * lerpAmount));
        }
        else
        {
            myTransform.localScale = Vector2.Lerp(originalSize, myTransform.localScale, Mathf.Pow(0.5f, Time.deltaTime * lerpAmount));
        }
    }

    public void MouseEnter()
    {
        inflate = true;
    }
    public void MouseExit()
    {
        inflate = false;
    }
}

