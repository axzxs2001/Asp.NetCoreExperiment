namespace AutoUpdateService
{
    public class UpgradeSettingModel
    {
        public bool Enable { get; set; }
        public string? CronExpression { get; set; }
        public string? UpgradeServerUrl { get; set; }

        public string? SelfUrl { get; set; }
        public string? ZipFile { get; set; }
    }
}
