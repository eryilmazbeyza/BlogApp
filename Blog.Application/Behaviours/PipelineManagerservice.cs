
namespace Blog.Application.Behaviours;

public class PipelineSetting
{
    public bool Skip { get; set; }
}

public class PipelineManagerService : IPipelineManagerService
{
    private Dictionary<Type, PipelineSetting> Pipelines { get; set; } = new Dictionary<Type, PipelineSetting>();

    public bool IsSkipPipeline(Type pipelineToSkip)
    {
        if (!Pipelines.ContainsKey(pipelineToSkip))
            return false;
        return Pipelines[pipelineToSkip].Skip;
    }

    public void SkipPipelines(params Type[] pipelines)
    {
        foreach (var pipeline in pipelines)
        {
            if (!Pipelines.ContainsKey(pipeline))
                Pipelines.Add(pipeline, new PipelineSetting());

            Pipelines[pipeline].Skip = true;
        }
    }
}