using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int[] randomSpikesLower = new int[17];
    private int[] randomSpikesUpper = new int[17];
    private List<GameObject> activateGhostLasers = new List<GameObject>();

    private GameObject[] GhostSpikes = new GameObject[67];

    [Header("Sound Effects")]
    [SerializeField]
    private AudioClip warningSFX;

    [SerializeField]
    private AudioClip drawSwordSFX;

    [SerializeField]
    private AudioClip laserSFX;

    [Header("Prefabs")]
    public GameObject ghostLaserPrefab;
    public GameObject ghostLaserVerticalPrefab;

    public TMP_Text PowerReserveText;

    public GameObject[] Lasers = new GameObject[12];

    //hi
    private float PowerReserve = 100.0f;

    void Start()
    {
        // get all ghost spikes and put them into an array for later use
        for (int ghostSpike = 1; ghostSpike <= 66; ghostSpike++)
        {
            GameObject spike = GameObject.Find("Ghost Spike (" + ghostSpike + ")");
            GhostSpikes[ghostSpike] = spike;
            spike.SetActive(false);
        }

        StartCoroutine(gameLoop());
    }

    void Update() { }

    IEnumerator gameLoop()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        bool LaserWave = false;
        int numberOfSpikeWaves = 0;
        while (true)
        {
            if (numberOfSpikeWaves >= Random.Range(3, 5))
            {
                numberOfSpikeWaves = 0;
                LaserWave = true;
            }

            if (LaserWave)
            {
                generateRandomLaserPositions();
                ShowGhostLasersHorizontal();
                ShowGhostLasersVertical();
                audioSource.PlayOneShot(warningSFX, 1f);
                yield return new WaitForSeconds(0.75f);
                DestroyAllGhostLasers();

                audioSource.PlayOneShot(laserSFX, 1f);
                ActivateHorizontalLasers();
                ActivateVerticalLasers();
                ChangePowerReserve(-Lasers.Length * 0.5f);
                yield return new WaitForSeconds(1f);

                UnActivateHoriztontalLasers();
                UnActivateVerticalLasers();
                yield return new WaitForSeconds(1f);

                if (Random.Range(1, 3) == 2)
                {
                    LaserWave = true;
                }
                else
                {
                    LaserWave = false;
                }
            }
            else
            {
                generateRandomSpikes();
                audioSource.PlayOneShot(warningSFX, 1f);
                ShowGhostSpikes();
                yield return new WaitForSeconds(1f);

                HideGhostSpikes();

                audioSource.PlayOneShot(drawSwordSFX, 1f);
                FoldChosenSpikes();
                ChangePowerReserve(-1.6f);
                yield return new WaitForSeconds(1f);

                UnfoldChosenSpikes();
                numberOfSpikeWaves++;
            }
        }
    }

    private void ChangePowerReserve(float amount)
    {
        PowerReserve += amount;

        PowerReserve = Mathf.Round(PowerReserve * 10f) / 10f;

        PowerReserve = Mathf.Clamp(PowerReserve, 0f, 100f);

        PowerReserveText.SetText("{0:0.0}%", PowerReserve);
    }

    private void generateRandomSpikes()
    {
        for (int spikeCount = 0; spikeCount < randomSpikesLower.Length; spikeCount++)
        {
            bool duplicate;

            do
            {
                duplicate = false;
                randomSpikesLower[spikeCount] = Random.Range(1, 33);

                for (int arrayCount = 0; arrayCount < spikeCount; arrayCount++)
                {
                    if (randomSpikesLower[spikeCount] == randomSpikesLower[arrayCount])
                    {
                        duplicate = true;
                        break;
                    }
                }
            } while (duplicate);
        }

        for (int spikeCount = 0; spikeCount < randomSpikesUpper.Length; spikeCount++)
        {
            bool duplicate;

            do
            {
                duplicate = false;
                randomSpikesUpper[spikeCount] = Random.Range(34, 67);

                for (int arrayCount = 0; arrayCount < spikeCount; arrayCount++)
                {
                    if (randomSpikesUpper[spikeCount] == randomSpikesUpper[arrayCount])
                    {
                        duplicate = true;
                        break;
                    }
                }
            } while (duplicate);
        }
    }

    private void generateRandomLaserPositions()
    {
        for (int laserCount = 0; laserCount < Lasers.Length / 2; laserCount++)
        {
            Lasers[laserCount].transform.position = new Vector3(
                Lasers[laserCount].transform.position.x,
                Random.Range(-4.5f, 4.5f),
                Lasers[laserCount].transform.position.z
            );
        }

        for (int laserCount = Lasers.Length / 2; laserCount < Lasers.Length; laserCount++)
        {
            Lasers[laserCount].transform.position = new Vector3(
                Random.Range(15f, -15f),
                Lasers[laserCount].transform.position.y,
                Lasers[laserCount].transform.position.z
            );
        }
    }

    private void ActivateHorizontalLasers()
    {
        for (int i = 0; i < Lasers.Length / 2; i++)
        {
            Laser laser = Lasers[i].GetComponent<Laser>();
            laser.Activate();
        }
    }

    private void UnActivateHoriztontalLasers()
    {
        for (int i = 0; i < Lasers.Length / 2; i++)
        {
            Laser laser = Lasers[i].GetComponent<Laser>();
            laser.UnActivate();
        }
    }

    private void ActivateVerticalLasers()
    {
        for (int i = Lasers.Length / 2; i < Lasers.Length; i++)
        {
            Laser laser = Lasers[i].GetComponent<Laser>();
            laser.Activate();
        }
    }

    private void UnActivateVerticalLasers()
    {
        for (int i = Lasers.Length / 2; i < Lasers.Length; i++)
        {
            Laser laser = Lasers[i].GetComponent<Laser>();
            laser.UnActivate();
        }
    }

    private void FoldChosenSpikes()
    {
        for (int i = 0; i <= 16; i++)
        {
            Spike spike = GameObject
                .Find("Spike (" + randomSpikesLower[i] + ")")
                .GetComponent<Spike>();
            spike.Fold();
            spike = GameObject.Find("Spike (" + randomSpikesUpper[i] + ")").GetComponent<Spike>();
            spike.Fold();
        }
    }

    private void UnfoldChosenSpikes()
    {
        for (int i = 0; i <= 16; i++)
        {
            Spike spike = GameObject
                .Find("Spike (" + randomSpikesLower[i] + ")")
                .GetComponent<Spike>();
            spike.Unfold();
            spike = GameObject.Find("Spike (" + randomSpikesUpper[i] + ")").GetComponent<Spike>();
            spike.Unfold();
        }
    }

    private void ShowGhostSpikes()
    {
        for (int i = 0; i <= 16; i++)
        {
            GhostSpikes[randomSpikesLower[i]].SetActive(true);
            GhostSpikes[randomSpikesUpper[i]].SetActive(true);
        }
    }

    private void HideGhostSpikes()
    {
        for (int i = 0; i <= 16; i++)
        {
            GhostSpikes[randomSpikesLower[i]].SetActive(false);
            GhostSpikes[randomSpikesUpper[i]].SetActive(false);
        }
    }

    void ShowGhostLasersHorizontal()
    {
        for (int i = 0; i < Lasers.Length / 2; i++)
        {
            GameObject ghostLaser = Instantiate(
                ghostLaserPrefab,
                new Vector3(0, Lasers[i].transform.position.y, Lasers[i].transform.position.z),
                ghostLaserPrefab.transform.rotation
            );
            activateGhostLasers.Add(ghostLaser);
        }
    }

    void ShowGhostLasersVertical()
    {
        for (int i = Lasers.Length / 2; i < Lasers.Length; i++)
        {
            GameObject ghostLaser = Instantiate(
                ghostLaserVerticalPrefab,
                new Vector3(Lasers[i].transform.position.x, 0, Lasers[i].transform.position.z),
                ghostLaserVerticalPrefab.transform.rotation
            );
            activateGhostLasers.Add(ghostLaser);
        }
    }

    void DestroyAllGhostLasers()
    {
        foreach (GameObject ghostLaser in activateGhostLasers)
        {
            Destroy(ghostLaser);
        }
        activateGhostLasers.Clear();
    }
}
