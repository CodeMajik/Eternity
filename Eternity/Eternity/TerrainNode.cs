using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Eternity
{
    public class TerrainNode
    {
        public enum COLLISION_FLAG { SOLID=0, CLEAR=1 };
        
        public COLLISION_FLAG m_eCollisionFlag;
        public int m_currCollisionValue;
        public String m_textureID;
        public Vector2 m_position; 
        public int layerValue; 
        public float m_width, m_height;
        
        public TerrainNode()
        {

        }

        public TerrainNode(ref TerrainNode other)
        {
            m_eCollisionFlag = other.m_eCollisionFlag;
            m_currCollisionValue = other.m_currCollisionValue;
            m_textureID = other.m_textureID;
            m_position = other.m_position; 
            layerValue = other.layerValue;
            m_width = other.m_width;
            m_height = other.m_height;
        }

        public TerrainNode(Vector2 pos, String textureid, int collVal, COLLISION_FLAG flag, int layer)
        {
            m_eCollisionFlag = flag;
            m_currCollisionValue = collVal;
            m_textureID = textureid;
            m_position = pos;
            layerValue = layer;
        }

        public TerrainNode(Vector2 pos, String textureid, int collVal, COLLISION_FLAG flag, int layer, float width, float height)
        {
            m_eCollisionFlag = flag;
            m_currCollisionValue = collVal;
            m_textureID = textureid;
            m_position = pos;
            layerValue = layer;
            m_width = width;
            m_height = height;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(m_width/2, m_height/2);
        }

        public float GetWCSLeftX()
        {
            return m_position.X;
        }

        public float GetWCSRightX()
        {
            return m_position.X+m_width;
        }

        public float GetWCSTopY()
        {
            return m_position.Y+m_height;
        }

        public float GetWCSBotY()
        {
            return m_position.Y;
        }
    }
}
