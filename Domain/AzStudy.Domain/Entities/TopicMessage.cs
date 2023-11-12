using Newtonsoft.Json;

namespace AzStudy.Domain.Entities;

public class TopicMessage<T>
{
    public Guid MessageId { get; set; }
    public T Content { get; set; }
    public DateTime CreatedAt { get; set; }
    private Dictionary<string, string> Properties { get; set; }

    public string TopicName { get; set; }
    public string TopicSubscription { get; set; }

    public TopicMessage(T content, string topicName, string topicSubscription)
    {
        MessageId = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Content = content;
        Properties = new Dictionary<string, string>();
        TopicName = topicName;
        TopicSubscription = topicSubscription;
    }

    public void AddProperty(string key, string value) => Properties[key] = value;

    public string? GetProperty(string key) => Properties.TryGetValue(key, out var value) ? value : null;
    
    public bool Validate(out string validationError)
    {
        validationError = string.Empty;

        if (string.IsNullOrWhiteSpace(TopicName))
        {
            validationError = "Topic name is required.";
            return false;
        }

        if (string.IsNullOrWhiteSpace(TopicSubscription))
        {
            validationError = "Topic subscription is required.";
            return false;
        }

        if (Content == null)
        {
            validationError = "Content cannot be null.";
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        string contentString;
        try
        {
            contentString = JsonConvert.SerializeObject(Content);
        }
        catch
        {
            contentString = Content?.ToString() ?? "null";
        }

        var propertiesString = Properties.Any() 
            ? string.Join(", ", Properties.Select(kv => $"{kv.Key}: {kv.Value}")) 
            : "None";

        return $"TopicMessage:\n" +
               $"  MessageId: {MessageId}\n" +
               $"  CreatedAt: {CreatedAt}\n" +
               $"  TopicName: {TopicName}\n" +
               $"  TopicSubscription: {TopicSubscription}\n" +
               $"  Content: {contentString}\n" +
               $"  Properties: {propertiesString}";
    }
}