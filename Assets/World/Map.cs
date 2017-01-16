using UnityEngine;

namespace Mayhem.World
{
    public class Map : MonoBehaviour
    {
        public Vector2 Size;
        public GameObject MapTilePrefab;
        public Rect Bounds
        {
            get
            {
                return m_Bounds;
            }
        }

        public Vector2 TileSize
        {
            get
            {
                return m_TileSize;
            }
        }


        private Vector2 m_TileSize;
        private Rect m_Bounds;

        // Use this for initialization
        void Start()
        {
            RectTransform tileRect = MapTilePrefab.GetComponent<RectTransform>();
            m_TileSize = new Vector2(tileRect.rect.width, tileRect.rect.height);

            float tileXCount = Size.x / m_TileSize.x;
            float tileYCount = Size.y / m_TileSize.y;

            m_Bounds = new Rect();

            m_Bounds.min = new Vector2(-(m_TileSize.x / 2.0f), -(m_TileSize.y / 2.0f));
            m_Bounds.max = new Vector2(Size.x, Size.y - (m_TileSize.y / 2.0f));

            for (float x = 0; x < tileXCount; x++)
            {
                for (float y = 0; y < tileYCount; y++)
                {
                    Vector3 pos = new Vector3((x * m_TileSize.x) + (m_TileSize.x / 2), (y * m_TileSize.y) + (m_TileSize.y / 2), 0);

                    var obj = (GameObject)Instantiate(MapTilePrefab, pos, Quaternion.identity);
                    obj.name = "MapTile(" + pos.ToString() + ")";
                    obj.transform.SetParent(this.transform);

                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}