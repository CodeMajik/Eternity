using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Eternity
{
    public class EntityManager
    {
        public List<Entity> m_entities;
        public Texture2D m_defCubetexture;
        public double floorY, screenRight, screenLeft=0.0;
        public static EntityManager m_instance=null;

        public static EntityManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new EntityManager();
            }
            return m_instance;
        }

        public static EntityManager GetInitInstance(ref Texture2D texture)
        {
            if (m_instance == null)
            {
                m_instance = new EntityManager(ref texture);
            }
            return m_instance;
        }
        
        private EntityManager()
        {
            Initialize();
        }

        private EntityManager(ref Texture2D texture)
        {
            m_defCubetexture = texture;
            Initialize();
            
        }

        public void Populate()
        {
            Vector2 pos, vel;
            Random rand = new Random();
            double[] xArray = new double[10];
            double[] yArray = new double[10];

            for (int i = 0; i < 10; ++i)
            {
                xArray[i] = rand.Next((int)screenLeft + 100, (int)screenRight - 100);
                yArray[i] = rand.Next((int)0.0 + 100, (int)floorY - 100);
            }

            for (int i = 0; i < 10; ++i)
            {
                pos = new Vector2((float)xArray[i], (float)yArray[i]);
                vel = new Vector2(-4.5f, -2.0f);
                AddEntity(new Entity(pos, vel));
            }
        }

        public void Initialize()
        {
            m_entities = new List<Entity>(0);
        }

        public void AddEntity(Entity e)
        {
            e.SetTexture(ref m_defCubetexture);
            m_entities.Add(e);
        }

        public void Update(ref GameTime gameTime)
        {
            foreach(Entity entity in m_entities)
            {
                entity.Update(ref gameTime);
            }
        }

        public void Draw(ref SpriteBatch sb)
        {
            foreach (Entity entity in m_entities)
            {
                 sb.Draw(entity.m_texture, entity.GetWCSCenter(), null, Color.White, entity.m_angular_momentum.Length(),
                   new Vector2((float)entity.GetCenterX(), (float)entity.GetCenterY()), 1.0f, SpriteEffects.None, 0f);
            }
        }
    }
}
