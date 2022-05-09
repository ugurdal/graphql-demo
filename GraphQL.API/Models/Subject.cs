namespace GraphQL.API.Models;

/// <summary>
/// Tum enum'lari UPPERCASE yazmak daha mantıklı, GraphQLName attribute yoksa Türkçe karakter problemi yasaniyor
/// </summary>
public enum Subject
{
    [GraphQLName("Mathematics")]
    Mathematics,

    [GraphQLName("Science")]
    Science,

    [GraphQLName("History")]
    History
}