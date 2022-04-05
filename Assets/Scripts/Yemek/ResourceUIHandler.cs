using TMPro;
using UnityEngine;

namespace Yemek{
    public class ResourceUIHandler: MonoBehaviour{
        [SerializeField] private TextMeshProUGUI foodText;
        [SerializeField] private TextMeshProUGUI woodText;

        [SerializeField] private Resource food;
        [SerializeField] private Resource wood;
    
        private void Start(){
            food.spentTrigger += FoodSpentTrigger;
            wood.spentTrigger += WoodSpentTrigger;
        }
        private void OnDestroy()
        {
            food.spentTrigger -= FoodSpentTrigger;
            wood.spentTrigger -= WoodSpentTrigger;
        }
        private void FoodSpentTrigger(int arg1, int arg2, int arg3){
            foodText.text = arg2.ToString();
        }
        private void WoodSpentTrigger(int arg1, int arg2, int arg3){
            woodText.text = arg2.ToString();
        }
    }
}
