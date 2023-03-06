using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Handles the saving and loading of xml files
    /// T can be any object type as long as it is serializable
    /// For more info : https://softwarebydefault.com/2013/02/04/xml-serialization-generics/,  https://stackoverflow.com/a/37178751
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class XmlManager<T>
    {
        /// <summary>
        /// Saves the object of type T in the path. Directory/File will be created if they don't exist.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void Save(T obj, string directoryPath, string fileName)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                FileStream stream;

                stream = new FileStream(directoryPath + fileName, FileMode.Create);

                xmlSerializer.Serialize(stream, obj);
                stream.Close();
            }
        }

        /// <summary>
        /// Returns the object of type T by reading the xml file in the path
        /// Returns defaultT if there is an error in loading the xml file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Load(string path, T defaultT)
        {
            T result = defaultT;

            if (!string.IsNullOrEmpty(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                XmlReader reader = XmlReader.Create(stream);
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                if (serializer.CanDeserialize(reader))
                {
                    //If the user edits the .xml file and enters any invalid data
                    //For example : a alphabet where a numeric value was expected
                    //Then the defaulT value will be returned and for all the settings the fallback option will be used.
                    try
                    {
                        result = (T)serializer.Deserialize(reader);
                    }catch
                    {
                        result = defaultT;
                        Debug.LogError("File in path : " + path + ", contains invalid data");
                    }
                }

                stream.Close();
            }

            return result;
        }

        /// <summary>
        /// Check if file exists in the specified path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool FileExists(string path)
        {
            if(File.Exists(path))
            {
                return true;
            }

            return false;
        }
    }
}


