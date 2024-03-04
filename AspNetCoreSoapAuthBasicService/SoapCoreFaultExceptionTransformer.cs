using AspNetCoreSoapAuthBasicService.SoapServices;
using SoapCore.Extensibility;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.Xml;

namespace AspNetCoreSoapAuthBasicService
{
    public class SoapCoreFaultExceptionTransformer : IFaultExceptionTransformer
    {
        public Message ProvideFault(
            Exception exception, 
            MessageVersion messageVersion, 
            Message requestMessage, 
            XmlNamespaceManager xmlNamespaceManager)
        {
            var erreur = new DemoSoapReponse
            {
              CodRetour = EnumCodRetour.ErreurTechnique,
              CodErreur = "HTTP 500",
              MessageErreur = exception.Message
            };

            var message = Message.CreateMessage(messageVersion, null, erreur);
 
            return message;
        }
    }

}
