using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Eternity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Storage;

namespace LevelEditor
{
    public class Saver
    {

        public Saver()
        {
        }

        public void SaveGameToFile()
        {
            // Get the path of the save game
            string fullpath = Path.Combine("savegame.sav");

            // Open the file, creating it if necessary
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            try
            {
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(Entity));
                Entity e = new Entity(new Vector2(200.0f, 100.0f), new Vector2(1.4f, 2.8f));
                serializer.Serialize(stream, e);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }

        public void SaveTerrainToFile()
        {
            TerrainManager mgr = TerrainManager.GetInstance();

            // Get the path of the save game
            string fullpath = Path.Combine("terrainSave.sav");

            // Open the file, creating it if necessary
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate);
            try
            {
                List<TerrainNode> nodes = new List<TerrainNode>(0);
                for (int i = 0; i < mgr.m_nodesLayer1.GetLength(0); ++i)
                {
                    nodes.Add(mgr.m_nodesLayer1[i, 0]);
                }
                // Convert the object to XML data and put it in the stream
                XmlSerializer serializer = new XmlSerializer(typeof(List<TerrainNode>));
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                serializer.Serialize(stream, nodes);
               
                //foreach (TerrainNode node in mgr.m_nodesLayer1)
                //{
                //    serializer.Serialize(stream, node);
                //}
                //foreach (TerrainNode node in mgr.m_nodesLayer2)
                //{
                //    serializer.Serialize(stream, node);
                //}
                //foreach (TerrainNode node in mgr.m_nodesLayer3)
                //{
                //    serializer.Serialize(stream, node);
                //}
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }
    }
}
