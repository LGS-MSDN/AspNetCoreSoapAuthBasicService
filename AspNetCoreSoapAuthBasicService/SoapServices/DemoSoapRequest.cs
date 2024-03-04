using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace AspNetCoreSoapAuthBasicService.SoapServices
{
    [DataContract]
    public class DemoSoapRequest
    {
        [DataMember]
        public string? ChaineIntegRetourDEG { get; set; }

        [DataMember]
        public EnumCodRaisonAppel? CodRaisonAppel { get; set; }

        [DataMember]
        public DateTime? DateDebutChargeRole { get; set; }

        [DataMember]
        public short? NoSecteurActivite { get; set; }

    }

    public enum EnumCodRaisonAppel
    {
        [EnumMember]
        InitierEtape,

        [EnumMember]
        ExecuterEtape,

        [EnumMember]
        SignalerErreur,

        [EnumMember]
        SignalerAnnulation
    }

}
