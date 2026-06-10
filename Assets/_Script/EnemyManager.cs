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

   [SerializeField] private int baseEnemyCount = 6;

   [SerializeField] private float enemyRate = 0.5f;
   [SerializeField] private float fastEnemyRate = 0.4f;
   [SerializeField] private float tankEnemyRate = 0.1f;

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
      Setwave();
   }

   void Update()
   {

   }

   private void Setwave()
   {
      enemyCount = Mathf.RoundToInt(baseEnemyCount * enemyRate);
      fastEnemyCount = Mathf.RoundToInt(baseEnemyCount * fastEnemyRate);
      tankEnemyCount = Mathf.RoundToInt(baseEnemyCount * tankEnemyRate);

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

      StartCoroutine(spawn());
   }
   
   IEnumerator spawn()
   {
      for (int i=0;i < waveset.Count;i++)
      {
         Instantiate(waveset[i], spampoint.position, Quaternion.identity);
         yield return new WaitForSeconds(0.5f);
      }
   }
}
