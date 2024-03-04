namespace AspNetCoreSoapAuthBasicService.SoapServices
{
    public class BasicAuthDemoSoapService : IBasicAuthDemoSoapService
    {

        public DemoSoapReponse ObtenirDemoSoapReponse(DemoSoapRequest request)
        {
            var resultat = new DemoSoapReponse
            { 
                CodErreur = "0",
                CodRetour = EnumCodRetour.Succes,
                MessageErreur = "Sans Erreur"
            };

            return resultat;
        }
    }
}
