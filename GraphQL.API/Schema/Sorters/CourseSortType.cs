using HotChocolate.Data.Sorting;

namespace GraphQL.API.Schema.Sorters;

public class CourseSortType : SortInputType<CourseType>
{
    protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
    {
        descriptor.Ignore(s => s.Students);
        descriptor.Ignore(s => s.Id);
        descriptor.Ignore(s => s.InstructorId);

        //descriptor.Field(s => s.Name).Name("CourseName");

        base.Configure(descriptor);
    }
}