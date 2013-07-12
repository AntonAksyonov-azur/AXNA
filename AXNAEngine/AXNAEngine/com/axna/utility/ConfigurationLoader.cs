using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AXNAEngine.com.axna.utility
{
    public class ConfigurationLoader
    {
        public static T LoadConfiguration<T>(String loadPath) where T : IConfigurationObject, new()
        {
            try
            {
                var serializer = new XmlSerializer(typeof (T));
                TextReader reader = new StreamReader(loadPath);
                T instance = (T)serializer.Deserialize(reader);
                reader.Close();

                return instance;
            }
            catch (IOException e)
            {
                MessageBox.Show("Error while loading configuration file! Using default values", "Error",
                                MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //T instance = default(T);
                T instance = new T();

                instance.InitDefault();
                return instance;
            }
        }

        public static void SaveConfiguration<T>(T configurationObject, String savePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof (T));
                TextWriter writer = new StreamWriter(savePath);
                serializer.Serialize(writer, configurationObject);
                writer.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show("Error in unloading configuration file!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public interface IConfigurationObject
    {
        void InitDefault();
    }
}