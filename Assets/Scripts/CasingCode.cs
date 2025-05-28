using UnityEngine;

public class CasingCode : MonoBehaviour
{
    [SerializeField] bool isBroken;
    [SerializeField] bool isPressed;
    [SerializeField] int currentButton;
    [SerializeField] float timer;
    [SerializeField] CircleCollider2D myCasing;
    [SerializeField] Camera myCamera;
    [SerializeField] Sprite buttonFrame1;
    [SerializeField] Sprite buttonFrame2;
    [SerializeField] Sprite buttonFrame3;
    [SerializeField] Sprite buttonFrame4;
    [SerializeField] Sprite buttonFrame5;
    [SerializeField] Sprite buttonFrame6;
    [SerializeField] Vector3 fingerStart;
    [SerializeField] Vector3 fingerMiddle;
    [SerializeField] Vector3 fingerEnd;

    private void Awake()
    {
        myCamera = Camera.main;
        myCasing = transform.GetComponent<CircleCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBroken && collision.transform.CompareTag("Player"))
        {
            myCamera.transform.GetChild(0).gameObject.SetActive(true);
            myCamera.transform.GetChild(1).gameObject.SetActive(true);
            myCamera.transform.GetChild(2).gameObject.SetActive(true);
            isBroken = true;
            Debug.Log("helo");
        }
    }
    private void Update()
    {
        if (isBroken && !isPressed)
        {
            currentButton = transform.parent.parent.parent.parent.parent.GetComponent<BossScript>().currentButton;

            if (myCamera.transform.GetChild(0).gameObject.activeSelf == true)
            {
                if (timer < 5)
                {
                    switch (currentButton)
                    {
                        case 0:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame1;
                            break;
                        case 1:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame2;
                            break;
                        case 2:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame3;
                            break;
                    }
                }
                else
                {
                    switch (currentButton)
                    {
                        case 0:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame4;
                            break;
                        case 1:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame5;
                            break;
                        case 2:
                            myCamera.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = buttonFrame6;
                            break;
                    }
                }
                if (Input.anyKeyDown == true)
                {
                    timer += 0.1f;
                    Debug.Log("press");
                }
                if (timer == 0)
                {
                    myCamera.transform.GetChild(2).position = fingerStart;
                }
                else if (timer > 5)
                {
                    myCamera.transform.GetChild(2).position = fingerEnd + transform.position;
                    currentButton++;
                    transform.parent.parent.parent.parent.parent.GetComponent<BossScript>().currentButton = currentButton;
                }
                else if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    myCamera.transform.GetChild(2).position = fingerMiddle;
                }
            }
        }

        if (isPressed)
        {
            myCamera.transform.GetChild(0).gameObject.SetActive(false);
            myCamera.transform.GetChild(1).gameObject.SetActive(false);
            myCamera.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
