using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int[] randomSpikesLower = new int[17];
    private int[] randomSpikesUpper = new int[17];

    void Start()
    {
        StartCoroutine(startWave());
    }

    void Update() { }

    IEnumerator startWave()
    {
        Debug.Log("startwave");

        yield return null;
        generateRandomSpikes();

        yield return null;
        FoldChosenSpikes();
        yield return null;
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
}
