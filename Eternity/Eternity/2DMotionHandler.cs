using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Eternity
{
    public class _2DMotionHandler
    {
        public static float delta = 0.00000001f;
        public static _2DMotionHandler m_instance=null;

        public static _2DMotionHandler GetInstance()
        {
            if (m_instance == null)
                m_instance = new _2DMotionHandler();
            return m_instance;
        }

    }
}
