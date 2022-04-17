using HotChocolate.Data.Filters;

namespace GraphQL.API.Schema.Filters;

public class CourseFilterType : FilterInputType<CourseType>
{
    protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
    {
        descriptor.Ignore(s => s.Students);

        base.Configure(descriptor);
    }
}