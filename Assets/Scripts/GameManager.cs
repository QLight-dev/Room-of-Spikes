using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int[] randomSpikesLower = new int[17];
    private int[] randomSpikesUpper = new int[17];

    private GameObject[] GhostSpikes = new GameObject[67];

    void Start()
    {
        // get all ghost spikes and put them into an array for later use
        for (int ghostSpike = 1; ghostSpike <= 66; ghostSpike++)
        {
            GameObject spike = GameObject.Find("Ghost Spike (" + ghostSpike + ")");
            GhostSpikes[ghostSpike] = spike;
            spike.SetActive(false);
        }

        StartCoroutine(startWave());
    }

    void Update() { }

    IEnumerator startWave()
    {
        Debug.Log("startwave");

        generateRandomSpikes();
        ShowGhostSpikes();
        yield return new WaitForSeconds(1);

        HideGhostSpikes();

        FoldChosenSpikes();
        yield return new WaitForSeconds(1);

        UnfoldChosenSpikes();
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
                randomSpikesUpper[spikeCount] = Random.Range(34, 66);

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

        Debug.Log(string.Join(", ", randomSpikesLower));
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
}
