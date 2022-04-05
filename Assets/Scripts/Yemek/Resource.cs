using System;
using UnityEngine;

namespace Yemek{
    public class Resource : MonoBehaviour{
        [SerializeField]
        protected int resourceCount;
        public delegate void ResourceSpendEvent(Resource self, int change, int oldCount, int newCount);
        public event ResourceSpendEvent OnResourceChange;
        public event Action<int, int, int> spentTrigger;

        public virtual void ResourcesChanger(int changeNumber){
            resourceCount += changeNumber;
            spentTrigger?.Invoke(changeNumber, resourceCount - changeNumber, resourceCount );
            OnResourceChange?.Invoke(this, changeNumber, resourceCount - changeNumber, resourceCount);
        }

        public int GetResourceCount(){
            return resourceCount;
        }
        
        public virtual void ResourceSpent(int spentResources){
            //if (resourceCount <= 0){
            //    resourceCount = 0;
            //} else{
                ResourcesChanger(spentResources);

                //if (resourceCount < spentResources){
                //    ResourcesChanger(resourceCount);
                //} else{
                //    ResourcesChanger(spentResources);
                //}
            //}
        }


        //[SerializeField]
        //float myCurrentValue = 250f;
        //[SerializeField]
        //float myMaxValue = 500f;
        //[Button]
        //public void Test()
        //{
        //    myCurrentValue += ResourceSpent(1, myCurrentValue, 10f);
        //}
    }
}