using AspAdventureLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

namespace Elaborato
{
    public class XMLManager
    {
        private string _directory;
        private HttpServerUtility _httpServerUtility;
        public XMLManager(HttpServerUtility httpServerUtility)
        {
            _directory = @"Game.xml";
            _httpServerUtility = httpServerUtility;
        }
        public XMLManager(string directory, HttpServerUtility httpServerUtility)
        {
            _httpServerUtility = httpServerUtility;
            _directory = directory;
        }
        public void Encode(Game game)
        {
            using (FileStream saveStream =
                    new FileStream(_directory,
                                    FileMode.Create,
                                    FileAccess.Write,
                                    FileShare.None))
            {
                // Grazie a Indent va anche a capo con i tag.
                XmlWriterSettings xws = new XmlWriterSettings()
                {
                    Indent = true
                };

                using (XmlWriter xmlWriter =
                        XmlWriter.Create(saveStream, xws))
                {
                    DataContractSerializer dcSerializer = new DataContractSerializer(typeof(Game));
                    dcSerializer.WriteObject(xmlWriter, game);
                }
            }
        }

        public Game Decode(string directoryFile)
        {
            Game gioco;
            using (FileStream openStream =
                    new FileStream(directoryFile,
                                    FileMode.Open,
                                    FileAccess.Read,
                                    FileShare.None))
            using (XmlReader xmlReader =
                    XmlReader.Create(openStream))
            {
                DataContractSerializer dcSerializer = new
                    DataContractSerializer(typeof(Game));
                gioco =
                    (Game)dcSerializer.ReadObject(
                    xmlReader);
            }

            return gioco;
        }
    }
}