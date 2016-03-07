

namespace Transpilation.ModelBuilders
{
    public interface IBuildModels
    {
        string BuildModel(object instance, TranspileOptions options);
    }
}
