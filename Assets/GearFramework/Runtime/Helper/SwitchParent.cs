using UnityEngine;

namespace GearFramework.Runtime
{
    public class SwitchParent
    {
        Transform transform;
        Transform newParentTransform;
        Transform originalParentTransform;
        
        public SwitchParent(Transform transform, Transform newParentTransform)
        {
            this.transform = transform;
            this.newParentTransform = newParentTransform;
            originalParentTransform = transform.parent;
        }
        
        public void SetNewParent() => transform.SetParent(newParentTransform);
        
        public void SetOriginalParent() => transform.SetParent(originalParentTransform);
    }
}
