using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneticManager : MonoBehaviour
{
    public GameObject attackPrefab;
    List<Attack> attackerList;
    public int nbAttackers;

    public TextMeshProUGUI txtGen;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtActuel;

    int bestScore = 0;
    float score = 0;

    int gen = 0;
    int bestNum = 2;
    public float mutation = 0.05f;

    public void nextGen()
    {
        gen++;
        if(attackerList == null)
        {
            attackerList = new List<Attack>();

            for(int i = 0; i < nbAttackers; i++)
            {
                Attack atk = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<Attack>();
                atk.GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                //create the network
                atk.network = new NeuralNetwork(2, 10, 1);
                atk.network.InitializeRandomWeight();
                attackerList.Add(atk);
            }
        }
        else
        {
            attackerList.Sort((x, y) => x.score.CompareTo(y.score));
            while(attackerList.Count > bestNum)
            {
                Destroy(attackerList[0].gameObject);
                attackerList.RemoveAt(0);
            }

            if((int)attackerList[attackerList.Count - 1].score > bestScore) bestScore = (int)attackerList[attackerList.Count - 1].score; 

            List<Attack> nextGen = Reproduce(attackerList, nbAttackers - bestNum);

            foreach (Attack parentAtk in attackerList)
            {
                Attack atk = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<Attack>();
                atk.GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

                //create the network
                atk.network = parentAtk.network;
                nextGen.Add(atk);
                Destroy(parentAtk.gameObject);
            }
            attackerList = nextGen;
        }
    }
    
    private List<Attack> Reproduce(List<Attack> parentsAttacks, int numChild)
    {
        int numParents = parentsAttacks.Count;
        List<Attack> childs = new List<Attack>();

        for(int i = 0; i < numChild; i++)
        {
            Attack atk1 = parentsAttacks[Random.Range(0, numParents)];
            Attack atk2 = parentsAttacks[Random.Range(0, numParents)];
            while (atk1 == atk2)
                atk2 = parentsAttacks[Random.Range(0, numParents)];

            Attack atk = Instantiate(attackPrefab, transform.position, Quaternion.identity).GetComponent<Attack>();
            atk.GetComponent<SpriteRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

            atk.network = NeuralNetwork.Fuse(atk1.network, atk2.network, mutation);
            childs.Add(atk);
        }

        return childs;
    }

    bool AllDead()
    {
        foreach (Attack atk in attackerList)
        {
            if (atk.alive)
                return false;
        }
        return true;
    }

    public void StartGameLoop()
    {
        txtGen.SetText("GENERATION : " + gen);
        txtScore.SetText("MEILLEUR SCORE : " + bestScore);
        score = 0;

        foreach (Attack atk in attackerList)
        {
            StartCoroutine(atk.GameLoop());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        txtScore.SetText("MEILLEUR SCORE : 0");
        nextGen();
        StartGameLoop();
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime;
        txtActuel.SetText("SCORE ACTUEL : " + (int)score);
        if (AllDead())
        {
            FindObjectOfType<SpawnerEnemy>().clearMap();
            nextGen();
            StartGameLoop();
        }
    }
}
