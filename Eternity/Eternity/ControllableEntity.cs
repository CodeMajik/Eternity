using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Eternity
{
    class ControllableEntity
    {
        public Entity m_entity;
        public ControllableEntity()
        {
            m_entity = new Entity(new Vector2(100.0f, 500.0f), new Vector2(0.0f, 0.0f));
            m_entity.m_bPlayerControlled = true;
            m_entity.m_coefRestitution = 0.75;
        }

        public void Update(GameTime time)
        {
            m_entity.Update(ref time);
        }
    }
}
