using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Eternity
{
    public class TerrainManager
    {
        [XmlIgnore]
        public const int max = 100;
        [XmlIgnore]
        public static TerrainManager m_instance = null;
        [XmlIgnore]
        public TerrainNode[,] m_nodesLayer1;
        [XmlIgnore]
        public TerrainNode[,] m_nodesLayer2;
        [XmlIgnore]
        public TerrainNode[,] m_nodesLayer3;
        [XmlIgnore]
        public List<Tuple<String, Texture2D>> m_textures;
       
        public static TerrainManager GetInstance()
        {
            if (m_instance==null)
                m_instance = new TerrainManager();
            return m_instance;
        }

        private TerrainManager()
        {
            m_nodesLayer1 = new TerrainNode[max, max];
            m_nodesLayer2 = new TerrainNode[max, max];
            m_nodesLayer3 = new TerrainNode[max, max];
            m_textures = new List<Tuple<String, Texture2D>>(0);
        }

        private TerrainManager(ref TerrainManager other)
        {
            m_nodesLayer1 = other.m_nodesLayer1;
            m_nodesLayer2 = other.m_nodesLayer2;
            m_nodesLayer3 = other.m_nodesLayer3;
            m_textures = other.m_textures;
        }

        public List<TerrainNode> XmlNodes1
        {
            get {
                return m_nodesLayer1.OfType<TerrainNode>().ToList();
            }
            set { 
                int size = (int)Math.Sqrt(value.Count);
                for(int i=0;i<size;++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        m_nodesLayer1[i, j] = value.ElementAt((i*size)+j);
                    }
                }
            }
        }

        public List<TerrainNode> XmlNodes2
        {
            get
            {
                return m_nodesLayer2.OfType<TerrainNode>().ToList();
            }
            set
            {
                int size = (int)Math.Sqrt(value.Count);
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        m_nodesLayer2[i, j] = value.ElementAt((i * size) + j);
                    }
                }
            }
        }

        public List<TerrainNode> XmlNodes3
        {
            get
            {
                return m_nodesLayer3.OfType<TerrainNode>().ToList();
            }
            set
            {
                int size = (int)Math.Sqrt(value.Count);
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        m_nodesLayer3[i, j] = value.ElementAt((i * size) + j);
                    }
                }
            }
        }

        public void AddTexture(ref Texture2D texture, String desc)
        {
            m_textures.Add(new Tuple<String, Texture2D>(desc, texture));
        }

        public void Init()
        {
            Vector2 pos = new Vector2(0, 0);
            for (int i = 0; i < max; ++i)
            {
                for (int j = 0; j < max; ++j)
                {
                    m_nodesLayer1[i, j] = new TerrainNode(pos, "Grass", 0, TerrainNode.COLLISION_FLAG.CLEAR, 1, 32.0f, 32.0f);
                    if((i>max/3&&i<max-(max/3))&&(j>max/3&&j<(max-(max/3))))
                        m_nodesLayer2[i, j] = new TerrainNode(pos, "Empty", 1, TerrainNode.COLLISION_FLAG.CLEAR, 2, 32.0f, 32.0f);
                    else if (i % (j + 1) < 5 || j % (i + 1) <4)
                        m_nodesLayer2[i, j] = new TerrainNode(pos, "Empty", 1, TerrainNode.COLLISION_FLAG.CLEAR, 2, 32.0f, 32.0f);
                    else
                        m_nodesLayer2[i, j] = new TerrainNode(pos, "Tree", 1, TerrainNode.COLLISION_FLAG.SOLID, 2, 32.0f, 32.0f);
                    m_nodesLayer3[i, j] = new TerrainNode(pos, "Empty", 0, TerrainNode.COLLISION_FLAG.CLEAR, 3, 32.0f, 32.0f);
                    pos.X += 32.0f;
                }
                pos.Y += 32.0f;
                pos.X = 0.0f;
            }
        }

        public void DrawLayeredTile(int i, int j, ref SpriteBatch sb)
        {
            Rectangle rect = new Rectangle();
            Texture2D tex = GetTextureById(ref m_nodesLayer1[i, j].m_textureID);
           
            if (tex != null)
            {
                rect.Width = tex.Width;
                rect.Height = tex.Height;
                rect.X = i * rect.Width;
                rect.Y = j * rect.Height;
                sb.Draw(tex, rect, null, Color.White, 0.0f, m_nodesLayer1[i, j].GetCenter(), SpriteEffects.None, 1.0f);
            } 
           
          
            tex = GetTextureById(ref m_nodesLayer2[i, j].m_textureID);
            if (tex != null)
            {
                rect.Width = tex.Width;
                rect.Height = tex.Height;
                rect.X = i * rect.Width;
                rect.Y = j * rect.Height;
                sb.Draw(tex, rect, null, Color.White, 0.0f, m_nodesLayer2[i, j].GetCenter(), SpriteEffects.None, 0.6f);
            }
            

            tex = GetTextureById(ref m_nodesLayer3[i, j].m_textureID);
            if (tex != null)
            {
                rect.Width = tex.Width;
                rect.Height = tex.Height;
                rect.X = i * rect.Width;
                rect.Y = j * rect.Height;
                sb.Draw(tex, rect, null, Color.White, 0.0f, m_nodesLayer3[i, j].GetCenter(), SpriteEffects.None, 0.1f);
            }
        }

        public void Draw(ref SpriteBatch sb)
        {
            for (int i = 0; i < max; ++i)
            {
                for (int j = 0; j < max; ++j)
                {
                    //DrawLayeredTile(i, j, ref sb);
                   
                        rect.Width = tex.Width;
                        rect.Height = tex.Height;
                        rect.X = i * rect.Width;
                        rect.Y = j * rect.Height;
                        sb.Draw(tex, rect, null, Color.White, 0.0f, m_nodesLayer1[i, j].GetCenter(), SpriteEffects.None, 1.0f);
                }
            }
        }

        Texture2D GetTextureById(ref String id)
        {
            for (int i = 0; i < m_textures.Count; ++i)
            {
                if (id == m_textures.ElementAt(i).Item1)
                {
                    return m_textures.ElementAt(i).Item2;
                }
            }
            return null;
        }

        public Vector2 NodeToScreenPosition(ref TerrainNode t)
        {
            return new Vector2(t.m_position.X * 32.0f, t.m_position.Y * 32.0f);
        }

        public Vector2 EntityToNodePosition(ref Entity t)
        {
            return new Vector2(t.m_position.X / 32.0f, t.m_position.Y / 32.0f);
        }

        public Tuple<int, int> ScreenToNodePosition(int x, int y)
        {
            return new Tuple<int, int>((int)(x / 32.0f), (int)(y / 32.0f));
        }
    }
}
