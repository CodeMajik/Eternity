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
    public class Loader
    {
        public Loader()
        {
        }

        public void LoadGame()
        {
            Entity data;

            // Get the path of the save game
            string fullpath = Path.Combine("savegame.sav");

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(Entity));
                data = (Entity)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }

        public void LoadTerrain()
        {
            TerrainManager mgr = TerrainManager.GetInstance();
            List<TerrainNode> nodes = new List<TerrainNode>(0);
            // Get the path of the save game
            string fullpath = Path.Combine("terrainSave.sav");

            // Open the file
            FileStream stream = File.Open(fullpath, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                // Read the data from the file
                XmlSerializer serializer = new XmlSerializer(typeof(List<TerrainNode>));
                nodes = (List<TerrainNode>)serializer.Deserialize(stream);
            }
            finally
            {
                // Close the file
                stream.Close();
            }
        }
    }
}
