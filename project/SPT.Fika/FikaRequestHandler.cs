using SPT.Launcher.MiniCommon;

namespace SPT.Fika 
{
    public static class RequestHandler
    {
        private static Request request = new Request(null, "");
        public static string RequestOnlinePlayers()
        {
            return request.GetJson("/fika/presence/get");
        }

        public static string RequestDedicatedClientStatus()
        {
            return request.GetJson("/fika/headless/available");
        }
    }
}