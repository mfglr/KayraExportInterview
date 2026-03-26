using MediatR;
using System.Text.Json.Serialization;

namespace LogService.Application.Commands.CreateLog
{
    [method: JsonConstructor]
    public record CreateLogCommandRequest_Exception(
        string Message,
        string StackTrace,
        CreateLogCommandRequest_Exception? InnerException
    );

    [method: JsonConstructor]
    public record CreateLogCommandRequest(
        string ServiceName,
        DateTime TimeStamp,
        string Level,
        string MessageTemplate,
        string? TraceId,
        string? Controller,
        string? Action,
        CreateLogCommandRequest_Exception? Exception
    ) : IRequest;
}
