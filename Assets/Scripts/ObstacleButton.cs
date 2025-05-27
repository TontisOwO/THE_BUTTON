using UnityEngine;

public class ObstacleButton : MonoBehaviour
{
    [SerializeField] GameObject[] myObstacle;
    [SerializeField] Movement player;

    [SerializeField] int obstacleType;
    [SerializeField] bool isBigButton;
    [SerializeField] bool isActivated = false;
    
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isBigButton && collision.gameObject.tag == "Player" && !isActivated)
        {
            FigureOutWhatObstacle();
            isActivated = true;
        }
        
        if (isBigButton && collision.gameObject.tag == "Player" && !isActivated) //&& player.isDashing = true
        {
            FigureOutWhatObstacle();
            isActivated = true;
        }
    }

    void FigureOutWhatObstacle()
    {
        for (int i = 0; i < myObstacle.Length; i++)
        {
            string obstacleName = null;
            myObstacle[i].gameObject.name = obstacleName;

            switch (obstacleName)
            {
                case "Spike":
                    {
                        obstacleType = 0;
                        break;
                    }
                case "Turret":
                    {
                        obstacleType = 1;
                        break;
                    }
                case "Hydraulic Press":
                    {
                        obstacleType = 2;
                        break;
                    }
                case "Laser":
                    {
                        obstacleType = 3;
                        break;
                    }
            }

            switch (obstacleType)
            {
                case 0: //spike
                    {
                        SpikeScript spike = myObstacle[i].GetComponent<SpikeScript>();
                        spike.Deactivation();
                        break;
                    }
                case 1: //turret
                    {
                        TurretShoot turret = myObstacle[i].GetComponent<TurretShoot>();
                        break;
                    }
                case 2: //hydraulic press
                    {
                        HydraulicPress hydraulicPress = myObstacle[i].GetComponent<HydraulicPress>();
                        break;
                    }
                case 3: // laser
                    {
                        LazerFire laser = myObstacle[i].GetComponent<LazerFire>();
                        laser.isActive = false;
                        break;
                    }
            }
        }
    }
}
