using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace AspNetCoreSoapAuthBasicService.Middlewares
{
    public class ValidateSoapRequestMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly string[] _xsdFilePaths;

        public ValidateSoapRequestMiddleware(RequestDelegate next)
        {
            _next = next;

            _xsdFilePaths = new string[]
            {
                "C:\\GH-ORDI\\GH-DEV\\LGS\\AspNetCoreSoapAuthBasicService\\AspNetCoreSoapAuthBasicService\\RecevoirSIPGIntrant.xsd",
                 "C:\\GH-ORDI\\GH-DEV\\LGS\\AspNetCoreSoapAuthBasicService\\AspNetCoreSoapAuthBasicService\\Serialisation.xsd"
            };
            //_xsdFilePaths = xsdFilePaths;
        }

        //public ValidateSoapRequestMiddleware(RequestDelegate next, string[] xsdFilePaths)
        //{
        //    _next = next;

        //    _xsdFilePaths = xsdFilePaths;
        //}

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.Equals("POST") && context.Request.ContentType == "text/xml;charset=UTF-8")
            {
                try
                {
                    // Charger les schémas XSD
                    XmlSchemaSet schemas = new XmlSchemaSet();
                    foreach (var xsdFilePath in _xsdFilePaths)
                    {
                        using (FileStream fs = new FileStream(xsdFilePath, FileMode.Open))
                        {
                            XmlSchema schema = XmlSchema.Read(fs, null);
                            schemas.Add(schema);
                        }
                    }

                    XDocument xmlDoc;
                    // Charger la requête SOAP dans un XmlDocument
                    using (StreamReader reader = new StreamReader(context.Request.Body))
                    {
                        string xmlContent = await reader.ReadToEndAsync();
                        xmlDoc = XDocument.Parse(xmlContent);

                        // Traitez le XDocument selon vos besoins
                    }

                    // Valider le document XML par rapport aux schémas XSD
                    bool isValid = true;

                    xmlDoc.Validate(schemas, (sender, e) => isValid = false);
                    //xmlDoc.Validate(schemas, (sender, e) =>
                    //{
                    //    isValid = false;
                    //});


                    if (!isValid)
                    {
                        // Si la validation échoue, renvoyer une réponse avec une erreur 400 (Bad Request)
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("La requête SOAP ne respecte pas la structure XSD spécifiée.");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    // En cas d'erreur lors de la validation, renvoyer une réponse avec une erreur 500 (Internal Server Error)
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync($"Une erreur s'est produite lors de la validation de la requête SOAP : {ex.Message}");
                    return;
                }
            }

            // Passer la requête au prochain middleware dans le pipeline
            await _next(context);
        }
    }
}
