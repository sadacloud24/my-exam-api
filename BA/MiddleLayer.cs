using Microsoft.Azure.Cosmos;
using MyExamApi.Models;

namespace MyExamApi.BA
{
    public class MiddleLayer : IMiddleLayer
    {
        private const string EndpointUrl = "your_cosmos_db_endpoint";
        private const string PrimaryKey = "your_cosmos_db_primary_key";
        private const string DatabaseId = "YourDatabaseId";
        private const string ContainerId = "YourContainerId";

        public async Task AddQuestionAsync(QuestionRequest questionRequest)
        {
            Container container = await GetContainerAsync();

            var question = GetQuestion(questionRequest);
            await container.CreateItemAsync(question, new PartitionKey(questionRequest.Id));
        }

        public async Task<IEnumerable<object>> GetQuestionsAsync(string type)
        {
            List<object> list = new List<object>();
            Container container = await GetContainerAsync();
            var query = container.GetItemQueryIterator<QuestionBase>(new QueryDefinition($"SELECT * FROM c WHERE c.Type = '{type}'"));
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                foreach (var item in response)
                {
                    if (item is DateQuestion dateQuestion)
                    {
                        // Process date questions
                    }
                }
            }
            return list;
        }

        public async Task UpdateQuestionAsync(QuestionRequest questionRequest)
        {
            Container container = await GetContainerAsync();

            var question = GetQuestion(questionRequest);
            await container.UpsertItemAsync(question, new PartitionKey(questionRequest.Id));
        }

        private async Task<Container> GetContainerAsync()
        {
            CosmosClient cosmosClient = new CosmosClient(EndpointUrl, PrimaryKey);
            Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseId);
            return await database.CreateContainerIfNotExistsAsync(ContainerId, "/PartitionKey");
        }

        private object? GetQuestion(QuestionRequest questionRequest)
        {
            switch (questionRequest.Type)
            {
                case "Paragraph":
                    return new ParagraphQuestion
                    {
                        Id = questionRequest.Id,
                        Type = questionRequest.Type,
                        Text = questionRequest.ParagraphQuestion!.Text
                    };
                case "YesNo":
                    return new YesNoQuestion
                    {
                        Id = questionRequest.Id,
                        Type = questionRequest.Type,
                        Answer = questionRequest.YesNoQuestion!.Answer
                    };
                case "Date":
                    return new DateQuestion
                    {
                        Id = questionRequest.Id,
                        Type = questionRequest.Type,
                        DateValue = questionRequest.DateQuestion!.DateValue
                    };
                case "Number":
                    return new NumberQuestion
                    {
                        Id = questionRequest.Id,
                        Type = questionRequest.Type,
                        NumericValue = questionRequest.NumberQuestion!.NumericValue
                    };
                case "DropdownQuestion":
                    return new DropdownQuestion
                    {
                        Id = questionRequest.Id,
                        Type = questionRequest.Type,
                        Options = questionRequest.DropdownQuestion!.Options
                    };

                default:
                    return default(QuestionBase);
            }
        }
    }
}
