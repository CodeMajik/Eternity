using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;
using System.Xml.Serialization;

namespace Eternity
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    
    public class Entity
    {
        [XmlIgnore]
        public Vector2 m_angular_velocity;
        [XmlIgnore]
        public Vector2 m_angular_momentum;
        [XmlIgnore]
        public Vector2 m_orientation;
        [XmlIgnore]
        public Vector2 m_impulseForce;
        [XmlIgnore]
        public Vector2 m_force;
        [XmlIgnore]
        public Vector2 m_zoneForces;
        [XmlIgnore]
        public List<Vector2> m_vertexPoints;
        [XmlIgnore]
        public NodeGrid m_nG;
        [XmlIgnore]
        public long m_id;

        public Vector2 m_position;
        public Vector2 m_velocity;
        public Texture2D m_texture;
        public bool m_bPlayerControlled;
        public double m_width, m_height, m_mass, m_coefRestitution, m_coefFriction;

        public Entity()
        {
            DefaultInitialize();
        }

        public Entity(Vector2 pos, Vector2 vel)
        {
            m_bPlayerControlled = false;
            m_position= pos;
            m_velocity = vel;
            m_angular_velocity = Vector2.Zero;
            m_angular_momentum = Vector2.Zero;
            m_orientation = Vector2.Zero;
            m_force = Vector2.Zero;
            m_zoneForces = Vector2.Zero;
            m_vertexPoints = new List<Vector2>(0);
            m_width = 0;
            m_coefRestitution = 0.8;//1.0 for full bounce
            m_coefFriction = 1.0;//lower for higher froction
            m_height = 0;
            Random rand = new Random();
            m_mass = rand.Next(60, 100);
            m_texture = null;
            m_id = IDManager.GenerateNewID();
            m_nG = new NodeGrid(3, 3);
        }

        public void LoadTexture(ref ContentManager content)
        {
            m_texture = content.Load<Texture2D>("cube.png");
            m_width = m_texture.Width;
            m_height = m_texture.Height;
            GenerateVertexes();
        }

        public void SetTexture(ref Texture2D tex)
        {
            m_texture = tex;
            m_width = tex.Width;
            m_height = tex.Height;
        }

        public void DefaultInitialize()
        {
            m_bPlayerControlled = false;
            m_texture = null;
            m_position = Vector2.Zero;
            m_velocity = Vector2.Zero;
            m_angular_velocity = Vector2.Zero;
            m_angular_momentum = Vector2.Zero;
            m_orientation = Vector2.Zero;
            m_force = Vector2.Zero;
            m_zoneForces = Vector2.Zero;
            m_width = 0;
            m_coefRestitution = 0.8;
            m_coefFriction = 1.0;
            m_height = 0;
            Random rand = new Random();
            m_mass = rand.Next(60, 100);
            m_vertexPoints = new List<Vector2>(0);
            m_id = IDManager.GenerateNewID();
            m_nG = new NodeGrid(3, 3);
        }

        public void GenerateVertexes()
        {
            /*
             *  X-----X
             *  |     |
             *  |     |
             *  X-----X
             *  
             * */
            m_vertexPoints.Clear();
            m_vertexPoints.Add(new Vector2((float)GetLeftX(), (float)GetTopY()));
            m_vertexPoints.Add(new Vector2((float)GetRightX(), (float)GetTopY()));
            m_vertexPoints.Add(new Vector2((float)GetRightX(), (float)GetBotY()));
            m_vertexPoints.Add(new Vector2((float)GetLeftX(), (float)GetBotY()));
        }

        public void Update(ref GameTime gameTime)
        {
            
        }

        public void RunInput(KeyboardState k)
        {
            Vector2 nodePos = ToNodePosition();
            int xIndex = (int)nodePos.X;
            int yIndex = (int)nodePos.Y;

            float movementSpeed = 4.0f;

            //if (k.IsKeyDown(Keys.A))
            //{
            //    if (!(xIndex > 0 && mgr.m_nodesLayer2[xIndex - 1, yIndex].m_eCollisionFlag != TerrainNode.COLLISION_FLAG.CLEAR && ((m_position.X - movementSpeed) % 32.0f == 0)))
            //    {
            //        m_position.X -= movementSpeed;
            //    }
            //}
            //else if (k.IsKeyDown(Keys.D))
            //{
            //    if (!(xIndex < TerrainManager.max - 1 && mgr.m_nodesLayer2[xIndex + 1, yIndex].m_eCollisionFlag != TerrainNode.COLLISION_FLAG.CLEAR && ((m_position.X + movementSpeed) % 32.0f != 0)))
            //    {
            //        m_position.X += movementSpeed;
            //    }
            //}
            //else if (yIndex > 0 && k.IsKeyDown(Keys.W))
            //{
            //    if (!(mgr.m_nodesLayer2[xIndex, yIndex - 1].m_eCollisionFlag != TerrainNode.COLLISION_FLAG.CLEAR && ((m_position.Y - movementSpeed) % 32.0f == 0)))
            //        m_position.Y -= movementSpeed;
            //}
            //else if (yIndex < TerrainManager.max - 1 && k.IsKeyDown(Keys.S))
            //{
            //    if (!(mgr.m_nodesLayer2[xIndex, yIndex + 1].m_eCollisionFlag != TerrainNode.COLLISION_FLAG.CLEAR && (((m_position.Y + m_height) + movementSpeed) % 32.0f != 0)))
            //        m_position.Y += movementSpeed;
            //}
          
            TerrainManager mgr = TerrainManager.GetInstance();
            if (k.IsKeyDown(Keys.A))
            {
                if (xIndex>0&& mgr.m_nodesLayer2[xIndex - 1, yIndex].m_eCollisionFlag == TerrainNode.COLLISION_FLAG.CLEAR)
                {
                    m_position.X -= movementSpeed;
                }
            }
            else if (k.IsKeyDown(Keys.D))
            {
                if (xIndex < TerrainManager.max-1 && mgr.m_nodesLayer2[xIndex + 1, yIndex].m_eCollisionFlag == TerrainNode.COLLISION_FLAG.CLEAR)
                {
                    m_position.X += movementSpeed;
                }
            }
            else if (yIndex>0&& k.IsKeyDown(Keys.W))
            {
                if (mgr.m_nodesLayer2[xIndex, yIndex - 1].m_eCollisionFlag == TerrainNode.COLLISION_FLAG.CLEAR)
                {
                    m_position.Y -= movementSpeed;
                }
            }
            else if (yIndex < TerrainManager.max-1 && k.IsKeyDown(Keys.S))
            {
                if (mgr.m_nodesLayer2[xIndex, yIndex + 1].m_eCollisionFlag == TerrainNode.COLLISION_FLAG.CLEAR)
                {
                    m_position.Y += movementSpeed;
                }
            }
        }

        public double GetWCSRightX()
        {
            return m_position.X + m_width;
        }

        public double GetRightX()
        {
            return m_width;
        }

        public double GetWCSLeftX()
        {
            return m_position.X;
        }

        public double GetLeftX()
        {
            return 0.0;
        }

        public double GetCenterX()
        {
            return m_width/2.0;
        }

        public double GetCenterY()
        {
            return m_height / 2.0;
        }

        public double GetWCSCenterX()
        {
            return m_position.X + m_width / 2.0;
        }

        public double GetWCSCenterY()
        {
            return m_position.Y + m_height / 2.0;
        }

        public double GetTopY()
        {
            return 0.0;
        }

        public double GetBotY()
        {
            return m_height;
        }

        public double GetWCSTopY()
        {
            return m_position.Y;
        }

        public double GetWCSBotY()
        {
            return m_position.Y+m_height;
        }

        public Vector2 GetCenter()
        {
            return new Vector2((float)m_width / 2.0f, (float)m_height / 2.0f);
        }

        public Vector2 GetWCSCenter()
        {
            return new Vector2(m_position.X + (float)(m_width / 2.0f), m_position.Y + (float)(m_height / 2.0f));
        }

        public Vector2 ToNodePosition()
        {
            return new Vector2((m_position.X + (float)(m_width / 2.0f)) / 32.0f, (m_position.Y + (float)(m_height / 2.0f)) / 32.0f);
        }

        public void Draw(ref SpriteBatch sb)
        {
            sb.Draw(m_texture, new Rectangle((int)m_position.X, (int)m_position.Y, (int)m_width, (int)m_height), null, Color.White, 0.0f, GetCenter(), SpriteEffects.None, 0.0f);
        }
    }
}
