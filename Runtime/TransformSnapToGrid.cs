using UnityEngine;

namespace Unitilities
{
    public class TransformSnapToGrid : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("Size of each grid cell.")]
        public float gridSize = 1f;

        [Tooltip("Snap in local or world space.")]
        public Space space = Space.World;
        public bool snapX = true;
        public bool snapY = true;
        public bool snapZ = true;

        [Header("State")]
        [Tooltip("Is snapping active?")]
        public bool snap;

        void LateUpdate()
        {
            if (!snap)
            {
                return;
            }
            SnapToGrid();
        }

        void SnapToGrid()
        {
            var position = space == Space.Self ? transform.localPosition : transform.position;
            var originalPosition = position;
            if (snapX)
            {
                position.x = SnapValue(position.x);
            }
            if (snapY)
            {
                position.y = SnapValue(position.y);
            }

            if (snapZ)
            {
                position.z = SnapValue(position.z);
            }

            if (position != originalPosition)
            {
                if (space == Space.Self)
                {
                    transform.localPosition = position;
                }
                else
                {
                    transform.position = position;
                }
            }
        }

        float SnapValue(float value)
        {
            return Mathf.Round(value / gridSize) * gridSize;
        }

        public void SetGridSize(float gridSize)
        {
            this.gridSize = gridSize;
        }

        public void SetSnapping(bool value)
        {
            snap = value;
        }
    }
}