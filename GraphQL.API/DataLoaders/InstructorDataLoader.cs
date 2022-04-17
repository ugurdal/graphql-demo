using GraphQL.API.DataAccess.Entity;
using GraphQL.API.Services;

namespace GraphQL.API.DataLoaders;

public class InstructorDataLoader : BatchDataLoader<Guid, Instructor>
{
    private readonly InstructorRepository _instructorRepository;

    public InstructorDataLoader(InstructorRepository instructorRepository, IBatchScheduler batchScheduler, DataLoaderOptions options = null)
    : base(batchScheduler, options)
    {
        this._instructorRepository = instructorRepository;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Instructor>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
        var intructors = await _instructorRepository.GetManyById(keys);
        return intructors.ToDictionary(x => x.Id);
    }
}