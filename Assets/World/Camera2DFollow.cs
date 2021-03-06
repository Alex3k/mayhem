﻿using UnityEngine;

namespace Mayhem.World
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;

        public Map m_WorldMap;

        private float rightBound;
        private float leftBound;
        private float topBound;
        private float bottomBound;
        private Vector3 pos;
        private Vector2 spriteBounds;

        void Start()
        {
            m_WorldMap = GameObject.Find("Map").GetComponent<Map>();
        }

        // Update is called once per frame
        void Update()
        {
            Camera cam = GetComponent<Camera>();

            float camVertExtent = cam.orthographicSize;
            float camHorzExtent = cam.aspect * camVertExtent;

            float leftBound = m_WorldMap.Bounds.min.x + camHorzExtent + (m_WorldMap.TileSize.x / 2);
            float rightBound = m_WorldMap.Bounds.max.x - camHorzExtent + (m_WorldMap.TileSize.y / 2);
            float bottomBound = m_WorldMap.Bounds.min.y + camVertExtent + (m_WorldMap.TileSize.y / 2);
            float topBound = m_WorldMap.Bounds.max.y - camVertExtent + (m_WorldMap.TileSize.y / 2);

            float camX = Mathf.Clamp(target.transform.position.x, leftBound, rightBound);
            float camY = Mathf.Clamp(target.transform.position.y, bottomBound, topBound);

            cam.transform.position = new Vector3(camX, camY, cam.transform.position.z);


        }
    }
}