using System;
using AzStudy.Domain.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzStudy.FunctionApp.Properties;

public static class AssayTopic
{
    [Function("AssayTopic")]
    public static void Run([ServiceBusTrigger("assays", "sub", Connection = "")] TopicMessage<AssayRequest> mySbMsg,
        FunctionContext context)
    {
        var logger = context.GetLogger("AssayTopic");
        
        if (!mySbMsg.Validate(out var validationError))
        {
            logger.LogError(validationError);
            return;
        }
        
        logger.LogInformation("Message received: {Message}", mySbMsg);
    }
}