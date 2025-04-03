namespace User.Api.Core
{
    public class GetUserBusiness
    {
        public GetUserBusiness()
        {
        }

        public string Process(string userName)
        {
            try
            {
                return Datebase.Instance.GetJson(userName);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar la solicitud", ex);
            }
        }

    }
}
