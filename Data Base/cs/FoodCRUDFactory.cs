using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Modelo;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;

namespace MVCConAzure.cs
{
    public class FoodCRUDFactory
    {
        private string EndpointUri;
        private string PrimaryKey;
        private string databaseId = "Nutri4U";
        private string containerId = "Alimentos";
        private DocumentClient client;

        public FoodCRUDFactory()
        {
            EndpointUri = "https://componentesgrupo7.documents.azure.com:443/";
            PrimaryKey = "tCNBr5q4pJRqwvGXnZyRu6CZgpsbytw2h36rjHQUq3OSuafEhWJ1hYjgnTZFCbzTnLC7tcAebGoiACDbDapAhQ==";
            Init();
        }
        private void Init()
        {
            try
            {
                client = new DocumentClient(
                    new Uri(EndpointUri),
                    PrimaryKey
                    );
            }
            catch (DocumentClientException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Food>> RetrieveAll()
        {
            var SQL = "Select * from c";
            List<Food> foods = new List<Food>();
            IDocumentQuery<Food> query = client.CreateDocumentQuery<Food>(
                UriFactory.CreateDocumentCollectionUri(databaseId, containerId), SQL)
                .AsDocumentQuery();

            while (query.HasMoreResults)
            {
                foods.AddRange(await query.ExecuteNextAsync<Food>());
            }
            return foods;
        }

        //public async Task<List<Food>> RetrieveAll()
        //{
        //    FeedOptions options = new FeedOptions() { MaxItemCount = -1 };
        //    var SQL = "Select * from c";
        //    Uri uri = UriFactory.CreateDocumentCollectionUri( databaseId, containerId );
        //    IQueryable<Food> consulta = this.client.CreateDocumentQuery<Food>(uri, SQL, options);
        //    return consulta.ToList();
        //}

        public Boolean Create(Food food)
        {
            Boolean insertok = true;

            try
            {

                food.Id = Guid.NewGuid().ToString("N");

                var database = client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseId)).Result.Resource;
                var collection = client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseId, containerId)).Result.Resource;

                var res = client.CreateDocumentAsync(collection.SelfLink, food).Result;

                if (!res.StatusCode.Equals(HttpStatusCode.Created))
                {
                    insertok = false;
                }
            }
            catch (DocumentClientException ex)
            {
                insertok = false;
                throw;
            }
            catch (Exception ex)
            {
                insertok = false;
                throw;
            }

            return insertok;
        }
        public async Task<Food>GetFoodById(string id)
        {
            try
            {
                if (id == null)
                {
                    
                    return null;
                }

               
                id = id.Trim();
                Console.WriteLine($"Reading item with ID: {id}");

                // Query documents with a case-insensitive comparison
                var query = new SqlQuerySpec
                {
                    QueryText = "SELECT * FROM c WHERE LOWER(c.id) = @id",
                    Parameters = new SqlParameterCollection
            {
                new SqlParameter("@id", id.ToLowerInvariant())
            }
                };

                IDocumentQuery<Food> documentQuery = client.CreateDocumentQuery<Food>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, containerId),
                    query,
                    new FeedOptions { EnableCrossPartitionQuery = true })
                    .AsDocumentQuery();

                var food = new List<Food>();
                while (documentQuery.HasMoreResults)
                {
                    food.AddRange(await documentQuery.ExecuteNextAsync<Food>());
                }

                var foundFood = food.FirstOrDefault();
                if (foundFood == null)
                {
                    Console.WriteLine($"Food not found for ID: {id}");
                }
                else
                {
                    Console.WriteLine($"Food found: {JsonConvert.SerializeObject(foundFood)}");
                }

                return foundFood;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in read_item method: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> delete_item(string id)
        {
            bool deleteOk = true;

            try
            {
                var response = await client.DeleteDocumentAsync(
                    UriFactory.CreateDocumentUri(databaseId, containerId, id),
                    new RequestOptions { PartitionKey = new PartitionKey(id) });
            }
            catch (DocumentClientException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    deleteOk = false;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                deleteOk = false;
                throw;
            }

            return deleteOk;
        }

        public async Task<bool> UpdateItem(Food food)
        {
            try
            {
                // Convert the id to lowercase for consistency
                string lowerCaseId = food.Id.ToLowerInvariant();
                Console.WriteLine($"Update action - Received id (lowercase): {lowerCaseId}");

                // Create document URI with the lowercase id
                Uri documentUri = UriFactory.CreateDocumentUri(databaseId, containerId, lowerCaseId);

                // Validate that the user exists before attempting to update
                var existingUser = await read_item(lowerCaseId);
                if (existingUser == null)
                {
                    Console.WriteLine($"User not found for ID: {lowerCaseId}");
                    return false;
                }

                // Perform the update
                var response = await client.ReplaceDocumentAsync(documentUri, food);

                Console.WriteLine($"Updated user with ID: {lowerCaseId}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n{ex.StackTrace}");

                if (ex is DocumentClientException documentClientException)
                {
                    Console.WriteLine($"DocumentClientException StatusCode: {documentClientException.StatusCode}");

                    // Add more details as needed
                }

                return false;
            }
        }

        public async Task<Food> read_item(string id)
        {
            try
            {
                // Trim leading and trailing whitespaces
                id = id.Trim();
                Console.WriteLine($"Reading item with ID: {id}");

                // Query documents with a case-insensitive comparison
                var query = new SqlQuerySpec
                {
                    QueryText = "SELECT * FROM c WHERE LOWER(c.id) = @id",
                    Parameters = new SqlParameterCollection
            {
                new SqlParameter("@id", id.ToLowerInvariant())
            }
                };

                IDocumentQuery<Food> documentQuery = client.CreateDocumentQuery<Food>(
                    UriFactory.CreateDocumentCollectionUri(databaseId, containerId),
                    query,
                    new FeedOptions { EnableCrossPartitionQuery = true })
                    .AsDocumentQuery();

                var users = new List<Food>();
                while (documentQuery.HasMoreResults)
                {
                    users.AddRange(await documentQuery.ExecuteNextAsync<Food>());
                }

                var foundUser = users.FirstOrDefault();
                if (foundUser == null)
                {
                    Console.WriteLine($"User not found for ID: {id}");
                }
                else
                {
                    Console.WriteLine($"User found: {JsonConvert.SerializeObject(foundUser)}");
                }

                return foundUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in read_item method: {ex.Message}");
                throw;
            }
        }


    }
}
