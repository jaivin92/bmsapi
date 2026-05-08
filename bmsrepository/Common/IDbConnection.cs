using bmslib.Config;

namespace bmsrepository.Common
{
    public class IDbConnection(AppConfig appConfig) : CIDbConnection.Core.IDbConnection(appConfig.ConnectionStrings.CMSDBConnection)
    {


    }
}
