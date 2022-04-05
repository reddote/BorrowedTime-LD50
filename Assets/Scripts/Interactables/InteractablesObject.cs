using UnityEngine;

namespace Interactables{
    public class InteractablesObject : MonoBehaviour{
        public delegate void InteractionEvent(InteractablesObject sender);
        public event InteractionEvent OnMouseClick;
        public event InteractionEvent OnMouseHover;
        public event InteractionEvent OnMouseHoverOver;

        protected virtual void OnMouseDown(){
            OnMouseClick?.Invoke(this);
        }

        protected virtual void OnMouseOver(){
            OnMouseHover?.Invoke(this);
        }
        private void OnMouseExit()
        {
            OnMouseHoverOver?.Invoke(this);
        }
    }
}
