using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQL.API.Schema.Subscriptions;

public class Subscription
{
    [Subscribe]
    public CourseType CourseCreated([EventMessage] CourseType course) => course;

    [SubscribeAndResolve]
    public ValueTask<ISourceStream<CourseType>> CourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
    {
        var topicName = $"{courseId}_{nameof(Subscription.CourseUpdated)}";

        return topicEventReceiver.SubscribeAsync<string, CourseType>(topicName);
    }
}