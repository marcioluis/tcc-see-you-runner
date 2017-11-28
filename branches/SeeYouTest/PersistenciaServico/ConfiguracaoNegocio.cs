using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Model;
using System.Xml;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.IO;

namespace PhoneNegocio
{
    public class ConfiguracaoNegocio
    {
        public bool NovaConfiguracao(string sexo, int idade, int peso, string smetrico)
        {
            try
            {
                Configuracao conf = new Configuracao();
                conf.Sexo = sexo;
                conf.Idade = idade;
                conf.Peso = peso;
                conf.SistemaMetrico = smetrico;
                SalvarConfigurao(conf);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SalvarConfigurao(Configuracao config)
        {
            // Write to the Isolated Storage
            try
            {
                XmlWriterSettings x_W_Settings = new XmlWriterSettings();
                x_W_Settings.Indent = true;
                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = ISF.OpenFile("Configuracao.xml", FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Configuracao));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, x_W_Settings))
                        {
                            serializer.Serialize(xmlWriter, config);
                            //MessageBox.Show("Data Save!!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Configuracao CarregaConfiguracao()
        {
            Configuracao config = new Configuracao();
            try
            {

                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream str = ISF.OpenFile("Configuracao.xml", FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Configuracao));
                        Configuracao conf = (Configuracao)serializer.Deserialize(str);
                        config = conf;
                    }
                }
            }
            catch (Exception ex)
            {
                config = ConfiguracaoPadrao();
            }
            return config;
        }

        private Configuracao ConfiguracaoPadrao()
        {
            Configuracao config = new Configuracao();
            config.Sexo = "Masculino";
            config.Idade = 22;
            config.Peso = 70;
            config.SistemaMetrico = "Métrico";
            return config;
        }

        public bool ConfIsMetrico()
        {
            if (CarregaConfiguracao().SistemaMetrico == "Métrico")
                return true;
            else
                return false;
        }

        public int ConfIdade()
        {
            return CarregaConfiguracao().Idade;
        }

        public int ConfiPeso()
        {
            return CarregaConfiguracao().Peso;
        }

        public bool ConfIsMasculino()
        {
            if (CarregaConfiguracao().Sexo == "Masculino")
                return true;
            else
                return false;
        }
    }
}
