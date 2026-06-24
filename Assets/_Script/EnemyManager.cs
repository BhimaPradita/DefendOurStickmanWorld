using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   public static EnemyManager main;

   public Transform spampoint;
   public Transform[] checkpoints;

   [SerializeField] private GameObject enemy;
   [SerializeField] private GameObject fastEnemy;
   [SerializeField] private GameObject tankEnemy;

   [SerializeField] private int wave = 1;
   [SerializeField] private int baseEnemyCount = 6;
   [SerializeField] private float enemyCountRate = 0.2f;
   [SerializeField] private float spawnDelayMax = 1f;
   [SerializeField] private float spawnDelayMin = 0.75f;

   [SerializeField] private float enemyRate = 0.5f;
   [SerializeField] private float fastEnemyRate = 0.4f;
   [SerializeField] private float tankEnemyRate = 0.1f;

   private bool wavedone = false;
   private List<GameObject> waveset = new List<GameObject>();
   private int enemyLeft;

   private int enemyCount;
   private int fastEnemyCount;
   private int tankEnemyCount;

   void Awake()
   {
      main = this;
   }

   void Start()
   {
      SetWave();
   }

   void Update()
   {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
      if (Input.GetKeyDown(KeyCode.Return) && wavedone && enemies.Length == 0)
      {
         wave++;
         wavedone = false;
         enemyCount += Mathf.RoundToInt(enemyCount * enemyCountRate);
         SetWave();
      }
      if (Input.GetKeyDown(KeyCode.D) && wavedone)
      {
         for (int i = 0; i < enemies.Length; i++)
         {
            Destroy(enemies[i]);
         }
      }
   }

   private void SetWave()
   {
      enemyCount = Mathf.RoundToInt(baseEnemyCount *(enemyRate + tankEnemyRate));
      fastEnemyCount = Mathf.RoundToInt(baseEnemyCount * fastEnemyRate);
      tankEnemyCount = 0;

      if (wave % 5 == 0)
      {
         enemyCount = Mathf.RoundToInt(baseEnemyCount * enemyRate);
         tankEnemyCount = Mathf.RoundToInt(baseEnemyCount * tankEnemyRate);
      }

         enemyLeft = enemyCount + tankEnemyCount + fastEnemyCount;
      enemyCount = enemyLeft;
      waveset = new List<GameObject>();

      for (int i = 0; i < enemyCount; i++)
      {
         waveset.Add(enemy);
      }
      for (int i = 0; i < fastEnemyCount; i++)
      {
         waveset.Add(fastEnemy);
      }
      for (int i = 0; i < tankEnemyCount; i++)
      {
         waveset.Add(tankEnemy);
      }

      waveset = Shuffle(waveset);

      StartCoroutine(spawn());
   }
   public List<GameObject> Shuffle(List<GameObject> waveSet)
   {
      List<GameObject> temp = new List<GameObject>();
      List<GameObject> result = new List<GameObject>();

      temp.AddRange(waveSet);

      for (int i = 0; i < waveSet.Count; i++)
      {
         int index = Random.Range(0, temp.Count - 1);

         result.Add(temp[index]);
         temp.RemoveAt(index);
      }

    return result;
}
   
   IEnumerator spawn()
   {
      for (int i = 0; i < waveset.Count; i++)
      {
         Instantiate(waveset[i], spampoint.position, Quaternion.identity);
         yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));
      }
      wavedone = true;
   }
}
