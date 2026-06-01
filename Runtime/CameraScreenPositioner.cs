using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unitilities
{
    public class CameraScreenPositioner : MonoBehaviour
    {
        [Header("Camera Position Entries")]
        public CameraScreenPositionEntry[] entries;

        void Awake()
        {
            ApplyCameraScreenPositions();
        }

        public void ApplyCameraScreenPositions(CameraScreenPositionEntry[] entries)
        {
            this.entries = entries;
            ApplyCameraScreenPositions();
        }

        public void ApplyCameraScreenPositions()
        {
            for (var i = 0; i < entries.Length; ++i)
            {
                if (entries[i].camera == null)
                {
                    continue;
                }
                SetCameraScreenPosition(entries[i].camera, entries[i].position);
            }
        }

        public void SetCameraScreenPosition(Camera camera, CameraScreenPosition position)
        {
            camera.rect = viewports[position];
        }

        static readonly Dictionary<CameraScreenPosition, Rect> viewports = new()
        {
            { CameraScreenPosition.Fullscreen, new(0f, 0f, 1f, 1f) },
            { CameraScreenPosition.Top, new(0f, 0.5f, 1f, 0.5f) },
            { CameraScreenPosition.Bottom, new(0f, 0f, 1f, 0.5f) },
            { CameraScreenPosition.Left, new(0f, 0f, 0.5f, 1f) },
            { CameraScreenPosition.Right, new(0.5f, 0f, 0.5f, 1f) },
            { CameraScreenPosition.TopLeft, new(0f, 0.5f, 0.5f, 0.5f) },
            { CameraScreenPosition.TopRight, new(0.5f, 0.5f, 0.5f, 0.5f) },
            { CameraScreenPosition.BottomLeft, new(0f, 0f, 0.5f, 0.5f) },
            { CameraScreenPosition.BottomRight, new(0.5f, 0f, 0.5f, 0.5f) },
        };
    }

    [Serializable]
    public class CameraScreenPositionEntry
    {
        public Camera camera;
        public CameraScreenPosition position;
    }

    [Serializable]
    public enum CameraScreenPosition
    {
        Fullscreen,
        Top,
        Bottom,
        Left,
        Right,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight,
    }
}