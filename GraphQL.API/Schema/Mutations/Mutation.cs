using GraphQL.API.Schema.Subscriptions;
using GraphQL.API.Services;
using HotChocolate.Subscriptions;

namespace GraphQL.API.Schema.Mutations;

public class Mutation
{
    private readonly CourseRepository _courseRepository;

    public Mutation(CourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<CourseType> CreateCourse([Service] ITopicEventSender eventSender, CourseInputType input)
    {
        var course = await _courseRepository.CreateCourse(new DataAccess.Entity.Course
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        });

        var result = new CourseType
        {
            Id = course.Id,
            Name = course.Name,
            // Instructor = new InstructorType { Id = course.Id },
            Subject = course.Subject
        };

        // publish event
        await eventSender.SendAsync(nameof(Subscription.CourseCreated), result);

        return result;
    }

    public async Task<CourseType> UpdateCourse([Service] ITopicEventSender eventSender, Guid id, CourseInputType input)
    {
        var course = await _courseRepository.UpdateCourse(new DataAccess.Entity.Course
        {
            Id = Guid.NewGuid(),
            Name = input.Name,
            Subject = input.Subject,
            InstructorId = input.InstructorId
        });

        var result = new CourseType
        {
            Id = course.Id,
            Name = course.Name,
            // Instructor = new InstructorType { Id = course.Id },
            Subject = course.Subject
        };

        var updateCourseTopic = $"{result.Id}_{nameof(Subscription.CourseUpdated)}";
        await eventSender.SendAsync(updateCourseTopic, result);

        return result;
    }

    public async Task<bool> DeleteCourse(Guid id)
    {
        return await _courseRepository.DeleteCourse(id);
    }
}