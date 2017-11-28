using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PhoneService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWindowsPhoneService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        int NovoPercurso(int idUser,string isMetrico);

        [OperationContract]
        void AtualizaPercurso(int id, int idUser, string descr, DateTime data, double velocidadeMedia, double velocidadeMaxima, double distancia, double ritimo, double altitudeMaxima, double altitudeMinima, double altitudeVariacao, double calorias, string duracao);

        [OperationContract]
        void AddPontos(int id, DateTime data, double velocidade, double distancia, double ritimo, double altitudeMaxima, double altitudeMinima, double altitudeVariacao, double calorias, int duracao, string longitude,string latitude, string isMetrico);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
