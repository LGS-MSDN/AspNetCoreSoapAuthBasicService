using System.Runtime.Serialization;

namespace AspNetCoreSoapAuthBasicService.SoapServices
{
    [DataContract]
    public class DemoSoapReponse
    {
        [DataMember]
        public string? CodErreur { get; set; }

        [DataMember]
        public EnumCodRetour? CodRetour { get; set; }

        [DataMember]
        public string? MessageErreur { get; set; }
    }

    public enum EnumCodRetour
    {
        [EnumMember]
        Succes,

        [EnumMember]
        ErreurFonctionnelle,

        [EnumMember]
        ErreurTechnique,
    }
}
