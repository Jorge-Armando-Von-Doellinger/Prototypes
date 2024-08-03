namespace Gateway.Core.Configs
{
    public static class ClientMessagingSettings
    {
        public static string QueueName { get; } = "prototype-hms-1";
        public static string Exchange { get; } = "prototype-hms-1";
        public static string TypeExchange { get; } = "direct";
        public static string Hostname { get; } = "localhost";
        public static string DeleteClientRouting { get; } = "client.delete";
        public static string PostClientRouting { get; } = "client.post";
        public static string PutClientRouting { get; } = "client.put";
        public static string GetClientRouting { get; } = "client.get";
        public static string GetClientByIDRouting { get; } = "client.get.id";
        public static string ResponseRouting { get; } = "client.response";
    }
}
