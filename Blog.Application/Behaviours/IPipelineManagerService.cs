
namespace Blog.Application.Behaviours;

public interface IPipelineManagerService
{
    bool IsSkipPipeline(Type pipelineToSkip);
    void SkipPipelines(params Type[] pipelinesToSkip);
}