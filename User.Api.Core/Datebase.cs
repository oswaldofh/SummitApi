namespace User.Api.Core
{
    public abstract class Datebase
    {
        private static Datebase? _instance;
        public static Datebase? Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("No se ha inicializado la base de datos");
                }
                return _instance;
            }
             set
            {
                _instance = value;
            }
        }

        public abstract void ExecuteNonQuery(User user);
        public abstract string GetJson(string userName);

        protected static void Initialize(Datebase datebase)
        {
            if (Instance != null)
            {
                throw new Exception("La base de datos ya ha sido inicializada");
            }
            Instance = datebase;
        }

    }
}
