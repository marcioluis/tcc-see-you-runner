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
using System.Collections.ObjectModel;

namespace PhoneNegocio
{
    public class PercursoNegocio
    {
        private const string prePercuso = "PrePercurso.xml";
        private const string ListaDePercuros = "ListaDePercuros.xml";


        public void SetPrePercurso(Percurso percurso)
        {
            // Write to the Isolated Storage
            try
            {
                XmlWriterSettings x_W_Settings = new XmlWriterSettings();
                x_W_Settings.Indent = true;
                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = ISF.OpenFile(prePercuso, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Percurso));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, x_W_Settings))
                        {
                            serializer.Serialize(xmlWriter, percurso);
                            //MessageBox.Show("Data Save!!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Percurso GetPrePercurso()
        {
            Percurso percurso = new Percurso();
            try
            {

                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream str = ISF.OpenFile(prePercuso, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Percurso));
                        Percurso per = (Percurso)serializer.Deserialize(str);
                        percurso = per;
                    }
                }
            }
            catch (Exception ex)
            {
                //config = ConfiguracaoPadrao();
            }
            return percurso;
        }

        public void AddToListaPercurso(Percurso percurso)
        {
            ObservableCollection<Percurso> percursos = new ObservableCollection<Percurso>();
            percursos = GetListaPercursos();
            percursos.Add(percurso);
            try
            {
                XmlWriterSettings x_W_Settings = new XmlWriterSettings();
                x_W_Settings.Indent = true;
                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = ISF.OpenFile(ListaDePercuros, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Percurso>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, x_W_Settings))
                        {
                            serializer.Serialize(xmlWriter, percursos);
                            //MessageBox.Show("Data Save!!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveFromListaPercurso(Percurso percurso)
        {
            ObservableCollection<Percurso> percursos = new ObservableCollection<Percurso>();
            percursos = GetListaPercursos();
            for (int i = 0; i < percursos.Count - 1; i++)
            {
                if (percurso.Id == percursos[i].Id)
                {
                    percursos.RemoveAt(i);
                    break;
                }
            }
            //percursos.Remove(percurso);
            try
            {
                XmlWriterSettings x_W_Settings = new XmlWriterSettings();
                x_W_Settings.Indent = true;
                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = ISF.OpenFile(ListaDePercuros, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Percurso>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, x_W_Settings))
                        {
                            serializer.Serialize(xmlWriter, percursos);
                            //MessageBox.Show("Data Save!!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void UpdatePercursoToListaPercurso(Percurso percurso)
        {
            ObservableCollection<Percurso> percursos = new ObservableCollection<Percurso>();
            percursos = GetListaPercursos();
            foreach (Percurso per in percursos)
            {
                if (per.Id == percurso.Id)
                {
                    per.Descricao = percurso.Descricao;
                    break;
                }
            }
            try
            {
                XmlWriterSettings x_W_Settings = new XmlWriterSettings();
                x_W_Settings.Indent = true;
                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = ISF.OpenFile(ListaDePercuros, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Percurso>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, x_W_Settings))
                        {
                            serializer.Serialize(xmlWriter, percursos);
                            //MessageBox.Show("Data Save!!!!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public ObservableCollection<Percurso> GetListaPercursos()
        {
            ObservableCollection<Percurso> percursos = new ObservableCollection<Percurso>();
            try
            {

                using (IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream str = ISF.OpenFile(ListaDePercuros, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Percurso>));
                        ObservableCollection<Percurso> pers = (ObservableCollection<Percurso>)serializer.Deserialize(str);
                        percursos = pers;
                    }
                }
            }
            catch (Exception ex)
            {
                return percursos;
            }
            return percursos;
        }

    }
}
