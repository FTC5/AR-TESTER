using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

namespace Assets.Scripts
{
    public class XRInteractionManagerBlockUI : XRInteractionManager
    {
        public delegate bool OnBlock();
        private List<OnBlock> _blockHandlers = new List<OnBlock>();
        
        protected override void ProcessInteractors(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !shouldBlock())
            {
                base.ProcessInteractors(updatePhase);
            }
        }

        public void AddOnBlock(OnBlock onBlock)
        {
            if(onBlock != null)
            {
                _blockHandlers.Add(onBlock);
            }
        }

        public void RemoveOnBlock(OnBlock onBlock)
        {
            if (onBlock != null)
            {
                _blockHandlers.Remove(onBlock);
            }
        }

        private bool shouldBlock()
        {
            return _blockHandlers.Any(handler => handler());
        }
    }
}
