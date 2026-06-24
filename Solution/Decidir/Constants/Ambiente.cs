namespace Decidir.Constants
{
    public sealed class Ambiente
    {
        public const int AMBIENTE_SANDBOX = 0;
        public const int AMBIENTE_PRODUCCION = 1;
        public const int AMBIENTE_QA = 2;
        public const int AMBIENTE_DESA = 3;
        public const int AMBIENTE_HOMO = 4;
    }
    public enum EAmbiente
    {
        NA = -1,
        SANDBOX = 0,
        PRD = 1,
        QA = 2,
        DESA = 3,
        HOMO = 4,
    }
    public static class Utils
    {
        public static int GetAmbiente(this EAmbiente environment)
        {
            switch (environment)
            {
                case EAmbiente.NA:
                    return -1;
                case EAmbiente.SANDBOX:
                    return Ambiente.AMBIENTE_SANDBOX;
                case EAmbiente.PRD:
                    return Ambiente.AMBIENTE_PRODUCCION;
                case EAmbiente.QA:
                    return Ambiente.AMBIENTE_QA;
                case EAmbiente.DESA:
                    return Ambiente.AMBIENTE_DESA;
                case EAmbiente.HOMO:
                    return Ambiente.AMBIENTE_HOMO;
                default:
                    return -1;

            }
        }
    }
      
}
