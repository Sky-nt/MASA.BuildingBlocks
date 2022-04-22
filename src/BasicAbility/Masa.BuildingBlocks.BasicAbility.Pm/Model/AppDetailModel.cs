using Masa.BuildingBlocks.BasicAbility.Pm.Enum;

namespace Masa.BuildingBlocks.BasicAbility.Pm.Model
{
    public class AppDetailModel : BaseModel
    {
        public int ProjectId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Identity { get; set; } = "";

        public string Description { get; set; } = "";

        public AppTypes Type { get; set; }

        public ServiceTypes ServiceType { get; set; }

        public string Url { get; set; } = "";

        public string SwaggerUrl { get; set; } = "";

        public List<EnvironmentClusterModel> EnvironmentClusters { get; set; } = new();
    }
}
