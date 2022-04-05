using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace Yemek
{
    public class RandomResourceGenerator : MonoBehaviour
    {
        public static RandomResourceGenerator Instance;
        public Resource[] consumables;
        private void Awake()
        {
            Instance = this;
        }
        [SerializeField] private int min, max;

        [Button]
        public void GetResources()
        {
            var _m = max;
            var l = consumables.ToList();
            while (l.Count != 0)
            {
                var r = Random.Range(min, _m);
                var i = Random.Range(0, l.Count);
                Debug.Log(r);
                l[i].ResourcesChanger(r);
                l.RemoveAt(i);
            }

            //consumables.ResourcesChanger();
        }

    }
}

/*
 Zar OK mi?
    GetResource(any)
 
 
 
 */