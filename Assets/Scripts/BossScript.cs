using Unity.Mathematics;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] bool bossStart;
    bool attackComplete;
    int attackPattern;
    public int currentButton;
    [SerializeField] float timer;
    [SerializeField] float attackSpeed;

    Vector3 movingGroundGoal;
    bool movingGroundComplete;

    Vector3 spikeGroundGoal;
    Vector3 spikeGroundStart;
    Vector3 spikeGroundEnd;
    bool spikeGroundComplete;

    Vector3 rotation;
    [SerializeField] float rotationSpeed;
    Transform lazerStar;

    [SerializeField] Vector3 lazerSweepGoal;
    [SerializeField] Vector3 lazerSweepStart;
    [SerializeField] Vector3 lazerSweepEnd;
    [SerializeField] bool lazerSweepComplete;

    [SerializeField] 

    void Awake()
    {
        //bossStart = false;
        lazerStar = transform.GetChild(2);
    }

    void Update()
    {
        if (bossStart)
        {
            timer += Time.deltaTime;

            if (!movingGroundComplete)
            {
                transform.GetChild(0).position = Vector3.Lerp(transform.GetChild(0).position, movingGroundGoal + transform.position, 0.8f * Time.deltaTime);
                if (transform.GetChild(0).position + transform.position == movingGroundGoal + transform.position)
                {
                    movingGroundComplete = true;
                }
            }

            if (timer > attackSpeed)
            {
                attackPattern++;
                if (attackPattern > 2)
                {
                    attackPattern = 0;
                }
                timer -= attackSpeed;
                
            }
            switch (attackPattern)
            {
                case 0:
                    spikeGroundComplete = false;
                    spikeGroundGoal = spikeGroundEnd;
                    break;
                case 1:
                    lazerStar.GetChild(0).gameObject.SetActive(true);
                    lazerStar.GetChild(1).gameObject.SetActive(true);
                    lazerStar.GetChild(2).gameObject.SetActive(true);
                    lazerStar.GetChild(3).gameObject.SetActive(true);
                    lazerStar.GetChild(4).gameObject.SetActive(true);
                    rotation.z += Time.deltaTime * rotationSpeed;
                    lazerStar.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
                    break;
                case 2:
                    lazerStar.GetChild(0).gameObject.SetActive(false);
                    lazerStar.GetChild(1).gameObject.SetActive(false);
                    lazerStar.GetChild(2).gameObject.SetActive(false);
                    lazerStar.GetChild(3).gameObject.SetActive(false);
                    lazerStar.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(true);
                    lazerSweepComplete = false;
                    lazerSweepGoal = lazerSweepEnd;
                    break;
            }

            if (!spikeGroundComplete && timer > 0.1f)
            {
                transform.GetChild(1).position = Vector3.Lerp(transform.GetChild(1).position, spikeGroundGoal + transform.position, 5f * Time.deltaTime);
                if (transform.GetChild(1).position == spikeGroundEnd + transform.position)
                {
                    spikeGroundGoal = spikeGroundStart;
                }
                else if (transform.GetChild(1).position == spikeGroundStart + transform.position)
                {
                    spikeGroundComplete = true;
                    attackComplete = true;
                }
            }
            if (!lazerSweepComplete && timer > 0.1f)
            {
                transform.GetChild(3).position = Vector3.Lerp(transform.GetChild(3).position, lazerSweepGoal + transform.position, 5f * Time.deltaTime);
                if (transform.GetChild(3).position == lazerSweepEnd + transform.position)
                {
                    lazerSweepGoal = lazerSweepStart;
                }
                else if (transform.GetChild(3).position == lazerSweepStart + transform.position)
                {
                    lazerSweepComplete = true;
                    attackComplete = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossStart = true;
        movingGroundComplete = false;
    }
}
