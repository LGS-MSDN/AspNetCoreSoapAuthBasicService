using AspNetCoreSoapAuthBasicService.SoapServices;
using System.Xml.Linq;

namespace AspNetCoreSoapAuthBasicService.Middlewares
{
    public class CustomSoapMiddleware
    {

        private readonly RequestDelegate _next;

        public CustomSoapMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/BasicAuthDemoSoapService.asmx"))
            {
                // Intercepter la demande SOAP ici
                using (var reader = new StreamReader(context.Request.Body))
                {
                    string requestBody = await reader.ReadToEndAsync();
                    var soapRequest = DeserializeSoapRequest(requestBody);

                    // Utilisez soapRequest selon vos besoins

                    // Passez la demande au prochain middleware dans le pipeline
                    await _next(context);
                }
            }
            else
            {
                // Passez la demande au prochain middleware dans le pipeline sans la modifier
                await _next(context);
            }
        }

        private DemoSoapRequest DeserializeSoapRequest(string requestBody)
        {
            // Charger la demande SOAP en tant que XDocument
            XDocument doc = XDocument.Parse(requestBody);

            // Extraire les valeurs de la demande SOAP et effectuer la validation et la conversion manuellement
            //short yourShortValue;
            //if (short.TryParse(doc.Root.Element("YourShortField").Value, out yourShortValue))
            //{
            //    // Créer une instance de YourSoapRequest et assigner les valeurs
            //    var soapRequest = new DemoSoapRequest
            //    {
            //        YourShortField = yourShortValue,
            //        // Assigner d'autres valeurs selon les besoins
            //    };

            //    return soapRequest;
            //}
            //else
            //{
            //    // Gérer l'erreur de désérialisation ou de conversion
            //    // Vous pouvez lancer une exception ou renvoyer une réponse d'erreur appropriée
            //    throw new Exception("Erreur de désérialisation ou de conversion pour le champ YourShortField.");
            //}

            return new DemoSoapRequest();
        }
    }
}
